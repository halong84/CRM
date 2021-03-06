using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CRM
{
    public partial class frmSearch_KHTT : Form
    {
        public frmSearch_KHTT()
        {
            InitializeComponent();
        }

        private void frmSearch_KHTT_Load(object sender, EventArgs e)
        {
            
        }

        private void layDSKHCN()
        {
            String Strcmd = "";

            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachCN.Refresh();
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
            col = new DataColumn("Xếp loại", typeof(string));   //11
            dtDanhsach.Columns.Add(col);        
            dtResult.Clear();

            Strcmd = "select khachhang.*,KETQUAXEPLOAI.xeploai from KHACHHANG left join KETQUAXEPLOAI on KHACHHANG.MAKH=KETQUAXEPLOAI.MAKH where XEPLOAI<>'' and PDTT=1 and khachhang.LOAIKH=1 and KETQUAXEPLOAI.DENTHANG=(select top 1 DENTHANG from KETQUAXEPLOai where pdtt=1 order by convert(date,'01/'+denthang) desc)";

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
                    row[11] = dtResult.Rows[i]["Xeploai"].ToString();                                               
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachCN.DataSource = dtDanhsach;
            dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachCN.Columns[0].Width = 40;
            


        }

        private void btnSTen_Click(object sender, EventArgs e)
        {
            if (txtSTen.Text == "")
                layDSKHCN();
            else
            {
                String Strcmd = "";

                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsachCN.Refresh();
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
                col = new DataColumn("Xếp loại", typeof(string));   //11
                dtDanhsach.Columns.Add(col);
                dtResult.Clear();

                Strcmd = "select khachhang.*,KETQUAXEPLOAI.xeploai from KHACHHANG left join KETQUAXEPLOAI on KHACHHANG.MAKH=KETQUAXEPLOAI.MAKH where khachhang.LOAIKH=1 and khachhang.hoten like N'%" + txtSTen.Text + "%' and XEPLOAI<>'' and PDTT=1 and KETQUAXEPLOAI.DENTHANG=(select top 1 DENTHANG from KETQUAXEPLOai where pdtt=1 and left(makh,4)='"+frmMain.cn+"' order by convert(date,'01/'+denthang) desc)";

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
                        row[11] = dtResult.Rows[i]["Xeploai"].ToString();
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsachCN.DataSource = dtDanhsach;
                dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsachCN.Columns[0].Width = 40;
                
            }
        }

        private void btnSMaKH_Click(object sender, EventArgs e)
        {
            if (txtSMaKH.Text == "")
                layDSKHCN();
            else
            {
                String Strcmd = "";

                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsachCN.Refresh();
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
                col = new DataColumn("Xếp loại", typeof(string));   //11
                dtDanhsach.Columns.Add(col);
                dtResult.Clear();

                Strcmd = "select khachhang.*,KETQUAXEPLOAI.xeploai from KHACHHANG left join KETQUAXEPLOAI on KHACHHANG.MAKH=KETQUAXEPLOAI.MAKH where khachhang.makh like'" + txtSMaKH.Text + "%' and XEPLOAI<>'' and PDTT=1 and khachhang.LOAIKH=1 and KETQUAXEPLOAI.DENTHANG=(select top 1 DENTHANG from KETQUAXEPLOai where pdtt=1 and left(makh,4)='" + frmMain.cn + "' order by convert(date,'01/'+denthang) desc)";

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
                        row[11] = dtResult.Rows[i]["Xeploai"].ToString();
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsachCN.DataSource = dtDanhsach;
                dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsachCN.Columns[0].Width = 40;

            }
        }

        private void btnSTel_Click(object sender, EventArgs e)
        {
            if (txtSTel.Text == "")
                layDSKHCN();
            else
            {
                String Strcmd = "";

                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsachCN.Refresh();
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
                col = new DataColumn("Xếp loại", typeof(string));   //11
                dtDanhsach.Columns.Add(col);
                dtResult.Clear();

                Strcmd = "select khachhang.*,KETQUAXEPLOAI.xeploai from KHACHHANG left join KETQUAXEPLOAI on KHACHHANG.MAKH=KETQUAXEPLOAI.MAKH where khachhang.dienthoai1 like '%" + txtSTel.Text + "' and XEPLOAI<>'' and PDTT=1 and khachhang.LOAIKH=1 and KETQUAXEPLOAI.DENTHANG=(select top 1 DENTHANG from KETQUAXEPLOai where pdtt=1 and left(makh,4)='" + frmMain.cn + "' order by convert(date,'01/'+denthang) desc)";

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
                        row[11] = dtResult.Rows[i]["Xeploai"].ToString();
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsachCN.DataSource = dtDanhsach;
                dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsachCN.Columns[0].Width = 40;

            }
        }

        private void btnSCMND_Click(object sender, EventArgs e)
        {
            if (txtSCMND.Text == "")
                layDSKHCN();
            else
            {
                String Strcmd = "";

                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsachCN.Refresh();
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
                col = new DataColumn("Xếp loại", typeof(string));   //11
                dtDanhsach.Columns.Add(col);
                dtResult.Clear();

                Strcmd = "select khachhang.*,KETQUAXEPLOAI.xeploai from KHACHHANG left join KETQUAXEPLOAI on KHACHHANG.MAKH=KETQUAXEPLOAI.MAKH where khachhang.cmnd = '" + txtSCMND.Text + "' and XEPLOAI<>'' and PDTT=1 and khachhang.LOAIKH=1 and KETQUAXEPLOAI.DENTHANG=(select top 1 DENTHANG from KETQUAXEPLOai where pdtt=1 and left(makh,4)='" + frmMain.cn + "' order by convert(date,'01/'+denthang) desc)";

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
                        row[11] = dtResult.Rows[i]["Xeploai"].ToString();
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsachCN.DataSource = dtDanhsach;
                dgvDanhsachCN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachCN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsachCN.Columns[0].Width = 40;

            }
        }

        private void layDSKHDN()
        {
            String Strcmd = "";

            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsachDN.Refresh();
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
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xếp loại", typeof(string));   //11
            dtDanhsach.Columns.Add(col);
            dtResult.Clear();

            Strcmd = "select khachhang.*,KETQUAXEPLOAI.xeploai from KHACHHANG left join KETQUAXEPLOAI on KHACHHANG.MAKH=KETQUAXEPLOAI.MAKH where XEPLOAI<>'' and PDTT=1 and khachhang.LOAIKH=2 and KETQUAXEPLOAI.DENTHANG=(select top 1 DENTHANG from KETQUAXEPLOai where pdtt=1 order by convert(date,'01/'+denthang) desc)";

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
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[4] = dtResult.Rows[i]["GPDK"].ToString();
                    row[5] = dtResult.Rows[i]["QDTL"].ToString();
                    row[6] = dtResult.Rows[i]["MST"].ToString();
                    row[7] = dtResult.Rows[i]["xeploai"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsachDN.DataSource = dtDanhsach;
            dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsachDN.Columns[0].Width = 40;



        }

        private void btnDN_STen_Click(object sender, EventArgs e)
        {
            if (txtDN_STen.Text == "")
                layDSKHDN();
            else
            {
                String Strcmd = "";

                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsachDN.Refresh();
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
                col = new DataColumn("Giấy phép ĐK", typeof(string));   //4
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("QĐ thành lập", typeof(string));   //5
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("MST", typeof(string));    //6
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));   //11
                dtDanhsach.Columns.Add(col);
                dtResult.Clear();

                Strcmd = "select khachhang.*,KETQUAXEPLOAI.xeploai from KHACHHANG left join KETQUAXEPLOAI on KHACHHANG.MAKH=KETQUAXEPLOAI.MAKH where XEPLOAI<>'' and PDTT=1 and khachhang.LOAIKH=2 and khachhang.hoten like N'%" + txtDN_STen.Text + "%' and KETQUAXEPLOAI.DENTHANG=(select top 1 DENTHANG from KETQUAXEPLOai where pdtt=1 and left(makh,4)='" + frmMain.cn + "' order by convert(date,'01/'+denthang) desc)";

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
                        row[1] = dtResult.Rows[i]["MaKH"].ToString();
                        row[2] = dtResult.Rows[i]["Hoten"].ToString();
                        row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                        row[4] = dtResult.Rows[i]["GPDK"].ToString();
                        row[5] = dtResult.Rows[i]["QDTL"].ToString();
                        row[6] = dtResult.Rows[i]["MST"].ToString();
                        row[7] = dtResult.Rows[i]["xeploai"].ToString();
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsachDN.DataSource = dtDanhsach;
                dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsachDN.Columns[0].Width = 40;
  
            }
        }

        private void btnDN_SMaKH_Click(object sender, EventArgs e)
        {
            if (txtDN_SMaKH.Text == "")
                layDSKHDN();
            else
            {
                String Strcmd = "";

                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsachDN.Refresh();
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
                col = new DataColumn("Giấy phép ĐK", typeof(string));   //4
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("QĐ thành lập", typeof(string));   //5
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("MST", typeof(string));    //6
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));   //11
                dtDanhsach.Columns.Add(col);
                dtResult.Clear();

                Strcmd = "select khachhang.*,KETQUAXEPLOAI.xeploai from KHACHHANG left join KETQUAXEPLOAI on KHACHHANG.MAKH=KETQUAXEPLOAI.MAKH where XEPLOAI<>'' and PDTT=1 and khachhang.LOAIKH=2 and khachhang.makh like'" + txtDN_SMaKH.Text + "%' and KETQUAXEPLOAI.DENTHANG=(select top 1 DENTHANG from KETQUAXEPLOai where pdtt=1 and left(makh,4)='" + frmMain.cn + "' order by convert(date,'01/'+denthang) desc)";

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
                        row[1] = dtResult.Rows[i]["MaKH"].ToString();
                        row[2] = dtResult.Rows[i]["Hoten"].ToString();
                        row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                        row[4] = dtResult.Rows[i]["GPDK"].ToString();
                        row[5] = dtResult.Rows[i]["QDTL"].ToString();
                        row[6] = dtResult.Rows[i]["MST"].ToString();
                        row[7] = dtResult.Rows[i]["xeploai"].ToString();
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsachDN.DataSource = dtDanhsach;
                dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsachDN.Columns[0].Width = 40;

            }
        }

        private void btnDN_STel_Click(object sender, EventArgs e)
        {
            if (txtDN_SGPDK.Text == "")
                layDSKHDN();
            else
            {
                String Strcmd = "";

                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsachDN.Refresh();
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
                col = new DataColumn("Giấy phép ĐK", typeof(string));   //4
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("QĐ thành lập", typeof(string));   //5
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("MST", typeof(string));    //6
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));   //11
                dtDanhsach.Columns.Add(col);
                dtResult.Clear();

                Strcmd = "select khachhang.*,KETQUAXEPLOAI.xeploai from KHACHHANG left join KETQUAXEPLOAI on KHACHHANG.MAKH=KETQUAXEPLOAI.MAKH where XEPLOAI<>'' and PDTT=1 and khachhang.gpdk='" + txtDN_SGPDK.Text + "' and khachhang.LOAIKH=2 and KETQUAXEPLOAI.DENTHANG=(select top 1 DENTHANG from KETQUAXEPLOai where pdtt=1 and left(makh,4)='" + frmMain.cn + "' order by convert(date,'01/'+denthang) desc)";

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
                        row[1] = dtResult.Rows[i]["MaKH"].ToString();
                        row[2] = dtResult.Rows[i]["Hoten"].ToString();
                        row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                        row[4] = dtResult.Rows[i]["GPDK"].ToString();
                        row[5] = dtResult.Rows[i]["QDTL"].ToString();
                        row[6] = dtResult.Rows[i]["MST"].ToString();
                        row[7] = dtResult.Rows[i]["xeploai"].ToString();
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsachDN.DataSource = dtDanhsach;
                dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsachDN.Columns[0].Width = 40;

            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (txtDN_SMST.Text == "")
                layDSKHDN();
            else
            {
                String Strcmd = "";

                Cursor.Current = Cursors.WaitCursor;
                dgvDanhsachDN.Refresh();
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
                col = new DataColumn("Giấy phép ĐK", typeof(string));   //4
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("QĐ thành lập", typeof(string));   //5
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("MST", typeof(string));    //6
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("Xếp loại", typeof(string));   //11
                dtDanhsach.Columns.Add(col);
                dtResult.Clear();

                Strcmd = "select khachhang.*,KETQUAXEPLOAI.xeploai from KHACHHANG left join KETQUAXEPLOAI on KHACHHANG.MAKH=KETQUAXEPLOAI.MAKH where XEPLOAI<>'' and PDTT=1 and khachhang.mst='" + txtDN_SMST.Text + "' and khachhang.LOAIKH=2 and KETQUAXEPLOAI.DENTHANG=(select top 1 DENTHANG from KETQUAXEPLOai where pdtt=1 and left(makh,4)='" + frmMain.cn + "' order by convert(date,'01/'+denthang) desc)";

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
                        row[1] = dtResult.Rows[i]["MaKH"].ToString();
                        row[2] = dtResult.Rows[i]["Hoten"].ToString();
                        row[3] = dtResult.Rows[i]["Diachi1"].ToString();
                        row[4] = dtResult.Rows[i]["GPDK"].ToString();
                        row[5] = dtResult.Rows[i]["QDTL"].ToString();
                        row[6] = dtResult.Rows[i]["MST"].ToString();
                        row[7] = dtResult.Rows[i]["xeploai"].ToString();
                        dtDanhsach.Rows.Add(row);
                    }
                    catch { }
                }
                dgvDanhsachDN.DataSource = dtDanhsach;
                dgvDanhsachDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhsachDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhsachDN.Columns[0].Width = 40;
  
            }
        }

        private void buttonX159_Click(object sender, EventArgs e)
        {
            String temp = "";
            temp = "VIPKhachHangCN.xls";

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
                for (int i = 0; i < dgvDanhsachCN.Rows.Count; i++)
                {
                    DataGridViewRow row = dgvDanhsachCN.Rows[i];
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

        private void buttonX2_Click(object sender, EventArgs e)
        {
            String temp = "";
            temp = "VIPKhachHangDN.xls";

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
                for (int i = 0; i < dgvDanhsachDN.Rows.Count; i++)
                {
                    DataGridViewRow row = dgvDanhsachDN.Rows[i];
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
    }
}