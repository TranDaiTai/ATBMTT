-- **PHẦN HỆ 1: ỨNG DỤNG QUẢN TRỊ CSDL ORACLE**
-- **Dành cho người quản trị cơ sở dữ liệu**

-- **1. Tạo các procedure hỗ trợ kiểm tra và xóa đối tượng nếu tồn tại**
-- Procedure kiểm tra user hoặc role có tồn tại hay không (trả về 0: không tồn tại, 1: là user, 2: là role)

alter session set container = QLDULIEUNOIBO;
alter session set CURRENT_SCHEMA = QLDL ;
CREATE OR REPLACE PROCEDURE QLDL.check_user_role_exist(
    user_role IN CHAR,
    result_ OUT NUMBER
)
AS
    che INT;
BEGIN
    SELECT COUNT(*) INTO che 
    FROM DBA_ROLES 
    WHERE role = user_role;
    IF che > 0 THEN
        result_ := 2;
    ELSE 
        SELECT COUNT(*) INTO che 
        FROM DBA_USERS 
        WHERE username = user_role;
        IF che > 0 THEN
            result_ := 1;
        ELSE
            result_ := 0;
        END IF;
    END IF;
END;
/

-- Procedure xóa user nếu tồn tại
CREATE OR REPLACE PROCEDURE QLDL.drop_user(
    user_name IN CHAR
)
IS
BEGIN
    EXECUTE IMMEDIATE 'DROP USER ' || user_name || ' CASCADE';
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -01918 THEN
            RAISE;
        END IF;
END;
/

-- Procedure xóa role nếu tồn tại
CREATE OR REPLACE PROCEDURE QLDL.drop_role(
    role_name IN CHAR
)
IS
BEGIN
    EXECUTE IMMEDIATE 'DROP ROLE ' || role_name;
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -01919 THEN
            RAISE;
        END IF;
END;
/

-- Procedure xóa view nếu tồn tại
CREATE OR REPLACE PROCEDURE QLDL.drop_view(
    view_name IN CHAR
)
IS
BEGIN
    EXECUTE IMMEDIATE 'DROP VIEW ' || view_name;
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -00942 THEN
            RAISE;
        END IF;
END;
/

-- Procedure xóa procedure nếu tồn tại
CREATE OR REPLACE PROCEDURE QLDL.drop_proc(
    proc_name IN CHAR
)
IS
BEGIN
    EXECUTE IMMEDIATE 'DROP PROCEDURE ' || proc_name;
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -04043 THEN
            RAISE;
        END IF;
END;
/

-- **2. Quản lý user và role (Tạo, Xóa, Sửa)**

-- Tạo user
CREATE OR REPLACE PROCEDURE QLDL.create_user(
    p_username IN VARCHAR2, 
    p_password IN VARCHAR2
)
IS
BEGIN
    EXECUTE IMMEDIATE 'CREATE USER ' || p_username || ' IDENTIFIED BY ' || p_password;
END;
/

-- Xóa user
CREATE OR REPLACE PROCEDURE QLDL.delete_user(
    p_username IN VARCHAR2
)
IS
BEGIN
    EXECUTE IMMEDIATE 'DROP USER ' || p_username;
END;
/

-- Đổi mật khẩu user
CREATE OR REPLACE PROCEDURE QLDL.change_password (
    p_username IN VARCHAR2,
    p_new_password IN VARCHAR2
)
AS
    v_user_count NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO v_user_count
    FROM dba_users
    WHERE username = p_username;
   
    IF v_user_count = 0 THEN
        RAISE_APPLICATION_ERROR(-20001, 'User does not exist');
    END IF;
   
    EXECUTE IMMEDIATE 'ALTER USER ' || p_username || ' IDENTIFIED BY ' || p_new_password;
    DBMS_OUTPUT.PUT_LINE('Password changed successfully');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/

-- Tạo role
CREATE OR REPLACE PROCEDURE QLDL.create_role(
    p_role_name IN VARCHAR2,
    p_password IN VARCHAR2 DEFAULT NULL
)
IS
BEGIN
    IF p_password IS NULL THEN
        EXECUTE IMMEDIATE 'CREATE ROLE ' || p_role_name;
    ELSE
        EXECUTE IMMEDIATE 'CREATE ROLE ' || p_role_name || ' IDENTIFIED BY ' || p_password;
    END IF;
