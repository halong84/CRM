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
//using N_MicrosoftExcelClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;

namespace CRM
{
    public partial class frmHH_ChamdiemKH : Form
    {
        //MicrosoftExcelClient m_ExcelClient = null;
        SqlCommand myCommand;
        DIEMKHBUS diemkh_bus = new DIEMKHBUS();
        SDBQCTBUS sdbqct_bus = new SDBQCTBUS();
        SPDVCTBUS spdvct_bus = new SPDVCTBUS();
        
        public frmHH_ChamdiemKH()
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

        private void frmHH_ChamdiemKH_Load(object sender, EventArgs e)
        {
            
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

        private void btnChamdiem_Click(object sender, EventArgs e)
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[10] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("THANG", typeof(string)),
                    new DataColumn("DIEM_SDBQ", typeof(int)),
                    new DataColumn("DIEM_TGG", typeof(int)),
                    new DataColumn("DIEM_SPDV", typeof(int)),
                    new DataColumn("SPDV", typeof(string)),
                    new DataColumn("DIEM_PROFIT", typeof(int)),
                    new DataColumn("TONGDIEMDL", typeof(int)),
                    new DataColumn("XEPLOAI", typeof(string)),
                    new DataColumn("LOAIKH", typeof(byte))
                }
            );
            DataRow dr2;

