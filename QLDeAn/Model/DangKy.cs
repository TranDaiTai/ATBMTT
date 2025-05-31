using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace QLDeAn.Model
{
    public class DangKy : INotifyPropertyChanged
    {
        public string maSV { get; set; }
        public string maMM { get; set; }
        public double? diemTH { get; set; }
        public double? diemQT { get; set; }
        public double? diemCK { get; set; }
        public double? diemTK { get; set; }

        public DangKy()
        {
            maMM = null;
            maSV = null;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
