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
    public partial class frmImport : Form
    {
        MicrosoftExcelClient m_ExcelClient = null;
        private DataTable dtResult = new DataTable();
        string strCmd = "";
        ArrayList arrMaDM, arrTien;
        
        public frmImport()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            cbbTien.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbTen.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbTien.Text = "VND";

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
        
        private void frmImport_Load(object sender, EventArgs e)
        {            
            ds_Tiente();
            layDS_Import();
        }

        private void ds_Tiente()
        {
            arrTien = new ArrayList();

            cbbTien.Items.Clear();
            cbbTien.Items.Add("USD");
            arrTien.Add("0");
            cbbTien.Items.Add("VND");
            arrTien.Add("1");

            cbbTien.SelectedIndex = 1;
        }

        private void layDS_Import()
        {
            arrMaDM = new ArrayList();
            
            cbbTen.Items.Clear();
            cbbTen.Refresh();

            strCmd = "SELECT * FROM DMIMPORT WHERE LoaiDM='2' ";
            
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
            openFileDialog1.FileName = "SDBQ.xls";
            openFileDialog1.Filter = "SDBQ(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp;
            DataTable dt_tygia = new DataTable();
            String filename = "", CCY = "VND";
            decimal tygia = 1;
            if (cbbTien.Text == "VND")
            {
                filename = "SDBQ" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.XLS";
            }
            else
            {
                filename = "SDBQ" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "USD.XLS";
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                Cursor.Current = Cursors.WaitCursor;
                if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 15, 15).ToUpper() != filename.ToUpper())
                {
                    MessageBox.Show("Chọn sai tên file import!");
                    return;

                }
                else
                {
                    if (cbbTien.Text == "USD")
                    {
                        try
                        {
                            CCY = "USD";
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
                        }
                        catch
                        {
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                        }
                    }
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','" + arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() + "','" + dtpThang.Text + "')";
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

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        //MessageBox.Show("Du lieu nay da duoc import!");   
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
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

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

                Cursor.Current = Cursors.Default;
            }
        }

        //Dua du lieu vao table SDBQCT
        private void InsertSDBQCT()
        {
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
                //Dua du lieu vao bang sdbqct
                try
                {
                    sCommand = "INSERT INTO SDBQCT(MAKH,THANG,SDBQ,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + sdbq + "," + loaikh + ")";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }

                    sCommand = "Update SDBQCT set SDBQ =" + sdbq + " where makh='" + makh + "'";
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
            }
        }

        private void lay_SKTG()
        {
            openFileDialog1.FileName = "SK.xls";
            openFileDialog1.Filter = "SK(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp;
            DataTable dt_tygia = new DataTable();
            String filename = "", CCY = "VND";
            decimal tygia = 1;
            if (cbbTien.Text == "VND")
            {
                filename = "SK" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.XLS";
            }
            else
            {
                filename = "SK" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "USD.XLS";
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 13, 13).ToUpper() != filename.ToUpper())
                {
                    MessageBox.Show("Chọn sai tên file import!");
                    return;
                }
                else
                {
                    if (cbbTien.Text == "USD")
                    {
                        try
                        {
                            CCY = "USD";
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
                        }
                        catch
                        {
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                        }
                    }
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','" + arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() + "','" + dtpThang.Text + "')";
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

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        //MessageBox.Show("Du lieu nay da duoc import!");   
                    }
                }
                //Xoa du lieu dang co trong table SDBQ
                try
                {
                    qyery_temp = "Delete SKTIENGUI where left(makh,4)='" + Thongtindangnhap.macn + "' and ccy='" + CCY + "'";
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
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    try
                    {
                        if ((dt_temp.Rows[i][18].ToString().Substring(0, 3) != "670") && (dt_temp.Rows[i][18].ToString().Substring(0, 3) != "671"))
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
                                qyery_temp = "INSERT INTO SKTIENGUI(MAKH,SOTK,CCY,NGAYMO,NGAYDENHAN,SOTIEN,SOTIENHOA,MANV,CMND) Values ('" + makh + "','" + dt_temp.Rows[i][8].ToString() + "','" + CCY + "','" + ngaymo + "','" + ngaydh + "'," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) + "," + Convert.ToDecimal(dt_temp.Rows[i][6].ToString()) * tygia + ",'" + dt_temp.Rows[i][37].ToString() + "','" + dt_temp.Rows[i][26].ToString() + "')";
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

                Cursor.Current = Cursors.Default;
            }
        }
        //Dua du lieu vao table TGGTCT
        private void InsertTGGTCT()
        {
            //Cap nhat so thang gui cua tung so tiet kiem
            DataTable dt = new DataTable();
            String makh = "";
            byte loaikh;
            int thang;
            String sCommand = "Update SKTIENGUI set Thang =datediff(month,ngaymo,ngaydenhan) where ngaydenhan <>'01/01/1900' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();

            sCommand = "SELECT SKTIENGUI.*,LOAIKH from SKTIENGUI,KHACHHANG where SKTIENGUI.MAKH=KHACHHANG.MAKH and SKTIENGUI.NGAYDENHAN<>'01/01/1900' and left(SKTIENGUI.MAKH,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

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
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }

                    sCommand = "Update TGGTCT set THOIGIAN=" + thang + " where sotk='" + dt.Rows[i]["SOTK"].ToString() + "' and Thang = '" + dtpThang.Text + "'";
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }

            }
        }

        private void lay_Chuyentiendi()
        {
            openFileDialog1.FileName = "FXDI.xls";
            openFileDialog1.Filter = "FXDI(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp;
            DataTable dt_tygia = new DataTable();
            String filename = "", CCY = "VND";
            decimal tygia = 1;
            if (cbbTien.Text == "VND")
            {
                filename = "FXDI" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.XLS";
            }
            else
            {
                filename = "FXDI" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "USD.XLS";
            }
                        
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 15, 15).ToUpper() != filename.ToUpper())
                {
                    MessageBox.Show("Chọn sai tên file import!");
                    return;
                }
                else
                {
                    if (cbbTien.Text == "USD")
                    {
                        try
                        {
                            CCY = "USD";
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
                        }
                        catch
                        {
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                        }
                    }
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','" + arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() + "','" + dtpThang.Text + "')";
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

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không?", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        //MessageBox.Show("Du lieu nay da duoc import!");   
                    }
                }
                //Xoa du lieu trong table CHUYENTIEN
                try
                {
                    qyery_temp = "DELETE CHUYENTIEN WHERE LOAICHUYENTIEN=1 and MaCN='" + Thongtindangnhap.macn + "' and ccy='" + CCY + "' ";
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
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

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
                                makh = Thongtindangnhap.macn + makh;
                                qyery_temp = "INSERT INTO CHUYENTIEN(ID,MAKH,SOTIEN,LOAICHUYENTIEN,HOTEN,CCY,SOTIENHOA,MACN) Values ('" + ID + "','" + makh + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",1,N'" + dt_temp.Rows[i][5].ToString() + "','" + CCY + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) * tygia + ",'" + Thongtindangnhap.macn + "')";
                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                                DataAccess.conn.Open();
                                frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                DataAccess.conn.Close();
                            }
                            else
                            //Dua cac khach hang chua co makh vao danh sach khach hang tiem nang
                            {

                                if (Char.IsNumber(Convert.ToChar(dt_temp.Rows[i][43].ToString().Substring(0, 1))))
                                {
                                    try
                                    {
                                        makh = "T" + Thongtindangnhap.macn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                        qyery_temp = "INSERT INTO KHACHHANGCHUYENTIEN(MAKH,HOTEN,CMND,MACN,GHICHU,TINHTRANG,LOAIKH) Values ('" + makh + "',N'" + dt_temp.Rows[i][5].ToString() + "','" + dt_temp.Rows[i][43].ToString().Substring(0, 9) + "','" + Thongtindangnhap.macn + "',N'Khách hàng giao dịch chuyển tiền',1,1)";
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
                            Thread.Sleep(1);
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

                Cursor.Current = Cursors.Default;
            }
        }

        private void lay_Chuyentienden()
        {
            openFileDialog1.FileName = "FXDEN.xls";
            openFileDialog1.Filter = "FXDEN(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp;
            DataTable dt_tygia = new DataTable();
            String filename = "", CCY = "VND";
            decimal tygia = 1;
            if (cbbTien.Text == "VND")
            {
                filename = "FXDEN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.XLS";
            }
            else
            {
                filename = "FXDEN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "USD.XLS";
            }
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 16, 16).ToUpper() != filename.ToUpper())
                {
                    MessageBox.Show("Chọn sai tên file import!");
                    return;

                }
                else
                {
                    if (cbbTien.Text == "USD")
                    {
                        try
                        {
                            CCY = "USD";
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
                        }
                        catch
                        {
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                        }
                    }
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','" + arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() + "','" + dtpThang.Text + "')";
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

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không?", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        //MessageBox.Show("Du lieu nay da duoc import!");   
                    }
                }
                //Xoa du lieu trong table CHUYENTIEN
                try
                {
                    qyery_temp = "DELETE CHUYENTIEN WHERE LOAICHUYENTIEN=2 and MaCN='" + Thongtindangnhap.macn + "' and ccy='" + CCY + "' ";
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
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    try
                    {
                        if (dt_temp.Rows[i][0].ToString() != null)
                        {

                            String benacc = dt_temp.Rows[i][7].ToString();
                            //Dua cac khach hang chuyen tien da co makh va danh sach chuyen tien de tinh diem
                            if (benacc.Substring(0, 4) == Thongtindangnhap.macn)
                            {
                                String ID = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                benacc = benacc.Replace(" ", "").Replace(".", "");
                                DataTable dt1 = new DataTable();
                                String sCommand = "SELECT makh from SKTIENGUI where sotk='" + benacc + "'";

                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                                DataAccess.conn.Open();
                                new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                                DataAccess.conn.Close();

                                String makh = "";
                                int j = dt1.Rows.Count;
                                if (j != 0)
                                {
                                    makh = dt1.Rows[j - 1]["makh"].ToString();
                                }
                                qyery_temp = "INSERT INTO CHUYENTIEN(ID,MAKH,SOTK,SOTIEN,LOAICHUYENTIEN,HOTEN,CCY,SOTIENHOA,MACN) Values ('" + ID + "','" + makh + "','" + benacc + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) + ",2,N'" + dt_temp.Rows[i][6].ToString() + "','" + CCY + "'," + Convert.ToDecimal(dt_temp.Rows[i][3].ToString()) * tygia + ",'" + Thongtindangnhap.macn + "')";
                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                                DataAccess.conn.Open();
                                frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                DataAccess.conn.Close();
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
                                            makh = "T" + Thongtindangnhap.macn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                            qyery_temp = "INSERT INTO KHACHHANGCHUYENTIEN(MAKH,HOTEN,CMND,NHGIAODICH,MACN,GHICHU,TINHTRANG,LOAIKH) Values ('" + makh + "',N'" + dt_temp.Rows[i][6].ToString() + "','" + dt_temp.Rows[i][60].ToString().Substring(0, 9) + "','" + dt_temp.Rows[i][47].ToString() + "','" + Thongtindangnhap.macn + "',N'Khách hàng giao dịch chuyển tiền',1,1)";
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
                            }
                            Thread.Sleep(1);
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

                Cursor.Current = Cursors.Default;
            }
        }

        private void lay_SMS()
        {
            openFileDialog1.FileName = "SMS.xls";
            openFileDialog1.Filter = "SMS(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp, filename = "";
            filename = "SMS" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Cursor.Current = Cursors.WaitCursor;
                if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 11, 11).ToUpper() != filename.ToUpper())
                {
                    MessageBox.Show("Chọn sai tên file import!");
                    return;

                }
                else
                {
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','" + arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() + "','" + dtpThang.Text + "')";
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

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không?", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        //MessageBox.Show("Du lieu nay da duoc import!");   
                    }
                }
                //Xoa du lieu dang co trong table SMS
                try
                {
                    qyery_temp = "Delete SMS where left(makh,4)='" + Thongtindangnhap.macn + "'";
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
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {

                    String makh = dt_temp.Rows[i][0].ToString() + dt_temp.Rows[i][1].ToString();
                    String ngaydk = dt_temp.Rows[i][16].ToString().Substring(3, 2) + "/" + dt_temp.Rows[i][16].ToString().Substring(0, 2) + "/" + dt_temp.Rows[i][16].ToString().Substring(6, 4);
                    //Dua cac khach hang su dung sms vao danh sach sms
                    try
                    {
                        if (makh.Substring(0, 4) == Thongtindangnhap.macn)
                        {
                            qyery_temp = "INSERT INTO SMS(MAKH,NGAYDK,HIENTRANG) Values ('" + makh + "','" + ngaydk + "',1)";
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
                MessageBox.Show("Đã import dữ liệu xong!");

                Cursor.Current = Cursors.Default;
            }
        }

        private void lay_Dien()
        {
            openFileDialog1.FileName = "DIEN.xls";
            openFileDialog1.Filter = "DIEN(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp, filename = "";
            filename = "DIEN" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Cursor.Current = Cursors.WaitCursor;
                if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 12, 12).ToUpper() != filename.ToUpper())
                {
                    MessageBox.Show("Chọn sai tên file import!");
                    return;

                }
                else
                {
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','" + arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() + "','" + dtpThang.Text + "')";
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

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không?", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        //MessageBox.Show("Du lieu nay da duoc import!");   
                    }
                }
                //Xoa du lieu dang co trong table DIEN
                try
                {
                    qyery_temp = "Delete DIEN where left(makh,4)='" + Thongtindangnhap.macn + "'";
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
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

                if (dt_temp.Rows.Count <= 0)
                    MessageBox.Show("File DIEN khong co du lieu, de nghi xem lai!");
                else
                {
                    for (int i = 0; i < dt_temp.Rows.Count; i++)
                    {

                        String sotk = dt_temp.Rows[i][5].ToString();
                        DataTable dt = new DataTable();
                        String strCmd = "SELECT makh from SKTIENGUI where left(makh,4)='" + frmDangnhap.macn + "' and sotk='" + sotk + "'";
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
                                qyery_temp = "INSERT INTO DIEN(SOTK,MAKH) Values ('" + sotk + "','" + makh + "')";
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
                    MessageBox.Show("Đã import dữ liệu xong!");
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void lay_VNPT()
        {
            openFileDialog1.FileName = "VNPT.xls";
            openFileDialog1.Filter = "VNPT(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp, filename = "";
            filename = "VNPT" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Cursor.Current = Cursors.WaitCursor;
                if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 12, 12).ToUpper() != filename.ToUpper())
                {
                    MessageBox.Show("Chọn sai tên file import!");
                    return;

                }
                else
                {
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','" + arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() + "','" + dtpThang.Text + "')";
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

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không?", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        //MessageBox.Show("Du lieu nay da duoc import!");   
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
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

                if (dt_temp.Rows.Count <= 0)
                    MessageBox.Show("File VNPT khong co du lieu, de nghi xem lai!");
                else
                {
                    for (int i = 0; i < dt_temp.Rows.Count; i++)
                    {

                        String sotk = dt_temp.Rows[i][5].ToString();
                        DataTable dt = new DataTable();
                        String strCmd = "SELECT makh from SKTIENGUI where left(makh,4)='" + frmDangnhap.macn + "' and sotk='" + sotk + "'";
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
                    MessageBox.Show("Đã import dữ liệu xong!");
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void lay_The()
        {
            openFileDialog1.FileName = "THE.xls";
            openFileDialog1.Filter = "THE(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp, filename = "";
            filename = "THE" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Cursor.Current = Cursors.WaitCursor;
                if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 11, 11).ToUpper() != filename.ToUpper())
                {
                    MessageBox.Show("Chọn sai tên file import!");
                    return;

                }
                else
                {
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','" + arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() + "','" + dtpThang.Text + "')";
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

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không?", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        //MessageBox.Show("Du lieu nay da duoc import!");   
                    }
                }
                //Xoa du lieu dang co trong table THE
                try
                {
                    qyery_temp = "Delete THE where left(makh,4)='" + Thongtindangnhap.macn + "'";
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
                //Dua du lieu vao bang The
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

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
                MessageBox.Show("Đã import dữ liệu xong!");

                Cursor.Current = Cursors.Default;
            }
        }

        private void lay_Profit()
        {
            openFileDialog1.FileName = "PROFIT.xls";
            openFileDialog1.Filter = "PROFIT(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp;
            DataTable dt_tygia = new DataTable();
            String filename = "", CCY = "VND";
            decimal tygia = 1;
            if (cbbTien.Text == "VND")
            {
                filename = "PROFIT" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "VND.XLS";
            }
            else
            {
                filename = "PROFIT" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + "USD.XLS";
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                Cursor.Current = Cursors.WaitCursor;
                if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 17, 17).ToUpper() != filename.ToUpper())
                {
                    MessageBox.Show("Chọn sai tên file import!");
                    return;

                }
                else
                {
                    if (cbbTien.Text == "USD")
                    {
                        try
                        {
                            CCY = "USD";
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
                        }
                        catch
                        {
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                        }
                    }
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','" + CCY + "','" + arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() + "','" + dtpThang.Text + "')";
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

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không?", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        //MessageBox.Show("Du lieu nay da duoc import!");   
                    }
                }
                //Xoa du lieu dang co trong table SDBQ
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
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

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
                            scommand = "select * from SDBQ where makh='" + makh + "' and ccy='" + CCY + "'";
                            dt_a.Clear();
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            new SqlDataAdapter(scommand, DataAccess.conn).Fill(dt_a);
                            DataAccess.conn.Close();

                            if (dt_a.Rows.Count > 0)
                            {
                                qyery_temp = "UPDATE SDBQ set PROFITRATIO=" + Convert.ToDecimal(profit) + ",PROFITVND=" + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + " where makh ='" + makh + "' and ccy='" + CCY + "'";
                                if (DataAccess.conn.State == ConnectionState.Open)
                                {
                                    DataAccess.conn.Close();
                                }
                                DataAccess.conn.Open();
                                frmMain.myCommand = new SqlCommand(qyery_temp, DataAccess.conn);
                                frmMain.myCommand.ExecuteNonQuery();
                                DataAccess.conn.Close();
                            }
                            else
                            {
                                qyery_temp = "INSERT INTO SDBQ(MAKH,CCY,PROFITRATIO,PROFITVND) Values ('" + makh + "','" + CCY + "'," + Convert.ToDecimal(profit) + "," + Convert.ToDecimal(dt_temp.Rows[i][13].ToString()) + ")";
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

                Cursor.Current = Cursors.Default;
            }
        }
        private void InsertProfitCT()
        {
            DataTable dt = new DataTable();
            String makh = "";
            decimal profit = 0;
            byte loaikh;
            String sCommand = "SELECT SDBQ.MAKH,PROFITRATIO,KHACHHANG.LOAIKH from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH  and PROFITRATIO<=100 and left(SDBQ.MAKH,4)='" + Thongtindangnhap.macn + "'";
            //So lieu thang 07/2012 profitvnd=0
            //sCommand = "SELECT SDBQ.MAKH,SUM(PROFITVND*PROFITRATIO)/SUM(PROFITVND) as PROFITRATIO,KHACHHANG.LOAIKH from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH and left(SDBQ.MAKH,4)='" + Thongtindangnhap.macn + "' group by sdbq.makh,khachhang.loaikh having sum(profitvnd)>0";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                makh = dt.Rows[i]["MAKH"].ToString();
                loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                profit = Convert.ToDecimal(dt.Rows[i]["PROFITRATIO"].ToString());
                //Dua du lieu vao bang PROFITCT
                try
                {
                    sCommand = "INSERT INTO PROFITCT(MAKH,THANG,PROFIT,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + profit + "," + loaikh + ")";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
                catch
                {
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }

                    sCommand = "Update PROFITCT set PROFIT =" + profit + " where makh='" + makh + "'";
                    DataAccess.conn.Open();
                    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    frmMain.myCommand.ExecuteNonQuery();
                    DataAccess.conn.Close();
                }
            }
        }
        private void lay_Luong()
        {
            openFileDialog1.FileName = "LUONG.xls";
            openFileDialog1.Filter = "LUONG(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp, filename = "";
            filename = "LUONG" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Cursor.Current = Cursors.WaitCursor;
                if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 13, 13).ToUpper() != filename.ToUpper())
                {
                    MessageBox.Show("Chọn sai tên file import!");
                    return;

                }
                else
                {
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','" + arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() + "','" + dtpThang.Text + "')";
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

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không?", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        //MessageBox.Show("Du lieu nay da duoc import!");   
                    }
                }
                //Xoa du lieu dang co trong table LUONG
                try
                {
                    qyery_temp = "Delete CHUYENLUONG where left(makh,4)='" + Thongtindangnhap.macn + "'";
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
                //Dua du lieu vao bang CHUYENLUONG
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

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
                MessageBox.Show("Đã import dữ liệu xong!");

                Cursor.Current = Cursors.Default;
            }
        }

        private void lay_WU()
        {
            openFileDialog1.FileName = "WU.xls";
            openFileDialog1.Filter = "WU(*.xls)|*.xls|Tất cả (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            String qyery_temp;
            String filename = "";
            filename = "WU" + dtpThang.Text.Substring(0, 2) + dtpThang.Text.Substring(5, 2) + ".XLS";
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 10, 10).ToUpper() != filename.ToUpper())
                {
                    MessageBox.Show("Chọn sai tên file import!");
                    return;

                }
                else
                {
                    try
                    {
                        qyery_temp = "INSERT INTO CAPNHAT(MACN,CCY,LOAI,THANG) Values ('" + Thongtindangnhap.macn + "','VND','" + arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() + "','" + dtpThang.Text + "')";
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

                        if (MessageBox.Show("Dữ liệu này đã được import! Import lại không? ", "Import du lieu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        //MessageBox.Show("Du lieu nay da duoc import!");   
                    }
                }
                //Xoa du lieu dang co trong table WU
                try
                {
                    qyery_temp = "Delete WU where MaCN='" + Thongtindangnhap.macn + "' and Thang='" + dtpThang.Text + "'";
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
                DataTable dt_temp = read_excel(openFileDialog1.FileName);

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
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                            DataAccess.conn.Close();

                            String makh = "";
                            int j = dt1.Rows.Count;
                            if (j != 0)
                            {
                                makh = dt1.Rows[j - 1]["makh"].ToString();
                            }

                            try
                            {
                                qyery_temp = "INSERT INTO WU(ID,MAKH,HOTEN,DIACHI,CMND,SOTIEN,CCY,NGAYNHAN,MACN,THANG,DIENTHOAI,MTCN,HOTEN_GUI,SOTIEN_GUI,CCY_GUI) ";
                                qyery_temp += " Values ('" + ID + "','" + makh + "',N'" + hoten + "',N'" + diachi + "','" + dt_temp.Rows[i][24].ToString() + "','" + Convert.ToDecimal(dt_temp.Rows[i][39].ToString()) + "','" + dt_temp.Rows[i][33].ToString() + "','" + ngaynhan + "','" + Thongtindangnhap.macn + "','" + dtpThang.Text + "','";
                                qyery_temp += dt_temp.Rows[i][20].ToString() + "','" + dt_temp.Rows[i][6].ToString() + "','" + hoten_gui + "','" + Convert.ToDecimal(dt_temp.Rows[i][28].ToString()) + "','" + dt_temp.Rows[i][32].ToString() + "')";
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

                            if (makh == "")
                            {
                                try
                                {
                                    makh = "T" + Thongtindangnhap.macn + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                    qyery_temp = "INSERT INTO KHACHHANGCHUYENTIEN(MAKH,HOTEN,DIACHI1,CMND,MACN,GHICHU,TINHTRANG,LOAIKH) ";
                                    qyery_temp += " Values ('" + makh + "',N'" + hoten + "','" + diachi + "','" + dt_temp.Rows[i][24].ToString() + "','" + Thongtindangnhap.macn + "',N'Khách hàng nhận tiền WU',1,1)";
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
                MessageBox.Show("Đã import dữ liệu xong!");

                Cursor.Current = Cursors.Default;
            }
        }
                
        private void btnImport_Click(object sender, EventArgs e)
        {
            int ispdv = 0;
            if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "SDBQ_THANG")
            {
                lay_SDBQ();
                InsertSDBQCT();
            }
            else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "SKTIENGUI_THANG")
            {
                lay_SKTG();
                InsertTGGTCT();
            }
            else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "FXDI_THANG")
            {
                ispdv = 1;
                lay_Chuyentiendi();
            }
            else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "FXDEN_THANG")
            {
                ispdv = 1;
                lay_Chuyentienden();
            }
            else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "SMS_THANG")
            {
                ispdv = 1;
                lay_SMS();
            }
            else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "DIEN_THANG")
            {
                ispdv = 1;
                lay_Dien();
            }
            else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "VNPT_THANG")
            {
                ispdv = 1;
                lay_VNPT();
            }
            else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "THE_THANG")
            {
                ispdv = 1;
                lay_The();
            }
            else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "PROFIT_THANG")
            {
                lay_Profit();
                InsertProfitCT();
            }
            else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "LUONG_THANG")
            {
                ispdv = 1;
                lay_Luong();
            }
            else if (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "WU_THANG")
            {
                lay_WU();
            }
            if (ispdv == 1)
            {
                MessageBox.Show("Tinh toan lai san pham dich vu!");
                InsertSPDVCT();
            }
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
                sCommand = "Delete SPDV where left(makh,4)='" + Thongtindangnhap.macn + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
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
            //Xoa du lieu trong table spdvct
            try
            {
                sCommand = "Delete SPDVCT where left(makh,4)='" + Thongtindangnhap.macn + "' and thang ='" + dtpThang.Text + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
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
            dt.Clear();

            sCommand = "Select * from Dien where left(makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

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
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }

                        sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + dt.Rows[i]["makh"].ToString() + "'";
                        DataAccess.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                    }

                }
            }

            //Tinh diem su dung dich vu VNPT
            dt.Clear();
            sCommand = "Select * from VNPT where left(makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'VNPT')";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                        DataAccess.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",VNPT";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            DataAccess.conn.Close();
                        }
                    }
                }
            }

            //Tinh diem su dung dich vu SMS
            dt.Clear();
            sCommand = "Select * from SMS where left(makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'SMS')";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                        DataAccess.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",SMS";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            DataAccess.conn.Close();
                        }
                    }
                }
            }

            //Tinh diem su dung dich vu The
            dt.Clear();
            //sCommand = "Select * from THE where ngaydk<='" + ngaytinhdiem + "' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            sCommand = "Select * from THE where left(makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'THE')";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                        DataAccess.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",THE";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            DataAccess.conn.Close();
                        }
                    }
                }
            }

            //Tinh diem su dung dich vu chuyen tien
            dt.Clear();
            sCommand = "Select distinct(makh) from Chuyentien where left(makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'Chuyentien')";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                        DataAccess.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",Chuyentien";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            DataAccess.conn.Close();
                        }
                    }
                }
            }

            //Tinh diem su dung dich vu tra luong qua tai khoan
            //Tinh diem su dung dich vu chuyen tien
            dt.Clear();
            sCommand = "Select makh from Chuyenluong where hientrang=1 and left(makh,4)='" + Thongtindangnhap.macn + "'";
            //sCommand = "Select makh from Chuyenluong where hientrang=1 and ngaydk<='" + ngaytinhdiem + "' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["MAKH"].ToString();
                    try
                    {
                        sCommand = "INSERT INTO SPDV(MAKH,SLSPDV,SPDV) Values ('" + makh + "',1,'Chuyenluong')";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }

                        DataTable dt1 = new DataTable();
                        sCommand = "select * from SPDV where makh='" + makh + "'";
                        DataAccess.conn.Open();
                        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
                        DataAccess.conn.Close();

                        if (dt1.Rows.Count > 0)
                        {
                            sldichvu = Convert.ToInt16(dt1.Rows[0]["slspdv"].ToString()) + 1;
                            strdvu = dt1.Rows[0]["spdv"].ToString() + ",Chuyenluong";
                            sCommand = "Update SPDV set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                            frmMain.myCommand.ExecuteNonQuery();
                            DataAccess.conn.Close();
                        }
                    }

                }
            }

            //Dua du lieu vao table SPDVCT
            dt.Clear();
            sCommand = "Select spdv.*,loaikh from SPDV,khachhang where spdv.makh=khachhang.makh and left(spdv.makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

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
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                    }
                    catch
                    {
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }

                        sCommand = "Update SPDVCT set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "' where makh='" + makh + "'";
                        DataAccess.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        DataAccess.conn.Close();
                    }
                }
            }

        }
        private void cbbTen_TextChanged(object sender, EventArgs e)
        {
            if ((arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "SDBQ_THANG") 
                        || (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "SKTIENGUI_THANG") 
                        || (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "PROFIT_THANG") 
                        || (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "FXDI_THANG")
                        || (arrMaDM[cbbTen.Items.IndexOf(cbbTen.Text.Trim())].ToString() == "FXDEN_THANG"))
            {
                lblTien.Visible = true;
                cbbTien.Visible = true;
            }
            else
            {
                lblTien.Visible = false;
                cbbTien.Visible = false;
            }
        }

        private void btnLichsu_Click(object sender, EventArgs e)
        {
            frmImport_Lichsu frmIM_LS = new frmImport_Lichsu();
            frmIM_LS.ShowDialog();
        }

        private void frmImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            //DataAccess.conn.Close();
        }
    }
}