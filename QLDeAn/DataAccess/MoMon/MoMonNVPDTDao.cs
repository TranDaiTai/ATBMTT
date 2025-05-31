using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDeAn.DataAccess.MoMon
{
    class MoMonNVPDTDao : IMoMonDao
    {
        private OracleConnection sqlConnection;
        public MoMonNVPDTDao(OracleConnection sqlConnection)
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
                Model.MoMon mm = (Model.MoMon)obj;
                using (var cmd = new OracleCommand(@"
                    INSERT INTO QLDL.VIEW_MOMON_PDT (MaMM, MaHP, MaGV, HK, NAM)
                    VALUES ('MM' || QLDL.momon_seq.NEXTVAL, :MaHP, :MaGV, QLDL.CURRENT_HK(), QLDL.CURRENT_NAM())", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new OracleParameter("MaHP", OracleDbType.Varchar2)).Value = mm.MAHP;
                    cmd.Parameters.Add(new OracleParameter("MaGV", OracleDbType.Varchar2)).Value = mm.MAGV;

                    int row = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    return row > 0;
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
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                Model.MoMon mm = (Model.MoMon)obj;

                using (var cmd = new OracleCommand("DELETE FROM QLDL.VIEW_MOMON_PDT WHERE MAMM = :Ma", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text; // sửa từ StoredProcedure sang Text
                    cmd.Parameters.Add("Ma", OracleDbType.Varchar2).Value = mm.MAMM;

                    int rowsAffected = cmd.ExecuteNonQuery(); // lấy số dòng bị ảnh hưởng

                    sqlConnection.Close();
                    return rowsAffected > 0; // chỉ trả true nếu có dòng bị xóa
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
            List<Model.MoMon> result = new List<Model.MoMon>();
            using (var cmd = new OracleCommand("SELECT * FROM QLDL.VIEW_MOMON_PDT", sqlConnection))
            {
                cmd.CommandType = CommandType.Text;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var mm = new Model.MoMon
                        {
                            MAMM = reader["maMM"].ToString(),
                            MAHP = reader["maHP"].ToString(),
                            MAGV = reader["maGV"].ToString(),
                            HK = reader["hk"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["hk"]),
                            NAM = reader["nam"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["nam"]),
                        }
                        ;
                        result.Add(mm);
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

                Model.MoMon mm = (Model.MoMon)obj;

                using (var cmd = new OracleCommand(
                    "UPDATE QLDL.VIEW_MOMON_PDT SET MaHP = :MaHP_, MaGV = :MaGV_ " +
                    "WHERE MaMM = :MaMM_ AND HK = :HK_ AND NAM = :NAM_", sqlConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("MaHP_", OracleDbType.Varchar2).Value = mm.MAHP;
                    cmd.Parameters.Add("MaGV_", OracleDbType.Varchar2).Value = mm.MAGV;
                    cmd.Parameters.Add("MaMM_", OracleDbType.Varchar2).Value = mm.MAMM;
                    cmd.Parameters.Add("HK_", OracleDbType.Int32).Value = mm.HK;
                    cmd.Parameters.Add("NAM_", OracleDbType.Int32).Value = mm.NAM;

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
                    sqlConnection.Close();
            }
        }

    }
}
