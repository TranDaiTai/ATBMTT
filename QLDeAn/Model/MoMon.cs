using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace QLDeAn.Model
{
    public class MoMon : INotifyPropertyChanged
    {
        public string MAMM { get; set; }
        public string MAHP { get; set; }
        public string MAGV { get; set; }
        public int? HK { get; set; }
        public int? NAM { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MoMon()
        {
            MAMM = null;
            MAHP = null;
            MAGV = null;
            HK = 1;
            NAM = 2025;
        }
    }
}
