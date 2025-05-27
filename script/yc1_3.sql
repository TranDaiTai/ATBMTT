--Hàm tổng POLICY_SINHVIEN – phân luồng
CREATE OR REPLACE FUNCTION POLICY_SINHVIEN (
    schema_name IN VARCHAR2,
    table_name  IN VARCHAR2
)
RETURN VARCHAR2
IS
    v_user VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_count NUMBER;
BEGIN
    -- Kiểm tra có trong bảng SINHVIEN không
    SELECT COUNT(*) INTO v_count
    FROM SINHVIEN
    WHERE MASV = v_user;

    IF v_count = 1 THEN
        RETURN POLICY_SINHVIEN_SV(schema_name, table_name);
    ELSE
        RETURN POLICY_SINHVIEN_NV(schema_name, table_name);
    END IF;
END;
/

-- POLICY_SINHVIEN_SV – hàm cho sinh viên
CREATE OR REPLACE FUNCTION POLICY_SINHVIEN_SV (
    schema_name IN VARCHAR2,
    table_name  IN VARCHAR2
)
RETURN VARCHAR2
IS
    v_user VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_exists NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_exists
    FROM SINHVIEN
    WHERE MASV = v_user;

    IF v_exists = 1 THEN
        RETURN 'MASV = ''' || v_user || '''';
    ELSE
        RETURN '1=0'; -- Không phải sinh viên
    END IF;
END;
/
--POLICY_SINHVIEN_NV – hàm cho giảng viên/nhân viên

CREATE OR REPLACE FUNCTION POLICY_SINHVIEN_NV (
    schema_name IN VARCHAR2,
    table_name  IN VARCHAR2
)
RETURN VARCHAR2
IS
    v_user VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_role NHANVIEN.VAITRO%TYPE;
    v_khoa NHANVIEN.MADV%TYPE;
BEGIN
    SELECT VAITRO, MADV INTO v_role, v_khoa
    FROM NHANVIEN
    WHERE MANV = v_user;

    IF v_role = 'GV' THEN
        RETURN 'KHOA = ''' || v_khoa || '''';
    ELSIF v_role = 'NV PCTSV' THEN
        RETURN '1=1'; -- toàn quyền trừ TINHTRANG (kiểm soát bằng trigger)
    ELSIF v_role = 'NV PĐT' THEN
        RETURN '1=1'; -- toàn quyền (giao cho trigger kiểm TINHTRANG)
    ELSE
        RETURN '1=0'; -- không có quyền
    END IF;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN '1=0'; -- Không phải nhân viên
END;
/


-- add policy to table SINHVIEN
BEGIN
  DBMS_RLS.ADD_POLICY (
    object_schema   => 'QLDL',  -- hoặc schema của em
    object_name     => 'SINHVIEN',
    policy_name     => 'POLICY_ACCESS_SINHVIEN',
    function_schema => 'QLDL',  -- hoặc schema của hàm
    policy_function => 'POLICY_SINHVIEN',
    statement_types => 'SELECT, INSERT, UPDATE, DELETE',
    update_check    => TRUE
  );
END;
/


--Tạo Trigger kiểm soát cập nhật trường TINHTRANG
--Sinh viên không được sửa TINHTRANG.
--NV PCTSV không được sửa TINHTRANG.
--Chỉ NV PĐT được sửa TINHTRANG.
CREATE OR REPLACE TRIGGER trg_block_update_TINHTRANG
BEFORE UPDATE OF TINHTRANG ON SINHVIEN
FOR EACH ROW
DECLARE
    v_user  VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_role  NHANVIEN.VAITRO%TYPE;
BEGIN
    -- Lấy vai trò người dùng
    BEGIN
        SELECT VAITRO INTO v_role
        FROM NHANVIEN
        WHERE MANV = v_user;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            -- Nếu không tìm thấy trong NHANVIEN thì có thể là SINHVIEN → không cho sửa
            RAISE_APPLICATION_ERROR(-20001, 'Không được phép sửa trường TINHTRANG');
    END;

    -- Chỉ cho NV PĐT sửa
    IF v_role != 'NV PĐT' THEN
        RAISE_APPLICATION_ERROR(-20002, 'Chỉ NV PĐT được sửa trường TINHTRANG');
    END IF;
END;
/

-- Trigger ngăn Sinh viên sửa các cột ngoài DCHI, DT
CREATE OR REPLACE TRIGGER trg_sv_limit_update
BEFORE UPDATE ON SINHVIEN
FOR EACH ROW
DECLARE
    v_user  VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_count NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_count FROM SINHVIEN WHERE MASV = v_user;

    IF v_count = 1 THEN
        -- Là sinh viên → chỉ được sửa DCHI, DT
        IF :OLD.MASV = v_user THEN
            IF (:OLD.HOTEN <> :NEW.HOTEN OR
                :OLD.PHAI <> :NEW.PHAI OR
                :OLD.NGSINH <> :NEW.NGSINH OR
                :OLD.KHOA <> :NEW.KHOA OR
                :OLD.TINHTRANG <> :NEW.TINHTRANG) THEN
                RAISE_APPLICATION_ERROR(-20003, 'Sinh viên chỉ được sửa ĐCHI, ĐT');
            END IF;
        END IF;
    END IF;
END;
/
