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
    public partial class frmKHCS_Chitiet : Form
    {
        public frmKHCS_Chitiet()
        {
            InitializeComponent();
        }

        private void frmKHCS_Chitiet_Load(object sender, EventArgs e)
        {
            layDSKH();
        }

        private void layDSKH()
        {
            String Strcmd = "";
            
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
            if (CRM.frmPheduyet_Lichchamsoc.loaikh == "1")
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

            if (CRM.frmPheduyet_Lichchamsoc.loaikh == "2")
            {
                col = new DataColumn("Giấy phép ĐK", typeof(string));   //12
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("QĐ thành lập", typeof(string));   //13
                dtDanhsach.Columns.Add(col);
                col = new DataColumn("MST", typeof(string));    //14
                dtDanhsach.Columns.Add(col);
            }
            

            //Kiem tra ma ke hoach co trong database chua
            dtResult.Clear();

            Strcmd = "select kh.* from khachhang kh,chitietkhcs ct where ct.makh=kh.makh and ct.ma='" + frmPheduyet_Lichchamsoc.strMaKHCS+"'";
            
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
                    if (CRM.frmPheduyet_Lichchamsoc.loaikh == "1")
                    {
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
                    if (CRM.frmPheduyet_Lichchamsoc.loaikh == "2")
                    {
                        row[4] = dtResult.Rows[i]["GPDK"].ToString();
                        row[5] = dtResult.Rows[i]["QDTL"].ToString();
                        row[6] = dtResult.Rows[i]["MST"].ToString();
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

        private void btnSCMND_Click(object sender, EventArgs e)
        {
            if (txtSCMND.Text == "")
                layDSKH();
            else
            {
                String Strcmd = "";

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


                //Kiem tra ma ke hoach co trong database chua
                dtResult.Clear();

                Strcmd = "select kh.* from khachhang kh,chitietkhcs ct where ct.makh=kh.makh and ct.ma='" + frmPheduyet_Lichchamsoc.strMaKHCS + "' and kh.cmnd='"+txtSCMND.Text+"'";

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
                        if (CRM.frmLichchamsoc.loaikh == 1)
                        {
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
                            row[4] = dtResult.Rows[i]["GPDK"].ToString();
                            row[5] = dtResult.Rows[i]["QDTL"].ToString();
                            row[6] = dtResult.Rows[i]["MST"].ToString();
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
        }

    }
}