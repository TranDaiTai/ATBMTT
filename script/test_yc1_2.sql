--Test với user Giảng viên GV:
CONNECT NV07/123456@localhost:1521/QLDULIEUNOIBO;

-- Kiểm tra xem có thấy phân công giảng dạy của chính mình không
SELECT * FROM QLDL.VIEW_MOMON_GV;

--Test với user NV PĐT:
CONNECT NV04/123456@localhost:1521/QLDULIEUNOIBO;

-- Xem các môn học kỳ hiện tại 29/5/2025 là học kì 3 năm 2025 
SELECT * FROM QLDL.VIEW_MOMON_PDT;

---- Thêm dữ liệu (ví dụ)
INSERT INTO QLDL.VIEW_MOMON_PDT (MAMM, MAHP, MAGV, HK, NAM)
VALUES ('MM0001', 'HP001', 'NV01', 3, 2025);
SELECT * FROM QLDL.VIEW_MOMON_PDT;

--
---- Cập nhật dữ liệu
UPDATE QLDL.VIEW_MOMON_PDT
SET MAGV = 'NV02'
WHERE MAMM = 'MM0001';

--
---- Xóa dữ liệu
DELETE FROM QLDL.VIEW_MOMON_PDT
WHERE MAMM = 'MM0001';

--Test với user Trưởng đơn vị TRGDV:
CONNECT NV01/123456@localhost:1521/QLDULIEUNOIBO;


SELECT * FROM QLDL.VIEW_MOMON_TRGDV;