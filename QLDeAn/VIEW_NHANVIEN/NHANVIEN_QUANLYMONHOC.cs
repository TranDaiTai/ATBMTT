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
using QLDeAn.DataAccess.MoMon;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using QLDeAn.Model;

namespace QLDeAn.VIEW_NHANVIEN
{
    public partial class NHANVIEN_QUANLYMONHOC: UserControl
    {
        public NHANVIEN_QUANLYMONHOC()
        {
            InitializeComponent();
            //Load_MoMon();
            SetButtonsByRole();
        }
        private static IMoMonDao dao = null;
        private static MoMon selected_momon = null;

        private void GB_MONHOCDUOCCHON_Enter(object sender, EventArgs e)
        {

        }
        public void Load_MoMon()
        {

            if (NhanVienUI.roleUser == "NV PĐT")
            {
                dao = new MoMonNVPDTDao(LoginUI.con);
            }
            else if (NhanVienUI.roleUser == "TRGDV")
            {
                dao = new MoMonTRGDVDao(LoginUI.con);
            }
            else if (NhanVienUI.roleUser == "GV")
            {
                dao = new MoMonGVDao(LoginUI.con);
            }
            else
            {
                return;
            }
            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                dataGridView1.DataSource = data.Select(x => (Model.MoMon)x).ToList();
            }
            else
            {
                //MessageBox.Show("Không có dữ liệu nhân viên.");
            }

        }
        public void Refesh_MoMon()
        {

           
            List<object> data = dao.Load(null);
            if (data.Count > 0)
            {
                dataGridView1.DataSource = data.Select(x => (Model.MoMon)x).ToList();
            }
            else
            {
                //MessageBox.Show("Không có dữ liệu nhân viên.");
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

                    // Hiển thị các trường thông tin
                    TB_HOCKI.ReadOnly = false;
                    TB_NAM.ReadOnly = false;
                    TB_MAGIAOVIEN.ReadOnly = false;
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

            selected_momon = (MoMon)row.DataBoundItem;


            TB_MAHOCPHAN.Text = row.Cells["MAHP"].Value?.ToString() ?? "";
            TB_HOCKI.Text = row.Cells["HK"].Value?.ToString() ?? "";
            TB_MAMONHOC.Text = row.Cells["maMM"].Value?.ToString() ?? "";
            TB_NAM.Text = row.Cells["nam"].Value?.ToString() ?? "";
            TB_MAGIAOVIEN.Text = row.Cells["maGV"].Value?.ToString() ?? "";

            
        }

        private void TB_MANHANVIEN_TextChanged(object sender, EventArgs e)
        {

        }

        private void BTN_CAPNHAT_Click(object sender, EventArgs e)
        {
            if (selected_momon == null)
            {
                MessageBox.Show("Vui lòng chọn một môn học để cập nhật.");
                return;
            }
            MoMon mm = new MoMon
            {
                MAHP = TB_MAHOCPHAN.Text,
                MAMM = TB_MAMONHOC.Text,
                HK = int.Parse(TB_HOCKI.Text),
                NAM = int.Parse(TB_NAM.Text),
                MAGV = TB_MAGIAOVIEN.Text
            };
            try
            {
                if (dao.Update(mm))
                {
                    MessageBox.Show("Cập nhật thành công.");
                    Refesh_MoMon();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra lại thông tin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}");
            }
        }
    }
}
