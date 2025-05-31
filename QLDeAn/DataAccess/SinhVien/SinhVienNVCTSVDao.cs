using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace QLDeAn.DataAccess.SinhVien
{
    class SinhVienNVCTSVDao : ISinhVienDao
    {
        private OracleConnection sqlConnection;

        public SinhVienNVCTSVDao(OracleConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        public bool Add(object obj)
        {
            var sv = obj as Model.SinhVien;

            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            string sql = @"
                INSERT INTO QLDL.SINHVIEN 
                (MASV, HOTEN, PHAI, NGSINH, DCHI, DT, KHOA,COSO) 
                VALUES (:p_maSV, :p_hoTen, :p_phai, :p_ngSinh, :p_dChi, :p_dt, :p_khoa,:p_coso)";

            using (OracleCommand cmd = new OracleCommand(sql, sqlConnection))
            {
                cmd.CommandType = CommandType.Text; // Thực thi câu SQL thuần
                cmd.Parameters.Add("p_maSV", OracleDbType.Varchar2).Value = sv.maSV;
                cmd.Parameters.Add("p_hoTen", OracleDbType.Varchar2).Value = sv.hoTen;
                cmd.Parameters.Add("p_phai", OracleDbType.Varchar2).Value = sv.phai;
                cmd.Parameters.Add("p_ngSinh", OracleDbType.Date).Value = sv.ngSinh;
                cmd.Parameters.Add("p_dChi", OracleDbType.Varchar2).Value = sv.dChi;
                cmd.Parameters.Add("p_dt", OracleDbType.Varchar2).Value = sv.dt;
                cmd.Parameters.Add("p_khoa", OracleDbType.Varchar2).Value = sv.khoa;
                cmd.Parameters.Add("p_coso", OracleDbType.Varchar2).Value = sv.coso;

                cmd.ExecuteNonQuery();
            }

            sqlConnection.Close();
            return true;
        }

        public bool Delete(object obj)
        {
            var sv = obj as Model.SinhVien;

            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            string sql = "DELETE FROM QLDL.SINHVIEN WHERE MASV = :p_maSV";

            using (OracleCommand cmd = new OracleCommand(sql, sqlConnection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("p_maSV", OracleDbType.Varchar2).Value = sv.maSV;

                int rowsAffected = cmd.ExecuteNonQuery();

                sqlConnection.Close();
                return rowsAffected > 0;
            }
        }

        public List<object> Load(object obj)
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            List<Model.SinhVien> result = new List<Model.SinhVien>();

            string sql = "SELECT * FROM QLDL.SINHVIEN";

            using (var cmd = new OracleCommand(sql, sqlConnection))
            {
                cmd.CommandType = CommandType.Text;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sv = new Model.SinhVien
                        {
                            maSV = reader["MASV"].ToString(),
                            hoTen = reader["HOTEN"].ToString(),
                            phai = reader["PHAI"].ToString(),
                            ngSinh = reader["NGSINH"] != DBNull.Value ? Convert.ToDateTime(reader["NGSINH"]) : (DateTime?)null,
                            dChi = reader["DCHI"].ToString(),
                            dt = reader["DT"].ToString(),
                            khoa = reader["KHOA"].ToString(),
                            TINHTRANG = reader["TINHTRANG"] != DBNull.Value ? reader["TINHTRANG"].ToString() : null,
                            coso = reader["COSO"] != DBNull.Value ? reader["COSO"].ToString() : null
                        };

                        result.Add(sv);
                    }
                }
            }

            sqlConnection.Close();
            return result.Cast<object>().ToList();
        }

        public bool Update(object obj)
        {
            var sv = obj as Model.SinhVien;

            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            string sql = @"
                UPDATE QLDL.SINHVIEN 
                SET HOTEN = :p_hoTen, PHAI = :p_phai, NGSINH = :p_ngSinh, DCHI = :p_dChi, DT = :p_dt, KHOA = :p_khoa, COSO = :p_coso
                WHERE MASV = :p_maSV";

            using (OracleCommand cmd = new OracleCommand(sql, sqlConnection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("p_hoTen", OracleDbType.Varchar2).Value = sv.hoTen;
                cmd.Parameters.Add("p_phai", OracleDbType.Varchar2).Value = sv.phai;
                cmd.Parameters.Add("p_ngSinh", OracleDbType.Date).Value = sv.ngSinh;
                cmd.Parameters.Add("p_dChi", OracleDbType.Varchar2).Value = sv.dChi;
                cmd.Parameters.Add("p_dt", OracleDbType.Varchar2).Value = sv.dt;
                cmd.Parameters.Add("p_khoa", OracleDbType.Varchar2).Value = sv.khoa;
                cmd.Parameters.Add("p_maSV", OracleDbType.Varchar2).Value = sv.maSV;
                cmd.Parameters.Add("p_coso", OracleDbType.Varchar2).Value = sv.coso;

                int rowsAffected = cmd.ExecuteNonQuery();

                sqlConnection.Close();
                return rowsAffected > 0;
            }
        }
    }
}
