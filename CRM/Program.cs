using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRM.DAL;
using CRM.Utilities;
using System.Data.SqlClient;

namespace CRM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (CommonMethod.IsServerConnected())
            {
                frmDangnhap frm = new frmDangnhap();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new frmMain());
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Không kết nối được đến máy chủ, đề nghị kiểm tra kết nối mạng hoặc liên hệ về Phòng Điện toán Agribank tỉnh!", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            ////Application.Run(new frmMain());
            //Application.Run(new frmDangnhap());
        }
    }
}