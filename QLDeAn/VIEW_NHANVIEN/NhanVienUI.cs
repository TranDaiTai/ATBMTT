using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using QLDeAn.Model;
using QLDeAn.DataAccess;
using QLDeAn.DataAccess.NhanVien;
namespace QLDeAn.VIEW_NHANVIEN
{
    public partial class NhanVienUI: Form
    {
        public NhanVienUI()
        {
            InitializeComponent();
            SetTabsByRole();
            NHANVIENUI_LOAD();
        }
        public static String roleUser;
        public static OracleConnection conNow;
        private bool isLogout = false;
        private static NhanVien s_nv; // Biến tĩnh để lưu thông tin nhân viên hiện tại

        private void NHANVIENUI_LOAD()
        {
            conNow = LoginUI.con;

            // Gọi DAO
            var dao = new NhanVienNVCBDao(conNow);
            List<object> data = dao.Load(null);

            if (data.Count > 0)
            {
                s_nv = (NhanVien)data[0]; // Lưu nhân viên hiện tại vào biến tĩnh
                Xinchao.Text = "XIN CHÀO " + s_nv.hoTen.ToUpper() + "!";
                TB_COSO.Text = s_nv.coso;
                TB_DT.Text = s_nv.dt;
                TB_HOTEN.Text = s_nv.hoTen;
                TB_MADV.Text = s_nv.maDV;
                TB_LUONG.Text = s_nv.luong.HasValue ? s_nv.luong.Value.ToString() : "Chưa cập nhật";
                TB_MANHANVIEN.Text = s_nv.maNV;
                TB_NGAYSINH.Text = s_nv.ngSinh.HasValue ? s_nv.ngSinh.Value.ToString("dd/MM/yyyy") : "Chưa cập nhật";
                TB_PHAI.Text = s_nv.phai;
                TB_PHUCAP.Text = s_nv.phuCap.HasValue ? s_nv.phuCap.Value.ToString() : "Chưa cập nhật";
            }
            else
            {
                Xinchao.Text = "KHÔNG THỂ LẤY THÔNG TIN NHÂN VIÊN!";
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GB_THÔNGTINCANHAN_GV_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void SetTabsByRole()
        {
            // Đầu tiên hiện tất cả tab (nếu cần)
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tabControl1.TabPages.Remove(tab); // Xóa hết tab để add lại theo quyền
            }

            tabControl1.TabPages.Add(TP_THONGTINNHANVIEN);
            string role = LoginUI.roleUser;
            Console.WriteLine(role);
            // Tùy role mà add lại các tab được phép
            if (role == "GV")
            {
                tabControl1.TabPages.Add(TP_QUANLYMONHOC);
                tabControl1.TabPages.Add(TP_QUANLYSINHVIEN);
                tabControl1.TabPages.Add(TP_DANGKYHP);

            }
            else if (role == "TRGDV")
            {
                tabControl1.TabPages.Add(TP_QUANLYNHANVIEN);
                tabControl1.TabPages.Add(TP_QUANLYMONHOC);


            }
            else if (role == "NV PĐT")
            {
                tabControl1.TabPages.Add(TP_QUANLYMONHOC);
                tabControl1.TabPages.Add(TP_QUANLYSINHVIEN);
                tabControl1.TabPages.Add(TP_DANGKYHP);

            }
            else if (role == "NV PKT")
            {
                tabControl1.TabPages.Add(TP_DANGKYHP);

            }
            else if (role == "NV TCHC")
            {
                tabControl1.TabPages.Add(TP_QUANLYNHANVIEN);
            }
            else if (role == "NV CTSV")
            {
                tabControl1.TabPages.Add(TP_QUANLYSINHVIEN);
            }
            tabControl1.TabPages.Add(TP_THONGBAO);

        }

        private void NhanVienUI_FormClosing(object sender, FormClosingEventArgs e)
        {
         


        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {
            isLogout = true; // Đánh dấu đã đăng xuất để không thực hiện lại khi form đóng
            try
            {
                LoginUI.con.Dispose();
                LoginUI.con.Close();
                OracleConnection.ClearPool(conNow);


                MessageBox.Show("Đóng kết nối và đăng xuất thành công");

                LoginUI login = new LoginUI();
                login.Show();
                this.Close();

            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void Xinchao_Click(object sender, EventArgs e)
        {

        }

        private void NhanVienUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isLogout)
            {
                isLogout = false; // Đặt lại trạng thái đăng xuất
                return; // Nếu đã đăng xuất thì không cần làm gì thêm
            }
            LoginUI.con.Dispose();
            LoginUI.con.Close();
            OracleConnection.ClearPool(conNow);
            Application.Exit(); // Đóng toàn bộ ứng dụng nếu không đăng xuất
        }

        private void BTN_CHINHSUATT_Click(object sender, EventArgs e)
        {
            NhanVienUI_ChinhsuaTT chinhsuaUI = new NhanVienUI_ChinhsuaTT();
            chinhsuaUI.ShowDialog();
        }

        private void nhanvieN_DANGKYHOCPHAN1_Load(object sender, EventArgs e)
        {

        }
    }
}
