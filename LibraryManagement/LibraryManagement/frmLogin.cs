using QuanLyThuVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagement
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text.Trim() == "") {
                MessageBox.Show("Chưa nhập tên đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsTaiKhoanDangNhap cls = new clsTaiKhoanDangNhap();
            cls.TenDangNhap = txtTenDangNhap.Text.Trim();
            cls.MatKhau = GlobalMethods.MD5Hash(txtMatKhau.Text.Trim());
            DataTable dt = cls.CheckLogin();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("Đăng nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GlobalVariables.ID_TaiKhoanDangNhap = dt.Rows[0].Field<int>("ID_TaiKhoanDangNhap");
                GlobalVariables.TenDangNhap = dt.Rows[0].Field<string>("TenDangNhap");
                GlobalVariables.TenDayDu = dt.Rows[0].Field<string>("TenDayDu");

                frmMain frm = new frmMain();
                frm.StartPosition = FormStartPosition.CenterScreen;
                this.Hide();
                frm.ShowDialog();
                this.Show();
            } 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
