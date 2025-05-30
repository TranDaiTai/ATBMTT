using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace QLDeAn.Model
{
    public class SinhVien :  INotifyPropertyChanged
    {
        public string maSV { get; set; }
        public string hoTen { get; set; }
        public string phai { get; set; }
        public DateTime? ngSinh { get; set; }
        public string dChi { get; set; }
        public string dt { get; set; }
        public string khoa { get; set; }
        public string TINHTRANG { get; set; }
        public bool? isInDB { get; set; }

        public SinhVien()
        {
            maSV = null;
            hoTen = "Họ tên";
            phai = null;
            ngSinh = DateTime.Now;
            dChi = "Địa chỉ";
            dt = "Điện thoại";
            khoa = null;
            TINHTRANG = null ;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
