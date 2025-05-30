using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using QLDeAn.Model;
using QLDeAn.VIEW_NHANVIEN;


namespace QLDeAn
{
    public partial class LoginUI : Form
    {
        public LoginUI()
        {
            
            InitializeComponent();
        }
        public static OracleConnection con;
        public static String userUser;
        public static String passUser;
        public static String roleUser;
        private void LoginUI_Load(object sender, EventArgs e) 
        {
            
        }
        private void title_Click(object sender, EventArgs e) { }
        private void label_user_Click(object sender, EventArgs e) { }
        private void username_TextChanged(object sender, EventArgs e) { }
        private void label_password_Click(object sender, EventArgs e) { }
        private void password_TextChanged(object sender, EventArgs e) { }
        private void role_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label_role_Click(object sender, EventArgs e) { }
        private void login_Click(object sender, EventArgs e)
        {
            if (username.Text.Length == 0)
            {
                MessageBox.Show("TÊN ĐĂNG NHẬP không được để trống.");
                return;
            }
            if (password.Text.Length == 0)
            {
                MessageBox.Show("MẬT KHẨU không được để trống.");
                return;
            }
            if (role.Text.Length == 0)
            {
                MessageBox.Show("VAI TRÒ không được để trống.");
                return;
            }

            //Check username, password and role
            try
            {
                string connectionString = "";
                if (role.Text == "SYSDBA")
                    connectionString = @"DATA SOURCE = localhost:1522/XE;DBA Privilege=SYSDBA; USER ID=" + username.Text + ";PASSWORD=" + password.Text;
                else
                    connectionString = @"DATA SOURCE = localhost:1522/QLDULIEUNOIBO; USER ID=" + username.Text + ";PASSWORD=" + password.Text;

                con = new OracleConnection();
                con.ConnectionString = connectionString;
                con.Open();

                userUser = username.Text;
                passUser = password.Text;

                if (role.Text == "SYSDBA" || role.Text == "ADMIN")
                {
                    MessageBox.Show("Connect với Oracle thành công");
                    DBAUI dba = new DBAUI();
                    dba.Show();
                }
                else if (role.Text == "NHÂN VIÊN")
                {
                    // dùng trong cdb nếu muốn bỏ cái C##
                    //OracleCommand command = new OracleCommand("alter session set \"_ORACLE_SCRIPT\"=true", con);
                    //command.ExecuteNonQuery();
                    string sqlRole = "SELECT VAITRO FROM QLDL.VIEW_NHANVIEN_NVCB WHERE MANV = :manv";
                   
                    OracleCommand command_role = new OracleCommand(sqlRole, con);
                    command_role.Parameters.Add(new OracleParameter("manv", username.Text));
                    OracleDataReader dr = command_role.ExecuteReader();


                    if (dr.Read())
                    {
                        roleUser = dr.GetString(0); 
                    }

                    //this.Hide();

                    MessageBox.Show("Connect với Oracle thành công");
                    NhanVienUI NVUI = new NhanVienUI();
                    switch (roleUser)
                    {
                        case "NVCB":
                            NVUI.Text = "NHÂN VIÊN";
                            break;
                        case "GV":
                            NVUI.Text = "Giáo viên";
                            break;
                        case "NV PĐT":
                            NVUI.Text = "Phòng Đào Tạo";
                            break;
                        case "NV PKT":
                            NVUI.Text = "Phòng Khảo Thí";
                            break;
                        case "NV TCHC":
                            NVUI.Text = "Tổ chức hành chính";
                            break;
                        case "NV CTSV":
                            NVUI.Text = "Cộng tác sinh viên";
                            break;
                        case "TRGDV":
                            NVUI.Text = "Trường đơn vị";
                            break;
                    }

                    NVUI.Show();
                    dr.Close();
                }


                this.Hide();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
