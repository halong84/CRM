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

namespace CRM
{
    public partial class frmThamkhao_KHTN : Form
    {
        public static string maKH = "";
        string strCmd = "";
        DataTable dtResult = new DataTable();

        public frmThamkhao_KHTN()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvDanhsach.RowHeadersVisible = false;
            dgvDanhsach.AllowUserToAddRows = false;
            dgvDanhsach.ReadOnly = true;
            dgvDanhsach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhsach.MultiSelect = false;

            cbbSLoaiKH.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbSLoaiKH.SelectedIndex = 0;
        }

        private void frmThamkhao_KHTN_Load(object sender, EventArgs e)
        {

        }

        private void layDS_TenKH()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Lĩnh vực", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã NV", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);

            strCmd = "Select * from Khachhangtiemnang Where Hoten like N'%" + txtSTen.Text.Trim() + "%' and macn='" + frmMain.cn + "'";
            strCmd += " Order by Hoten, MaKH ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
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
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();
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
                    row[7] = dtResult.Rows[i]["Linhvuc"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["Xa"].ToString();
                    row[10] = dtResult.Rows[i]["Huyen"].ToString();
                    row[11] = dtResult.Rows[i]["Tinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[17] = tinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0.00}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    string loaiKH = "";
                    if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    {
                        loaiKH = "Cá nhân";
                    }
                    else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    {
                        loaiKH = "Doanh nghiệp";
                    }
                    row[19] = loaiKH;
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();
                    if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                    {
                        string ngayCap, ngayC, thangC, namC;
                        ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                        ngayC = ngayCap.Substring(0, 2);
                        thangC = ngayCap.Substring(3, 2);
                        namC = ngayCap.Substring(6, 4);

                        row[22] = ngayC + "/" + thangC + "/" + namC;
                    }
                    row[23] = dtResult.Rows[i]["Noicap"].ToString();
                    row[24] = dtResult.Rows[i]["GPDK"].ToString();
                    row[25] = dtResult.Rows[i]["QDTL"].ToString();
                    row[26] = dtResult.Rows[i]["MST"].ToString();
                    row[27] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[28] = dtResult.Rows[i]["Ghichu"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch
                {
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 60;
            dgvDanhsach.Columns[1].Width = 140;
            dgvDanhsach.Columns[2].Width = 200;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[4].Width = 120;
            dgvDanhsach.Columns[5].Width = 100;
            dgvDanhsach.Columns[6].Width = 100;
            dgvDanhsach.Columns[5].Visible = false;
            dgvDanhsach.Columns[6].Visible = false;
            dgvDanhsach.Columns[7].Width = 150;
            dgvDanhsach.Columns[8].Width = 200;
            dgvDanhsach.Columns[9].Width = 120;
            dgvDanhsach.Columns[10].Width = 120;
            dgvDanhsach.Columns[11].Width = 120;
            dgvDanhsach.Columns[12].Width = 200;
            dgvDanhsach.Columns[13].Width = 200;
            dgvDanhsach.Columns[14].Width = 200;
            dgvDanhsach.Columns[15].Width = 150;
            dgvDanhsach.Columns[16].Width = 150;
            dgvDanhsach.Columns[16].Visible = false;
            dgvDanhsach.Columns[17].Width = 150;
            dgvDanhsach.Columns[18].Width = 120;
            dgvDanhsach.Columns[18].Visible = false;
            dgvDanhsach.Columns[19].Width = 120;
            dgvDanhsach.Columns[20].Width = 120;
            dgvDanhsach.Columns[21].Width = 120;
            dgvDanhsach.Columns[22].Width = 100;
            dgvDanhsach.Columns[23].Width = 150;
            dgvDanhsach.Columns[21].Visible = false;
            dgvDanhsach.Columns[22].Visible = false;
            dgvDanhsach.Columns[23].Visible = false;
            dgvDanhsach.Columns[24].Width = 150;
            dgvDanhsach.Columns[25].Width = 150;
            dgvDanhsach.Columns[26].Width = 150;
            dgvDanhsach.Columns[24].Visible = false;
            dgvDanhsach.Columns[25].Visible = false;
            dgvDanhsach.Columns[26].Visible = false;
            dgvDanhsach.Columns[27].Width = 150;
            dgvDanhsach.Columns[28].Width = 150;
            Cursor.Current = Cursors.Default;
        }

        private void layDS_MaKH()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgvDanhsach.Refresh();
            DataTable dtDanhsach = new DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));   //0
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));  //1
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tên KH", typeof(string)); //2
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT di động", typeof(string)); //3
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("ĐT nhà", typeof(string)); //4
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày sinh", typeof(string));  //5
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giới tính", typeof(string));  //6
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Lĩnh vực", typeof(string));   //7
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Xã", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Huyện", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tỉnh", typeof(string));    //8
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Địa chỉ khác", typeof(string));   //9
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Email", typeof(string));  //10
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Website", typeof(string));    //11
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("NH giao dịch", typeof(string));   //12
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Sở thích", typeof(string));   //13
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Tình trạng", typeof(string)); //14
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Thu nhập", typeof(decimal));   //15
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Loại KH", typeof(string));    //16
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Mã NV", typeof(string));  //17
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));   //18
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ngày cấp", typeof(string));   //19
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Nơi cấp", typeof(string));    //20
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("QĐ thành lập", typeof(string));   //22
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("MST", typeof(string));    //23
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Chi tiết", typeof(string));   //24
            dtDanhsach.Columns.Add(col);
            col = new DataColumn("Ghi chú", typeof(string));    //25
            dtDanhsach.Columns.Add(col);

            strCmd = "Select * from Khachhangtiemnang Where MaKH like '%" + txtSMaKH.Text.Trim() + "%' and macn='" + frmMain.cn + "' ";
            strCmd += " Order by MaKH, Hoten ";

            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
                adapter.SelectCommand.ExecuteReader();
                frmMain.conn.Close();
            }
            catch
            {
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
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
                    DataRow row = dtDanhsach.NewRow();
                    row[0] = i + 1;
                    row[1] = dtResult.Rows[i]["MaKH"].ToString();
                    row[2] = dtResult.Rows[i]["Hoten"].ToString();
                    row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
                    row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();
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
                    row[7] = dtResult.Rows[i]["Linhvuc"].ToString();
                    row[8] = dtResult.Rows[i]["Diachi1"].ToString();
                    row[9] = dtResult.Rows[i]["Xa"].ToString();
                    row[10] = dtResult.Rows[i]["Huyen"].ToString();
                    row[11] = dtResult.Rows[i]["Tinh"].ToString();
                    row[12] = dtResult.Rows[i]["Diachi2"].ToString();
                    row[13] = dtResult.Rows[i]["Email"].ToString();
                    row[14] = dtResult.Rows[i]["Website"].ToString();
                    row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
                    row[16] = dtResult.Rows[i]["Sothich"].ToString();

                    string tinhtrang = "";
                    if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
                    {
                        tinhtrang = "Hoạt động";
                    }
                    else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
                    {
                        tinhtrang = "Không hoạt động";
                    }
                    row[17] = tinhtrang;

                    if (dtResult.Rows[i]["Thunhap"].ToString() == "")
                    {
                        row[18] = 0;
                    }
                    else
                    {
                        row[18] = String.Format("{0:0.00}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
                    }

                    string loaiKH = "";
                    if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
                    {
                        loaiKH = "Cá nhân";
                    }
                    else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
                    {
                        loaiKH = "Doanh nghiệp";
                    }
                    row[19] = loaiKH;
                    row[20] = dtResult.Rows[i]["MaNV"].ToString();
                    row[21] = dtResult.Rows[i]["CMND"].ToString();
                    if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
                    {
                        string ngayCap, ngayC, thangC, namC;
                        ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

                        ngayC = ngayCap.Substring(0, 2);
                        thangC = ngayCap.Substring(3, 2);
                        namC = ngayCap.Substring(6, 4);

                        row[22] = ngayC + "/" + thangC + "/" + namC;
                    }
                    row[23] = dtResult.Rows[i]["Noicap"].ToString();
                    row[24] = dtResult.Rows[i]["GPDK"].ToString();
                    row[25] = dtResult.Rows[i]["QDTL"].ToString();
                    row[26] = dtResult.Rows[i]["MST"].ToString();
                    row[27] = dtResult.Rows[i]["CTLoaiKH"].ToString();
                    row[28] = dtResult.Rows[i]["Ghichu"].ToString();
                    dtDanhsach.Rows.Add(row);
                }
                catch { }
            }
            dgvDanhsach.DataSource = dtDanhsach;
            dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhsach.Columns[0].Width = 60;
            dgvDanhsach.Columns[1].Width = 140;
            dgvDanhsach.Columns[2].Width = 200;
            dgvDanhsach.Columns[3].Width = 120;
            dgvDanhsach.Columns[4].Width = 120;
            dgvDanhsach.Columns[5].Width = 100;
            dgvDanhsach.Columns[6].Width = 100;
            dgvDanhsach.Columns[5].Visible = false;
            dgvDanhsach.Columns[6].Visible = false;
            dgvDanhsach.Columns[7].Width = 150;
            dgvDanhsach.Columns[8].Width = 200;
            dgvDanhsach.Columns[9].Width = 120;
            dgvDanhsach.Columns[10].Width = 120;
            dgvDanhsach.Columns[11].Width = 120;
            dgvDanhsach.Columns[12].Width = 200;
            dgvDanhsach.Columns[13].Width = 200;
            dgvDanhsach.Columns[14].Width = 200;
            dgvDanhsach.Columns[15].Width = 150;
            dgvDanhsach.Columns[16].Width = 150;
            dgvDanhsach.Columns[16].Visible = false;
            dgvDanhsach.Columns[17].Width = 150;
            dgvDanhsach.Columns[18].Width = 120;
            dgvDanhsach.Columns[18].Visible = false;
            dgvDanhsach.Columns[19].Width = 120;
            dgvDanhsach.Columns[20].Width = 120;
            dgvDanhsach.Columns[21].Width = 120;
            dgvDanhsach.Columns[22].Width = 100;
            dgvDanhsach.Columns[23].Width = 150;
            dgvDanhsach.Columns[21].Visible = false;
            dgvDanhsach.Columns[22].Visible = false;
            dgvDanhsach.Columns[23].Visible = false;
            dgvDanhsach.Columns[24].Width = 150;
            dgvDanhsach.Columns[25].Width = 150;
            dgvDanhsach.Columns[26].Width = 150;
            dgvDanhsach.Columns[24].Visible = false;
            dgvDanhsach.Columns[25].Visible = false;
            dgvDanhsach.Columns[26].Visible = false;
            dgvDanhsach.Columns[27].Width = 150;
            dgvDanhsach.Columns[28].Width = 150;
            Cursor.Current = Cursors.Default;
        }

        //private void layDS_LoaiKH()
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    dgvDanhsach.Refresh();
        //    DataTable dtDanhsach = new DataTable();
        //    DataColumn col = null;
        //    col = new DataColumn("STT", typeof(int));   //0
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Mã KH", typeof(string));  //1
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Tên KH", typeof(string)); //2
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("ĐT di động", typeof(string)); //3
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("ĐT nhà", typeof(string)); //4
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Ngày sinh", typeof(string));  //5
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Giới tính", typeof(string));  //6
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Lĩnh vực", typeof(string));   //7
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Địa chỉ", typeof(string));    //8
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Xã", typeof(string));    //8
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Huyện", typeof(string));    //8
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Tỉnh", typeof(string));    //8
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Địa chỉ khác", typeof(string));   //9
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Email", typeof(string));  //10
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Website", typeof(string));    //11
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("NH giao dịch", typeof(string));   //12
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Sở thích", typeof(string));   //13
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Tình trạng", typeof(string)); //14
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Thu nhập", typeof(decimal));   //15
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Loại KH", typeof(string));    //16
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Mã NV", typeof(string));  //17
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("CMND", typeof(string));   //18
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Ngày cấp", typeof(string));   //19
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Nơi cấp", typeof(string));    //20
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Giấy phép ĐK", typeof(string));   //21
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("QĐ thành lập", typeof(string));   //22
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("MST", typeof(string));    //23
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Chi tiết", typeof(string));   //24
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Ghi chú", typeof(string));    //25
        //    dtDanhsach.Columns.Add(col);
        //    col = new DataColumn("Chọn", typeof(bool));    //25
        //    dtDanhsach.Columns.Add(col);

        //    int iloaiKH = 1;
        //    if (cbbSLoaiKH.Text.Trim() == "Cá nhân")
        //    {
        //        iloaiKH = 1;
        //    }
        //    else if (cbbSLoaiKH.Text.Trim() == "Doanh nghiệp")
        //    {
        //        iloaiKH = 2;
        //    }

        //    strCmd = "Select * from Khachhangtiemnang Where LoaiKH ='" + iloaiKH + "' and macn='" + frmMain.cn + "'";
        //    strCmd += " Order by Hoten, MaKH ";

        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    try
        //    {
        //        frmMain.conn.Open();
        //        adapter.SelectCommand = new SqlCommand(strCmd, frmMain.conn);
        //        adapter.SelectCommand.ExecuteReader();
        //        frmMain.conn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }

        //    DataSet ds = new DataSet();
        //    adapter.Fill(ds);

        //    dtResult = ds.Tables[0];

        //    int iRows = dtResult.Rows.Count;
        //    for (int i = 0; i < iRows; i++)
        //    {
        //        try
        //        {
        //            DataRow row = dtDanhsach.NewRow();
        //            row[0] = i + 1;
        //            row[1] = dtResult.Rows[i]["MaKH"].ToString();
        //            row[2] = dtResult.Rows[i]["Hoten"].ToString();
        //            row[3] = dtResult.Rows[i]["Dienthoai1"].ToString();
        //            row[4] = dtResult.Rows[i]["Dienthoai2"].ToString();
        //            if (dtResult.Rows[i]["Ngaysinh"].ToString() != "")
        //            {
        //                string ngaySinh, ngayS, thangS, namS;
        //                ngaySinh = dtResult.Rows[i]["Ngaysinh"].ToString();

        //                ngayS = ngaySinh.Substring(0, 2);
        //                thangS = ngaySinh.Substring(3, 2);
        //                namS = ngaySinh.Substring(6, 4);

        //                row[5] = ngayS + "/" + thangS + "/" + namS;
        //            }
        //            if (dtResult.Rows[i]["Gioitinh"].ToString() != "")
        //            {
        //                string gioitinh = "";
        //                if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == true)
        //                {
        //                    gioitinh = "Nam";
        //                }
        //                else if (Boolean.Parse(dtResult.Rows[i]["Gioitinh"].ToString()) == false)
        //                {
        //                    gioitinh = "Nữ";
        //                }

        //                row[6] = gioitinh;
        //            }
        //            row[7] = dtResult.Rows[i]["Linhvuc"].ToString();
        //            row[8] = dtResult.Rows[i]["Diachi1"].ToString();
        //            row[9] = dtResult.Rows[i]["Xa"].ToString();
        //            row[10] = dtResult.Rows[i]["Huyen"].ToString();
        //            row[11] = dtResult.Rows[i]["Tinh"].ToString();
        //            row[12] = dtResult.Rows[i]["Diachi2"].ToString();
        //            row[13] = dtResult.Rows[i]["Email"].ToString();
        //            row[14] = dtResult.Rows[i]["Website"].ToString();
        //            row[15] = dtResult.Rows[i]["NHGiaodich"].ToString();
        //            row[16] = dtResult.Rows[i]["Sothich"].ToString();

        //            string tinhtrang = "";
        //            if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == true)
        //            {
        //                tinhtrang = "Hoạt động";
        //            }
        //            else if (Boolean.Parse(dtResult.Rows[i]["Tinhtrang"].ToString()) == false)
        //            {
        //                tinhtrang = "Không hoạt động";
        //            }
        //            row[17] = tinhtrang;

        //            if (dtResult.Rows[i]["Thunhap"].ToString() == "")
        //            {
        //                row[18] = 0;
        //            }
        //            else
        //            {
        //                row[18] = String.Format("{0:0.00}", Decimal.Parse(dtResult.Rows[i]["Thunhap"].ToString()));
        //            }

        //            string loaiKH = "";
        //            if (dtResult.Rows[i]["LoaiKH"].ToString() == "1")
        //            {
        //                loaiKH = "Cá nhân";
        //            }
        //            else if (dtResult.Rows[i]["LoaiKH"].ToString() == "2")
        //            {
        //                loaiKH = "Doanh nghiệp";
        //            }
        //            row[19] = loaiKH;
        //            row[20] = dtResult.Rows[i]["MaNV"].ToString();
        //            row[21] = dtResult.Rows[i]["CMND"].ToString();
        //            if (dtResult.Rows[i]["Ngaycap"].ToString() != "")
        //            {
        //                string ngayCap, ngayC, thangC, namC;
        //                ngayCap = dtResult.Rows[i]["Ngaycap"].ToString();

        //                ngayC = ngayCap.Substring(0, 2);
        //                thangC = ngayCap.Substring(3, 2);
        //                namC = ngayCap.Substring(6, 4);

        //                row[22] = ngayC + "/" + thangC + "/" + namC;
        //            }
        //            row[23] = dtResult.Rows[i]["Noicap"].ToString();
        //            row[24] = dtResult.Rows[i]["GPDK"].ToString();
        //            row[25] = dtResult.Rows[i]["QDTL"].ToString();
        //            row[26] = dtResult.Rows[i]["MST"].ToString();
        //            row[27] = dtResult.Rows[i]["CTLoaiKH"].ToString();
        //            row[28] = dtResult.Rows[i]["Ghichu"].ToString();
        //            row[29] = false;
        //            dtDanhsach.Rows.Add(row);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //        }
        //    }
        //    dgvDanhsach.DataSource = dtDanhsach;
        //    dgvDanhsach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgvDanhsach.Columns[0].Width = 60;
        //    dgvDanhsach.Columns[1].Width = 140;
        //    dgvDanhsach.Columns[2].Width = 200;
        //    dgvDanhsach.Columns[3].Width = 120;
        //    dgvDanhsach.Columns[4].Width = 120;
        //    dgvDanhsach.Columns[5].Width = 100;
        //    dgvDanhsach.Columns[6].Width = 100;
        //    dgvDanhsach.Columns[7].Width = 150;
        //    dgvDanhsach.Columns[8].Width = 200;
        //    dgvDanhsach.Columns[9].Width = 120;
        //    dgvDanhsach.Columns[10].Width = 120;
        //    dgvDanhsach.Columns[11].Width = 120;
        //    dgvDanhsach.Columns[12].Width = 200;
        //    dgvDanhsach.Columns[13].Width = 200;
        //    dgvDanhsach.Columns[14].Width = 200;
        //    dgvDanhsach.Columns[15].Width = 150;
        //    dgvDanhsach.Columns[16].Width = 150;
        //    dgvDanhsach.Columns[17].Width = 150;
        //    dgvDanhsach.Columns[18].Width = 120;
        //    dgvDanhsach.Columns[19].Width = 120;
        //    dgvDanhsach.Columns[20].Width = 120;
        //    dgvDanhsach.Columns[21].Width = 120;
        //    dgvDanhsach.Columns[22].Width = 100;
        //    dgvDanhsach.Columns[23].Width = 150;
        //    dgvDanhsach.Columns[24].Width = 150;
        //    dgvDanhsach.Columns[25].Width = 150;
        //    dgvDanhsach.Columns[26].Width = 150;
        //    dgvDanhsach.Columns[24].Visible = false;
        //    dgvDanhsach.Columns[25].Visible = false;
        //    dgvDanhsach.Columns[26].Visible = false;
        //    dgvDanhsach.Columns[27].Width = 150;
        //    dgvDanhsach.Columns[28].Width = 150;
        //    Cursor.Current = Cursors.Default;
        //}

        private void btnSTen_Click(object sender, EventArgs e)
        {
            layDS_TenKH();
        }

        private void btnSMaKH_Click(object sender, EventArgs e)
        {
            layDS_MaKH();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
            maKH = dgvDanhsach.CurrentRow.Cells["Mã KH"].Value.ToString();
            //int dem = 0;
            //for (int i = 0; i < dgvDanhsach.RowCount; i++)
            //{
            //    if (dgvDanhsach.Rows[i].Cells[29].Value.ToString() == "True")
            //    {
            //        dem++;
            //        break;
            //    }
            //}

            //if (dem > 0)
            //{
            //    for (int i = 0; i < dgvDanhsach.RowCount; i++)
            //    {
            //        if (dgvDanhsach.Rows[i].Cells[29].Value.ToString() == "True")
            //        {
            //            if (Boolean.Parse(dgvDanhsach.Rows[i].Cells[29].Value.ToString()) == true)
            //            {
            //                arrKH.Add(dgvDanhsach.Rows[i].Cells["Mã KH"].Value.ToString());
            //            }


            //        }
            //    }

            //}

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoaiKH_Click(object sender, EventArgs e)
        {
            //layDS_LoaiKH();
        }

        

       
        private void frmThamkhao_KHTN_Load_1(object sender, EventArgs e)
        {

        }

    }  
}