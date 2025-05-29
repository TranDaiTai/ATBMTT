-- Đăng nhập
CONNECT SV001/123456@localhost:1521/QLDuLieuNoiBo;

--  Xem dữ liệu của chính mình (nếu có MASV = 'SV001')
--SELECT * FROM QLDL.V_SINHVIEN;

---- ✅ Sửa thông tin ĐCHI, ĐT của chính mình
--UPDATE QLDL.V_SINHVIEN
--SET DCHI = '123 Đường Mới', DT = '0999888777'
--WHERE MASV = 'SV001';
--
-- ❌ Thử sửa TINHTRANG → bị chặn bởi trigger
--UPDATE QLDL.V_SINHVIEN
--SET TINHTRANG = 'Đã tốt nghiệp'
--WHERE MASV = 'SV001';

------ ❌ Thử sửa HỌ TÊN → bị chặn bởi trigger
UPDATE QLDL.V_SINHVIEN
SET HOTEN = 'Nguyễn Văn b'
WHERE MASV = 'SV001';


 --2. Test với user giảng viên (GV001)

-- Kết nối với user NV07
CONNECT NV07/123456@localhost:1521/QLDULIEUNOIBO;

-- Xem danh sách sinh viên cùng khoa
SELECT * FROM QLDL.V_SINHVIEN;

-- Thử cập nhật (không có quyền UPDATE): sẽ báo lỗi
UPDATE QLDL.V_SINHVIEN
SET DCHI = 'Sửa bởi GV'
WHERE MASV = 'SV001';

-- Thử xóa (không có quyền DELETE): sẽ báo lỗi
DELETE FROM QLDL.V_SINHVIEN WHERE MASV = 'SV001';


-- 3. Test với user nhân viên NVCTSV001 (NV04)  
-- Kết nối với user NVCTSV001
CONNECT NV11/123456@localhost:1521/QLDULIEUNOIBO;

-- Xem toàn bộ sinh viên
SELECT * FROM QLDL.SINHVIEN;

-- Thêm sinh viên mới
INSERT INTO QLDL.SINHVIEN (MASV, HOTEN, DCHI, DT, KHOA)
VALUES ('SVTEST1', 'Test Sinh Vien', 'HCM', '0123456789', 'CNTT');

-- Cập nhật thông tin sinh viên (ngoài TINHTRANG) → thành công
UPDATE QLDL.SINHVIEN
SET DT = '0988888888'
WHERE MASV = 'SV001';

-- Thử sửa TINHTRANG → bị trigger chặn
UPDATE QLDL.SINHVIEN
SET TINHTRANG = 'Tốt nghiệp'
WHERE MASV = 'SV001';

-- Xóa sinh viên → được phép
DELETE FROM QLDL.SINHVIEN WHERE MASV = 'SVTEST1';

-- 4. Test với user nhân viên NV04  
-- Kết nối với user NV04
CONNECT NV04/123456@localhost:1521/QLDULIEUNOIBO;

-- Xem toàn bộ sinh viên
SELECT * FROM QLDL.SINHVIEN;

-- Cập nhật TINHTRANG → thành công
UPDATE QLDL.SINHVIEN
SET TINHTRANG = 'Đang học'
WHERE MASV = 'SV001';

-- Cập nhật thông tin khác cũng được phép
UPDATE QLDL.SINHVIEN
SET DCHI = 'Cập nhật bởi PĐT'
WHERE MASV = 'SV001';
