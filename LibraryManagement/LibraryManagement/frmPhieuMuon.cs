using QuanLyThuVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagement
{
    public partial class frmPhieuMuon : Form
    {
        private int m_iID_MuonSach;
        public frmPhieuMuon(int ID_MuonSach)
        {
            InitializeComponent();
            m_iID_MuonSach = ID_MuonSach;
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadForm();
            base.OnLoad(e);
        }

        private void LoadCmbDocGia()
        {
            var cmb = cbDocGia;
            clsDM_DocGia cls = new clsDM_DocGia();
            DataTable dt = cls.SelectAll();
            dt.DefaultView.RowFilter = "TonTai = 1";
            dt = dt.DefaultView.ToTable();
            cmb.DataSource = dt;
            cmb.ValueMember = "ID_DocGia";
            cmb.DisplayMember = "Ten_DocGia";
        }

        private void LoadCmbSach()
        {
            var cmb = cbSach;
            clsDM_Sach cls = new clsDM_Sach();
            DataTable dt = cls.SelectAll();
            dt.DefaultView.RowFilter = "TonTai = 1";
            dt = dt.DefaultView.ToTable();
            cmb.DataSource = dt;
            cmb.ValueMember = "ID_Sach";
            cmb.DisplayMember = "Ten_Sach";
        }


        private void LoadForm()
        {
            LoadCmbDocGia();
            LoadCmbSach();
            dgvChiTiet.Rows.Clear();
            if (m_iID_MuonSach > 0)
            {
                clsMuonTraSach cls = new clsMuonTraSach();
                cls.ID_MuonSach = m_iID_MuonSach;
                DataRow dr = cls.SelectOne().Rows[0];
                cbDocGia.SelectedValue = dr["ID_DocGia"];
                dtpNgayMuon.Value = dr.Field<DateTime>("NgayMuon");
                dtpNgayTra.Value = dr.Field<DateTime>("NgayTra");

                if (dr.IsNull("NgayTra_ThucTe")) {
                    dtpNgayTra_ThucTe.Checked = false;
                }
                else {
                    dtpNgayTra_ThucTe.Checked = true;
                    dtpNgayTra_ThucTe.Value = dr.Field<DateTime>("NgayTra_ThucTe");
                }

                clsMuonTraSach_ChiTiet clsCT = new clsMuonTraSach_ChiTiet();
                clsCT.ID_MuonSach = m_iID_MuonSach;
                DataTable dtCT = clsCT.Select_wID_MuonSach();

                foreach (DataRow drCT in dtCT.Rows)
                {
                    DataGridViewRow row = (DataGridViewRow)dgvChiTiet.Rows[0].Clone();
                    row.Cells[0].Value = drCT["ID_Sach"];
                    row.Cells[1].Value = drCT["Ten_Sach"];
                    row.Cells[2].Value = drCT["SoLuong"];
                    dgvChiTiet.Rows.Add(row);
                }
            }
        }

        private bool IsValid()
        {
            if (nmSoLuong.Value == 0)
            {
                MessageBox.Show("Chưa nhập số lượng. ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            for (int i = 0; i < dgvChiTiet.Rows.Count - 1; i++)
            {
                if (int.Parse(cbSach.SelectedValue.ToString()) == int.Parse(dgvChiTiet.Rows[i].Cells[0].Value.ToString()))
                {
                    MessageBox.Show("Sách đã có trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            clsDM_Sach cls = new clsDM_Sach();
            cls.ID_Sach = int.Parse(cbSach.SelectedValue.ToString());
            cls.SelectOne();

            if (cls.SoLuongConLai.Value < nmSoLuong.Value)
            {
                MessageBox.Show("Số sách còn lại không đủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nmSoLuong.Value = cls.SoLuongConLai.Value;
                return false;
            }

            return true;
        }
        private void btnThemSach_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;
            DataGridViewRow row = (DataGridViewRow)dgvChiTiet.Rows[0].Clone();
            row.Cells[0].Value = cbSach.SelectedValue;
            row.Cells[1].Value = cbSach.Text;
            row.Cells[2].Value = nmSoLuong.Value;
            dgvChiTiet.Rows.Add(row);
            dgvChiTiet.Rows[row.Index].Selected = true;
        }

        private void dgvChiTiet_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == dgvChiTiet.Rows.Count - 1)
            {
                nmSoLuong.Value = 0;
                return;
            }
            cbSach.SelectedValue = dgvChiTiet.Rows[e.RowIndex].Cells[0].Value;
            nmSoLuong.Value = decimal.Parse(dgvChiTiet.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow.Index == dgvChiTiet.Rows.Count - 1) return;
            if (!IsValid()) return;
            dgvChiTiet.Rows[dgvChiTiet.CurrentRow.Index].Cells[0].Value = cbSach.SelectedValue;
            dgvChiTiet.Rows[dgvChiTiet.CurrentRow.Index].Cells[1].Value = cbSach.Text;
            dgvChiTiet.Rows[dgvChiTiet.CurrentRow.Index].Cells[2].Value = nmSoLuong.Value;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow.Index == dgvChiTiet.Rows.Count - 1) return;
            dgvChiTiet.Rows.RemoveAt(dgvChiTiet.CurrentRow.Index);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cbDocGia.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn độc giả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            clsMuonTraSach cls = new clsMuonTraSach();
            cls.ID_MuonSach = m_iID_MuonSach;
            cls.SelectOne();

            cls.ID_DocGia = int.Parse(cbDocGia.SelectedValue.ToString());
            cls.NgayMuon = dtpNgayMuon.Value;
            cls.NgayTra = dtpNgayTra.Value;
            cls.NgayTra_ThucTe = dtpNgayTra_ThucTe.Checked ? dtpNgayTra_ThucTe.Value : SqlDateTime.Null;
            cls.TonTai = true;
            if (m_iID_MuonSach == 0)
                cls.Insert();
            else
                cls.Update();

            clsMuonTraSach_ChiTiet clsCT = new clsMuonTraSach_ChiTiet();
            clsCT.ID_MuonSach = cls.ID_MuonSach;
            clsCT.DeleteWID_MuonSachLogic();

            for (int i = 0; i < dgvChiTiet.Rows.Count - 1; i++)
            {
                clsCT.ID_Sach = int.Parse(dgvChiTiet.Rows[i].Cells[0].Value.ToString());
                clsCT.SoLuong = int.Parse(dgvChiTiet.Rows[i].Cells[2].Value.ToString());
                clsCT.Insert();
            }

            clsDM_Sach clsSach = new clsDM_Sach();
            clsSach.UpdateSoLuongConLai();

            MessageBox.Show("Lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            if (!dtp.Checked)
            {
                dtp.CustomFormat = " ";
                dtp.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dtp.CustomFormat = "dd/MM/yyyy";
                dtp.Format = DateTimePickerFormat.Custom;
            }
        }
    }
}
