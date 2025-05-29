
-- Đăng nhập SYSDBA trong CDB$ROOT
ALTER SESSION SET CONTAINER = CDB$ROOT;

ALTER USER LBACSYS ACCOUNT UNLOCK;




select name, status, description from dba_ols_status;


--Tạo bảng THONGBAO Thêm bảng THONGBAO với cột NOIDUNG và cột LABEL để lưu nhãn OLS.
ALTER SESSION SET CONTAINER = QLDulieuNoiBo;
EXEC LBACSYS.CONFIGURE_OLS;
EXEC LBACSYS.OLS_ENFORCEMENT.ENABLE_OLS;
ALTER SESSION SET CURRENT_SCHEMA = QLDL;
-- làm xong rồi thoát ra vô lại vẫn tiếp tục ở container QLDulieuNoiBo và schema QLDL
--tao bảng THONGBAO
CREATE TABLE THONGBAO(
    ID_THONGBAO NUMBER PRIMARY KEY,
    NOIDUNG     VARCHAR2(4000),
    ThoiGian TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    DiaDiem NVARCHAR2(50)
);




-- Thêm sequence để tự động tạo ID_THONGBAO
CREATE SEQUENCE thongbao_seq START WITH 1 INCREMENT BY 1;

--Điều chỉnh bảng NHANVIEN và SINHVIEN Để hỗ trợ vị trí địa lý (Cơ sở 1, Cơ sở 2), thêm cột COSO vào bảng NHANVIEN và SINHVIEN.
ALTER TABLE QLDL.NHANVIEN ADD COSO VARCHAR2(10) CHECK (COSO IN ('Cơ sở 1', 'Cơ sở 2'));
ALTER TABLE QLDL.SINHVIEN ADD COSO VARCHAR2(10) CHECK (COSO IN ('Cơ sở 1', 'Cơ sở 2'));

-- Cập nhật dữ liệu mẫu cho cột COSO
UPDATE QLDL.NHANVIEN SET COSO = 'Cơ sở 1' WHERE MADV IN ('CNTT', 'TOAN', 'PDT', 'PTV');
UPDATE QLDL.NHANVIEN SET COSO = 'Cơ sở 2' WHERE MADV IN ('HOA', 'VLY', 'PQTTB');
UPDATE QLDL.SINHVIEN SET COSO = 'Cơ sở 1' WHERE KHOA IN ('CNTT', 'TOAN');
UPDATE QLDL.SINHVIEN SET COSO = 'Cơ sở 2' WHERE KHOA IN ('HOA', 'VLY'); 


----------- LƯU Ý
--------
-------đăng nhập với quyền LBACSYS
-------
--------
------
CONNECT lbacsys/123456@localhost:1521/QLDULIEUNOIBO;


--Tạo chính sách OLS Tạo chính sách OLS với tên THONGBAO_POLICY.

BEGIN
    SA_SYSDBA.CREATE_POLICY (
        policy_name    => 'THONGBAO_POLICY',
        column_name    => 'LABEL',
        default_options => 'NO_CONTROL'
    );
END;
/



-- Tạo Levels
BEGIN
    SA_COMPONENTS.CREATE_LEVEL (
        policy_name => 'THONGBAO_POLICY',
        level_num   => 3000,
        short_name  => 'TRUONGDV',
        long_name   => 'Trưởng đơn vị'
    );
    SA_COMPONENTS.CREATE_LEVEL (
        policy_name => 'THONGBAO_POLICY',
        level_num   => 2000,
        short_name  => 'NHANVIEN',
        long_name   => 'Nhân viên'
    );
    SA_COMPONENTS.CREATE_LEVEL (
        policy_name => 'THONGBAO_POLICY',
        level_num   => 1000,
        short_name  => 'SINHVIEN',
        long_name   => 'Sinh viên'
    );
END;
/

-- Tạo Compartments
BEGIN
    SA_COMPONENTS.CREATE_COMPARTMENT (
        policy_name => 'THONGBAO_POLICY',
        comp_num    => 10,
        short_name  => 'TOAN',
        long_name   => 'Toán'
    );
    SA_COMPONENTS.CREATE_COMPARTMENT (
        policy_name => 'THONGBAO_POLICY',
        comp_num    => 20,
        short_name  => 'LY',
        long_name   => 'Lý'
    );
    SA_COMPONENTS.CREATE_COMPARTMENT (
        policy_name => 'THONGBAO_POLICY',
        comp_num    => 30,
        short_name  => 'HOA',
        long_name   => 'Hóa'
    );
    SA_COMPONENTS.CREATE_COMPARTMENT (
        policy_name => 'THONGBAO_POLICY',
        comp_num    => 40,
        short_name  => 'HANHCHINH',
        long_name   => 'Hành chính'
    );
