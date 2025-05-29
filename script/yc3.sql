-- =============================================
-- AUDIT CONFIGURATION SCRIPT (YC3)
-- =============================================
-- This script sets up auditing for the X_UNIVERSITY database system
-- It requires connection as QLDL/123456@localhost:1521/QLDULIEUNOIBO or appropriate privileges

-- -- Connect to the correct PDB
-- CONNECT QLDL/123456@localhost:1521/QLDULIEUNOIBO;
ALTER SESSION SET CONTAINER = QLDulieuNoiBo;
ALTER SESSION SET CURRENT_SCHEMA = QLDL;


GRANT ADMINISTER DATABASE TRIGGER TO QLDL;

-- =============================================
-- 0. CREATE APPLICATION CONTEXT (IF DOESN'T EXIST)
-- =============================================
-- This is needed for the audit policies to work properly

-- First check if the context exists
DECLARE
  v_count NUMBER;
BEGIN
  SELECT COUNT(*) INTO v_count
  FROM DBA_CONTEXT
  WHERE NAMESPACE = 'X_UNIVERITY_CONTEXT';
  
  IF v_count = 0 THEN
    -- Create the context if it doesn't exist
    EXECUTE IMMEDIATE 'CREATE OR REPLACE CONTEXT X_UNIVERITY_CONTEXT USING SET_X_UNIVERITY_CONTEXT';
    DBMS_OUTPUT.PUT_LINE('Context created successfully');
  ELSE
    DBMS_OUTPUT.PUT_LINE('Context already exists');
  END IF;
EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/

-- Create the package to set context values
CREATE OR REPLACE PACKAGE SET_X_UNIVERITY_CONTEXT AS
  PROCEDURE set_context;
END;
/

CREATE OR REPLACE PACKAGE BODY SET_X_UNIVERITY_CONTEXT AS
  PROCEDURE set_context IS
    v_user VARCHAR2(30) := SYS_CONTEXT('USERENV', 'SESSION_USER');
    v_role VARCHAR2(30);
    v_is_student NUMBER := 0;
    v_is_nvpkt NUMBER := 0;
    v_is_nvtchc NUMBER := 0;
  BEGIN
    -- Set the user name
    DBMS_SESSION.SET_CONTEXT('X_UNIVERITY_CONTEXT', 'USER_NAME', v_user);
    
    -- Check if user is a student
    BEGIN
      SELECT 1 INTO v_is_student FROM SINHVIEN WHERE MASV = v_user;
      DBMS_SESSION.SET_CONTEXT('X_UNIVERITY_CONTEXT', 'IS_SV', '1');
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        DBMS_SESSION.SET_CONTEXT('X_UNIVERITY_CONTEXT', 'IS_SV', '0');
    END;
    
    -- Check employee role
    BEGIN
      SELECT VAITRO INTO v_role FROM NHANVIEN WHERE MANV = v_user;
      
      IF v_role = 'NV PKT' THEN
        DBMS_SESSION.SET_CONTEXT('X_UNIVERITY_CONTEXT', 'IS_NVPKT', '1');
      ELSE
        DBMS_SESSION.SET_CONTEXT('X_UNIVERITY_CONTEXT', 'IS_NVPKT', '0');
      END IF;
      
      IF v_role = 'NV TCHC' THEN
        DBMS_SESSION.SET_CONTEXT('X_UNIVERITY_CONTEXT', 'IS_NVTCHC', '1');
      ELSE
        DBMS_SESSION.SET_CONTEXT('X_UNIVERITY_CONTEXT', 'IS_NVTCHC', '0');
      END IF;
      
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        DBMS_SESSION.SET_CONTEXT('X_UNIVERITY_CONTEXT', 'IS_NVPKT', '0');
        DBMS_SESSION.SET_CONTEXT('X_UNIVERITY_CONTEXT', 'IS_NVTCHC', '0');
    END;
  END set_context;
END SET_X_UNIVERITY_CONTEXT;
/

-- Create a logon trigger to set context for each session
CREATE OR REPLACE TRIGGER set_app_context_trigger
AFTER LOGON ON DATABASE
BEGIN
  SET_X_UNIVERITY_CONTEXT.set_context;
END;
/


-- =============================================
-- 1. SYSTEM-WIDE AUDIT SETTINGS
-- =============================================
-- cần kết nối với CDB$ROOT để thay đổi cấu hình hệ thống
ALTER SESSION SET CONTAINER = CDB$ROOT;
ALTER SYSTEM SET audit_sys_operations=TRUE SCOPE=SPFILE;

-- =============================================
-- 2. STANDARD AUDIT CONFIGURATION
-- =============================================
ALTER SESSION SET CONTAINER = QLDulieuNoiBo;
ALTER SESSION SET CURRENT_SCHEMA = QLDL;
AUDIT SELECT ON QLDL.DANGKY BY ACCESS;
COMMIT;

-- =============================================
-- 3. FINE-GRAINED AUDIT POLICIES
-- =============================================

-- ---------------------------------------------
-- 3.1 Audit updates to DANGKY grade fields by non-PKT users
-- ---------------------------------------------
BEGIN
  -- Remove policy if it already exists
  BEGIN
    DBMS_FGA.drop_policy(
      object_schema => 'QLDL',
      object_name => 'DANGKY',
      policy_name => 'AUDIT_UPDATE_DIEM'
    );
  EXCEPTION
    WHEN OTHERS THEN NULL;
  END;

  -- Add policy
  DBMS_FGA.add_policy(
    object_schema   => 'QLDL',
    object_name     => 'DANGKY',
    policy_name     => 'AUDIT_UPDATE_DIEM',
    audit_condition => 'SYS_CONTEXT(''X_UNIVERITY_CONTEXT'', ''IS_NVPKT'') < 1',
    audit_column    => 'DIEMTH, DIEMQT, DIEMCK, DIEMTK',
    statement_types => 'UPDATE',
    enable          => TRUE
  );
END;
/
COMMIT;

-- ---------------------------------------------
-- 3.2 Audit access to salary information by non-TCHC users
-- ---------------------------------------------
-- Create helper function
CREATE OR REPLACE FUNCTION VIOLATE_SELECT_NHANVIEN_POLICY(
  p_current_username IN VARCHAR2,
  p_actual_username IN VARCHAR2
)
  RETURN NUMBER DETERMINISTIC
  AS
    p_is_nvtchc NUMBER;
  BEGIN
    p_is_nvtchc := SYS_CONTEXT('X_UNIVERITY_CONTEXT', 'IS_NVTCHC');
    IF p_is_nvtchc < 1 AND p_current_username != p_actual_username THEN
      RETURN 1; -- Policy violation
    ELSE
      RETURN 0; -- No violation
    END IF;
  END VIOLATE_SELECT_NHANVIEN_POLICY;
/

-- Add policy
BEGIN
  -- Remove policy if it already exists
  BEGIN
    DBMS_FGA.drop_policy(
      object_schema => 'QLDL',
      object_name => 'NHANVIEN',
      policy_name => 'AUDIT_SELECT_NHANVIEN'
    );
  EXCEPTION
    WHEN OTHERS THEN NULL;
  END;

  -- Add policy
  DBMS_FGA.add_policy(
    object_schema   => 'QLDL',
    object_name     => 'NHANVIEN',
    policy_name     => 'AUDIT_SELECT_NHANVIEN',
    audit_condition => 'QLDL.VIOLATE_SELECT_NHANVIEN_POLICY(SYS_CONTEXT(''X_UNIVERITY_CONTEXT'', ''USER_NAME''), MANV) = 1',
    audit_column    => 'LUONG, PHUCAP',
    statement_types => 'SELECT',
    enable          => TRUE
  );
END;
/
COMMIT;

-- ---------------------------------------------
-- 3.3a Audit registration modifications outside allowed time periods
-- ---------------------------------------------
-- Create helper function
CREATE OR REPLACE FUNCTION isInModifyTime(
  p_maMM IN VARCHAR2
  )
  RETURN NUMBER
  DETERMINISTIC
AS
  v_count NUMBER;
  v_hk NUMBER;
  v_nam NUMBER;
BEGIN
  SELECT HK, NAM INTO v_hk, v_nam
  FROM QLDL.MOMON
  WHERE MAMM = p_maMM;
  
  SELECT COUNT(*) INTO v_count
  FROM DUAL
  WHERE CURRENT_DATE 
        - TRUNC(
            TO_DATE(
              v_nam || '-' ||
              CASE v_hk 
                WHEN 1 THEN '09'
                WHEN 2 THEN '01'
                WHEN 3 THEN '05'
              END,
            'YYYY-MM')
          )
      BETWEEN 0 AND 13;
  
  IF v_count > 0 THEN
    RETURN 1;
  ELSE
    RETURN 0;
  END IF;
EXCEPTION
  WHEN NO_DATA_FOUND THEN
    RETURN 0;
END isInModifyTime;
/

-- Add policy
BEGIN
  -- Remove policy if it already exists
  BEGIN
    DBMS_FGA.drop_policy(
      object_schema => 'QLDL',
      object_name => 'DANGKY',
      policy_name => 'AUDIT_NOT_IN_MODIFY_TIME_DANGKY'
    );
  EXCEPTION
    WHEN OTHERS THEN NULL;
  END;

  -- Add policy
  DBMS_FGA.add_policy(
    object_schema   => 'QLDL',
    object_name     => 'DANGKY',
    policy_name     => 'AUDIT_NOT_IN_MODIFY_TIME_DANGKY',
    audit_condition => 'QLDL.isInModifyTime(MAMM) = 0',
    statement_types => 'INSERT, UPDATE, DELETE',
    enable          => TRUE
  );
END;
/
COMMIT;

-- ---------------------------------------------
-- 3.3b Audit student modifications to other students' registration data
-- ---------------------------------------------
-- Create helper function
CREATE OR REPLACE FUNCTION get_audit_condition(
  v_is_student IN NUMBER,
  v_username IN VARCHAR2,
  v_masv IN VARCHAR2) 
  RETURN NUMBER
AS
BEGIN
  IF v_is_student >= 1 AND v_masv != v_username THEN
    RETURN 1;
  ELSE 
    RETURN 0;
  END IF;
END;
/

-- Add policy
BEGIN
  -- Remove policy if it already exists
  BEGIN
    DBMS_FGA.drop_policy(
      object_schema => 'QLDL',
      object_name => 'DANGKY',
      policy_name => 'AUDIT_INSERT_UPDATE_DELETE_DANGKY'
    );
  EXCEPTION
    WHEN OTHERS THEN NULL;
  END;

  -- Add policy
  DBMS_FGA.add_policy(
    object_schema   => 'QLDL',
    object_name     => 'DANGKY',
    policy_name     => 'AUDIT_INSERT_UPDATE_DELETE_DANGKY',
    audit_condition => 'QLDL.get_audit_condition(SYS_CONTEXT(''X_UNIVERITY_CONTEXT'', ''IS_SV''), SYS_CONTEXT(''X_UNIVERITY_CONTEXT'', ''USER_NAME''), MASV) = 1',
    statement_types => 'INSERT, UPDATE, DELETE',
    enable          => TRUE
  );
END;
/
COMMIT;

-- =============================================
-- 4. VERIFY AUDIT CONFIGURATION
-- =============================================
-- Check if standard audit is enabled
-- SELECT * FROM DBA_STMT_AUDIT_OPTS WHERE OWNER='QLDL' AND OBJECT_NAME='DANGKY'; 
-- cái trên không tồn tại, dùng cái này thay thế
SELECT * FROM DBA_OBJ_AUDIT_OPTS WHERE OBJECT_NAME = 'DANGKY' AND OWNER = 'QLDL';


-- Check if fine-grained audit policies are enabled
SELECT POLICY_NAME, ENABLED FROM DBA_AUDIT_POLICIES WHERE OBJECT_SCHEMA='QLDL';

-- =============================================
-- 5. AUDIT REVIEW PROCEDURES
-- =============================================
-- To review standard audit records:
-- SELECT * FROM DBA_AUDIT_TRAIL WHERE OWNER='QLDL' AND OBJ_NAME='DANGKY';

-- To review fine-grained audit records:
-- SELECT * FROM DBA_FGA_AUDIT_TRAIL WHERE OBJECT_SCHEMA='QLDL';



-- SELECT * 
-- FROM DBA_AUDIT_POLICIES 
-- WHERE OBJECT_NAME = 'DANGKY' 
--   AND OBJECT_SCHEMA = 'QLDL';
