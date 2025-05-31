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
