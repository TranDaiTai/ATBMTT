using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDeAn.Model;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace QLDeAn.DataAccess.SinhVien
{
    class SinhVienNVPDTDao : ISinhVienDao
    {
        private OracleConnection sqlConnection;
        public SinhVienNVPDTDao(OracleConnection sqlConnection)
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

            using (var cmd = new OracleCommand("SELECT * FROM QLSL.SINHVIEN", sqlConnection))
            {
                cmd.CommandType = CommandType.Text; // ✅ Sửa lại ở đây

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
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                Model.SinhVien sinhVien = (Model.SinhVien)obj;

                using (var cmd = new OracleCommand("UPDATE QLDL.SINHVIEN SET TINHTRANG = :TINHTRANG_ WHERE MASV = :MaSV_", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text; // ❗Vì đây là SQL thường

                    cmd.Parameters.Add("TINHTRANG_", OracleDbType.Varchar2).Value = sinhVien.TINHTRANG;
                    cmd.Parameters.Add("MaSV_", OracleDbType.Varchar2).Value = sinhVien.maSV;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Có thể log lỗi: ex.Message
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
