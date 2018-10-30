using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRM.GUI_DT
{
    public partial class frmDienToan : Form
    {
        public frmDienToan()
        {
            InitializeComponent();
        }

        private void btnIPCAS_Click(object sender, EventArgs e)
        {
            frmIPCAS frm = new frmIPCAS();
            frm.ShowDialog();
        }
    }
}
