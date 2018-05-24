using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CRM
{
    public partial class frmTKSDBQ : Form
    {
        public static string tuthang = "", denthang = "";
        //public static string thang = "";
        public static string cn = "";
        public static byte loaikh = 1;
        public static decimal tusdbq = 0, densdbq = 0;
        public static string maKH = "";

        public frmTKSDBQ()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbbChinhanh.DropDownStyle = ComboBoxStyle.DropDownList;
            txtMaKH.Enabled = false;
            chkMaKH.Checked = false;

            DateTime dtCurrent = DateTime.Now;
            dtpFrom.CustomFormat = "MM/yyyy";
            dtpTo.CustomFormat = "MM/yyyy";
            
            if (dtCurrent.Month == 1)
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);
                dtpTo.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);
            }
            else
            {
                dtpFrom.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);
                dtpTo.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);
            }
        }

        private void frmTKSDBQ_Load(object sender, EventArgs e)
        {
            if (frmMain.cn == "4800")
            {
                cbbChinhanh.Enabled = true;
            }
            else
            {
                cbbChinhanh.Enabled = false;
            }

            layCN(); 
        }

        private void layCN()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT macn,tencn from Chinhanh order by macn";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            cbbChinhanh.DataSource = dt;
            cbbChinhanh.DisplayMember = "tencn";
            cbbChinhanh.ValueMember = "macn";

            cbbChinhanh.SelectedValue = frmMain.cn;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            frmMain.manhinhin = 18;
            cn = cbbChinhanh.SelectedValue.ToString();
            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
            //thang = dtpFrom.Text;
            
            if (rdbCN.Checked == true)
            {
                loaikh = 1;
            }
            else
            {
                loaikh = 2;
            }

            if (txtFrom.Text == "")
            {
                tusdbq = 0;                
            }
            else
            {
                tusdbq = Convert.ToDecimal(txtFrom.Text.Trim());                
            }

            if (txtTo.Text == "")
            {
                densdbq = 100000000000000000;
            }
            else
            {
                densdbq = Convert.ToDecimal(txtTo.Text.Trim());
            }

            if (chkMaKH.Checked == true)
            {
                maKH = txtMaKH.Text;
            }
            else
            {
                maKH = "%";
            }
            
            @In form_in = new @In();
            form_in.Show();
        }

        private void txtFrom_TextChanged(object sender, EventArgs e)
        {
            if (txtFrom.Text != "")
            {
                string sDummy = txtFrom.Text;
                try
                {
                    int iKeep = txtFrom.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtFrom.Text[i] == ',')
                        {
                            iKeep -= 1;
                        }
                    }

                    sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                    for (int i = 0; i <= iKeep; i++)
                    {
                        if (sDummy[i] == ',')
                        {
                            iKeep += 1;
                        }
                    }

                    txtFrom.Text = sDummy;
                    txtFrom.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }            
        }

        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {
            if (txtTo.Text != "")
            {
                string sDummy = txtTo.Text;
                try
                {
                    int iKeep = txtTo.SelectionStart - 1;
                    for (int i = iKeep; i >= 0; i--)
                    {
                        if (txtTo.Text[i] == ',')
                        {
                            iKeep -= 1;
                        }
                    }

                    sDummy = String.Format("{0:N0}", Convert.ToInt64(sDummy.Replace(",", "")));
                    for (int i = 0; i <= iKeep; i++)
                    {
                        if (sDummy[i] == ',')
                        {
                            iKeep += 1;
                        }
                    }

                    txtTo.Text = sDummy;
                    txtTo.SelectionStart = iKeep + 1;
                }
                catch
                {
                }
            }            
        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void chkMaKH_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMaKH.Checked == true)
            {
                txtMaKH.Enabled = true;                
            }
            else
            {
                txtMaKH.Enabled = false;
                txtMaKH.Text = "";
            }
        }
    }
}