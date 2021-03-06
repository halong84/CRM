using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.Threading;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using N_MicrosoftExcelClient;

namespace CRM
{
    public partial class frmHT_TienIch : Form
    {
        MicrosoftExcelClient m_ExcelClient = null;        
        public frmHT_TienIch()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            dtpThang.CustomFormat = "MM/yyyy";
            DateTime dtCurrent = DateTime.Now;
            //dtpThang.Value = dtCurrentTime.AddMonths(-1);
            if (dtCurrent.Month == 1)
            {
                dtpThang.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.AddYears(-1).Year);
            }
            else
            {
                dtpThang.Text = Convert.ToString(dtCurrent.AddMonths(-1).Month) + "/" + Convert.ToString(dtCurrent.Year);
            }
        }

        private void frmHT_TienIch_Load(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            String cn = "";
            DataTable dt = new DataTable();
            String strCmd = "SELECT macn from _User where User_ID='" + frmDangnhap.UserID + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            int i = dt.Rows.Count;
            if (i != 0)
            {
                cn = dt.Rows[i - 1]["MACN"].ToString();
            }

            String qyery_temp = "";
            qyery_temp = "Delete DiemKH where left(makh,4)='" + cn + "' and thang ='" + dtpThang.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();
            qyery_temp = "update SDBQCT set mact = null,diem=0,tytrong=0,thucdiem=0 where left(makh,4)='" + cn + "' and thang ='" + dtpThang.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();
            qyery_temp = "update TGGTCT set MACT=null, DIEM=0,TYTRONG=0,THUCDIEM=0 where left(makh,4)='" + cn + "' and thang ='" + dtpThang.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }

            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();
            qyery_temp = "Update SPDVCT set MACT=null, DIEM=0,TYTRONG=0,THUCDIEM=0 where left(makh,4)='" + cn + "' and thang ='" + dtpThang.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();
            qyery_temp = "update PROFITCT set MACT=null, DIEM=0,TYTRONG=0,THUCDIEM=0 where left(makh,4)='" + cn + "' and thang ='" + dtpThang.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();

            MessageBox.Show("Đã reset xong!");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            xuatdiemweb();
        }
        private void xuatdiemweb()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            string line = "";
            OleDbConnection myConnection;
            OleDbCommand myCommand;


            //Cap nhat du lieu access
            try
            {
                StreamReader reader = new StreamReader("dbweb.txt");


                line = reader.ReadLine();
                reader.Close();
            }
            catch
            {
                MessageBox.Show("không đọc được file dbweb.txt");
                this.Close();
            }


            string strConnect = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + line;
            myConnection = new OleDbConnection(strConnect);
            myConnection.Open();


            DataTable dt1 = new DataTable();
            String sCommand = "SELECT * from diem_cn";
            new OleDbDataAdapter(sCommand, myConnection).Fill(dt1);
            if (dt1.Rows.Count != 0)
            {
                sCommand = "delete * from diem_cn";
                myCommand = new OleDbCommand(sCommand, myConnection);
                myCommand.ExecuteNonQuery();

            }
            String strCmd = "SELECT * from diem_cn";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                sCommand = "INSERT INTO diem_cn(MAKH,TENKH,DIEM,NGAYCAPNHAT,MA) Values ('" + dt.Rows[i - 1]["MAKH"].ToString() + "','" + dt.Rows[i - 1]["TENKH"].ToString() + "'," + dt.Rows[i - 1]["DIEM"].ToString() + ",'" + dt.Rows[i - 1]["NGAYCAPNHAT"].ToString() + "','" + dt.Rows[i - 1]["MA"].ToString() + "')";

                try
                {
                    myCommand = new OleDbCommand(sCommand, myConnection);
                    myCommand.ExecuteNonQuery();
                }
                catch { }
            }
            MessageBox.Show("Đã cập nhật!");

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

        private void buttonX2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "SKTIENVAY.xls";
            openFileDialog1.Filter = "SKTIENVAY(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp, filename = "";
            filename = "SKTIENVAY" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Cursor.Current = Cursors.WaitCursor;
                //if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 11, 11).ToUpper() != filename.ToUpper())
                //{
                //    MessageBox.Show("Chọn sai tên file import!");
                //    return;

                //}
                //else
                //{
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','VND','" + filename + "','" + dtpThang.Text + "')";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        frmMain.conn.Close();
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không?", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        //MessageBox.Show("Du lieu nay da duoc import!");   
                    }
                //}
                //Xoa du lieu dang co trong table SMS
                try
                {
                    qyery_temp = "Delete SKTIENVAY where left(makh,4)='" + frmMain.cn + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {

                    String makh = dt_temp.Rows[i][0].ToString();
                    String ngaygn = dt_temp.Rows[i][9].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(6, 4);
                    String ngaydh = dt_temp.Rows[i][10].ToString();
                    if (ngaydh == "00/00/0000")
                    {
                        ngaydh = "01/01/1900";
                    }
                    else
                    {
                        ngaydh = dt_temp.Rows[i][10].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][10].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][10].ToString().Substring(6, 4);
                    }

                    try
                    {
                        qyery_temp = "INSERT INTO SKTIENVAY(MAKH,SOHD,SOGN,NGAYGN,SOTIENGN,DUNO,NGAYDENHAN,LAISUAT,CCY,MACN) Values ('" + makh + "','" + dt_temp.Rows[i][3].ToString() + "','" + dt_temp.Rows[i][8].ToString() + "','" + ngaygn + "'," + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + "," + Convert.ToDecimal(dt_temp.Rows[i][17].ToString()) + ",'" + ngaydh + "'," + Convert.ToDecimal(dt_temp.Rows[i][11].ToString()) + ",'" + dt_temp.Rows[i][14].ToString() + "','" + frmMain.cn + "')";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        frmMain.conn.Close();
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                    }

                }
                InsertSKTienVayCT();
                MessageBox.Show("Đã import dữ liệu xong!");

                Cursor.Current = Cursors.Default;
            }
            
        }
        //Dua du lieu vao table SKTienVayCT
        private void InsertSKTienVayCT()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT * from sktienvay where left(SKTIENVAY.MAKH,4)='" + frmMain.cn + "'";
                      
            int i = 0;
            
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            dt.Clear();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            for (i = 0; i < dt.Rows.Count; i++)
            {

                String smakh = dt.Rows[i][0].ToString();
                String ngaygn = dt.Rows[i][3].ToString().Substring(3, 2) + "/" + dt.Rows[i][3].ToString().Substring(0, 2) + "/" + dt.Rows[i][3].ToString().Substring(6, 4);
                String ngaydh = dt.Rows[i][6].ToString();
                if (ngaydh == "00/00/0000")
                {
                    ngaydh = "01/01/1900";
                }
                else
                {
                    ngaydh = dt.Rows[i][6].ToString().Substring(3, 2) + "/" + dt.Rows[i][6].ToString().Substring(0, 2) + "/" + dt.Rows[i][6].ToString().Substring(6, 4);
                }

                //Dua du lieu vao bang sktienvayct
                try
                {
                    sCommand = "INSERT INTO SKTienVayCT(MAKH,SOHD,SOGN,NGAYGN,SOTIENGN,DUNO,NGAYDENHAN,LAISUAT,CCY,MACN,THANG) Values ('" + smakh + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + ngaygn + "'," + Convert.ToDecimal(dt.Rows[i][4].ToString()) + "," + Convert.ToDecimal(dt.Rows[i][5].ToString()) + ",'" + ngaydh + "'," + Convert.ToDecimal(dt.Rows[i][7].ToString()) + ",'" + dt.Rows[i][8].ToString() + "','" + frmMain.cn + "','" + dtpThang.Text + "')";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }

                    sCommand = "Update SKTienVayCT set SoTienGN =" + Convert.ToDecimal(dt.Rows[i][4].ToString()) + " where SOGN='" + dt.Rows[i][2].ToString() + "' and thang='" + dtpThang.Text + "'";
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }
            }
            

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String sCommand = "";
            String strCmd = "SELECT * from lichsudiem where thang='04/2013' and left(makh,4)='4809'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sCommand = "Update diem_cn set diem_cn.diem = diem_cn.diem - " + Convert.ToDecimal(dt.Rows[i]["diem"].ToString()) + " where makh = '" + dt.Rows[i]["MAKH"].ToString() + "'";

                try
                {
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }
                catch { }
            }
            MessageBox.Show("Đã cập nhật!");
        }

        
    }
}