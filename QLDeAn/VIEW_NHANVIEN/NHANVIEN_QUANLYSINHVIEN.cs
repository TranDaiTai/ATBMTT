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
using SD.LLBLGen.Pro.ORMSupportClasses;
using QLDeAn.DataAccess.SinhVien;
using QLDeAn.Model;

namespace QLDeAn.VIEW_NHANVIEN
{
    public partial class NHANVIEN_QUANLYSINHVIEN: UserControl
    {
        public NHANVIEN_QUANLYSINHVIEN()
        {
            InitializeComponent();
            Load_SinhVien();
            SetButtonsByRole();
        }
        private static ISinhVienDao dao = null;
        private static SinhVien selected_sinhvien = null;
        private void Load_SinhVien()
        {

            if (NhanVienUI.roleUser == "GV")
            {
                dao = new SinhVienGVDao(LoginUI.con);
            }
            else if (NhanVienUI.roleUser == "NV PĐT")
            {
                dao = new SinhVienNVPDTDao(LoginUI.con);
            }
            else if (NhanVienUI.roleUser == "NV CTSV")
            {
                dao = new SinhVienNVCTSVDao(LoginUI.con);
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
                    break;
                case "NV PĐT":
                    BTN_CAPNHAT.Visible = true;
                    BTN_THÊM.Visible = true;
                    BTN_XOA.Visible = true;
                    break;
                case "NV CTSV":
                    BTN_CAPNHAT.Visible = true;
                    BTN_THÊM.Visible = true;
                    BTN_XOA.Visible = true;
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
            selected_sinhvien = (SinhVien)row.DataBoundItem;

            TB_COSO.Text = row.Cells["coso"].Value?.ToString() ?? "";
            TB_HOTEN.Text = row.Cells["hoTen"].Value?.ToString() ?? "";
            TB_PHAI.Text = row.Cells["phai"].Value?.ToString() ?? "";
            TB_DIACHI.Text = row.Cells["diaChi"].Value?.ToString() ?? "";
            TB_KHOA.Text = row.Cells["khoa"].Value?.ToString() ?? "";
            TB_NGAYSINH.Text = row.Cells["ngSinh"].Value is DateTime date ? date.ToString("dd/MM/yyyy") : "";
            TB_MASINHVIEN.Text = row.Cells["maSV"].Value?.ToString() ?? "";
            TB_TINHTRANG.Text = row.Cells["tinhTrang"].Value?.ToString() ?? "";
            TB_SDT.Text = row.Cells["dt"].Value?.ToString() ?? "";
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (selected_sinhvien == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.");
                return;
            }
            if (dao.Delete(selected_sinhvien))
            {
                MessageBox.Show("Xóa thành công.");
            }
            else
            {
                MessageBox.Show("Xóa thất bại. Vui lòng kiểm tra lại thông tin.");
            }
            selected_sinhvien = null; // Reset sau khi xóa
            Refesh_SinhVien();
        }

        private void BTN_CAPNHAT_Click(object sender, EventArgs e)
        {
            if (selected_sinhvien == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để cập nhật.");
                return;
            }
            if (dao.Update(selected_sinhvien))
            {
                MessageBox.Show("Cập nhật thành công.");
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra lại thông tin.");
            }
            selected_sinhvien = null;
            Refesh_SinhVien();

        }

        private void BTN_THÊM_Click(object sender, EventArgs e)
        {

        }
        private void Refesh_SinhVien()
        {

            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                dataGridView1.DataSource = data.Select(x => (Model.SinhVien)x).ToList();
            }
            else
            {
            }

        }
    }
}
