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

namespace CRM
{
    public partial class frmKHChamsoc : Form
    {
        
        public static ArrayList arrMaKH = new ArrayList();
        DataTable dt = new DataTable();

        public frmKHChamsoc()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            
        }

        private void frmKHChamsoc_Load(object sender, EventArgs e)
        {
            layDSKH();

        }
        private void layDSKH()
        {
            String Strcmd = "";
            String StrCon = " and 1=1";
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataTable dtResult = new DataTable();
            DataTable dt = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //3
            dtDanhsach.Columns.Add(col);  
            if (CRM.frmLichchamsoc.loaikh == 1)
            {
                col = new DataColumn("ĐT di động", typeof(string)); //4
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("ĐT nhà", typeof(string)); //5
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Ngày sinh", typeof(string));  //6
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Giới tính", typeof(string));  //7
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("CMND", typeof(string));   //8
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Ngày cấp", typeof(string));   //9
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Nơi cấp", typeof(string));    //10
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Ngày kết hôn", typeof(string));   //11
                dtDanhsach.Columns.Add(col);
            }                   
               
            if (CRM.frmLichchamsoc.loaikh == 2)
            {
                col = new DataColumn("Giấy phép ĐK", typeof(string));   //12
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("QĐ thành lập", typeof(string));   //13
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("MST", typeof(string));    //14
                dtDanhsach.Columns.Add(col);
            }
            if (CRM.frmLichchamsoc.loaikh == 3)
            {
                col = new DataColumn("Điện thoại", typeof(string)); //4
                dtDanhsach.Columns.Add(col);               
                col = new DataColumn("Ngày sinh", typeof(string));  //6
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Giới tính", typeof(string));  //7
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("CMND", typeof(string));   //8
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Ngày cấp", typeof(string));   //9
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Nơi cấp", typeof(string));    //10
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Chức vụ", typeof(string)); //4
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Con nhỏ", typeof(string)); //4
                dtDanhsach.Columns.Add(col); 
               
            }    
            col = new DataColumn("Chọn", typeof(bool));   //11
            dtDanhsach.Columns.Add(col);

            //Kiem tra ma ke hoach co trong database chua
            dtResult.Clear();
            Strcmd = "select * from kehoachchamsoc where ma='" + CRM.frmLichchamsoc.strma + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            dt.Clear();
            new SqlDataAdapter(Strcmd, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            if ((CRM.frmLichchamsoc.loaikh == 1) || (CRM.frmLichchamsoc.loaikh == 2))
            {
                if (CRM.frmLichchamsoc.maTieuchi == "CS01")
                    StrCon = " and left(convert(char(10),kh.ngaysinh,101),2)>='" + CRM.frmLichchamsoc.tuthang.Substring(0, 2) + "' and left(convert(char(10),kh.ngaysinh,101),2)<='" + CRM.frmLichchamsoc.denthang.Substring(0, 2) + "'";
                if ((CRM.frmLichchamsoc.maTieuchi == "CS02") || (CRM.frmLichchamsoc.maTieuchi == "CS03"))
                    StrCon = " and kh.gioitinh=0";
                if (CRM.frmLichchamsoc.maTieuchi == "CS07")
                    StrCon = " and left(convert(char(10),kh.ngaykethon,101),2)>='" + CRM.frmLichchamsoc.tuthang.Substring(0, 2) + "' and left(convert(char(10),kh.ngaykethon,101),2)<='" + CRM.frmLichchamsoc.denthang.Substring(0, 2) + "'";
                if (CRM.frmLichchamsoc.maTieuchi == "CS06")
                    StrCon = "and left(convert(char(10),kh.ngaythanhlap,101),2)>='" + CRM.frmLichchamsoc.tuthang.Substring(0, 2) + "' and left(convert(char(10),kh.ngaythanhlap,101),2)<='" + CRM.frmLichchamsoc.denthang.Substring(0, 2) + "'";
                if (CRM.frmLichchamsoc.maTieuchi == "CS10")
                    StrCon = "and left(convert(char(10),kh.ngaytlnganh,101),2)>='" + CRM.frmLichchamsoc.tuthang.Substring(0, 2) + "' and left(convert(char(10),kh.ngaytlnganh,101),2)<='" + CRM.frmLichchamsoc.denthang.Substring(0, 2) + "'";
                if (CRM.frmLichchamsoc.xeploaikh == "9999")
                {
                    Strcmd = "select kh.* from khachhang kh,ketquaxeploai kqxl where kh.loaikh=" + CRM.frmLichchamsoc.loaikh + " and kqxl.xeploai<>'' and kqxl.pdtt=1 and kh.makh=kqxl.makh and left(kh.makh,4)='" + CRM.frmMain.cn + "'" + StrCon;
                    Strcmd = Strcmd + " and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and pdtt=1 order by ngay desc) ";
                }
                else
                {
                    Strcmd = "select kh.* from khachhang kh,ketquaxeploai kqxl where kh.loaikh=" + CRM.frmLichchamsoc.loaikh + " and kqxl.xeploai='" + CRM.frmLichchamsoc.xeploaikh + "' and kqxl.pdtt=1 and kh.makh=kqxl.makh and left(kh.makh,4)='" + CRM.frmMain.cn + "'" + StrCon;
                    Strcmd = Strcmd + " and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and pdtt=1 order by ngay desc) ";
                }
            }
            if (CRM.frmLichchamsoc.loaikh == 3)
            {
                if (CRM.frmLichchamsoc.maTieuchi == "CS01")
                    StrCon = " and left(convert(char(10),kh.ngaysinh,101),2)>='" + CRM.frmLichchamsoc.tuthang.Substring(0, 2) + "' and left(convert(char(10),kh.ngaysinh,101),2)<='" + CRM.frmLichchamsoc.denthang.Substring(0, 2) + "'";
                if ((CRM.frmLichchamsoc.maTieuchi == "CS02") || (CRM.frmLichchamsoc.maTieuchi == "CS03"))
                    StrCon = " and kh.gioitinh=0";
                if (CRM.frmLichchamsoc.maTieuchi == "CS09") 
                    StrCon = " and kh.connho=1"; 
               
                if (CRM.frmLichchamsoc.xeploaikh == "9999")
                {
                    Strcmd = "select kh.* from nguoilienhe kh,ketquaxeploai kqxl where kqxl.xeploai<>'' and kqxl.pdtt=1 and kh.makh=kqxl.makh and left(kh.makh,4)='" + CRM.frmMain.cn + "'" + StrCon;
                    Strcmd = Strcmd + " and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and pdtt=1 order by ngay desc) ";
                }
                else
                {
                    Strcmd = "select kh.* from nguoilienhe kh,ketquaxeploai kqxl where kqxl.loaikh=2 and kqxl.xeploai='" + CRM.frmLichchamsoc.xeploaikh + "' and kqxl.pdtt=1 and kh.makh=kqxl.makh and left(kh.makh,4)='" + CRM.frmMain.cn + "'" + StrCon;
                    Strcmd = Strcmd + " and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and pdtt=1 order by ngay desc) ";
                }
            }
            
            
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(Strcmd, frmMain.conn).Fill(dtResult);
            frmMain.conn.Close();

            int iRows = dtResult.Rows.Count;
            for (int i = 0; i < iRows; i++)
            {
                try
                {
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    
                    if (CRM.frmLichchamsoc.loaikh == 1)
                    {
                        row[1] = dtResult.Rows[i]["MaKH"].ToString();
                        row[2] = dtResult.Rows[i]["Hoten"].ToString();
                        row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                        row[4] = dtResult.Rows[i]["Dienthoai1"].ToString();
                        row[5] = dtResult.Rows[i]["Dienthoai2"].ToString();
                        if (dtResult.Rows[i]["Ngaysinh"].ToString() != "")
                        {
                            string ngaySinh, ngayS, thangS, namS;
                            ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                            ngayS = ngaySinh.Substring(0, 2);
                            thangS = ngaySinh.Substring(3, 2);
                            namS = ngaySinh.Substring(6, 4);

                            row[6] = ngayS + "/" + thangS + "/" + namS;
                        }
                        if (dtResult.Rows[i]["Gioitinh"].ToString() != "")
                        {
                            string gioitinh = "";
                            if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                            {
                                gioitinh = "Nam";
                            }
                            else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                            {
                                gioitinh = "Nữ";
                            }

                            row[7] = gioitinh;
                        }

                       

                        row[8] = dtResult.Rows[i]["CMND"].ToString();
                        if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                        {
                            string ngayCap, ngayC, thangC, namC;
                            ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                            ngayC = ngayCap.Substring(0, 2);
                            thangC = ngayCap.Substring(3, 2);
                            namC = ngayCap.Substring(6, 4);

                            row[9] = ngayC + "/" + thangC + "/" + namC;
                        }
                        row[10] = dtResult.Rows[i]["Noicap"].ToString();
                        if (dtResult.Rows[i]["Ngaykethon"].ToString() != "")
                        {
                            string ngayKethon, ngayKH, thangKH, namKH;
                            ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                            ngayKH = ngayKethon.Substring(0, 2);
                            thangKH = ngayKethon.Substring(3, 2);
                            namKH = ngayKethon.Substring(6, 4);

                            row[11] = ngayKH + "/" + thangKH + "/" + namKH;
                        }
                    }
                    if (CRM.frmLichchamsoc.loaikh == 2)
                    {
                        row[1] = dtResult.Rows[i]["MaKH"].ToString();
                        row[2] = dtResult.Rows[i]["Hoten"].ToString();
                        row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                        row[4] = dtResult.Rows[i]["GPDK"].ToString();
                        row[5] = dtResult.Rows[i]["QDTL"].ToString();
                        row[6] = dtResult.Rows[i]["MST"].ToString();
                    }
                    if (CRM.frmLichchamsoc.loaikh == 3)
                    {
                        row[1] = dtResult.Rows[i]["MaNLH"].ToString();
                        row[2] = dtResult.Rows[i]["Hoten"].ToString();
                        row[3] = dtResult.Rows[i]["Diachi"].ToString();
                        row[4] = dtResult.Rows[i]["Dienthoai"].ToString();
                        if (dtResult.Rows[i]["Ngaysinh"].ToString() != "")
                        {
                            string ngaySinh, ngayS, thangS, namS;
                            ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                            ngayS = ngaySinh.Substring(0, 2);
                            thangS = ngaySinh.Substring(3, 2);
                            namS = ngaySinh.Substring(6, 4);

                            row[5] = ngayS + "/" + thangS + "/" + namS;
                        }
                        if (dtResult.Rows[i]["Gioitinh"].ToString() != "")
                        {
                            string gioitinh = "";
                            if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                            {
                                gioitinh = "Nam";
                            }
                            else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                            {
                                gioitinh = "Nữ";
                            }

                            row[6] = gioitinh;
                        }
                        row[7] = dtResult.Rows[i]["CMND"].ToString();
                        if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                        {
                            string ngayCap, ngayC, thangC, namC;
                            ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                            ngayC = ngayCap.Substring(0, 2);
                            thangC = ngayCap.Substring(3, 2);
                            namC = ngayCap.Substring(6, 4);

                            row[8] = ngayC + "/" + thangC + "/" + namC;
                        }
                        row[9] = dtResult.Rows[i]["Noicap"].ToString();
                        row[10] = dtResult.Rows[i]["Chucvu"].ToString();
                        if (dtResult.Rows[i]["connho"].ToString() != "")
                        {
                            string connho = "";
                            if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == true)
                            {
                                connho = "Có";
                            }
                            else if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == false)
                            {
                                connho = "Không";
                            }

                            row[11] = connho;
                        }



                    }
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dt1 = new DataTable();
                        if ((CRM.frmLichchamsoc.loaikh == 1) || (CRM.frmLichchamsoc.loaikh == 2))
                            Strcmd = "select * from chitietkhcs ct where ma='" + CRM.frmLichchamsoc.strma + "' and makh='" + dtResult.Rows[i]["MAKH"].ToString() + "'";
                        else
                            Strcmd = "select * from chitietkhcs ct where ma='" + CRM.frmLichchamsoc.strma + "' and makh='" + dtResult.Rows[i]["MANLH"].ToString() + "'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        dt1.Clear();
                        new SqlDataAdapter(Strcmd, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();
                        if (dt1.Rows.Count > 0)
                        {
                            if ((CRM.frmLichchamsoc.loaikh == 1) || (CRM.frmLichchamsoc.loaikh == 3))
                                row[12] = true;
                            else
                                row[7] = true;
                        }
                        else
                        {
                            if ((CRM.frmLichchamsoc.loaikh == 1) || (CRM.frmLichchamsoc.loaikh == 3))
                                row[12] = false;
                            else
                                row[7] = false;
                        }
                    }
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 40;
            dgvDanhsach.Columns[1].Visible = false;
            //dgvDanhsach.Columns[4].Visible = false;
            

        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if((frmLichchamsoc.loaikh==1)||(frmLichchamsoc.loaikh==3))
                    dgvDanhsach.Rows[i].Cells[12].Value = true;
                else
                    dgvDanhsach.Rows[i].Cells[7].Value = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnDeselectall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dgvDanhsach.RowCount; i++)
            {
                if ((frmLichchamsoc.loaikh == 1)||(frmLichchamsoc.loaikh==3))
                    dgvDanhsach.Rows[i].Cells[12].Value = false;
                else
                    dgvDanhsach.Rows[i].Cells[7].Value = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSCMND_Click(object sender, EventArgs e)
        {
            if (txtSCMND.Text == "")
                layDSKH();
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsach.Refresh();
                DataTable dtDanhsach = new DataTable();
                DataTable dtResult = new DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));   //0
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Mã KH", typeof(string));  //1
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Tên KH", typeof(string)); //2
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));    //3
                dtDanhsach.Columns.Add(col);
                if (CRM.frmLichchamsoc.loaikh == 1)
                {
                    col = new DataColumn("ĐT di động", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("ĐT nhà", typeof(string)); //5
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày sinh", typeof(string));  //6
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Giới tính", typeof(string));  //7
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("CMND", typeof(string));   //8
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày cấp", typeof(string));   //9
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Nơi cấp", typeof(string));    //10
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày kết hôn", typeof(string));   //11
                    dtDanhsach.Columns.Add(col);
                }                   
               
                if (CRM.frmLichchamsoc.loaikh == 2)
                {
                    col = new DataColumn("Giấy phép ĐK", typeof(string));   //12
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("QĐ thành lập", typeof(string));   //13
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("MST", typeof(string));    //14
                    dtDanhsach.Columns.Add(col);
                }
                if (CRM.frmLichchamsoc.loaikh == 3)
                {
                    col = new DataColumn("Điện thoại", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);               
                    col = new DataColumn("Ngày sinh", typeof(string));  //6
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Giới tính", typeof(string));  //7
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("CMND", typeof(string));   //8
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày cấp", typeof(string));   //9
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Nơi cấp", typeof(string));    //10
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Chức vụ", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Con nhỏ", typeof(string)); //4
                    dtDanhsach.Columns.Add(col); 
                   
                }    
                col = new DataColumn("Chọn", typeof(bool));   //11
                dtDanhsach.Columns.Add(col);
                string Strcmd="";
                if((CRM.frmLichchamsoc.loaikh == 1)||(CRM.frmLichchamsoc.loaikh == 2))
                    Strcmd = "select kh.* from khachhang kh,ketquaxeploai kqxl where kh.loaikh=" + CRM.frmLichchamsoc.loaikh + " and kqxl.xeploai='" + CRM.frmLichchamsoc.xeploaikh + "' and kqxl.pheduyet=1 and kh.makh=kqxl.makh and kh.cmnd='" + txtSCMND.Text + "' and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and PHEDUYET=1 order by ngay desc) ";
                else
                    Strcmd = "select kh.* from nguoilienhe kh,ketquaxeploai kqxl where kqxl.loaikh= 2 and kqxl.xeploai='" + CRM.frmLichchamsoc.xeploaikh + "' and kqxl.pheduyet=1 and kh.makh=kqxl.makh and kh.cmnd='" + txtSCMND.Text + "' and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and PHEDUYET=1 order by ngay desc) ";

                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(Strcmd, frmMain.conn).Fill(dtResult);
                frmMain.conn.Close();

                int iRows = dtResult.Rows.Count;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dtDanhsach.NewRow();
                        row[0] = i + 1;
                        if (CRM.frmLichchamsoc.loaikh == 1)
                        {
                            row[1] = dtResult.Rows[i]["MaKH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                            row[4] = dtResult.Rows[i]["Dienthoai1"].ToString();
                            row[5] = dtResult.Rows[i]["Dienthoai2"].ToString();
                            if (dtResult.Rows[i]["Ngaysinh"].ToString() != "")
                            {
                                string ngaySinh, ngayS, thangS, namS;
                                ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                                ngayS = ngaySinh.Substring(0, 2);
                                thangS = ngaySinh.Substring(3, 2);
                                namS = ngaySinh.Substring(6, 4);

                                row[6] = ngayS + "/" + thangS + "/" + namS;
                            }
                            if (dtResult.Rows[i]["Gioitinh"].ToString() != "")
                            {
                                string gioitinh = "";
                                if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                                {
                                    gioitinh = "Nam";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                                {
                                    gioitinh = "Nữ";
                                }

                                row[7] = gioitinh;
                            }



                            row[8] = dtResult.Rows[i]["CMND"].ToString();
                            if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                            {
                                string ngayCap, ngayC, thangC, namC;
                                ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                                ngayC = ngayCap.Substring(0, 2);
                                thangC = ngayCap.Substring(3, 2);
                                namC = ngayCap.Substring(6, 4);

                                row[9] = ngayC + "/" + thangC + "/" + namC;
                            }
                            row[10] = dtResult.Rows[i]["Noicap"].ToString();
                            if (dtResult.Rows[i]["Ngaykethon"].ToString() != "")
                            {
                                string ngayKethon, ngayKH, thangKH, namKH;
                                ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                                ngayKH = ngayKethon.Substring(0, 2);
                                thangKH = ngayKethon.Substring(3, 2);
                                namKH = ngayKethon.Substring(6, 4);

                                row[11] = ngayKH + "/" + thangKH + "/" + namKH;
                            }
                        }
                        if (CRM.frmLichchamsoc.loaikh == 2)
                        {
                            row[1] = dtResult.Rows[i]["MaKH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                            row[4] = dtResult.Rows[i]["GPDK"].ToString();
                            row[5] = dtResult.Rows[i]["QDTL"].ToString();
                            row[6] = dtResult.Rows[i]["MST"].ToString();
                        }
                        if (CRM.frmLichchamsoc.loaikh == 3)
                        {
                            row[1] = dtResult.Rows[i]["MaNLH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi"].ToString();
                            row[4] = dtResult.Rows[i]["Dienthoai"].ToString();
                            if (dtResult.Rows[i]["Ngaysinh"].ToString() != "")
                            {
                                string ngaySinh, ngayS, thangS, namS;
                                ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                                ngayS = ngaySinh.Substring(0, 2);
                                thangS = ngaySinh.Substring(3, 2);
                                namS = ngaySinh.Substring(6, 4);

                                row[5] = ngayS + "/" + thangS + "/" + namS;
                            }
                            if (dtResult.Rows[i]["Gioitinh"].ToString() != "")
                            {
                                string gioitinh = "";
                                if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                                {
                                    gioitinh = "Nam";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                                {
                                    gioitinh = "Nữ";
                                }

                                row[6] = gioitinh;
                            }
                            row[7] = dtResult.Rows[i]["CMND"].ToString();
                            if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                            {
                                string ngayCap, ngayC, thangC, namC;
                                ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                                ngayC = ngayCap.Substring(0, 2);
                                thangC = ngayCap.Substring(3, 2);
                                namC = ngayCap.Substring(6, 4);

                                row[8] = ngayC + "/" + thangC + "/" + namC;
                            }
                            row[9] = dtResult.Rows[i]["Noicap"].ToString();
                            row[10] = dtResult.Rows[i]["Chucvu"].ToString();
                            if (dtResult.Rows[i]["connho"].ToString() != "")
                            {
                                string connho = "";
                                if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == true)
                                {
                                    connho = "Có";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == false)
                                {
                                    connho = "Không";
                                }

                                row[11] = connho;
                            }
                        }
                       
                        DataTable dt1 = new DataTable();
                        Strcmd = "select * from chitietkhcs ct where ma='" + CRM.frmLichchamsoc.strma + "' and makh='" + dtResult.Rows[i]["MaKH"].ToString() + "'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        dt1.Clear();
                        new SqlDataAdapter(Strcmd, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();
                        if (dt1.Rows.Count > 0)
                        {
                            if (CRM.frmLichchamsoc.loaikh == 2)
                                row[7] = true;
                            else
                                row[12] = true;
                        }
                        else
                        {
                            if (CRM.frmLichchamsoc.loaikh == 2)
                                row[7] = false;
                            else
                                row[12] = false;
                        }
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsach.DataSource = dtDanhsach;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Visible = false;
                
            }
        }

        private void btnSTel_Click(object sender, EventArgs e)
        {
            if (txtSTel.Text == "")
                layDSKH();
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsach.Refresh();
                DataTable dtDanhsach = new DataTable();
                DataTable dtResult = new DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));   //0
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Mã KH", typeof(string));  //1
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Tên KH", typeof(string)); //2
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));    //3
                dtDanhsach.Columns.Add(col);

                if (CRM.frmLichchamsoc.loaikh == 1)
                {
                    col = new DataColumn("ĐT di động", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("ĐT nhà", typeof(string)); //5
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày sinh", typeof(string));  //6
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Giới tính", typeof(string));  //7
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("CMND", typeof(string));   //8
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày cấp", typeof(string));   //9
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Nơi cấp", typeof(string));    //10
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày kết hôn", typeof(string));   //11
                    dtDanhsach.Columns.Add(col);
                }

                if (CRM.frmLichchamsoc.loaikh == 2)
                {
                    col = new DataColumn("Giấy phép ĐK", typeof(string));   //12
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("QĐ thành lập", typeof(string));   //13
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("MST", typeof(string));    //14
                    dtDanhsach.Columns.Add(col);
                }
                if (CRM.frmLichchamsoc.loaikh == 3)
                {
                    col = new DataColumn("Điện thoại", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày sinh", typeof(string));  //6
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Giới tính", typeof(string));  //7
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("CMND", typeof(string));   //8
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày cấp", typeof(string));   //9
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Nơi cấp", typeof(string));    //10
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Chức vụ", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Con nhỏ", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);

                }
                col = new DataColumn("Chọn", typeof(bool));   //11
                dtDanhsach.Columns.Add(col);
                string Strcmd = "";
                if ((CRM.frmLichchamsoc.loaikh == 1) || (CRM.frmLichchamsoc.loaikh == 2))
                    Strcmd = "select kh.* from khachhang kh,ketquaxeploai kqxl where kh.loaikh=" + CRM.frmLichchamsoc.loaikh + " and kqxl.xeploai='" + CRM.frmLichchamsoc.xeploaikh + "' and kqxl.pheduyet=1 and kh.makh=kqxl.makh and kh.dienthoai1='" + txtSTel.Text + "' and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and PHEDUYET=1 order by ngay desc) ";
                else
                    Strcmd = "select kh.* from nguoilienhe kh,ketquaxeploai kqxl where kqxl.loaikh=2 and kqxl.xeploai='" + CRM.frmLichchamsoc.xeploaikh + "' and kqxl.pheduyet=1 and kh.makh=kqxl.makh and kh.dienthoai='" + txtSTel.Text + "' and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and PHEDUYET=1 order by ngay desc) ";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(Strcmd, frmMain.conn).Fill(dtResult);
                frmMain.conn.Close();

                int iRows = dtResult.Rows.Count;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dtDanhsach.NewRow();
                        row[0] = i + 1;
                        if (CRM.frmLichchamsoc.loaikh == 1)
                        {
                            row[1] = dtResult.Rows[i]["MaKH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                            row[4] = dtResult.Rows[i]["Dienthoai1"].ToString();
                            row[5] = dtResult.Rows[i]["Dienthoai2"].ToString();
                            if (dtResult.Rows[i]["Ngaysinh"].ToString() != "")
                            {
                                string ngaySinh, ngayS, thangS, namS;
                                ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                                ngayS = ngaySinh.Substring(0, 2);
                                thangS = ngaySinh.Substring(3, 2);
                                namS = ngaySinh.Substring(6, 4);

                                row[6] = ngayS + "/" + thangS + "/" + namS;
                            }
                            if (dtResult.Rows[i]["Gioitinh"].ToString() != "")
                            {
                                string gioitinh = "";
                                if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                                {
                                    gioitinh = "Nam";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                                {
                                    gioitinh = "Nữ";
                                }

                                row[7] = gioitinh;
                            }



                            row[8] = dtResult.Rows[i]["CMND"].ToString();
                            if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                            {
                                string ngayCap, ngayC, thangC, namC;
                                ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                                ngayC = ngayCap.Substring(0, 2);
                                thangC = ngayCap.Substring(3, 2);
                                namC = ngayCap.Substring(6, 4);

                                row[9] = ngayC + "/" + thangC + "/" + namC;
                            }
                            row[10] = dtResult.Rows[i]["Noicap"].ToString();
                            if (dtResult.Rows[i]["Ngaykethon"].ToString() != "")
                            {
                                string ngayKethon, ngayKH, thangKH, namKH;
                                ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                                ngayKH = ngayKethon.Substring(0, 2);
                                thangKH = ngayKethon.Substring(3, 2);
                                namKH = ngayKethon.Substring(6, 4);

                                row[11] = ngayKH + "/" + thangKH + "/" + namKH;
                            }
                        }
                        if (CRM.frmLichchamsoc.loaikh == 2)
                        {
                            row[1] = dtResult.Rows[i]["MaKH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                            row[4] = dtResult.Rows[i]["GPDK"].ToString();
                            row[5] = dtResult.Rows[i]["QDTL"].ToString();
                            row[6] = dtResult.Rows[i]["MST"].ToString();
                        }
                        if (CRM.frmLichchamsoc.loaikh == 3)
                        {
                            row[1] = dtResult.Rows[i]["MaNLH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi"].ToString();
                            row[4] = dtResult.Rows[i]["Dienthoai"].ToString();
                            if (dtResult.Rows[i]["Ngaysinh"].ToString() != "")
                            {
                                string ngaySinh, ngayS, thangS, namS;
                                ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                                ngayS = ngaySinh.Substring(0, 2);
                                thangS = ngaySinh.Substring(3, 2);
                                namS = ngaySinh.Substring(6, 4);

                                row[5] = ngayS + "/" + thangS + "/" + namS;
                            }
                            if (dtResult.Rows[i]["Gioitinh"].ToString() != "")
                            {
                                string gioitinh = "";
                                if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                                {
                                    gioitinh = "Nam";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                                {
                                    gioitinh = "Nữ";
                                }

                                row[6] = gioitinh;
                            }
                            row[7] = dtResult.Rows[i]["CMND"].ToString();
                            if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                            {
                                string ngayCap, ngayC, thangC, namC;
                                ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                                ngayC = ngayCap.Substring(0, 2);
                                thangC = ngayCap.Substring(3, 2);
                                namC = ngayCap.Substring(6, 4);

                                row[8] = ngayC + "/" + thangC + "/" + namC;
                            }
                            row[9] = dtResult.Rows[i]["Noicap"].ToString();
                            row[10] = dtResult.Rows[i]["Chucvu"].ToString();
                            if (dtResult.Rows[i]["connho"].ToString() != "")
                            {
                                string connho = "";
                                if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == true)
                                {
                                    connho = "Có";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == false)
                                {
                                    connho = "Không";
                                }

                                row[11] = connho;
                            }
                        }

                        DataTable dt1 = new DataTable();
                        Strcmd = "select * from chitietkhcs ct where ma='" + CRM.frmLichchamsoc.strma + "' and makh='" + dtResult.Rows[i]["MaKH"].ToString() + "'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        dt1.Clear();
                        new SqlDataAdapter(Strcmd, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();
                        if (dt1.Rows.Count > 0)
                        {
                            if (CRM.frmLichchamsoc.loaikh == 2)
                                row[7] = true;
                            else
                                row[12] = true;
                        }
                        else
                        {
                            if (CRM.frmLichchamsoc.loaikh == 2)
                                row[7] = false;
                            else
                                row[12] = false;
                        }
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsach.DataSource = dtDanhsach;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Visible = false;

                
                

            } 
                   
            
        }

        private void btnSTen_Click(object sender, EventArgs e)
        {
            if (txtSTen.Text == "")
                layDSKH();
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsach.Refresh();
                DataTable dtDanhsach = new DataTable();
                DataTable dtResult = new DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));   //0
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Mã KH", typeof(string));  //1
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Tên KH", typeof(string)); //2
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));    //3
                dtDanhsach.Columns.Add(col);

                if (CRM.frmLichchamsoc.loaikh == 1)
                {
                    col = new DataColumn("ĐT di động", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("ĐT nhà", typeof(string)); //5
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày sinh", typeof(string));  //6
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Giới tính", typeof(string));  //7
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("CMND", typeof(string));   //8
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày cấp", typeof(string));   //9
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Nơi cấp", typeof(string));    //10
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày kết hôn", typeof(string));   //11
                    dtDanhsach.Columns.Add(col);
                }

                if (CRM.frmLichchamsoc.loaikh == 2)
                {
                    col = new DataColumn("Giấy phép ĐK", typeof(string));   //12
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("QĐ thành lập", typeof(string));   //13
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("MST", typeof(string));    //14
                    dtDanhsach.Columns.Add(col);
                }
                if (CRM.frmLichchamsoc.loaikh == 3)
                {
                    col = new DataColumn("Điện thoại", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày sinh", typeof(string));  //6
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Giới tính", typeof(string));  //7
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("CMND", typeof(string));   //8
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày cấp", typeof(string));   //9
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Nơi cấp", typeof(string));    //10
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Chức vụ", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Con nhỏ", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);

                }
                col = new DataColumn("Chọn", typeof(bool));   //11
                dtDanhsach.Columns.Add(col);
                string Strcmd = "";
                if ((CRM.frmLichchamsoc.loaikh == 1) || (CRM.frmLichchamsoc.loaikh == 2))
                    Strcmd = "select kh.* from khachhang kh,ketquaxeploai kqxl where kh.loaikh=" + CRM.frmLichchamsoc.loaikh + " and kqxl.xeploai='" + CRM.frmLichchamsoc.xeploaikh + "' and kqxl.pheduyet=1 and kh.makh=kqxl.makh and kh.hoten like N'%" + txtSTen.Text + "%' and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and PHEDUYET=1 order by ngay desc) ";
                else
                    Strcmd = "select kh.* from nguoilienhe kh,ketquaxeploai kqxl where kqxl.loaikh=2 and kqxl.xeploai='" + CRM.frmLichchamsoc.xeploaikh + "' and kqxl.pheduyet=1 and kh.makh=kqxl.makh and kh.hoten like N'%" + txtSTen.Text + "%' and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and PHEDUYET=1 order by ngay desc) ";

                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(Strcmd, frmMain.conn).Fill(dtResult);
                frmMain.conn.Close();

                int iRows = dtResult.Rows.Count;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dtDanhsach.NewRow();
                        row[0] = i + 1;
                        if (CRM.frmLichchamsoc.loaikh == 1)
                        {
                            row[1] = dtResult.Rows[i]["MaKH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                            row[4] = dtResult.Rows[i]["Dienthoai1"].ToString();
                            row[5] = dtResult.Rows[i]["Dienthoai2"].ToString();
                            if (dtResult.Rows[i]["Ngaysinh"].ToString() != "")
                            {
                                string ngaySinh, ngayS, thangS, namS;
                                ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                                ngayS = ngaySinh.Substring(0, 2);
                                thangS = ngaySinh.Substring(3, 2);
                                namS = ngaySinh.Substring(6, 4);

                                row[6] = ngayS + "/" + thangS + "/" + namS;
                            }
                            if (dtResult.Rows[i]["Gioitinh"].ToString() != "")
                            {
                                string gioitinh = "";
                                if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                                {
                                    gioitinh = "Nam";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                                {
                                    gioitinh = "Nữ";
                                }

                                row[7] = gioitinh;
                            }



                            row[8] = dtResult.Rows[i]["CMND"].ToString();
                            if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                            {
                                string ngayCap, ngayC, thangC, namC;
                                ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                                ngayC = ngayCap.Substring(0, 2);
                                thangC = ngayCap.Substring(3, 2);
                                namC = ngayCap.Substring(6, 4);

                                row[9] = ngayC + "/" + thangC + "/" + namC;
                            }
                            row[10] = dtResult.Rows[i]["Noicap"].ToString();
                            if (dtResult.Rows[i]["Ngaykethon"].ToString() != "")
                            {
                                string ngayKethon, ngayKH, thangKH, namKH;
                                ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                                ngayKH = ngayKethon.Substring(0, 2);
                                thangKH = ngayKethon.Substring(3, 2);
                                namKH = ngayKethon.Substring(6, 4);

                                row[11] = ngayKH + "/" + thangKH + "/" + namKH;
                            }
                        }
                        if (CRM.frmLichchamsoc.loaikh == 2)
                        {
                            row[1] = dtResult.Rows[i]["MaKH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                            row[4] = dtResult.Rows[i]["GPDK"].ToString();
                            row[5] = dtResult.Rows[i]["QDTL"].ToString();
                            row[6] = dtResult.Rows[i]["MST"].ToString();
                        }
                        if (CRM.frmLichchamsoc.loaikh == 3)
                        {
                            row[1] = dtResult.Rows[i]["MaNLH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi"].ToString();
                            row[4] = dtResult.Rows[i]["Dienthoai"].ToString();
                            if (dtResult.Rows[i]["Ngaysinh"].ToString() != "")
                            {
                                string ngaySinh, ngayS, thangS, namS;
                                ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                                ngayS = ngaySinh.Substring(0, 2);
                                thangS = ngaySinh.Substring(3, 2);
                                namS = ngaySinh.Substring(6, 4);

                                row[5] = ngayS + "/" + thangS + "/" + namS;
                            }
                            if (dtResult.Rows[i]["Gioitinh"].ToString() != "")
                            {
                                string gioitinh = "";
                                if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                                {
                                    gioitinh = "Nam";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                                {
                                    gioitinh = "Nữ";
                                }

                                row[6] = gioitinh;
                            }
                            row[7] = dtResult.Rows[i]["CMND"].ToString();
                            if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                            {
                                string ngayCap, ngayC, thangC, namC;
                                ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                                ngayC = ngayCap.Substring(0, 2);
                                thangC = ngayCap.Substring(3, 2);
                                namC = ngayCap.Substring(6, 4);

                                row[8] = ngayC + "/" + thangC + "/" + namC;
                            }
                            row[9] = dtResult.Rows[i]["Noicap"].ToString();
                            row[10] = dtResult.Rows[i]["Chucvu"].ToString();
                            if (dtResult.Rows[i]["connho"].ToString() != "")
                            {
                                string connho = "";
                                if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == true)
                                {
                                    connho = "Có";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == false)
                                {
                                    connho = "Không";
                                }

                                row[11] = connho;
                            }
                        }

                        DataTable dt1 = new DataTable();
                        Strcmd = "select * from chitietkhcs ct where ma='" + CRM.frmLichchamsoc.strma + "' and makh='" + dtResult.Rows[i]["MaKH"].ToString() + "'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        dt1.Clear();
                        new SqlDataAdapter(Strcmd, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();
                        if (dt1.Rows.Count > 0)
                        {
                            if (CRM.frmLichchamsoc.loaikh == 2)
                                row[7] = true;
                            else
                                row[12] = true;
                        }
                        else
                        {
                            if (CRM.frmLichchamsoc.loaikh == 2)
                                row[7] = false;
                            else
                                row[12] = false;
                        }
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsach.DataSource = dtDanhsach;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Visible = false;                
            }
        }

        private void btnSMaKH_Click(object sender, EventArgs e)
        {
            if (txtSMaKH.Text == "")
                layDSKH();
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsach.Refresh();
                DataTable dtDanhsach = new DataTable();
                DataTable dtResult = new DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));   //0
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Mã KH", typeof(string));  //1
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Tên KH", typeof(string)); //2
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Địa chỉ", typeof(string));    //3
                dtDanhsach.Columns.Add(col);
                if (CRM.frmLichchamsoc.loaikh == 1)
                {
                    col = new DataColumn("ĐT di động", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("ĐT nhà", typeof(string)); //5
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày sinh", typeof(string));  //6
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Giới tính", typeof(string));  //7
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("CMND", typeof(string));   //8
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày cấp", typeof(string));   //9
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Nơi cấp", typeof(string));    //10
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày kết hôn", typeof(string));   //11
                    dtDanhsach.Columns.Add(col);
                }

                if (CRM.frmLichchamsoc.loaikh == 2)
                {
                    col = new DataColumn("Giấy phép ĐK", typeof(string));   //12
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("QĐ thành lập", typeof(string));   //13
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("MST", typeof(string));    //14
                    dtDanhsach.Columns.Add(col);
                }
                if (CRM.frmLichchamsoc.loaikh == 3)
                {
                    col = new DataColumn("Điện thoại", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày sinh", typeof(string));  //6
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Giới tính", typeof(string));  //7
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("CMND", typeof(string));   //8
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Ngày cấp", typeof(string));   //9
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Nơi cấp", typeof(string));    //10
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Chức vụ", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);
                    col = new DataColumn("Con nhỏ", typeof(string)); //4
                    dtDanhsach.Columns.Add(col);

                }
                col = new DataColumn("Chọn", typeof(bool));   //11
                dtDanhsach.Columns.Add(col);
                string Strcmd = "";
                if ((CRM.frmLichchamsoc.loaikh == 1) || (CRM.frmLichchamsoc.loaikh == 2))
                    Strcmd = "select kh.* from khachhang kh,ketquaxeploai kqxl where kh.loaikh=" + CRM.frmLichchamsoc.loaikh + " and kqxl.xeploai='" + CRM.frmLichchamsoc.xeploaikh + "' and kqxl.pheduyet=1 and kh.makh=kqxl.makh and kh.makh  like'" + txtSMaKH.Text + "%' and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and PHEDUYET=1 order by ngay desc) ";
                else
                    Strcmd = "select kh.* from nguoilienhe kh,ketquaxeploai kqxl where kqxl.loaikh= 2 and kqxl.xeploai='" + CRM.frmLichchamsoc.xeploaikh + "' and kqxl.pheduyet=1 and kh.makh=kqxl.makh and kh.makh  like'" + txtSMaKH.Text + "%' and convert(date,'01/'+kqxl.denthang)=(select top 1 convert(date,'01/'+denthang) as ngay from ketquaxeploai where left(makh,4)='" + CRM.frmMain.cn + "' and PHEDUYET=1 order by ngay desc) ";

                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(Strcmd, frmMain.conn).Fill(dtResult);
                frmMain.conn.Close();

                int iRows = dtResult.Rows.Count;
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        DataRow row = dtDanhsach.NewRow();
                        row[0] = i + 1;
                        if (CRM.frmLichchamsoc.loaikh == 1)
                        {
                            row[1] = dtResult.Rows[i]["MaKH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                            row[4] = dtResult.Rows[i]["Dienthoai1"].ToString();
                            row[5] = dtResult.Rows[i]["Dienthoai2"].ToString();
                            if (dtResult.Rows[i]["Ngaysinh"].ToString() != "")
                            {
                                string ngaySinh, ngayS, thangS, namS;
                                ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                                ngayS = ngaySinh.Substring(0, 2);
                                thangS = ngaySinh.Substring(3, 2);
                                namS = ngaySinh.Substring(6, 4);

                                row[6] = ngayS + "/" + thangS + "/" + namS;
                            }
                            if (dtResult.Rows[i]["Gioitinh"].ToString() != "")
                            {
                                string gioitinh = "";
                                if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                                {
                                    gioitinh = "Nam";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                                {
                                    gioitinh = "Nữ";
                                }

                                row[7] = gioitinh;
                            }
                            row[8] = dtResult.Rows[i]["CMND"].ToString();
                            if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                            {
                                string ngayCap, ngayC, thangC, namC;
                                ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                                ngayC = ngayCap.Substring(0, 2);
                                thangC = ngayCap.Substring(3, 2);
                                namC = ngayCap.Substring(6, 4);

                                row[9] = ngayC + "/" + thangC + "/" + namC;
                            }
                            row[10] = dtResult.Rows[i]["Noicap"].ToString();
                            if (dtResult.Rows[i]["Ngaykethon"].ToString() != "")
                            {
                                string ngayKethon, ngayKH, thangKH, namKH;
                                ngayKethon = dtResult.Rows[i]["Ngaykethon"].ToString();

                                ngayKH = ngayKethon.Substring(0, 2);
                                thangKH = ngayKethon.Substring(3, 2);
                                namKH = ngayKethon.Substring(6, 4);

                                row[11] = ngayKH + "/" + thangKH + "/" + namKH;
                            }
                        }
                        if (CRM.frmLichchamsoc.loaikh == 2)
                        {
                            row[1] = dtResult.Rows[i]["MaKH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                            row[4] = dtResult.Rows[i]["GPDK"].ToString();
                            row[5] = dtResult.Rows[i]["QDTL"].ToString();
                            row[6] = dtResult.Rows[i]["MST"].ToString();
                        }
                        if (CRM.frmLichchamsoc.loaikh == 3)
                        {
                            row[1] = dtResult.Rows[i]["MaNLH"].ToString();
                            row[2] = dtResult.Rows[i]["Hoten"].ToString();
                            row[3] = dtResult.Rows[i]["Diachi"].ToString();
                            row[4] = dtResult.Rows[i]["Dienthoai"].ToString();
                            if (dtResult.Rows[i]["Ngaysinh"].ToString() != "")
                            {
                                string ngaySinh, ngayS, thangS, namS;
                                ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

                                ngayS = ngaySinh.Substring(0, 2);
                                thangS = ngaySinh.Substring(3, 2);
                                namS = ngaySinh.Substring(6, 4);

                                row[5] = ngayS + "/" + thangS + "/" + namS;
                            }
                            if (dtResult.Rows[i]["Gioitinh"].ToString() != "")
                            {
                                string gioitinh = "";
                                if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
                                {
                                    gioitinh = "Nam";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
                                {
                                    gioitinh = "Nữ";
                                }

                                row[6] = gioitinh;
                            }
                            row[7] = dtResult.Rows[i]["CMND"].ToString();
                            if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                            {
                                string ngayCap, ngayC, thangC, namC;
                                ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                                ngayC = ngayCap.Substring(0, 2);
                                thangC = ngayCap.Substring(3, 2);
                                namC = ngayCap.Substring(6, 4);

                                row[8] = ngayC + "/" + thangC + "/" + namC;
                            }
                            row[9] = dtResult.Rows[i]["Noicap"].ToString();
                            row[10] = dtResult.Rows[i]["Chucvu"].ToString();
                            if (dtResult.Rows[i]["connho"].ToString() != "")
                            {
                                string connho = "";
                                if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == true)
                                {
                                    connho = "Có";
                                }
                                else if (Boolean.Parse(dtResult.Rows[i]["connho"].ToString()) == false)
                                {
                                    connho = "Không";
                                }

                                row[11] = connho;
                            }
                        }

                        DataTable dt1 = new DataTable();
                        Strcmd = "select * from chitietkhcs ct where ma='" + CRM.frmLichchamsoc.strma + "' and makh='" + dtResult.Rows[i]["MaKH"].ToString() + "'";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        dt1.Clear();
                        new SqlDataAdapter(Strcmd, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();
                        if (dt1.Rows.Count > 0)
                        {
                            if (CRM.frmLichchamsoc.loaikh == 2)
                                row[7] = true;
                            else
                                row[12] = true;
                        }
                        else
                        {
                            if (CRM.frmLichchamsoc.loaikh == 2)
                                row[7] = false;
                            else
                                row[12] = false;
                        }
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsach.DataSource = dtDanhsach;
                dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsach.Columns[0].Width = 40;
                dgvDanhsach.Columns[1].Visible = false;

            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            arrMaKH.Clear();
            int dem = 0;
            if ((CRM.frmLichchamsoc.loaikh == 1)||(CRM.frmLichchamsoc.loaikh == 3))
            {
                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    if (dgvDanhsach.Rows[i].Cells[12].Value.ToString() == "True")
                    {
                        dem++;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    if (dgvDanhsach.Rows[i].Cells[7].Value.ToString() == "True")
                    {
                        dem++;
                        break;
                    }
                }
            }
            if (dem > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < dgvDanhsach.RowCount; i++)
                {
                    if ((frmLichchamsoc.loaikh == 1)||(frmLichchamsoc.loaikh == 3))
                    {
                        if (dgvDanhsach.Rows[i].Cells[12].Value.ToString() == "True")
                        {
                            try
                            {
                                arrMaKH.Add(dgvDanhsach.Rows[i].Cells[1].Value.ToString());
                                frmLichchamsoc.tongkinhphi = frmLichchamsoc.tongkinhphi + frmLichchamsoc.kinhphi;
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        if (dgvDanhsach.Rows[i].Cells[7].Value.ToString() == "True")
                        {
                            try
                            {
                                arrMaKH.Add(dgvDanhsach.Rows[i].Cells[1].Value.ToString());
                                frmLichchamsoc.tongkinhphi = frmLichchamsoc.tongkinhphi + frmLichchamsoc.kinhphi;
                            }
                            catch { }
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
                //frmLichchamsoc.lblTongKP.Text = Convert.ToString(frmLichchamsoc.tongkinhphi);
                this.Close();
            }
            else
            {
                MessageBox.Show("Chưa chọn dòng nào.");
            }
 
        }

        private void labelX5_Click(object sender, EventArgs e)
        {

        }
        
    }
       
    
}