            DataTable dt_temp3 = new DataTable();
            dt_temp3.Columns.AddRange
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
                    new DataColumn("LOAIKH", typeof(byte))
                }
            );
            DataRow dr3;

            DataTable dt_temp4 = new DataTable();
            dt_temp4.Columns.AddRange
            (
                new DataColumn[9] 
                { 
                    new DataColumn("THANG", typeof(string)),
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("SPDV", typeof(string)),
                    new DataColumn("SLSPDV", typeof(int)),
                    new DataColumn("MACT", typeof(string)),
                    new DataColumn("DIEM", typeof(int)),
                    new DataColumn("TYTRONG", typeof(int)),
                    new DataColumn("THUCDIEM", typeof(int)),
                    new DataColumn("LOAIKH", typeof(byte)),
                }
            );
            DataRow dr4;

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            String  sCommand;          
            //Kiểm tra có dữ liệu để chấm điểm chưa
            dt.Clear();
            sCommand = "select * from sdbqnt where thang= '" + dtpThang.Text + "' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            myCommand = new SqlCommand(sCommand, DataAccess.conn);
            myCommand.CommandTimeout = 0;
            adapter.SelectCommand = myCommand;
            adapter.Fill(dt);
            
            DataAccess.conn.Close();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Chưa import dữ liệu tháng này!");
                return;
            }
            //Kiem tra thang da cham diem chua
            dt.Clear();
            sCommand = "select * from diemkh where THANG= '" + dtpThang.Text + "' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            myCommand = new SqlCommand(sCommand, DataAccess.conn);
            myCommand.CommandTimeout = 0;
            adapter.SelectCommand = myCommand;
            adapter.Fill(dt);
            DataAccess.conn.Close();
            if (dt.Rows.Count > 0)
            {
                if (MessageBox.Show("Tháng này đã chấm điểm! Chấm lại không? ", "cham diem CRM ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }                
            }
            
            Cursor.Current = Cursors.WaitCursor;
            //Tinh diem theo SDBQ 
            //string makh = ""
            string ngaytinhdiem = "";
            //Byte loaikh = 1;
            int i = 0;
            decimal sdbq = 0;
            ngaytinhdiem = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
            dt.Clear();
            dt = diemkh_bus.CREATE_DIEMKH_SDBQ(Thongtindangnhap.macn, ngaytinhdiem, dtpThang.Text);

            //sCommand = "SELECT SDBQCT.MAKH,SDBQCT.SDBQ as SOTIENHOA,KHACHHANG.LOAIKH from SDBQCT,KHACHHANG where SDBQCT.MAKH=KHACHHANG.MAKH and left(SDBQCT.MAKH,4)='" + Thongtindangnhap.macn + "' and thang ='" + dtpThang.Text + "'";
            //dt.Clear();
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //myCommand.CommandTimeout = 0;
            //adapter.SelectCommand = myCommand;
            //adapter.Fill(dt);
            //DataAccess.conn.Close();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    //Đưa dữ liệu vào bảng DIEMKH
                    dr2 = dt_temp2.NewRow();
                    dr2["MAKH"] = dt.Rows[i]["MAKH"].ToString();
                    dr2["THANG"] = dtpThang.Text;
                    dr2["DIEM_SDBQ"] = Convert.ToInt32(dt.Rows[i]["DIEM_SDBQ"].ToString());
                    dr2["DIEM_TGG"] = 0;
                    dr2["DIEM_SPDV"] = 0;
                    dr2["SPDV"] = "";
                    dr2["DIEM_PROFIT"] = 0;
                    dr2["TONGDIEMDL"] = 0;
                    dr2["XEPLOAI"] = "";
                    dr2["LOAIKH"] = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                    dt_temp2.Rows.Add(dr2);

                    //Dua du lieu vao bang sdbqct
                    dr3 = dt_temp3.NewRow();
                    dr3["THANG"] = dtpThang.Text;
                    dr3["MAKH"] = dt.Rows[i]["MAKH"].ToString();
                    dr3["SDBQ"] = 0;
                    dr3["MACT"] = dt.Rows[i]["MACT"].ToString();
                    dr3["DIEM"] = Convert.ToInt32(dt.Rows[i]["DIEM_SDBQ"].ToString()); ;
                    dr3["TYTRONG"] = 0;
                    dr3["THUCDIEM"] = 0;
                    dr3["LOAIKH"] = 1;
                    dt_temp3.Rows.Add(dr3);

                    //makh = dt.Rows[i]["MAKH"].ToString();
                    //loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                    //sdbq = Convert.ToDecimal(dt.Rows[i]["SOTIENHOA"].ToString());
                    //DataTable dt1 = new DataTable();
                    //sCommand = "select top 1 mact from dmchitieu where giatri<=" + sdbq + " and manhom ='SDBQ' and loaikh=" + loaikh + " order by giatri desc";
                    //if (DataAccess.conn.State == ConnectionState.Open)
                    //{
                    //    DataAccess.conn.Close();
                    //}
                    //DataAccess.conn.Open();
                    //myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //myCommand.CommandTimeout = 0;
                    //adapter.SelectCommand = myCommand;
                    //adapter.Fill(dt1);
                    //DataAccess.conn.Close();

                    //if (dt1.Rows.Count > 0)
                    //{


                    //DataTable dt2 = new DataTable();
                    //sCommand = "select DMDIEM.* from DMDIEM where MACT='" + dt1.Rows[0]["MACT"].ToString() + "' and loaiKH=" + loaikh + " and macn='" + Thongtindangnhap.macn + "' and ngaybdhl <= '" + ngaytinhdiem + "' and ngayhethl ='12/31/9998'";
                    //if (DataAccess.conn.State == ConnectionState.Open)
                    //{
                    //    DataAccess.conn.Close();
                    //}
                    //DataAccess.conn.Open();
                    //myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //myCommand.CommandTimeout = 0;
                    //adapter.SelectCommand = myCommand;
                    //adapter.Fill(dt2);

                    //DataAccess.conn.Close();

                    //if (dt2.Rows.Count > 0)
                    //{
                    //    int diemsdbq = 0;
                    //    diemsdbq = Convert.ToInt32(dt2.Rows[0]["DIEM"].ToString());
                    //    dr2 = dt_temp2.NewRow();
                    //    dr2["MAKH"]= makh;
                    //    dr2["THANG"]= dtpThang.Text;
                    //    dr2["DIEM_SDBQ"]= diemsdbq;
                    //    dr2["DIEM_TGG"]= 0;
                    //    dr2["DIEM_SPDV"]= 0;
                    //    dr2["SPDV"]="";
                    //    dr2["DIEM_PROFIT"]=0;
                    //    dr2["TONGDIEMDL"]=0;
                    //    dr2["XEPLOAI"]="";
                    //    dr2["LOAIKH"]= loaikh;
                    //try
                    //{
                    //    sCommand = "INSERT INTO DIEMKH(MAKH,THANG,DIEM_SDBQ,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + diemsdbq + "," + loaikh + ")";
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //    DataAccess.conn.Open();
                    //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //    frmMain.myCommand.CommandTimeout = 0;
                    //    frmMain.myCommand.ExecuteNonQuery();
                    //    DataAccess.conn.Close();
                    //}
                    //catch
                    //{
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }

                    //    sCommand = "Update DIEMKH set DIEM_SDBQ =" + diemsdbq + "where makh='" + makh + "' and thang ='" + dtpThang.Text + "'";
                    //    DataAccess.conn.Open();
                    //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //    frmMain.myCommand.CommandTimeout = 0;
                    //    frmMain.myCommand.ExecuteNonQuery();
                    //    DataAccess.conn.Close();
                    //}
                    //Dua du lieu vao bang sdbqct

                    //dr3 = dt_temp3.NewRow();
                    //dr3["THANG"] = dtpThang.Text;
                    //dr3["MAKH"] = makh;
                    //dr3["SDBQ"] = 0;
                    //dr3["MACT"] = dt1.Rows[0]["MACT"].ToString();
                    //dr3["DIEM"] = diemsdbq;
                    //dr3["TYTRONG"] = 0;
                    //dr3["THUCDIEM"] = 0;
                    //dr3["LOAIKH"] = 0;

                    //try
                    //{

                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }

                    //    sCommand = "Update SDBQCT set MACT='" + dt1.Rows[0]["MACT"].ToString() + "',DIEM=" + diemsdbq + " where makh='" + makh + "'and thang ='" + dtpThang.Text + "'";
                    //    DataAccess.conn.Open();
                    //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //    frmMain.myCommand.CommandTimeout = 0;
                    //    frmMain.myCommand.ExecuteNonQuery();
                    //    DataAccess.conn.Close();
                    //}
                    //catch { }
                    //    }
                    //}
                }

                //Cập nhật điểm SDBQ khách hàng trong bảng DIEMKH
                if (diemkh_bus.UPDATE_DIEMKH(dt_temp2))
                {
                    //MessageBox.Show("Cập nhật điểm SDBQ khách hàng tháng " + dtpThang.Text + " vào bảng DIEMKH thành công");
                }
                else
                {
                    //MessageBox.Show("Có lỗi xảy ra khi cập nhật điểm SDBQ khách hàng tháng " + dtpThang.Text + " vào bảng DIEMKH");
                }
                //Cập nhật bảng SDBQCT
                if (sdbqct_bus.UPDATE_SDBQCT_DIEM(dt_temp3))
                {
                    //MessageBox.Show("Cập nhật bảng SDBQCT tháng" + dtpThang.Text + " thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật bảng SDBQCT tháng" + dtpThang.Text);
                }
            }
            
            ////Tinh diem theo Thoi gian gui           

            ////Tinh diem cho cac so tiet kiem co ky han
            //dt.Clear();
            //sCommand = "SELECT * from TGGTCT where left(TGGTCT.MAKH,4)='" + Thongtindangnhap.macn + "'and thang ='" + dtpThang.Text + "'";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //for (i = 0; i < dt.Rows.Count; i++)
            //{
            //    makh = dt.Rows[i]["MAKH"].ToString();
            //    loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
            //    thang = Convert.ToInt16(dt.Rows[i]["THOIGIAN"].ToString());
            //    DataTable dt1 = new DataTable();
            //    sCommand = "select top 1 mact from dmchitieu where giatri<=" + thang + " and manhom ='TGGT' and loaikh=" + loaikh + " order by giatri desc";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //    DataAccess.conn.Close();

            //    if (dt1.Rows.Count > 0)
            //    {

            //        DataTable dt2 = new DataTable();
            //        sCommand = "select DMDIEM.* from DMDIEM where MACT='" + dt1.Rows[0]["MACT"].ToString() + "' and loaiKH=" + loaikh + " and macn='" + Thongtindangnhap.macn + "' and ngaybdhl <= '" + ngaytinhdiem + "' and ngayhethl ='12/31/9998'";
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }
            //        DataAccess.conn.Open();
            //        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt2);
            //        DataAccess.conn.Close();

            //        if (dt2.Rows.Count > 0)
            //        {
            //            decimal diem = 0;
            //            diem = Convert.ToDecimal(dt2.Rows[0]["DIEM"].ToString());
                        
            //            //Dua du lieu vao bang TGGTCT
            //            try
            //            {

            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }

            //                sCommand = "Update TGGTCT set MACT='" + dt1.Rows[0]["MACT"].ToString() + "',DIEM=" + diem + " where sotk='" + dt.Rows[i]["SOTK"].ToString() + "'and thang ='" + dtpThang.Text + "'";
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //            catch { }
            //        }
            //    }
            //}

            ////Tinh diem binh quan theo tung khach hang theo thoi gian gui tien
            //dt.Clear();
            //sCommand = "select makh,avg(diem)as diem from TGGTCT where diem>0 and thang ='" + dtpThang.Text + "' and left(makh,4)='" + Thongtindangnhap.macn + "' group by makh";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //for (i = 0; i < dt.Rows.Count; i++)
            //{
            //    try
            //    {
            //        sCommand = "INSERT INTO DIEMKH(MAKH,THANG,DIEM_TGG,LOAIKH) Values ('" + dt.Rows[i]["makh"].ToString() + "','" + dtpThang.Text + "'," + Convert.ToDecimal(dt.Rows[i]["diem"].ToString()) + "," + loaikh + ")";
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

            //        sCommand = "Update DIEMKH set DIEM_TGG =" + Convert.ToDecimal(dt.Rows[i]["diem"].ToString()) + "where makh='" + dt.Rows[i]["makh"].ToString() + "'and thang ='" + dtpThang.Text + "'";
            //        DataAccess.conn.Open();
            //        frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //        frmMain.myCommand.ExecuteNonQuery();
            //        DataAccess.conn.Close();
            //    }
            //}

            //Tinh diem theo so luong dich vu su dung            
            //Tinh diem khach hang su dung dich vu dua vao table diemkh
            dt.Clear();
            dt_temp2.Clear();
            dt_temp3.Clear();
            dt = diemkh_bus.CREATE_DIEMKH_SPDV(Thongtindangnhap.macn, ngaytinhdiem, dtpThang.Text);
            //sCommand = "Select spdvct.* from SPDVCT where left(spdvCT.makh,4)='" + Thongtindangnhap.macn + "'and thang ='" + dtpThang.Text + "'";
            //int sldichvu = 0;
            //string strdvu = "";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //myCommand.CommandTimeout = 0;
            //adapter.SelectCommand = myCommand;
            //adapter.Fill(dt);           
            //DataAccess.conn.Close();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    //Đưa dữ liệu vào bảng DIEMKH
                    dr2 = dt_temp2.NewRow();
                    dr2["MAKH"] = dt.Rows[i]["MAKH"].ToString();
                    dr2["THANG"] = dtpThang.Text;
                    dr2["DIEM_SDBQ"] = 0;
                    dr2["DIEM_TGG"] = 0;
                    dr2["DIEM_SPDV"] = Convert.ToInt32(dt.Rows[i]["DIEM_SPDV"].ToString());
                    dr2["SPDV"] = dt.Rows[i]["SPDV"].ToString();
                    dr2["DIEM_PROFIT"] = 0;
                    dr2["TONGDIEMDL"] = 0;
                    dr2["XEPLOAI"] = "";
                    dr2["LOAIKH"] = Convert.ToByte(dt.Rows[i]["loaikh"].ToString());
                    dt_temp2.Rows.Add(dr2);

                    //Đưa dữ liệu vào bảng SPDVCT
                    dr4 = dt_temp4.NewRow();
                    dr4["THANG"] = dtpThang.Text;
                    dr4["MAKH"] = dt.Rows[i]["MAKH"].ToString();
                    dr4["SPDV"] = dt.Rows[i]["SPDV"].ToString();
                    dr4["SLSPDV"] = Convert.ToInt16(dt.Rows[i]["SLSPDV"].ToString());
                    dr4["MACT"] = dt.Rows[i]["MACT"].ToString();
                    dr4["DIEM"] = Convert.ToInt32(dt.Rows[i]["DIEM_SPDV"].ToString());
                    dr4["TYTRONG"] = 0;
                    dr4["THUCDIEM"] = 0;
                    dr4["LOAIKH"] = 1;
                    dt_temp4.Rows.Add(dr4);

                    //makh = dt.Rows[i]["makh"].ToString();
                    //loaikh = Convert.ToByte(dt.Rows[i]["loaikh"].ToString());
                    //sldichvu = Convert.ToInt16(dt.Rows[i]["slspdv"].ToString());
                    //strdvu = dt.Rows[i]["spdv"].ToString();
                    //DataTable dt1 = new DataTable();
                    //sCommand = "select top 1 mact from dmchitieu where giatri<=" + sldichvu + " and manhom ='SPDV' and loaikh=" + loaikh + " order by giatri desc";
                    //if (DataAccess.conn.State == ConnectionState.Open)
                    //{
                    //    DataAccess.conn.Close();
                    //}
                    //DataAccess.conn.Open();
                    //myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //myCommand.CommandTimeout = 0;
                    //adapter.SelectCommand = myCommand;
                    //adapter.Fill(dt1);
                    //DataAccess.conn.Close();

                    //if (dt1.Rows.Count > 0)
                    //{
                    //    DataTable dt2 = new DataTable();
                    //    sCommand = "select DMDIEM.* from DMDIEM where MACT='" + dt1.Rows[0]["MACT"].ToString() + "' and loaiKH=" + loaikh + " and macn='" + Thongtindangnhap.macn + "' and ngaybdhl <= '" + ngaytinhdiem + "' and ngayhethl ='12/31/9998'";
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //    DataAccess.conn.Open();
                    //    myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //    myCommand.CommandTimeout = 0;
                    //    adapter.SelectCommand = myCommand;
                    //    adapter.Fill(dt2);
                    //    DataAccess.conn.Close();

                    //    if (dt2.Rows.Count > 0)
                    //    {
                    //        decimal diemdv = 0;
                    //        diemdv = Convert.ToDecimal(dt2.Rows[0]["DIEM"].ToString());

                    //        dr2 = dt_temp2.NewRow();
                    //        dr2["MAKH"] = makh;
                    //        dr2["THANG"] = dtpThang.Text;
                    //        dr2["DIEM_SDBQ"] = 0;
                    //        dr2["DIEM_TGG"] = 0;
                    //        dr2["DIEM_SPDV"] = diemdv;
                    //        dr2["SPDV"] = strdvu;
                    //        dr2["DIEM_PROFIT"] = 0;
                    //        dr2["TONGDIEMDL"] = 0;
                    //        dr2["XEPLOAI"] = "";
                    //        dr2["LOAIKH"] = loaikh;

                    //        //try
                    //        //{
                    //        //    sCommand = "INSERT INTO DIEMKH(MAKH,THANG,DIEM_SPDV,SPDV,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + diemdv + ",'" + strdvu + "'," + loaikh + ")";
                    //        //    if (DataAccess.conn.State == ConnectionState.Open)
                    //        //    {
                    //        //        DataAccess.conn.Close();
                    //        //    }
                    //        //    DataAccess.conn.Open();
                    //        //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //        //    frmMain.myCommand.CommandTimeout = 0;
                    //        //    frmMain.myCommand.ExecuteNonQuery();
                    //        //    DataAccess.conn.Close();
                    //        //}
                    //        //catch
                    //        //{
                    //        //    if (DataAccess.conn.State == ConnectionState.Open)
                    //        //    {
                    //        //        DataAccess.conn.Close();
                    //        //    }

                    //        //    sCommand = "Update DIEMKH set DIEM_SPDV =" + diemdv + ",SPDV='" + strdvu + "' where makh='" + makh + "'and thang ='" + dtpThang.Text + "'";
                    //        //    DataAccess.conn.Open();
                    //        //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //        //    frmMain.myCommand.CommandTimeout = 0;
                    //        //    frmMain.myCommand.ExecuteNonQuery();
                    //        //    DataAccess.conn.Close();
                    //        //}
                    //        //Dua du lieu vao table SPDVCT

                    //        dr4 = dt_temp4.NewRow();
                    //        dr4["THANG"] = dtpThang.Text;
                    //        dr4["MAKH"] = makh;
                    //        dr4["SPDV"] = strdvu;
                    //        dr4["SLSPDV"] = sldichvu;
                    //        dr4["MACT"] = dt1.Rows[0]["MACT"].ToString();
                    //        dr4["DIEM"] = diemdv;
                    //        dr4["TYTRONG"] = 0;
                    //        dr4["THUCDIEM"] = 0;
                    //        dr4["LOAIKH"] = 0;
                          
                    //        //try
                    //        //{
                                
                    //        //    if (DataAccess.conn.State == ConnectionState.Open)
                    //        //    {
                    //        //        DataAccess.conn.Close();
                    //        //    }

                    //        //    sCommand = "Update SPDVCT set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "',MACT='" + dt1.Rows[0]["MACT"].ToString() + "',DIEM=" + diemdv + " where makh='" + makh + "'and thang ='" + dtpThang.Text + "'";
                    //        //    DataAccess.conn.Open();
                    //        //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //        //    frmMain.myCommand.CommandTimeout = 0;
                    //        //    frmMain.myCommand.ExecuteNonQuery();
                    //        //    DataAccess.conn.Close();
                    //        //}
                    //        //catch { }
                    //    }
                    //}
                }

                //Cập nhật điểm SPDV trong bảng DIEMKH
                if (diemkh_bus.UPDATE_DIEMKH_DIEMSPDV(dt_temp2))
                {
                    //MessageBox.Show("Cập nhật điểm SPDV của khách hàng tháng " + dtpThang.Text + " vào bảng DIEMKH thành công");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật điểm SPDV của khách hàng tháng " + dtpThang.Text + " vào bảng DIEMKH");
                }
                //Cập nhật bảng SPDVCT
                if (spdvct_bus.UPDATE_SPDVCT_DIEMDV(dt_temp4))
                {
                    //MessageBox.Show("Cập nhật bảng SPDVCT tháng" + dtpThang.Text + " thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật bảng SPDVCT tháng" + dtpThang.Text);
                }
            }

            ////Tinh diem theo ty suat loi nhuan            
            //dt.Clear();
            //sCommand = "SELECT * from PROFITCT where left(PROFITCT.MAKH,4)='" + Thongtindangnhap.macn + "'and thang ='" + dtpThang.Text + "'";
            ////So lieu thang 07/2012 profitvnd=0
            ////sCommand = "SELECT SDBQ.MAKH,SUM(PROFITVND*PROFITRATIO)/SUM(PROFITVND) as PROFITRATIO,KHACHHANG.LOAIKH from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH and left(SDBQ.MAKH,4)='" + Thongtindangnhap.macn + "' group by sdbq.makh,khachhang.loaikh having sum(profitvnd)>0";
            //if (DataAccess.conn.State == ConnectionState.Open)
            //{
            //    DataAccess.conn.Close();
            //}
            //DataAccess.conn.Open();
            //new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            //DataAccess.conn.Close();

            //decimal profit = 0;
            //for (i = 0; i < dt.Rows.Count; i++)
            //{
            //    makh = dt.Rows[i]["MAKH"].ToString();
            //    loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
            //    profit = Convert.ToDecimal(dt.Rows[i]["PROFIT"].ToString());
            //    DataTable dt1 = new DataTable();
            //    sCommand = "select top 1 mact from dmchitieu where giatri<=" + profit + " and manhom ='TSLN' and loaikh=" + loaikh + " order by giatri desc";
            //    if (DataAccess.conn.State == ConnectionState.Open)
            //    {
            //        DataAccess.conn.Close();
            //    }
            //    DataAccess.conn.Open();
            //    new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt1);
            //    DataAccess.conn.Close();

            //    if (dt1.Rows.Count > 0)
            //    {
            //        DataTable dt2 = new DataTable();
            //        sCommand = "select DMDIEM.* from DMDIEM where MACT='" + dt1.Rows[0]["MACT"].ToString() + "' and loaiKH=" + loaikh + " and macn='" + Thongtindangnhap.macn + "' and ngaybdhl <= '" + ngaytinhdiem + "' and ngayhethl ='12/31/9998'";
            //        if (DataAccess.conn.State == ConnectionState.Open)
            //        {
            //            DataAccess.conn.Close();
            //        }
            //        DataAccess.conn.Open();
            //        new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt2);
            //        DataAccess.conn.Close();

            //        if (dt2.Rows.Count > 0)
            //        {
            //            decimal diemTSLN = 0;
            //            diemTSLN = Convert.ToDecimal(dt2.Rows[0]["DIEM"].ToString());
            //            try
            //            {
            //                sCommand = "INSERT INTO DIEMKH(MAKH,THANG,DIEM_PROFIT,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + diemTSLN + "," + loaikh + ")";
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //            catch
            //            {
            //                if (DataAccess.conn.State == ConnectionState.Open)
            //                {
            //                    DataAccess.conn.Close();
            //                }

            //                sCommand = "Update DIEMKH set DIEM_PROFIT =" + diemTSLN + "where makh='" + makh + "'and thang ='" + dtpThang.Text + "'";
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //            //Dua du lieu vao bang PROFITCT
            //            try
            //            {
            //                sCommand = "Update PROFITCT set DIEM =" + diemTSLN + ",MACT ='" + dt1.Rows[0]["MACT"].ToString() + "' where makh='" + makh + "'and thang ='" + dtpThang.Text + "'";
            //                DataAccess.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                DataAccess.conn.Close();
            //            }
            //            catch { }

            //        }
            //    }
            //}

            //Tinh tong diem cua khach hang theo ty trong
            //Tinh tong diem cho khach hang ca nhan
            dt.Clear();
            decimal tggt = 1, spdv = 1, tsln = 1;
            sCommand = "select * from dmtytrong where loaikh=1 and ngaybatdauhl<='" + ngaytinhdiem + "' and ngayhethl='12/31/9998' and macn='"+Thongtindangnhap.macn +"'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            myCommand = new SqlCommand(sCommand, DataAccess.conn);
            myCommand.CommandTimeout = 0;
            adapter.SelectCommand = myCommand;
            adapter.Fill(dt);
            
            DataAccess.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MANHOM"].ToString() == "SDBQ")
                    {
                        sdbq = Convert.ToInt16(dt.Rows[i]["TYTRONG"].ToString());
                    }
                    if (dt.Rows[i]["MANHOM"].ToString() == "TGGT")
                    {
                        tggt = Convert.ToInt16(dt.Rows[i]["TYTRONG"].ToString());
                    }
                    if (dt.Rows[i]["MANHOM"].ToString() == "SPDV")
                    {
                        spdv = Convert.ToInt16(dt.Rows[i]["TYTRONG"].ToString());
                    }
                }
            }

            sCommand = "Update DiemKH set tongdiemdl= round((diem_sdbq* " + sdbq / 100 + ")+(diem_tgg * " + tggt / 100 + ")+(diem_spdv * " + spdv / 100 + "),0) where loaikh=1 and thang ='" + dtpThang.Text + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();

            //Tinh thuc diem cua cac bang chi tiet
            sCommand = "Update SDBQCT set tytrong= " + sdbq + ",thucdiem= round ((diem *" + sdbq / 100 + "),0) where loaikh=1 and thang ='" + dtpThang.Text + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();

            sCommand = "Update TGGTCT set tytrong= " + tggt + ",thucdiem= round ((diem *" + tggt / 100 + "),0) where loaikh=1 and thang ='" + dtpThang.Text + "'";
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();

            sCommand = "Update SPDVCT set tytrong= " + spdv + ",thucdiem= round ((diem *" + spdv / 100 + "),0) where loaikh=1 and thang ='" + dtpThang.Text + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();

            //Tinh tong diem khach hang doanh nghiep
            dt.Clear();
            sCommand = "select * from dmtytrong where loaikh=2 and ngaybatdauhl<='" + ngaytinhdiem + "' and ngayhethl='12/31/9998' and macn='"+Thongtindangnhap.macn +"'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MANHOM"].ToString() == "SDBQ")
                    {
                        sdbq = Convert.ToInt16(dt.Rows[i]["TYTRONG"].ToString());
                    }
                    if (dt.Rows[i]["MANHOM"].ToString() == "TSLN")
                    {
                        tsln = Convert.ToInt16(dt.Rows[i]["TYTRONG"].ToString());
                    }
                    if (dt.Rows[i]["MANHOM"].ToString() == "SPDV")
                    {
                        spdv = Convert.ToInt16(dt.Rows[i]["TYTRONG"].ToString());
                    }
                }
            }

            sCommand = "Update DiemKH set tongdiemdl= round ((diem_sdbq* " + sdbq / 100 + ")+(diem_profit * " + tsln / 100 + ")+(diem_spdv * " + spdv / 100 + "),0) where loaikh=2 and thang ='" + dtpThang.Text + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();

            //Tinh thuc diem cua cac bang chi tiet
            sCommand = "Update SDBQCT set tytrong= " + sdbq + ",thucdiem= round ((diem *" + sdbq / 100 + "),0) where loaikh=2 and thang ='" + dtpThang.Text + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();

            sCommand = "Update PROFITCT set tytrong= " + tsln + ",thucdiem= round ((diem *" + tsln / 100 + "),0) where loaikh=2 and thang ='" + dtpThang.Text + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();

            sCommand = "Update SPDVCT set tytrong= " + spdv + ",thucdiem= round ((diem *" + spdv / 100 + "),0) where loaikh=2 and thang ='" + dtpThang.Text + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            DataAccess.conn.Close();

            Cursor.Current = Cursors.Default;
            MessageBox.Show("Hoàn thành.");            
        }

        private void dtpThang_ValueChanged(object sender, EventArgs e)
        {

        }        
    }
}