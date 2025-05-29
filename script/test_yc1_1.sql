-- Đăng nhập user NVCB
CONNECT NV01/123456@localhost:1521/QLDULIEUNOIBO;

Kiểm tra xem user chỉ xem được thông tin chính mình qua view NVCB
SELECT * FROM QLDL.VIEW_NHANVIEN_NVCB;

-- Thử cập nhật số điện thoại của chính mình - nên thành công nếu thay đổi số điện thoại
UPDATE QLDL.VIEW_NHANVIEN_NVCB
SET DT = '0909999999'
WHERE MANV = SYS_CONTEXT('USERENV', 'SESSION_USER');

-- Kiểm tra lại số điện thoại cần chạy với nhân viên có quyền select được bảng nhanvien (
SELECT DT FROM QLDL.NHANVIEN WHERE MANV = SYS_CONTEXT('USERENV', 'SESSION_USER');

-- Thử cập nhật số điện thoại của người khác - phải bị lỗi hoặc không được phép
UPDATE QLDL.VIEW_NHANVIEN_NVCB
SET DT = '0911111111'
WHERE MANV != SYS_CONTEXT('USERENV', 'SESSION_USER'); -- nên lỗi hoặc 0 dòng bị cập nhật

-- Đăng nhập user TRGDV
CONNECT NV01/123456@localhost:1521/QLDULIEUNOIBO;

-- có quyền xem các dòng dữ liệu liên quan đến các nhân viên thuộc đơn vị mình làm trưởng, trừ các thuộc tính LUONG và PHUCAP.
SELECT * FROM QLDL.VIEW_NHANVIEN_TRGDV;
--

-- Đăng nhập user TCHC

CONNECT NV10/123456@localhost:1521/QLDULIEUNOIBO;

-- Xem tất cả nhân viên
SELECT * FROM QLDL.NHANVIEN;

-- Thêm 1 nhân viên mới
INSERT INTO QLDL.NHANVIEN (MANV, HOTEN, PHAI, NGSINH, DT, VAITRO, LUONG, PHUCAP)
VALUES ('M999', 'Test User', 'Nam', TO_DATE('1990-01-01', 'YYYY-MM-DD'), '0907777777', 'NVCB',  5000000, 1000000);

-- Cập nhật thông tin nhân viên
UPDATE QLDL.NHANVIEN SET DT = '0906666666' WHERE MANV = 'M999';

-- Xóa nhân viên
DELETE FROM QLDL.NHANVIEN WHERE MANV = 'M999';
