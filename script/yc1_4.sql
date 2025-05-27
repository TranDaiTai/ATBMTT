--Tạo VIEW tạm 
CREATE OR REPLACE VIEW QLDL.V_DANGKY AS
SELECT * FROM QLDL.DANGKY;
--Tạo hàm kiểm tra quyền truy cập VPD
--a. Hàm cho sinh viên
CREATE OR REPLACE FUNCTION POLICY_DANGKY_SV (
    schema_name IN VARCHAR2,
    table_name  IN VARCHAR2
)
RETURN VARCHAR2
IS
    v_user VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_count NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_count FROM SINHVIEN WHERE MASV = v_user;

    IF v_count = 1 THEN
        RETURN 'MASV = ''' || v_user || '''';
    ELSE
        RETURN '1=0'; -- không phải sinh viên
    END IF;
END;
/

--b. Hàm cho NV PĐT
CREATE OR REPLACE FUNCTION POLICY_DANGKY_NV_PDT (
    schema_name IN VARCHAR2,
    table_name  IN VARCHAR2
)
RETURN VARCHAR2
IS
    v_user VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_role VARCHAR2(30);
BEGIN
    SELECT VAITRO INTO v_role FROM NHANVIEN WHERE MANV = v_user;

    IF v_role = 'NV PĐT' THEN
        RETURN '1=1'; -- toàn quyền trong 14 ngày đầu
    ELSE
        RETURN '1=0';
    END IF;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN '1=0';
END;
/
--c. Hàm cho GV
CREATE OR REPLACE FUNCTION POLICY_DANGKY_GV (
    schema_name IN VARCHAR2,
    table_name  IN VARCHAR2
)
RETURN VARCHAR2
IS
    v_user VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
BEGIN
    RETURN 'MAMM IN (SELECT MAMM FROM MOMON WHERE MAGV = ''' || v_user || ''')';
END;
/
--d. Hàm cho NV PKT
CREATE OR REPLACE FUNCTION POLICY_DANGKY_NV_PKT (
    schema_name IN VARCHAR2,
    table_name  IN VARCHAR2
)
RETURN VARCHAR2
IS
    v_user VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_role VARCHAR2(30);
BEGIN
    SELECT VAITRO INTO v_role FROM NHANVIEN WHERE MANV = v_user;

    IF v_role = 'NV PKT' THEN
        RETURN '1=1';
    ELSE
        RETURN '1=0';
    END IF;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN '1=0';
END;
/

--Hàm tổng điều phối phân quyền truy cập
CREATE OR REPLACE FUNCTION POLICY_DANGKY (
    schema_name IN VARCHAR2,
    table_name  IN VARCHAR2
)
RETURN VARCHAR2
IS
    v_user VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_role VARCHAR2(30);
    v_count NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_count FROM SINHVIEN WHERE MASV = v_user;
    IF v_count = 1 THEN
        RETURN POLICY_DANGKY_SV(schema_name, table_name);
    END IF;

    BEGIN
        SELECT VAITRO INTO v_role FROM NHANVIEN WHERE MANV = v_user;
        IF v_role = 'NV PĐT' THEN
            RETURN POLICY_DANGKY_NV_PDT(schema_name, table_name);
        ELSIF v_role = 'NV PKT' THEN
            RETURN POLICY_DANGKY_NV_PKT(schema_name, table_name);
        ELSIF v_role = 'GV' THEN
            RETURN POLICY_DANGKY_GV(schema_name, table_name);
        ELSE
            RETURN '1=0';
        END IF;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RETURN '1=0';
    END;
END;
/

-- Gán VPD vào quan hệ DANGKY
BEGIN
  DBMS_RLS.ADD_POLICY (
    object_schema   => 'QLDL',
    object_name     => 'DANGKY',
    policy_name     => 'POLICY_ACCESS_DANGKY',
    function_schema => 'QLDL',
    policy_function => 'POLICY_DANGKY',
    statement_types => 'SELECT, INSERT, UPDATE, DELETE',
    update_check    => TRUE
  );
END;
/
--Tạo trigger kiểm soát
--a. Không cho SV/NV PĐT cập nhật điểm (DIEM_CC, DIEM_GK, DIEM_CK)
CREATE OR REPLACE TRIGGER trg_block_update_diem
BEFORE UPDATE OF DIEMCK, DIEMTK, DIEMTH,DIEMQT ON DANGKY
FOR EACH ROW
DECLARE
    v_user  VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_role  VARCHAR2(30);
    v_count NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_count FROM SINHVIEN WHERE MASV = v_user;

    IF v_count = 1 THEN
        -- là sinh viên → chặn sửa điểm
        RAISE_APPLICATION_ERROR(-20010, 'Sinh viên không được sửa điểm');
    END IF;

    BEGIN
        SELECT VAITRO INTO v_role FROM NHANVIEN WHERE MANV = v_user;
        IF v_role = 'NV PĐT' THEN
            RAISE_APPLICATION_ERROR(-20011, 'NV PĐT không được cập nhật điểm');
        END IF;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN NULL;
    END;
END;
/
--Gán quyền truy cập
-- SV
GRANT SELECT, INSERT, UPDATE, DELETE ON QLDL.V_DANGKY TO ROLE_SINHVIEN;

-- GV
GRANT SELECT ON QLDL.V_DANGKY TO ROLE_GV;

-- NV PĐT
GRANT SELECT, INSERT, UPDATE, DELETE ON QLDL.DANGKY TO ROLE_NV_PDT;

-- NV PKT
GRANT SELECT, UPDATE(DIEMTH, DIEMQT, DIEMCK,DIEMTK) ON QLDL.DANGKY TO ROLE_NV_PKT;

-- Cấp quyền thực thi hàm
GRANT EXECUTE ON QLDL.POLICY_DANGKY TO PUBLIC;
GRANT EXECUTE ON QLDL.POLICY_DANGKY_SV TO PUBLIC;
GRANT EXECUTE ON QLDL.POLICY_DANGKY_GV TO PUBLIC;
GRANT EXECUTE ON QLDL.POLICY_DANGKY_NV_PDT TO PUBLIC;
GRANT EXECUTE ON QLDL.POLICY_DANGKY_NV_PKT TO PUBLIC;
