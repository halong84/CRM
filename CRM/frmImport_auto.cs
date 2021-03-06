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
//using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
//using N_MicrosoftExcelClient;
using ExcelDataReader;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmImport_auto : Form
    {
        SDBQBUS sdbq_bus = new SDBQBUS();
        SDBQCTBUS sdbqct_bus = new SDBQCTBUS();
        SDBQNTBUS sdbqnt_bus = new SDBQNTBUS();
        SKTIENGUIBUS sktg_bus = new SKTIENGUIBUS();
        TAIKHOANBUS tk_bus = new TAIKHOANBUS();
        TGGTCTDALBUS tggtct_bus = new TGGTCTDALBUS();
        SKTGTTCTBUS sktgttct_bus = new SKTGTTCTBUS();
        CHUYENTIENBUS chuyentien_bus = new CHUYENTIENBUS();
        KhachHangChuyenTienBUS khct_bus = new KhachHangChuyenTienBUS();
        SMSBUS sms_bus = new SMSBUS();
        DIENBUS dien_bus = new DIENBUS();
        THEBUS the_bus = new THEBUS();
        SPDVBUS spdv_bus = new SPDVBUS();
        SPDVCTBUS spdvct_bus = new SPDVCTBUS();
        PROFITCTBUS profitct_bus = new PROFITCTBUS();
        CHUYENLUONGBUS chuyenluong_bus = new CHUYENLUONGBUS();
        WUBUS wu_bus = new WUBUS();
        ABICBUS abic_bus = new ABICBUS();
        SMSLOANBUS smsloan_bus = new SMSLOANBUS();
        CHUYENTIENNBUS chuyentienn_bus = new CHUYENTIENNBUS();
        SKTIENVAYBUS sktienvay_bus = new SKTIENVAYBUS();
        SKTIENVAYCTBUS sktienvayct_bus = new SKTIENVAYCTBUS();

        //Excel.Application ExcelObj = null;
        //MicrosoftExcelClient m_ExcelClient = null;
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
        
        private void frmImport_auto_Load(object sender, EventArgs e)
        {
           
            //dtpThang.Value = dtCurrentTime.AddMonths(-1);
        }

        public DataTable read_excel(string excel_path)
        {
            DataTable dt = new DataTable();
            var file = new FileInfo(excel_path);
            if (File.Exists(excel_path))
            {
                using (
                var stream = File.Open(excel_path, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader reader;

                    if (file.Extension.Equals(".xls") || file.Extension.Equals(".XLS"))
                        reader = ExcelDataReader.ExcelReaderFactory.CreateBinaryReader(stream);
                    else if (file.Extension.Equals(".xlsx") || file.Extension.Equals(".XLSX"))
                        reader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(stream);
                    else
                        throw new Exception("Invalid FileName");

                    //// reader.IsFirstRowAsColumnNames
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };

                    var dataSet = reader.AsDataSet(conf);
                    dt = dataSet.Tables[0];
                }
            }
            else dt = null;
            return dt;
        }

        //private DataTable read_excel(String file_excel)
        //{
        //    //Excel.Application ExcelObj = new Excel.Application();
        //    Excel.Workbook theWorkbook = null;
        //    try
        //    {
        //        if (ExcelObj == null)
        //        {
        //            ExcelObj = new Excel.Application();
        //        }

        //        //Excel.Workbook theWorkbook = null;

        //        Excel.Workbooks theWorkbooks = ExcelObj.Workbooks;

        //        theWorkbook  = theWorkbooks.Open(file_excel, Missing.Value, Missing.Value, Missing.Value
        //                                              , Missing.Value, Missing.Value, Missing.Value, Missing.Value
        //                                             , Missing.Value, Missing.Value, Missing.Value, Missing.Value
        //                                            , Missing.Value, Missing.Value, Missing.Value);
        //        Excel.Sheets sheets = theWorkbook.Worksheets;

        //        Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);//Get the reference of second worksheet

        //        //MessageBox.Show(worksheet.Name);//Get the name of worksheet.
        //        this.m_ExcelClient = new MicrosoftExcelClient(file_excel);



        //        //Reset & Reopen Connection
        //        this.m_ExcelClient.openConnection();

        //        //Update the message window
        //        //this.updateMessageWindow(1);

        //        DataTable result = this.m_ExcelClient.readEntireSheet(worksheet.Name);
        //        this.m_ExcelClient.closeConnection();

        //        //Đóng file excel
        //        if (theWorkbook != null)
        //        theWorkbook.Close(0);
        //        //ExcelObj.Quit();

        //        return result;
        //    }
        //    catch
        //    {
        //        //this.m_ExcelClient.closeConnection();
        //        if (theWorkbook != null)
        //        theWorkbook.Close(0);
        //        return null;
        //    }       
        //}

        
        private void lay_SDBQ()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[6] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("CCY", typeof(string)), 
                    new DataColumn("SOTIEN",typeof(decimal)), 
                    new DataColumn("SOTIENHOA",typeof(decimal)),
                    new DataColumn("PROFITRATIO",typeof(float)),
                    new DataColumn("PROFITVND",typeof(decimal)),
                }
            );
            DataTable dt_temp3;
            String qyery_temp;
            DataTable dt_temp = new DataTable();
            String filename = "", CCY = "VND";            
            filename = frmMain.ddimport+ "\\SDBQ" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.xls";
            //Kiem tra du lieu da duoc import chua
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','SDBQ_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File "+filename+ " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            //Xoa du lieu dang co trong table SDBQ
            try
            {
                qyery_temp = "Delete SDBQ where left(makh,4)='" + Thongtindangnhap.macn + "' and ccy='" + CCY + "'";
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
            //Import du lieu so du binh quan VND

            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }
            //try
            //{
            //  dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

            //for (int i = 0; i < dt_temp.Rows.Count; i++)
            //{
            //    try
            //    {
            //        if (dt_temp.Rows[i][0].ToString() != null)
            //        {

            //            String sdu = dt_temp.Rows[i][4].ToString().Replace(",", "");
            //            String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();

            //            try
            //            {
            //                qyery_temp = "INSERT INTO SDBQ(MAKH,CCY,SOTIEN,SOTIENHOA) Values ('" + makh + "','" + CCY + "'," + Convert.ToDecimal(sdu) + "," + Convert.ToDecimal(sdu) + ")";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //            catch
            //            {
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //            }
            //        }
            //    }
            //    catch
            //    {
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }
            //    }
            //}
            

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {

                        String sdu = dt_temp.Rows[i][4].ToString().Replace(",", "");
                        String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();

                        dt_temp2.Rows.Add(makh, CCY, Convert.ToDecimal(sdu), Convert.ToDecimal(sdu),0,0);
                        //try
                        //{
                        //    qyery_temp = "INSERT INTO SDBQ(MAKH,CCY,SOTIEN,SOTIENHOA) Values ('" + makh + "','" + CCY + "'," + Convert.ToDecimal(sdu) + "," + Convert.ToDecimal(sdu) + ")";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //catch
                        //{
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //}
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
            
            dt_temp3 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");
            if (sdbq_bus.Update_SDBQ(dt_temp3))
            {
                //MessageBox.Show("Nhập dữ liệu số dư bình quân VND tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu số dư bình quân VND tháng " + dtpThang.Text);
            }

            //Xóa dữ liệu trong dt_temp2, dt_temp3
            dt_temp2.Clear();
            dt_temp3.Clear();
            filename = frmMain.ddimport+ "\\SDBQ" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "USD.xls";
            CCY="USD";

            //Xoa du lieu dang co trong table SDBQ
            try
            {
                qyery_temp = "Delete SDBQ where left(makh,4)='" + Thongtindangnhap.macn + "' and ccy='" + CCY + "'";
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
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','SDBQ_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }                
            }
            //
            //Import du lieu so du binh quan USD
            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp.Clear();
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        String sdu = dt_temp.Rows[i][4].ToString().Replace(",", "");
                        String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();

                        dt_temp2.Rows.Add(makh, CCY, Convert.ToDecimal(sdu), Convert.ToDecimal(sdu) * tygia, 0, 0);
                    }
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }

                //try
                //{
                //    if (dt_temp.Rows[i][0].ToString() != null)
                //    {

                //        String sdu = dt_temp.Rows[i][4].ToString().Replace(",", "");
                //        String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();

                //        try
                //        {
                //            qyery_temp = "INSERT INTO SDBQ(MAKH,CCY,SOTIEN,SOTIENHOA) Values ('" + makh + "','" + CCY + "'," + Convert.ToDecimal(sdu) + "," + Convert.ToDecimal(sdu) * tygia + ")";
                //            if (DataAccess.conn.State == ConnectionState.Open)
                //            {
                //                DataAccess.conn.Close();
                //            }
                //            DataAccess.conn.Open();
                //            frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                //            frmMain.myCommand.ExecuteNonQuery();
                //            DataAccess.conn.Close();
                //        }
                //        catch
                //        {
                //            if (DataAccess.conn.State == ConnectionState.Open)
                //            {
                //                DataAccess.conn.Close();
                //            }
                //        }
                //    }
                //}
                //catch
                //{
                //    if (DataAccess.conn.State == ConnectionState.Open)
                //    {
                //        DataAccess.conn.Close();
                //    }
                //}
            }
            dt_temp3 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");
            if (sdbq_bus.Update_SDBQ(dt_temp3))
            {
                //MessageBox.Show("Nhập dữ liệu số dư bình quân VND tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu số dư bình quân VND tháng " + dtpThang.Text);
            }
            isdbq=1;
       }
        //Dua du lieu vao table SDBQCT
        private void InsertSDBQCT()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[8] 
                { 
                    new DataColumn("THANG", typeof(string)),
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("SDBQ", typeof(decimal)),
                    new DataColumn("MACT", typeof(string)),
                    new DataColumn("DIEM", typeof(int)),
                    new DataColumn("TYTRONG", typeof(int)),
                    new DataColumn("THUCDIEM", typeof(int)),
                    new DataColumn("LOAIKH", typeof(int))
                }
            );
            DataTable dt = new DataTable();
            String sCommand = "SELECT SDBQ.MAKH,KHACHHANG.LOAIKH,sum(SDBQ.SOTIENHOA) as SOTIENHOA from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH and left(SDBQ.MAKH,4)='" + Thongtindangnhap.macn + "'group by sdbq.makh, khachhang.loaikh Order by SOTIENHOA desc";
            String makh = "";
            Byte loaikh = 1;
            int i = 0;
            decimal sdbq = 0;
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            for (i = 0; i < dt.Rows.Count; i++)
            {             
                sdbq = Convert.ToDecimal(dt.Rows[i]["SOTIENHOA"].ToString());
                makh = dt.Rows[i]["MAKH"].ToString();
                loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());

                dt_temp2.Rows.Add(dtpThang.Text,makh,sdbq,"",0,0,0,loaikh);

                //Dua du lieu vao bang sdbqct
                //try
                //{
                //    sCommand = "INSERT INTO SDBQCT(MAKH,THANG,SDBQ,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + sdbq + "," + loaikh + ")";
                //    if (DataAccess.conn.State == ConnectionState.Open)
                //    {
                //        DataAccess.conn.Close();
                //    }
                //    DataAccess.conn.Open();
                //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                //    frmMain.myCommand.ExecuteNonQuery();
                //    DataAccess.conn.Close();
                //}
                //catch
                //{
                //    if (DataAccess.conn.State == ConnectionState.Open)
                //    {
                //        DataAccess.conn.Close();
                //    }

                //    sCommand = "Update SDBQCT set SDBQ =" + sdbq + " where makh='" + makh + "' and thang='"+dtpThang.Text+"'";
                //    DataAccess.conn.Open();
                //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                //    frmMain.myCommand.ExecuteNonQuery();
                //    DataAccess.conn.Close();
                //}
            }
            if (sdbqct_bus.UPDATE_SDBQCT(dt_temp2))
            {
                //MessageBox.Show("Nhập dữ liệu SDBQCT tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập SDBQCT tháng " + dtpThang.Text + ".");
            }
        }

        //Dua du lieu vao table SDBQNT
        private void InsertSDBQNT()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[4] 
                {    
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("THANG", typeof(string)),
                    new DataColumn("LOAITIEN", typeof(string)),
                    new DataColumn("SDBQ", typeof(decimal)),               
                }
            );

            DataTable dt = new DataTable();
            String sCommand = "SELECT SDBQ.MAKH,CCY,SOTIEN from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH and left(SDBQ.MAKH,4)='" + Thongtindangnhap.macn + "'and KHACHHANG.LOAIKH=1";
                      
            int i = 0;
            
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                dt_temp2.Rows.Add(dt.Rows[i]["makh"].ToString(), dtpThang.Text, dt.Rows[i]["CCY"].ToString(), Convert.ToDecimal(dt.Rows[i]["SOTIEN"].ToString()));
               
                //Dua du lieu vao bang sdbqnt
                //try
                //{
                //    sCommand = "INSERT INTO SDBQNT(MAKH,THANG,LOAITIEN,SDBQ) Values ('" + dt.Rows[i]["makh"].ToString() + "','" + dtpThang.Text + "','" + dt.Rows[i]["CCY"].ToString() + "'," + Convert.ToDecimal(dt.Rows[i]["SOTIEN"].ToString()) + ")";
                //    if (DataAccess.conn.State == ConnectionState.Open)
                //    {
                //        DataAccess.conn.Close();
                //    }
                //    DataAccess.conn.Open();
                //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                //    frmMain.myCommand.ExecuteNonQuery();
                //    DataAccess.conn.Close();
                //}
                //catch
                //{
                //    if (DataAccess.conn.State == ConnectionState.Open)
                //    {
                //        DataAccess.conn.Close();
                //    }

                //    sCommand = "Update SDBQNT set SDBQ =" + Convert.ToDecimal(dt.Rows[i]["SOTIEN"].ToString()) + " where makh='" + dt.Rows[i]["makh"].ToString() + "' and thang='" + dtpThang.Text + "' and loaitien='" + dt.Rows[i]["CCY"].ToString() + "'";
                //    DataAccess.conn.Open();
                //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                //    frmMain.myCommand.ExecuteNonQuery();
                //    DataAccess.conn.Close();
                //}
            }
            if (sdbqnt_bus.UPDATE_SDBQNT(dt_temp2))
            {
                //MessageBox.Show("Nhập dữ liệu SDBQNT tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập SDBQNT tháng " + dtpThang.Text + ".");
            }
        }

        private void lay_SKTG()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[11] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("SOTK", typeof(string)),
                    new DataColumn("CCY", typeof(string)),
                    new DataColumn("NGAYMO", typeof(string)),
                    new DataColumn("NGAYDENHAN", typeof(string)),
                    new DataColumn("SOTIEN", typeof(decimal)),
                    new DataColumn("SOTIENHOA", typeof(decimal)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("THANG", typeof(byte)),
                    new DataColumn("DIEM", typeof(byte)),
                    new DataColumn("CMND", typeof(string)),
                }
            );
            DataRow dr;
            String qyery_temp;           
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport+"\\SK" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.xls";                         
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','SKTIENGUI_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                   // this.Close();
                    
                }
                
            }
                
            //Xoa du lieu dang co trong table SaoKe
            //try
            //{
            //    qyery_temp = "Delete SKTIENGUI where left(makh,4)='" + Thongtindangnhap.macn + "' and ccy='" + CCY + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //    //this.Dispose();
            //}

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
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = makh;
                        dr["SOTK"] = dt_temp.Rows[i][8].ToString();
                        dr["CCY"] = CCY;
                        dr["NGAYMO"] = ngaymo;
                        dr["NGAYDENHAN"] = ngaydh;
                        dr["SOTIEN"] = Convert.ToDecimal(dt_temp.Rows[i][6].ToString());
                        dr["SOTIENHOA"] = Convert.ToDecimal(dt_temp.Rows[i][6].ToString());
                        dr["MANV"] = dt_temp.Rows[i][37].ToString();
                        dr["THANG"] = 0;
                        dr["DIEM"] = 0;
                        dr["CMND"] = dt_temp.Rows[i][26].ToString();
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    qyery_temp = "INSERT INTO SKTIENGUI(MAKH,SOTK,CCY,NGAYMO,NGAYDENHAN,SOTIEN,SOTIENHOA,MANV) Values ('" + makh + "','" + dt_temp.Rows[i][8].ToString() + "','" + CCY + "','" + ngaymo + "','" + ngaydh + "'," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) + "," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) + ",'" + dt_temp.Rows[i][37].ToString() + "')";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //catch
                        //{
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //}
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
            if (sktg_bus.UPDATE_SKTIENGUI(dt_temp2,Thongtindangnhap.macn,"VND"))
            {
                //MessageBox.Show("Nhập sao kê tiền gửi VND tháng " + dtpThang.Text + " thành công.");
     
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập sao kê tiền gửi VND tháng " + dtpThang.Text);
            }
            dt_temp2.Clear();
            //Import sao ke USD
            filename = frmMain.ddimport + "\\SK" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "USD.xls";
            CCY = "USD";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','SKTIENGUI_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            //Xoa du lieu dang co trong table SaoKe
            //try
            //{
            //    qyery_temp = "Delete SKTIENGUI where left(makh,4)='" + Thongtindangnhap.macn + "' and ccy='" + CCY + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}
            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp.Clear();
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

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
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = makh;
                        dr["SOTK"] = dt_temp.Rows[i][8].ToString();
                        dr["CCY"] = CCY;
                        dr["NGAYMO"] = ngaymo;
                        dr["NGAYDENHAN"] = ngaydh;
                        dr["SOTIEN"] = Convert.ToDecimal(dt_temp.Rows[i][6].ToString());
                        dr["SOTIENHOA"] = Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) * tygia;
                        dr["MANV"] = dt_temp.Rows[i][37].ToString();
                        dr["THANG"] = 0;
                        dr["DIEM"] = 0;
                        dr["CMND"] = dt_temp.Rows[i][26].ToString();
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    qyery_temp = "INSERT INTO SKTIENGUI(MAKH,SOTK,CCY,NGAYMO,NGAYDENHAN,SOTIEN,SOTIENHOA,MANV) Values ('" + makh + "','" + dt_temp.Rows[i][8].ToString() + "','" + CCY + "','" + ngaymo + "','" + ngaydh + "'," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) + "," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) * tygia + ",'" + dt_temp.Rows[i][37].ToString() + "')";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //catch
                        //{
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //}
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
            if (sktg_bus.UPDATE_SKTIENGUI(dt_temp2, Thongtindangnhap.macn, "USD"))
            {
                //MessageBox.Show("Nhập sao kê tiền gửi USD tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập sao kê tiền gửi USD tháng " + dtpThang.Text);
            }

            //Cập nhật dữ liệu vào bảng tài khoản
            bool capnhat_taikhoan = tk_bus.UPDATE_TAIKHOAN();
            itggt = 1;
        }
        //Dua du lieu vao table TGGTCT
        private void InsertTGGTCT()
        {
            //Cap nhat so thang gui cua tung so tiet kiem
            //DataTable dt = new DataTable();
            //String makh = "";
            //byte loaikh;
            //int thang;
            ////String sCommand = "Update SKTIENGUI set Thang = datediff(month,ngaymo,ngaydenhan) where ngaydenhan <>'01/01/1900' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            ////if (DataAccess.conn.State == ConnectionState.Open)
            ////{
            ////    DataAccess.conn.Close();
            ////}
            ////DataAccess.conn.Open();
            ////frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            ////frmMain.myCommand.ExecuteNonQuery();
            ////DataAccess.conn.Close();

            //String sCommand = "SELECT SKTIENGUI.*,LOAIKH from SKTIENGUI,KHACHHANG where SKTIENGUI.MAKH=KHACHHANG.MAKH and SKTIENGUI.NGAYDENHAN<>'01/01/1900' and left(SKTIENGUI.MAKH,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    makh = dt.Rows[i]["MAKH"].ToString();
            //    loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
            //    thang = Convert.ToInt16(dt.Rows[i]["THANG"].ToString());
            //    //Dua du lieu vao bang TGGTCT
            //    try
            //    {
            //        String ngaymo = dt.Rows[i]["NGAYMO"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["NGAYMO"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["NGAYMO"].ToString().Substring(7, 4);
            //        String ngaydenhan = dt.Rows[i]["NGAYDENHAN"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["NGAYDENHAN"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["NGAYDENHAN"].ToString().Substring(7, 4);
            //        sCommand = "INSERT INTO TGGTCT(MAKH,THANG,SOTK,NGAYMO,NGAYDENHAN,THOIGIAN,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "','" + dt.Rows[i]["SOTK"].ToString() + "','" + ngaymo + "','" + ngaydenhan + "'," + thang + "," + loaikh + ")";
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }
            //        DataAccess.conn.Open();
            //        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //        frmMain.myCommand.ExecuteNonQuery();
            //        DataAccess.conn.Close();
            //    }
            //    catch
            //    {
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }

            //        sCommand = "Update TGGTCT set THOIGIAN=" + thang + " where sotk='" + dt.Rows[i]["SOTK"].ToString() + "' and Thang = '" + dtpThang.Text + "'";
            //        DataAccess.conn.Open();
            //        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //        frmMain.myCommand.ExecuteNonQuery();
            //        DataAccess.conn.Close();
            //    }
            //}
            if (tggtct_bus.UPDATE_TGGTCT(Thongtindangnhap.macn, dtpThang.Text))
            {
                //MessageBox.Show("Nhập dữ liệu TGGTCT tháng "+ dtpThang.Text+ " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu TGGTCT tháng " + dtpThang.Text);
            }
        }
        //Dua du lieu vao table SKTGTTCT
        private void InsertSKTGTTCT()
        {
            //Cap nhat so thang gui cua tung so tiet kiem
            //DataTable dt = new DataTable();
            
            //String sCommand = "delete SKTGTTCT where Thang ='"+dtpThang.Text+"' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //frmMain.myCommand.ExecuteNonQuery();
            //DataAccess.conn.Close();            
          
            //try
            //{
            //    sCommand = "insert into SKTGTTCT(MAKH,thang) select distinct(sktiengui.makh) as makh,'" + dtpThang.Text + "' as thang from sktiengui where left(sktiengui.makh,4)='" + Thongtindangnhap.macn + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close(); 
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }              
            //}
            if (sktgttct_bus.UPDATE_SKTGTTCT(Thongtindangnhap.macn, dtpThang.Text))
            {
                //MessageBox.Show("Nhập dữ liệu SKTGTTCT tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu SKTGTTCT tháng " + dtpThang.Text);
            }
           
        }
        private void lay_Chuyentiendi()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[10] 
                { 
                    new DataColumn("ID", typeof(string)),
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("SOTK", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("SOTIEN", typeof(decimal)),
                    new DataColumn("LOAICHUYENTIEN", typeof(byte)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("CCY", typeof(string)),
                    new DataColumn("SOTIENHOA", typeof(decimal)),
                    new DataColumn("MACN", typeof(string))
                }
            );
            DataRow dr2;

            DataTable dt_temp3 = new DataTable();
            dt_temp3.Columns.AddRange
            (
                new DataColumn[32] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(byte)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string))
                }
            );
            DataRow dr3;
            //String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport +"\\FXDI" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.xls";            
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','FXDI_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }                  
            }
                
            //Xoa du lieu trong table CHUYENTIEN
            //try
            //{
            //    qyery_temp = "DELETE CHUYENTIEN WHERE LOAICHUYENTIEN=1 and left(makh,4)='" + Thongtindangnhap.macn + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}
            String makh = "";
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        makh = dt_temp.Rows[i][10].ToString();
                        //Dua cac khach hang chuyen tien da co makh va danh sach chuyen tien de tinh diem
                        if (makh != "000000000")
                        {
                            String ID = Thongtindangnhap.macn+DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                            makh = Thongtindangnhap.macn + makh;
                            //qyery_temp = "INSERT INTO CHUYENTIEN(ID,MAKH,SOTIEN,LOAICHUYENTIEN,HOTEN,CCY,SOTIENHOA,MACN) Values ('" + ID + "','" + makh + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",1,N'" + dt_temp.Rows[i][5].ToString() + "','" + CCY + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",'" + Thongtindangnhap.macn + "')";
                            //if (DataAccess.conn.State == ConnectionState.Open)
                            //{
                            //    DataAccess.conn.Close();
                            //}
                            //DataAccess.conn.Open();
                            //frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                            //frmMain.myCommand.ExecuteNonQuery();
                            //DataAccess.conn.Close();
                            dr2 = dt_temp2.NewRow();
                            dr2["ID"] = ID;
                            dr2["MAKH"] = makh;
                            dr2["SOTK"] = "";
                            dr2["CMND"] = "";
                            dr2["SOTIEN"] = Convert.ToDecimal(dt_temp.Rows[i][3].ToString());
                            dr2["LOAICHUYENTIEN"] = 1;
                            dr2["HOTEN"] =  dt_temp.Rows[i][5].ToString();
                            dr2["CCY"] = CCY;
                            dr2["SOTIENHOA"] = Convert.ToDecimal(dt_temp.Rows[i][3].ToString());
                            dr2["MACN"] = Thongtindangnhap.macn;
                            dt_temp2.Rows.Add(dr2);
                        }
                        else
                        //Dua cac khach hang chua co makh vao danh sach khach hang tiem nang
                        {
                            if (dt_temp.Rows[i][43].ToString() != "")
                            {
                                if (Char.IsNumber(Convert.ToChar(dt_temp.Rows[i][43].ToString().Substring(0, 1))))
                                {
                                    makh = "T" + Thongtindangnhap.macn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                    dr3 = dt_temp3.NewRow();
                                    dr3["MAKH"] = makh;
                                    dr3["HOTEN"] = dt_temp.Rows[i][5].ToString();
                                    dr3["DIACHI1"] = dt_temp.Rows[i][41].ToString();
                                    dr3["DIACHI2"] = "";
                                    dr3["DIENTHOAI1"] = "";
                                    dr3["DIENTHOAI2"] = "";
                                    dr3["EMAIL"] = "";
                                    if (dt_temp.Rows[i][43].ToString().Length >= 12 && CommonMethod.IsDigitsOnly(dt_temp.Rows[i][43].ToString().Substring(0, 12)))
                                    {
                                        dr3["CMND"] = dt_temp.Rows[i][43].ToString().Substring(0, 12);
                                    }
                                    else
                                    {
                                        dr3["CMND"] = dt_temp.Rows[i][43].ToString().Substring(0, 9);
                                    }                                    
                                    dr3["NGAYCAP"] = "";
                                    dr3["NOICAP"] = "";
                                    dr3["NGAYSINH"] = "";
                                    dr3["GIOITINH"] = 1;
                                    dr3["LINHVUC"] = "";
                                    dr3["WEBSITE"] = "";
                                    dr3["GPDK"] = "";
                                    dr3["QDTL"] = "";
                                    dr3["MST"] = "";
                                    dr3["LOAIKH"] = 1;
                                    dr3["THUNHAP"] = 0;
                                    dr3["SOTHICH"] = "";
                                    dr3["MANV"] = "";
                                    dr3["NHGIAODICH"] = "";
                                    dr3["GHICHU"] = "Khách hàng giao dịch chuyển tiền đi";
                                    dr3["MACN"] = Thongtindangnhap.macn;
                                    dr3["TINHTRANG"] = 1;
                                    dr3["CTLOAIKH"] = "";
                                    dr3["TINH"] = "";
                                    dr3["HUYEN"] = "";
                                    dr3["XA"] = "";
                                    dr3["LOAIKH_IPCAS"] = "";
                                    dr3["NGAYKETHON"] = "";
                                    dr3["NGAYTHANHLAP"] = "";
                                    dt_temp3.Rows.Add(dr3);

                                    //try
                                    //{
                                    //    makh = "T" + Thongtindangnhap.macn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                    //    qyery_temp = "INSERT INTO KHACHHANGCHUYENTIEN(MAKH,HOTEN,DIACHI1,CMND,MACN,GHICHU,TINHTRANG,LOAIKH) Values ('" + makh + "',N'" + dt_temp.Rows[i][5].ToString() + "',N'" + dt_temp.Rows[i][41].ToString() + "','" + dt_temp.Rows[i][43].ToString().Substring(0, 9) + "','" + Thongtindangnhap.macn + "',N'Khách hàng giao dịch chuyển tiền',1,1)";
                                    //    if (DataAccess.conn.State == ConnectionState.Open)
                                    //    {
                                    //        DataAccess.conn.Close();
                                    //    }
                                    //    DataAccess.conn.Open();
                                    //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                    //    frmMain.myCommand.ExecuteNonQuery();
                                    //    DataAccess.conn.Close();
                                    //}
                                    //catch
                                    //{
                                    //    if (DataAccess.conn.State == ConnectionState.Open)
                                    //    {
                                    //        qyery_temp = "update KHACHHANGCHUYENTIEN set DIACHI1=N'" + dt_temp.Rows[i][41].ToString() + "' where CMND='" + dt_temp.Rows[i][43].ToString().Substring(0, 9) + "'";
                                    //        if (DataAccess.conn.State == ConnectionState.Open)
                                    //        {
                                    //            DataAccess.conn.Close();
                                    //        }
                                    //        DataAccess.conn.Open();
                                    //        frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                    //        frmMain.myCommand.ExecuteNonQuery();
                                    //        DataAccess.conn.Close();
                                    //    }
                                    //}
                                }
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
                Thread.Sleep(1);
            }
            if (dt_temp2.Rows.Count != 0)
            {
                if (chuyentien_bus.UPDATE_CHUYENTIEN(dt_temp2,1,Thongtindangnhap.macn,CCY))
                {
                    //MessageBox.Show("Nhập dữ liệu khách hàng chuyển tiền đi tháng " + dtpThang.Text +" vào bảng CHUYENTIEN thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu khách hàng chuyển tiền đi tháng " + dtpThang.Text + " vào bảng CHUYENTIEN.");
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu khách hàng chuyển tiền đi nào được nhập vào bảng CHUYENTIEN.");
            }
            if (dt_temp3.Rows.Count != 0)
            {
                DataTable dt_temp4;
                dt_temp4 = CommonMethod.RemoveDuplicateRows(dt_temp3, "CMND");
                if (khct_bus.UPDATE_KhachHangChuyenTien(dt_temp4))
                {
                    //MessageBox.Show("Nhập dữ liệu khách hàng chuyển tiền đi vào bảng KhachHangChuyenTien thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu KhachHangChuyenTien đối với khách hàng chuyển tiền đi.");
                }
            }
            else
            {
                //MessageBox.Show("Không có dữ liệu khách hàng chuyển tiền đi nào được nhập vào bảng KhachHangChuyenTien.");
            }
            ispdv = 1;
        }

        private void lay_Chuyentienden()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[10] 
                { 
                    new DataColumn("ID", typeof(string)),
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("SOTK", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("SOTIEN", typeof(decimal)),
                    new DataColumn("LOAICHUYENTIEN", typeof(byte)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("CCY", typeof(string)),
                    new DataColumn("SOTIENHOA", typeof(decimal)),
                    new DataColumn("MACN", typeof(string))
                }
            );
            DataRow dr2;

            DataTable dt_temp3 = new DataTable();
            dt_temp3.Columns.AddRange
            (
                new DataColumn[32] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI1", typeof(string)),
                    new DataColumn("DIACHI2", typeof(string)),
                    new DataColumn("DIENTHOAI1", typeof(string)),
                    new DataColumn("DIENTHOAI2", typeof(string)),
                    new DataColumn("EMAIL", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("NGAYCAP", typeof(string)),
                    new DataColumn("NOICAP", typeof(string)),
                    new DataColumn("NGAYSINH", typeof(string)),
                    new DataColumn("GIOITINH", typeof(bool)),
                    new DataColumn("LINHVUC", typeof(string)),
                    new DataColumn("WEBSITE", typeof(string)),
                    new DataColumn("GPDK", typeof(string)),
                    new DataColumn("QDTL", typeof(string)),
                    new DataColumn("MST", typeof(string)),
                    new DataColumn("LOAIKH", typeof(byte)),
                    new DataColumn("THUNHAP", typeof(decimal)),
                    new DataColumn("SOTHICH", typeof(string)),
                    new DataColumn("MANV", typeof(string)),
                    new DataColumn("NHGIAODICH", typeof(string)),
                    new DataColumn("GHICHU", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("TINHTRANG", typeof(bool)),
                    new DataColumn("CTLOAIKH", typeof(string)),
                    new DataColumn("TINH", typeof(string)),
                    new DataColumn("HUYEN", typeof(string)),
                    new DataColumn("XA", typeof(string)),
                    new DataColumn("LOAIKH_IPCAS", typeof(string)),
                    new DataColumn("NGAYKETHON", typeof(string)),
                    new DataColumn("NGAYTHANHLAP", typeof(string))
                }
            );
            DataRow dr3;
            String qyery_temp;            
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();       
            filename = frmMain.ddimport + "\\FXDEN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.xls";
           
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','FXDEN_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
               
            }
               
            //Xoa du lieu trong table CHUYENTIEN
            //try
            //{
            //    qyery_temp = "DELETE CHUYENTIEN WHERE LOAICHUYENTIEN=2 and left(makh,4)='" + Thongtindangnhap.macn + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }
            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

            String makh = "";
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        String benacc = dt_temp.Rows[i][7].ToString();
                        //Dua cac khach hang chuyen tien da co makh vao danh sach chuyen tien de tinh diem
                        if (benacc.Length >= 4 && benacc.Substring(0, 4) == Thongtindangnhap.macn)
                        {
                            String ID = Thongtindangnhap.macn+DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                            benacc = benacc.Replace(" ", "").Replace(".", "");
                            
                            //DataTable dt1 = new DataTable();
                            //String sCommand = "SELECT makh from SKTIENGUI where sotk='" + benacc + "'";
                            //if (DataAccess.conn.State == ConnectionState.Open)
                            //{
                            //    DataAccess.conn.Close();
                            //}
                            //DataAccess.conn.Open();
                            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                            //DataAccess.conn.Close();

                            ////String makh = "";
                            //int j = dt1.Rows.Count;
                            //if (j != 0)
                            //{
                            //    makh = dt1.Rows[j - 1]["makh"].ToString();
                            //}

                            dr2 = dt_temp2.NewRow();
                            dr2["ID"] = ID;
                            dr2["MAKH"] = "";
                            dr2["SOTK"] = benacc;
                            dr2["CMND"] = "";
                            dr2["SOTIEN"] = Convert.ToDecimal(dt_temp.Rows[i][3].ToString());
                            dr2["LOAICHUYENTIEN"] = 2;
                            dr2["HOTEN"] = dt_temp.Rows[i][6].ToString();
                            dr2["CCY"] = CCY;
                            dr2["SOTIENHOA"] = Convert.ToDecimal(dt_temp.Rows[i][3].ToString());
                            dr2["MACN"] = Thongtindangnhap.macn;
                            dt_temp2.Rows.Add(dr2);

                            //qyery_temp = "INSERT INTO CHUYENTIEN(ID,MAKH,SOTK,SOTIEN,LOAICHUYENTIEN,HOTEN,CCY,SOTIENHOA,MACN) Values ('" + ID + "','" + makh + "','" + benacc + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",2,N'" + dt_temp.Rows[i][6].ToString() + "','" + CCY + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) * tygia + ",'" + Thongtindangnhap.macn + "')";
                            //if (DataAccess.conn.State == ConnectionState.Open)
                            //{
                            //    DataAccess.conn.Close();
                            //}
                            //DataAccess.conn.Open();
                            //frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                            //frmMain.myCommand.ExecuteNonQuery();
                            //DataAccess.conn.Close();
                        }
                        else
                        //Dua cac khach hang chua co makh vao danh sach khach hang tiem nang
                        {
                            //if (dt_temp.Rows[i][47].ToString() != "")
                            if ((dt_temp.Rows[i][47].ToString() != "30204001") && (dt_temp.Rows[i][47].ToString().Substring(0, 2) == "30"))
                            {
                                if (dt_temp.Rows[i][60].ToString() != "")
                                {
                                    if (Char.IsNumber(Convert.ToChar(dt_temp.Rows[i][60].ToString().Substring(0, 1))))
                                    {
                                        makh = "T" + Thongtindangnhap.macn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                        dr3 = dt_temp3.NewRow();
                                        dr3["MAKH"] = makh;
                                        dr3["HOTEN"] = dt_temp.Rows[i][6].ToString();
                                        dr3["DIACHI1"] = dt_temp.Rows[i][54].ToString();
                                        dr3["DIACHI2"] = "";
                                        dr3["DIENTHOAI1"] = "";
                                        dr3["DIENTHOAI2"] = "";
                                        dr3["EMAIL"] = "";
                                        if (dt_temp.Rows[i][60].ToString().Length >= 12 && CommonMethod.IsDigitsOnly(dt_temp.Rows[i][60].ToString().Substring(0, 12)))
                                        {
                                            dr3["CMND"] = dt_temp.Rows[i][60].ToString().Substring(0, 12);
                                        }
                                        else
                                        {
                                            dr3["CMND"] = dt_temp.Rows[i][60].ToString().Substring(0, 9);
                                        }
                                        dr3["NGAYCAP"] = "";
                                        dr3["NOICAP"] = "";
                                        dr3["NGAYSINH"] = "";
                                        dr3["GIOITINH"] = 1;
                                        dr3["LINHVUC"] = "";
                                        dr3["WEBSITE"] = "";
                                        dr3["GPDK"] = "";
                                        dr3["QDTL"] = "";
                                        dr3["MST"] = "";
                                        dr3["LOAIKH"] = 1;
                                        dr3["THUNHAP"] = 0;
                                        dr3["SOTHICH"] = "";
                                        dr3["MANV"] = "";
                                        dr3["NHGIAODICH"] = dt_temp.Rows[i][47].ToString();
                                        dr3["GHICHU"] = "Khách hàng giao dịch chuyển tiền đến";
                                        dr3["MACN"] = Thongtindangnhap.macn;
                                        dr3["TINHTRANG"] = 1;
                                        dr3["CTLOAIKH"] = "";
                                        dr3["TINH"] = "";
                                        dr3["HUYEN"] = "";
                                        dr3["XA"] = "";
                                        dr3["LOAIKH_IPCAS"] = "";
                                        dr3["NGAYKETHON"] = "";
                                        dr3["NGAYTHANHLAP"] = "";
                                        dt_temp3.Rows.Add(dr3);
                                        //try
                                        //{
                                        //    String makh = "";
                                        //    makh = "T" + Thongtindangnhap.macn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                        //    qyery_temp = "INSERT INTO KHACHHANGCHUYENTIEN(MAKH,HOTEN,DIACHI1,CMND,NHGIAODICH,MACN,GHICHU,TINHTRANG,LOAIKH) Values ('" + makh + "',N'" + dt_temp.Rows[i][6].ToString() + "',N'" + dt_temp.Rows[i][54].ToString() + "','" + dt_temp.Rows[i][60].ToString().Substring(0, 9) + "','" + dt_temp.Rows[i][47].ToString() + "','" + Thongtindangnhap.macn + "',N'Khách hàng giao dịch chuyển tiền',1,1)";
                                        //    if (DataAccess.conn.State == ConnectionState.Open)
                                        //    {
                                        //        DataAccess.conn.Close();
                                        //    }
                                        //    DataAccess.conn.Open();
                                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                        //    frmMain.myCommand.ExecuteNonQuery();
                                        //    DataAccess.conn.Close();
                                        //}
                                        //catch
                                        //{
                                        //    if (DataAccess.conn.State == ConnectionState.Open)
                                        //    {
                                        //        qyery_temp = "Update KHACHHANGCHUYENTIEN set DIACHI1= N'" + dt_temp.Rows[i][54].ToString() + "' where CMND=N'" + dt_temp.Rows[i][60].ToString().Substring(0, 9) + "'";
                                        //        if (DataAccess.conn.State == ConnectionState.Open)
                                        //        {
                                        //            DataAccess.conn.Close();
                                        //        }
                                        //        DataAccess.conn.Open();
                                        //        frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                        //        frmMain.myCommand.ExecuteNonQuery();
                                        //        DataAccess.conn.Close();
                                        //    }
                                        //}
                                    }
                                }
                                
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
                Thread.Sleep(1);
            }
            if (dt_temp2.Rows.Count != 0)
            {
                if (chuyentien_bus.UPDATE_CHUYENTIEN(dt_temp2, 2, Thongtindangnhap.macn, CCY))
                {
                    //Update MAKH trong bảng CHUYENTIEN
                    bool update_chuyentien_makh = chuyentien_bus.UPDATE_CHUYENTIEN_MAKH();
                    //MessageBox.Show("Nhập dữ liệu khách hàng chuyển tiền đến vào bảng CHUYENTIEN thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu khách hàng chuyển tiền đến vào bảng CHUYENTIEN.");
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu khách hàng chuyển tiền đến nào được nhập vào bảng CHUYENTIEN.");
            }
            if (dt_temp3.Rows.Count != 0)
            {
                DataTable dt_temp4;
                dt_temp4 = CommonMethod.RemoveDuplicateRows(dt_temp3, "CMND");
                if (khct_bus.UPDATE_KhachHangChuyenTien(dt_temp4))
                {
                    //MessageBox.Show("Nhập dữ liệu khách hàng chuyển tiền đến vào bảng KhachHangChuyenTien thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu KhachHangChuyenTien đối với khách hàng chuyển tiền đến.");
                }
            }
            else
            {
                //MessageBox.Show("Không có dữ liệu khách hàng chuyển tiền đến nào được nhập vào bảng KhachHangChuyenTien.");
            }
            ispdv = 1;  
        }

        private void lay_SMS()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[3] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("NGAYDK", typeof(string)),
                    new DataColumn("HIENTRANG", typeof(byte))
                }
            );
            DataRow dr2;

            DataTable dt_temp = new DataTable();
            String qyery_temp, filename = "";
            filename = frmMain.ddimport + "\\SMS" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";
           
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','SMS_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }                        
            }

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }
            
            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {

                String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();
                String ngaydk = dt_temp.Rows[i][16].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][16].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][16].ToString().Substring(6, 4);
                //Dua cac khach hang su dung sms vao danh sach sms
                try
                {
                    if (makh.Substring(0, 4) == Thongtindangnhap.macn)
                    {
                        dr2 = dt_temp2.NewRow();
                        dr2["MAKH"] = makh;
                        dr2["NGAYDK"] = ngaydk;
                        dr2["HIENTRANG"] = 1;
                        dt_temp2.Rows.Add(dr2);
                        //qyery_temp = "INSERT INTO SMS(MAKH,NGAYDK,HIENTRANG) Values ('" + makh + "','" + ngaydk + "',1)";
                        //if (DataAccess.conn.State == ConnectionState.Open)
                        //{
                        //    DataAccess.conn.Close();
                        //}
                        //DataAccess.conn.Open();
                        //frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //frmMain.myCommand.ExecuteNonQuery();
                        //DataAccess.conn.Close();
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
            if (dt_temp2.Rows.Count != 0)
            {
                if (sms_bus.UPDATE_SMS(dt_temp2))
                {
                    //MessageBox.Show("Nhập dữ liệu khách hàng sử dụng dịch vụ SMS tháng " + dtpThang.Text + " thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu khách hàng sử dụng dịch vụ SMS tháng " + dtpThang.Text);
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu khách hàng sử dụng dịch vụ SMS nào được nhập trong tháng " + dtpThang.Text);
            }
            ispdv = 1; 
        }

        private void lay_Dien()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[2] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("SOTK", typeof(string)),
                }
            );
            DataRow dr2;
            DataTable dt_temp = new DataTable();
            String qyery_temp, filename = "";
            filename = frmMain.ddimport + "\\DIEN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";
            
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','DIEN_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }                
            }
                
            //Xoa du lieu dang co trong table DIEN
            //try
            //{
            //    qyery_temp = "Delete DIEN where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //   dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

            if (dt_temp.Rows.Count <= 0)
                MessageBox.Show("File DIEN khong co du lieu, de nghi xem lai!");
            else
            {
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    String sotk = dt_temp.Rows[i][5].ToString();
                    if (sotk.Length >=4 && sotk.Substring(0,4) == Thongtindangnhap.macn)
                    {
                        dr2 = dt_temp2.NewRow();
                        dr2["MAKH"] = "";
                        dr2["SOTK"] = sotk;
                        dt_temp2.Rows.Add(dr2);
                    }                 
                    //DataTable dt = new DataTable();
                    //String strCmd = "SELECT makh from SKTIENGUI where sotk='" + sotk + "'";
                    //if (DataAccess.conn.State == ConnectionState.Open)
                    //{
                    //    DataAccess.conn.Close();
                    //}
                    //DataAccess.conn.Open();
                    //new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                    //DataAccess.conn.Close();

                    //String makh = "";
                    //int j = dt.Rows.Count;
                    //if (j != 0)
                    //{
                    //    makh = dt.Rows[j - 1]["makh"].ToString();
                    //}

                    ////Dua cac khach hang su dung dich vu tra tien dien vao danh sach dien
                    //try
                    //{
                    //    if (makh.Substring(0, 4) == Thongtindangnhap.macn)
                    //    {
                    //        qyery_temp = "INSERT INTO DIEN(SOTK,MAKH) Values ('" + sotk + "','" + makh + "')";
                    //        if (DataAccess.conn.State == ConnectionState.Open)
                    //        {
                    //            DataAccess.conn.Close();
                    //        }
                    //        DataAccess.conn.Open();
                    //        frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                    //        frmMain.myCommand.ExecuteNonQuery();
                    //        DataAccess.conn.Close();
                    //    }
                    //}
                    //catch
                    //{
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //}
                }
                ispdv = 1;
            }
            if (dt_temp2.Rows.Count > 0)
            {
                DataTable dt_temp3 = CommonMethod.RemoveDuplicateRows(dt_temp2,"SOTK");
                if (dien_bus.UPDATE_DIEN(dt_temp3, Thongtindangnhap.macn))
                {
                    bool update_dien_makh = dien_bus.UPDATE_DIEN_MAKH();
                    //MessageBox.Show("Nhập dữ liệu khách hàng sử dụng điện tháng " + dtpThang.Text + " thành công");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu khách hàng sử dụng điện tháng " + dtpThang.Text);
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu khách hàng sử dụng điện nào được nhập mới trong tháng " + dtpThang.Text);
            }
        }

        private void lay_VNPT()
        {

            DataTable dt_temp = new DataTable();
            String qyery_temp, filename = "";
            filename = frmMain.ddimport + "\\VNPT" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";            
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','VNPT_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                
            }
                
            //Xoa du lieu dang co trong table VNPT
            try
            {
                qyery_temp = "Delete VNPT where left(makh,4)='" + Thongtindangnhap.macn + "'";
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

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

            if (dt_temp.Rows.Count <= 0)
                MessageBox.Show("File VNPT khong co du lieu, de nghi xem lai!");
            else
            {
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {

                    String sotk = dt_temp.Rows[i][5].ToString();
                    DataTable dt = new DataTable();
                    String strCmd = "SELECT makh from SKTIENGUI where sotk='" + sotk + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                    DataAccess.conn.Close();

                    String makh = "";
                    int j = dt.Rows.Count;
                    if (j != 0)
                    {
                        makh = dt.Rows[j - 1]["makh"].ToString();
                    }

                    //Dua cac khach hang su dung dich vu tra tien dien vao danh sach dien
                    try
                    {
                        if (makh.Substring(0, 4) == Thongtindangnhap.macn)
                        {
                            qyery_temp = "INSERT INTO VNPT(SOTK,MAKH) Values ('" + sotk + "','" + makh + "')";
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
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
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
            filename = frmMain.ddimport + "\\NUOC" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','NUOC_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

            }

            //Xoa du lieu dang co trong table NUOC
            try
            {
                qyery_temp = "Delete NUOC where left(makh,4)='" + Thongtindangnhap.macn + "'";
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

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

            if (dt_temp.Rows.Count <= 0)
                MessageBox.Show("File NUOC khong co du lieu, de nghi xem lai!");
            else
            {
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {

                    String sotk = dt_temp.Rows[i][5].ToString();
                    DataTable dt = new DataTable();
                    String strCmd = "SELECT makh from SKTIENGUI where sotk='" + sotk + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                    DataAccess.conn.Close();

                    String makh = "";
                    int j = dt.Rows.Count;
                    if (j != 0)
                    {
                        makh = dt.Rows[j - 1]["makh"].ToString();
                    }

                    //Dua cac khach hang su dung dich vu tra tien nuoc vao danh sach nuoc
                    try
                    {
                        if (makh.Substring(0, 4) == Thongtindangnhap.macn)
                        {
                            qyery_temp = "INSERT INTO NUOC(SOTK,MAKH) Values ('" + sotk + "','" + makh + "')";
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
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                    }

                }
                ispdv = 1;
            }
        }

        private void lay_The()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[3] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("NgayDK", typeof(string)),
                    new DataColumn("HienTrang", typeof(byte))
                }
            );
            DataRow dr2;
            DataTable dt_temp = new DataTable();
            String qyery_temp, filename = "";
            filename = frmMain.ddimport + "\\THE" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";            
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','THE_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                
            }

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    //Dua du lieu vao bang The
            //   dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

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
                    dr2 = dt_temp2.NewRow();
                    dr2["MAKH"] = makh;
                    dr2["NgayDK"] = ngaydk;
                    dr2["HienTrang"] = 1;
                    dt_temp2.Rows.Add(dr2);
                    //try
                    //{
                    //    qyery_temp = "INSERT INTO THE(MAKH,NGAYDK,HIENTRANG) Values ('" + makh + "','" + ngaydk + "',1)";
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //    DataAccess.conn.Open();
                    //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                    //    frmMain.myCommand.ExecuteNonQuery();
                    //    DataAccess.conn.Close();
                    //}
                    //catch
                    //{
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //}
                }
            }
            if (dt_temp2.Rows.Count != 0)
            {
                DataTable dt_temp3 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");
                if (the_bus.UPDATE_THE(dt_temp3))
                {
                    //MessageBox.Show("Nhập dữ liệu khách hàng sử dụng dịch vụ thẻ tháng " + dtpThang.Text + " thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi nhập dữ liệu khách hàng sử dụng dịch vụ thẻ tháng " + dtpThang.Text);
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu khách hàng sử dụng dịch vụ thẻ nào được nhập trong tháng " + dtpThang.Text);
            }

            ispdv = 1;  
        }

        private void InsertSPDVCT()
        { 
            //Dua du lieu vao table SPDV
            //Xoa du lieu trong bang SPDV theo chi nhanh tinh diem
            DataTable dt = new DataTable();
            //int sldichvu = 0;
            //String strdvu = "";
            //String makh = "";
            //String sCommand = "";
            //Xoa du lieu trong table spdv
            spdv_bus.DELETE_SPDV(Thongtindangnhap.macn);
            //try
            //{
            //    sCommand = "Delete SPDV where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}
            //Xoa du lieu trong table spdvct
            spdvct_bus.DELETE_SPDVCT(Thongtindangnhap.macn, dtpThang.Text);
            //try
            //{
            //    sCommand = "Delete SPDVCT where left(makh,4)='" + Thongtindangnhap.macn + "' and thang ='"+dtpThang.Text+"'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            //Tinh diem su dung dich vu Dien

            if (spdv_bus.UPDATE_SPDV_DIEN(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ điện tháng "+ dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ điện tháng " + dtpThang.Text + " vào bảng SPDV");
            }
            //dt.Clear();
            
            //sCommand = "Select * from Dien where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        sldichvu = 1;
            //        strdvu = "Dien";
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + dt.Rows[i]["makh"].ToString() + "'," + sldichvu + ",'" + strdvu + "')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + dt.Rows[i]["makh"].ToString() + "'";
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }

            //    }
            //}
            //Tinh diem su dung TKTGTT          

            
            if (spdv_bus.UPDATE_SPDV_SKTIENGUI(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ tiền gửi thanh toán tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ tiền gửi thanh toán tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();
            //sCommand = "Select distinct(makh) from SKTIENGUI where left(sotk,6) not like '____51' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
                    
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'TKTGTT')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",TKTGTT";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}
            //Tinh diem su dung Tien vay         

            if (spdv_bus.UPDATE_SPDV_SKTIENVAY(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ tiền vay tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ tiền vay tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();

            //sCommand = "Select distinct(makh) from SKTIENVAY where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'TIENVAY')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",TIENVAY";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}

            //Tinh diem su dung SMSLOAN        

            if (spdv_bus.UPDATE_SPDV_SMSLOAN(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ SMSLOAN tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ SMSLOAN tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();

            //sCommand = "Select * from SMSLOAN where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'SMSLOAN')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",SMSLOAN";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}
            //Tinh diem su dung ABIC       

            if (spdv_bus.UPDATE_SPDV_ABIC(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ ABIC vào bảng SPDV tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ ABIC tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();

            //sCommand = "Select distinct(makh) from ABIC where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'ABIC')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",ABIC";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}

            //Tinh diem su dung dich vu VNPT

            if (spdv_bus.UPDATE_SPDV_VNPT(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ VNPT tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ VNPT tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();
            //sCommand = "Select * from VNPT where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'VNPT')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",VNPT";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}
            //Tinh diem su dung dich vu Nuoc

            if (spdv_bus.UPDATE_SPDV_NUOC(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ NUOC tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ NUOC tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();
            //sCommand = "Select * from NUOC where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'NUOC')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",NUOC";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}
            //Tinh diem su dung dich vu SMS

            if (spdv_bus.UPDATE_SPDV_SMS(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ SMS tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ SMS tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();
            //sCommand = "Select * from SMS where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'SMS')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",SMS";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}

            //Tinh diem su dung dich vu The

            if (spdv_bus.UPDATE_SPDV_THE(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ thẻ tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ thẻ tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();
            ////sCommand = "Select * from THE where ngaydk<='" + ngaytinhdiem + "' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            //sCommand = "Select * from THE where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'THE')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",THE";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}

            //Tinh diem su dung dich vu chuyen tien

            if (spdv_bus.UPDATE_SPDV_CHUYENTIEN(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ chuyển tiền tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ chuyển tiền tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();
            //sCommand = "Select distinct(makh) from Chuyentien where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'Chuyentien')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",Chuyentien";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}
            //Tinh diem su dung chuyen tien nuoc ngoai      

            if (spdv_bus.UPDATE_SPDV_CHUYENTIENN(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ chuyển tiền nước ngoài tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ chuyển tiền nước ngoài tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();

            //sCommand = "Select distinct(makh)from Chuyentienn where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'chuyentiennuocngoai')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",chuyentiennuocngoai";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}

            //Tinh diem su dung dich vu tra luong qua tai khoan

            if (spdv_bus.UPDATE_SPDV_CHUYENLUONG(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ trả lương qua tài khoản tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ trả lương qua tài khoản tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();
            //sCommand = "Select makh from Chuyenluong where hientrang=1 and left(makh,4)='" + Thongtindangnhap.macn + "'";
            ////sCommand = "Select makh from Chuyenluong where hientrang=1 and ngaydk<='" + ngaytinhdiem + "' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'Chuyenluong')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",Chuyenluong";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }

            //    }
            //}
            //Tinh diem su dung dich vu CMS/POS

            if (spdv_bus.UPDATE_SPDV_CMSPOS(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ CMS,POS tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ CMS,POS tháng " + dtpThang.Text + " vào bảng SPDV");
            }


            //dt.Clear();
            //sCommand = "Select makh from CMSPOS where hientrang='True' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            ////sCommand = "Select makh from Chuyenluong where hientrang=1 and ngaydk<='" + ngaytinhdiem + "' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'CMS_POS')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",CMS_POS";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }

            //    }
            //}
            //Tinh diem su dung TK Hoc Duong

            if (spdv_bus.UPDATE_SPDV_SKTIENGUI_TKHOCDUONG(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ tiết kiệm học đường tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ tiết kiệm học đường tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();

            //sCommand = "Select distinct(makh) from SKTIENGUI where left(sotk,7) like '____510' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'TKHD')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",TKHD";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}
            //Tinh diem su dung TK An Sinh

            if (spdv_bus.UPDATE_SPDV_SKTIENGUI_ANSINH(Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật khách hàng sử dụng dịch vụ tiết kiệm an sinh tháng " + dtpThang.Text + " vào bảng SPDV thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng sử dụng dịch vụ tiết kiệm an sinh tháng " + dtpThang.Text + " vào bảng SPDV");
            }

            //dt.Clear();

            //sCommand = "Select distinct(makh) from SKTIENGUI where (left(sotk,7)like '____511' or left(sotk,7)like '____512') and left(makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["MAKH"].ToString();
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'TKAS')";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            DataTable dt1 = new DataTable();
            //            sCommand = "select * from SPDV where makh='" + makh + "'";
            //            DataAccess.conn.Open();
            //            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //            DataAccess.conn.Close();

            //            if (dt1.Rows.Count > 0)
            //            {
            //                sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
            //                strdvu = dt1.Rows[0]["spdv"].ToString() + ",TKAS";
            //                sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //        }
            //    }
            //}

            //Dua du lieu vao table SPDVCT

            if (spdvct_bus.UPDATE_SPDVCT(Thongtindangnhap.macn,dtpThang.Text))
            {
                //MessageBox.Show("Cập nhật dữ liệu vào bảng SPDVCT tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật dữ liệu tháng " + dtpThang.Text + " vào bảng SPDVCT");
            }

            //dt.Clear();
            //sCommand = "Select spdv.*,loaikh from SPDV,khachhang where spdv.makh=khachhang.makh and left(spdv.makh,4)='" + Thongtindangnhap.macn + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        makh = dt.Rows[i]["makh"].ToString();
            //        int loaikh = Convert.ToByte(dt.Rows[i]["loaikh"].ToString());
            //        sldichvu = Convert.ToInt16(dt.Rows[i]["slspdv"].ToString());
            //        strdvu = dt.Rows[i]["spdv"].ToString();
            //        //Dua du lieu vao table SPDVCT
            //        try
            //        {
            //            sCommand = "INSERT INTO SPDVCT(MAKH,THANG,SPDV,SLSPDV,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "','" + strdvu + "'," + sldichvu + "," + loaikh + ")";
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //        catch
            //        {
            //            if (DataAccess.conn.State == ConnectionState.Open)
            //            {
            //                DataAccess.conn.Close();
            //            }

            //            sCommand = "Update SPDVCT set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "' and thang='"+dtpThang.Text+"'";
            //            DataAccess.conn.Open();
            //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //            frmMain.myCommand.ExecuteNonQuery();
            //            DataAccess.conn.Close();
            //        }
            //    }
            //}

        }
        private void lay_Profit()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[6] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("CCY", typeof(string)), 
                    new DataColumn("SOTIEN",typeof(decimal)), 
                    new DataColumn("SOTIENHOA",typeof(decimal)),
                    new DataColumn("PROFITRATIO",typeof(float)),
                    new DataColumn("PROFITVND",typeof(decimal)),
                }
            );
            DataRow dr;

            String qyery_temp;            
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\PROFIT" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.xls";
            
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','PROFIT_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                 
            }
                
            //Xoa du lieu dang co trong table Profit
            try
            {
                qyery_temp = "Update SDBQ set profitratio=0,profitvnd=0 where left(makh,4)='" + Thongtindangnhap.macn + "' and ccy='" + CCY + "'";
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

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}
                
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToDecimal(dt_temp.Rows[i][7].ToString().Replace("%","")) > 0)
                    {
                        String profit = dt_temp.Rows[i][7].ToString().Replace("%", "");
                        String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = makh;
                        dr["CCY"] = CCY;
                        dr["SOTIEN"] = 0;
                        dr["SOTIENHOA"] = 0;
                        dr["PROFITRATIO"] = Convert.ToDecimal(profit);
                        dr["PROFITVND"] = Convert.ToDecimal(dt_temp.Rows[i][13].ToString());
                        dt_temp2.Rows.Add(dr);

                        //DataTable dt_a = new DataTable();
                        //String scommand = "";
                        //scommand = "select * from SDBQ where makh='" + makh + "' and ccy='VND'";
                        //dt_a.Clear();
                        //if (DataAccess.conn.State == ConnectionState.Open)
                        //{
                        //    DataAccess.conn.Close();
                        //}
                        //DataAccess.conn.Open();
                        //new SqlDataAdapter(scommand, DataAccess.conn).Fill(dt_a);
                        //DataAccess.conn.Close();

                        //if (dt_a.Rows.Count > 0)
                        //{
                        //    qyery_temp = "UPDATE SDBQ set PROFITRATIO=" + Convert.ToDecimal(profit) + ",PROFITVND=" + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + " where makh ='" + makh + "' and ccy='" + CCY + "'";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //else
                        //{
                        //    qyery_temp = "INSERT INTO SDBQ(MAKH,CCY,PROFITRATIO,PROFITVND) Values ('" + makh + "','" + CCY + "'," + Convert.ToDecimal(profit) + "," + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + ")";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
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
            if (sdbq_bus.UPDATE_SDBQ_PROFITRATIO(dt_temp2))
            {
                //MessageBox.Show("Cập nhật dữ liệu PROFIT VND vào bảng SDBQ tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật dữ liệu PROFIT VND tháng " + dtpThang.Text + " vào bảng SDBQ");
            }
 
            //Import du lieu profit USD
            dt_temp2.Clear();
            CCY = "USD";
            filename = frmMain.ddimport + "\\PROFIT" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "USD.xls";

            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','PROFIT_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

            }

            //Xoa du lieu dang co trong table Profit
            try
            {
                qyery_temp = "Update SDBQ set profitratio=0,profitvnd=0 where left(makh,4)='" + Thongtindangnhap.macn + "' and ccy='" + CCY + "'";
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

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp.Clear();
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) > 0)
                    {
                        String profit = dt_temp.Rows[i][7].ToString().Replace("%", "");
                        String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = makh;
                        dr["CCY"] = CCY;
                        dr["SOTIEN"] = 0;
                        dr["SOTIENHOA"] = 0;
                        dr["PROFITRATIO"] = Convert.ToDecimal(profit);
                        dr["PROFITVND"] = Convert.ToDecimal(dt_temp.Rows[i][13].ToString());
                        dt_temp2.Rows.Add(dr);
                        //DataTable dt_a = new DataTable();
                        //String scommand = "";
                        //scommand = "select * from SDBQ where makh='" + makh + "' and ccy='USD'";
                        //dt_a.Clear();
                        //if (DataAccess.conn.State == ConnectionState.Open)
                        //{
                        //    DataAccess.conn.Close();
                        //}
                        //DataAccess.conn.Open();
                        //new SqlDataAdapter(scommand, DataAccess.conn).Fill(dt_a);
                        //DataAccess.conn.Close();

                        //if (dt_a.Rows.Count > 0)
                        //{
                        //    qyery_temp = "INSERT INTO SDBQ(MAKH,CCY,PROFITRATIO,PROFITVND) Values ('" + makh + "','" + CCY + "'," + Convert.ToDecimal(profit) + "," + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + ")";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //else
                        //{
                        //    qyery_temp = "UPDATE SDBQ set PROFITRATIO=" + Convert.ToDecimal(profit) + ",PROFITVND=" + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + " where makh ='" + makh + "' and ccy='" + CCY + "'";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
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
            if (sdbq_bus.UPDATE_SDBQ_PROFITRATIO(dt_temp2))
            {
                //MessageBox.Show("Cập nhật dữ liệu PROFIT USD vào bảng SDBQ tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật dữ liệu PROFIT USD tháng " + dtpThang.Text + " vào bảng SDBQ");
            }
           iprofit=1;
       }
        private void InsertProfitCT()
        {
            if (profitct_bus.UPDATE_PROFITCT(Thongtindangnhap.macn, dtpThang.Text))
            {
                //MessageBox.Show("Cập nhật dữ liệu vào bảng PROFITCT tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật dữ liệu tháng " + dtpThang.Text + " vào bảng PROFITCT");
            }
            //DataTable dt = new DataTable();
            //String makh = "";
            //decimal profit = 0;
            //byte loaikh;
            //String sCommand = "SELECT SDBQ.MAKH,PROFITRATIO,KHACHHANG.LOAIKH from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH  and PROFITRATIO<=100 and left(SDBQ.MAKH,4)='" + Thongtindangnhap.macn + "'";
            ////So lieu thang 07/2012 profitvnd=0
            ////sCommand = "SELECT SDBQ.MAKH,SUM(PROFITVND*PROFITRATIO)/SUM(PROFITVND) as PROFITRATIO,KHACHHANG.LOAIKH from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH and left(SDBQ.MAKH,4)='" + Thongtindangnhap.macn + "' group by sdbq.makh,khachhang.loaikh having sum(profitvnd)>0";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    makh = dt.Rows[i]["MAKH"].ToString();
            //    loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
            //    profit = Convert.ToDecimal(dt.Rows[i]["PROFITRATIO"].ToString());
            //    //Dua du lieu vao bang PROFITCT
            //    try
            //    {
            //        sCommand = "INSERT INTO PROFITCT(MAKH,THANG,PROFIT,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + profit + "," + loaikh + ")";
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }
            //        DataAccess.conn.Open();
            //        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //        frmMain.myCommand.ExecuteNonQuery();
            //        DataAccess.conn.Close();
            //    }
            //    catch
            //    {
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }

            //        sCommand = "Update PROFITCT set PROFIT =" + profit + " where makh='" + makh + "' and thang ='"+dtpThang.Text+"'";
            //        DataAccess.conn.Open();
            //        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //        frmMain.myCommand.ExecuteNonQuery();
            //        DataAccess.conn.Close();
            //    }
            //}
        }
        private void lay_Luong()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[4] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("NGAYDK", typeof(string)), 
                    new DataColumn("NGAYKETTHUC",typeof(string)), 
                    new DataColumn("HIENTRANG",typeof(bool))
                }
            );
            DataRow dr;

            DataTable dt_temp = new DataTable();
            String qyery_temp, filename = "";
            filename = frmMain.ddimport + "\\LUONG" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";
           
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','LUONG_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //MessageBox.Show("Du lieu nay da duoc import!");   
            }              
            //Xoa du lieu dang co trong table LUONG
            //try
            //{
            //    qyery_temp = "Delete CHUYENLUONG where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}
            //Dua du lieu vao bang CHUYENLUONG

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //   dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

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
                    dr = dt_temp2.NewRow();
                    dr["MAKH"] = makh;
                    dr["NGAYDK"] = ngaydk;
                    dr["NGAYKETTHUC"] = ngaykt;
                    dr["HIENTRANG"] = 1;
                    dt_temp2.Rows.Add(dr);
                    //try
                    //{
                    //    qyery_temp = "INSERT INTO CHUYENLUONG(MAKH,NGAYDK,NGAYKETTHUC,HIENTRANG) Values ('" + makh + "','" + ngaydk + "','" + ngaykt + "'," + Convert.ToInt16(dt_temp.Rows[i][3].ToString()) + ")";
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //    DataAccess.conn.Open();
                    //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                    //    frmMain.myCommand.ExecuteNonQuery();
                    //    DataAccess.conn.Close();
                    //}
                    //catch
                    //{
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //}
                }
             }
            if (chuyenluong_bus.UPDATE_CHUYENLUONG(dt_temp2, Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật dữ liệu khách hàng chuyển lương tháng " + dtpThang.Text + " vào bảng CHUYENLUONG thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật dữ liệu khách hàng chuyển lương tháng " + dtpThang.Text + " vào bảng CHUYENLUONG");
            }
             ispdv = 1;
        }

        private void lay_WU()
        {
            DataTable dt_temp_wu = new DataTable();
            dt_temp_wu.Columns.AddRange
            (
                new DataColumn[15] 
                { 
                    new DataColumn("ID", typeof(string)),
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                    new DataColumn("SOTIEN", typeof(decimal)),
                    new DataColumn("CCY", typeof(string)),
                    new DataColumn("NGAYNHAN", typeof(string)),
                    new DataColumn("MACN", typeof(string)),
                    new DataColumn("THANG", typeof(string)),
                    new DataColumn("DIENTHOAI", typeof(string)),
                    new DataColumn("MTCN", typeof(string)),
                    new DataColumn("HOTEN_GUI", typeof(string)),
                    new DataColumn("SOTIEN_GUI", typeof(decimal)),
                    new DataColumn("CCY_GUI", typeof(string))
                }
            );
            DataRow dr;
            String qyery_temp;
            DataTable dt_temp = new DataTable();
            String filename = "";
            filename = frmMain.ddimport + "\\WU" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";
            //Kiem tra du lieu da duoc import chua
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','WU_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            //Xoa du lieu dang co trong table WU
            //try
            //{
            //    qyery_temp = "Delete WU where MaCN='" + Thongtindangnhap.macn + "' and Thang='" + dtpThang.Text + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}
            //Import du lieu WU VND

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {
                        String ID = Thongtindangnhap.macn+DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                        string hoten = dt_temp.Rows[i][14].ToString() + " " + dt_temp.Rows[i][13].ToString();
                        string diachi = dt_temp.Rows[i][15].ToString() + " " + dt_temp.Rows[i][17].ToString();
                        string ngaynhan = dt_temp.Rows[i][40].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][40].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][40].ToString().Substring(6, 4);
                        string hoten_gui = dt_temp.Rows[i][8].ToString() + " " + dt_temp.Rows[i][9].ToString();

                        string cmt = dt_temp.Rows[i][24].ToString();
                        //DataTable dt1 = new DataTable();
                        //String sCommand = "SELECT makh from SKTIENGUI where CMND='" + cmt + "'";
                        //if (DataAccess.conn.State == ConnectionState.Open)
                        //{
                        //    DataAccess.conn.Close();
                        //}
                        //DataAccess.conn.Open();
                        //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                        //DataAccess.conn.Close();

                        String makh = Thongtindangnhap.macn + "000000000";
                        //int j = dt1.Rows.Count;
                        //if (j != 0)
                        //{
                        //    makh = dt1.Rows[j - 1]["makh"].ToString();
                        //}

                        dr = dt_temp_wu.NewRow();
                        dr["ID"] = ID;
                        dr["MAKH"] = makh;
                        dr["HOTEN"] = hoten;
                        dr["DIACHI"] = diachi;
                        dr["CMND"] = cmt;
                        dr["SOTIEN"] = Convert.ToDecimal(dt_temp.Rows[i][39].ToString());
                        dr["CCY"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYNHAN"] = ngaynhan;
                        dr["MACN"] = Thongtindangnhap.macn;
                        dr["THANG"] = dtpThang.Text;
                        dr["DIENTHOAI"] = dt_temp.Rows[i][20].ToString();
                        dr["MTCN"] = dt_temp.Rows[i][6].ToString();
                        dr["HOTEN_GUI"] = hoten_gui;
                        dr["SOTIEN_GUI"] = Convert.ToDecimal(dt_temp.Rows[i][28].ToString());
                        dr["CCY_GUI"] = dt_temp.Rows[i][32].ToString();
                        dt_temp_wu.Rows.Add(dr);

                        //try
                        //{
                        //    if (makh != "")
                        //    {
                        //        qyery_temp = "INSERT INTO WU(ID,MAKH,HOTEN,DIACHI,CMND,SOTIEN,CCY,NGAYNHAN,MACN,THANG,DIENTHOAI,MTCN,HOTEN_GUI,SOTIEN_GUI,CCY_GUI) ";
                        //        qyery_temp += " Values ('" + ID + "','" + makh + "',N'" + hoten + "',N'" + diachi + "','" + dt_temp.Rows[i][24].ToString() + "','" + Convert.ToDecimal(dt_temp.Rows[i][39].ToString()) + "','" + dt_temp.Rows[i][33].ToString() + "','" + ngaynhan + "','" + Thongtindangnhap.macn + "','" + dtpThang.Text + "','";
                        //        qyery_temp += dt_temp.Rows[i][20].ToString() + "','" + dt_temp.Rows[i][6].ToString() + "','" + hoten_gui + "','" + Convert.ToDecimal(dt_temp.Rows[i][28].ToString()) + "','" + dt_temp.Rows[i][32].ToString() + "')";
                        //    }
                        //    else
                        //    {
                        //        qyery_temp = "INSERT INTO WU(ID,MAKH,HOTEN,DIACHI,CMND,SOTIEN,CCY,NGAYNHAN,MACN,THANG,DIENTHOAI,MTCN,HOTEN_GUI,SOTIEN_GUI,CCY_GUI) ";
                        //        qyery_temp += " Values ('" + ID + "','" + Thongtindangnhap.macn + "'+'000000000',N'" + hoten + "',N'" + diachi + "','" + dt_temp.Rows[i][24].ToString() + "','" + Convert.ToDecimal(dt_temp.Rows[i][39].ToString()) + "','" + dt_temp.Rows[i][33].ToString() + "','" + ngaynhan + "','" + Thongtindangnhap.macn + "','" + dtpThang.Text + "','";
                        //        qyery_temp += dt_temp.Rows[i][20].ToString() + "','" + dt_temp.Rows[i][6].ToString() + "','" + hoten_gui + "','" + Convert.ToDecimal(dt_temp.Rows[i][28].ToString()) + "','" + dt_temp.Rows[i][32].ToString() + "')";
                        //    }            
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //catch
                        //{
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //}

                        //if (makh == "")
                        //{
                        //    try
                        //    {
                        //        makh = "T" + Thongtindangnhap.macn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                        //        qyery_temp = "INSERT INTO KHACHHANGCHUYENTIEN(MAKH,HOTEN,DIACHI1,CMND,MACN,GHICHU,TINHTRANG,LOAIKH) ";
                        //        qyery_temp += " Values ('" + makh + "',N'" + hoten + "','" + diachi + "','" + dt_temp.Rows[i][24].ToString() + "','" + Thongtindangnhap.macn + "',N'Khách hàng nhận tiền WU',1,1)";
                        //        if (DataAccess.conn.State == ConnectionState.Open)
                        //        {
                        //            DataAccess.conn.Close();
                        //        }
                        //        DataAccess.conn.Open();
                        //        frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //        frmMain.myCommand.ExecuteNonQuery();
                        //        DataAccess.conn.Close();
                        //    }
                        //    catch
                        //    {
                        //        if (DataAccess.conn.State == ConnectionState.Open)
                        //        {
                        //            DataAccess.conn.Close();
                        //        }
                        //    }
                        //}
                    }
                    Thread.Sleep(1);
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
            }

            //int maxStringLength = dt_temp_wu.AsEnumerable()
            //                  .SelectMany(row => row.ItemArray.OfType<string>())
            //                  .Max(str => str.Length);
            //MessageBox.Show(maxStringLength.ToString());
            if (wu_bus.UPDATE_WU(dt_temp_wu, Thongtindangnhap.macn, dtpThang.Text))
            {
                bool update_wu_makh = wu_bus.UPDATE_WU_MAKH(Thongtindangnhap.macn);
                //MessageBox.Show("Cập nhật dữ liệu khách hàng sử dụng dịch vụ WU tháng " + dtpThang.Text + " vào bảng WU thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi Cập nhật dữ liệu khách hàng sử dụng dịch vụ WU tháng " + dtpThang.Text + " vào bảng WU.");
            }
            //Cập nhật khách hàng không có mã khách hàng vào bảng khách hàng chuyển tiền
            if (khct_bus.UPDATE_KHACHHANGCHUYENTIEN_WU(Thongtindangnhap.macn, dtpThang.Text))
            {
                //MessageBox.Show("Cập nhật dữ liệu khách hàng sử dụng dịch vụ WU tháng " + dtpThang.Text + " vào bảng KHACHHANGCHUYENTIEN thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi Cập nhật dữ liệu khách hàng sử dụng dịch vụ WU tháng " + dtpThang.Text + " vào bảng KHACHHANGCHUYENTIEN.");
            }
        }

        //Import ABic
        private void lay_ABIC()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[4] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("SOHD", typeof(string)), 
                    new DataColumn("SOTIENBH",typeof(decimal)), 
                    new DataColumn("SOTIENKS",typeof(decimal))
                }
            );
            DataRow dr;

            String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\ABIC" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','ABIC_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                    // this.Close();

                }

            }

            //Xoa du lieu dang co trong table ABIC
            //try
            //{
            //    qyery_temp = "Delete ABIC where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //    //this.Dispose();
            //}

            for (int i = 7; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    string sohd = dt_temp.Rows[i][3].ToString();
                    if (sohd != "")
                    //if ((dt_temp.Rows[i][18].ToString().Substring(0, 3) != "670") && (dt_temp.Rows[i][18].ToString().Substring(0, 3) != "671"))
                    {
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = Thongtindangnhap.macn + "000000000";
                        dr["SOHD"] = sohd;
                        dr["SOTIENBH"] = Convert.ToDecimal(dt_temp.Rows[i][6].ToString());
                        dr["SOTIENKS"] = Convert.ToDecimal(dt_temp.Rows[i][8].ToString());
                        dt_temp2.Rows.Add(dr);

                        //String sCommand = "SELECT distinct(MAKH) from sktienvay where sohd='" + sohd + "'";                                          
                        //if (DataAccess.conn.State == ConnectionState.Open)
                        //{
                        //    DataAccess.conn.Close();
                        //}
                        //DataAccess.conn.Open();
                        //DataTable dt = new DataTable();
                        //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
                        //DataAccess.conn.Close();
                        //String makh = dt.Rows[0][0].ToString();
                        //dt.Clear();

                        //try
                        //{
                        //    qyery_temp = "INSERT INTO ABIC(MAKH,SOHD,SOTIENBH,SOTIENKS) Values ('" + makh + "','" + sohd + "'," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) + "," + Convert.ToDecimal(dt_temp.Rows[i][8].ToString()) + ")";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //catch
                        //{
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //}
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
            if (abic_bus.UPDATE_ABIC(dt_temp2, Thongtindangnhap.macn))
            {
                bool update_abic_makh = abic_bus.UPDATE_ABIC_MAKH();
                //MessageBox.Show("Cập nhật dữ liệu khách hàng sử dụng dịch vụ ABIC tháng " + dtpThang.Text + " vào bảng ABIC thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi Cập nhật dữ liệu khách hàng sử dụng dịch vụ ABIC tháng " + dtpThang.Text + " vào bảng ABIC.");
            }

            ispdv = 1;
        }


        //Import SMSLOAN
        private void lay_SMSLoan()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[3] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("SOHD", typeof(string)), 
                    new DataColumn("SOGN",typeof(string)), 
                }
            );
            DataRow dr;

            String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\SMSLOAN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','SMSLOAN_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                    // this.Close();

                }

            }

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //    //this.Dispose();
            //}            

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    string makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();
                    if (makh != "")
                    //if ((dt_temp.Rows[i][18].ToString().Substring(0, 3) != "670") && (dt_temp.Rows[i][18].ToString().Substring(0, 3) != "671"))
                    {
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = makh;
                        dr["SOHD"] = dt_temp.Rows[i][3].ToString();
                        dr["SOGN"] = dt_temp.Rows[i][2].ToString();
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    qyery_temp = "INSERT INTO SMSLOAN(MAKH,SOHD,SOGN) Values ('" + makh + "','" + dt_temp.Rows[i][3].ToString() + "','" + dt_temp.Rows[i][2].ToString() + "')";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //catch
                        //{
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //}
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
            if (smsloan_bus.UPDATE_SMSLOAN(dt_temp2))
            {
                //MessageBox.Show("Cập nhật dữ liệu khách hàng sử dụng dịch vụ SMSLOAN tháng " + dtpThang.Text + " vào bảng SMSLOAN thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi Cập nhật dữ liệu khách hàng sử dụng dịch vụ SMSLOAN tháng " + dtpThang.Text + " vào bảng SMSLOAN.");
            }

            ispdv = 1;
        }

        //lay chuyen tien den ngoai te
        private void lay_ChuyentiendenN()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[9] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("LOAINT", typeof(string)),
                    new DataColumn("SOTIEN", typeof(decimal)),
                    new DataColumn("LOAICHUYENTIEN", typeof(byte)),
                    new DataColumn("THANG", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI", typeof(string)),
                    new DataColumn("SDT", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                }
            );
            DataRow dr;

            String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\FXNDEN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','FXNDEN_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            //Xoa du lieu trong table CHUYENTIEN
            //try
            //{
            //    qyery_temp = "DELETE CHUYENTIENN WHERE LOAICHUYENTIEN=1 and left(makh,4)='" + Thongtindangnhap.macn + "' and thang='" + dtpThang.Text + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null)
                    {

                        String makh = dt_temp.Rows[i][11].ToString();
                        makh = Thongtindangnhap.macn + makh;

                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = makh;
                        dr["LOAINT"] = dt_temp.Rows[i][2].ToString();
                        dr["SOTIEN"] = Convert.ToDecimal(dt_temp.Rows[i][3].ToString());
                        dr["LOAICHUYENTIEN"] = 1;
                        dr["THANG"] = dtpThang.Text;
                        dr["HOTEN"] = dt_temp.Rows[i][6].ToString();
                        dr["DIACHI"] = "";
                        dr["SDT"] = "";
                        dr["CMND"] = "";
                        dt_temp2.Rows.Add(dr);

                        //Dua cac khach hang chuyen tien da co makh va danh sach chuyen tien de tinh diem
                        //if (makh != "000000000")
                        //{
                        //    makh = Thongtindangnhap.macn + makh;
                        //    DataTable dt1 = new DataTable();

                        //    String sCommand = "SELECT hoten,diachi1,dienthoai1,cmnd from khachhang where makh='" + makh + "'";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    dt1.Clear();
                        //    new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                        //    DataAccess.conn.Close();                            
                        //    try
                        //    {
                        //        qyery_temp = "INSERT INTO CHUYENTIENN(MAKH,LOAINT,SOTIEN,LOAICHUYENTIEN,THANG,HOTEN,DIACHI,SDT,CMND) Values ('" + makh + "','" + dt_temp.Rows[i][2].ToString() + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",1,'" + dtpThang.Text + "',N'" + dt1.Rows[0]["HOTEN"].ToString() + "',N'" + dt1.Rows[0]["DIACHI1"].ToString() + "','" + dt1.Rows[0]["DIENTHOAI1"].ToString() + "','" + dt1.Rows[0]["CMND"].ToString() + "')";
                        //        if (DataAccess.conn.State == ConnectionState.Open)
                        //        {
                        //            DataAccess.conn.Close();
                        //        }
                        //        DataAccess.conn.Open();
                        //        frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //        frmMain.myCommand.ExecuteNonQuery();
                        //        DataAccess.conn.Close();
                        //    }
                        //    catch
                        //    {
                        //        if (DataAccess.conn.State == ConnectionState.Open)
                        //        {
                        //            DataAccess.conn.Close();
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    makh = Thongtindangnhap.macn + makh;
                        //    try
                        //    {
                        //        qyery_temp = "INSERT INTO CHUYENTIENN(MAKH,LOAINT,SOTIEN,LOAICHUYENTIEN,THANG,HOTEN) Values ('" + makh + "','" + dt_temp.Rows[i][2].ToString() + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",1,'" + dtpThang.Text + "','" + dt_temp.Rows[i][6].ToString() + "')";
                        //        if (DataAccess.conn.State == ConnectionState.Open)
                        //        {
                        //            DataAccess.conn.Close();
                        //        }
                        //        DataAccess.conn.Open();
                        //        frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //        frmMain.myCommand.ExecuteNonQuery();
                        //        DataAccess.conn.Close();
                        //    }
                        //    catch
                        //    {
                        //        if (DataAccess.conn.State == ConnectionState.Open)
                        //        {
                        //            DataAccess.conn.Close();
                        //        }
                        //    }
                        //}
                    }
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                }
                Thread.Sleep(1);
            }
            if (chuyentienn_bus.UPDATE_CHUYENTIENN(dt_temp2, Thongtindangnhap.macn, dtpThang.Text, 1))
            {
                bool update_chuyentienn_hoten = chuyentienn_bus.UPDATE_CHUYENTIENN_HOTEN();
                //MessageBox.Show("Cập nhật dữ liệu chuyển tiền đến vào bảng CHUYENTIENN tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật dữ liệu chuyển tiền đến vào bảng CHUYENTIENN tháng " + dtpThang.Text);
            }
            ispdv = 1;
        }
        private void lay_ChuyentiendiN()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[9] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("LOAINT", typeof(string)),
                    new DataColumn("SOTIEN", typeof(decimal)),
                    new DataColumn("LOAICHUYENTIEN", typeof(byte)),
                    new DataColumn("THANG", typeof(string)),
                    new DataColumn("HOTEN", typeof(string)),
                    new DataColumn("DIACHI", typeof(string)),
                    new DataColumn("SDT", typeof(string)),
                    new DataColumn("CMND", typeof(string)),
                }
            );
            DataRow dr;
            String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\FXNDI" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','FXNDI_THANG','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("File " + filename + " đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            //Xoa du lieu trong table CHUYENTIEN
            //try
            //{
            //    qyery_temp = "DELETE CHUYENTIENN WHERE LOAICHUYENTIEN=2 and left(makh,4)='" + Thongtindangnhap.macn + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

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
                            makh = Thongtindangnhap.macn + makh;
                            dr = dt_temp2.NewRow();
                            dr["MAKH"] = makh;
                            dr["LOAINT"] = dt_temp.Rows[i][5].ToString();
                            dr["SOTIEN"] = Convert.ToDecimal(dt_temp.Rows[i][6].ToString());
                            dr["LOAICHUYENTIEN"] = 2;
                            dr["THANG"] = dtpThang.Text;
                            dr["HOTEN"] = "";
                            dr["DIACHI"] = "";
                            dr["SDT"] = "";
                            dr["CMND"] = "";
                            dt_temp2.Rows.Add(dr);

                            //try
                            //{
                            //    qyery_temp = "INSERT INTO CHUYENTIENN(MAKH,LOAINT,SOTIEN,LOAICHUYENTIEN) Values ('" + makh + "','" + dt_temp.Rows[i][5].ToString() + "'," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) + ",2)";
                            //    if (DataAccess.conn.State == ConnectionState.Open)
                            //    {
                            //        DataAccess.conn.Close();
                            //    }
                            //    DataAccess.conn.Open();
                            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                            //    frmMain.myCommand.ExecuteNonQuery();
                            //    DataAccess.conn.Close();
                            //}
                            //catch
                            //{
                            //    if (DataAccess.conn.State == ConnectionState.Open)
                            //    {
                            //        DataAccess.conn.Close();
                            //    }
                            //}
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
                Thread.Sleep(1);
            }
            if (chuyentienn_bus.UPDATE_CHUYENTIENN(dt_temp2, Thongtindangnhap.macn, dtpThang.Text, 2))
            {
                bool update_chuyentienn_hoten = chuyentienn_bus.UPDATE_CHUYENTIENN_HOTEN();
                //MessageBox.Show("Cập nhật dữ liệu chuyển tiền đi vào bảng CHUYENTIENN tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật dữ liệu chuyển tiền đi vào bảng CHUYENTIENN tháng " + dtpThang.Text);
            }
            ispdv = 1;
        }
        //Lay sao ke tien vay
        private void lay_SKTV()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[10] 
                { 
                    new DataColumn("MaKH", typeof(string)),
                    new DataColumn("SoHD", typeof(string)),
                    new DataColumn("SoGN", typeof(string)),
                    new DataColumn("NgayGN", typeof(string)),
                    new DataColumn("SoTienGN", typeof(decimal)),
                    new DataColumn("DuNo", typeof(decimal)),
                    new DataColumn("NgayDenHan", typeof(string)),
                    new DataColumn("LaiSuat", typeof(decimal)),
                    new DataColumn("CCY", typeof(string)),
                    new DataColumn("MaCN", typeof(string)),
                }
            );
            DataRow dr;

            String qyery_temp;
            String filename = "", CCY = "VND";
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\SKVAY" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";
            try
            {
                qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','" + "SKVAY_THANG" + "','" + dtpThang.Text + "')";
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

                if (MessageBox.Show("Dữ liệu SKVAY tháng " + dtpThang.Text + " đã được import! Import lại không?", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //MessageBox.Show("Du lieu nay da duoc import!");   
            }
            //}
            //Xoa du lieu dang co trong table SMS
            //try
            //{
            //    qyery_temp = "Delete SKTIENVAY where left(makh,4)='" + Thongtindangnhap.macn + "'";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
            //    frmMain.myCommand.ExecuteNonQuery();
            //    DataAccess.conn.Close();
            //}
            //catch
            //{
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //}

            dt_temp.Clear();
            dt_temp = read_excel(filename);
            if (dt_temp == null)
            {
                MessageBox.Show("Không đọc được file " + filename);
                return;
            }

            //try
            //{
            //    dt_temp = read_excel(filename);
            //}
            //catch
            //{
            //    MessageBox.Show("Không đọc được file " + filename);
            //    return;
            //}

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

                dr = dt_temp2.NewRow();
                dr["MaKH"] = makh;
                dr["SoHD"] = dt_temp.Rows[i][3].ToString();
                dr["SoGN"] = dt_temp.Rows[i][8].ToString();
                dr["NgayGN"] = ngaygn;
                dr["SoTienGN"] = Convert.ToDecimal(dt_temp.Rows[i][13].ToString());
                dr["DuNo"] = Convert.ToDecimal(dt_temp.Rows[i][17].ToString());
                dr["NgayDenHan"] = ngaydh;
                dr["LaiSuat"] = Convert.ToDecimal(dt_temp.Rows[i][11].ToString());
                dr["CCY"] = CCY;
                dr["MaCN"] = Thongtindangnhap.macn;
                dt_temp2.Rows.Add(dr);

                //try
                //{
                //    qyery_temp = "INSERT INTO SKTIENVAY(MAKH,SOHD,SOGN,NGAYGN,SOTIENGN,DUNO,NGAYDENHAN,LAISUAT,CCY,MACN) Values ('" + makh + "','" + dt_temp.Rows[i][3].ToString() + "','" + dt_temp.Rows[i][8].ToString() + "','" + ngaygn + "'," + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + "," + Convert.ToDecimal(dt_temp.Rows[i][17].ToString()) + ",'" + ngaydh + "'," + Convert.ToDecimal(dt_temp.Rows[i][11].ToString()) + ",'" + dt_temp.Rows[i][14].ToString() + "','" + Thongtindangnhap.macn + "')";
                //    if (DataAccess.conn.State == ConnectionState.Open)
                //    {
                //        DataAccess.conn.Close();
                //    }
                //    DataAccess.conn.Open();
                //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                //    frmMain.myCommand.ExecuteNonQuery();
                //    DataAccess.conn.Close();
                //}
                //catch
                //{
                //    if (DataAccess.conn.State == ConnectionState.Open)
                //    {
                //        DataAccess.conn.Close();
                //    }
                //}

            }
            if (sktienvay_bus.UPDATE_SKTIENVAY(dt_temp2, Thongtindangnhap.macn))
            {
                //MessageBox.Show("Cập nhật sao kê tiền vay tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật sao kê tiền vay tháng " + dtpThang.Text);
            }
            //Import sao ke tien vay chi tiet
            if (sktienvayct_bus.UPDATE_SKTIENVAYCT(Thongtindangnhap.macn, dtpThang.Text))
            {
                //MessageBox.Show("Cập nhật sao kê tiền vay chi tiết tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật sao kê tiền vay chi tiết tháng " + dtpThang.Text);
            }
            //MessageBox.Show("Đã import dữ liệu xong!");

            Cursor.Current = Cursors.Default;
        }
        //Import sao ke tien vay chi tiet
        //private void InsertSKTienVayCT()
        //{
        //    DataTable dt = new DataTable();
        //    String sCommand = "SELECT * from sktienvay where left(SKTIENVAY.MAKH,4)='" + Thongtindangnhap.macn + "'";

        //    int i = 0;

        //    if (DataAccess.conn.State == ConnectionState.Open)
        //    {
        //        DataAccess.conn.Close();
        //    }
        //    DataAccess.conn.Open();
        //    dt.Clear();
        //    new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
        //    DataAccess.conn.Close();
        //    for (i = 0; i < dt.Rows.Count; i++)
        //    {

        //        String smakh = dt.Rows[i][0].ToString();
        //        String ngaygn = dt.Rows[i][3].ToString().Substring(3, 2) + "/" + dt.Rows[i][3].ToString().Substring(0, 2) + "/" + dt.Rows[i][3].ToString().Substring(6, 4);
        //        String ngaydh = dt.Rows[i][6].ToString();
        //        if (ngaydh == "00/00/0000")
        //        {
        //            ngaydh = "01/01/1900";
        //        }
        //        else
        //        {
        //            ngaydh = dt.Rows[i][6].ToString().Substring(3, 2) + "/" + dt.Rows[i][6].ToString().Substring(0, 2) + "/" + dt.Rows[i][6].ToString().Substring(6, 4);
        //        }

        //        //Dua du lieu vao bang sktienvayct
        //        try
        //        {
        //            sCommand = "INSERT INTO SKTienVayCT(MAKH,SOHD,SOGN,NGAYGN,SOTIENGN,DUNO,NGAYDENHAN,LAISUAT,CCY,MACN,THANG) Values ('" + smakh + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + ngaygn + "'," + Convert.ToDecimal(dt.Rows[i][4].ToString()) + "," + Convert.ToDecimal(dt.Rows[i][5].ToString()) + ",'" + ngaydh + "'," + Convert.ToDecimal(dt.Rows[i][7].ToString()) + ",'" + dt.Rows[i][8].ToString() + "','" + Thongtindangnhap.macn + "','" + dtpThang.Text + "')";
        //            if (DataAccess.conn.State == ConnectionState.Open)
        //            {
        //                DataAccess.conn.Close();
        //            }
        //            DataAccess.conn.Open();
        //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
        //            frmMain.myCommand.ExecuteNonQuery();
        //            DataAccess.conn.Close();
        //        }
        //        catch
        //        {
        //            if (DataAccess.conn.State == ConnectionState.Open)
        //            {
        //                DataAccess.conn.Close();
        //            }

        //            sCommand = "Update SKTienVayCT set SoTienGN =" + Convert.ToDecimal(dt.Rows[i][4].ToString()) + " where SOGN='" + dt.Rows[i][2].ToString() + "' and thang='" + dtpThang.Text + "'";
        //            DataAccess.conn.Open();
        //            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
        //            frmMain.myCommand.ExecuteNonQuery();
        //            DataAccess.conn.Close();
        //        }
        //    }
        //}

                
        private void btnImport_Click(object sender, EventArgs e)
        {
            int Counter = 0;
            decimal perCounter;
                        
            qyery_temp = "select tygia from tygia where thang=" + Convert.ToInt16(dtpThang.Text.Substring(0, 2)) + "";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(qyery_temp, DataAccess.conn).Fill(dt_tygia);
            DataAccess.conn.Close();
            if (dt_tygia.Rows.Count > 0)
            {
                tygia = Convert.ToDecimal(dt_tygia.Rows[0][0].ToString());
            }

            Cursor.Current = Cursors.WaitCursor;
            //done
            //Thread t_sdbq = new Thread(lay_SDBQ);
            //t_sdbq.Start();
            //t_sdbq.Join();
            byte sodv_import = 10;
            lay_SDBQ();
            if (isdbq == 1)
            {
                InsertSDBQNT();
                InsertSDBQCT();
            }
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100/sodv_import);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 11));
            label1.Refresh();

            ////import sao ke tien gui de tinh thoi gian gui tien
            //Thread t_sktg = new Thread(lay_SKTG);
            //t_sktg.Start();
            //t_sktg.Join();
            

            lay_SKTG();
            if (itggt == 1)
            {
                InsertTGGTCT();
                InsertSKTGTTCT();
            }
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / sodv_import);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            label1.Refresh();

            ////import sao ke tien gui de lay khach hang co tai khoan tien gui thanh toan
            //lay_SKTG();
            //if (itggt == 1)
            //    InsertSKTGTTCT();
            //Counter = Counter + 1;
            //perCounter = (decimal)(Counter * 100 / 15);
            //groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            //groupBox1.Refresh();
            //label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 15));
            //label1.Refresh();

            //Thread t_sktv = new Thread(lay_SKTV);
            //t_sktv.Start();
            //t_sktv.Join();

            lay_SKTV();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / sodv_import);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            label1.Refresh();

            lay_Chuyentiendi();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / sodv_import);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            label1.Refresh();

            lay_Chuyentienden();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / sodv_import);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            label1.Refresh();
            
            lay_SMS();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / sodv_import);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            label1.Refresh();

            lay_Dien();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / sodv_import);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            label1.Refresh();

            //lay_VNPT();
            //Counter = Counter + 1;
            //perCounter = (decimal)(Counter * 100 / 10);
            //groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            //groupBox1.Refresh();
            //label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));
            //label1.Refresh();

            //lay_NUOC();
            //Counter = Counter + 1;
            //perCounter = (decimal)(Counter * 100 / 17);
            //groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            //groupBox1.Refresh();
            //label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 17));
            //label1.Refresh();

            lay_The();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / sodv_import);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            label1.Refresh();

            //lay_Profit();
            //if (iprofit == 1)
            //    InsertProfitCT();
            //Counter = Counter + 1;
            //perCounter = (decimal)(Counter * 100 / sodv_import);
            //groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            //groupBox1.Refresh();
            //label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            //label1.Refresh();

            //lay_Luong();
            //Counter = Counter + 1;
            //perCounter = (decimal)(Counter * 100 / sodv_import);
            //groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            //groupBox1.Refresh();
            //label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            //label1.Refresh();

            //lay_ABIC();
            //Counter = Counter + 1;
            //perCounter = (decimal)(Counter * 100 / sodv_import);
            //groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            //groupBox1.Refresh();
            //label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            //label1.Refresh();

            //lay_ChuyentiendenN();
            //Counter = Counter + 1;
            //perCounter = (decimal)(Counter * 100 / sodv_import);
            //groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            //groupBox1.Refresh();
            //label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            //label1.Refresh();

            //lay_ChuyentiendiN();
            //Counter = Counter + 1;
            //perCounter = (decimal)(Counter * 100 / sodv_import);
            //groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            //groupBox1.Refresh();
            //label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            //label1.Refresh();

            lay_SMSLoan();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / sodv_import);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            label1.Refresh();

            lay_WU();            
            if(ispdv==1)
                InsertSPDVCT();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 100 / sodv_import);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / sodv_import));
            label1.Refresh();

            //ExcelObj.Quit();
            //while (Marshal.ReleaseComObject(ExcelObj) != 0) { }
            //ExcelObj = null;
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //Thread.Sleep(10000);
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
    }
}