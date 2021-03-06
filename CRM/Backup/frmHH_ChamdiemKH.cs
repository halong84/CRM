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
    public partial class frmHH_ChamdiemKH : Form
    {
        MicrosoftExcelClient m_ExcelClient = null;
        SqlCommand myCommand;
        
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

        private void btnChamdiem_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            String  sCommand;          
            //Kiểm tra có dữ liệu để chấm điểm chưa
            dt.Clear();
            sCommand = "select * from sdbqnt where thang= '" + dtpThang.Text + "' and left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            myCommand = new SqlCommand(sCommand, frmMain.conn);
            myCommand.CommandTimeout = 0;
            adapter.SelectCommand = myCommand;
            adapter.Fill(dt);
            
            frmMain.conn.Close();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Chưa import dữ liệu tháng này!");
                return;
            }
            //Kiem tra thang da cham diem chua
            dt.Clear();
            sCommand = "select * from diemkh where THANG= '" + dtpThang.Text + "' and left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            myCommand = new SqlCommand(sCommand, frmMain.conn);
            myCommand.CommandTimeout = 0;
            adapter.SelectCommand = myCommand;
            adapter.Fill(dt);
            frmMain.conn.Close();
            if (dt.Rows.Count > 0)
            {
                if (MessageBox.Show("Tháng này đã chấm điểm! Chấm lại không? ", "cham diem CRM ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }                
            }
            
            Cursor.Current = Cursors.WaitCursor;
            //Tinh diem theo SDBQ 
            String makh = "", ngaytinhdiem = "";
            Byte loaikh = 1;
            int i = 0;
            decimal sdbq = 0;
            ngaytinhdiem = dtpThang.Text.Substring(0, 2) + "/01/" + dtpThang.Text.Substring(3, 4);
            sCommand = "SELECT SDBQCT.MAKH,SDBQCT.SDBQ as SOTIENHOA,KHACHHANG.LOAIKH from SDBQCT,KHACHHANG where SDBQCT.MAKH=KHACHHANG.MAKH and left(SDBQCT.MAKH,4)='" + frmMain.cn + "' and thang ='" + dtpThang.Text + "'";
            dt.Clear();
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            myCommand = new SqlCommand(sCommand, frmMain.conn);
            myCommand.CommandTimeout = 0;
            adapter.SelectCommand = myCommand;
            adapter.Fill(dt);
            frmMain.conn.Close();

            for (i = 0; i < dt.Rows.Count; i++)
            {
                makh = dt.Rows[i]["MAKH"].ToString();
                loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                sdbq = Convert.ToDecimal(dt.Rows[i]["SOTIENHOA"].ToString());
                DataTable dt1 = new DataTable();
                sCommand = "select top 1 mact from dmchitieu where giatri<=" + sdbq + " and manhom ='SDBQ' and loaikh=" + loaikh + " order by giatri desc";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                myCommand = new SqlCommand(sCommand, frmMain.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt1);
                frmMain.conn.Close();

                if (dt1.Rows.Count > 0)
                {
                    DataTable dt2 = new DataTable();
                    sCommand = "select DMDIEM.* from DMDIEM where MACT='" + dt1.Rows[0]["MACT"].ToString() + "' and loaiKH=" + loaikh + " and macn='" + frmMain.cn + "' and ngaybdhl <= '" + ngaytinhdiem + "' and ngayhethl ='12/31/9998'";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    myCommand = new SqlCommand(sCommand, frmMain.conn);
                    myCommand.CommandTimeout = 0;
                    adapter.SelectCommand = myCommand;
                    adapter.Fill(dt2);
                    
                    frmMain.conn.Close();

                    if (dt2.Rows.Count > 0)
                    {
                        decimal diemsdbq = 0;
                        diemsdbq = Convert.ToDecimal(dt2.Rows[0]["DIEM"].ToString());
                        try
                        {
                            sCommand = "INSERT INTO DIEMKH(MAKH,THANG,DIEM_SDBQ,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + diemsdbq + "," + loaikh + ")";
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.CommandTimeout = 0;
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                        catch
                        {
                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }

                            sCommand = "Update DIEMKH set DIEM_SDBQ =" + diemsdbq + "where makh='" + makh + "' and thang ='" + dtpThang.Text + "'";
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.CommandTimeout = 0;
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                        //Dua du lieu vao bang sdbqct
                        try
                        {

                            if (frmMain.conn.State == ConnectionState.Open)
                            {
                                frmMain.conn.Close();
                            }

                            sCommand = "Update SDBQCT set MACT='" + dt1.Rows[0]["MACT"].ToString() + "',DIEM=" + diemsdbq + " where makh='" + makh + "'and thang ='" + dtpThang.Text + "'";
                            frmMain.conn.Open();
                            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                            frmMain.myCommand.CommandTimeout = 0;
                            frmMain.myCommand.ExecuteNonQuery();
                            frmMain.conn.Close();
                        }
                        catch { }
                    }
                }
            }

            ////Tinh diem theo Thoi gian gui           

            ////Tinh diem cho cac so tiet kiem co ky han
            //dt.Clear();
            //sCommand = "SELECT * from TGGTCT where left(TGGTCT.MAKH,4)='" + frmMain.cn + "'and thang ='" + dtpThang.Text + "'";
            //if (frmMain.conn.State == ConnectionState.Open)
            //{
            //    frmMain.conn.Close();
            //}
            //frmMain.conn.Open();
            //new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            //frmMain.conn.Close();

            //for (i = 0; i < dt.Rows.Count; i++)
            //{
            //    makh = dt.Rows[i]["MAKH"].ToString();
            //    loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
            //    thang = Convert.ToInt16(dt.Rows[i]["THOIGIAN"].ToString());
            //    DataTable dt1 = new DataTable();
            //    sCommand = "select top 1 mact from dmchitieu where giatri<=" + thang + " and manhom ='TGGT' and loaikh=" + loaikh + " order by giatri desc";
            //    if (frmMain.conn.State == ConnectionState.Open)
            //    {
            //        frmMain.conn.Close();
            //    }
            //    frmMain.conn.Open();
            //    new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
            //    frmMain.conn.Close();

            //    if (dt1.Rows.Count > 0)
            //    {

            //        DataTable dt2 = new DataTable();
            //        sCommand = "select DMDIEM.* from DMDIEM where MACT='" + dt1.Rows[0]["MACT"].ToString() + "' and loaiKH=" + loaikh + " and macn='" + frmMain.cn + "' and ngaybdhl <= '" + ngaytinhdiem + "' and ngayhethl ='12/31/9998'";
            //        if (frmMain.conn.State == ConnectionState.Open)
            //        {
            //            frmMain.conn.Close();
            //        }
            //        frmMain.conn.Open();
            //        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt2);
            //        frmMain.conn.Close();

            //        if (dt2.Rows.Count > 0)
            //        {
            //            decimal diem = 0;
            //            diem = Convert.ToDecimal(dt2.Rows[0]["DIEM"].ToString());
                        
            //            //Dua du lieu vao bang TGGTCT
            //            try
            //            {

            //                if (frmMain.conn.State == ConnectionState.Open)
            //                {
            //                    frmMain.conn.Close();
            //                }

            //                sCommand = "Update TGGTCT set MACT='" + dt1.Rows[0]["MACT"].ToString() + "',DIEM=" + diem + " where sotk='" + dt.Rows[i]["SOTK"].ToString() + "'and thang ='" + dtpThang.Text + "'";
            //                frmMain.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                frmMain.conn.Close();
            //            }
            //            catch { }
            //        }
            //    }
            //}

            ////Tinh diem binh quan theo tung khach hang theo thoi gian gui tien
            //dt.Clear();
            //sCommand = "select makh,avg(diem)as diem from TGGTCT where diem>0 and thang ='" + dtpThang.Text + "' and left(makh,4)='" + frmMain.cn + "' group by makh";
            //if (frmMain.conn.State == ConnectionState.Open)
            //{
            //    frmMain.conn.Close();
            //}
            //frmMain.conn.Open();
            //new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            //frmMain.conn.Close();

            //for (i = 0; i < dt.Rows.Count; i++)
            //{
            //    try
            //    {
            //        sCommand = "INSERT INTO DIEMKH(MAKH,THANG,DIEM_TGG,LOAIKH) Values ('" + dt.Rows[i]["makh"].ToString() + "','" + dtpThang.Text + "'," + Convert.ToDecimal(dt.Rows[i]["diem"].ToString()) + "," + loaikh + ")";
            //        if (frmMain.conn.State == ConnectionState.Open)
            //        {
            //            frmMain.conn.Close();
            //        }
            //        frmMain.conn.Open();
            //        frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            //        frmMain.myCommand.ExecuteNonQuery();
            //        frmMain.conn.Close();
            //    }
            //    catch
            //    {
            //        if (frmMain.conn.State == ConnectionState.Open)
            //        {
            //            frmMain.conn.Close();
            //        }

            //        sCommand = "Update DIEMKH set DIEM_TGG =" + Convert.ToDecimal(dt.Rows[i]["diem"].ToString()) + "where makh='" + dt.Rows[i]["makh"].ToString() + "'and thang ='" + dtpThang.Text + "'";
            //        frmMain.conn.Open();
            //        frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            //        frmMain.myCommand.ExecuteNonQuery();
            //        frmMain.conn.Close();
            //    }
            //}

            //Tinh diem theo so luong dich vu su dung            
            //Tinh diem khach hang su dung dich vu dua vao table diemkh
            dt.Clear();
            sCommand = "Select spdvct.* from SPDVCT where left(spdvCT.makh,4)='" + frmMain.cn + "'and thang ='" + dtpThang.Text + "'";
            int sldichvu = 0;
            string strdvu = "";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            myCommand = new SqlCommand(sCommand, frmMain.conn);
            myCommand.CommandTimeout = 0;
            adapter.SelectCommand = myCommand;
            adapter.Fill(dt);
            
            frmMain.conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    makh = dt.Rows[i]["makh"].ToString();
                    loaikh = Convert.ToByte(dt.Rows[i]["loaikh"].ToString());
                    sldichvu = Convert.ToInt16(dt.Rows[i]["slspdv"].ToString());
                    strdvu = dt.Rows[i]["spdv"].ToString();
                    DataTable dt1 = new DataTable();
                    sCommand = "select top 1 mact from dmchitieu where giatri<=" + sldichvu + " and manhom ='SPDV' and loaikh=" + loaikh + " order by giatri desc";
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    myCommand = new SqlCommand(sCommand, frmMain.conn);
                    myCommand.CommandTimeout = 0;
                    adapter.SelectCommand = myCommand;
                    adapter.Fill(dt1);
                    frmMain.conn.Close();

                    if (dt1.Rows.Count > 0)
                    {
                        DataTable dt2 = new DataTable();
                        sCommand = "select DMDIEM.* from DMDIEM where MACT='" + dt1.Rows[0]["MACT"].ToString() + "' and loaiKH=" + loaikh + " and macn='" + frmMain.cn + "' and ngaybdhl <= '" + ngaytinhdiem + "' and ngayhethl ='12/31/9998'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        myCommand = new SqlCommand(sCommand, frmMain.conn);
                        myCommand.CommandTimeout = 0;
                        adapter.SelectCommand = myCommand;
                        adapter.Fill(dt2);
                        frmMain.conn.Close();

                        if (dt2.Rows.Count > 0)
                        {
                            decimal diemdv = 0;
                            diemdv = Convert.ToDecimal(dt2.Rows[0]["DIEM"].ToString());
                            try
                            {
                                sCommand = "INSERT INTO DIEMKH(MAKH,THANG,DIEM_SPDV,SPDV,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + diemdv + ",'" + strdvu + "'," + loaikh + ")";
                                if (frmMain.conn.State == ConnectionState.Open)
                                {
                                    frmMain.conn.Close();
                                }
                                frmMain.conn.Open();
                                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                                frmMain.myCommand.CommandTimeout = 0;
                                frmMain.myCommand.ExecuteNonQuery();
                                frmMain.conn.Close();
                            }
                            catch
                            {
                                if (frmMain.conn.State == ConnectionState.Open)
                                {
                                    frmMain.conn.Close();
                                }

                                sCommand = "Update DIEMKH set DIEM_SPDV =" + diemdv + ",SPDV='" + strdvu + "' where makh='" + makh + "'and thang ='" + dtpThang.Text + "'";
                                frmMain.conn.Open();
                                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                                frmMain.myCommand.CommandTimeout = 0;
                                frmMain.myCommand.ExecuteNonQuery();
                                frmMain.conn.Close();
                            }
                            //Dua du lieu vao table SPDVCT
                            try
                            {

                                if (frmMain.conn.State == ConnectionState.Open)
                                {
                                    frmMain.conn.Close();
                                }

                                sCommand = "Update SPDVCT set SLSPDV =" + sldichvu + ",SPDV='" + strdvu + "',MACT='" + dt1.Rows[0]["MACT"].ToString() + "',DIEM=" + diemdv + " where makh='" + makh + "'and thang ='" + dtpThang.Text + "'";
                                frmMain.conn.Open();
                                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                                frmMain.myCommand.CommandTimeout = 0;
                                frmMain.myCommand.ExecuteNonQuery();
                                frmMain.conn.Close();
                            }
                            catch { }
                        }
                    }
                }
            }

            ////Tinh diem theo ty suat loi nhuan            
            //dt.Clear();
            //sCommand = "SELECT * from PROFITCT where left(PROFITCT.MAKH,4)='" + frmMain.cn + "'and thang ='" + dtpThang.Text + "'";
            ////So lieu thang 07/2012 profitvnd=0
            ////sCommand = "SELECT SDBQ.MAKH,SUM(PROFITVND*PROFITRATIO)/SUM(PROFITVND) as PROFITRATIO,KHACHHANG.LOAIKH from SDBQ,KHACHHANG where SDBQ.MAKH=KHACHHANG.MAKH and left(SDBQ.MAKH,4)='" + frmMain.cn + "' group by sdbq.makh,khachhang.loaikh having sum(profitvnd)>0";
            //if (frmMain.conn.State == ConnectionState.Open)
            //{
            //    frmMain.conn.Close();
            //}
            //frmMain.conn.Open();
            //new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            //frmMain.conn.Close();

            //decimal profit = 0;
            //for (i = 0; i < dt.Rows.Count; i++)
            //{
            //    makh = dt.Rows[i]["MAKH"].ToString();
            //    loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
            //    profit = Convert.ToDecimal(dt.Rows[i]["PROFIT"].ToString());
            //    DataTable dt1 = new DataTable();
            //    sCommand = "select top 1 mact from dmchitieu where giatri<=" + profit + " and manhom ='TSLN' and loaikh=" + loaikh + " order by giatri desc";
            //    if (frmMain.conn.State == ConnectionState.Open)
            //    {
            //        frmMain.conn.Close();
            //    }
            //    frmMain.conn.Open();
            //    new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt1);
            //    frmMain.conn.Close();

            //    if (dt1.Rows.Count > 0)
            //    {
            //        DataTable dt2 = new DataTable();
            //        sCommand = "select DMDIEM.* from DMDIEM where MACT='" + dt1.Rows[0]["MACT"].ToString() + "' and loaiKH=" + loaikh + " and macn='" + frmMain.cn + "' and ngaybdhl <= '" + ngaytinhdiem + "' and ngayhethl ='12/31/9998'";
            //        if (frmMain.conn.State == ConnectionState.Open)
            //        {
            //            frmMain.conn.Close();
            //        }
            //        frmMain.conn.Open();
            //        new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt2);
            //        frmMain.conn.Close();

            //        if (dt2.Rows.Count > 0)
            //        {
            //            decimal diemTSLN = 0;
            //            diemTSLN = Convert.ToDecimal(dt2.Rows[0]["DIEM"].ToString());
            //            try
            //            {
            //                sCommand = "INSERT INTO DIEMKH(MAKH,THANG,DIEM_PROFIT,LOAIKH) Values ('" + makh + "','" + dtpThang.Text + "'," + diemTSLN + "," + loaikh + ")";
            //                if (frmMain.conn.State == ConnectionState.Open)
            //                {
            //                    frmMain.conn.Close();
            //                }
            //                frmMain.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                frmMain.conn.Close();
            //            }
            //            catch
            //            {
            //                if (frmMain.conn.State == ConnectionState.Open)
            //                {
            //                    frmMain.conn.Close();
            //                }

            //                sCommand = "Update DIEMKH set DIEM_PROFIT =" + diemTSLN + "where makh='" + makh + "'and thang ='" + dtpThang.Text + "'";
            //                frmMain.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                frmMain.conn.Close();
            //            }
            //            //Dua du lieu vao bang PROFITCT
            //            try
            //            {
            //                sCommand = "Update PROFITCT set DIEM =" + diemTSLN + ",MACT ='" + dt1.Rows[0]["MACT"].ToString() + "' where makh='" + makh + "'and thang ='" + dtpThang.Text + "'";
            //                frmMain.conn.Open();
            //                frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            //                frmMain.myCommand.ExecuteNonQuery();
            //                frmMain.conn.Close();
            //            }
            //            catch { }

            //        }
            //    }
            //}

            //Tinh tong diem cua khach hang theo ty trong
            //Tinh tong diem cho khach hang ca nhan
            dt.Clear();
            decimal tggt = 1, spdv = 1, tsln = 1;
            sCommand = "select * from dmtytrong where loaikh=1 and ngaybatdauhl<='" + ngaytinhdiem + "' and ngayhethl='12/31/9998' and macn='"+CRM.frmMain.cn +"'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            myCommand = new SqlCommand(sCommand, frmMain.conn);
            myCommand.CommandTimeout = 0;
            adapter.SelectCommand = myCommand;
            adapter.Fill(dt);
            
            frmMain.conn.Close();

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
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();

            //Tinh thuc diem cua cac bang chi tiet
            sCommand = "Update SDBQCT set tytrong= " + sdbq + ",thucdiem= round ((diem *" + sdbq / 100 + "),0) where loaikh=1 and thang ='" + dtpThang.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();

            sCommand = "Update TGGTCT set tytrong= " + tggt + ",thucdiem= round ((diem *" + tggt / 100 + "),0) where loaikh=1 and thang ='" + dtpThang.Text + "'";
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();

            sCommand = "Update SPDVCT set tytrong= " + spdv + ",thucdiem= round ((diem *" + spdv / 100 + "),0) where loaikh=1 and thang ='" + dtpThang.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();

            //Tinh tong diem khach hang doanh nghiep
            dt.Clear();
            sCommand = "select * from dmtytrong where loaikh=2 and ngaybatdauhl<='" + ngaytinhdiem + "' and ngayhethl='12/31/9998' and macn='"+CRM.frmMain.cn +"'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

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
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();

            //Tinh thuc diem cua cac bang chi tiet
            sCommand = "Update SDBQCT set tytrong= " + sdbq + ",thucdiem= round ((diem *" + sdbq / 100 + "),0) where loaikh=2 and thang ='" + dtpThang.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();

            sCommand = "Update PROFITCT set tytrong= " + tsln + ",thucdiem= round ((diem *" + tsln / 100 + "),0) where loaikh=2 and thang ='" + dtpThang.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();

            sCommand = "Update SPDVCT set tytrong= " + spdv + ",thucdiem= round ((diem *" + spdv / 100 + "),0) where loaikh=2 and thang ='" + dtpThang.Text + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
            frmMain.myCommand.CommandTimeout = 0;
            frmMain.myCommand.ExecuteNonQuery();
            frmMain.conn.Close();

            Cursor.Current = Cursors.Default;
            MessageBox.Show("Đã chấm điểm xong.");            
        }

        private void dtpThang_ValueChanged(object sender, EventArgs e)
        {

        }        
    }
}