using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDeAn.DataAccess.NhanVien
{
    internal class NhanVienNVCBDao : INhanVienDao
    {
        private OracleConnection sqlConnection;
        public NhanVienNVCBDao(OracleConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }
        public bool Add(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public List<object> Load(object obj)
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            List<Model.NhanVien> result = new List<Model.NhanVien>();

            // Sử dụng câu SQL thường thay vì stored procedure
            using (var cmd = new OracleCommand("SELECT * FROM QLDL.VIEW_NHANVIEN_NVCB", sqlConnection))
            {
                cmd.CommandType = CommandType.Text; // <- Sửa tại đây

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var nv = new Model.NhanVien
                        {
                            maNV = reader["MANV"].ToString(),
                            hoTen = reader["HOTEN"].ToString(),
                            phai = reader["PHAI"].ToString(),
                            ngSinh = Convert.ToDateTime(reader["NGSINH"]),
                            luong = reader["LUONG"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["LUONG"]),
                            phuCap = reader["PHUCAP"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["PHUCAP"]),
                            dt = reader["DT"].ToString(),
                            vaiTro = reader["VAITRO"].ToString(),
                            maDV = reader["MADV"].ToString(),
                            coso = reader["COSO"].ToString()


                        };

                        result.Add(nv);
                    }
                }
            }

            sqlConnection.Close();
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
