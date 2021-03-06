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

namespace CRM
{
    public partial class frmImport_auto : Form
    {
        MicrosoftExcelClient m_ExcelClient = null;
        private DataTable dtResult = new DataTable();
        //string strCmd = "";
        String qyery_temp;
        DataTable dt_tygia = new DataTable();
        decimal tygia = 1;
        int isdbq = 0, itggt = 0, ispdv = 0, iprofit = 0;
        public frmImport_auto()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            DateTime dtCurrent = DateTime.Now;
            dtpThang.CustomFormat = "MM/yyyy";
            dtpThang.Value = dtCurrent.AddMonths(-1);
        }
        
        private void frmImport_auto_Load(object sender, EventArgs e)
        {
           
            //dtpThang.Value = dtCurrentTime.AddMonths(-1);
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

        private void lay_SDBQ()
        {
           
            String qyery_temp;
            DataTable dt_temp = new DataTable();
            String filename = "", CCY = "VND";            
            filename = frmMain.ddimport+ "\\SDBQ" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.XLS";
            //Kiem tra du lieu da duoc import chua
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','SDBQ_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File "+filename+ " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            //Xoa du lieu dang co trong table SDBQ
            try
            {
                qyery_temp = "Delete SDBQ where left(makh,4)='" + frmMain.cn + "' and ccy='" + CCY + "'";
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
            //Import du lieu so du binh quan VND
            try
            {
              dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {

                        String sdu = dt_temp.Rows[i][4].ToString().Replace(",", "");
                        String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();

                        try
                        {
                            qyery_temp = "INSERT INTO SDBQ(MAKH,CCY,SOTIEN,SOTIENHOA) Values ('" + makh + "','" + CCY + "'," + Convert.ToDecimal(sdu) + "," + Convert.ToDecimal(sdu) + ")";
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
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
            filename = frmMain.ddimport+ "\\SDBQ" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "USD.XLS";
            CCY="USD";

            //Xoa du lieu dang co trong table SDBQ
            try
            {
                qyery_temp = "Delete SDBQ where left(makh,4)='" + frmMain.cn + "' and ccy='" + CCY + "'";
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
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','SDBQ_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }                
            }
            //
            //Import du lieu so du binh quan USD
            try
            {
                dt_temp.Clear();
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {

                        String sdu = dt_temp.Rows[i][4].ToString().Replace(",", "");
                        String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();

                        try
                        {
                            qyery_temp = "INSERT INTO SDBQ(MAKH,CCY,SOTIEN,SOTIENHOA) Values ('" + makh + "','" + CCY + "'," + Convert.ToDecimal(sdu) + "," + Convert.ToDecimal(sdu) * tygia + ")";
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
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
            
            isdbq=1;
       }
        //Dua du lieu vao table SDBQCT
        private void InsertSDBQCT()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT SDBQ.MAKH,KHACHHANG.LOAIKH,sum(SDBQ.SOTIENHOA) as SOTIENHOA from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH and left(SDBQ.MAKH,4)='" + frmMain.cn + "'group by sdbq.makh, khachhang.loaikh Order by SOTIENHOA desc";
            String makh = "";
            Byte loaikh = 1;
            int i = 0;
            decimal sdbq = 0;
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            for (i = 0; i < dt.Rows.Count; i++)
            {

                sdbq = Convert.ToDecimal(dt.Rows[i]["SOTIENHOA"].ToString());
                makh = dt.Rows[i]["MAKH"].ToString();
                loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                //Dua du lieu vao bang sdbqct
                try
                {
                    sCommand = "INSERT INTO SDBQCT(MAKH,THANG,SDBQ,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + sdbq + "," + loaikh + ")";
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

                    sCommand = "Update SDBQCT set SDBQ =" + sdbq + " where makh='" + makh + "' and thang='"+dtpThang.Text+"'";
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }
            }
        }

        //Dua du lieu vao table SDBQNT
        private void InsertSDBQNT()
        {
            DataTable dt = new DataTable();
            String sCommand = "SELECT SDBQ.MAKH,CCY,SOTIEN from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH and left(SDBQ.MAKH,4)='" + frmMain.cn + "'and KHACHHANG.LOAIKH=1";
                      
            int i = 0;
            
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            for (i = 0; i < dt.Rows.Count; i++)
            {

               
                //Dua du lieu vao bang sdbqnt
                try
                {
                    sCommand = "INSERT INTO SDBQNT(MAKH,THANG,LOAITIEN,SDBQ) Values ('" + dt.Rows[i]["makh"].ToString() + "','" + dtpThang.Text + "','" + dt.Rows[i]["CCY"].ToString() + "'," + Convert.ToDecimal(dt.Rows[i]["SOTIEN"].ToString()) + ")";
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

                    sCommand = "Update SDBQNT set SDBQ =" + Convert.ToDecimal(dt.Rows[i]["SOTIEN"].ToString()) + " where makh='" + dt.Rows[i]["makh"].ToString() + "' and thang='" + dtpThang.Text + "' and loaitien='" + dt.Rows[i]["CCY"].ToString() + "'";
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }
            }
            
        }


        private void lay_SKTG()
        {
            String qyery_temp;           
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport+"\\SK" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.XLS";                         
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','SKTIENGUI_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                   // this.Close();
                    
                }
                
            }
                
            //Xoa du lieu dang co trong table SaoKe
            try
            {
                qyery_temp = "Delete SKTIENGUI where left(makh,4)='" + frmMain.cn + "' and ccy='" + CCY + "'";
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

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
                //this.Dispose();
                
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (((Convert.ToDecimal(dt_temp.Rows[i][7].ToString()) < 3) || (Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) < 0) || (dt_temp.Rows[i][18].ToString().Substring(0, 3) == "510") || (dt_temp.Rows[i][18].ToString().Substring(0, 3) == "511") || (dt_temp.Rows[i][18].ToString().Substring(0, 3) == "512")) && (dt_temp.Rows[i][34].ToString() == "Normal"))
                    //if ((dt_temp.Rows[i][18].ToString().Substring(0, 3) != "670") && (dt_temp.Rows[i][18].ToString().Substring(0, 3) != "671"))
                    {
                        String sdu = dt_temp.Rows[i][4].ToString().Replace(",", "");
                        String profit = dt_temp.Rows[i][7].ToString().Replace("%", "");
                        String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][2].ToString();
                        String ngaymo = dt_temp.Rows[i][9].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(6, 4);
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
                            qyery_temp = "INSERT INTO SKTIENGUI(MAKH,SOTK,CCY,NGAYMO,NGAYDENHAN,SOTIEN,SOTIENHOA,MANV) Values ('" + makh + "','" + dt_temp.Rows[i][8].ToString() + "','" + CCY + "','" + ngaymo + "','" + ngaydh + "'," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) + "," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) + ",'" + dt_temp.Rows[i][37].ToString() + "')";
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
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
            //Import sao ke USD
            filename = frmMain.ddimport + "\\SK" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "USD.XLS";
            CCY = "USD";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','SKTIENGUI_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            //Xoa du lieu dang co trong table SaoKe
            try
            {
                qyery_temp = "Delete SKTIENGUI where left(makh,4)='" + frmMain.cn + "' and ccy='" + CCY + "'";
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

            try
            {
                dt_temp.Clear();
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    //if ((dt_temp.Rows[i][18].ToString().Substring(0, 3) != "670") && (dt_temp.Rows[i][18].ToString().Substring(0, 3) != "671"))
                    if ((Convert.ToDouble (dt_temp.Rows[i][7].ToString())==0) && (dt_temp.Rows[i][34].ToString() == "Normal"))
                    {

                        String sdu = dt_temp.Rows[i][4].ToString().Replace(",", "");
                        String profit = dt_temp.Rows[i][7].ToString().Replace("%", "");
                        String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][2].ToString();
                        String ngaymo = dt_temp.Rows[i][9].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][9].ToString().Substring(6, 4);
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
                            qyery_temp = "INSERT INTO SKTIENGUI(MAKH,SOTK,CCY,NGAYMO,NGAYDENHAN,SOTIEN,SOTIENHOA,MANV) Values ('" + makh + "','" + dt_temp.Rows[i][8].ToString() + "','" + CCY + "','" + ngaymo + "','" + ngaydh + "'," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) + "," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) * tygia + ",'" + dt_temp.Rows[i][37].ToString() + "')";
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
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
            itggt = 1;
        }
        //Dua du lieu vao table TGGTCT
        private void InsertTGGTCT()
        {
            //Cap nhat so thang gui cua tung so tiet kiem
            DataTable dt = new DataTable();
            String makh = "";
            byte loaikh;
            int thang;
            String sCommand = "Update SKTIENGUI set Thang =datediff(month,ngaymo,ngaydenhan) where ngaydenhan <>'01/01/1900' and left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();

            sCommand = "SELECT SKTIENGUI.*,LOAIKH from SKTIENGUI,KHACHHANG where SKTIENGUI.MAKH=KHACHHANG.MAKH and SKTIENGUI.NGAYDENHAN<>'01/01/1900' and left(SKTIENGUI.MAKH,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                makh = dt.Rows[i]["MAKH"].ToString();
                loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                thang = Convert.ToInt16(dt.Rows[i]["THANG"].ToString());
                //Dua du lieu vao bang TGGTCT
                try
                {
                    String ngaymo = dt.Rows[i]["NGAYMO"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["NGAYMO"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["NGAYMO"].ToString().Substring(7, 4);
                    String ngaydenhan = dt.Rows[i]["NGAYDENHAN"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["NGAYDENHAN"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["NGAYDENHAN"].ToString().Substring(7, 4);
                    sCommand = "INSERT INTO TGGTCT(MAKH,THANG,SOTK,NGAYMO,NGAYDENHAN,THOIGIAN,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "','" + dt.Rows[i]["SOTK"].ToString() + "','" + ngaymo + "','" + ngaydenhan + "'," + thang + "," + loaikh + ")";
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

                    sCommand = "Update TGGTCT set THOIGIAN=" + thang + " where sotk='" + dt.Rows[i]["SOTK"].ToString() + "' and Thang = '" + dtpThang.Text + "'";
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }

            }
        }
        //Dua du lieu vao table SKTGTTCT
        private void InsertSKTGTTCT()
        {
            //Cap nhat so thang gui cua tung so tiet kiem
            DataTable dt = new DataTable();
            
            String sCommand = "delete SKTGTTCT where Thang ='"+dtpThang.Text+"' and left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();            
          
            try
            {
                sCommand = "insert into SKTGTTCT(MAKH,thang) select distinct(sktiengui.makh) as makh,'" + dtpThang.Text + "' as thang from sktiengui where left(sktiengui.makh,4)='" + frmMain.cn + "'";
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
            }
           
        }
        private void lay_Chuyentiendi()
        {
            String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport +"\\FXDI" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.XLS";            
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','FXDI_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }                  
            }
                
            //Xoa du lieu trong table CHUYENTIEN
            try
            {
                qyery_temp = "DELETE CHUYENTIEN WHERE LOAICHUYENTIEN=1 and left(makh,4)='" + frmMain.cn + "'";
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
            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;

            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {

                        String makh = dt_temp.Rows[i][10].ToString();
                        //Dua cac khach hang chuyen tien da co makh va danh sach chuyen tien de tinh diem
                        if (makh != "000000000")
                        {
                            String ID = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                            makh = frmMain.cn + makh;
                            qyery_temp = "INSERT INTO CHUYENTIEN(ID,MAKH,SOTIEN,LOAICHUYENTIEN,HOTEN,CCY,SOTIENHOA,MACN) Values ('" + ID + "','" + makh + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",1,N'" + dt_temp.Rows[i][5].ToString() + "','" + CCY + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",'" + frmMain.cn + "')";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                        else
                        //Dua cac khach hang chua co makh vao danh sach khach hang tiem nang
                        {


                            if (Char.IsNumber(Convert.ToChar(dt_temp.Rows[i][43].ToString().Substring(0, 1))))
                            {
                                try
                                {
                                    makh = "T" + frmMain.cn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                    qyery_temp = "INSERT INTO KHACHHANGCHUYENTIEN(MAKH,HOTEN,DIACHI1,CMND,MACN,GHICHU,TINHTRANG,LOAIKH) Values ('" + makh + "',N'" + dt_temp.Rows[i][5].ToString() + "',N'" + dt_temp.Rows[i][41].ToString() + "','" + dt_temp.Rows[i][43].ToString().Substring(0, 9) + "','" + frmMain.cn + "',N'Khách hàng giao dịch chuyển tiền',1,1)";
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
                                        qyery_temp = "update KHACHHANGCHUYENTIEN set DIACHI1=N'" + dt_temp.Rows[i][41].ToString() + "' where CMND='" + dt_temp.Rows[i][43].ToString().Substring(0, 9) + "'";
                                        if (frmMain.conn.State == ConnectionState.Open)
                                        {
                                            frmMain.conn.Close();
                                        }
                                        frmMain.conn.Open();
                                        frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                                        frmMain.myCommand.ExecuteNonQuery();
                                        frmMain.conn.Close();
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                Thread.Sleep(1);
            }
            ispdv = 1;
        }

        private void lay_Chuyentienden()
        {
            
            String qyery_temp;            
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();       
            filename = frmMain.ddimport + "\\FXDEN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.XLS";
           
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','FXDEN_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
               
            }
               
            //Xoa du lieu trong table CHUYENTIEN
            try
            {
                qyery_temp = "DELETE CHUYENTIEN WHERE LOAICHUYENTIEN=2 and left(makh,4)='" + frmMain.cn + "'";
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
            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {

                        String benacc = dt_temp.Rows[i][7].ToString();
                        //Dua cac khach hang chuyen tien da co makh vao danh sach chuyen tien de tinh diem
                        if (benacc.Substring(0, 4) == frmMain.cn)
                        {
                            String ID = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                            benacc = benacc.Replace(" ", "").Replace(".", "");
                            DataTable dt1 = new DataTable();

                            String sCommand = "SELECT makh from SKTIENGUI where sotk='" + benacc + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                            frmMain.conn.Close();

                            String makh = "";
                            int j = dt1.Rows.Count;
                            if (j != 0)
                            {
                                makh = dt1.Rows[j - 1]["makh"].ToString();
                            }
                            qyery_temp = "INSERT INTO CHUYENTIEN(ID,MAKH,SOTK,SOTIEN,LOAICHUYENTIEN,HOTEN,CCY,SOTIENHOA,MACN) Values ('" + ID + "','" + makh + "','" + benacc + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",2,N'" + dt_temp.Rows[i][6].ToString() + "','" + CCY + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) * tygia + ",'" + frmMain.cn + "')";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                        else
                        //Dua cac khach hang chua co makh vao danh sach khach hang tiem nang
                        {
                            if ((dt_temp.Rows[i][47].ToString() != "60204001") && (dt_temp.Rows[i][47].ToString().Substring(0, 2) == "60"))
                            {
                                if (Char.IsNumber(Convert.ToChar(dt_temp.Rows[i][60].ToString().Substring(0, 1))))
                                {
                                    try
                                    {
                                        String makh = "";
                                        makh = "T" + frmMain.cn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                        qyery_temp = "INSERT INTO KHACHHANGCHUYENTIEN(MAKH,HOTEN,DIACHI1,CMND,NHGIAODICH,MACN,GHICHU,TINHTRANG,LOAIKH) Values ('" + makh + "',N'" + dt_temp.Rows[i][6].ToString() + "',N'" + dt_temp.Rows[i][54].ToString() + "','" + dt_temp.Rows[i][60].ToString().Substring(0, 9) + "','" + dt_temp.Rows[i][47].ToString() + "','" + frmMain.cn + "',N'Khách hàng giao dịch chuyển tiền',1,1)";
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
                                            qyery_temp = "Update KHACHHANGCHUYENTIEN set DIACHI1= N'" + dt_temp.Rows[i][54].ToString() + "' where CMND=N'" + dt_temp.Rows[i][60].ToString().Substring(0, 9) + "'";
                                            if (frmMain.conn.State == ConnectionState.Open)
                                            {
                                                frmMain.conn.Close();
                                            }
                                            frmMain.conn.Open();
                                            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                                            frmMain.myCommand.ExecuteNonQuery();
                                            frmMain.conn.Close();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                Thread.Sleep(1);
            }
            ispdv = 1;  
        }

        private void lay_SMS()
        {
            DataTable dt_temp = new DataTable();
            String qyery_temp, filename = "";
            filename = frmMain.ddimport + "\\SMS" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
           
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','VND','SMS_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }                        
            }
        
            
            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {

                String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();
                String ngaydk = dt_temp.Rows[i][16].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][16].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][16].ToString().Substring(6, 4);
                //Dua cac khach hang su dung sms vao danh sach sms
                try
                {
                    if (makh.Substring(0, 4) == frmMain.cn)
                    {
                        qyery_temp = "INSERT INTO SMS(MAKH,NGAYDK,HIENTRANG) Values ('" + makh + "','" + ngaydk + "',1)";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        frmMain.conn.Close();
                    }
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
            ispdv = 1; 
        }

        private void lay_Dien()
        {
            DataTable dt_temp = new DataTable();
            String qyery_temp, filename = "";
            filename = frmMain.ddimport + "\\DIEN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','VND','DIEN_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }                
            }
                
            //Xoa du lieu dang co trong table DIEN
            try
            {
                qyery_temp = "Delete DIEN where left(makh,4)='" + frmMain.cn + "'";
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
            try
            {
               dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }
            if (dt_temp.Rows.Count <= 0)
                MessageBox.Show("File DIEN khong co du lieu, de nghi xem lai!");
            else
            {
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {

                    String sotk = dt_temp.Rows[i][5].ToString();
                    DataTable dt = new DataTable();
                    String strCmd = "SELECT makh from SKTIENGUI where sotk='" + sotk + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                    frmMain.conn.Close();

                    String makh = "";
                    int j = dt.Rows.Count;
                    if (j != 0)
                    {
                        makh = dt.Rows[j - 1]["makh"].ToString();
                    }

                    //Dua cac khach hang su dung dich vu tra tien dien vao danh sach dien
                    try
                    {
                        if (makh.Substring(0, 4) == frmMain.cn)
                        {
                            qyery_temp = "INSERT INTO DIEN(SOTK,MAKH) Values ('" + sotk + "','" + makh + "')";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                    }
                }
                ispdv = 1;
            }
        }

        private void lay_VNPT()
        {

            DataTable dt_temp = new DataTable();
            String qyery_temp, filename = "";
            filename = frmMain.ddimport + "\\VNPT" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";            
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','VND','VNPT_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                
            }
                
            //Xoa du lieu dang co trong table VNPT
            try
            {
                qyery_temp = "Delete VNPT where left(makh,4)='" + frmMain.cn + "'";
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
            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }
            if (dt_temp.Rows.Count <= 0)
                MessageBox.Show("File VNPT khong co du lieu, de nghi xem lai!");
            else
            {
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {

                    String sotk = dt_temp.Rows[i][5].ToString();
                    DataTable dt = new DataTable();
                    String strCmd = "SELECT makh from SKTIENGUI where sotk='" + sotk + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                    frmMain.conn.Close();

                    String makh = "";
                    int j = dt.Rows.Count;
                    if (j != 0)
                    {
                        makh = dt.Rows[j - 1]["makh"].ToString();
                    }

                    //Dua cac khach hang su dung dich vu tra tien dien vao danh sach dien
                    try
                    {
                        if (makh.Substring(0, 4) == frmMain.cn)
                        {
                            qyery_temp = "INSERT INTO VNPT(SOTK,MAKH) Values ('" + sotk + "','" + makh + "')";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                    }

                }
                ispdv = 1;
            } 
       }

        private void lay_NUOC()
        {

            DataTable dt_temp = new DataTable();
            String qyery_temp, filename = "";
            filename = frmMain.ddimport + "\\NUOC" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','VND','NUOC_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

            }

            //Xoa du lieu dang co trong table NUOC
            try
            {
                qyery_temp = "Delete NUOC where left(makh,4)='" + frmMain.cn + "'";
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
            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }
            if (dt_temp.Rows.Count <= 0)
                MessageBox.Show("File NUOC khong co du lieu, de nghi xem lai!");
            else
            {
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {

                    String sotk = dt_temp.Rows[i][5].ToString();
                    DataTable dt = new DataTable();
                    String strCmd = "SELECT makh from SKTIENGUI where sotk='" + sotk + "'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    new SqlDataAdapter(strCmd, frmMain.conn).Fill(dt);
                    frmMain.conn.Close();

                    String makh = "";
                    int j = dt.Rows.Count;
                    if (j != 0)
                    {
                        makh = dt.Rows[j - 1]["makh"].ToString();
                    }

                    //Dua cac khach hang su dung dich vu tra tien nuoc vao danh sach nuoc
                    try
                    {
                        if (makh.Substring(0, 4) == frmMain.cn)
                        {
                            qyery_temp = "INSERT INTO NUOC(SOTK,MAKH) Values ('" + sotk + "','" + makh + "')";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                    }

                }
                ispdv = 1;
            }
        }

        private void lay_The()
        {
            DataTable dt_temp = new DataTable();
            String qyery_temp, filename = "";
            filename = frmMain.ddimport + "\\THE" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";            
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','VND','THE_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                
            }
               
           
            try
            {
                //Dua du lieu vao bang The
               dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {

                String makh = dt_temp.Rows[i][20].ToString().Replace("-", "");
                String ngaydk = "";
                if (dt_temp.Rows[i][33].ToString() != "")
                {
                    ngaydk = dt_temp.Rows[i][33].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][33].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][33].ToString().Substring(6, 4);
                }

                //Dua cac khach hang su dung the vao danh sach the
                if ((dt_temp.Rows[i][14].ToString() == "Normal") && (ngaydk != ""))
                {
                    try
                    {
                        qyery_temp = "INSERT INTO THE(MAKH,NGAYDK,HIENTRANG) Values ('" + makh + "','" + ngaydk + "',1)";
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
            }
            ispdv = 1;  
        }

        private void InsertSPDVCT()
        { 
            //Dua du lieu vao table SPDV
            //Xoa du lieu trong bang SPDV theo chi nhanh tinh diem
            DataTable dt = new DataTable();
            int sldichvu = 0;
            String strdvu = "";
            String makh = "";
            String sCommand = "";
            try
            {
                sCommand = "Delete SPDV where left(makh,4)='" + frmMain.cn + "'";
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
            }
            //Xoa du lieu trong table spdvct
            try
            {
                sCommand = "Delete SPDVCT where left(makh,4)='" + frmMain.cn + "' and thang ='"+dtpThang.Text+"'";
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
            }

            //Tinh diem su dung dich vu Dien

            dt.Clear();
            
            sCommand = "Select * from Dien where left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    sldichvu = 1;
                    strdvu = "Dien";
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + dt.Rows[i]["makh"].ToString() + "'," + sldichvu + ",'" + strdvu + "')";
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

                        sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + dt.Rows[i]["makh"].ToString() + "'";
                        frmMain.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        frmMain.conn.Close();
                    }

                }
            }
            //Tinh diem su dung TKTGTT          

            dt.Clear();

            sCommand = "Select distinct(makh) from SKTIENGUI where left(sotk,6) not like '____51' and left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'TKTGTT')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",TKTGTT";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }
            //Tinh diem su dung Tien vay         

            dt.Clear();

            sCommand = "Select distinct(makh) from SKTIENVAY where left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'TIENVAY')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",TIENVAY";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }

            //Tinh diem su dung SMSLOAN        

            dt.Clear();

            sCommand = "Select * from SMSLOAN where left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'SMSLOAN')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",SMSLOAN";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }
            //Tinh diem su dung ABIC       

            dt.Clear();

            sCommand = "Select distinct(makh) from ABIC where left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'ABIC')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",ABIC";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }

            //Tinh diem su dung dich vu VNPT
            dt.Clear();
            sCommand = "Select * from VNPT where left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'VNPT')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",VNPT";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }
            //Tinh diem su dung dich vu Nuoc
            dt.Clear();
            sCommand = "Select * from NUOC where left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'NUOC')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",NUOC";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }
            //Tinh diem su dung dich vu SMS
            dt.Clear();
            sCommand = "Select * from SMS where left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'SMS')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",SMS";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }

            //Tinh diem su dung dich vu The
            dt.Clear();
            //sCommand = "Select * from THE where ngaydk<='" + ngaytinhdiem + "' and left(makh,4)='" + frmMain.cn + "'";
            sCommand = "Select * from THE where left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'THE')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",THE";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }

            //Tinh diem su dung dich vu chuyen tien
            dt.Clear();
            sCommand = "Select distinct(makh) from Chuyentien where left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'Chuyentien')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",Chuyentien";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }
            //Tinh diem su dung chuyen tien nuoc ngoai      

            dt.Clear();

            sCommand = "Select distinct(makh)from Chuyentienn where left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'chuyentiennuocngoai')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",chuyentiennuocngoai";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }

            //Tinh diem su dung dich vu tra luong qua tai khoan            
            dt.Clear();
            sCommand = "Select makh from Chuyenluong where hientrang=1 and left(makh,4)='" + frmMain.cn + "'";
            //sCommand = "Select makh from Chuyenluong where hientrang=1 and ngaydk<='" + ngaytinhdiem + "' and left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'Chuyenluong')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",Chuyenluong";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }

                }
            }
            //Tinh diem su dung dich vu CMS/POS           
            dt.Clear();
            sCommand = "Select makh from CMSPOS where hientrang='True' and left(makh,4)='" + frmMain.cn + "'";
            //sCommand = "Select makh from Chuyenluong where hientrang=1 and ngaydk<='" + ngaytinhdiem + "' and left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'CMS_POS')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",CMS_POS";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }

                }
            }
            //Tinh diem su dung TK Hoc Duong        

            dt.Clear();

            sCommand = "Select distinct(makh) from SKTIENGUI where left(sotk,7)like '____510' and left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'TKHD')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",TKHD";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }
            //Tinh diem su dung TK An Sinh
            dt.Clear();

            sCommand = "Select distinct(makh) from SKTIENGUI where (left(sotk,7)like '____511' or left(sotk,7)like '____512') and left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'TKAS')";
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

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",TKAS";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
            }

            //Dua du lieu vao table SPDVCT
            dt.Clear();
            sCommand = "Select spdv.*,loaikh from SPDV,khachhang where spdv.makh=khachhang.makh and left(spdv.makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["makh"].ToString();
                    int loaikh = Convert.ToByte(dt.Rows[i]["loaikh"].ToString());
                    sldichvu = Convert.ToInt16(dt.Rows[i]["slspdv"].ToString());
                    strdvu = dt.Rows[i]["spdv"].ToString();
                    //Dua du lieu vao table SPDVCT
                    try
                    {
                        sCommand = "INSERT INTO SPDVCT(MAKH,THANG,SPDV,SLSPDV,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "','" + strdvu + "'," + sldichvu + "," + loaikh + ")";
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

                        sCommand = "Update SPDVCT set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "' and thang='"+dtpThang.Text+"'";
                        frmMain.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        frmMain.conn.Close();
                    }
                }
            }

        }
        private void lay_Profit()
        {
            
            String qyery_temp;            
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\PROFIT" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.XLS";
            
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','PROFIT_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                 
            }
                
            //Xoa du lieu dang co trong table Profit
            try
            {
                qyery_temp = "Update SDBQ set profitratio=0,profitvnd=0 where left(makh,4)='" + frmMain.cn + "' and ccy='" + CCY + "'";
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
            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }
                
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToDecimal(dt_temp.Rows[i][7].ToString().Replace("%","")) > 0)
                    {

                        String profit = dt_temp.Rows[i][7].ToString().Replace("%", "");
                        String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();
                        DataTable dt_a = new DataTable();
                        String scommand = "";
                        scommand = "select * from SDBQ where makh='" + makh + "' and ccy='VND'";
                        dt_a.Clear();
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        new SqlDataAdapter(scommand, frmMain.conn).Fill(dt_a);
                        frmMain.conn.Close();

                        if (dt_a.Rows.Count > 0)
                        {
                            qyery_temp = "UPDATE SDBQ set PROFITRATIO=" + Convert.ToDecimal(profit) + ",PROFITVND=" + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + " where makh ='" + makh + "' and ccy='" + CCY + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                        else
                        {
                            qyery_temp = "INSERT INTO SDBQ(MAKH,CCY,PROFITRATIO,PROFITVND) Values ('" + makh + "','" + CCY + "'," + Convert.ToDecimal(profit) + "," + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + ")";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }

                    }
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }

            }               
            //Import du lieu profit USD
            CCY = "USD";
            filename = frmMain.ddimport + "\\PROFIT" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "USD.XLS";

            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','PROFIT_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

            }

            //Xoa du lieu dang co trong table Profit
            try
            {
                qyery_temp = "Update SDBQ set profitratio=0,profitvnd=0 where left(makh,4)='" + frmMain.cn + "' and ccy='" + CCY + "'";
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
            try
            {
                dt_temp.Clear();
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) > 0)
                    {

                        String profit = dt_temp.Rows[i][7].ToString().Replace("%", "");
                        String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();
                        DataTable dt_a = new DataTable();
                        String scommand = "";
                        scommand = "select * from SDBQ where makh='" + makh + "' and ccy='USD'";
                        dt_a.Clear();
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        new SqlDataAdapter(scommand, frmMain.conn).Fill(dt_a);
                        frmMain.conn.Close();

                        if (dt_a.Rows.Count > 0)
                        {
                            qyery_temp = "INSERT INTO SDBQ(MAKH,CCY,PROFITRATIO,PROFITVND) Values ('" + makh + "','" + CCY + "'," + Convert.ToDecimal(profit) + "," + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + ")";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                        else
                        {
                            qyery_temp = "UPDATE SDBQ set PROFITRATIO=" + Convert.ToDecimal(profit) + ",PROFITVND=" + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + " where makh ='" + makh + "' and ccy='" + CCY + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                    }
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
           iprofit=1;
       }
        private void InsertProfitCT()
        { 
            DataTable dt = new DataTable();
            String makh = "";
            decimal profit = 0;
            byte loaikh;
            String sCommand = "SELECT SDBQ.MAKH,PROFITRATIO,KHACHHANG.LOAIKH from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH  and PROFITRATIO<=100 and left(SDBQ.MAKH,4)='" + frmMain.cn + "'";
            //So lieu thang 07/2012 profitvnd=0
            //sCommand = "SELECT SDBQ.MAKH,SUM(PROFITVND*PROFITRATIO)/SUM(PROFITVND) as PROFITRATIO,KHACHHANG.LOAIKH from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH and left(SDBQ.MAKH,4)='" + frmMain.cn + "' group by sdbq.makh,khachhang.loaikh having sum(profitvnd)>0";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                makh = dt.Rows[i]["MAKH"].ToString();
                loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                profit = Convert.ToDecimal(dt.Rows[i]["PROFITRATIO"].ToString());
                //Dua du lieu vao bang PROFITCT
                try
                {
                    sCommand = "INSERT INTO PROFITCT(MAKH,THANG,PROFIT,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + profit + "," + loaikh + ")";
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

                    sCommand = "Update PROFITCT set PROFIT =" + profit + " where makh='" + makh + "' and thang ='"+dtpThang.Text+"'";
                    frmMain.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    frmMain.conn.Close();
                }
            }
        }
        private void lay_Luong()
        {

            DataTable dt_temp = new DataTable();
            String qyery_temp, filename = "";
            filename = frmMain.ddimport + "\\LUONG" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
           
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','VND','LUONG_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //MessageBox.Show("Du lieu nay da duoc import!");   
            }              
            //Xoa du lieu dang co trong table LUONG
            try
            {
                qyery_temp = "Delete CHUYENLUONG where left(makh,4)='" + frmMain.cn + "'";
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
            //Dua du lieu vao bang CHUYENLUONG
            try
            {
               dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {

                String makh = dt_temp.Rows[i][0].ToString();
                String ngaydk = "", ngaykt = "";
                if (dt_temp.Rows[i][1].ToString() != "")
                {
                    ngaydk = dt_temp.Rows[i][1].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][1].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][1].ToString().Substring(6, 4);
                }

                if (dt_temp.Rows[i][2].ToString() != "")
                {
                    ngaykt = dt_temp.Rows[i][1].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][1].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][1].ToString().Substring(6, 4);
                }
                else
                {
                    ngaykt = "12/31/9998";
                }

                //Dua cac khach hang su dung the vao danh sach chuyenluong
                if (dt_temp.Rows[i][3].ToString() == "1")
                {
                    try
                    {
                        qyery_temp = "INSERT INTO CHUYENLUONG(MAKH,NGAYDK,NGAYKETTHUC,HIENTRANG) Values ('" + makh + "','" + ngaydk + "','" + ngaykt + "'," + Convert.ToInt16(dt_temp.Rows[i][3].ToString()) + ")";
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
             }
             ispdv = 1;
        }

        private void lay_WU()
        {
            String qyery_temp;
            DataTable dt_temp = new DataTable();
            String filename = "";
            filename = frmMain.ddimport + "\\WU" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            //Kiem tra du lieu da duoc import chua
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','VND','WU_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            //Xoa du lieu dang co trong table WU
            try
            {
                qyery_temp = "Delete WU where MaCN='" + frmMain.cn + "' and Thang='" + dtpThang.Text + "'";
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
            //Import du lieu WU VND
            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        String ID = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                        string hoten = dt_temp.Rows[i][14].ToString() + " " + dt_temp.Rows[i][13].ToString();
                        string diachi = dt_temp.Rows[i][15].ToString() + " " + dt_temp.Rows[i][17].ToString();
                        string ngaynhan = dt_temp.Rows[i][40].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][40].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][40].ToString().Substring(6, 4);
                        string hoten_gui = dt_temp.Rows[i][8].ToString() + " " + dt_temp.Rows[i][9].ToString();

                        string cmt = dt_temp.Rows[i][24].ToString();
                        DataTable dt1 = new DataTable();
                        String sCommand = "SELECT makh from SKTIENGUI where CMND='" + cmt + "'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();

                        String makh = "";
                        int j = dt1.Rows.Count;
                        if (j != 0)
                        {
                            makh = dt1.Rows[j - 1]["makh"].ToString();
                        }

                        try
                        {
                            if (makh != "")
                            {
                                qyery_temp = "INSERT INTO WU(ID,MAKH,HOTEN,DIACHI,CMND,SOTIEN,CCY,NGAYNHAN,MACN,THANG,DIENTHOAI,MTCN,HOTEN_GUI,SOTIEN_GUI,CCY_GUI) ";
                                qyery_temp += " Values ('" + ID + "','" + makh + "',N'" + hoten + "',N'" + diachi + "','" + dt_temp.Rows[i][24].ToString() + "','" + Convert.ToDecimal(dt_temp.Rows[i][39].ToString()) + "','" + dt_temp.Rows[i][33].ToString() + "','" + ngaynhan + "','" + frmMain.cn + "','" + dtpThang.Text + "','";
                                qyery_temp += dt_temp.Rows[i][20].ToString() + "','" + dt_temp.Rows[i][6].ToString() + "','" + hoten_gui + "','" + Convert.ToDecimal(dt_temp.Rows[i][28].ToString()) + "','" + dt_temp.Rows[i][32].ToString() + "')";
                            }
                            else
                            {
                                qyery_temp = "INSERT INTO WU(ID,MAKH,HOTEN,DIACHI,CMND,SOTIEN,CCY,NGAYNHAN,MACN,THANG,DIENTHOAI,MTCN,HOTEN_GUI,SOTIEN_GUI,CCY_GUI) ";
                                qyery_temp += " Values ('" + ID + "','" + frmMain.cn + "'+'000000000',N'" + hoten + "',N'" + diachi + "','" + dt_temp.Rows[i][24].ToString() + "','" + Convert.ToDecimal(dt_temp.Rows[i][39].ToString()) + "','" + dt_temp.Rows[i][33].ToString() + "','" + ngaynhan + "','" + frmMain.cn + "','" + dtpThang.Text + "','";
                                qyery_temp += dt_temp.Rows[i][20].ToString() + "','" + dt_temp.Rows[i][6].ToString() + "','" + hoten_gui + "','" + Convert.ToDecimal(dt_temp.Rows[i][28].ToString()) + "','" + dt_temp.Rows[i][32].ToString() + "')";
                            }            
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

                        if (makh == "")
                        {
                            try
                            {
                                makh = "T" + frmMain.cn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                qyery_temp = "INSERT INTO KHACHHANGCHUYENTIEN(MAKH,HOTEN,DIACHI1,CMND,MACN,GHICHU,TINHTRANG,LOAIKH) ";
                                qyery_temp += " Values ('" + makh + "',N'" + hoten + "','" + diachi + "','" + dt_temp.Rows[i][24].ToString() + "','" + frmMain.cn + "',N'Khách hàng nhận tiền WU',1,1)";
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
                    }
                    Thread.Sleep(1);
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
        }
                
        private void btnImport_Click(object sender, EventArgs e)
        {
            int Counter = 0;
            decimal perCounter;
            
            
            qyery_temp = "select tygia from tygia where thang=" + Convert.ToInt16(dtpThang.Text.Substring(0, 2)) + "";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(qyery_temp, frmMain.conn).Fill(dt_tygia);
            frmMain.conn.Close();
            if (dt_tygia.Rows.Count > 0)
            {
                tygia = Convert.ToDecimal(dt_tygia.Rows[0][0].ToString());
            }

            Cursor.Current = Cursors.WaitCursor;
            lay_SDBQ();
            if (isdbq == 1)
            {
                InsertSDBQNT();
                InsertSDBQCT();
            }
            Counter = Counter + 1;
            perCounter = (decimal)(Counter *100/15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width/15));
            label1.Refresh();
            
            //import sao ke tien gui de tinh thoi gian gui tien
            lay_SKTG();
            if (itggt == 1)
                InsertTGGTCT();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter *100/11);
            groupBox1.Text = ((int)(perCounter )).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width/11));
            label1.Refresh();

            //import sao ke tien gui de lay khach hang co tai khoan tien gui thanh toan
            lay_SKTG();
            if (itggt == 1)
                InsertSKTGTTCT();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / 15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 15));
            label1.Refresh();
            
            lay_SKTV();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / 15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 15));
            label1.Refresh();

            lay_Chuyentiendi();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter *100/15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width /15));

            lay_Chuyentienden();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter *100/15);
            groupBox1.Text = ((int)(perCounter )).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width /15));

            lay_SMS();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter *100/15);
            groupBox1.Text = ((int)(perCounter )).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width/15));

            lay_Dien();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter *100/15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 15));

            lay_VNPT();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter *100/15);
            groupBox1.Text = ((int)(perCounter )).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 15));

            lay_NUOC();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / 15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 15));

            lay_The();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter *100/15);
            groupBox1.Text = ((int)(perCounter )).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width/ 15));

            //lay_Profit();
            //if (iprofit == 1)
            //    InsertProfitCT();
            //Counter = Counter + 1;
            //perCounter = (decimal)(Counter *100/11);
            //groupBox1.Text = ((int)(perCounter )).ToString() + "%";
            //groupBox1.Refresh();
            //label1.Width = Convert.ToInt32(Counter * (groupBox1.Width/11));

            lay_Luong();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / 15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 15));

            lay_ABIC();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / 15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 15));

            lay_ChuyentiendenN();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / 15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 15));

            lay_ChuyentiendiN();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / 15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 15));

            lay_SMSLoan();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter *100/15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width /15));

            lay_WU();            

            if(ispdv==1)
                InsertSPDVCT();            

            Counter = Counter + 1;
            perCounter = (decimal)(Counter *100/15);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 15));

            Cursor.Current = Cursors.Default;
            MessageBox.Show("Đã import dữ liệu xong!");            
        }
               
        private void btnLichsu_Click(object sender, EventArgs e)
        {
            frmImport_Lichsu frmIM_LS = new frmImport_Lichsu();
            frmIM_LS.ShowDialog();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            InsertSDBQNT();
        }


       
        //Import ABic
        private void lay_ABIC()
        {
            String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\ABIC" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','ABIC_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                    // this.Close();

                }

            }

            //Xoa du lieu dang co trong table SaoKe
            try
            {
                qyery_temp = "Delete ABIC where left(makh,4)='" + frmMain.cn + "'";
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

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
                //this.Dispose();

            }

            for (int i = 7; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    string sohd = dt_temp.Rows[i][3].ToString();
                    if (sohd!="")
                    //if ((dt_temp.Rows[i][18].ToString().Substring(0, 3) != "670") && (dt_temp.Rows[i][18].ToString().Substring(0, 3) != "671"))
                    {
                        String sCommand = "SELECT distinct(MAKH) from sktienvay where sohd='" + sohd + "'";                                          
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        DataTable dt = new DataTable();
                        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                        frmMain.conn.Close();
                        String makh = dt.Rows[0][0].ToString();
                        dt.Clear();
                        
                        try
                        {
                            qyery_temp = "INSERT INTO ABIC(MAKH,SOHD,SOTIENBH,SOTIENKS) Values ('" + makh + "','" + sohd + "'," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) + "," + Convert.ToDecimal(dt_temp.Rows[i][8].ToString()) + ")";
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
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
            ispdv = 1;
        }

        
        //Import SMSLOAN
        private void lay_SMSLoan()
        {
            String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\SMSLOAN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','SMSLOAN_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                    // this.Close();

                }

            }                    

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
                //this.Dispose();

            }            

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    string makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();
                    if (makh != "")
                    //if ((dt_temp.Rows[i][18].ToString().Substring(0, 3) != "670") && (dt_temp.Rows[i][18].ToString().Substring(0, 3) != "671"))
                    {                                        
                        try
                        {
                            qyery_temp = "INSERT INTO SMSLOAN(MAKH,SOHD,SOGN) Values ('" + makh + "','" + dt_temp.Rows[i][3].ToString() + "','" + dt_temp.Rows[i][2].ToString() + "')";
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
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
            ispdv = 1;
        }

        //lay chuyen tien den ngoai te
        private void lay_ChuyentiendenN()
        {
            String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\FXNDEN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','FXNDEN_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            //Xoa du lieu trong table CHUYENTIEN
            try
            {
                qyery_temp = "DELETE CHUYENTIENN WHERE LOAICHUYENTIEN=1 and left(makh,4)='" + frmMain.cn + "' and thang='" + dtpThang.Text + "'";
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
            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;

            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
                        String makh = dt_temp.Rows[i][11].ToString();
                        //Dua cac khach hang chuyen tien da co makh va danh sach chuyen tien de tinh diem
                        if (makh != "000000000")
                        {
                            makh = frmMain.cn + makh;
                            DataTable dt1 = new DataTable();

                            String sCommand = "SELECT hoten,diachi1,dienthoai1,cmnd from khachhang where makh='" + makh + "'";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            dt1.Clear();
                            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
                            frmMain.conn.Close();                            
                            try
                            {
                                qyery_temp = "INSERT INTO CHUYENTIENN(MAKH,LOAINT,SOTIEN,LOAICHUYENTIEN,THANG,HOTEN,DIACHI,SDT,CMND) Values ('" + makh + "','" + dt_temp.Rows[i][2].ToString() + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",1,'" + dtpThang.Text + "',N'" + dt1.Rows[0]["HOTEN"].ToString() + "',N'" + dt1.Rows[0]["DIACHI1"].ToString() + "','" + dt1.Rows[0]["DIENTHOAI1"].ToString() + "','" + dt1.Rows[0]["CMND"].ToString() + "')";
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
                        else
                        {
                            makh = frmMain.cn + makh;
                            try
                            {
                                qyery_temp = "INSERT INTO CHUYENTIENN(MAKH,LOAINT,SOTIEN,LOAICHUYENTIEN,THANG,HOTEN) Values ('" + makh + "','" + dt_temp.Rows[i][2].ToString() + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",1,'" + dtpThang.Text + "','" + dt_temp.Rows[i][6].ToString() + "')";
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
                    }
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                Thread.Sleep(1);
            }
            ispdv = 1;
        }
        private void lay_ChuyentiendiN()
        {
            String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\FXNDI" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + frmMain.cn + "','" + CCY + "','FXNDI_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            //Xoa du lieu trong table CHUYENTIEN
            try
            {
                qyery_temp = "DELETE CHUYENTIENN WHERE LOAICHUYENTIEN=2 and left(makh,4)='" + frmMain.cn + "'";
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
            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;

            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {

                        String makh = dt_temp.Rows[i][2].ToString();
                        //Dua cac khach hang chuyen tien da co makh va danh sach chuyen tien de tinh diem
                        if (makh != "000000000")
                        {

                            makh = frmMain.cn + makh;
                            try
                            {
                                qyery_temp = "INSERT INTO CHUYENTIENN(MAKH,LOAINT,SOTIEN,LOAICHUYENTIEN) Values ('" + makh + "','" + dt_temp.Rows[i][5].ToString() + "'," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) + ",2)";
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
                    }
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
                Thread.Sleep(1);
            }
            ispdv = 1;
        }
        //Lay sao ke tien vay
        private void lay_SKTV()
        {
            String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\SKVAY" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
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

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;

            }
            //dt_temp = read_excel(filename);

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
            //MessageBox.Show("Đã import dữ liệu xong!");

            Cursor.Current = Cursors.Default;
        }
        //Import sao ke tien vay chi tiet
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

    }
}