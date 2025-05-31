using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLDeAn.DataAccess.MoMon;
using QLDeAn.Model;
using QLDeAn.DataAccess.DangKy;

namespace QLDeAn.VIEW_SINHVIEN
{
    public partial class SINHVIENUI_DANGKYMON : UserControl
    {
        public SINHVIENUI_DANGKYMON()
        {
            InitializeComponent();
        }
        private static IMoMonDao dao = null;
        private static MoMon selected_monhoc = null;
        public void Load_DangKyhocPhan()
        {
            dao = new MoMonSVDao(LoginUI.con);


            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                dataGridView1.DataSource = data.Select(x => (Model.MoMon)x).ToList();
            }
            else
            {
            }

        }

        private void Refesh_Dangkyhocphan()
        {

            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                dataGridView1.DataSource = data.Select(x => (Model.MoMon)x).ToList();
            }
            else
            {
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua nếu click header

            var row = dataGridView1.Rows[e.RowIndex];

            selected_monhoc = (MoMon)row.DataBoundItem;

            TB_HOCKI.Text = row.Cells["HK"].Value?.ToString() ?? "";
            TB_MAMONHOC.Text = row.Cells["maMM"].Value?.ToString() ?? "";
            TB_NAM.Text = row.Cells["nam"].Value?.ToString() ?? "";
            TB_MAGIAOVIEN.Text = row.Cells["maGV"].Value?.ToString() ?? "";
        }

        private void BTN_XOA_Click(object sender, EventArgs e)
        {
            Refesh_Dangkyhocphan();
        }

        private void BTN_CAPNHAT_Click(object sender, EventArgs e)
        {
            Refesh_Dangkyhocphan();
        }



        private void BTN_DANGKY_Click(object sender, EventArgs e)
        {
            var dao_dangky = new DangKySVDao(LoginUI.con);
           
            if (selected_monhoc == null)
            {
                MessageBox.Show("Vui lòng chọn môn học để đăng ký.");
                return;
            }
            var dangky = new DangKy
            {
                maSV = LoginUI.userUser, // Sử dụng userUser từ LoginUI
                maMM = TB_MAMONHOC.Text.Trim() // Lấy mã môn học từ TextBox
            };
            if (dao_dangky.Add(dangky))
            {
                MessageBox.Show("Đăng ký môn học thành công!");
                Refesh_Dangkyhocphan();
            }
            else
            {
                MessageBox.Show("Đăng ký môn học thất bại. Vui lòng thử lại.");
            }
        }

        private void dataGridView1_CellClick_2(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua nếu click header

            var row = dataGridView1.Rows[e.RowIndex];

            selected_monhoc = (MoMon)row.DataBoundItem;

            TB_HOCKI.Text = row.Cells["HK"].Value?.ToString() ?? "";
            TB_MAMONHOC.Text = row.Cells["maMM"].Value?.ToString() ?? "";
            TB_NAM.Text = row.Cells["nam"].Value?.ToString() ?? "";
            TB_HOCPHAN.Text = row.Cells["maHP"].Value?.ToString() ?? "";
            TB_MAGIAOVIEN.Text = row.Cells["maGV"].Value?.ToString() ?? "";
        }
    }
}
