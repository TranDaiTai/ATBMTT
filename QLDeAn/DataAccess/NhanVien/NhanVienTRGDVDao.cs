using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace QLDeAn.DataAccess.NhanVien
{
    class NhanVienTRGDVDao : INhanVienDao
    {
        private OracleConnection sqlConnection;

        public NhanVienTRGDVDao(OracleConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }
        public bool Add(object obj)
        {
            //throw new NoPrivilegeException();
            return false;
        }

        public bool Delete(object obj)
        {
            //throw new NoPrivilegeException();
            return false;
        }

        public List<object> Load(object obj)
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            List<Model.NhanVien> result = new List<Model.NhanVien>();

            try
            {
                using (var cmd = new OracleCommand("SELECT * FROM QLDL.VIEW_NHANVIEN_TRGDV", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text; // sửa lại từ StoredProcedure -> Text

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nv = new Model.NhanVien
                            {
                                maNV = reader["maNV"].ToString(),
                                hoTen = reader["hoTen"].ToString(),
                                phai = reader["phai"].ToString(),
                                dt = reader["dt"].ToString(),
                                vaiTro = reader["vaiTro"].ToString(),
                                maDV = reader["maDV"].ToString(),
                                isInDB = true
                            };

                            // Gán ngày sinh nếu không null
                            if (reader["ngSinh"] != DBNull.Value)
                            {
                                nv.ngSinh = Convert.ToDateTime(reader["ngSinh"]);
                            }

                            result.Add(nv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                sqlConnection.Close();
            }

            return result.Cast<object>().ToList();
        }


        public bool Update(object obj)
        {
            try
            {
                Model.NhanVien nv = (Model.NhanVien)obj;

                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                // Câu lệnh UPDATE dùng biến bind chứ không phải hardcoded như NEWDT
                using (var cmd = new OracleCommand(@"
                    UPDATE QLDL.VIEW_NHANVIEN_NVCB 
                    SET DT = :newDt 
                    WHERE MANV = SYS_CONTEXT('X_UNIVERITY_CONTEXT', 'USER_NAME')", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text; // ✅ Rất quan trọng: Vì đây là SQL thường, không phải stored procedure

                    cmd.Parameters.Add("newDt", OracleDbType.Varchar2).Value = nv.dt;

                    int rowsAffected = cmd.ExecuteNonQuery(); // Không cần dùng output param

                    return rowsAffected > 0;
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
