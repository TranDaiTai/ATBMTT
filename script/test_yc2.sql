BEGIN
  -- TRƯỞNG ĐƠN VỊ có toàn quyền (đọc tất cả thông báo)
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'THONGBAO_POLICY',
    user_name   => 'NV01',
    max_read_label => 'TRUONGDV:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2'
  );
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'THONGBAO_POLICY',
    user_name   => 'NV03',
    max_read_label => 'TRUONGDV:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2'
  );
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'THONGBAO_POLICY',
    user_name   => 'NV06',
    max_read_label => 'TRUONGDV:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2'
  );

  -- TRƯỞNG ĐƠN VỊ khoa HÓA cơ sở 2
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'THONGBAO_POLICY',
    user_name   => 'NV05',
    max_read_label => 'TRUONGDV:HOA:COSO2'
  );

  -- TRƯỞNG ĐƠN VỊ khoa LÝ cơ sở 2
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'THONGBAO_POLICY',
    user_name   => 'NV08',
    max_read_label => 'TRUONGDV:LY:COSO2'
  );

  -- NHÂN VIÊN đọc thông báo nhân viên (mọi khoa)
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'THONGBAO_POLICY',
    user_name   => 'NV02',
    max_read_label => 'NHANVIEN:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2'
  );
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'THONGBAO_POLICY',
    user_name   => 'NV04',
    max_read_label => 'NHANVIEN:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2'
  );
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'THONGBAO_POLICY',
    user_name   => 'NV07',
    max_read_label => 'NHANVIEN:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2'
  );
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'THONGBAO_POLICY',
    user_name   => 'NV09',
    max_read_label => 'NHANVIEN:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2'
  );
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'THONGBAO_POLICY',
    user_name   => 'NV11',
    max_read_label => 'NHANVIEN:TOAN,LY,HOA,HANHCHINH:COSO1,COSO2'
  );

  -- NHÂN VIÊN hành chính – chỉ đọc thông báo hành chính cơ sở 1
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'THONGBAO_POLICY',
    user_name   => 'NV10',
    max_read_label => 'NHANVIEN:HANHCHINH:COSO1'
  );
END;
/


-- Thêm thông báo với nhãn t1: Dành cho tất cả trưởng đơn vị
INSERT INTO THONGBAO (ID_THONGBAO, NOIDUNG, LABEL)
VALUES (thongbao_seq.NEXTVAL, 'Thông báo họp trưởng đơn vị toàn trường.', CHAR_TO_LABEL('THONGBAO_POLICY', 'TRUONGDV'));

-- Thêm thông báo với nhãn t2: Dành cho tất cả nhân viên
INSERT INTO THONGBAO (ID_THONGBAO, NOIDUNG, LABEL)
VALUES (thongbao_seq.NEXTVAL, 'Thông báo nghỉ lễ dành cho toàn thể nhân viên.', CHAR_TO_LABEL('THONGBAO_POLICY', 'NHANVIEN'));

-- Thêm thông báo với nhãn t3: Dành cho tất cả sinh viên
INSERT INTO THONGBAO (ID_THONGBAO, NOIDUNG, LABEL)
VALUES (thongbao_seq.NEXTVAL, 'Thông báo lịch thi học kỳ cho sinh viên.', CHAR_TO_LABEL('THONGBAO_POLICY', 'SINHVIEN'));

-- Thêm thông báo với nhãn t4: Dành cho sinh viên khoa Hóa cơ sở 1
INSERT INTO THONGBAO (ID_THONGBAO, NOIDUNG, LABEL)
VALUES (thongbao_seq.NEXTVAL, 'Thông báo seminar khoa Hóa cơ sở 1.', CHAR_TO_LABEL('THONGBAO_POLICY', 'SINHVIEN:HOA:COSO1'));

-- Thêm thông báo với nhãn t5: Dành cho sinh viên khoa Hóa cơ sở 2
INSERT INTO THONGBAO (ID_THONGBAO, NOIDUNG, LABEL)
VALUES (thongbao_seq.NEXTVAL, 'Thông báo thực hành thí nghiệm khoa Hóa cơ sở 2.', CHAR_TO_LABEL('THONGBAO_POLICY', 'SINHVIEN:HOA:COSO2'));

-- Thêm thông báo với nhãn t6: Dành cho sinh viên khoa Hóa cả 2 cơ sở
INSERT INTO THONGBAO (ID_THONGBAO, NOIDUNG, LABEL)
VALUES (thongbao_seq.NEXTVAL, 'Thông báo hội thảo khoa Hóa cả hai cơ sở.', CHAR_TO_LABEL('THONGBAO_POLICY', 'SINHVIEN:HOA:COSO1,COSO2'));

-- Thêm thông báo với nhãn t7: Dành cho tất cả sinh viên cả 2 cơ sở
INSERT INTO THONGBAO (ID_THONGBAO, NOIDUNG, LABEL)
VALUES (thongbao_seq.NEXTVAL, 'Thông báo ngày hội sinh viên toàn trường.', CHAR_TO_LABEL('THONGBAO_POLICY', 'SINHVIEN::COSO1,COSO2'));

-- Thêm thông báo với nhãn t8: Dành cho trưởng khoa Hóa cơ sở 1
INSERT INTO THONGBAO (ID_THONGBAO, NOIDUNG, LABEL)
VALUES (thongbao_seq.NEXTVAL, 'Thông báo họp trưởng khoa Hóa cơ sở 1.', CHAR_TO_LABEL('THONGBAO_POLICY', 'TRUONGDV:HOA:COSO1'));

-- Thêm thông báo với nhãn t9: Dành cho trưởng khoa Hóa cả 2 cơ sở
INSERT INTO THONGBAO (ID_THONGBAO, NOIDUNG, LABEL)
VALUES (thongbao_seq.NEXTVAL, 'Thông báo kế hoạch phát triển khoa Hóa cả hai cơ sở.', CHAR_TO_LABEL('THONGBAO_POLICY', 'TRUONGDV:HOA:COSO1,COSO2'));