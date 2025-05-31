using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace QLDeAn.DataAccess.NhanVien
{
    class NhanVienNVTCHCDao : INhanVienDao
    {
        private OracleConnection sqlConnection;

        public NhanVienNVTCHCDao(OracleConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        public bool Add(object obj)
        {
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                using (var cmd = new OracleCommand(@"INSERT INTO QLDL.NHANVIEN 
                    (MANV, HOTEN, PHAI, NGSINH, LUONG, PHUCAP, DT, VAITRO, MADV)
                    VALUES (:MaNLD, :HoTen, :PHAI, :NgaySinh, :Luong, :PhuCap, :SDT, :VaiTro, :MaDV)", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text;
                    var nv = (Model.NhanVien)obj;

                    cmd.Parameters.Add("MaNLD", OracleDbType.Varchar2).Value = nv.maNV;
                    cmd.Parameters.Add("HoTen", OracleDbType.Varchar2).Value = nv.hoTen;
                    cmd.Parameters.Add("PHAI", OracleDbType.Varchar2).Value = nv.phai;
                    cmd.Parameters.Add("NgaySinh", OracleDbType.Date).Value = nv.ngSinh;
                    cmd.Parameters.Add("Luong", OracleDbType.Int32).Value = nv.luong;
                    cmd.Parameters.Add("PhuCap", OracleDbType.Int32).Value = nv.phuCap;
                    cmd.Parameters.Add("SDT", OracleDbType.Varchar2).Value = nv.dt;
                    cmd.Parameters.Add("VaiTro", OracleDbType.Varchar2).Value = nv.vaiTro;
                    cmd.Parameters.Add("MaDV", OracleDbType.Varchar2).Value = nv.maDV;

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool Delete(object obj)
        {
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                using (var cmd = new OracleCommand("DELETE FROM QLDL.NHANVIEN WHERE MANV = :MaNLD", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text;
                    var nv = (Model.NhanVien)obj;
                    cmd.Parameters.Add("MaNLD", OracleDbType.Varchar2).Value = nv.maNV;

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<object> Load(object obj)
        {
            var result = new List<Model.NhanVien>();
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                using (var cmd = new OracleCommand("SELECT * FROM QLDL.NHANVIEN", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nv = new Model.NhanVien
                            {
                                maNV = reader["maNV"].ToString(),
                                hoTen = reader["hoTen"].ToString(),
                                phai = reader["phai"].ToString(),
                                ngSinh = Convert.ToDateTime(reader["ngSinh"]),
                                luong = reader["luong"] != DBNull.Value ? Convert.ToInt32(reader["luong"]) : 0,
                                phuCap = reader["phuCap"] != DBNull.Value ? Convert.ToInt32(reader["phuCap"]) : 0,
                                dt = reader["dt"].ToString(),
                                vaiTro = reader["vaiTro"].ToString(),
                                maDV = reader["maDV"].ToString(),
                                coso = reader["coso"].ToString()

                            };
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
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                using (var cmd = new OracleCommand(@"
                    UPDATE QLDL.NHANVIEN 
                    SET HOTEN = :HoTen, 
                        PHAI = :Phai, 
                        NGSINH = :NgaySinh, 
                        LUONG = :Luong, 
                        PHUCAP = :PhuCap, 
                        DT = :SDT, 
                        VAITRO = :VaiTro, 
                        MADV = :MaDV 
                    WHERE MANV = :MaNLD", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text;
                    var nv = (Model.NhanVien)obj;

                    cmd.Parameters.Add("HoTen", OracleDbType.Varchar2).Value = nv.hoTen;
                    cmd.Parameters.Add("Phai", OracleDbType.Varchar2).Value = nv.phai;
                    cmd.Parameters.Add("NgaySinh", OracleDbType.Date).Value = nv.ngSinh;
                    cmd.Parameters.Add("Luong", OracleDbType.Int32).Value = nv.luong;
                    cmd.Parameters.Add("PhuCap", OracleDbType.Int32).Value = nv.phuCap;
                    cmd.Parameters.Add("SDT", OracleDbType.Varchar2).Value = nv.dt;
                    cmd.Parameters.Add("VaiTro", OracleDbType.Varchar2).Value = nv.vaiTro;
                    cmd.Parameters.Add("MaDV", OracleDbType.Varchar2).Value = nv.maDV;
                    cmd.Parameters.Add("MaNLD", OracleDbType.Varchar2).Value = nv.maNV;

                    int rowAffected = cmd.ExecuteNonQuery();

                    return rowAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
