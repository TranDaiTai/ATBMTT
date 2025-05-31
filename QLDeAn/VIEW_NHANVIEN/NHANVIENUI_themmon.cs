using QLDeAn.DataAccess.MoMon;
using QLDeAn.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDeAn.VIEW_NHANVIEN
{
    public partial class NHANVIENUI_themmon: Form
    {
        public NHANVIENUI_themmon()
        {
            InitializeComponent();
        }
        private  MoMonNVPDTDao dao = new MoMonNVPDTDao(LoginUI.con);
        private void BTN_THEM_Click(object sender, EventArgs e)
        {
            if (TB_MAHOCPHAN.Text == "" || TB_MAGIAOVIEN.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin môn học!");
                return;
            }

            MoMon mm = new MoMon
            {
                MAHP = TB_MAHOCPHAN.Text,
                MAGV = TB_MAGIAOVIEN.Text
            };
            try
            {
                if (dao.Add(mm))
                {
                    MessageBox.Show("thêm môn thành công.");
                    this.Close(); // Đóng form sau khi thêm thành công
                }
                else
                {
                    MessageBox.Show("thêm môn thất bại. Vui lòng kiểm tra lại thông tin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}");
            }
        }

        private void BTN_huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
