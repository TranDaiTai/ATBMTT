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

        private void GB_MONHOCDUOCCHON_Enter(object sender, EventArgs e)
        {

        }
        public void Load_MoMon()
        {
            IMoMonDao dao = null;

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
                    TB_MAMONHOC.ReadOnly = false;
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
            TB_MAHOCPHAN.Text = row.Cells["MAHP"].Value?.ToString() ?? "";
            TB_HOCKI.Text = row.Cells["HK"].Value?.ToString() ?? "";
            TB_MAMONHOC.Text = row.Cells["maMM"].Value?.ToString() ?? "";
            TB_NAM.Text = row.Cells["nam"].Value?.ToString() ?? "";
            TB_MAGIAOVIEN.Text = row.Cells["maGV"].Value?.ToString() ?? "";
            
        }

        private void TB_MANHANVIEN_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
