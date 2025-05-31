using QLDeAn.DataAccess.DonVi;
using QLDeAn.DataAccess.NhanVien;
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
    public partial class NHANVIENUI_THEMNHANVIEN: Form
    {
        public NHANVIENUI_THEMNHANVIEN()
        {
            InitializeComponent();
        }
        
        private NhanVienNVTCHCDao dao = new NhanVienNVTCHCDao(LoginUI.con);
        private void BTN_THEM_Click(object sender, EventArgs e)
        {
            if (TB_DT.Text == "" || TB_HOTEN.Text == "" )
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên!");
                return;
            }
            if (CBB_COSO.SelectedItem == null || CBB_VAITRO.SelectedItem == null || CBB_PHAI.SelectedItem == null || CBB_DONVI.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên!");
                return;
            }
            if (!DateTime.TryParse(TB_NGAYSINH.Text, out DateTime ngSinh))
            {
                MessageBox.Show("Ngày sinh không hợp lệ! Vui lòng nhập đúng định dạng dd/MM/yyyy.");
                return;
            }
            NhanVien nv_temp = new NhanVien
            {
                hoTen = TB_HOTEN.Text,
                phai = CBB_PHAI.SelectedItem.ToString(),
                ngSinh = ngSinh,
                luong = string.IsNullOrEmpty(TB_LUONG.Text) ? (int?)null : int.Parse(TB_LUONG.Text),
                phuCap = string.IsNullOrEmpty(TB_PHUCAP.Text) ? (int?)null : int.Parse(TB_PHUCAP.Text),
                dt = TB_DT.Text,
                vaiTro = CBB_VAITRO.SelectedItem.ToString(),
                maDV = CBB_DONVI.SelectedItem.ToString(),
                coso = CBB_COSO.SelectedItem.ToString()
            };
            if (dao.Add(nv_temp))
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                this.Close(); // Đóng form sau khi thêm thành công
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại! Vui lòng kiểm tra lại thông tin.");
            }
        }

        private void BTN_huy_Click(object sender, EventArgs e)
        {
            // Xác nhận hủy thêm nhân viên
            this.Close();
        }
    }
}
