using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
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
            try
            {
                var sv = obj as Model.SinhVien;

                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                string sql = @"
                INSERT INTO QLDL.SINHVIEN 
                (MASV, HOTEN, PHAI, NGSINH, DCHI, DT, KHOA,COSO) 
                VALUES ('SV'||qldl.sinhvien_seq.NEXTVAL, :p_hoTen, :p_phai, :p_ngSinh, :p_dChi, :p_dt, :p_khoa,:p_coso)";
                
                using (OracleCommand cmd = new OracleCommand(sql, sqlConnection))
                {
                    cmd.CommandType = CommandType.Text; // Thực thi câu SQL thuần
                    cmd.Parameters.Add("p_hoTen", OracleDbType.Varchar2).Value = sv.hoTen;
                    cmd.Parameters.Add("p_phai", OracleDbType.Varchar2).Value = sv.phai;
                    cmd.Parameters.Add("p_ngSinh", OracleDbType.Date).Value = sv.ngSinh;
                    cmd.Parameters.Add("p_dChi", OracleDbType.Varchar2).Value = sv.dChi;
                    cmd.Parameters.Add("p_dt", OracleDbType.Varchar2).Value = sv.dt;
                    cmd.Parameters.Add("p_khoa", OracleDbType.Varchar2).Value = sv.khoa;
                    cmd.Parameters.Add("p_coso", OracleDbType.Varchar2).Value = sv.coso;

                     
                    int row = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    // Đóng kết nối sau khi thực hiện xong
                    return row > 0; // Trả về true nếu có ít nhất một dòng được thêm vào


                }


            }
            catch (OracleException ex)
            {

                MessageBox.Show("Lỗi Oracle: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public bool Delete(object obj)
        {
            try
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
            catch (OracleException ex)
            {

                MessageBox.Show("Lỗi Oracle: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
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

            try
            {


                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                Model.SinhVien sv = (Model.SinhVien)obj;
                
                using (var cmd = new OracleCommand("UPDATE QLDL.SINHVIEN  SET HOTEN = :p_hoTen, PHAI = :p_phai, NGSINH = :p_ngSinh, DCHI = :p_dChi, DT = :p_dt, KHOA = :p_khoa, COSO = :p_coso WHERE MASV = :p_maSV", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text; // ❗Vì đây là SQL thường

                    cmd.Parameters.Add("p_hoTen", OracleDbType.Varchar2).Value = sv.hoTen ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_phai", OracleDbType.Varchar2).Value = sv.phai ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_ngSinh", OracleDbType.Date).Value =
                    sv.ngSinh.HasValue ? (object)sv.ngSinh.Value : DBNull.Value;


                    cmd.Parameters.Add("p_dChi", OracleDbType.Varchar2).Value = sv.dChi ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_dt", OracleDbType.Varchar2).Value = sv.dt ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_khoa", OracleDbType.Varchar2).Value = sv.khoa ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_maSV", OracleDbType.Varchar2).Value = sv.maSV ?? (object)DBNull.Value;
                    cmd.Parameters.Add("p_coso", OracleDbType.Varchar2).Value = sv.coso ?? (object)DBNull.Value;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
   
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lỗi Oracle: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
