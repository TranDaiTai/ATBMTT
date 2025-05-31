using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace QLDeAn.DataAccess.MoMon
{
    class MoMonTRGDVDao : IMoMonDao
    {
        private OracleConnection sqlConnection;

        public MoMonTRGDVDao(OracleConnection sqlConnection)
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
            List<Model.MoMon> result = new List<Model.MoMon>();
            using (var cmd = new OracleCommand("SELECT * FROM QLDL.VIEW_MOMON_TRGDV", sqlConnection))
            {
                cmd.CommandType = CommandType.Text; // Phải để Text khi chạy câu lệnh SQL thuần

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var mm = new Model.MoMon
                        {
                            MAMM = reader["maMM"].ToString(),
                            MAHP = reader["maHP"].ToString(),
                            MAGV = reader["maGV"].ToString(),
                            // Gán mặc định, sau đó kiểm tra null để set riêng
                        };

                        if (reader["hk"] != DBNull.Value)
                            mm.HK = Convert.ToInt32(reader["hk"]);

                        if (reader["nam"] != DBNull.Value)
                            mm.NAM = Convert.ToInt32(reader["nam"]);

                        result.Add(mm);
                    }
                }
            }


            return result.Cast<object>().ToList();
        }

        public bool Update(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