END;
/

-- Xóa role
CREATE OR REPLACE PROCEDURE QLDL.delete_role (
    p_role_name IN VARCHAR2
)
AS
BEGIN
    EXECUTE IMMEDIATE 'DROP ROLE ' || p_role_name;
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20001, 'Error deleting role: ' || SQLERRM);
END;
/

-- Đổi mật khẩu role
CREATE OR REPLACE PROCEDURE QLDL.change_role_password(
    p_role_name IN VARCHAR2,
    p_new_password IN VARCHAR2
)
IS
    role_exists NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO role_exists
    FROM dba_roles
    WHERE role = p_role_name;

    IF role_exists > 0 THEN
        IF (p_new_password IS NULL) THEN
            EXECUTE IMMEDIATE 'ALTER ROLE ' || p_role_name || ' NOT IDENTIFIED';
            DBMS_OUTPUT.PUT_LINE('Password for role ' || p_role_name || ' has been removed.');
        ELSE
            EXECUTE IMMEDIATE 'ALTER ROLE ' || p_role_name || ' IDENTIFIED BY ' || p_new_password;
            DBMS_OUTPUT.PUT_LINE('Password for role ' || p_role_name || ' has been changed.');
        END IF;
    ELSE
        DBMS_OUTPUT.PUT_LINE('Role ' || p_role_name || ' does not exist.');
    END IF;
END;
/

-- **3. Cấp quyền cho user/role**

-- Cấp role cho user
CREATE OR REPLACE PROCEDURE QLDL.grant_role(
    user_name IN CHAR,
    role_name IN CHAR,
    withadminoption IN CHAR,
    result_ OUT NUMBER
)
AS
    user_role_exist INT;
BEGIN
    QLDL.check_user_role_exist(user_name, user_role_exist);
    IF user_role_exist = 0 THEN
        result_ := -1;
        RETURN;
    END IF;
    
    QLDL.check_user_role_exist(role_name, user_role_exist);
    IF user_role_exist = 0 OR user_role_exist = 1 THEN
        result_ := -2;
        RETURN;
    END IF;
    
    EXECUTE IMMEDIATE 'GRANT ' || role_name || ' TO ' || user_name || ' ' || withadminoption;
    result_ := 3;
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -01918 THEN
            RAISE;
        END IF;
END;
/

-- Cấp quyền SELECT trên cột của bảng (tạo view để cấp quyền)
CREATE OR REPLACE PROCEDURE QLDL.grant_select_privilege(
    user_role IN CHAR,
    schema_name IN CHAR,
    table_name IN CHAR,
    column_name IN CHAR,
    withgrantoption IN CHAR
)
AS
    view_string CHAR(100);
BEGIN
    view_string := 'V_' || table_name || '_' || user_role;
    QLDL.drop_view(schema_name || '.' || view_string);
    EXECUTE IMMEDIATE 'CREATE OR REPLACE VIEW ' || schema_name || '.' || view_string || ' AS SELECT ' || column_name || ' FROM ' || schema_name || '.' || table_name;
    EXECUTE IMMEDIATE 'GRANT SELECT ON ' || schema_name || '.' || view_string || ' TO ' || user_role || ' ' || withgrantoption;   
END;
/

-- Cấp quyền UPDATE trên cột của bảng
CREATE OR REPLACE PROCEDURE QLDL.grant_update_privilege(
    user_role IN CHAR,
    table_name IN CHAR,
    column_name IN CHAR,
    withgrantoption IN CHAR
)
IS
BEGIN
    EXECUTE IMMEDIATE 'GRANT UPDATE (' || column_name || ') ON ' || table_name || ' TO ' || user_role || ' ' || withgrantoption;
END;
/

-- Cấp quyền INSERT trên bảng
CREATE OR REPLACE PROCEDURE QLDL.grant_insert_privilege(
    user_role IN CHAR,
    table_name IN CHAR,
    withgrantoption IN CHAR
)
IS
BEGIN
    EXECUTE IMMEDIATE 'GRANT INSERT ON ' || table_name || ' TO ' || user_role || ' ' || withgrantoption;