END;
/

-- Tạo Groups
BEGIN
    SA_COMPONENTS.CREATE_GROUP (
        policy_name => 'THONGBAO_POLICY',
        group_num   => 3,
        short_name  => 'COSO2',
        long_name   => 'Cơ sở 2'
    );

    SA_COMPONENTS.CREATE_GROUP (
        policy_name => 'THONGBAO_POLICY',
        group_num   => 1,
        short_name  => 'COSO1',
        long_name   => 'Cơ sở 1'
    );

END;
/
--Tạo nhãn (Labels)
--Tạo các nhãn cho các định danh dữ liệu (t1, t2, ..., t9) dựa trên mô tả.
-- t1: cần phát tán đến tất cả trường đơn vị
BEGIN
    SA_LABEL_ADMIN.CREATE_LABEL (
        policy_name => 'THONGBAO_POLICY',
        label_tag   => 1001,
        label_value => 'TRUONGDV'
    );
END;
/
-- t2: cần phát tán đến tất cả nhân viên
BEGIN
    SA_LABEL_ADMIN.CREATE_LABEL (
        policy_name => 'THONGBAO_POLICY',
        label_tag   => 1002,
        label_value => 'NHANVIEN'
    );
END;
/
-- nhãn t3: cần phát tán đến tất cả sinh viên
BEGIN
    SA_LABEL_ADMIN.CREATE_LABEL (
        policy_name => 'THONGBAO_POLICY',
        label_tag   => 1003,
        label_value => 'SINHVIEN'
    );
END;
/
-- nhãn t4: cần phát tán đến tất cả sinh viên thuộc khoa Hóa cs1
BEGIN
    SA_LABEL_ADMIN.CREATE_LABEL (
        policy_name => 'THONGBAO_POLICY',
        label_tag   => 1004,
        label_value => 'SINHVIEN:HOA:COSO1'
    );
END;
/
-- nhãn t5: cần phát tán đến tất cả sinh viên thuộc khoa Hóa cs2
BEGIN
    SA_LABEL_ADMIN.CREATE_LABEL (
        policy_name => 'THONGBAO_POLICY',
        label_tag   => 1005,
        label_value => 'SINHVIEN:HOA:COSO2'
    );
END;
/
-- nhãn t6: cần phát tán đến tất cả sinh viên thuộc khoa hóa cả 2 cơ sở
BEGIN
    SA_LABEL_ADMIN.CREATE_LABEL (
        policy_name => 'THONGBAO_POLICY',
        label_tag   => 1006,
        label_value => 'SINHVIEN:HOA:COSO1,COSO2'
    );
END;
/
-- nhãn t7: cần phát tán đến tất cả sinh viên cả 2 cơ sở

BEGIN
    SA_LABEL_ADMIN.CREATE_LABEL (
        policy_name => 'THONGBAO_POLICY',
        label_tag   => 1007,
        label_value => 'SINHVIEN::COSO1,COSO2'
    );
END;
/
-- nhãn t8: cần phát tán đến trưởng khoa hóa cơ sở 1
BEGIN
    SA_LABEL_ADMIN.CREATE_LABEL (
        policy_name => 'THONGBAO_POLICY',
        label_tag   => 1008,
        label_value => 'TRUONGDV:HOA:COSO1'
    );
END;
/
-- nhãn t9: cần phát tán đến trưởng khoa hóa cơ sở 1 và cơ sở 2
BEGIN
    SA_LABEL_ADMIN.CREATE_LABEL (
        policy_name => 'THONGBAO_POLICY',
        label_tag   => 1009,
        label_value => 'TRUONGDV:HOA:COSO1,COSO2'
    );
END;
/



--Áp dụng chính sách OLS cho bảng THONGBAO
-- tự gián nhãn cho bảng THONGBAO column LABEL 
-- BEGIN
--     SA_POLICY_ADMIN.APPLY_TABLE_POLICY (
--         policy_name    => 'THONGBAO_POLICY',
--         schema_name    => 'QLDL',
--         table_name     => 'THONGBAO',
--        TABLE_OPTIONS  => NULL
--     );
-- END;
-- /   

