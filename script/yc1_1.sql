-- yêu cầu 1 
-- Tạo role cho từng vai trò
CREATE ROLE NVCB;
CREATE ROLE GV;
CREATE ROLE NV_PDT;
CREATE ROLE NV_PKT;
CREATE ROLE NV_TCHC;
CREATE ROLE NV_CTSV;
CREATE ROLE TRGDV;

--Cấp quyền cho role NVCB
--Tạo policy function để giới hạn truy cập đến dòng của chính người dùng

CREATE OR REPLACE FUNCTION restrict_nhanvien_nvcb (
    p_schema IN VARCHAR2,
    p_object IN VARCHAR2
) RETURN VARCHAR2 
AS
    USERNAME VARCHAR2(128);
BEGIN
    -- Lấy username của user hiện tại
    USERNAME := SYS_CONTEXT('userenv', 'SESSION_USER');
    -- Giới hạn truy cập đến dòng có MANV = USER (người dùng hiện tại)
    RETURN 'MANV = ''' || USERNAME || '''';
END;
/

--Áp dụng policy VPD cho SELECT trên NHANVIEN
BEGIN
    DBMS_RLS.ADD_POLICY (
        object_schema   => 'QLDL',
        object_name     => 'NHANVIEN',
        policy_name     => 'NVCB_SELECT_POLICY',
        function_schema => 'QLDL',
        policy_function => 'restrict_nhanvien_nvcb',
        statement_types  => 'SELECT',
        sec_relevant_cols => 'MANV,HOTEN,PHAI,NGSINH,DT,VAITRO,MADV'
    );
END;
/

--Áp dụng policy VPD cho UPDATE trên cột DT
BEGIN
    DBMS_RLS.ADD_POLICY (
        object_schema   => 'QLDL',
        object_name     => 'NHANVIEN',
        policy_name     => 'NVCB_UPDATE_DT_POLICY',
        function_schema => 'QLDL',
        policy_function => 'restrict_nhanvien_nvcb',
        statement_types  => 'UPDATE',
        sec_relevant_cols => 'DT',
        sec_relevant_cols_opt => DBMS_RLS.ALL_ROWS
    );
END;
/

--Cấp quyền cho role NVCB
GRANT SELECT ON QLDL.NHANVIEN TO NVCB;
GRANT UPDATE (DT) ON QLDL.NHANVIEN TO NVCB;
--Cấp quyền cho các vai trò khác (kế thừa quyền NVCB)
GRANT NVCB TO GV;
GRANT NVCB TO NV_PDT;
GRANT NVCB TO NV_PKT;
GRANT NVCB TO NV_TCHC;
GRANT NVCB TO NV_CTSV;
GRANT NVCB TO TRGDV;

-- Cấp quyền cho role trgdv
--Tạo policy function cho TRGDV
CREATE OR REPLACE FUNCTION restrict_nhanvien_trgdv (
    p_schema IN VARCHAR2,
    p_object IN VARCHAR2
) RETURN VARCHAR2 AS
    v_madv VARCHAR2(5);
BEGIN
    -- Lấy MADV của đơn vị mà người dùng là trưởng (TRGDV)
    SELECT MADV INTO v_madv
    FROM QLDL.DONVI
    WHERE TRGDV = SYS_CONTEXT('userenv', 'SESSION_USER');
    
    -- Giới hạn truy cập đến các nhân viên thuộc đơn vị đó
    RETURN 'MADV = ''' || v_madv || '''';
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN '1=0'; -- Không cho phép truy cập nếu không tìm thấy đơn vị
END;
/
--Áp dụng policy VPD cho SELECT của TRGDV
BEGIN
    DBMS_RLS.ADD_POLICY (
        object_schema   => 'QLDL',
        object_name     => 'NHANVIEN',
        policy_name     => 'TRGDV_SELECT_POLICY',
        function_schema => 'QLDL',
        policy_function => 'restrict_nhanvien_trgdv',
        statement_types  => 'SELECT',
        sec_relevant_cols => 'MANV,HOTEN,PHAI,NGSINH,DT,VAITRO,MADV'
    );
END;
/


-- Cấp quyền cho role NV_TCHC
GRANT SELECT, INSERT, UPDATE, DELETE ON QLDL.NHANVIEN TO NV_TCHC;

