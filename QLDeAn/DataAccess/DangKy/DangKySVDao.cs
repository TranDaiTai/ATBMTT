using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace QLDeAn.DataAccess.DangKy
{
    class DangKySVDao : IDangKyDao
    {
        private OracleConnection sqlConnection;
        public DangKySVDao(OracleConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }
        public bool Add(object obj)
        {
            var dk = obj as Model.DangKy;

            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            using (OracleCommand cmd = new OracleCommand("INSERT INTO QLDL.V_DANGKY (MASV, MAMM) VALUES (:p_maSV, :p_maMM)", sqlConnection))
            {
                cmd.CommandType = CommandType.Text;  //  dùng lệnh SQL trực tiếp
                cmd.Parameters.Add("p_maSV", OracleDbType.Varchar2).Value = dk.maSV;
                cmd.Parameters.Add("p_maMM", OracleDbType.Varchar2).Value = dk.maMM;

                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }

        }

        public bool Delete(object obj)
        {
            var dk = obj as Model.DangKy;

            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            using (OracleCommand cmd = new OracleCommand("DELETE FROM QLDL.V_DANGKY WHERE MASV = :p_maSV AND MAMM = :p_maMM", sqlConnection))
            {
                cmd.CommandType = CommandType.Text;  // Sửa thành Text

                cmd.Parameters.Add("p_maSV", OracleDbType.Varchar2).Value = dk.maSV;
                cmd.Parameters.Add("p_maMM", OracleDbType.Varchar2).Value = dk.maMM;

                int rowAffected = cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return rowAffected > 0;
            }

        }

        public List<object> Load(object obj)
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            List<Model.DangKy> result = new List<Model.DangKy>();

            try
            {
                using (var cmd = new OracleCommand("SELECT * FROM QLDL.V_DANGKY", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text; // ✅ sửa lại từ StoredProcedure

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dk = new Model.DangKy
                            {
                                maSV = reader["maSV"].ToString(),
                                maMM = reader["maMM"].ToString(),
                                diemTH = reader["diemTH"] != DBNull.Value ? (short?)Convert.ToInt16(reader["diemTH"]) : null,
                                diemQT = reader["diemCT"] != DBNull.Value ? (short?)Convert.ToInt16(reader["diemCT"]) : null,
                                diemCK = reader["diemCK"] != DBNull.Value ? (short?)Convert.ToInt16(reader["diemCK"]) : null,
                                diemTK = reader["diemTK"] != DBNull.Value ? (short?)Convert.ToInt16(reader["diemTK"]) : null
                            };

                            result.Add(dk);
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
            var dk = obj as Model.DangKy;

            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            try
            {
                using (OracleCommand cmd = new OracleCommand(
                    "UPDATE QLDL.V_DANGKY SET MAMM = :p_maMM WHERE MASV = :p_maSV", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text; // ❗Vì đây là câu SQL thường

                    cmd.Parameters.Add("p_maMM", OracleDbType.Varchar2).Value = dk.maMM;
                    cmd.Parameters.Add("p_maSV", OracleDbType.Varchar2).Value = dk.maSV;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật: " + ex.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }


    }
}
