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
    public partial class frmHT_ImportKH : Form
    {
        KHACHHANGBUS khachhang_bus = new KHACHHANGBUS();
        //MicrosoftExcelClient m_ExcelClient = null;
        //Excel.Application ExcelObj = null;
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
            //using (var stream = File.Open(excel_path, FileMode.Open, FileAccess.Read))
            //{

            //    // Auto-detect format, supports:
            //    //  - Binary Excel files (2.0-2003 format; *.xls)
            //    //  - OpenXml Excel files (2007 format; *.xlsx)
            //    using (var reader = ExcelReaderFactory.CreateReader(stream))
            //    {

            //        // Choose one of either 1 or 2:

            //        // 1. Use the reader methods
            //        //do
            //        //{
            //        //    while (reader.Read())
            //        //    {
            //        //        // reader.GetDouble(0);
            //        //    }
            //        //} while (reader.NextResult());

            //        // 2. Use the AsDataSet extension method
            //        var result = reader.AsDataSet();
            //        dt = result.Tables[0];
            //        // The result of each spreadsheet is in result.Tables
            //    }
            //}
            //return reader
        }

        //public DataTable read_excel2(String excel_path)
        //{
        //    //Create COM Objects. Create a COM object for everything that is referenced
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(excel_path);
        //    Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        //    Excel.Range xlRange = xlWorksheet.UsedRange;

        //    int rowCount = xlRange.Rows.Count;
        //    int colCount = xlRange.Columns.Count;

        //    //iterate over the rows and columns and print to the console as it appears in the file
        //    //excel is not zero based!!
        //    for (int i = 1; i <= rowCount; i++)
        //    {
        //        for (int j = 1; j <= colCount; j++)
        //        {
        //            //new line
        //            if (j == 1)
        //                Console.Write("\r\n");

        //            //write the value to the console
        //            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
        //                Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
        //        }
        //    }

        //    //cleanup
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();

        //    //rule of thumb for releasing com objects:
        //    //  never use two dots, all COM objects must be referenced and released individually
        //    //  ex: [somthing].[something].[something] is bad

        //    //release com objects to fully kill excel process from running in the background
        //    Marshal.ReleaseComObject(xlRange);
        //    Marshal.ReleaseComObject(xlWorksheet);

        //    //close and release
        //    xlWorkbook.Close();
        //    Marshal.ReleaseComObject(xlWorkbook);

        //    //quit and release
        //    xlApp.Quit();
        //    Marshal.ReleaseComObject(xlApp);
        //}
        //private DataTable read_excel2(String file_excel)
        //{
        //    Excel.Workbook theWorkbook = null;
        //    try
        //    {
        //        if (ExcelObj == null)
        //        {
        //            ExcelObj = new Excel.Application();
        //        }

        //        //Excel.Workbook theWorkbook = null;

        //        Excel.Workbooks theWorkbooks = ExcelObj.Workbooks;

        //        theWorkbook = theWorkbooks.Open(file_excel, Missing.Value, Missing.Value, Missing.Value
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
        //            theWorkbook.Close(0);
        //        //ExcelObj.Quit();

        //        return result;
        //    }
        //    catch
        //    {
        //        //this.m_ExcelClient.closeConnection();
        //        if (theWorkbook != null)
        //            theWorkbook.Close(0);
        //        return null;
        //    } 
        //}

        private void btnImport_Click(object sender, EventArgs e)
        {
            int Counter = 0;
            decimal perCounter;
            //DataAccess.conn.Open();

            lay_KHCN();
            Cursor.Current = Cursors.Default;
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 12));
            label1.Refresh();
            //Counter = Counter + 1;
            //perCounter = (decimal)(Counter * 10);
            //groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            //groupBox1.Refresh();
            //label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 10));
            //label1.Refresh();

            lay_KHDNTN();
            Cursor.Current = Cursors.Default;
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 12));
            label1.Refresh();

            lay_KHHGD();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 12));
            label1.Refresh();

            lay_KHHTX();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 12));
            label1.Refresh();

            lay_KHCTTNHH();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 12));
            label1.Refresh();

            lay_KHCTCP();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 12));
            label1.Refresh();

            lay_KHCTLD();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 12));
            label1.Refresh();

            lay_KHDNDTNN();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 12));
            label1.Refresh();

            lay_KHDNNN();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 12));
            label1.Refresh();

            lay_KHTCTC();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 12));
            label1.Refresh();

            lay_KHTCXH();
            Counter = Counter + 1;
            perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = ((int)(perCounter)).ToString() + "%";
            groupBox1.Refresh();
            label1.Width = Convert.ToInt32(Counter * (groupBox1.Width / 12));
            label1.Refresh();

            lay_KHTC();
            //perCounter = (decimal)(Counter * 8.33);
            groupBox1.Text = "100%";
            groupBox1.Refresh();
            label1.Width = groupBox1.Width;
            label1.Refresh();

            //ExcelObj.Quit();
            //while (Marshal.ReleaseComObject(ExcelObj) != 0) { }
            //ExcelObj = null;
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //Thread.Sleep(10000);
            MessageBox.Show("Đã import dữ liệu xong!");
            //DataAccess.conn.Close();
        }

        private void lay_KHCN()
        {
            filename = frmMain.ddimport + "\\CIFCN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
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
            //}
            //Định dạng ngày tháng theo dạng en-US cho hàm convert.todatetie
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
            
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0,4) == Thongtindangnhap.macn)
                    {
                        String ngaycap, ngaysinh, didong;
                        String hoten = dt_temp.Rows[i][4].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi1 = dt_temp.Rows[i][22].ToString().Replace(",", "-").Replace("'", "-");
                        String diachi2 = dt_temp.Rows[i][23].ToString().Replace(",", "-").Replace("'", "-");
                        didong = dt_temp.Rows[i][9].ToString();
                        ngaysinh = dt_temp.Rows[i][12].ToString().Trim();
                        if (ngaysinh != "")
                        {
                            //định dạng mm/dd/yyy
                            ngaysinh = ngaysinh.Substring(4, 2) + "/" + ngaysinh.Substring(6, 2) + "/" + ngaysinh.Substring(0, 4);
                        }
                        else
                        {
                            //định dạng mm/dd/yyy
                            ngaysinh = "01/01/1900";
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
                        loaikh = 1;
                        String ngaytao = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        if (dt_temp.Rows[i][14].ToString() != "")
                        {
                            //Khách hàng sử dụng chứng minh nhân dân
                            dr["CMND"] = dt_temp.Rows[i][14].ToString();
                            ngaycap = dt_temp.Rows[i][34].ToString().Trim();
                            if (ngaycap != "")
                            {
                                //định dạng mm/dd/yyy
                                ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                            }
                            else
                            {
                                //định dạng mm/dd/yyy
                                ngaycap = "01/01/1900";
                            }
                            dr["NGAYCAP"] = ngaycap;
                            dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        }
                        else if (dt_temp.Rows[i][15].ToString() != "")
                        {
                            //Khách hàng sử dụng hộ chiếu
                            dr["CMND"] = dt_temp.Rows[i][15].ToString();
                            ngaycap = dt_temp.Rows[i][36].ToString().Trim();
                            if (ngaycap != "")
                            {
                                //định dạng mm/dd/yyy
                                ngaycap = ngaycap.Substring(4, 2) + "/" + ngaycap.Substring(6, 2) + "/" + ngaycap.Substring(0, 4);
                            }
                            else
                            {
                                //định dạng mm/dd/yyy
                                ngaycap = "01/01/1900";
                            }
                            dr["NGAYCAP"] = ngaycap;
                            dr["NOICAP"] = dt_temp.Rows[i][35].ToString();
                        }
                        else
                        {
                            dr["CMND"] = "";
                            dr["NGAYCAP"] = "01/01/1900";
                            dr["NOICAP"] = "";
                        }
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); //"Cá nhân"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "14";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3,4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Cá nhân','" + strngaytao + "','14')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "',loaikh=" + loaikh + " where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                //MessageBox.Show("Nhập thông tin khách hàng cá nhân tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng cá nhân tháng " + dtpThang.Text);
            }      
        }
        private void lay_KHDNTN()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFDNTN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";

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
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
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
                        loaikh = 2;
                        String ngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); //Doanh nghiệp tư nhân
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "23";
                        dr["DOITUONGDN"] = "2";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh,doituongdn) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Doanh nghiệp tư nhân','"+strngaytao+"','23','2')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                //MessageBox.Show("Nhập thông tin khách hàng DN tư nhân tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng DN tư nhân tháng " + dtpThang.Text);
            }
        }
        private void lay_KHHGD()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFHGD" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";

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
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
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
                        loaikh = 2;
                        String ngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Hộ gia đình"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "13";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Hộ gia đình','"+strngaytao+"','13')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                //MessageBox.Show("Nhập thông tin khách hàng Hộ gia đình tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng Hộ gia đình tháng " + dtpThang.Text);
            }
        }
        private void lay_KHHTX()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFHTX" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";

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
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
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
                        loaikh = 2;
                        String ngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Hợp tác xã"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "24";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);

                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Hợp tác xã','"+strngaytao+"','24')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                //MessageBox.Show("Nhập thông tin khách hàng Hợp tác xã tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng Hợp tác xã tháng " + dtpThang.Text);
            }
        }

        private void lay_KHCTCP()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFCTCP" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";

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
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
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
                        loaikh = 2;
                        String ngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Công ty cổ phần"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "21";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Công ty cổ phần','"+strngaytao+"','21')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2,Thongtindangnhap.user_id))
            {
                //MessageBox.Show("Nhập thông tin khách hàng Công ty cổ phần tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng Công ty cổ phần tháng " + dtpThang.Text);
            }

        }
        private void lay_KHCTTNHH()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFCTTNHH" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";

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
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
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
                        loaikh = 2;
                        String ngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Công ty TNHH"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "21";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Công ty TNHH','"+strngaytao+"','21')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                //MessageBox.Show("Nhập thông tin khách hàng Công ty trách nhiệm hữu hạn tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng Công ty trách nhiệm hữu hạn tháng " + dtpThang.Text);
            }
        }
        private void lay_KHCTLD()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFCTLD" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";

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
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
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
                        loaikh = 2;
                        String ngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString();// "Công ty liên doanh";
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "21";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Công ty liên doanh','"+strngaytao+"','21')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                //MessageBox.Show("Nhập thông tin khách hàng Công ty liên doanh tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng Công ty liên doanh tháng " + dtpThang.Text);
            }
        }

        private void lay_KHDNDTNN()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFDNDTNN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";

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
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
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
                        Int16 loaikh = 2;
                        String ngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Doanh nghiệp có vốn ĐT nước ngoài"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "14";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);

                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Doanh nghiệp có vốn ĐT nước ngoài','"+strngaytao+"','21')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                //MessageBox.Show("Nhập thông tin khách hàng Doanh nghiệp có vốn ĐT nước ngoài tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng Doanh nghiệp có vốn ĐT nước ngoài tháng " + dtpThang.Text);
            }
        }
        private void lay_KHDNNN()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFDNNN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";

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
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
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
                        Int16 loaikh=2;
                        String ngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString();// "Doanh nghiệp Nhà nước";
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "41";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Doanh nghiệp Nhà nước','"+strngaytao+"','41')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                //MessageBox.Show("Nhập thông tin khách hàng Doanh nghiệp Nhà nước tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng Doanh nghiệp Nhà nước tháng " + dtpThang.Text);
            }

        }
        private void lay_KHTCTC()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFTCTC" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";

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
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
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
                        Int16 loaikh=2;
                        String ngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Tổ chức Tài chính"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "41";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);

                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Tổ chức Tài chính','"+strngaytao+"','41')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2,Thongtindangnhap.user_id))
            {
               //MessageBox.Show("Nhập thông tin khách hàng Tổ chức tài chính tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng Tổ chức tài chính tháng " + dtpThang.Text);
            }

        }
        private void lay_KHTCXH()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFTCXH" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";

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
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
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
                        Int16 loaikh=2;
                        String ngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Tổ chức XH TƯ & Địa phương";
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "34";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Tổ chức XH TƯ & Địa phương','"+strngaytao+"','34')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
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
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                //MessageBox.Show("Nhập thông tin khách hàng Tổ chức XH TƯ & Địa phương tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng Tổ chức XH TƯ & Địa phương tháng " + dtpThang.Text);
            }

        }
        private void lay_KHTC()
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[39] 
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
                    new DataColumn("LOAIKH", typeof(int)),
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
                    new DataColumn("NGAYTHANHLAP", typeof(string)),
                    new DataColumn("NGAYTAO", typeof(string)),
                    new DataColumn("DOITUONGKH", typeof(string)),
                    new DataColumn("DOITUONGDN", typeof(string)),
                    new DataColumn("VONDAUTU", typeof(decimal)),
                    new DataColumn("SOLAODONG", typeof(decimal)),
                    new DataColumn("DSXNK", typeof(decimal)),
                    new DataColumn("NGAYTLNGANH", typeof(string))
                }
            );
            DataRow dr;
            //String qyery_temp;
            DataTable dt_temp = new DataTable();
            filename = frmMain.ddimport + "\\CIFTC" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".xls";

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
            //}

            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                try
                {
                    if (dt_temp.Rows[i][0].ToString() != null && dt_temp.Rows[i][0].ToString().Substring(0, 4) == Thongtindangnhap.macn)
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
                        Int16 loaikh=2;
                        String ngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        dr = dt_temp2.NewRow();
                        dr["MAKH"] = dt_temp.Rows[i][0].ToString();
                        dr["HOTEN"] = hoten;
                        dr["DIACHI1"] = diachi1;
                        dr["DIACHI2"] = diachi2;
                        dr["DIENTHOAI1"] = didong;
                        dr["DIENTHOAI2"] = "";
                        dr["EMAIL"] = "";
                        dr["CMND"] = dt_temp.Rows[i][14].ToString();
                        dr["NGAYCAP"] = ngaycap;
                        dr["NOICAP"] = dt_temp.Rows[i][33].ToString();
                        dr["NGAYSINH"] = ngaysinh;
                        dr["GIOITINH"] = Convert.ToBoolean(gioitinh);
                        dr["LINHVUC"] = "";
                        dr["WEBSITE"] = "";
                        dr["GPDK"] = dt_temp.Rows[i][31].ToString();
                        dr["QDTL"] = dt_temp.Rows[i][30].ToString();
                        dr["MST"] = dt_temp.Rows[i][45].ToString();
                        dr["LOAIKH"] = loaikh;
                        dr["THUNHAP"] = 0;
                        dr["SOTHICH"] = "";
                        dr["MANV"] = "";
                        dr["NHGIAODICH"] = "";
                        dr["GHICHU"] = "";
                        dr["MACN"] = dt_temp.Rows[i][0].ToString().Substring(0, 4);
                        dr["TINHTRANG"] = true;
                        dr["CTLOAIKH"] = dt_temp.Rows[i][8].ToString();
                        dr["TINH"] = dt_temp.Rows[i][46].ToString();
                        dr["HUYEN"] = dt_temp.Rows[i][47].ToString();
                        dr["XA"] = dt_temp.Rows[i][48].ToString();
                        dr["LOAIKH_IPCAS"] = dt_temp.Rows[i][7].ToString(); // "Tổ chức"
                        dr["NGAYKETHON"] = "01/01/1900";
                        dr["NGAYTHANHLAP"] = "01/01/1900";
                        dr["NGAYTAO"] = ngaytao;
                        dr["DOITUONGKH"] = "35";
                        dr["DOITUONGDN"] = "";
                        dr["VONDAUTU"] = 0;
                        dr["SOLAODONG"] = 0;
                        dr["DSXNK"] = 0;
                        dr["NGAYTLNGANH"] = "01/01/1900";
                        dt_temp2.Rows.Add(dr);
                        //try
                        //{
                        //    String strngaytao = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
                        //    qyery_temp = "INSERT INTO KHACHHANG(MAKH,Hoten,Diachi1,Diachi2,dienthoai1,CMND,Ngaycap,noicap,ngaysinh,gioitinh,GPDK,QDTL,MST,LOAIKH,tinhtrang,ctloaikh,tinh,huyen,xa,macn,LOAIKH_IPCAS,ngaytao,doituongkh) Values ('" + dt_temp.Rows[i][0].ToString() + "'," + "N'" + hoten + "',N'" + diachi1 + "',N'" + diachi2 + "','" + didong + "','" + dt_temp.Rows[i][14].ToString() + "','" + ngaycap + "',N'" + dt_temp.Rows[i][33].ToString() + "','" + ngaysinh + "','" + gioitinh + "','" + dt_temp.Rows[i][31].ToString() + "','" + dt_temp.Rows[i][30].ToString() + "','" + dt_temp.Rows[i][45].ToString() + "','" + loaikh + "',1" + ",N'" + dt_temp.Rows[i][8].ToString() + "',N'" + dt_temp.Rows[i][46].ToString() + "',N'" + dt_temp.Rows[i][47].ToString() + "',N'" + dt_temp.Rows[i][48].ToString() + "','" + dt_temp.Rows[i][0].ToString().Substring(0, 4) + "',N'Tổ chức','"+strngaytao+"','35')";
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
                        //    qyery_temp = "UPDATE KHACHHANG  set hoten= N'" + hoten + "',diachi1= N'" + diachi1 + "',diachi2=N'" + diachi2 + "',dienthoai1='" + didong + "',Ngaysinh='" + ngaysinh + "' where makh='" + dt_temp.Rows[i][0].ToString() + "'";
                        //    DataAccess.conn.Open();
                        //    frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                        //    frmMain.myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                    }
                }
                catch
                { }
            }
            //Xóa các dòng có cùng mã khách hàng
            dt_temp2 = CommonMethod.RemoveDuplicateRows(dt_temp2, "MAKH");

            //Nhập thông tin vào bảng KHACHHANG
            if (khachhang_bus.UPDATE_KHACHHANG(dt_temp2, Thongtindangnhap.user_id))
            {
                //MessageBox.Show("Nhập thông tin khách hàng Tổ chức tháng " + dtpThang.Text + " thành công.");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi nhập thông tin khách hàng Tổ chức tháng " + dtpThang.Text);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}