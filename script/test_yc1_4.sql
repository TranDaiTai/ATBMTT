-- Đăng nhập SV001
CONNECT SV001/123@LOCALHOST:1521/QLDULIEUNOIBO;
;

-- Xem dữ liệu đăng ký của chính mình
SELECT * FROM QLDL.V_DANGKY;
--
---- Thêm đăng ký học phần (chỉ thành công trong 14 ngày đầu HK)
INSERT INTO QLDL.V_DANGKY(MASV, MAMM)
VALUES ('SV001', 'MM001');
--
---- Cập nhật đăng ký (chỉ trong 14 ngày đầu HK, học kỳ hiện tại)
UPDATE QLDL.V_DANGKY
SET MAMM = 'MM002'
WHERE MASV = 'SV001' AND MAMM = 'MM001';

---- Xóa đăng ký (chỉ trong 14 ngày đầu HK)
DELETE FROM QLDL.V_DANGKY
WHERE MASV = 'SV001' AND MAMM = 'MM002';
--
---- Cố gắng xem hoặc sửa điểm (phải bị lỗi)
UPDATE QLDL.V_DANGKY
SET DIEMCK = 8
WHERE MASV = 'SV001';





