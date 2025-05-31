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
using Oracle.ManagedDataAccess.Client;
using QLDeAn.DataAccess.NhanVien;
using QLDeAn.DataAccess.SinhVien;
using QLDeAn.DataAccess.DangKy;


namespace QLDeAn.VIEW_SINHVIEN
{
    public partial class SINHVIENUI: Form
    {
        public SINHVIENUI()
        {
            InitializeComponent();
        }
        private bool isLogout = false;
        private OracleConnection conNow = LoginUI.con; // Kết nối hiện tại từ LoginUI
        private static SinhVien s_sv; // Biến tĩnh để lưu thông tin nhân viên hiện tại
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == TP_Thongtinsv)
            {
                // Khi tab Thông tin cá nhân được chọn, gọi hàm Load_SinhVien
                sinhvienuI_THONGTINSINHVIEN1.Load_SinhVien();
            }
            else if (tabControl1.SelectedTab == TP_THONGBAO)
            {
                sinhvienuI_THONGBAO2.Load_Thongbao(); 

            }
            else if (tabControl1.SelectedTab == TP_MOMON)
            {
                sinhvienuI_MOMON2.Load_DangKyhocPhan();
            }
            else if (tabControl1.SelectedTab == TP_LICHSUHOCPHAN)
            {
                sinhvienuI_LICHSUDANGKY1.Load_DangKyhocPhan();
            }
            else if (tabControl1.SelectedTab == TP_DANGKYMON)
            {
                sinhvienuI_DANGKYMON1.Load_DangKyhocPhan();
            }
           
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

        private void SINHVIENUI_FormClosing(object sender, FormClosingEventArgs e)
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

        private void TP_Thongtinsv_Click(object sender, EventArgs e)
        {

        }

        private void TP_MOMON_Click(object sender, EventArgs e)
        {

        }

        private void sinhvienuI_MOMON2_Click(object sender, EventArgs e)
        {

        }

        private void SINHVIENUI_Load(object sender, EventArgs e)
        {
            sinhvienuI_MOMON2.Load_DangKyhocPhan();
            sinhvienuI_THONGTINSINHVIEN1.Load_SinhVien();
            
            //var dao = new SinhVienSVDao(LoginUI.con);

            //List<object> data = dao.Load(null);

            //if (data.Count > 0)
            //{
            //    s_sv = (SinhVien)data[0]; // Lưu nhân viên hiện tại vào biến tĩnh
            Xinchao.Text = "XIN CHÀO " + sinhvienuI_THONGTINSINHVIEN1.current_sinhvien.hoTen.ToUpper() + "!";
            //}
            //else
            //{
            //    Xinchao.Text = "KHÔNG THỂ LẤY THÔNG TIN NHÂN VIÊN!";
            //}
        }
    }
}
