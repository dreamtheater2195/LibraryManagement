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
    public partial class frmQuanLyNguoiDung : Form
    {
        public frmQuanLyNguoiDung()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            ClearControls();
            LoadAccount();
            base.OnLoad(e);
        }

        private void ClearControls()
        {
            txtTenDayDu.Text = "";
            txtUserName.Text = "";
        }
        private void LoadAccount()
        {
            var dtgv = dtgvAccount;
            dtgv.Tag = 0;
            clsTaiKhoanDangNhap cls = new clsTaiKhoanDangNhap();
            DataTable dt = cls.SelectAll();
            dt.DefaultView.RowFilter = "TonTai = 1";
            dt = dt.DefaultView.ToTable();
            dtgv.Rows.Clear();
            int STT = 1;
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow row = (DataGridViewRow)dtgv.Rows[0].Clone();
                row.Cells[0].Value = dr["ID_TaiKhoanDangNhap"];
                row.Cells[1].Value = STT++;
                row.Cells[2].Value = dr["TenDangNhap"];
                row.Cells[3].Value = dr["TenDayDu"];
                dtgv.Rows.Add(row);
            }
            dtgv.Rows[dtgv.Rows.Count - 1].Selected = true;
            dtgv.Tag = 1;
        }

        private void dtgvAccount_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvAccount.Tag + "" != "1" || e.RowIndex == dtgvAccount.Rows.Count - 1)
            {
                ClearControls();
                return;
            }
            txtUserName.Text = dtgvAccount.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtTenDayDu.Text = dtgvAccount.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private bool IsValid()
        {
            return true;
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;

            clsTaiKhoanDangNhap cls = new clsTaiKhoanDangNhap();
            cls.TenDangNhap = txtUserName.Text.Trim();
            cls.TenDayDu = txtTenDayDu.Text.Trim();
            cls.MatKhau = GlobalMethods.MD5Hash("1");
            cls.TonTai = true;
            cls.Insert();
            MessageBox.Show("Thêm mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAccount();
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            if (dtgvAccount.CurrentRow.Index == dtgvAccount.Rows.Count - 1) return;
            if (!IsValid()) return;

            clsTaiKhoanDangNhap cls = new clsTaiKhoanDangNhap();
            cls.ID_TaiKhoanDangNhap = int.Parse(dtgvAccount.Rows[dtgvAccount.CurrentRow.Index].Cells[0].Value.ToString());
            cls.SelectOne();
            cls.TenDangNhap = txtUserName.Text.Trim();
            cls.TenDayDu = txtTenDayDu.Text.Trim();
            cls.TonTai = true;
            cls.Update();
            MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAccount();
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            if (dtgvAccount.CurrentRow.Index == dtgvAccount.Rows.Count - 1) return;
            clsTaiKhoanDangNhap cls = new clsTaiKhoanDangNhap();
            cls.ID_TaiKhoanDangNhap = int.Parse(dtgvAccount.Rows[dtgvAccount.CurrentRow.Index].Cells[0].Value.ToString());
            cls.SelectOne();
            cls.TonTai = false;
            cls.Update();
            MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAccount();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dtgvAccount.CurrentRow.Index == dtgvAccount.Rows.Count - 1) return;
            clsTaiKhoanDangNhap cls = new clsTaiKhoanDangNhap();
            cls.ID_TaiKhoanDangNhap = int.Parse(dtgvAccount.Rows[dtgvAccount.CurrentRow.Index].Cells[0].Value.ToString());
            cls.SelectOne();
            cls.MatKhau = GlobalMethods.MD5Hash("1");
            cls.Update();
            MessageBox.Show("Reset mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAccount();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgvAccount.Rows.Count - 1; i++)
            {
                string tenDangNhap = dtgvAccount.Rows[i].Cells[2].Value.ToString().ToLower().Trim();
                string tenDayDu = dtgvAccount.Rows[i].Cells[3].Value.ToString().ToLower().Trim();

                if (tenDangNhap.Contains(txtTimKiem.Text.ToLower().Trim()) || tenDayDu.Contains(txtTimKiem.Text.ToLower().Trim()))
                {
                    dtgvAccount.Rows[i].Visible = true;
                }
                else
                {
                    dtgvAccount.Rows[i].Visible = false;
                }
            }
        }
    }
}
