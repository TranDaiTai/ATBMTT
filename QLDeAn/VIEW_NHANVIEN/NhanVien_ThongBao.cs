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
using QLDeAn.DataAccess.ThongBao;

namespace QLDeAn.VIEW_NHANVIEN
{
    public partial class NhanVien_ThongBao: UserControl
    {
        public NhanVien_ThongBao()
        {
            InitializeComponent();
            
          
        }

        private void groupMONHOC_Enter(object sender, EventArgs e)
        {

        }
        private static IThongBaoDao dao = null;
        private static ThongBao selected_ThongBao = null;
      
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua nếu click header

            var row = dataGridView1.Rows[e.RowIndex];
            selected_ThongBao = (ThongBao)row.DataBoundItem;

            TB_DIADIEM.Text = row.Cells["diadiem"].Value?.ToString() ?? "";
            TB_MATHONGBAO.Text = row.Cells["ID_ThongBao"].Value?.ToString() ?? "";
            TB_THOIGIAN.Text = row.Cells["thoigian"].Value?.ToString() ?? "";
            TB_noidung.Text = row.Cells["noidung"].Value?.ToString() ?? "";


        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (selected_ThongBao == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.");
                return;
            }
            if (dao.Delete(selected_ThongBao))
            {
                MessageBox.Show("Xóa thành công.");
            }
            else
            {
                MessageBox.Show("Xóa thất bại. Vui lòng kiểm tra lại thông tin.");
            }
            selected_ThongBao = null; // Reset sau khi xóa
            Refesh_Dangkyhocphan();
        }

        private void BTN_CAPNHAT_Click(object sender, EventArgs e)
        {
            if (selected_ThongBao == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để cập nhật.");
                return;
            }
            if (dao.Update(selected_ThongBao))
            {
                MessageBox.Show("Cập nhật thành công.");
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra lại thông tin.");
            }
            selected_ThongBao = null;
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

        public void Load_Thongbao()
        {
            dao = new ThongBaoXAdminDao(LoginUI.con);

            //if (NhanVienUI.roleUser == "GV")
            //{
            //    dao = new DangKyGVDao(LoginUI.con);
            //}
            //else if (NhanVienUI.roleUser == "NV PĐT")
            //{
            //    dao = new DangKyNVPDTDao(LoginUI.con);
            //}
            //else if (NhanVienUI.roleUser == "NV PKT")
            //{
            //    dao = new DangKyNVPKTDao(LoginUI.con);
            //}
            //else
            //{
            //    return;
            //}

            List<object> data = dao.Load(null);


            if (data.Count > 0)
            {
                dataGridView1.DataSource = data.Select(x => (Model.ThongBao)x).ToList();
            }
            else
            {
            }
        }

        private void TB_MATHONGBAO_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
