using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDeAn.DataAccess.DangKy
{
    class DangKyNVPDTDao : IDangKyDao
    {
        private OracleConnection sqlConnection;
        public DangKyNVPDTDao(OracleConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }
        public bool Add(object obj)
        {
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }
                Model.DangKy dk = (Model.DangKy)obj;
                using (var cmd = new OracleCommand("INSERT INTO QLDL.DANGKY(MASV, MAMM) VALUES (:MaSV_, :MaMM_)", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text; // Sử dụng SQL thường
                    cmd.Parameters.Add(new OracleParameter("MaSV_", dk.maSV));
                    cmd.Parameters.Add(new OracleParameter("MaMM_", dk.maMM));
                    cmd.ExecuteNonQuery();
                }

                sqlConnection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool Delete(object obj)
        {
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                Model.DangKy dk = (Model.DangKy)obj;

                string sql = "DELETE FROM QLDL.DANGKY WHERE MAMM = :MaMM_ AND MASV = :MaSV_";

                using (var cmd = new OracleCommand(sql, sqlConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new OracleParameter("MaSV_", dk.maSV));
                    cmd.Parameters.Add(new OracleParameter("MaMM_", dk.maMM));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Có thể log ex nếu cần
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

        public List<object> Load(object obj)
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            List<Model.DangKy> result = new List<Model.DangKy>();
            using (var cmd = new OracleCommand("SELECT * FROM QLDL.DANGKY", sqlConnection))
            {
                cmd.CommandType = CommandType.Text;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dk = new Model.DangKy
                        {
                            maSV = reader["maSV"].ToString(),
                            maMM = reader["maMM"].ToString(),
                            diemTH = reader["diemTH"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["diemTH"]),
                            diemQT = reader["diemCT"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["diemCT"]),
                            diemCK = reader["diemCK"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["diemCK"]),
                            diemTK = reader["diemTK"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["diemTK"]),
                        }
                        ;
                        result.Add(dk);
                    }
                }
            }
            sqlConnection.Close();
            return result.Cast<object>().ToList();
        }

        public bool Update(object obj)
        {
            return true;
        }
    }
}
