using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace QLDeAn.Model
{

    public class NhanVien 
    {
        public string maNV { get; set; }
        public string hoTen { get; set; }
        public string phai { get; set; }
        public DateTime? ngSinh { get; set; }
        public int? luong { get; set; }
        public int? phuCap { get; set; }
        public string dt { get; set; }
        public string vaiTro { get; set; }
        public string maDV { get; set; }
        public string coso { get; set; }

        public NhanVien()
        {
            maNV = "";
            hoTen = "";
            phai = "";
            ngSinh = null;
            luong = null;
            phuCap = null;
            dt = "";
            vaiTro = "";
            maDV = "";
            coso = "";

        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
