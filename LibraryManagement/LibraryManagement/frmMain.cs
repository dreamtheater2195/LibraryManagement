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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            mnuTaiKhoan.Text = GlobalVariables.TenDayDu;
            LoadPhieuMuon();
            base.OnLoad(e);
        }

        private void LoadPhieuMuon()
        {
            var dtgv = dtgvPhieuMuon;
            dtgv.Tag = 0;
            clsMuonTraSach cls = new clsMuonTraSach();
            dtgv.DataSource = cls.Select_MainForm();
            dtgv.Columns["ID_MuonSach"].Visible = false;
            dtgv.Columns["ID_DocGia"].Visible = false;
            dtgv.Rows[dtgv.Rows.Count - 1].Selected = true;
            dtgv.Tag = 1;
        }

        private void dtgvPhieuMuon_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvPhieuMuon.Tag + "" != "1" || e.RowIndex == dtgvPhieuMuon.Rows.Count - 1)
            {
                dtgvChiTiet.Rows.Clear();
                return;
            }
            dtgvChiTiet.Rows.Clear();
            clsMuonTraSach_ChiTiet cls = new clsMuonTraSach_ChiTiet();
            cls.ID_MuonSach = int.Parse(dtgvPhieuMuon.Rows[e.RowIndex].Cells["ID_MuonSach"].Value.ToString());
            DataTable dt = cls.Select_wID_MuonSach();

            foreach (DataRow drCT in dt.Rows)
            {
                DataGridViewRow row = (DataGridViewRow)dtgvChiTiet.Rows[0].Clone();
                row.Cells[0].Value = drCT["Ten_Sach"];
                row.Cells[1].Value = drCT["SoLuong"];
                dtgvChiTiet.Rows.Add(row);
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            frmPhieuMuon frm = new frmPhieuMuon(0);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            LoadPhieuMuon();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgvPhieuMuon.CurrentRow.Index == dtgvPhieuMuon.Rows.Count - 1)
            {
                return;
            }
            frmPhieuMuon frm = new frmPhieuMuon(int.Parse(dtgvPhieuMuon.Rows[dtgvPhieuMuon.CurrentRow.Index].Cells["ID_MuonSach"].Value.ToString()));
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            LoadPhieuMuon();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgvPhieuMuon.CurrentRow.Index == dtgvPhieuMuon.Rows.Count - 1) return;
            clsMuonTraSach cls = new clsMuonTraSach();
            cls.ID_MuonSach = int.Parse(dtgvPhieuMuon.Rows[dtgvPhieuMuon.CurrentRow.Index].Cells["ID_MuonSach"].Value.ToString());
            cls.SelectOne();
            cls.TonTai = false;
            cls.Update();

            clsDM_Sach clsSach = new clsDM_Sach();
            clsSach.UpdateSoLuongConLai();
            LoadPhieuMuon();
        }

        private void quảnLýNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLyNguoiDung frm = new frmQuanLyNguoiDung();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }
    }
}
