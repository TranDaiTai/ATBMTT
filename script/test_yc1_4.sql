-- Đăng nhập SV001
CONNECT SV001/123@LOCALHOST:1521/QLDULIEUNOIBO;

---- Xem dữ liệu của chính mình
SELECT * FROM QLDL.V_DANGKY;

---- Cập nhật số thứ tự ưu tiên đăng ký
--UPDATE QLDL.V_DANGKY
--SET MAMM = 'MM003'
--WHERE MASV = 'SV001' AND MAMM = 'MM002';
--
---- Thử cập nhật điểm → BỊ CHẶN
--UPDATE QLDL.V_DANGKY
--SET DIEMCK = 8
--WHERE MASV = 'SV001' AND MAMM = 'MM001';
---- => ❌ expected: ORA-20010
--
---- Thêm đăng ký mới (nếu trong 14 ngày)
--INSERT INTO QLDL.V_DANGKY (MASV, MAMM)
--VALUES ('SV001', 'MM002');
