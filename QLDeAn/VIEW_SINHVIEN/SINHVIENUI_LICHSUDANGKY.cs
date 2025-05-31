using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLDeAn.DataAccess.DangKy;
using QLDeAn.Model;
using QLDeAn.VIEW_NHANVIEN;

namespace QLDeAn.VIEW_SINHVIEN
{
    public partial class SINHVIENUI_LICHSUDANGKY : UserControl
    {
        public SINHVIENUI_LICHSUDANGKY()
        {
            InitializeComponent();
        }

    
        private static IDangKyDao dao = null;
        private static DangKy selected_DangKy = null;
        public void Load_DangKyhocPhan()
        {
            dao = new DangKySVDao(LoginUI.con);


            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                dataGridView1.DataSource = data.Select(x => (Model.DangKy)x).ToList();
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
                dataGridView1.DataSource = data.Select(x => (Model.DangKy)x).ToList();
            }
            else
            {
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua nếu click header

            var row = dataGridView1.Rows[e.RowIndex];
            selected_DangKy = (DangKy)row.DataBoundItem;

            TB_DIEMCK.Text = row.Cells["diemck"].Value?.ToString() ?? "";
            TB_MASINHVIEN.Text = row.Cells["maSV"].Value?.ToString() ?? "";
            TB_DIEMTK.Text = row.Cells["diemtk"].Value?.ToString() ?? "";
            TB_DiemQT.Text = row.Cells["diemqt"].Value?.ToString() ?? "";
            TB_Diemth.Text = row.Cells["diemth"].Value?.ToString() ?? "";
            TB_Mamonhoc.Text = row.Cells["maMM"].Value?.ToString() ?? "";
        }

        private void BTN_XOA_Click(object sender, EventArgs e)
        {
            if (selected_DangKy == null)
            {
                MessageBox.Show("Vui lòng chọn một đăng ký để xóa.");
                return;
            }
            
            dao.Delete(selected_DangKy);
            selected_DangKy = null; // Reset selected_DangKy after deletion
            Refesh_Dangkyhocphan();
        }

        //private void BTN_CAPNHAT_Click(object sender, EventArgs e)
        //{
        //    if (selected_DangKy == null)
        //    {
        //        MessageBox.Show("Vui lòng chọn một đăng ký để cập nhật.");
        //        return;
        //    }
        //    dao.Update(selected_DangKy);
        //    selected_DangKy = null; // Reset selected_DangKy after deletion
        //    Refesh_Dangkyhocphan();

        //}
    }
}
