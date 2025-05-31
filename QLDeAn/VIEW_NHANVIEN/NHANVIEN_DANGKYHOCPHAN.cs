using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLDeAn.DataAccess.SinhVien;
using QLDeAn.Model;
using QLDeAn.DataAccess.DangKy;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace QLDeAn.VIEW_NHANVIEN
{
    public partial class NHANVIEN_DANGKYHOCPHAN: UserControl
    {
        public NHANVIEN_DANGKYHOCPHAN()
        {
            InitializeComponent();
            //Load_DangKyhocPhan();
            SetButtonsByRole();
        }
        private static IDangKyDao dao = null;
        private static DangKy selected_DangKy = null;
        public void Load_DangKyhocPhan()
        {

            if (NhanVienUI.roleUser == "GV")
            {
                dao = new DangKyGVDao(LoginUI.con);
            }
            else if (NhanVienUI.roleUser == "NV PĐT")
            {
                dao = new DangKyNVPDTDao(LoginUI.con);
            }
            else if (NhanVienUI.roleUser == "NV PKT")
            {
                dao = new DangKyNVPKTDao(LoginUI.con);
            }
            else
            {
                return;
            }
            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                dataGridView1.DataSource = data.Select(x => (Model.DangKy)x).ToList();
            }
            else
            {
            }

        }
        private void SetButtonsByRole()
        {
            switch (NhanVienUI.roleUser)
            {
                case "NV PĐT":
                    BTN_CAPNHAT.Visible = true;
                    BTN_THÊM.Visible = true;
                    BTN_XOA.Visible = true;


                    TB_MASINHVIEN.ReadOnly = false;
                    TB_Mamonhoc.ReadOnly = false;

                    break;
                case "NV PKT":
                    BTN_CAPNHAT.Visible = true;
                    BTN_THÊM.Visible = false;
                    BTN_XOA.Visible = false;


                    TB_DiemQT.ReadOnly = false;
                    TB_DIEMCK.ReadOnly = false;
                    TB_Diemth.ReadOnly = false;
                    TB_DIEMTK.ReadOnly = false;
                    break;
                default:
                    // Ẩn hết nếu không xác định được vai trò
                    BTN_CAPNHAT.Visible = false;
                    BTN_THÊM.Visible = false;
                    BTN_XOA.Visible = false;
                    break;
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

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (selected_DangKy == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.");
                return;
            }
            if (dao.Delete(selected_DangKy))
            {
                MessageBox.Show("Xóa thành công.");
            }
            else
            {
                MessageBox.Show("Xóa thất bại. Vui lòng kiểm tra lại thông tin.");
            }
            selected_DangKy = null; // Reset sau khi xóa
            Refesh_Dangkyhocphan();
        }

        private void BTN_CAPNHAT_Click(object sender, EventArgs e)
        {
            if (selected_DangKy == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để cập nhật.");
                return;
            }
            if (dao.Update(selected_DangKy))
            {
                MessageBox.Show("Cập nhật thành công.");
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra lại thông tin.");
            }
            selected_DangKy = null;
            Refesh_Dangkyhocphan();

        }

        private void BTN_THÊM_Click(object sender, EventArgs e)
        {

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
    }
}
