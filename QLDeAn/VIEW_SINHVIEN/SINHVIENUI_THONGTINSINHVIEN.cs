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
using QLDeAn.VIEW_NHANVIEN;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace QLDeAn.VIEW_SINHVIEN
{
    public partial class SINHVIENUI_THONGTINSINHVIEN: UserControl
    {
        public SINHVIENUI_THONGTINSINHVIEN()
        {
            InitializeComponent();
        }


        private static ISinhVienDao dao = null;
        public  SinhVien current_sinhvien = null;

        public void Load_SinhVien()
        {
            dao = new SinhVienSVDao(LoginUI.con);

            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                current_sinhvien = (SinhVien)data[0];
            }
            else
            {
                MessageBox.Show("Không có dữ liệu sinh viên.");
                return;
            }



            TB_COSO.Text =current_sinhvien.coso ?? "";
            TB_HOTEN.Text = current_sinhvien.hoTen ?? "";
            TB_PHAI.Text = current_sinhvien.phai ?? "";
            TB_DIACHI.Text = current_sinhvien.dChi ?? "";
            TB_KHOA.Text = current_sinhvien.khoa ?? "";
            TB_NGAYSINH.Text = current_sinhvien.ngSinh?.ToString("dd/MM/yyyy") ?? "";
            TB_MASINHVIEN.Text = current_sinhvien.maSV ?? "";
            TB_TINHTRANG.Text = current_sinhvien.TINHTRANG ?? "";
            TB_SDT.Text = current_sinhvien.dt ?? "";

        }
      
   
        private void Refesh_SinhVien()
        {

            dao = new SinhVienSVDao(LoginUI.con);

            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                current_sinhvien = (SinhVien)data[0];
            }
            else
            {
                MessageBox.Show("Không có dữ liệu sinh viên.");
                return;
            }
            TB_COSO.Text = current_sinhvien.coso ?? "";
            TB_HOTEN.Text = current_sinhvien.hoTen ?? "";
            TB_PHAI.Text = current_sinhvien.phai ?? "";
            TB_DIACHI.Text = current_sinhvien.dChi ?? "";
            TB_KHOA.Text = current_sinhvien.khoa ?? "";
            TB_NGAYSINH.Text = current_sinhvien.ngSinh?.ToString("dd/MM/yyyy") ?? "";
            TB_MASINHVIEN.Text = current_sinhvien.maSV ?? "";
            TB_TINHTRANG.Text = current_sinhvien.TINHTRANG ?? "";
            TB_SDT.Text = current_sinhvien.dt ?? "";

        }

        private void GB_THÔNGTINCANHAN_GV_Enter(object sender, EventArgs e)
        {

        }

        private void BTN_CAPNHAT_Click(object sender, EventArgs e)
        {
            current_sinhvien.dChi = TB_DIACHI.Text.Trim();
            current_sinhvien.dt = TB_SDT.Text.Trim();
            if (dao.Update(current_sinhvien))
            {
                MessageBox.Show("Cập nhật thành công.");
                Refesh_SinhVien();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra lại thông tin.");

            }
        }
    }
}
