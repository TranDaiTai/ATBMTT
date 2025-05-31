using QLDeAn.DataAccess.DangKy;
using QLDeAn.DataAccess.SinhVien;
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
    public partial class NHANVIENUI_THEMDANGKY: Form
    {
        public NHANVIENUI_THEMDANGKY()
        {
            InitializeComponent();
        }

        private static ISinhVienDao dao = null;
        private static SinhVien selected_sinhvien = null;
        public void Load_SinhVien()
        {
            dao = new SinhVienNVPDTDao(LoginUI.con);

           
            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                dataGridView1.DataSource = data.Select(x => (Model.SinhVien)x).ToList();
            }
            else
            {
            }

        }

        private void BTN_dangKy_Click_1(object sender, EventArgs e)
        {
            if (selected_sinhvien == null)
            {
                MessageBox.Show("Vui lòng chọn Sinh viên để đăng ký.");
                return;
            }
            DangKy dk = new DangKy()
            {
                diemCK = null,
                diemQT = null,
                diemTH = null,
                diemTK = null,
                maMM = ((MoMon)this.Tag).MAMM,
                maSV = selected_sinhvien.maSV,
            };
            var dao_dk = new DangKyNVPDTDao(LoginUI.con);
            if (dao_dk.Add(dk))
            {
                MessageBox.Show("Đăng ký thành công.");
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại. Vui lòng kiểm tra lại thông tin.");
            }
            selected_sinhvien = null; // Reset sau khi xóa
        }

        private void NHANVIENUI_THEMDANGKY_Load(object sender, EventArgs e)
        {
            Load_SinhVien();
        }

        private void BTN_HUy_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua nếu click header

            var row = dataGridView1.Rows[e.RowIndex];
            selected_sinhvien = (SinhVien)row.DataBoundItem;

            CBB_COSO.Text = row.Cells["coso"].Value?.ToString() ?? "";
            TB_HOTEN.Text = row.Cells["hoTen"].Value?.ToString() ?? "";
            CBB_PHAI.Text = row.Cells["phai"].Value?.ToString() ?? "";
            TB_DIACHI.Text = row.Cells["dChi"].Value?.ToString() ?? "";
            CBB_DONVI.Text = row.Cells["khoa"].Value?.ToString() ?? "";
            TB_NGAYSINH.Text = row.Cells["ngSinh"].Value is DateTime date ? date.ToString("dd/MMM/yyyy") : "";
            TB_MASINHVIEN.Text = row.Cells["maSV"].Value?.ToString() ?? "";
            TB_TINHTRANG.Text = row.Cells["tinhTrang"].Value?.ToString() ?? "";
            TB_SDT.Text = row.Cells["dt"].Value?.ToString() ?? "";
        }
    }
}
