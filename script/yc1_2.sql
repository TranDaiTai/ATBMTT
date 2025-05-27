--Tạo policy VPD cho từng vai trò
--Policy cho GV
CREATE OR REPLACE FUNCTION restrict_momon_gv (
    p_schema IN VARCHAR2,
    p_object IN VARCHAR2
) RETURN VARCHAR2 AS
BEGIN
    RETURN 'MAGV = ''' || SYS_CONTEXT('USERENV', 'SESSION_USER') || '''';
END;
/ 
BEGIN
    DBMS_RLS.ADD_POLICY (
        object_schema   => 'QLDL',
        object_name     => 'MOMON',
        policy_name     => 'GV_MOMON_SELECT_POLICY',
        function_schema => 'QLDL',
        policy_function => 'restrict_momon_gv',
        statement_types  => 'SELECT',
        sec_relevant_cols => 'MAMM,MAHP,MAGV,HK,NAM'
    );
END;
/
--Policy cho NV_PDT:
CREATE OR REPLACE FUNCTION restrict_momon_nv_pdt (
    p_schema IN VARCHAR2,
    p_object IN VARCHAR2
) RETURN VARCHAR2 AS
    v_month NUMBER := EXTRACT(MONTH FROM SYSDATE);
    v_year  NUMBER := EXTRACT(YEAR FROM SYSDATE);
    v_hk    NUMBER;
BEGIN
    -- Xác định học kỳ theo tháng
    IF v_month BETWEEN 9 AND 12 THEN
        v_hk := 1;
    ELSIF v_month BETWEEN 1 AND 4 THEN
        v_hk := 2;
    ELSE
        v_hk := 3;
    END IF;

    RETURN 'HK = ' || v_hk || ' AND NAM = ' || v_year;
END;

BEGIN
    DBMS_RLS.ADD_POLICY (
        object_schema   => 'QLDL',
        object_name     => 'MOMON',
        policy_name     => 'NV_PDT_MOMON_POLICY',
        function_schema => 'QLDL',
        policy_function => 'restrict_momon_nv_pdt',
        statement_types  => 'SELECT,INSERT,UPDATE,DELETE',
        update_check     => TRUE -- Kiểm tra dữ liệu mới khi UPDATE/INSERT
    );
END;
/
--Policy cho TRGDV:
CREATE OR REPLACE FUNCTION restrict_momon_trgdv (
    p_schema IN VARCHAR2,
    p_object IN VARCHAR2
) RETURN VARCHAR2 AS
    v_madv VARCHAR2(5);
BEGIN
    -- Lấy MADV của đơn vị mà người dùng là trưởng
    SELECT MADV INTO v_madv
    FROM QLDL.DONVI
    WHERE TRGDV = sys_context('userenv', 'SESSION_USER');
    
    -- Giới hạn truy cập đến các dòng MOMON có MAHP thuộc đơn vị đó
    RETURN 'MAGV IN (SELECT MAGV FROM QLDL.NHANVIEN WHERE MADV = ''' || v_madv || ''')';
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN '1=0'; -- Không cho phép truy cập nếu không phải trưởng đơn vị
END;
/
BEGIN
    DBMS_RLS.ADD_POLICY (
        object_schema   => 'QLDL',
        object_name     => 'MOMON',
        policy_name     => 'TRGDV_MOMON_SELECT_POLICY',
        function_schema => 'QLDL',
        policy_function => 'restrict_momon_trgdv',
        statement_types  => 'SELECT',
        sec_relevant_cols => 'MAMM,MAHP,MAGV,HK,NAM'
    );
END;
/
--Policy cho SINHVIEN
CREATE OR REPLACE FUNCTION restrict_momon_sinhvien (
    p_schema IN VARCHAR2,
    p_object IN VARCHAR2
) RETURN VARCHAR2 AS
    v_khoa VARCHAR2(5);
BEGIN
    -- Lấy KHOA của sinh viên từ bảng SINHVIEN
    SELECT KHOA INTO v_khoa
    FROM QLDL.SINHVIEN
    WHERE MASV = sys_context('userenv', 'SESSION_USER');
    
    -- Giới hạn truy cập đến các dòng MOMON có MAHP thuộc khoa của sinh viên
    RETURN 'MAHP IN (SELECT MAHP FROM QLDL.HOCPHAN WHERE MADV = ''' || v_khoa || ''')';
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN '1=0'; -- Không cho phép truy cập nếu không phải sinh viên
END;
/
BEGIN
    DBMS_RLS.ADD_POLICY (
        object_schema   => 'QLDL',
        object_name     => 'MOMON',
        policy_name     => 'SINHVIEN_MOMON_SELECT_POLICY',
        function_schema => 'QLDL',
        policy_function => 'restrict_momon_sinhvien',
        statement_types  => 'SELECT',
        sec_relevant_cols => 'MAMM,MAHP,MAGV,HK,NAM'
    );
END;
/
--Cấp quyền cho các role
-- Quyền cho GV
GRANT SELECT ON QLDL.MOMON TO GV;

-- Quyền cho NV_PDT
GRANT SELECT, INSERT, UPDATE, DELETE ON QLDL.MOMON TO NV_PDT;

-- Quyền cho TRGDV
GRANT SELECT ON QLDL.MOMON TO TRGDV;

-- Quyền cho SINHVIEN
GRANT SELECT ON QLDL.MOMON TO SINHVIEN;