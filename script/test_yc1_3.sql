-- Đăng nhập
CONNECT SV001/123@localhost:1521/QLDuLieuNoiBo;

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
