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
    public partial class frmHT_ImportKH : Form
    {
        MicrosoftExcelClient m_ExcelClient = null;
        private DataTable dtResult = new DataTable();
        //string strCmd = "";
        //String qyery_temp;
        String filename = "";
        public frmHT_ImportKH()
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
        //private DataTable read_excel(String file_excel)
        //{
        //    Excel.Application ExcelObj = new Excel.Application();

        //    Excel.Workbook theWorkbook = null;



        //    theWorkbook = ExcelObj.Workbooks.Open(file_excel, Missing.Value, Missing.Value, Missing.Value
        //                                          , Missing.Value, Missing.Value, Missing.Value, Missing.Value
        //                                         , Missing.Value, Missing.Value, Missing.Value, Missing.Value
        //                                        , Missing.Value, Missing.Value, Missing.Value);
        //    Excel.Sheets sheets = theWorkbook.Worksheets;

        //    Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);//Get the reference of second worksheet

        //    //MessageBox.Show(worksheet.Name);//Get the name of worksheet.
        //    this.m_ExcelClient = new MicrosoftExcelClient(file_excel);



        //    //Reset & Reopen Connection
        //    this.m_ExcelClient.openConnection();

        //    //Update the message window
        //    //this.updateMessageWindow(1);

        //    DataTable result = this.m_ExcelClient.readEntireSheet(worksheet.Name);
        //    this.m_ExcelClient.closeConnection();

        //    //ExcelObj.Quit();

        //    return result;
        //}

        private DataTable read_excel(String file_excel)
        {
            Excel.Application ExcelObj = new Excel.Application();

            Excel.Workbook theWorkbook = null;



            theWorkbook = ExcelObj.Workbooks.Open(file_excel, Missing.Value, Missing.Value, Missing.Value
                                                  , Missing.Value, Missing.Value, Missing.Value, Missing.Value
                                                 , Missing.Value, Missing.Value, Missing.Value, Missing.Value
                                                , Missing.Value, Missing.Value, Missing.Value);
            Excel.Sheets sheets = theWorkbook.Worksheets;

            Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);
            //Get the reference of second worksheet

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

        private void btnImport_Click(object sender, EventArgs e)
        {
            int Counter = 0;
            decimal perCounter;
            frmMain.conn.Open();
           
            lay_KHCN();
            Cursor.Current = Cursors.Default;
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 10);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));
            label1.Refresh();

            lay_KHDNTN();
            Cursor.Current = Cursors.Default;
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 10);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));
            label1.Refresh();

            lay_KHHGD();
            lay_KHHTX();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 10);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));


            lay_KHCTTNHH();
            lay_KHCTCP();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 10);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));

            
            

            lay_KHCTLD();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 10);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));

            lay_KHDNDTNN();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 10);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));

            lay_KHDNNN();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 10);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));

            lay_KHTCTC();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 10);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));

            lay_KHTCXH();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 10);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));

            lay_KHTC();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 10);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));
            MessageBox.Show("Đã import dữ liệu xong!");
            frmMain.conn.Close();
        }

        private void lay_KHCN()
        {
            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFCN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";           
            
            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3,4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Cá nhân','" + strngaytao + "','14')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "',loaikh="+loaikh+" where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }                  
        }
        private void lay_KHDNTN()
        {

            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFDNTN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh,doituongdn) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Doanh nghiệp tư nhân','"+strngaytao+"','23','2')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
        }
        private void lay_KHHGD()
        {

            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFHGD" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
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
                        //if ((dt_temp.Rows[i][7].ToString() == "Cá nhân")||(dt_temp.Rows[i][7].ToString() =="Hộ gia đình"))
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Hộ gia đình','"+strngaytao+"','13')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
        }
        private void lay_KHHTX()
        {

            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFHTX" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Hợp tác xã','"+strngaytao+"','24')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
        }

        private void lay_KHCTCP()
        {

            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFCTCP" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Công ty cổ phần','"+strngaytao+"','21')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
        }
        private void lay_KHCTTNHH()
        {

            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFCTTNHH" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                       
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Công ty TNHH','"+strngaytao+"','21')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
        }
        private void lay_KHCTLD()
        {

            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFCTLD" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Công ty liên doanh','"+strngaytao+"','21')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
        }

        private void lay_KHDNDTNN()
        {

            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFDNDTNN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Doanh nghiệp có vốn ĐT nước ngoài','"+strngaytao+"','21')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
        }
        private void lay_KHDNNN()
        {

            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFDNNN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Doanh nghiệp Nhà nước','"+strngaytao+"','41')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
        }
        private void lay_KHTCTC()
        {

            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFTCTC" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Tổ chức Tài chính','"+strngaytao+"','41')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
        }
        private void lay_KHTCXH()
        {

            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFTCXH" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Tổ chức XH TƯ & Địa phương','"+strngaytao+"','34')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
        }
        private void lay_KHTC()
        {

            String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFTC" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";

            try
            {
                dt_temp = read_excel(filename);
            }
            catch
            {
                MessageBox.Show("Không đọc được file " + filename);
            }

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        
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
                            String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                            qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Tổ chức','"+strngaytao+"','35')";
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
                            qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(qyery_temp, frmMain.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }

                    }
                }
                catch
                { }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}