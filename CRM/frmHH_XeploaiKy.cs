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
    public partial class frmHH_XeploaiKy : Form
    {
        KETQUAXEPLOAIBUS kqxl_bus = new KETQUAXEPLOAIBUS();
        String strCmd = "";
        SqlCommand myCommand;
        public static String makh = "", tuthang = "", denthang = "";
        public static decimal diemtggt = 0, tdiem = 0;
        public static byte loaikh;
        String ngaytinhdiem;

        public frmHH_XeploaiKy()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            DateTime dtCurrent = DateTime.Now;
            ////dtpFrom.CustomFormat = "MM/yyyy";            
            dtpFrom.Text = dtCurrent.ToShortDateString().Substring(3,7);
            ////dtpTo.CustomFormat = "MM/yyyy";
            dtpTo.Text = dtCurrent.ToShortDateString().Substring(3, 7); ;
            buttonX29.Enabled = false;
            btnXuatExcel.Enabled = false;
        }       

        private void buttonX4_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[22] 
                { 
                    new DataColumn("TUTHANG", typeof(string)),
                    new DataColumn("DENTHANG", typeof(string)),
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("DIEM_SDBQ", typeof(byte)),
                    new DataColumn("DIEM_TGGT", typeof(byte)),
                    new DataColumn("DIEM_SPDV", typeof(byte)),
                    new DataColumn("DIEM_PROFIT", typeof(byte)),
                    new DataColumn("TONGDIEMDL", typeof(byte)),
                    new DataColumn("DINHTINH", typeof(int)),
                    new DataColumn("DIENGIAI", typeof(string)),
                    new DataColumn("TONGDIEM", typeof(byte)),
                    new DataColumn("XEPLOAI", typeof(string)),
                    new DataColumn("LOAIKH", typeof(byte)),
                    new DataColumn("XACNHAN", typeof(bool)),
                    new DataColumn("NGAYXACNHAN", typeof(string)),
                    new DataColumn("NGUOIXACNHAN", typeof(string)),
                    new DataColumn("PHEDUYET", typeof(bool)),
                    new DataColumn("NGAYPHEDUYET", typeof(string)),
                    new DataColumn("NGUOIPHEDUYET", typeof(string)),
                    new DataColumn("PDTT", typeof(bool)),
                    new DataColumn("NGAYPDTT", typeof(string)),
                    new DataColumn("NGUOIPDTT", typeof(string))
                }
            );
            DataRow dr2;

            SqlDataAdapter adapter = new SqlDataAdapter();
            int sothangxl = 0;
            DateTime tuthang=Convert.ToDateTime("01/"+dtpFrom.Text);
            DateTime denthang = Convert.ToDateTime("01/" + dtpTo.Text);
            DateTime thang = tuthang;
            while (thang <= denthang)
            {
                sothangxl = sothangxl + 1;
                thang = thang.AddMonths(1);
            }
            String FNgay = "01/" + dtpFrom.Text;
            String TNgay = "01/" + dtpTo.Text;
           
            ngaytinhdiem = dtpFrom.Text.Substring(0, 2) + "/01/" + dtpFrom.Text.Substring(3, 4);
            if (optCN.Checked == true)
            {
                loaikh = 1;
                //Kiểm tra kỳ đã được xếp loại chưa
                dt.Clear();
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                myCommand = new SqlCommand(strCmd, DataAccess.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);
                
                DataAccess.conn.Close();
                if (dt.Rows.Count > 0)
                {
                    if (MessageBox.Show("Kỳ này đã được xếp loại, bạn có muốn xếp loại lại không?", "Xếp loại khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Kiem tra ketquaxeploai nay da duoc xac nhan chua, neu da xac nhan thi khong cho phep xep loai
                        strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and xacnhan=1";
                        dt.Clear();
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        myCommand.CommandTimeout = 0;
                        adapter.SelectCommand = myCommand;
                        adapter.Fill(dt);
                        DataAccess.conn.Close();
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Kết quả xếp loại này đã xác nhận, không được xếp loại lại!");
                            return;
                        }
                        //Xep loai khach hang
                        //decimal diem = 0;

                        if (kqxl_bus.UPDATE_KETQUAXEPLOAI_FROM_DIEMKH(dtpFrom.Text, dtpTo.Text, Thongtindangnhap.macn, loaikh, sothangxl, FNgay, TNgay))
                        {
                            //Xep loai khach hang
                            dt.Clear();
                            dt_temp2.Clear();
                            dt = kqxl_bus.CREATE_KETQUAXEPLOAI_XEPLOAI(Thongtindangnhap.macn, loaikh, dtpFrom.Text, dtpTo.Text, ngaytinhdiem);
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    dr2 = dt_temp2.NewRow();
                                    dr2["TUTHANG"] = dt.Rows[i]["TUTHANG"].ToString();
                                    dr2["DENTHANG"] = dt.Rows[i]["DENTHANG"].ToString();
                                    dr2["MAKH"] = dt.Rows[i]["MAKH"].ToString();
                                    dr2["DIEM_SDBQ"] = 0;
                                    dr2["DIEM_TGGT"] = 0;
                                    dr2["DIEM_SPDV"] = 0;
                                    dr2["DIEM_PROFIT"] = 0;
                                    dr2["TONGDIEMDL"] = 0;
                                    dr2["DINHTINH"] = 0;
                                    dr2["DIENGIAI"] = "";
                                    dr2["TONGDIEM"] = 0;
                                    dr2["XEPLOAI"] = dt.Rows[i]["XEPLOAI"].ToString();
                                    dr2["LOAIKH"] = loaikh;
                                    dr2["XACNHAN"] = false;
                                    dr2["NGAYXACNHAN"] = "01/01/1900";
                                    dr2["NGUOIXACNHAN"] = "";
                                    dr2["PHEDUYET"] = false;
                                    dr2["NGAYPHEDUYET"] = "01/01/1900";
                                    dr2["NGUOIPHEDUYET"] = "";
                                    dr2["PDTT"] = false;
                                    dr2["NGAYPDTT"] = "01/01/1900";
                                    dr2["NGUOIPDTT"] = "";
                                    dt_temp2.Rows.Add(dr2);
                                }
                            }

                            if (kqxl_bus.UPDATE_KETQUAXEPLOAI_XEPLOAI(dt_temp2))
                            {
                                MessageBox.Show("Xếp loại khách hàng cá nhân kỳ từ tháng " + dtpFrom.Text + " đến tháng " + dtpTo.Text + " thành công");
                            }
                            else
                            {
                                MessageBox.Show("Có lỗi xảy ra khi xếp loại khách hàng cá nhân kỳ từ tháng " + dtpFrom.Text + " đến tháng " + dtpTo.Text);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra khi cập nhật điểm khách hàng cá nhân vào bảng KETQUAXEPLOAI.");
                        }

                        ////Dua du lieu tu bang diemkh sang bang ketquaxeploai, tinh toan theo ky xep loai va xep loai
                        //try
                        //{
                        //    //Xoa du lieu xep loai ky
                        //    strCmd = "delete ketquaxeploai where tuthang='" + dtpFrom.Text + "' and denthang ='" + dtpTo.Text + "' and left(makh,4)='" + Thongtindangnhap.macn + "' and loaikh=1";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //    myCommand.CommandTimeout = 0;
                        //    myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //    //Insert ket qua xep loai moi
                        //    strCmd = "insert into ketquaxeploai(tuthang,denthang,makh,diem_sdbq,diem_tggt,diem_spdv,tongdiemdl,tongdiem,loaikh) select '" + dtpFrom.Text + "' as TUTHANG,'" + dtpTo.Text + "' as DENTHANG, diemkh.makh,round(sum(diem_sdbq)/" + sothangxl + ",0),round(sum(diem_tgg)/" + sothangxl + ",0),round(sum(diem_spdv)/" + sothangxl + ",0),round(sum(tongdiemdl)/" + sothangxl + ",0),round(sum(tongdiemdl)/" + sothangxl + ",0),loaikh from diemkh where convert(date,'01/'+thang)>='" + FNgay + "' and convert(date,'01/'+thang) <='" + TNgay + "' and left(makh,4)='" + Thongtindangnhap.macn + "' and loaikh=1 group by diemkh.makh,loaikh ";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //    myCommand.CommandTimeout = 0;
                        //    myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //catch { }
                        ////Xep loai khach hang
                        //strCmd = "select ketquaxeploai.*,khachhang.hoten from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                        //if (DataAccess.conn.State == ConnectionState.Open)
                        //{
                        //    DataAccess.conn.Close();
                        //}
                        //DataAccess.conn.Open();
                        //myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //myCommand.CommandTimeout = 0;
                        //adapter.SelectCommand = myCommand;
                        //adapter.Fill(dt);
                        //DataAccess.conn.Close();

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    makh = dt.Rows[i]["MAKH"].ToString();
                        //    loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                        //    diem = Convert.ToDecimal(dt.Rows[i]["tongdiem"].ToString());

                        //    DataTable dt1 = new DataTable();
                        //    strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + diem + " and dmdiemxl.loaikh=" + loaikh + " and ngaybd <= '" + ngaytinhdiem + "' and ngaykt ='12/31/9998' order by diem desc";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //    myCommand.CommandTimeout = 0;
                        //    adapter.SelectCommand = myCommand;
                        //    adapter.Fill(dt1);
                        //    DataAccess.conn.Close();
                        //    if (dt1.Rows.Count > 0)
                        //    {
                        //        strCmd = "Update ketquaxeploai set xeploai ='" + dt1.Rows[0]["MALOAI"].ToString() + "' where makh='" + makh + "' and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "'";
                        //        if (DataAccess.conn.State == ConnectionState.Open)
                        //        {
                        //            DataAccess.conn.Close();
                        //        }
                        //        DataAccess.conn.Open();
                        //        myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //        myCommand.CommandTimeout = 0;
                        //        myCommand.ExecuteNonQuery();
                        //        DataAccess.conn.Close();
                        //    }
                        //}
                    }
                }
                else
                //Chua xep loai
                {
                    //Kiem tra cac thang xep loai theo ky da cham diem theo thang chua                   
                    thang = tuthang;
                    while(thang<=denthang)
                    {
                        String strThang;
                        strThang = thang.ToShortDateString().Substring(3, 7);
                        strCmd = "select * from diemkh where thang='" +strThang + "' and left(diemkh.makh,4)='" + Thongtindangnhap.macn + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        dt.Clear();
                        myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        myCommand.CommandTimeout = 0;
                        adapter.SelectCommand = myCommand;
                        adapter.Fill(dt);
                        DataAccess.conn.Close();
                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("Thang " + strThang +  " chua duoc cham diem!");
                            return;
                        }
                        thang = thang.AddMonths(1);
                    }
                    
                    if (kqxl_bus.UPDATE_KETQUAXEPLOAI_FROM_DIEMKH(dtpFrom.Text, dtpTo.Text, Thongtindangnhap.macn, loaikh, sothangxl, FNgay, TNgay))
                    {
                        //Xep loai khach hang
                        dt.Clear();
                        dt_temp2.Clear();
                        dt = kqxl_bus.CREATE_KETQUAXEPLOAI_XEPLOAI(Thongtindangnhap.macn, loaikh, dtpFrom.Text, dtpTo.Text, ngaytinhdiem);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dr2 = dt_temp2.NewRow();
                                dr2["TUTHANG"] = dt.Rows[i]["TUTHANG"].ToString();
                                dr2["DENTHANG"] = dt.Rows[i]["DENTHANG"].ToString();
                                dr2["MAKH"] = dt.Rows[i]["MAKH"].ToString();
                                dr2["DIEM_SDBQ"] = 0;
                                dr2["DIEM_TGGT"] = 0;
                                dr2["DIEM_SPDV"] = 0;
                                dr2["DIEM_PROFIT"] = 0;
                                dr2["TONGDIEMDL"] = 0;
                                dr2["DINHTINH"] = 0;
                                dr2["DIENGIAI"] = "";
                                dr2["TONGDIEM"] = 0;
                                dr2["XEPLOAI"] = dt.Rows[i]["XEPLOAI"].ToString();
                                dr2["LOAIKH"] = loaikh;
                                dr2["XACNHAN"] = false;
                                dr2["NGAYXACNHAN"] = "01/01/1900";
                                dr2["NGUOIXACNHAN"] = "";
                                dr2["PHEDUYET"] = false;
                                dr2["NGAYPHEDUYET"] = "01/01/1900";
                                dr2["NGUOIPHEDUYET"] = "";
                                dr2["PDTT"] = false;
                                dr2["NGAYPDTT"] = "01/01/1900";
                                dr2["NGUOIPDTT"] = "";
                                dt_temp2.Rows.Add(dr2);
                            }
                        }

                        if (kqxl_bus.UPDATE_KETQUAXEPLOAI_XEPLOAI(dt_temp2))
                        {
                            MessageBox.Show("Xếp loại khách hàng cá nhân kỳ từ tháng " + dtpFrom.Text + " đến tháng " + dtpTo.Text + " thành công");
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra khi xếp loại khách hàng cá nhân kỳ từ tháng " + dtpFrom.Text + " đến tháng " + dtpTo.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi cập nhật điểm khách hàng cá nhân vào bảng KETQUAXEPLOAI.");
                    }

                    //decimal diem = 0;
                    //dt.Clear();
                    ////Dua du lieu tu bang diemkh sang bang ketquaxeploai, tinh toan theo ky xep loai va xep loai                    
                    //try
                    //{
                    //    strCmd = "insert into ketquaxeploai(tuthang,denthang,makh,diem_sdbq,diem_tggt,diem_spdv,tongdiemdl,tongdiem,loaikh) select '" + dtpFrom.Text + "' as TUTHANG,'" + dtpTo.Text + "' as DENTHANG, diemkh.makh,round(sum(diem_sdbq)/" + sothangxl + ",0),round(sum(diem_tgg)/" + sothangxl + ",0),round(sum(diem_spdv)/" + sothangxl + ",0),round(sum(tongdiemdl)/" + sothangxl + ",0),round(sum(tongdiemdl)/" + sothangxl + ",0),loaikh from diemkh where convert(date,'01/'+thang)>='" + FNgay + "' and convert(date,'01/'+thang) <='" + TNgay + "' and left(makh,4)='" + Thongtindangnhap.macn + "' and loaikh=1 group by diemkh.makh,loaikh ";
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //    DataAccess.conn.Open();
                    //    myCommand = new SqlCommand(strCmd, DataAccess.conn);
                    //    myCommand.CommandTimeout = 0;
                    //    myCommand.ExecuteNonQuery();
                    //    DataAccess.conn.Close();
                    //}
                    //catch { }
                    ////Xep loai khach hang
                    //strCmd = "select ketquaxeploai.*,khachhang.hoten from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                    //if (DataAccess.conn.State == ConnectionState.Open)
                    //{
                    //    DataAccess.conn.Close();
                    //}
                    //DataAccess.conn.Open();
                    //myCommand = new SqlCommand(strCmd, DataAccess.conn);
                    //myCommand.CommandTimeout = 0;
                    //adapter.SelectCommand = myCommand;
                    //adapter.Fill(dt);
                    //DataAccess.conn.Close();

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    makh = dt.Rows[i]["MAKH"].ToString();
                    //    loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                    //    diem = Convert.ToDecimal(dt.Rows[i]["tongdiem"].ToString());

                    //    DataTable dt1 = new DataTable();
                    //    strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + diem + " and dmdiemxl.loaikh=" + loaikh + " and ngaybd <= '" + ngaytinhdiem + "' and ngaykt ='12/31/9998' order by diem desc";
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //    DataAccess.conn.Open();
                    //    myCommand = new SqlCommand(strCmd, DataAccess.conn);
                    //    myCommand.CommandTimeout = 0;
                    //    adapter.SelectCommand = myCommand;
                    //    adapter.Fill(dt1);
                    //    DataAccess.conn.Close();
                    //    if (dt1.Rows.Count > 0)
                    //    {
                    //        strCmd = "Update ketquaxeploai set xeploai ='" + dt1.Rows[0]["MALOAI"].ToString() + "' where makh='" + makh + "' and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "'";
                    //        if (DataAccess.conn.State == ConnectionState.Open)
                    //        {
                    //            DataAccess.conn.Close();
                    //        }
                    //        DataAccess.conn.Open();
                    //        myCommand = new SqlCommand(strCmd, DataAccess.conn);
                    //        myCommand.CommandTimeout = 0;
                    //        myCommand.ExecuteNonQuery();
                    //        DataAccess.conn.Close();
                    //    }
                    //}
                }
                dt.Clear();
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh="+loaikh+" and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                myCommand = new SqlCommand(strCmd, DataAccess.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);
                DataAccess.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGGT"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;
                dataGridViewX2.Columns[0].ReadOnly = true;
                dataGridViewX2.Columns[1].ReadOnly = true;
                dataGridViewX2.Columns[2].ReadOnly = true;
                dataGridViewX2.Columns[3].ReadOnly = true;
                dataGridViewX2.Columns[4].ReadOnly = true;
                dataGridViewX2.Columns[5].ReadOnly = true;
                dataGridViewX2.Columns[6].ReadOnly = true;
                dataGridViewX2.Columns[7].ReadOnly = false;
                dataGridViewX2.Columns[8].ReadOnly = false;
                dataGridViewX2.Columns[9].ReadOnly = true;
                dataGridViewX2.Columns[10].ReadOnly = true;
                dataGridViewX2.Columns[11].ReadOnly = true;

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {
                loaikh = 2;
                //Kiem tra ky nay da duoc xep loai chua
                dt.Clear();
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                dt.Clear();
                myCommand = new SqlCommand(strCmd, DataAccess.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);
                DataAccess.conn.Close();
                if (dt.Rows.Count > 0)
                {
                    if (MessageBox.Show("Kỳ này đã được xếp loại, bạn có muốn xếp loại lại không?", "Xếp loại khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Kiem tra ketquaxeploai nay da duoc xac nhan chua, neu da xac nhan thi khong cho phep xep loai
                        strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and xacnhan=1";
                        dt.Clear();
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        myCommand.CommandTimeout = 0;
                        adapter.SelectCommand = myCommand;
                        adapter.Fill(dt);
                        DataAccess.conn.Close();
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Kết quả xếp loại này đã xác nhận, không được xếp loại lại!");
                            return;
                        }
                       
                        if (kqxl_bus.UPDATE_KETQUAXEPLOAI_FROM_DIEMKH(dtpFrom.Text, dtpTo.Text, Thongtindangnhap.macn, loaikh, sothangxl, FNgay, TNgay))
                        {
                            //Xep loai khach hang
                            dt.Clear();
                            dt_temp2.Clear();
                            dt = kqxl_bus.CREATE_KETQUAXEPLOAI_XEPLOAI(Thongtindangnhap.macn, loaikh, dtpFrom.Text, dtpTo.Text, ngaytinhdiem);
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    dr2 = dt_temp2.NewRow();
                                    dr2["TUTHANG"] = dt.Rows[i]["TUTHANG"].ToString();
                                    dr2["DENTHANG"] = dt.Rows[i]["DENTHANG"].ToString();
                                    dr2["MAKH"] = dt.Rows[i]["MAKH"].ToString();
                                    dr2["DIEM_SDBQ"] = 0;
                                    dr2["DIEM_TGGT"] = 0;
                                    dr2["DIEM_SPDV"] = 0;
                                    dr2["DIEM_PROFIT"] = 0;
                                    dr2["TONGDIEMDL"] = 0;
                                    dr2["DINHTINH"] = 0;
                                    dr2["DIENGIAI"] = "";
                                    dr2["TONGDIEM"] = 0;
                                    dr2["XEPLOAI"] = dt.Rows[i]["XEPLOAI"].ToString();
                                    dr2["LOAIKH"] = loaikh;
                                    dr2["XACNHAN"] = false;
                                    dr2["NGAYXACNHAN"] = "01/01/1900";
                                    dr2["NGUOIXACNHAN"] = "";
                                    dr2["PHEDUYET"] = false;
                                    dr2["NGAYPHEDUYET"] = "01/01/1900";
                                    dr2["NGUOIPHEDUYET"] = "";
                                    dr2["PDTT"] = false;
                                    dr2["NGAYPDTT"] = "01/01/1900";
                                    dr2["NGUOIPDTT"] = "";
                                    dt_temp2.Rows.Add(dr2);
                                }
                            }

                            if (kqxl_bus.UPDATE_KETQUAXEPLOAI_XEPLOAI(dt_temp2))
                            {
                                MessageBox.Show("Xếp loại khách hàng doanh nghiệp kỳ từ tháng " + dtpFrom.Text + " đến tháng " + dtpTo.Text + " thành công");
                            }
                            else
                            {
                                MessageBox.Show("Có lỗi xảy ra khi xếp loại khách hàng doanh nghiệp kỳ từ tháng " + dtpFrom.Text + " đến tháng " + dtpTo.Text);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra khi cập nhật điểm khách hàng doanh nghiệp vào bảng KETQUAXEPLOAI.");
                        }

                        //decimal diem = 0;
                        ////Dua du lieu tu bang diemkh sang bang ketquaxeploai, tinh toan theo ky xep loai va xep loai
                        //try
                        //{
                        //    //Xoa du lieu xep loai ky

                        //    strCmd = "delete ketquaxeploai where tuthang='" + dtpFrom.Text + "' and denthang ='" + dtpTo.Text + "' and left(makh,4)='" + Thongtindangnhap.macn + "' and loaikh=2";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //    myCommand.CommandTimeout = 0;
                        //    myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();

                        //    strCmd = "insert into ketquaxeploai(tuthang,denthang,makh,diem_sdbq,diem_profit,diem_spdv,tongdiemdl,tongdiem,loaikh) select '" + dtpFrom.Text + "' as TUTHANG,'" + dtpTo.Text + "' as DENTHANG, diemkh.makh,round(sum(diem_sdbq)/" + sothangxl + ",0),round(sum(diem_tgg)/" + sothangxl + ",0),round(sum(diem_spdv)/" + sothangxl + ",0),round(sum(tongdiemdl)/" + sothangxl + ",0),round(sum(tongdiemdl)/" + sothangxl + ",0),loaikh from diemkh where convert(date,'01/'+thang)>='" + FNgay + "' and convert(date,'01/'+thang) <='" + TNgay + "' and left(makh,4)='" + Thongtindangnhap.macn + "' and loaikh=2 group by diemkh.makh,loaikh";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //    myCommand.CommandTimeout = 0;
                        //    myCommand.ExecuteNonQuery();
                        //    DataAccess.conn.Close();
                        //}
                        //catch { }
                        ////Xep loai khach hang
                        //strCmd = "select ketquaxeploai.*,khachhang.hoten from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                        //if (DataAccess.conn.State == ConnectionState.Open)
                        //{
                        //    DataAccess.conn.Close();
                        //}
                        //DataAccess.conn.Open();
                        //myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //myCommand.CommandTimeout = 0;
                        //adapter.SelectCommand = myCommand;
                        //adapter.Fill(dt);
                        //DataAccess.conn.Close();

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    makh = dt.Rows[i]["MAKH"].ToString();
                        //    loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                        //    diem = Convert.ToDecimal(dt.Rows[i]["TONGDIEM"].ToString());

                        //    DataTable dt1 = new DataTable();
                        //    strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + diem + " and dmdiemxl.loaikh=" + loaikh + " and ngaybd <= '" + ngaytinhdiem + "' and ngaykt ='12/31/9998' order by diem desc";
                        //    if (DataAccess.conn.State == ConnectionState.Open)
                        //    {
                        //        DataAccess.conn.Close();
                        //    }
                        //    DataAccess.conn.Open();
                        //    myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //    myCommand.CommandTimeout = 0;
                        //    adapter.SelectCommand = myCommand;
                        //    adapter.Fill(dt1);
                        //    DataAccess.conn.Close();
                        //    if (dt1.Rows.Count > 0)
                        //    {
                        //        strCmd = "Update ketquaxeploai set xeploai ='" + dt1.Rows[0]["MALOAI"].ToString() + "' where makh='" + makh + "'and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "'";
                        //        if (DataAccess.conn.State == ConnectionState.Open)
                        //        {
                        //            DataAccess.conn.Close();
                        //        }
                        //        DataAccess.conn.Open();
                        //        myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        //        myCommand.CommandTimeout = 0;
                        //        myCommand.ExecuteNonQuery();
                        //        DataAccess.conn.Close();
                        //    }
                        //}
                    }
                }
                else
                //Chua xep loai
                {
                    //Kiem tra cac thang xep loai theo ky da xep loai theo thang chua
                    
                    while (thang <= denthang)
                    {
                        String strThang;
                        strThang = thang.ToShortDateString().Substring(3, 7);
                        strCmd = "select * from diemkh where thang='" + strThang + "' and left(diemkh.makh,4)='" + Thongtindangnhap.macn + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        dt.Clear();
                        myCommand = new SqlCommand(strCmd, DataAccess.conn);
                        myCommand.CommandTimeout = 0;
                        adapter.SelectCommand = myCommand;
                        adapter.Fill(dt);
                        DataAccess.conn.Close();
                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("Thang " + strThang + " chua duoc xep loai!");
                            return;
                        }
                        thang = thang.AddMonths(1);
                    }

                    if (kqxl_bus.UPDATE_KETQUAXEPLOAI_FROM_DIEMKH(dtpFrom.Text, dtpTo.Text, Thongtindangnhap.macn, loaikh, sothangxl, FNgay, TNgay))
                    {
                        //Xep loai khach hang
                        dt.Clear();
                        dt_temp2.Clear();
                        dt = kqxl_bus.CREATE_KETQUAXEPLOAI_XEPLOAI(Thongtindangnhap.macn, loaikh, dtpFrom.Text, dtpTo.Text, ngaytinhdiem);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dr2 = dt_temp2.NewRow();
                                dr2["TUTHANG"] = dt.Rows[i]["TUTHANG"].ToString();
                                dr2["DENTHANG"] = dt.Rows[i]["DENTHANG"].ToString();
                                dr2["MAKH"] = dt.Rows[i]["MAKH"].ToString();
                                dr2["DIEM_SDBQ"] = 0;
                                dr2["DIEM_TGGT"] = 0;
                                dr2["DIEM_SPDV"] = 0;
                                dr2["DIEM_PROFIT"] = 0;
                                dr2["TONGDIEMDL"] = 0;
                                dr2["DINHTINH"] = 0;
                                dr2["DIENGIAI"] = "";
                                dr2["TONGDIEM"] = 0;
                                dr2["XEPLOAI"] = dt.Rows[i]["XEPLOAI"].ToString();
                                dr2["LOAIKH"] = loaikh;
                                dr2["XACNHAN"] = false;
                                dr2["NGAYXACNHAN"] = "01/01/1900";
                                dr2["NGUOIXACNHAN"] = "";
                                dr2["PHEDUYET"] = false;
                                dr2["NGAYPHEDUYET"] = "01/01/1900";
                                dr2["NGUOIPHEDUYET"] = "";
                                dr2["PDTT"] = false;
                                dr2["NGAYPDTT"] = "01/01/1900";
                                dr2["NGUOIPDTT"] = "";
                                dt_temp2.Rows.Add(dr2);
                            }
                        }

                        if (kqxl_bus.UPDATE_KETQUAXEPLOAI_XEPLOAI(dt_temp2))
                        {
                            MessageBox.Show("Xếp loại khách hàng doanh nghiệp kỳ từ tháng " + dtpFrom.Text + " đến tháng " + dtpTo.Text + " thành công");
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra khi xếp loại khách hàng doanh nghiệp kỳ từ tháng " + dtpFrom.Text + " đến tháng " + dtpTo.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi cập nhật điểm khách hàng doanh nghiệp vào bảng KETQUAXEPLOAI.");
                    }

                    //decimal diem = 0;
                    ////Dua du lieu tu bang diemkh sang bang ketquaxeploai, tinh toan theo ky xep loai va xep loai
                    //try
                    //{
                    //    strCmd = "insert into ketquaxeploai(tuthang,denthang,makh,diem_sdbq,diem_profit,diem_spdv,tongdiemdl,tongdiem,loaikh) select '" + dtpFrom.Text + "' as TUTHANG,'" + dtpTo.Text + "' as DENTHANG, diemkh.makh,round(sum(diem_sdbq)/" + sothangxl + ",0),round(sum(diem_tgg)/" + sothangxl + ",0),round(sum(diem_spdv)/" + sothangxl + ",0),round(sum(tongdiemdl)/" + sothangxl + ",0),round(sum(tongdiemdl)/" + sothangxl + ",0),loaikh from diemkh where convert(date,'01/'+thang)>='" + FNgay + "' and convert(date,'01/'+thang) <='" + TNgay + "' and left(makh,4)='" + Thongtindangnhap.macn + "' and loaikh=2 group by diemkh.makh,loaikh ";
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //    DataAccess.conn.Open();
                    //    myCommand = new SqlCommand(strCmd, DataAccess.conn);
                    //    myCommand.CommandTimeout = 0;
                    //    myCommand.ExecuteNonQuery();
                    //    DataAccess.conn.Close();
                    //}
                    //catch { }
                    ////Xep loai khach hang
                    //strCmd = "select ketquaxeploai.*,khachhang.hoten from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                    //if (DataAccess.conn.State == ConnectionState.Open)
                    //{
                    //    DataAccess.conn.Close();
                    //}
                    //DataAccess.conn.Open();
                    //dt.Clear();
                    //myCommand = new SqlCommand(strCmd, DataAccess.conn);
                    //myCommand.CommandTimeout = 0;
                    //adapter.SelectCommand = myCommand;
                    //adapter.Fill(dt);
                    //DataAccess.conn.Close();

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    makh = dt.Rows[i]["MAKH"].ToString();
                    //    loaikh = Convert.ToByte(dt.Rows[i]["LOAIKH"].ToString());
                    //    diem = Convert.ToDecimal(dt.Rows[i]["TONGDIEM"].ToString());

                    //    DataTable dt1 = new DataTable();
                    //    strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + diem + " and dmdiemxl.loaikh=" + loaikh + " and ngaybd <= '" + ngaytinhdiem + "' and ngaykt ='12/31/9998' order by diem desc";
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //    DataAccess.conn.Open();
                    //    myCommand = new SqlCommand(strCmd, DataAccess.conn);
                    //    myCommand.CommandTimeout = 0;
                    //    adapter.SelectCommand = myCommand;
                    //    adapter.Fill(dt1);
                    //    DataAccess.conn.Close();
                    //    if (dt1.Rows.Count > 0)
                    //    {
                    //        strCmd = "Update ketquaxeploai set xeploai ='" + dt1.Rows[0]["MALOAI"].ToString() + "' where makh='" + makh + "'and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "'";
                    //        if (DataAccess.conn.State == ConnectionState.Open)
                    //        {
                    //            DataAccess.conn.Close();
                    //        }
                    //        DataAccess.conn.Open();
                    //        myCommand = new SqlCommand(strCmd, DataAccess.conn);
                    //        myCommand.CommandTimeout = 0;
                    //        myCommand.ExecuteNonQuery();
                    //        DataAccess.conn.Close();
                    //    }
                    //}
                }
                dt.Clear();
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=" + loaikh + " and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                myCommand = new SqlCommand(strCmd, DataAccess.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);
                DataAccess.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;
                dataGridViewX3.Columns[0].ReadOnly = true;
                dataGridViewX3.Columns[1].ReadOnly = true;
                dataGridViewX3.Columns[2].ReadOnly = true;
                dataGridViewX3.Columns[3].ReadOnly = true;
                dataGridViewX3.Columns[4].ReadOnly = true;
                dataGridViewX3.Columns[5].ReadOnly = true;
                dataGridViewX3.Columns[6].ReadOnly = true;
                dataGridViewX3.Columns[7].ReadOnly = false;
                dataGridViewX3.Columns[8].ReadOnly = false;
                dataGridViewX3.Columns[9].ReadOnly = true;
                dataGridViewX3.Columns[10].ReadOnly = true;
                dataGridViewX3.Columns[11].ReadOnly = true;

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }

        private void dataGridViewX2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Kiem tra du lieu nhap vao phai la so
            ngaytinhdiem = dtpFrom.Text.Substring(0, 2) + "/01/" + dtpFrom.Text.Substring(3, 4);
            DataGridViewCell cuCell = dataGridViewX2.CurrentCell;
            string mainStr = dataGridViewX2.CurrentCell.Value.ToString();
            if (cuCell.ColumnIndex == 7)
            {
                for (int scan = 1; scan < mainStr.Length; scan++)
                {
                    if (Char.IsDigit(mainStr[scan])) { }
                    else
                    {
                        dataGridViewX2.CurrentCell.Value = 0;
                        dataGridViewX2.ClearSelection();
                        dataGridViewX2.CurrentCell = cuCell;
                        dataGridViewX2.CurrentCell.Selected = true;
                        break;
                    }
                }
            }

            // Tinh tong diem tren moi dong du lieu            
            if (Convert.ToInt32(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[7].Value.ToString()) != 0)
            {
                dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[9].Value = Convert.ToDecimal(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[6].Value.ToString()) + Convert.ToInt32(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[7].Value.ToString());
                if (Convert.ToDecimal(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[9].Value) > 100)
                {
                    MessageBox.Show("Tong diem khong duoc > 100!");
                    dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[7].Value = 0;
                    dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[9].Value = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[6].Value;
                    return;
                }
                decimal tongdiem = 0;
                tongdiem = Convert.ToDecimal(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[9].Value);
                DataTable dt = new DataTable();
                if (optCN.Checked == true)
                    loaikh = 1;
                else
                    loaikh = 2;
                DataTable dt1 = new DataTable();
                String strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + tongdiem + " and dmdiemxl.loaikh=" + loaikh + " and ngaybd <= '" + ngaytinhdiem + "'and macn='"+Thongtindangnhap.macn+"' and ngaykt ='12/31/9998' order by diem desc";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt1);
                DataAccess.conn.Close();
                if (dt1.Rows.Count > 0)
                {

                    dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[10].Value = dt1.Rows[0]["tenloai"].ToString();
                    dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[11].Value = dt1.Rows[0]["maloai"].ToString();
                }
                else
                {
                    dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[10].Value = "";
                    dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[11].Value = "";
                }
            }
        }

        private void optCN_Click(object sender, EventArgs e)
        {
            if (optCN.Checked == true)
            {
                loaikh = 1;
                dataGridViewX2.Visible = true;
                dataGridViewX3.Visible = false;
            }
            
        }

        private void optDN_Click(object sender, EventArgs e)
        {
            if (optDN.Checked == true)
            {
                loaikh = 2;
                dataGridViewX2.Visible = false;
                dataGridViewX3.Visible = true;
            }     
        }

        private void buttonX29_Click(object sender, EventArgs e)
        {
            //Kiem tra ky du lieu da phe duyet chua, neu da phe duyet thi khong cho phep cham dinh tinh            
            DataTable dt = new DataTable();
            String strCmd = "";
            
            if (optCN.Checked == true)
            {
                strCmd="select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and xacnhan=1";
                dt.Clear();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Kết quả xếp loại này đã xác nhận, không chấm điểm định tính lại được!");
                }
                else
                {
                    for (int i = 0; i < dataGridViewX2.RowCount; i++)
                    {
                        try
                        {
                            strCmd = "Insert into KETQUAXEPLOAI(TUTHANG,DENTHANG,MAKH,DIEM_SDBQ,DIEM_TGGT,DIEM_SPDV,TONGDIEMDL,DINHTINH,DIENGIAI,TONGDIEM,XEPLOAI,LOAIKH)";
                            strCmd += " Values('" + dtpFrom.Text + "','" + dtpTo.Text + "','" + dataGridViewX2.Rows[i].Cells[1].Value.ToString() + "','" + dataGridViewX2.Rows[i].Cells[3].Value.ToString();
                            strCmd += "','" + dataGridViewX2.Rows[i].Cells[4].Value.ToString() + "','" + dataGridViewX2.Rows[i].Cells[5].Value.ToString();
                            strCmd += "','" + dataGridViewX2.Rows[i].Cells[6].Value.ToString() + "','" + dataGridViewX2.Rows[i].Cells[7].Value.ToString();
                            strCmd += "','" + dataGridViewX2.Rows[i].Cells[8].Value.ToString() + "','" + dataGridViewX2.Rows[i].Cells[9].Value.ToString();
                            strCmd += "','" + dataGridViewX2.Rows[i].Cells[11].Value.ToString() + "','" + "1" + "')";

                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            myCommand = new SqlCommand(strCmd, DataAccess.conn);
                            myCommand.CommandTimeout = 0;
                            myCommand.ExecuteNonQuery();
                            DataAccess.conn.Close();
                        }
                        catch
                        {
                            strCmd = "Update Ketquaxeploai set dinhtinh=" + Convert.ToDecimal(dataGridViewX2.Rows[i].Cells[7].Value.ToString()) + ",diengiai=N'" + dataGridViewX2.Rows[i].Cells[8].Value.ToString() + "',tongdiem= " + Convert.ToDecimal(dataGridViewX2.Rows[i].Cells[9].Value.ToString()) + ",xeploai='" + dataGridViewX2.Rows[i].Cells[11].Value.ToString() + "' where makh ='" + dataGridViewX2.Rows[i].Cells[1].Value.ToString() + "' and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "'";

                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            myCommand = new SqlCommand(strCmd, DataAccess.conn);
                            myCommand.CommandTimeout = 0;
                            myCommand.ExecuteNonQuery();
                            DataAccess.conn.Close();
                        }
                    }
                    MessageBox.Show("Đã lưu!");
                }
            }
            if (optDN.Checked == true)
            {
                strCmd="select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and xacnhan=1";
                dt.Clear();
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Kết quả xếp loại này đã xác nhận, không chấm điểm định tính lại được!");
                }
                else
                {
                    for (int i = 0; i < dataGridViewX3.RowCount; i++)
                    {
                        try
                        {
                            strCmd = "Insert into KETQUAXEPLOAI(TUTHANG,DENTHANG,MAKH,DIEM_SDBQ,DIEM_SPDV,DIEM_PROFIT,TONGDIEMDL,DINHTINH,DIENGIAI,TONGDIEM,XEPLOAI,LOAIKH)";
                            strCmd += " Values('" + dtpFrom.Text + "','" + dtpTo.Text + "','" + dataGridViewX3.Rows[i].Cells[1].Value.ToString() + "','" + dataGridViewX3.Rows[i].Cells[3].Value.ToString();
                            strCmd += "','" + dataGridViewX3.Rows[i].Cells[5].Value.ToString() + "','" + dataGridViewX3.Rows[i].Cells[4].Value.ToString();
                            strCmd += "','" + dataGridViewX3.Rows[i].Cells[6].Value.ToString() + "','" + dataGridViewX3.Rows[i].Cells[7].Value.ToString();
                            strCmd += "','" + dataGridViewX3.Rows[i].Cells[8].Value.ToString() + "','" + dataGridViewX3.Rows[i].Cells[9].Value.ToString();
                            strCmd += "','" + dataGridViewX3.Rows[i].Cells[11].Value.ToString() + "','" + "2" + "')";

                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            myCommand = new SqlCommand(strCmd, DataAccess.conn);
                            myCommand.CommandTimeout = 0;
                            myCommand.ExecuteNonQuery();
                            DataAccess.conn.Close();
                        }
                        catch
                        {
                            strCmd = "Update Ketquaxeploai set dinhtinh=" + Convert.ToDecimal(dataGridViewX3.Rows[i].Cells[7].Value.ToString()) + ",diengiai=N'" + dataGridViewX3.Rows[i].Cells[8].Value.ToString() + "',tongdiem= " + Convert.ToDecimal(dataGridViewX3.Rows[i].Cells[9].Value.ToString()) + ",xeploai='" + dataGridViewX3.Rows[i].Cells[11].Value.ToString() + "' where makh ='" + dataGridViewX3.Rows[i].Cells[1].Value.ToString() + "' and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "'";

                            if (DataAccess.conn.State == ConnectionState.Open)
                            {
                                DataAccess.conn.Close();
                            }
                            DataAccess.conn.Open();
                            myCommand = new SqlCommand(strCmd, DataAccess.conn);
                            myCommand.CommandTimeout = 0;
                            myCommand.ExecuteNonQuery();
                            DataAccess.conn.Close();
                        }
                    }
                    MessageBox.Show("Đã lưu!");
                }
            }
            

        }

        private void dataGridViewX3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Kiem tra du lieu nhap vao phai la so
            ngaytinhdiem = dtpFrom.Text.Substring(0, 2) + "/01/" + dtpFrom.Text.Substring(3, 4);
            DataGridViewCell cuCell = dataGridViewX3.CurrentCell;
            string mainStr = dataGridViewX3.CurrentCell.Value.ToString();
            if (cuCell.ColumnIndex == 7)
            {
                for (int scan = 1; scan < mainStr.Length; scan++)
                {
                    if (Char.IsDigit(mainStr[scan])) { }
                    else
                    {
                        dataGridViewX3.CurrentCell.Value = 0;
                        dataGridViewX3.ClearSelection();
                        dataGridViewX3.CurrentCell = cuCell;
                        dataGridViewX3.CurrentCell.Selected = true;
                        break;
                    }
                }
            }

            // Tinh tong diem tren moi dong du lieu            
            if (Convert.ToInt32(dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[7].Value.ToString()) != 0)
            {
                dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[9].Value = Convert.ToDecimal(dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[6].Value.ToString()) + Convert.ToInt32(dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[7].Value.ToString());
                if (Convert.ToDecimal(dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[9].Value) > 100)
                {
                    MessageBox.Show("Tong diem khong duoc > 100!");
                    dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[7].Value = 0;
                    dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[9].Value = dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[6].Value;
                    return;
                }
                decimal tongdiem = 0;
                tongdiem = Convert.ToDecimal(dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[9].Value);
                DataTable dt = new DataTable();
                if (optCN.Checked == true)
                    loaikh = 1;
                else
                    loaikh = 2;
                DataTable dt1 = new DataTable();
                String strCmd = "select top 1 dmdiemxl.maloai,tenloai from dmdiemxl,dmxeploaikh where dmdiemxl.maloai=dmxeploaikh.maloai and diem<=" + tongdiem + " and dmdiemxl.loaikh=" + loaikh + " and ngaybd <= '" + ngaytinhdiem + "' and ngaykt ='12/31/9998' and macn='"+Thongtindangnhap.macn+"' order by diem desc";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt1);
                DataAccess.conn.Close();

                if (dt1.Rows.Count > 0)
                {
                    dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[10].Value = dt1.Rows[0]["tenloai"].ToString();
                    dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[11].Value = dt1.Rows[0]["maloai"].ToString();
                }
                else
                {
                    dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[10].Value = "";
                    dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[11].Value = "";
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (optCN.Checked == true)
            {
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                strCmd += " where ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and ketquaxeploai.makh like '%" + textBox1.Text + "%'";
                
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and ketquaxeploai.makh like '%" + textBox1.Text + "%' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' ";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                int iRows = dt.Rows.Count;
                if (iRows == 0)
                {
                    strCmd = "select ketquaxeploai.*,khachhang.hoten,'' as tenloai from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and ketquaxeploai.makh like '%" + textBox1.Text + "%' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                    DataAccess.conn.Close();
                    iRows = dt.Rows.Count;
                    if (iRows == 0)
                    {
                        strCmd = "select makh,hoten,0 as diem_sdbq,0 as diem_spdv,0 as diem_TGGT,0 as tongdiemdl,0 as dinhtinh,'' as diengiai,0 as tongdiem,'' as tenloai,''as xeploai from khachhang where khachhang.loaikh=1 and khachhang.makh like '%" + textBox1.Text + "%' and left(khachhang.makh,4)='" + Thongtindangnhap.macn + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        iRows = dt.Rows.Count;
                    }
                }
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);



                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGGT"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;
                dataGridViewX2.Columns[0].ReadOnly = true;
                dataGridViewX2.Columns[1].ReadOnly = true;
                dataGridViewX2.Columns[2].ReadOnly = true;
                dataGridViewX2.Columns[3].ReadOnly = true;
                dataGridViewX2.Columns[4].ReadOnly = true;
                dataGridViewX2.Columns[5].ReadOnly = true;
                dataGridViewX2.Columns[6].ReadOnly = true;
                dataGridViewX2.Columns[7].ReadOnly = false;
                dataGridViewX2.Columns[8].ReadOnly = false;
                dataGridViewX2.Columns[9].ReadOnly = true;
                dataGridViewX2.Columns[10].ReadOnly = true;
                dataGridViewX2.Columns[11].ReadOnly = true;

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                strCmd += " where ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and ketquaxeploai.makh like '%" + textBox1.Text + "%'";
                
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and ketquaxeploai.makh like '%" + textBox1.Text + "%' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                int iRows = dt.Rows.Count;
                if (iRows == 0)
                {
                    strCmd = "select ketquaxeploai.*,khachhang.hoten,'' as tenloai from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and ketquaxeploai.makh like '%" + textBox1.Text + "%' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                    DataAccess.conn.Close();
                    iRows = dt.Rows.Count;
                    if (iRows == 0)
                    {
                        strCmd = "select makh,hoten,0 as diem_sdbq,0 as diem_spdv,0 as diem_TGGT,0 as tongdiemdl,0 as dinhtinh,'' as diengiai,0 as tongdiem,'' as tenloai,''as xeploai from khachhang where khachhang.loaikh=2 and khachhang.makh like '%" + textBox1.Text + "%' and left(khachhang.makh,4)='" + Thongtindangnhap.macn + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        iRows = dt.Rows.Count;
                    }
                }
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;
                dataGridViewX3.Columns[0].ReadOnly = true;
                dataGridViewX3.Columns[1].ReadOnly = true;
                dataGridViewX3.Columns[2].ReadOnly = true;
                dataGridViewX3.Columns[3].ReadOnly = true;
                dataGridViewX3.Columns[4].ReadOnly = true;
                dataGridViewX3.Columns[5].ReadOnly = true;
                dataGridViewX3.Columns[6].ReadOnly = true;
                dataGridViewX3.Columns[7].ReadOnly = false;
                dataGridViewX3.Columns[8].ReadOnly = false;
                dataGridViewX3.Columns[9].ReadOnly = true;
                dataGridViewX3.Columns[10].ReadOnly = true;
                dataGridViewX3.Columns[11].ReadOnly = true;

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dataGridViewX3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            loaikh = 2;
            makh = dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[1].Value.ToString();
            tdiem = Convert.ToDecimal(dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[6].Value.ToString());
            CRM.frmHH_CTDiemKH form_ct = new frmHH_CTDiemKH();
            form_ct.ShowDialog();            
        }

        private void dataGridViewX2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            loaikh = 1;            
            makh = dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[1].Value.ToString();
            tdiem = Convert.ToDecimal(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[6].Value.ToString());
            diemtggt = Convert.ToDecimal(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[4].Value.ToString());
            CRM.frmHH_CTDiemKH form_ct = new frmHH_CTDiemKH();
            form_ct.ShowDialog();
            
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            String temp = "";
            if (optCN.Checked == true)
            {
                temp = "XeploaikhachhangCN.xls";
            }
            else
            {
                temp = "XeploaikhachhangDN.xls";
            }

            saveFileDialog1.FileName = temp.Replace("/", "-");
            saveFileDialog1.Filter = " Excel (*.xls)|*.xls|Tất cả (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            string path = "";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                path = saveFileDialog1.FileName;
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 30;
                if (optCN.Checked == true)
                {
                    for (int i = 0; i < dataGridViewX2.Rows.Count; i++)
                    {
                        DataGridViewRow row = dataGridViewX2.Rows[i];
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            ExcelApp.Cells[i + 1, j + 1] = row.Cells[j].Value.ToString();
                        }
                    }
                }
                else
                    for (int i = 0; i < dataGridViewX3.Rows.Count; i++)
                    {
                        DataGridViewRow row = dataGridViewX3.Rows[i];
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            ExcelApp.Cells[i + 1, j + 1] = row.Cells[j].Value.ToString();
                        }
                    }
                ExcelApp.ActiveWorkbook.SaveCopyAs(path);
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
                MessageBox.Show("Đã Lưu");
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tuthang = dtpFrom.Text;
            denthang = dtpTo.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            if (optCN.Checked == true)
            {
                loaikh = 1;
                DataTable dt = new DataTable();
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                //strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                //strCmd += " where ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";

                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=" + loaikh + " and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                myCommand = new SqlCommand(strCmd, DataAccess.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);
                DataAccess.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGGT"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;
                dataGridViewX2.Columns[0].ReadOnly = true;
                dataGridViewX2.Columns[1].ReadOnly = true;
                dataGridViewX2.Columns[2].ReadOnly = true;
                dataGridViewX2.Columns[3].ReadOnly = true;
                dataGridViewX2.Columns[4].ReadOnly = true;
                dataGridViewX2.Columns[5].ReadOnly = true;
                dataGridViewX2.Columns[6].ReadOnly = true;
                dataGridViewX2.Columns[7].ReadOnly = false;
                dataGridViewX2.Columns[8].ReadOnly = false;
                dataGridViewX2.Columns[9].ReadOnly = true;
                dataGridViewX2.Columns[10].ReadOnly = true;
                dataGridViewX2.Columns[11].ReadOnly = true;

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {
                loaikh = 2;
                DataTable dt = new DataTable();
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                //strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                //strCmd += " where ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=" + loaikh + " and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                myCommand = new SqlCommand(strCmd, DataAccess.conn);
                myCommand.CommandTimeout = 0;
                adapter.SelectCommand = myCommand;
                adapter.Fill(dt);
                DataAccess.conn.Close();
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;
                dataGridViewX3.Columns[0].ReadOnly = true;
                dataGridViewX3.Columns[1].ReadOnly = true;
                dataGridViewX3.Columns[2].ReadOnly = true;
                dataGridViewX3.Columns[3].ReadOnly = true;
                dataGridViewX3.Columns[4].ReadOnly = true;
                dataGridViewX3.Columns[5].ReadOnly = true;
                dataGridViewX3.Columns[6].ReadOnly = true;
                dataGridViewX3.Columns[7].ReadOnly = false;
                dataGridViewX3.Columns[8].ReadOnly = false;
                dataGridViewX3.Columns[9].ReadOnly = true;
                dataGridViewX3.Columns[10].ReadOnly = true;
                dataGridViewX3.Columns[11].ReadOnly = true;

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
 
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (optCN.Checked == true)
            {
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                strCmd += " where ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and khachhang.hoten like N'%" + textBox2.Text + "%'";
                
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and khachhang.hoten like N'%" + textBox2.Text + "%' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' ";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                int iRows = dt.Rows.Count;
                if (iRows == 0)
                {
                    strCmd = "select ketquaxeploai.*,khachhang.hoten,'' as tenloai from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and khachhang.hoten like N'%" + textBox2.Text + "%' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                    DataAccess.conn.Close();
                    iRows = dt.Rows.Count;
                    if (iRows == 0)
                    {
                        strCmd = "select makh,hoten,0 as diem_sdbq,0 as diem_spdv,0 as diem_TGGT,0 as tongdiemdl,0 as dinhtinh,'' as diengiai,0 as tongdiem,'' as tenloai,''as xeploai from khachhang where khachhang.loaikh=1 and khachhang.hoten like N'%" + textBox2.Text + "%' and left(khachhang.makh,4)='" + Thongtindangnhap.macn + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        iRows = dt.Rows.Count;
                    }
                }
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);



                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGGT"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;
                dataGridViewX2.Columns[0].ReadOnly = true;
                dataGridViewX2.Columns[1].ReadOnly = true;
                dataGridViewX2.Columns[2].ReadOnly = true;
                dataGridViewX2.Columns[3].ReadOnly = true;
                dataGridViewX2.Columns[4].ReadOnly = true;
                dataGridViewX2.Columns[5].ReadOnly = true;
                dataGridViewX2.Columns[6].ReadOnly = true;
                dataGridViewX2.Columns[7].ReadOnly = false;
                dataGridViewX2.Columns[8].ReadOnly = false;
                dataGridViewX2.Columns[9].ReadOnly = true;
                dataGridViewX2.Columns[10].ReadOnly = true;
                dataGridViewX2.Columns[11].ReadOnly = true;

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                strCmd += " where ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and khachhang.hoten like N'%" + textBox2.Text + "%'";
                
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and ketquaxeploai.makh like N'%" + textBox2.Text + "%' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                dt.Clear();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                int iRows = dt.Rows.Count;
                if (iRows == 0)
                {
                    strCmd = "select ketquaxeploai.*,khachhang.hoten,'' as tenloai from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and ketquaxeploai.makh like N'%" + textBox2.Text + "%' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    dt.Clear();
                    new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                    DataAccess.conn.Close();
                    iRows = dt.Rows.Count;
                    if (iRows == 0)
                    {
                        strCmd = "select makh,hoten,0 as diem_sdbq,0 as diem_spdv,0 as diem_pfofit,0 as tongdiemdl,0 as dinhtinh,'' as diengiai,0 as tongdiem,'' as tenloai,''as xeploai from khachhang where khachhang.loaikh=2 and khachhang.hoten like N'%" + textBox2.Text + "%' and left(khachhang.makh,4)='" + Thongtindangnhap.macn + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        dt.Clear();
                        new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        iRows = dt.Rows.Count;
                    }
                }
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;
                dataGridViewX3.Columns[0].ReadOnly = true;
                dataGridViewX3.Columns[1].ReadOnly = true;
                dataGridViewX3.Columns[2].ReadOnly = true;
                dataGridViewX3.Columns[3].ReadOnly = true;
                dataGridViewX3.Columns[4].ReadOnly = true;
                dataGridViewX3.Columns[5].ReadOnly = true;
                dataGridViewX3.Columns[6].ReadOnly = true;
                dataGridViewX3.Columns[7].ReadOnly = false;
                dataGridViewX3.Columns[8].ReadOnly = false;
                dataGridViewX3.Columns[9].ReadOnly = true;
                dataGridViewX3.Columns[10].ReadOnly = true;
                dataGridViewX3.Columns[11].ReadOnly = true;

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (optCN.Checked == true)
            {
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                strCmd += " where ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and (ketquaxeploai.tongdiem between '" + txtTu.Text.Trim() + "' and '" + txtDen.Text.Trim() + "')";
                
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and (ketquaxeploai.tongdiem between '" + txtTu.Text.Trim() + "' and '" + txtDen.Text.Trim() + "') and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' ";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                int iRows = dt.Rows.Count;
                if (iRows == 0)
                {
                    strCmd = "select ketquaxeploai.*,khachhang.hoten,'' as tenloai from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and (ketquaxeploai.tongdiem between '" + txtTu.Text.Trim() + "' and '" + txtDen.Text.Trim() + "') and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                    DataAccess.conn.Close();
                    iRows = dt.Rows.Count;
                    if (iRows == 0)
                    {
                        strCmd = "select makh,hoten,0 as diem_sdbq,0 as diem_spdv,0 as diem_TGGT,0 as tongdiemdl,0 as dinhtinh,'' as diengiai,0 as tongdiem,'' as tenloai,''as xeploai from khachhang where khachhang.loaikh=1 and left(khachhang.makh,4)='" + Thongtindangnhap.macn + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        iRows = dt.Rows.Count;
                    }
                }
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);



                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGGT"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;
                dataGridViewX2.Columns[0].ReadOnly = true;
                dataGridViewX2.Columns[1].ReadOnly = true;
                dataGridViewX2.Columns[2].ReadOnly = true;
                dataGridViewX2.Columns[3].ReadOnly = true;
                dataGridViewX2.Columns[4].ReadOnly = true;
                dataGridViewX2.Columns[5].ReadOnly = true;
                dataGridViewX2.Columns[6].ReadOnly = true;
                dataGridViewX2.Columns[7].ReadOnly = false;
                dataGridViewX2.Columns[8].ReadOnly = false;
                dataGridViewX2.Columns[9].ReadOnly = true;
                dataGridViewX2.Columns[10].ReadOnly = true;
                dataGridViewX2.Columns[11].ReadOnly = true;

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {
                strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                strCmd += " where ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and (ketquaxeploai.tongdiem between '" + txtTu.Text.Trim() + "' and '" + txtDen.Text.Trim() + "')";
                
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and (ketquaxeploai.tongdiem between '" + txtTu.Text.Trim() + "' and '" + txtDen.Text.Trim() + "') and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                int iRows = dt.Rows.Count;
                if (iRows == 0)
                {
                    strCmd = "select ketquaxeploai.*,khachhang.hoten,'' as tenloai from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and (ketquaxeploai.tongdiem between '" + txtTu.Text.Trim() + "' and '" + txtDen.Text.Trim() + "') and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                    DataAccess.conn.Close();
                    iRows = dt.Rows.Count;
                    if (iRows == 0)
                    {
                        strCmd = "select makh,hoten,0 as diem_sdbq,0 as diem_spdv,0 as diem_TGGT,0 as tongdiemdl,0 as dinhtinh,'' as diengiai,0 as tongdiem,'' as tenloai,''as xeploai from khachhang where khachhang.loaikh=2 and left(khachhang.makh,4)='" + Thongtindangnhap.macn + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        iRows = dt.Rows.Count;
                    }
                }
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;
                dataGridViewX3.Columns[0].ReadOnly = true;
                dataGridViewX3.Columns[1].ReadOnly = true;
                dataGridViewX3.Columns[2].ReadOnly = true;
                dataGridViewX3.Columns[3].ReadOnly = true;
                dataGridViewX3.Columns[4].ReadOnly = true;
                dataGridViewX3.Columns[5].ReadOnly = true;
                dataGridViewX3.Columns[6].ReadOnly = true;
                dataGridViewX3.Columns[7].ReadOnly = false;
                dataGridViewX3.Columns[8].ReadOnly = false;
                dataGridViewX3.Columns[9].ReadOnly = true;
                dataGridViewX3.Columns[10].ReadOnly = true;
                dataGridViewX3.Columns[11].ReadOnly = true;

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (optCN.Checked == true)
            {
                if (textBox3.Text.Trim() != "")
                {
                    strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                    strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                    strCmd += " where ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and dmxeploaikh.tenloai like N'%" + textBox3.Text + "%'";
                }
                else
                {
                    strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                    strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                    strCmd += " where ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                }
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and dmxeploaikh.tenloai like N'%" + textBox3.Text + "%' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' ";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();

                int iRows = dt.Rows.Count;
                if (iRows == 0)
                {
                    strCmd = "select ketquaxeploai.*,khachhang.hoten,'' as tenloai from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=1 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                    DataAccess.conn.Close();
                    iRows = dt.Rows.Count;
                    if (iRows == 0)
                    {
                        strCmd = "select makh,hoten,0 as diem_sdbq,0 as diem_spdv,0 as diem_TGGT,0 as tongdiemdl,0 as dinhtinh,'' as diengiai,0 as tongdiem,'' as tenloai,''as xeploai from khachhang where khachhang.loaikh=1 and left(khachhang.makh,4)='" + Thongtindangnhap.macn + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        iRows = dt.Rows.Count;
                    }
                }
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm thời gian gửi", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);



                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_TGGT"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX2.DataSource = dskh;
                dataGridViewX2.Columns[0].ReadOnly = true;
                dataGridViewX2.Columns[1].ReadOnly = true;
                dataGridViewX2.Columns[2].ReadOnly = true;
                dataGridViewX2.Columns[3].ReadOnly = true;
                dataGridViewX2.Columns[4].ReadOnly = true;
                dataGridViewX2.Columns[5].ReadOnly = true;
                dataGridViewX2.Columns[6].ReadOnly = true;
                dataGridViewX2.Columns[7].ReadOnly = false;
                dataGridViewX2.Columns[8].ReadOnly = false;
                dataGridViewX2.Columns[9].ReadOnly = true;
                dataGridViewX2.Columns[10].ReadOnly = true;
                dataGridViewX2.Columns[11].ReadOnly = true;

                dataGridViewX2.Columns[0].FillWeight = 30;
                dataGridViewX2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (optDN.Checked == true)
            {
                if (textBox3.Text.Trim() != "")
                {
                    strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                    strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                    strCmd += " where ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "' and dmxeploaikh.tenloai like N'%" + textBox3.Text + "%'";
                }
                else
                {
                    strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai left join khachhang on ketquaxeploai.makh=khachhang.makh ";
                    strCmd += " left join dmxeploaikh on ketquaxeploai.xeploai=dmxeploaikh.maloai ";
                    strCmd += " where ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                }
                //strCmd = "select ketquaxeploai.*,dmxeploaikh.tenloai,khachhang.hoten from ketquaxeploai,khachhang,dmxeploaikh where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.xeploai=dmxeploaikh.maloai and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and dmxeploaikh.tenloai like N'%" + textBox3.Text + "%' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                if (DataAccess.conn.State == ConnectionState.Open)
                {
                    DataAccess.conn.Close();
                }
                DataAccess.conn.Open();
                new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                DataAccess.conn.Close();
                int iRows = dt.Rows.Count;
                if (iRows == 0)
                {
                    strCmd = "select ketquaxeploai.*,khachhang.hoten,'' as tenloai from ketquaxeploai,khachhang where khachhang.makh=ketquaxeploai.makh and ketquaxeploai.loaikh=2 and tuthang='" + dtpFrom.Text + "' and denthang='" + dtpTo.Text + "' and left(ketquaxeploai.makh,4)='" + Thongtindangnhap.macn + "'";
                    if (DataAccess.conn.State == ConnectionState.Open)
                    {
                        DataAccess.conn.Close();
                    }
                    DataAccess.conn.Open();
                    new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                    DataAccess.conn.Close();
                    iRows = dt.Rows.Count;
                    if (iRows == 0)
                    {
                        strCmd = "select makh,hoten,0 as diem_sdbq,0 as diem_spdv,0 as diem_TGGT,0 as tongdiemdl,0 as dinhtinh,'' as diengiai,0 as tongdiem,'' as tenloai,''as xeploai from khachhang where khachhang.loaikh=2 and left(khachhang.makh,4)='" + Thongtindangnhap.macn + "'";
                        if (DataAccess.conn.State == ConnectionState.Open)
                        {
                            DataAccess.conn.Close();
                        }
                        DataAccess.conn.Open();
                        new SqlDataAdapter(strCmd, DataAccess.conn).Fill(dt);
                        DataAccess.conn.Close();
                        iRows = dt.Rows.Count;
                    }
                }
                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã khách hàng", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm SDBQ", typeof(string));
                dskh.Columns.Add(col);

                col = new DataColumn("Điểm tỷ suất lợi nhuận", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm sử dụng SPDV", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm định lượng(theo tỷ trọng)", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Điểm định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Đánh giá định tính", typeof(string));

                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã loại", typeof(string));
                dskh.Columns.Add(col);

                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        row[0] = i + 1;

                        row[1] = dt.Rows[i]["makh"].ToString();

                        row[2] = dt.Rows[i]["hoten"].ToString();
                        row[3] = dt.Rows[i]["diem_sdbq"].ToString();
                        row[4] = dt.Rows[i]["diem_profit"].ToString();
                        row[5] = dt.Rows[i]["diem_spdv"].ToString();
                        row[6] = dt.Rows[i]["tongdiemdl"].ToString();
                        row[7] = dt.Rows[i]["dinhtinh"].ToString();
                        row[8] = dt.Rows[i]["Diengiai"].ToString();
                        row[9] = dt.Rows[i]["Tongdiem"].ToString();
                        row[10] = dt.Rows[i]["tenloai"].ToString();
                        row[11] = dt.Rows[i]["xeploai"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dataGridViewX3.DataSource = dskh;
                dataGridViewX3.Columns[0].ReadOnly = true;
                dataGridViewX3.Columns[1].ReadOnly = true;
                dataGridViewX3.Columns[2].ReadOnly = true;
                dataGridViewX3.Columns[3].ReadOnly = true;
                dataGridViewX3.Columns[4].ReadOnly = true;
                dataGridViewX3.Columns[5].ReadOnly = true;
                dataGridViewX3.Columns[6].ReadOnly = true;
                dataGridViewX3.Columns[7].ReadOnly = false;
                dataGridViewX3.Columns[8].ReadOnly = false;
                dataGridViewX3.Columns[9].ReadOnly = true;
                dataGridViewX3.Columns[10].ReadOnly = true;
                dataGridViewX3.Columns[11].ReadOnly = true;

                dataGridViewX3.Columns[0].FillWeight = 30;
                dataGridViewX3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewX3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridViewX3.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        
    }
}