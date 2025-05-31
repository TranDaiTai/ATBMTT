using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace QLDeAn.DataAccess.DangKy
{
    class DangKyNVPKTDao : IDangKyDao
    {
        private OracleConnection sqlConnection;
        public DangKyNVPKTDao(OracleConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }
        public bool Add(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object obj)
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            try
            {
                var dangKy = (Model.DangKy)obj;

                string sql = @"DELETE FROM QLDL.DANGKY WHERE MASV = :p_MASV AND MAMM = :p_MAMM";

                using (var cmd = new OracleCommand(sql, sqlConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("p_MASV", OracleDbType.Varchar2).Value = dangKy.maSV;
                    cmd.Parameters.Add("p_MAMM", OracleDbType.Varchar2).Value = dangKy.maMM;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
        }

        public List<object> Load(object obj)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            List<Model.DangKy> result = new List<Model.DangKy>();
            try
            {
                using (var cmd = new OracleCommand("SELECT * FROM QLDL.DANGKY", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dk = new Model.DangKy
                            {
                                maMM = reader["MAMM"].ToString(),
                                maSV = reader["MASV"].ToString(),
                                diemTH = reader["DIEMTH"] != DBNull.Value ? (double?)Convert.ToDouble(reader["DIEMTH"]) : null,
                                diemQT = reader["DIEMQT"] != DBNull.Value ? (double?)Convert.ToDouble(reader["DIEMQT"]) : null,
                                diemCK = reader["DIEMCK"] != DBNull.Value ? (double?)Convert.ToDouble(reader["DIEMCK"]) : null,
                                diemTK = reader["DIEMTK"] != DBNull.Value ? (double?)Convert.ToDouble(reader["DIEMTK"]) : null
                            };

                            result.Add(dk);
                        }
                    }
                }

            }
            catch (System.Exception ex)
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
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            try
            {
                var dangKy = (Model.DangKy)obj;

                string sql = @"UPDATE QLDL.DANGKY 
                       SET DIEMTH = :p_DIEMTH, 
                           DIEMQT = :p_DIEMQT, 
                           DIEMCK = :p_DIEMCK, 
                           DIEMTK = :p_DIEMTK 
                       WHERE MAMM = :p_MAMM AND MASV = :p_MASV";

                using (var cmd = new OracleCommand(sql, sqlConnection))
                {
                    cmd.CommandType = CommandType.Text; // Sửa lại thành Text

                    cmd.Parameters.Add("p_DIEMTH", OracleDbType.Double).Value = dangKy.diemTH;
                    cmd.Parameters.Add("p_DIEMQT", OracleDbType.Double).Value = dangKy.diemQT;
                    cmd.Parameters.Add("p_DIEMCK", OracleDbType.Double).Value = dangKy.diemCK;
                    cmd.Parameters.Add("p_DIEMTK", OracleDbType.Double).Value = dangKy.diemTK;
                    cmd.Parameters.Add("p_MAMM", OracleDbType.Varchar2).Value = dangKy.maMM;
                    cmd.Parameters.Add("p_MASV", OracleDbType.Varchar2).Value = dangKy.maSV;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
        }
    }

}