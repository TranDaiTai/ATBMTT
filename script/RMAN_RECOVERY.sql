-- Luu ý: Phải Bật Achivelog 
--sqlplus / as sysdba mở CMD và nhập lệnh trên và tiếp tục các bước 
---- 1. Tắt database
--SHUTDOWN IMMEDIATE;
--
---- 2. Mở ở chế độ MOUNT
--STARTUP MOUNT;
--
---- 3. Bật chế độ ARCHIVELOG
--ALTER DATABASE ARCHIVELOG;
--
---- 4. Mở lại database
--ALTER DATABASE OPEN;
--
---- 5. Kiểm tra
--ARCHIVE LOG LIST;

SET SERVEROUTPUT ON;

--du LIEU TAO SAN 
--Sao lưu vật lý bằng RMAN
-- Kết nối tới RMAN với tài khoản có quyền trong PDB
-- B1.Mở CMD chạy BASH: rman target QLDL/123456@localhost:1521/QLDULIEUNOIBO
--RMAN> BACKUP TABLESPACE RECOVERY_TEST;
--RMAN> BACKUP ARCHIVELOG ALL;
--RMAN> LIST BACKUP OF TABLESPACE RECOVERY_TEST;
ALTER TABLESPACE RECOVERY_TEST OFFLINE IMMEDIATE;
ALTER DATABASE DATAFILE 'RECOVERY_TEST.DBF' OFFLINE DROP;
DBMS_OUTPUT.PUT_LINE('Datafile RECOVERY_TEST.DBF mô phỏng mất dữ liệu');
/
-- Thử truy vấn bảng (sẽ báo lỗi)
SELECT * FROM DATAPUMP_TEST_USER.RECOVERY_TEST;
--Phục hồi bằng RMAN (bao gồm PITR) tất cả thực hiện trên CMND đối vói RMAN
-- Chạy trong RMAN: rman target QLDL/123456@localhost:1521/QLDULIEUNOIBO
--RMAN> RUN {
--    SET UNTIL SCN 12345678; -- Thay bằng SCN thực tế từ bước 4
--    RESTORE TABLESPACE RECOVERY_TEST;
--    RECOVER TABLESPACE RECOVERY_TEST;
--    ALTER TABLESPACE RECOVERY_TEST ONLINE;
--}
--Kiểm tra dữ liệu sau phục hồi
SELECT COUNT(*) AS "Total Records" FROM DATAPUMP_TEST_USER.RECOVERY_TEST;
SELECT * FROM DATAPUMP_TEST_USER.RECOVERY_TEST;
