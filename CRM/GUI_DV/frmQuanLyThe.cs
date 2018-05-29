using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRM.DAL.DV;
using CRM.Entities.DV;
using CRM.Utilities.DV;

namespace CRM.GUI_DV
{
    public partial class frmQuanLyThe : Form
    {
        public frmQuanLyThe()
        {
            InitializeComponent();
            cbTieuChi_TheoNgay.SelectedIndex = 0;
            cbTieuChi_ThongTin.SelectedIndex = 0;
        }

        void DSTheChuaNhan()
        {
            try
            {
                DataTable dt = TheDAL.TheChuaNhan(dtpTuNgay.Text, dtpDenNgay.Text);
                FillData(dt);
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }

        void DSTheDaNhanChuaGiao()
        {
            try
            {
                DataTable dt = TheDAL.TheDaNhanChuaGiao(dtpTuNgay.Text, dtpDenNgay.Text);
                FillData(dt);
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }

        void DSTheDaGiao()
        {
            try
            {
                DataTable dt = TheDAL.TheDaGiao(dtpTuNgay.Text, dtpDenNgay.Text);
                FillData(dt);
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }


        void TatCaThe()
        {
            try
            {
                DataTable dt = TheDAL.TatCaThe(dtpTuNgay.Text, dtpDenNgay.Text);
                FillData(dt);
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }

        void DSTheTheoMaKH()
        {
            try
            {
                DataTable dt = TheDAL.TheTheoMaKH(txtThongTin.Text);
                FillData(dt);
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }

        void DSTheTheoCMND()
        {
            try
            {
                DataTable dt = TheDAL.TheTheoCMND(txtThongTin.Text);
                FillData(dt);
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
            }
        }

        void FillData(DataTable dt)
        {
            BindingSource bindS = new BindingSource();
            bindS.DataSource = dt;
            dgvThongTinThe.DataSource = bindS;
            dgvThongTinThe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThongTinThe.Columns[0].Width = 30;
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy thẻ nào!");
            }
        }

        private void dgvThongTinThe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowThongTinThe();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_TheoNgay_Click(object sender, EventArgs e)
        {
            TimKiem_TheoNgay();
        }

        void TimKiem_TheoNgay()
        {
            switch (cbTieuChi_TheoNgay.SelectedIndex)
            {
                case 0:
                    DSTheChuaNhan();
                    break;
                case 1:
                    DSTheDaNhanChuaGiao();
                    break;
                case 2:
                    DSTheDaGiao();
                    break;
                case 3:
                    TatCaThe();
                    break;
                default: break;
            }
        }

        private void btnTimKiem_TheoThongTin_Click(object sender, EventArgs e)
        {
            TimKiem_TheoThongTin();
        }

        void TimKiem_TheoThongTin()
        {
            if (string.IsNullOrEmpty(txtThongTin.Text))
            {
                MessageBox.Show("Hãy nhập thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK);
                txtThongTin.Focus();
                return;
            }
            switch (cbTieuChi_ThongTin.SelectedIndex)
            {
                case 0:
                    DSTheTheoCMND();
                    break;
                case 1:
                    DSTheTheoMaKH();
                    break;
                default: break;
            }
        }

        private void dgvThongTinThe_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add("Xóa", new EventHandler(btnDeleteThe_Click));
                cm.MenuItems.Add("Sửa", new EventHandler(btnSua_Click));
                dgvThongTinThe.ContextMenu = cm;
                dgvThongTinThe.ContextMenu.Show(dgvThongTinThe, e.Location);
            }
        }

        void ShowThongTinThe()
        {
            try
            {
                int rowIndex = dgvThongTinThe.SelectedRows[0].Index;
                frmThongTinThe frm = new frmThongTinThe(
                    Convert.ToInt32(dgvThongTinThe.Rows[rowIndex].Cells[0].Value)
                    );
                //frm.MdiParent = this;
                frm.Show();
                frm.BringToFront();
            }
            catch
            {

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ShowThongTinThe();
        }

        private void btnDeleteThe_Click(object sender, EventArgs e)
        {
            string soThe = dgvThongTinThe.SelectedRows[0].Cells[3].Value.ToString();
            int ID = Convert.ToInt32(dgvThongTinThe.SelectedRows[0].Cells[0].Value);

            try
            {
                The the = new The(ThongTinTheDAL.LayThongTinThe(ID));

                if (Thong_tin_dang_nhap.ma_pb != the.maPB)
                {
                    MessageBox.Show("Không có quyền xóa thẻ này!\nLiên hệ chi nhánh phát hành thẻ để được hỗ trợ!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }
            catch
            {
                ErrorMessageDAL.DataAccessError();
                return;
            }

            DialogResult result = MessageBox.Show("Có chắc chắn xóa thẻ này?", "Thông báo", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    TheDAL.XoaThe_TheoID(ID);
                    
                    MessageBox.Show("Xóa thẻ "+ soThe + " thành công!","Thông báo", MessageBoxButtons.OK);
                    if (tcrlQuanLyThe.SelectedIndex == 1)
                        TimKiem_TheoThongTin();
                    else TimKiem_TheoNgay();
                }
                catch
                {
                    ErrorMessageDAL.DataAccessError();
                }
            }
        }

        private void txtThongTin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Tim kiem
                TimKiem_TheoThongTin();
            }
        }
    }
}
