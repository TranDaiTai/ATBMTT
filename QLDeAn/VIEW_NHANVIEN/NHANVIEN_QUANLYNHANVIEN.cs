using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLDeAn.DataAccess.NhanVien;
using QLDeAn.Model;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace QLDeAn.VIEW_NHANVIEN
{
    public partial class NHANVIEN_QUANLYNHANVIEN: UserControl
    {
        public NHANVIEN_QUANLYNHANVIEN()
        {
            InitializeComponent();
            //Load_Nhanvien();
            SetButtonsByRole();
        }
        private static INhanVienDao dao = null;
        private static NhanVien selected_nhanvien = null;

        public void Load_Nhanvien()
        {
          
            if (NhanVienUI.roleUser == "NV TCHC")
            {
                dao = new NhanVienNVTCHCDao(LoginUI.con);
            }
            else if (NhanVienUI.roleUser == "TRGDV")
            {
                dao = new NhanVienTRGDVDao(LoginUI.con);
            }
            else
            {
                return;
            }
            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                dataGridView1.DataSource = data.Select(x => (Model.NhanVien)x).ToList();
            }
            else
            {
            }

        }
        private void SetButtonsByRole()
        {
            switch (NhanVienUI.roleUser)
            {
                case "NV TCHC":
                    BTN_CAPNHAT.Visible = true;
                    BTN_THÊM.Visible = true;
                    BTN_XOA.Visible = true;

                    // Thiết lập quyền truy cập cho các trường
                    TB_COSO.ReadOnly = false;
                    TB_DT.ReadOnly = false;
                    TB_HOTEN.ReadOnly = false;
                    TB_LUONG.ReadOnly = false;
                    TB_MADV.ReadOnly = false;
                    TB_NGSINH.ReadOnly = false;
                    TB_PHUCAP.ReadOnly = false  ;
                    TB_VAITRO.ReadOnly = false  ;
                    TB_PHAI.ReadOnly = false;
                    break;

                default:
                    // Ẩn hết nếu không xác định được vai trò
                    BTN_CAPNHAT.Visible = false;
                    BTN_THÊM.Visible = false;
                    BTN_XOA.Visible = false;
                    break;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

       

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua nếu click header

            var row = dataGridView1.Rows[e.RowIndex];
            selected_nhanvien = (NhanVien)row.DataBoundItem;

            TB_COSO.Text = row.Cells["coso"].Value?.ToString() ?? "";
            TB_DT.Text = row.Cells["dt"].Value?.ToString() ?? "";
            TB_HOTEN.Text = row.Cells["hoTen"].Value?.ToString() ?? "";
            TB_LUONG.Text = row.Cells["luong"].Value?.ToString() ?? "";
            TB_MADV.Text = row.Cells["maDV"].Value?.ToString() ?? "";
            TB_MANHANVIEN.Text = row.Cells["maNV"].Value?.ToString() ?? "";
            TB_NGSINH.Text = row.Cells["ngSinh"].Value is DateTime date ? date.ToString("dd/MM/yyyy"): "";
            TB_PHUCAP.Text = row.Cells["phuCap"].Value?.ToString() ?? "";
            TB_VAITRO.Text = row.Cells["vaiTro"].Value?.ToString() ?? "";
            TB_PHAI.Text = row.Cells["phai"].Value?.ToString() ?? "";
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if ( selected_nhanvien == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.");
                return;
            }
            if(dao.Delete(selected_nhanvien))
            {
                MessageBox.Show("Xóa thành công.");
            }
            else
            {
                MessageBox.Show("Xóa thất bại. Vui lòng kiểm tra lại thông tin.");
            }
            selected_nhanvien = null; // Reset sau khi xóa
            Refesh_Nhanvien();
        }

        private void BTN_CAPNHAT_Click(object sender, EventArgs e)
        {
            if (selected_nhanvien == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để cập nhật.");
                return;
            }
            NHANVIENUI_CAPNHATNHANVIEN nvui_capnhatnv = new NHANVIENUI_CAPNHATNHANVIEN();
            nvui_capnhatnv.Tag = selected_nhanvien; // Truyền nhân viên đã chọn vào form cập nhật
            nvui_capnhatnv.Show(); // Hiển thị form cập nhật
            selected_nhanvien = null; 
            Refesh_Nhanvien();

        }

        private void BTN_THÊM_Click(object sender, EventArgs e)
        {
            NHANVIENUI_THEMNHANVIEN themNhanVienForm = new NHANVIENUI_THEMNHANVIEN();
            themNhanVienForm.Show();
        }
        private void Refesh_Nhanvien()
        {

            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                dataGridView1.DataSource = data.Select(x => (Model.NhanVien)x).ToList();
            }
            else
            {
            }

        }
    }
}
