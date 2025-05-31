using QLDeAn.DataAccess.NhanVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using QLDeAn.Model;

namespace QLDeAn.VIEW_NHANVIEN
{
    public partial class NhanVienUI_ChinhsuaTT: Form
    {
        public NhanVienUI_ChinhsuaTT()
        {
            InitializeComponent();
            conNow = LoginUI.con;

        }
        public static OracleConnection conNow;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BTN_CẬP_NHẬT_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 10)
            {
                MessageBox.Show("Thông tin không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var dao = new NhanVienNVCBDao(conNow);
                List<object> data = dao.Load(null);
                NhanVien nv = (NhanVien)data[0];
                nv.dt = textBox1.Text;
                // Giả sử đây là nơi gọi hàm cập nhật thông tin nhân viên
                // Cần thay thế bằng logic thực tế để cập nhật thông tin
                dao.Update(nv);
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form sau khi cập nhật thành công
            }
        }

        private void BTN_HUỶ_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
