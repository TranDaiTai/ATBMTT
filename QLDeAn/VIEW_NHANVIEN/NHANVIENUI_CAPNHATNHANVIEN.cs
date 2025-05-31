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
    public partial class NHANVIENUI_CAPNHATNHANVIEN: Form
    {
        public NHANVIENUI_CAPNHATNHANVIEN()
        {
            InitializeComponent();
        }
        private NhanVienNVTCHCDao dao = new NhanVienNVTCHCDao(LoginUI.con);
        private void BTN_THEM_Click(object sender, EventArgs e)
        {
           

            if (TB_DT.Text == "" || TB_HOTEN.Text == "")
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
          
            NhanVien nv = new NhanVien
            {
                maNV = TB_MANHANVIEN.Text,
                hoTen = TB_HOTEN.Text,
                phuCap = int.TryParse(TB_PHUCAP.Text, out int phuCap) ? phuCap : 0,
                luong = int.TryParse(TB_LUONG.Text, out int luong) ? luong : 0,
                ngSinh = ngSinh,
                dt = TB_DT.Text,
                vaiTro = CBB_VAITRO.SelectedItem.ToString(),
                maDV = CBB_DONVI.SelectedItem.ToString(),
                phai = CBB_PHAI.SelectedItem.ToString(),
                coso = CBB_COSO.SelectedItem.ToString()
            };

            if (dao.Update(nv))
            {
                MessageBox.Show("câp nhật nhân viên thành công!");
                this.Close(); // Đóng form sau khi câp nhật thành công
            }
            else
            {
                MessageBox.Show("câp nhật nhân viên thất bại! Vui lòng kiểm tra lại thông tin.");
            }
        }

        private void BTN_huy_Click(object sender, EventArgs e)
        {
            // Xác nhận hủy thêm nhân viên
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void NHANVIENUI_CAPNHATNHANVIEN_Load(object sender, EventArgs e)
        {
            TB_HOTEN.Text = ((NhanVien)this.Tag).hoTen.ToString();
            TB_LUONG.Text = ((NhanVien)this.Tag).luong.ToString();
            TB_NGAYSINH.Text = ((NhanVien)this.Tag).ngSinh.ToString();
            TB_PHUCAP.Text = ((NhanVien)this.Tag).phuCap.ToString();
            TB_DT.Text = ((NhanVien)this.Tag).dt.ToString();
            TB_MANHANVIEN.Text = ((NhanVien)this.Tag).maNV.ToString();

            CBB_COSO.SelectedItem = ((NhanVien)this.Tag).coso.ToString();
            CBB_DONVI.SelectedItem = ((NhanVien)this.Tag).maDV.ToString();
            CBB_PHAI.SelectedItem = ((NhanVien)this.Tag).phai.ToString();
            CBB_VAITRO.SelectedItem = ((NhanVien)this.Tag).vaiTro.ToString();
        }
    }
}