END;
/

-- Cấp quyền DELETE trên bảng
CREATE OR REPLACE PROCEDURE QLDL.grant_delete_privilege(
    user_role IN CHAR,
    table_name IN CHAR,
    withgrantoption IN CHAR
)
IS
BEGIN
    EXECUTE IMMEDIATE 'GRANT DELETE ON ' || table_name || ' TO ' || user_role || ' ' || withgrantoption;
END;
/

-- **4. Thu hồi quyền từ user/role**

-- Thu hồi quyền từ user/role
CREATE OR REPLACE PROCEDURE QLDL.revoke_privil(
    username IN VARCHAR2,
    table_view IN VARCHAR2,
    privil IN VARCHAR2,
    result_ OUT INT
)
IS
BEGIN  
    EXECUTE IMMEDIATE 'REVOKE ' || privil || ' ON ' || table_view || ' FROM ' || username;
    result_ := 1;
EXCEPTION
    WHEN OTHERS THEN
        result_ := 0;
END;
/

-- Thu hồi role từ user
CREATE OR REPLACE PROCEDURE QLDL.revoke_role(
    user_name IN VARCHAR2,
    role_name IN VARCHAR2,
    result_ OUT INT
)
IS
    user_role_exist INT;
    res INT;
BEGIN
    QLDL.check_user_role_exist(user_name, user_role_exist);
    IF user_role_exist = 0 THEN
        result_ := -1;
        RETURN;
    END IF;
    
    QLDL.check_user_role_exist(role_name, user_role_exist);
    IF user_role_exist = 0 OR user_role_exist = 1 THEN
        result_ := -2;
        RETURN;
    END IF;
    
    SELECT COUNT(*) INTO res 
    FROM dba_role_privs 
    WHERE grantee = user_name AND granted_role = role_name;
    
    IF res = 0 THEN
        result_ := -3;
        RETURN;
    END IF;
    
    EXECUTE IMMEDIATE 'REVOKE ' || role_name || ' FROM ' || user_name;
    result_ := 3;
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -01918 THEN
            RAISE;
        END IF;
END;
/

-- **5. Xem thông tin về user, role và quyền**
--
---- Xem danh sách user
--SELECT * FROM ALL_USERS;
--
---- Xem danh sách role của user
--SELECT * FROM dba_role_privs WHERE grantee = :username;
--
---- Xem quyền hệ thống của user hoặc role
--SELECT * FROM dba_sys_privs WHERE grantee = :username;
--
---- Xem các view và table đã tạo
--SELECT * FROM ALL_OBJECTS 
--WHERE (OBJECT_TYPE = 'VIEW' OR OBJECT_TYPE = 'TABLE') AND OWNER = 'QLDA';
--
---- Xem các thuộc tính của một bảng hoặc view
--SELECT * FROM ALL_TAB_COLUMNS 
--WHERE TABLE_NAME = :table_name AND OWNER = 'QLDA';
--
---- Xem các procedure đã tạo
--SELECT * FROM ALL_OBJECTS 
--WHERE OBJECT_TYPE = 'PROCEDURE';
--
---- Xem quyền của user/role (UPDATE, INSERT, DELETE, v.v.)
--SELECT * FROM DBA_TAB_PRIVS 
--WHERE GRANTEE = :username;
--
---- Xem thuộc tính trên một view mà user có quyền
--SELECT * FROM DBA_COL_PRIVS 
--WHERE GRANTEE = :username;
--
---- Xem danh sách table
--SELECT * FROM dba_tables 
--WHERE owner = 'QLDA';
--
---- Xem danh sách view
--SELECT * FROM dba_views 
--WHERE owner = 'QLDA';
--
---- Xem cột của một bảng
--OPEN :result FOR
--SELECT COLUMN_NAME 
--FROM dba_tab_columns 
--WHERE table_name = 'QLDA_PHONGBAN';
--
---- Lấy user hiện tại
--SELECT sys_context('userenv', 'current_user') FROM dual;
--
---- Lấy owner của table
--SELECT * FROM dba_tables 
--WHERE owner = 'QLDA';