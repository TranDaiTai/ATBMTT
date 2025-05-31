using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDeAn.Model;
using Oracle.ManagedDataAccess.Client;

namespace QLDeAn.DataAccess.SinhVien
{
    class SinhVienSVDao : ISinhVienDao
    {
        private OracleConnection sqlConnection;
        public SinhVienSVDao(OracleConnection sqlConnection)
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
            List<Model.SinhVien> result = new List<Model.SinhVien>();

            using (var cmd = new OracleCommand("SELECT * FROM V_SINHVIEN", sqlConnection))
            {
                cmd.CommandType = CommandType.Text;  // Sửa lại đây

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sv = new Model.SinhVien
                        {
                            maSV = reader["maSV"].ToString(),
                            hoTen = reader["hoTen"].ToString(),
                            phai = reader["phai"].ToString(),
                            ngSinh = Convert.ToDateTime(reader["ngSinh"]),
                            dChi = reader["dChi"].ToString(),
                            dt = reader["dt"].ToString(),
                            khoa = reader["khoa"].ToString(),
                            TINHTRANG = reader["tinhTrang"].ToString(),
                            isInDB = true
                        };

                        result.Add(sv);
                    }
                }
            }

            return result.Cast<object>().ToList();
        }


        public bool Update(object obj)
        {
            var sv = obj as Model.SinhVien;

            if (sv == null)
                return false;

            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            using (OracleCommand cmd = new OracleCommand(
                "UPDATE QLDL.SINHVIEN SET DCHI = :p_dChi, DT = :p_dt WHERE MASV = :p_maSV", sqlConnection))
            {
                cmd.CommandType = CommandType.Text;  // Thực thi câu lệnh SQL bình thường

                cmd.Parameters.Add("p_dChi", OracleDbType.Varchar2).Value = sv.dChi ?? (object)DBNull.Value;
                cmd.Parameters.Add("p_dt", OracleDbType.Varchar2).Value = sv.dt ?? (object)DBNull.Value;
                cmd.Parameters.Add("p_maSV", OracleDbType.Varchar2).Value = sv.maSV;

                int rowsAffected = cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return rowsAffected > 0;
            }
        }

    }
}