-- BEGIN
--     SA_POLICY_ADMIN.REMOVE_TABLE_POLICY (
--         policy_name => 'THONGBAO_POLICY',
--         schema_name => 'QLDL',
--         table_name  => 'THONGBAO',
--         DROP_COLUMN => FALSE
--     );
-- END;
-- /


BEGIN
    SA_POLICY_ADMIN.APPLY_TABLE_POLICY (
        policy_name    => 'THONGBAO_POLICY',
        schema_name    => 'QLDL',
        table_name     => 'THONGBAO',
        TABLE_OPTIONS  => 'READ_CONTROL, WRITE_CONTROL, CHECK_CONTROL'
    );
END;
/   



--Gán nhãn cho người dùng
--Gán nhãn cho các user (u1, u2, ..., u8) dựa trên mô tả. Dựa trên dữ liệu mẫu, ánh xạ các user như sau:
-- Nhãn người dùng
-- u1: trưởng đơn vị có thể đọc được tất cả thông báo
BEGIN
SA_LABEL_ADMIN.CREATE_LABEL  (
  policy_name     => 'THONGBAO_POLICY',
  label_tag       => 1010,
  label_value     => 'TRUONGDV:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2');
END;
/
-- u2: trưởng đơn vị phụ trách khoa hóa tại cơ sở 2
BEGIN
SA_LABEL_ADMIN.CREATE_LABEL  (
  policy_name     => 'THONGBAO_POLICY',
  label_tag       => 1011,
  label_value     => 'TRUONGDV:HOA:COSO2');
END;
/
-- u3: truong đơn vị phụ trách khoa lý tại cơ sở 2
BEGIN
SA_LABEL_ADMIN.CREATE_LABEL  (
  policy_name     => 'THONGBAO_POLICY',
  label_tag       => 1012,
  label_value     => 'TRUONGDV:LY:COSO2');
END;
/
-- u4: nhân viên thuộc khoa hóa tại cơ sở 2
BEGIN
SA_LABEL_ADMIN.CREATE_LABEL  (
  policy_name     => 'THONGBAO_POLICY',
  label_tag       => 1013,
  label_value     => 'NHANVIEN:HOA:COSO2');
END;
/
-- u5: sinh viên thuộc khoa hóa tại cơ sở 2 (đã có nhãn SV:HOA:CS2 - t5)
-- u6: Trưởng đơn vị đọc thông báo hành chính
BEGIN
SA_LABEL_ADMIN.CREATE_LABEL  (
  policy_name     => 'THONGBAO_POLICY',
  label_tag       => 1014,
  label_value     => 'TRUONGDV:HANHCHINH:COSO1,COSO2');
END;
/
-- u7: NV đọc được thông báo dành cho nhân viên
BEGIN
SA_LABEL_ADMIN.CREATE_LABEL  (
  policy_name     => 'THONGBAO_POLICY',
  label_tag       => 1015,
  label_value     => 'NHANVIEN:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2');
END;
/
-- u8: nhân viên đọc được thông báo hành chính tại cơ sở 1
BEGIN
SA_LABEL_ADMIN.CREATE_LABEL  (
  policy_name     => 'THONGBAO_POLICY',
  label_tag       => 1016,
  label_value     => 'NHANVIEN:HANHCHINH:COSO1');
END;
/ 
-- kiểm tra  các nhãn đã tạo
-- SELECT * FROM dba_sa_labels WHERE policy_name = 'THONGBAO_POLICY';

--Gán nhãn mặc định và phạm vi nhãn mà user QLDL có thể xử lý

BEGIN
    SA_USER_ADMIN.SET_USER_LABELS(
        policy_name => 'THONGBAO_POLICY',
        user_name   => 'QLDL',
        max_read_label => 'TRUONGDV:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2',
        max_write_label => 'TRUONGDV:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2'
    );
END;
/
-- kiểm tra nhãn mặc định và phạm vi nhãn của user QLDL
--SELECT * FROM dba_sa_user_labels WHERE policy_name = 'THONGBAO_POLICY';

-- BEGIN
--   SA_USER_ADMIN.SET_USER_PRIVS(
--     policy_name => 'THONGBAO_POLICY',
--     user_name   => 'QLDL',
--     privileges  => 'FULL' -- hoặc 'READ,WRITE' nếu bạn muốn hạn chế hơn
--   );
-- END;
-- /


grant select, insert, update, delete on QLDL.THONGBAO to ROLE_NVCB;
grant select, insert, update, delete on QLDL.THONGBAO to ROLE_SINHVIEN;
