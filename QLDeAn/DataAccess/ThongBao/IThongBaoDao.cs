using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDeAn.Model;

namespace QLDeAn.DataAccess.ThongBao
{
    public interface IThongBaoDao: IBaseDao
    {
        List<LabelComponent> GetAllLevels();
        List<LabelComponent> GetAllDepartments();
        List<LabelComponent> GetAllGroups();
        bool SendNotification(string content, string label);


    }
}
