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
using Excel = Microsoft.Office.Interop.Excel;
using N_MicrosoftExcelClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmDM_Import : Form
    {
        MicrosoftExcelClient m_ExcelClient = null;
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrMaDM;        

        public frmDM_Import()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbbTen.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void frmDM_Import_Load(object sender, EventArgs e)
        {
            layDS_Import();            
        }

        private DataTable read_excel(String file_excel)
        {
            Excel.Application ExcelObj = new Excel.Application();

            Excel.Workbook theWorkbook = null;



            theWorkbook = ExcelObj.Workbooks.Open(file_excel, Missing.Value, Missing.Value, Missing.Value
                                                  , Missing.Value, Missing.Value, Missing.Value, Missing.Value
                                                 , Missing.Value, Missing.Value, Missing.Value, Missing.Value
                                                , Missing.Value, Missing.Value, Missing.Value);
            Excel.Sheets sheets = theWorkbook.Worksheets;

            Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);//Get the reference of second worksheet

            //MessageBox.Show(worksheet.Name);//Get the name of worksheet.
            this.m_ExcelClient = new MicrosoftExcelClient(file_excel);



            //Reset & Reopen Connection
            this.m_ExcelClient.openConnection();

            //Update the message window
            //this.updateMessageWindow(1);

            DataTable result = this.m_ExcelClient.readEntireSheet(worksheet.Name);
            this.m_ExcelClient.closeConnection();

            //ExcelObj.Quit();

            return result;
        }

        private void layDS_Import()
        {
            arrMaDM = new ArrayList();

            cbbTen.Items.Clear();
            cbbTen.Refresh();

            strCmd = "SELECT * FROM DMIMPORT WHERE LoaiDM='1' ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, DataAccess.conn);
                adapter.SelectCommand.ExecuteReader();
                DataAccess.conn.Close();
            }
            catch
            {
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
            }

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dtResult = ds.Tables[0];

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    cbbTen.Items.Add(dtResult.Rows[i]["Ten"].ToString());
                    arrMaDM.Add(dtResult.Rows[i]["MaDM"].ToString());
                }
                catch { }
            }

            if (iRows > 0)
            {
                cbbTen.SelectedIndex = 0;
            }
        }

        private void layDS_Khachhang()
        {
            openFileDialog1.FileName = "DSKH.xls";
            openFileDialog1.Filter = "DSKH (*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;

                DataTable dt_temp = read_excel(openFileDialog1.FileName);

                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    try
                    {
                        if (dt_temp.Rows[i][0].ToString() != null)
                        {
                            String qyery_temp;
                            String ngaycap, ngaysinh, didong;
                            String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                            String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                            String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                            didong = dt_temp.Rows[i][9].ToString();
                            ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                            if (ngaysinh != "")
                            {
                                ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                            }
                            else
                            {
                                ngaysinh = "01/01/1990";
                            }
                            ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                            if (ngaycap != "")
                            {
                                ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                            }
                            else
                            {
                                ngaycap = "01/01/1990";
                            }
                            String gt = dt_temp.Rows[i][10].ToString();
                            Int16 gioitinh;
                            if (gt == "Nam" || gt == "Male" || gt == "nam")
                            {
                                gioitinh = 1;
                            }
                            else
                            {
                                gioitinh = 0;
                            }
                            Int16 loaikh;
                            if (dt_temp.Rows[i][7].ToString() == "Cá nhân")
                            {
                                loaikh = 1;
                            }
                            else
                            {
                                loaikh = 2;
                            }

                            try
                            {
                                
                                qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "')";
                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                                DataAccess.conn.Open();
                                frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                DataAccess.conn.Close();
                            }
                            catch
                            {
                                qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                                DataAccess.conn.Open();
                                frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                DataAccess.conn.Close();
                            }

                        }
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                    }                   
                }
                MessageBox.Show("Đã import dữ liệu xong!");

                //Cap nhat matinh la ten tinh
               
               
                Cursor.Current = Cursors.Default;
            }            
        }

        private void layDS_Nhanvien()
        {
            openFileDialog1.FileName = "DSNV.xls";
            openFileDialog1.Filter = "DSNV (*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;

                DataTable dt_temp = read_excel(openFileDialog1.FileName);

                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    try
                    {
                        if (dt_temp.Rows[i][0].ToString() != null)
                        {
                            String qyery_temp;
                            try
                            {
                                qyery_temp = "INSERT INTO _USER(MANV,TENNV,CHUCVU,MAPB,MACN) Values ('" + dt_temp.Rows[i][20].ToString() + "',N'" + dt_temp.Rows[i][5].ToString() + "',N'" + dt_temp.Rows[i][8].ToString() + "','" + dt_temp.Rows[i][3].ToString() + "','" + dt_temp.Rows[i][1].ToString() + "')";
                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                                DataAccess.conn.Open();
                                frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                DataAccess.conn.Close();
                            }
                            catch
                            {
                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                            }
                        }
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                    }
                }
                MessageBox.Show("Đã import dữ liệu xong!");

                //Cap nhat matinh la ten tinh
                Cursor.Current = Cursors.Default;
            }
        }

        private void layDS_Phongban()
        {
            openFileDialog1.FileName = "DSKH.xls";
            openFileDialog1.Filter = " DSKH (*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;

                DataTable dt_temp = read_excel(openFileDialog1.FileName);

                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    try
                    {
                        if (dt_temp.Rows[i][0].ToString() != null)
                        {
                            String qyery_temp;
                            try
                            {
                                qyery_temp = "INSERT INTO PHONGBAN(MAPB,MACN,TENPB) Values ('" + dt_temp.Rows[i][1].ToString() + "','" + dt_temp.Rows[i][0].ToString() + "',N'" + dt_temp.Rows[i][2].ToString() + "')";
                                DataAccess.conn.Open();
                                frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                DataAccess.conn.Close();
                            }
                            catch
                            {
                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                            }
                        }
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                    }
                }
                MessageBox.Show("Đã import dữ liệu xong!");

                //Cap nhat matinh la ten tinh
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "DM_KHACHHANG")
            {
                layDS_Khachhang();
            }
            //else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "DM_PHONGBAN")
            //{
            //    layDS_Phongban();
            //}
            //else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "DM_NHANVIEN")
            //{
            //    layDS_Nhanvien();
            //}
            
        }        
    }
}