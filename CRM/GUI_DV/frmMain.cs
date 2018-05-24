using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGRIBANKHD.GUI
{
    public partial class frmMain : Form
    {
        
        public frmMain(bool admin, string chuc_vu)
        {
            if (admin || chuc_vu == "Trưởng phòng Kế hoạch & Kinh doanh" || chuc_vu == "Trưởng phòng Khách hàng Doanh nghiệp" || chuc_vu == "Trưởng phòng Khách hàng Hộ sản xuất và Cá nhân")
            {
                InitializeComponent();
            }
            else
            {
                InitializeComponent();
                quaToolStripMenuItem.Visible = false;
            }

            if (admin)
            {
                testToolStripMenuItem.Visible = true;
            }
            else
            {
                testToolStripMenuItem.Visible = false;
            }
            Text = "PHẦN MỀM HỖ TRỢ NGHIỆP VỤ - v" + Application.ProductVersion + " - AGRIBANKHD - "+ AGRIBANKHD.Utilities.Thong_tin_dang_nhap.ma_cn;
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDang_nhap frm = new frmDang_nhap();
            frm.Show();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoi_mat_khau frm = new frmDoi_mat_khau();
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
        }


        private void tsMenuPhatHanhThe_Click(object sender, EventArgs e)
        {
            bool isAlive = false;
            foreach (var c in this.MdiChildren)
            {
                if (c.GetType() == typeof(frmPhatHanhTheGhiNo))
                {
                    isAlive = true;
                    break;
                }
            }
            if (isAlive) return;
            frmPhatHanhTheGhiNo frm = new frmPhatHanhTheGhiNo();
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
        }

        private void liênHệToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bộ phận Điện toán\nPhòng Kế toán và Ngân quỹ\nAgribank tỉnh Hải Dương\nĐiện thoại: 02203.890.972", "Liên Hệ", MessageBoxButtons.OK);
        }

        private void quảnLýThẻToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isAlive = false;
            foreach (var c in this.MdiChildren)
            {
                if (c.GetType() == typeof(frmQuanLyThe))
                {
                    isAlive = true;
                    break;
                }
            }
            if (isAlive) return;
            frmQuanLyThe frm = new frmQuanLyThe();
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
        }

        private void kiểmQuỹToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isAlive = false;
            foreach (var c in this.MdiChildren)
            {
                if (c.GetType() == typeof(frmKiemQuy))
                {
                    isAlive = true;
                    break;
                }
            }
            if (isAlive) return;
            frmKiemQuy frm = new frmKiemQuy();
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
        }

        private void tiếpQuỹToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isAlive = false;
            foreach (var c in this.MdiChildren)
            {
                if (c.GetType() == typeof(frmTiepQuy))
                {
                    isAlive = true;
                    break;
                }
            }
            if (isAlive) return;
            frmTiepQuy frm = new frmTiepQuy();
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
        }

        private void mẫu01ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isAlive = false;
            foreach (var c in this.MdiChildren)
            {
                if (c.GetType() == typeof(frmPhatHanhTheGhiNo))
                {
                    isAlive = true;
                    break;
                }
            }
            if (isAlive) return;
            frmPhatHanhTheGhiNo frm = new frmPhatHanhTheGhiNo();
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
        }

        private void mẫu08THEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isAlive = false;
            foreach (var c in this.MdiChildren)
            {
                if (c.GetType() == typeof(frmThanhLapToVanChuyen))
                {
                    isAlive = true;
                    break;
                }
            }
            if (isAlive) return;
            frmThanhLapToVanChuyen frm = new frmThanhLapToVanChuyen();
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
        }

        private void mẫu49THEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isAlive = false;
            foreach (var c in this.MdiChildren)
            {
                if (c.GetType() == typeof(frmTiepQuy))
                {
                    isAlive = true;
                    break;
                }
            }
            if (isAlive) return;
            frmTiepQuy frm = new frmTiepQuy();
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
        }

        private void mẫuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isAlive = false;
            foreach (var c in this.MdiChildren)
            {
                if (c.GetType() == typeof(frmKiemQuy))
                {
                    isAlive = true;
                    break;
                }
            }
            if (isAlive) return;
            frmKiemQuy frm = new frmKiemQuy();
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
        }

        private void quảnLýThẻToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool isAlive = false;
            foreach (var c in this.MdiChildren)
            {
                if (c.GetType() == typeof(frmQuanLyThe))
                {
                    isAlive = true;
                    break;
                }
            }
            if (isAlive) return;
            frmQuanLyThe frm = new frmQuanLyThe();
            frm.MdiParent = this;
            frm.Show();
            frm.BringToFront();
        }


    }
}
