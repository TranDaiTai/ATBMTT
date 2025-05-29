
CREATE OR REPLACE PROCEDURE sp_create_user_with_role (
    p_username IN VARCHAR2,
    p_password IN VARCHAR2
) IS
    v_role_name VARCHAR2(30);
    v_exists NUMBER;
    v_vaitro VARCHAR2(20);
BEGIN
    -- Kiểm tra user trong bảng SINHVIEN
    SELECT COUNT(*) INTO v_exists FROM SINHVIEN WHERE MASV = p_username;

    IF v_exists > 0 THEN
        -- Nếu là sinh viên, cấp role ROLE_SINHVIEN
        v_role_name := 'ROLE_SINHVIEN';

    ELSE
        -- Nếu không phải sinh viên, kiểm tra trong bảng NHANVIEN và lấy vai trò
        SELECT COUNT(*) INTO v_exists FROM NHANVIEN WHERE MANV = p_username;
        IF v_exists = 0 THEN
            RAISE_APPLICATION_ERROR(-20002, 'User ' || p_username || ' không tồn tại trong bảng SINHVIEN hoặc NHANVIEN');
        END IF;

        -- Lấy vai trò thực tế trong NHANVIEN
        SELECT VAITRO INTO v_vaitro FROM NHANVIEN WHERE MANV = p_username;

        -- Gán role tương ứng dựa trên VAITRO lấy được
        IF v_vaitro = 'NVCB' THEN
            v_role_name := 'ROLE_NVCB';
        ELSIF v_vaitro = 'GV' THEN
            v_role_name := 'ROLE_GV';
        ELSIF v_vaitro = 'NV PĐT' THEN
            v_role_name := 'ROLE_NV_PDT';
        ELSIF v_vaitro = 'NV PKT' THEN
            v_role_name := 'ROLE_NV_PKT';
        ELSIF v_vaitro = 'NV CTSV' THEN
            v_role_name := 'ROLE_NV_CTSV';
        ELSIF v_vaitro = 'TRGDV' THEN
            v_role_name := 'ROLE_TRGDV';
        ELSIF v_vaitro = 'NV TCHC' THEN
            v_role_name := 'ROLE_TCHC';
        ELSE
            RAISE_APPLICATION_ERROR(-20001, 'Vai trò trong bảng NHANVIEN không hợp lệ: ' || v_vaitro);
        END IF;
    END IF;

    -- Tạo user Oracle với password
    EXECUTE IMMEDIATE 'CREATE USER ' || p_username || ' IDENTIFIED BY "' || p_password || '"';

    -- Grant connect và resource cơ bản
    EXECUTE IMMEDIATE 'GRANT CONNECT, RESOURCE TO ' || p_username;

    -- Grant role tương ứng
    EXECUTE IMMEDIATE 'GRANT ' || v_role_name || ' TO ' || p_username;

    DBMS_OUTPUT.PUT_LINE('User ' || p_username || ' được tạo và cấp role ' || v_role_name || ' thành công.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Lỗi khi tạo user hoặc cấp role: ' || SQLERRM);
END;
/


BEGIN
    sp_create_user_with_role('NV01', '123456');
    sp_create_user_with_role('NV02', '123456');
    sp_create_user_with_role('NV03', '123456');
    sp_create_user_with_role('NV04', '123456');
    sp_create_user_with_role('NV05', '123456');
    sp_create_user_with_role('NV06', '123456');
    sp_create_user_with_role('NV07', '123456');
    sp_create_user_with_role('NV08', '123456');
    sp_create_user_with_role('NV09', '123456');
    sp_create_user_with_role('NV10', '123456');
    sp_create_user_with_role('NV11', '123456');
    sp_create_user_with_role('SV001', '123456');
END;
/
