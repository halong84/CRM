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
    public partial class frmCSKH_TraCuuDiem : Form
    {
        public static string str_makh;
        public static decimal tongdiem=0;
        string strpheduyet = "";
        public static DataTable st_table = new DataTable();
        public frmCSKH_TraCuuDiem()
        {
            InitializeComponent();
        }

        private void frmCSKH_TraCuuDiem_Load(object sender, EventArgs e)
        {
            //layDiem();
        }
        private void layDiem()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            String sCommand = "";
            //sCommand = "select khachhang.MAKH,khachhang.HOTEN,diem_cn.diem as diemthangtruoc,sum(lichsudiem.diem) as diem from LICHSUDIEM,KHACHHANG,diem_cn where lichsudiem.MAKH=KHACHHANG.MAKH and left(Lichsudiem.makh,4)='"+frmMain.cn+"' and LICHSUDIEM.MAKH=diem_cn.MAKH and convert(date,'01/'+thang)=( select top 1 convert(date,'01/'+thang) as ngay from LICHSUDIEM order by ngay desc)group by LICHSUDIEM.MAKH,khachhang.MAKH,khachhang.HOTEN,diem_cn.diem";
            //sCommand = "select khachhang.MAKH,khachhang.HOTEN,diem_cn.diem as diemthangtruoc,sum(lichsudiem.diem) as diem,pheduyet,lichsudiem.thang from LICHSUDIEM inner join KHACHHANG on lichsudiem.MAKH=khachhang.makh left join diem_cn on LICHSUDIEM.MAKH = diem_cn.MAKH group by LICHSUDIEM.MAKH,LICHSUDIEM.pheduyet,khachhang.MAKH,khachhang.HOTEN,khachhang.loaikh,diem_cn.diem,LICHSUDIEM.thang having left(Lichsudiem.makh,4)='" + frmMain.cn + "' and khachhang.loaikh=1 and convert(date,'01/'+thang)=( select top 1 convert(date,'01/'+thang) as ngay from LICHSUDIEM where LEFT(makh,4)='" + frmMain.cn + "' order by ngay desc)";
            sCommand = "select khachhang.makh,khachhang.hoten,khachhang.diachi1,khachhang.cmnd,diem_cn.diem as diemthangtruoc from diem_cn,khachhang where khachhang.loaikh=1 and khachhang.makh=diem_cn.makh and left(khachhang.makh,4)='" + frmMain.cn + "'";
            sCommand = sCommand + " union select khachhang.makh,khachhang.hoten,khachhang.diachi1,khachhang.cmnd,0 as diemthangtruoc from khachhang,lichsudiem where khachhang.loaikh=1 and khachhang.makh=lichsudiem.makh and left(khachhang.makh,4)='" + frmMain.cn + "' and lichsudiem.makh not in (select makh from diem_cn where left(makh,4)='" + frmMain.cn + "')";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            sCommand = "select top 1 convert(date,'01/'+thang)as thang from LICHSUDIEM order by convert(date,'01/'+thang) desc";
            DataTable dttemp = new DataTable();
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dttemp);
            frmMain.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Điểm tháng trước", typeof(decimal));
            dskh.Columns.Add(col);
            col = new DataColumn("Số điểm", typeof(decimal));
            dskh.Columns.Add(col);
            col = new DataColumn("Tổng điểm", typeof(decimal));
            dskh.Columns.Add(col);
            col = new DataColumn("cmnd", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("diachi", typeof(string));
            dskh.Columns.Add(col);

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dskh.NewRow();
                    DataTable dt1 = new DataTable();
                    String sCmd = "select sum(diem) as diem, pheduyet,convert(date,'01/'+thang) as thang from lichsudiem group by makh,pheduyet,thang having makh='" + dt.Rows[i]["makh"].ToString() + "' and thang =(select top 1 thang from LICHSUDIEM where makh='" + dt.Rows[i]["makh"].ToString() + "' order by convert(date,'01/'+thang) desc)";
                    dt1.Clear();
                    if (frmMain.conn.State == ConnectionState.Open)
                    {
                        frmMain.conn.Close();
                    }
                    frmMain.conn.Open();
                    new SqlDataAdapter(sCmd, frmMain.conn).Fill(dt1);
                    frmMain.conn.Close();
                    row[0] = i + 1;
                    row[1] = dt.Rows[i]["makh"].ToString();
                    row[2] = dt.Rows[i]["hoten"].ToString();
                    if (dt.Rows[i]["diemthangtruoc"].ToString() == "0")
                        row[3] = 0;
                    else
                        row[3] = dt.Rows[i]["diemthangtruoc"].ToString(); 
                    row[4] = dt1.Rows[0]["diem"].ToString();
                    //ngayhl = dt.Rows[i]["ThoiGian"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(6, 4);
                    if (dt1.Rows[0]["pheduyet"].ToString() == "True")
                    {
                        strpheduyet = "True";
                        if (Convert.ToDateTime(dt1.Rows[0]["thang"].ToString()) < Convert.ToDateTime(dttemp.Rows[0]["thang"].ToString()))
                        {
                            row[4] = 0;
                            row[5] = row[3];
                        }
                        else
                        {
                            row[5] = dt.Rows[i]["diemthangtruoc"].ToString();
                            row[3] = Convert.ToDecimal(row[5].ToString()) - Convert.ToDecimal(row[4].ToString());
                        }
                    }
                    else
                        row[5] = Convert.ToDecimal(row[3].ToString()) + Convert.ToDecimal(row[4].ToString());
                    row[6] = dt.Rows[i]["cmnd"].ToString();
                    row[7] = dt.Rows[i]["diachi1"].ToString();
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;

            dgvDanhsach.Columns[0].FillWeight = 30;
            dgvDanhsach.Columns[0].Width = 20;            
            dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            layDiemCMND();
        }
        private void layDiemCMND()
        {
            if (textBox1.Text == "")
                layDiem();
            else
            {
                DataTable dt = new DataTable();
                String sCommand = "";
                //sCommand = "select khachhang.MAKH,khachhang.HOTEN,diem_cn.diem as diemthangtruoc,sum(lichsudiem.diem) as diem,pheduyet,lichsudiem.thang from LICHSUDIEM inner join KHACHHANG on lichsudiem.MAKH=khachhang.makh left join diem_cn on LICHSUDIEM.MAKH = diem_cn.MAKH group by LICHSUDIEM.MAKH,khachhang.MAKH,khachhang.HOTEN,khachhang.loaikh,diem_cn.diem,pheduyet,LICHSUDIEM.thang,khachhang.cmnd having khachhang.loaikh=1 and left(Lichsudiem.makh,4)='" + frmMain.cn + "' and khachhang.cmnd ='" + textBox1.Text + "' and convert(date,'01/'+thang)=( select top 1 convert(date,'01/'+thang) as ngay from LICHSUDIEM where LEFT(makh,4)='" + frmMain.cn + "' order by ngay desc)";
                //sCommand = "select khachhang.MAKH,khachhang.HOTEN,diem_cn.diem as diemthangtruoc,sum(lichsudiem.diem) as diem from LICHSUDIEM,KHACHHANG,diem_cn where lichsudiem.MAKH=KHACHHANG.MAKH and left(Lichsudiem.makh,4)='" + frmMain.cn + "' and LICHSUDIEM.MAKH=diem_cn.MAKH and cmnd ='"+textBox1.Text+"' and convert(date,'01/'+thang)=( select top 1 convert(date,'01/'+thang) as ngay from LICHSUDIEM order by ngay desc)group by LICHSUDIEM.MAKH,khachhang.MAKH,khachhang.HOTEN,diem_cn.diem";
                sCommand = "select khachhang.makh,khachhang.hoten,khachhang.diachi1,khachhang.cmnd,diem_cn.diem as diemthangtruoc from diem_cn,khachhang where khachhang.loaikh=1 and khachhang.makh=diem_cn.makh and khachhang.cmnd='" + textBox1.Text + "' and left(khachhang.makh,4)='" + frmMain.cn + "'";
                sCommand = sCommand + " union select khachhang.makh,khachhang.hoten,khachhang.diachi1,khachhang.cmnd,0 as diemthangtruoc from khachhang,lichsudiem where khachhang.loaikh=1 and khachhang.makh=lichsudiem.makh and khachhang.cmnd='" + textBox1.Text + "' and left(khachhang.makh,4)='" + frmMain.cn + "' and lichsudiem.makh not in (select makh from diem_cn where left(makh,4)='" + frmMain.cn + "')";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                frmMain.conn.Close();

                sCommand = "select top 1 convert(date,'01/'+thang)as thang from LICHSUDIEM order by convert(date,'01/'+thang) desc";
                DataTable dttemp = new DataTable();
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dttemp);
                frmMain.conn.Close();

                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm tháng trước", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("Số điểm", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("cmnd", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("diachi", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;


                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        DataTable dt1 = new DataTable();
                        String sCmd = "select sum(diem) as diem, pheduyet,convert(date,'01/'+thang) as thang from lichsudiem group by makh,pheduyet,thang having makh='" + dt.Rows[i]["makh"].ToString() + "' and thang =(select top 1 thang from LICHSUDIEM where makh='" + dt.Rows[i]["makh"].ToString() + "' order by convert(date,'01/'+thang) desc)";
                        dt1.Clear();
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCmd, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hoten"].ToString();
                        if (dt.Rows[i]["diemthangtruoc"].ToString() == "")
                            row[3] = 0;
                        else
                            row[3] = dt.Rows[i]["diemthangtruoc"].ToString();
                        row[4] = dt1.Rows[0]["diem"].ToString();
                        //ngayhl = dt.Rows[i]["ThoiGian"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(6, 4);
                        if (dt1.Rows[0]["pheduyet"].ToString() == "True")
                        {
                            strpheduyet = "True";
                            if (Convert.ToDateTime(dt1.Rows[0]["thang"].ToString()) < Convert.ToDateTime(dttemp.Rows[0]["thang"].ToString()))
                            {
                                row[4] = 0;
                                row[5] = row[3];
                            }
                            else
                            {
                                row[5] = dt.Rows[i]["diemthangtruoc"].ToString();
                                row[3] = Convert.ToDecimal(row[5].ToString()) - Convert.ToDecimal(row[4].ToString());
                            }
                        }
                        else
                            row[5] = Convert.ToDecimal(row[3].ToString()) + Convert.ToDecimal(row[4].ToString());
                        row[6] = dt.Rows[i]["cmnd"].ToString();
                        row[7] = dt.Rows[i]["diachi1"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dgvDanhsach.DataSource = dskh;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[0].Width = 20;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void buttonX3_Click(object sender, EventArgs e)
        {
            layDiemHoTen(); 
        }
        private void layDiemHoTen()
        { 
            if (textBox2.Text == "")
                layDiem();
            else
            {
                DataTable dt = new DataTable();
                String sCommand = "";
                //sCommand = "select khachhang.MAKH,khachhang.HOTEN,diem_cn.diem as diemthangtruoc,sum(lichsudiem.diem) as diem,pheduyet,lichsudiem.thang from LICHSUDIEM inner join KHACHHANG on lichsudiem.MAKH=khachhang.makh left join diem_cn on LICHSUDIEM.MAKH = diem_cn.MAKH group by LICHSUDIEM.MAKH,khachhang.MAKH,khachhang.HOTEN,khachhang.loaikh,diem_cn.diem,pheduyet,LICHSUDIEM.thang,khachhang.cmnd having khachhang.loaikh=1 and left(Lichsudiem.makh,4)='" + frmMain.cn + "' and khachhang.cmnd ='" + textBox1.Text + "' and convert(date,'01/'+thang)=( select top 1 convert(date,'01/'+thang) as ngay from LICHSUDIEM where LEFT(makh,4)='" + frmMain.cn + "' order by ngay desc)";
                //sCommand = "select khachhang.MAKH,khachhang.HOTEN,diem_cn.diem as diemthangtruoc,sum(lichsudiem.diem) as diem from LICHSUDIEM,KHACHHANG,diem_cn where lichsudiem.MAKH=KHACHHANG.MAKH and left(Lichsudiem.makh,4)='" + frmMain.cn + "' and LICHSUDIEM.MAKH=diem_cn.MAKH and cmnd ='"+textBox1.Text+"' and convert(date,'01/'+thang)=( select top 1 convert(date,'01/'+thang) as ngay from LICHSUDIEM order by ngay desc)group by LICHSUDIEM.MAKH,khachhang.MAKH,khachhang.HOTEN,diem_cn.diem";
                sCommand = "select khachhang.makh,khachhang.hoten,khachhang.diachi1,khachhang.cmnd,diem_cn.diem as diemthangtruoc from diem_cn,khachhang where khachhang.loaikh=1 and khachhang.makh=diem_cn.makh and khachhang.hoten like N'%" + textBox2.Text + "%' and left(khachhang.makh,4)='" + frmMain.cn + "'";
                sCommand = sCommand + " union select khachhang.makh,khachhang.hoten,khachhang.diachi1,khachhang.cmnd,0 as diemthangtruoc from khachhang,lichsudiem where khachhang.loaikh=1 and khachhang.makh=lichsudiem.makh and khachhang.hoten like N'%" + textBox2.Text + "%' and left(khachhang.makh,4)='" + frmMain.cn + "' and lichsudiem.makh not in (select makh from diem_cn where left(makh,4)='" + frmMain.cn + "')";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                frmMain.conn.Close();

                sCommand = "select top 1 convert(date,'01/'+thang)as thang from LICHSUDIEM order by convert(date,'01/'+thang) desc";
                DataTable dttemp = new DataTable();
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dttemp);
                frmMain.conn.Close();

                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm tháng trước", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("Số điểm", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("cmnd", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("diachi", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;


                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        DataTable dt1 = new DataTable();
                        String sCmd = "select sum(diem) as diem, pheduyet,convert(date,'01/'+thang) as thang from lichsudiem group by makh,pheduyet,thang having makh='" + dt.Rows[i]["makh"].ToString() + "' and thang =(select top 1 thang from LICHSUDIEM where makh='" + dt.Rows[i]["makh"].ToString() + "' order by convert(date,'01/'+thang) desc)";
                        dt1.Clear();
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCmd, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hoten"].ToString();
                        if (dt.Rows[i]["diemthangtruoc"].ToString() == "")
                            row[3] = 0;
                        else
                            row[3] = dt.Rows[i]["diemthangtruoc"].ToString();
                        row[4] = dt1.Rows[0]["diem"].ToString();
                        //ngayhl = dt.Rows[i]["ThoiGian"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(6, 4);
                        if (dt1.Rows[0]["pheduyet"].ToString() == "True")
                        {
                            strpheduyet = "True";
                            if (Convert.ToDateTime(dt1.Rows[0]["thang"].ToString()) < Convert.ToDateTime(dttemp.Rows[0]["thang"].ToString()))
                            {
                                row[4] = 0;
                                row[5] = row[3];
                            }
                            else
                            {
                                row[5] = dt.Rows[i]["diemthangtruoc"].ToString();
                                row[3] = Convert.ToDecimal(row[5].ToString()) - Convert.ToDecimal(row[4].ToString());
                            }
                        }
                        else
                            row[5] = Convert.ToDecimal(row[3].ToString()) + Convert.ToDecimal(row[4].ToString());
                        row[6] = dt.Rows[i]["cmnd"].ToString();
                        row[7] = dt.Rows[i]["diachi1"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };

                }
                dgvDanhsach.DataSource = dskh;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[0].Width = 20;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void buttonX7_Click(object sender, EventArgs e)
        {
            layDiemMKH();
        }
        private void layDiemMKH()
        {
            if (textBox4.Text == "")
                layDiem();
            else
            {
                DataTable dt = new DataTable();
                String sCommand = "";
                //sCommand = "select khachhang.MAKH,khachhang.HOTEN,diem_cn.diem as diemthangtruoc,sum(lichsudiem.diem) as diem,pheduyet,lichsudiem.thang from LICHSUDIEM inner join KHACHHANG on lichsudiem.MAKH=khachhang.makh left join diem_cn on LICHSUDIEM.MAKH = diem_cn.MAKH group by LICHSUDIEM.MAKH,khachhang.MAKH,khachhang.HOTEN,khachhang.loaikh,diem_cn.diem,pheduyet,LICHSUDIEM.thang having khachhang.loaikh=1 and left(Lichsudiem.makh,4)='" + frmMain.cn + "' and khachhang.makh ='" + textBox4.Text + "' and convert(date,'01/'+thang)=( select top 1 convert(date,'01/'+thang) as ngay from LICHSUDIEM where LEFT(makh,4)='" + frmMain.cn + "' order by ngay desc)";
                sCommand = "select khachhang.makh,khachhang.hoten,khachhang.diachi1,khachhang.cmnd,diem_cn.diem as diemthangtruoc from diem_cn,khachhang where khachhang.loaikh=1 and khachhang.makh=diem_cn.makh and khachhang.makh='" + textBox4.Text + "'";
                sCommand = sCommand + " union select khachhang.makh,khachhang.hoten,khachhang.diachi1,khachhang.cmnd,0 as diemthangtruoc from khachhang,lichsudiem where khachhang.loaikh=1 and khachhang.makh=lichsudiem.makh and khachhang.makh='" + textBox4.Text + "%' and lichsudiem.makh not in (select makh from diem_cn where left(makh,4)='" + frmMain.cn + "')";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                frmMain.conn.Close();

                sCommand = "select top 1 convert(date,'01/'+thang)as thang from LICHSUDIEM order by convert(date,'01/'+thang) desc";
                DataTable dttemp = new DataTable();
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dttemp);
                frmMain.conn.Close();

                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm tháng trước", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("Số điểm", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("cmnd", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("diachi", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;


                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        DataTable dt1 = new DataTable();
                        String sCmd = "select sum(diem) as diem, pheduyet,convert(date,'01/'+thang) as thang from lichsudiem group by makh,pheduyet,thang having makh='" + dt.Rows[i]["makh"].ToString() + "' and thang =(select top 1 thang from LICHSUDIEM where makh='" + dt.Rows[i]["makh"].ToString() + "' order by convert(date,'01/'+thang) desc)";
                        dt1.Clear();
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCmd, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hoten"].ToString();
                        if (dt.Rows[i]["diemthangtruoc"].ToString() == "")
                            row[3] = 0;
                        else
                            row[3] = dt.Rows[i]["diemthangtruoc"].ToString();
                        row[4] = dt1.Rows[0]["diem"].ToString();
                        //ngayhl = dt.Rows[i]["ThoiGian"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(6, 4);
                        if (dt1.Rows[0]["pheduyet"].ToString() == "True")
                        {
                            strpheduyet = "True";
                            if (Convert.ToDateTime(dt1.Rows[0]["thang"].ToString()) < Convert.ToDateTime(dttemp.Rows[0]["thang"].ToString()))
                            {
                                row[4] = 0;
                                row[5] = row[3];
                            }
                            else
                            {
                                row[5] = dt.Rows[i]["diemthangtruoc"].ToString();
                                row[3] = Convert.ToDecimal(row[5].ToString()) - Convert.ToDecimal(row[4].ToString());
                            }
                        }
                        else
                            row[5] = Convert.ToDecimal(row[3].ToString()) + Convert.ToDecimal(row[4].ToString());
                        row[6] = dt.Rows[i]["cmnd"].ToString();
                        row[7] = dt.Rows[i]["diachi1"].ToString();
                        dskh.Rows.Add(row);

                    }
                    catch { };


                }
                dgvDanhsach.DataSource = dskh;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[0].Width = 20;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            layDiemSDT();
        }
        private void layDiemSDT()
        {
            if (textBox3.Text == "")
                layDiem();
            else
            {
                DataTable dt = new DataTable();
                String sCommand = "";
                //sCommand = "select khachhang.MAKH,khachhang.HOTEN,diem_cn.diem as diemthangtruoc,sum(lichsudiem.diem) as diem,pheduyet,lichsudiem.thang from LICHSUDIEM inner join KHACHHANG on lichsudiem.MAKH=khachhang.makh left join diem_cn on LICHSUDIEM.MAKH = diem_cn.MAKH group by LICHSUDIEM.MAKH,khachhang.MAKH,khachhang.HOTEN,khachhang.loaikh,diem_cn.diem,pheduyet,LICHSUDIEM.thang,khachhang.dienthoai1 having khachhang.loaikh=1 and left(Lichsudiem.makh,4)='" + frmMain.cn + "' and khachhang.dienthoai1 ='" + textBox3.Text + "' and convert(date,'01/'+thang)=( select top 1 convert(date,'01/'+thang) as ngay from LICHSUDIEM where LEFT(makh,4)='" + frmMain.cn + "' order by ngay desc)";
                sCommand = "select khachhang.makh,khachhang.hoten,khachhang.diachi1,khachhang.cmnd,diem_cn.diem as diemthangtruoc from diem_cn,khachhang where khachhang.loaikh=1 and khachhang.makh=diem_cn.makh and khachhang.dienthoai1='" + textBox3.Text + "' and left(khachhang.makh,4)='" + frmMain.cn + "'";
                sCommand = sCommand + " union select khachhang.makh,khachhang.hoten,khachhang.diachi1,khachhang.cmnd,0 as diemthangtruoc from khachhang,lichsudiem where khachhang.loaikh=1 and khachhang.makh=lichsudiem.makh and khachhang.dienthoai1='" + textBox3.Text + "' and left(khachhang.makh,4)='" + frmMain.cn + "' and lichsudiem.makh not in (select makh from diem_cn where left(makh,4)='" + frmMain.cn + "')";
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
                frmMain.conn.Close();

                sCommand = "select top 1 convert(date,'01/'+thang)as thang from LICHSUDIEM order by convert(date,'01/'+thang) desc";
                DataTable dttemp = new DataTable();
                if (frmMain.conn.State == ConnectionState.Open)
                {
                    frmMain.conn.Close();
                }
                frmMain.conn.Open();
                new SqlDataAdapter(sCommand, frmMain.conn).Fill(dttemp);
                frmMain.conn.Close();

                System.Data.DataTable dskh = new System.Data.DataTable();
                DataColumn col = null;
                col = new DataColumn("STT", typeof(int));
                dskh.Columns.Add(col);
                col = new DataColumn("Mã", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Họ tên", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("Điểm tháng trước", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("Số điểm", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("Tổng điểm", typeof(decimal));
                dskh.Columns.Add(col);
                col = new DataColumn("cmnd", typeof(string));
                dskh.Columns.Add(col);
                col = new DataColumn("diachi", typeof(string));
                dskh.Columns.Add(col);

                int iRows = dt.Rows.Count;


                for (int i = 0; i < iRows; i++)
                {
                    try
                    {

                        DataRow row = dskh.NewRow();
                        DataTable dt1 = new DataTable();
                        String sCmd = "select sum(diem) as diem, pheduyet,convert(date,'01/'+thang) as thang from lichsudiem group by makh,pheduyet,thang having makh='" + dt.Rows[i]["makh"].ToString() + "' and thang =(select top 1 thang from LICHSUDIEM where makh='" + dt.Rows[i]["makh"].ToString() + "' order by convert(date,'01/'+thang) desc)";
                        dt1.Clear();
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        new SqlDataAdapter(sCmd, frmMain.conn).Fill(dt1);
                        frmMain.conn.Close();
                        row[0] = i + 1;
                        row[1] = dt.Rows[i]["makh"].ToString();
                        row[2] = dt.Rows[i]["hoten"].ToString();
                        if (dt.Rows[i]["diemthangtruoc"].ToString() == "")
                            row[3] = 0;
                        else
                            row[3] = dt.Rows[i]["diemthangtruoc"].ToString();
                        row[4] = dt1.Rows[0]["diem"].ToString();
                        //ngayhl = dt.Rows[i]["ThoiGian"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["Thoigian"].ToString().Substring(6, 4);
                        if (dt1.Rows[0]["pheduyet"].ToString() == "True")
                        {
                            strpheduyet = "True";
                            if (Convert.ToDateTime(dt1.Rows[0]["thang"].ToString()) < Convert.ToDateTime(dttemp.Rows[0]["thang"].ToString()))
                            {
                                row[4] = 0;
                                row[5] = row[3];
                            }
                            else
                            {
                                row[5] = dt.Rows[i]["diemthangtruoc"].ToString();
                                row[3] = Convert.ToDecimal(row[5].ToString()) - Convert.ToDecimal(row[4].ToString());
                            }
                        }
                        else
                            row[5] = Convert.ToDecimal(row[3].ToString()) + Convert.ToDecimal(row[4].ToString());
                        row[6] = dt.Rows[i]["cmnd"].ToString();
                        row[7] = dt.Rows[i]["diachi1"].ToString();
                        dskh.Rows.Add(row);
                    }
                    catch { };


                }
                dgvDanhsach.DataSource = dskh;

                dgvDanhsach.Columns[0].FillWeight = 30;
                dgvDanhsach.Columns[0].Width = 20;
                dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }
        private void dgvDanhsach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void buttonX166_Click(object sender, EventArgs e)
        {
            string sCommand = "";
            DataTable dt = new DataTable();
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dshd = new System.Data.DataTable();
            DataColumn col = null;
            dgvDanhsach.DataSource = dshd;
            col = new DataColumn("STT", typeof(int));
            dshd.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dshd.Columns.Add(col);            
            col = new DataColumn("Số Điện Thoại", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Điểm", typeof(Int64));
            dshd.Columns.Add(col);
            col = new DataColumn("Ngày cập nhật", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dshd.Columns.Add(col);


            if (txt_diem1cn.Text != "" && txt_diem2cn.Text != "")
            {
                sCommand = "SELECT diem_cn.*,diachi1 from DIEM_CN,khachhang where khachhang.makh=diem_cn.makh and diem >= " + txt_diem1cn.Text + " and diem <= " + txt_diem2cn.Text + " " + " ORDER BY 6 DESC ";
               
            }
            else
            {
                if (txt_diem1cn.Text == "" && txt_diem2cn.Text == "")
                {
                    sCommand = "SELECT diem_cn.*,diachi1 from DIEM_CN,khachhang where khachhang.makh=diem_cn.makh and 1=1 " + "  ORDER BY 6 DESC ";
                    
                }
                else
                {
                    if (txt_diem1cn.Text == "")
                    {
                        sCommand = "SELECT diem_cn.*,diachi1 from DIEM_CN,khachhang where khachhang.makh=diem_cn.makh and diem <= " + txt_diem2cn.Text + " " + " ORDER BY 6 DESC ";
                       
                    }
                    else
                    {
                        sCommand = "SELECT diem_cn.*,diachi1 from DIEM_CN ,khachhang where khachhang.makh=diem_cn.makh and diem >= " + txt_diem1cn.Text + " " + " ORDER BY 6 DESC ";
                       
                    }
                }
            }
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            dt.Clear();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dshd.NewRow();
                    row[0] = i + 1;

                    row[1] = dt.Rows[i]["makh"].ToString();

                    row[2] = dt.Rows[i]["tenkh"].ToString();
                    row[3] = dt.Rows[i]["ngaysinh"].ToString();
                    row[4] = dt.Rows[i]["cmt"].ToString();



                    row[5] = dt.Rows[i]["sdt"].ToString();

                    row[6] = dt.Rows[i]["diem"].ToString();
                    row[7] = dt.Rows[i]["ngaycapnhat"].ToString();
                    row[8] = dt.Rows[i]["diachi1"].ToString();

                    dshd.Rows.Add(row);



                }
                catch { };

            }
            dgvDanhsach.DataSource = dshd;

            dgvDanhsach.Columns[0].FillWeight = 30;
            dgvDanhsach.Columns[0].Width = 20;
            dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void buttonX51_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dgvDanhsach.RowCount > 0)
            {
                String temp = "Diem khach hang.xls";
                saveFileDialog1.FileName = temp.Replace("/", "-");
                saveFileDialog1.Filter = " Excel (*.xls)|*.xls|Tất cả (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                string path = "";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = saveFileDialog1.FileName;
                    Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    ExcelApp.Columns.ColumnWidth = 30;
                    for (int i = 0; i < dgvDanhsach.Rows.Count; i++)
                    {
                        DataGridViewRow row = dgvDanhsach.Rows[i];
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
            }
            else
            {
                MessageBox.Show("Không có khách hàng!");
            }

            Cursor.Current = Cursors.Default;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            CRM.frmMain.manhinhin = 30;
            DataTable dt = new DataTable();
            string sCommand = "";
            sCommand="select * from hethong where macn='"+CRM.frmMain.cn+"'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            dt.Clear();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            st_table = new DataTable();
            st_table.Columns.Add("hoten", Type.GetType("System.String"));
            st_table.Columns.Add("cmnd", Type.GetType("System.String"));
            st_table.Columns.Add("diachi", Type.GetType("System.String"));
           // st_table.Columns.Add("sdt", Type.GetType("System.String"));
            st_table.Columns.Add("diem", Type.GetType("System.String"));
            st_table.Columns.Add("sdt", Type.GetType("System.String"));
            st_table.Columns.Add("diachilh", Type.GetType("System.String"));
            st_table.Columns.Add("makh", Type.GetType("System.String"));
            Cursor.Current = Cursors.WaitCursor;
            DataRow r;

            r = st_table.NewRow();

            r["hoten"] = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[2].Value.ToString();
            r["cmnd"] = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[6].Value.ToString();
            r["diachi"] = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[7].Value.ToString();
            r["diem"] = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[5].Value.ToString();
            r["sdt"] = dt.Rows[0]["dt"].ToString();
            r["diachilh"] = dt.Rows[0]["diachi"].ToString();
            r["makh"] = dt.Rows[0]["tencn"].ToString();
            st_table.Rows.Add(r);

            if (dgvDanhsach.RowCount > 0)
            {
                @In form_in = new @In();
                form_in.Show();
            }
            else
            {
                MessageBox.Show("Chưa chọn khách hàng!");  
            }
        }

        private void labelX5_Click(object sender, EventArgs e)
        {

        }

        private void dgvDanhsach_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvDanhsach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            str_makh = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[1].Value.ToString();
            if (tabControl1.SelectedTabIndex == 0)
                layDiemCMND();
            if (tabControl1.SelectedTabIndex == 1)
                layDiemHoTen();
            if (tabControl1.SelectedTabIndex == 2)
                layDiemMKH();
            if (tabControl1.SelectedTabIndex == 3)
                layDiemSDT();

           // str_makh = dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[1].Value.ToString();
            if (strpheduyet == "True")
                tongdiem = Convert.ToDecimal(dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[5].Value.ToString());
            else
                tongdiem = Convert.ToDecimal(dgvDanhsach.Rows[dgvDanhsach.CurrentRow.Index].Cells[3].Value.ToString());
            CRM.frmCSKH_ChiTiet frmCSKH_CT = new frmCSKH_ChiTiet();
            frmCSKH_CT.ShowDialog();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            string sCommand = "";
            DataTable dt = new DataTable();
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataTable dshd = new System.Data.DataTable();
            DataColumn col = null;
            dgvDanhsach.DataSource = dshd;
            col = new DataColumn("STT", typeof(int));
            dshd.Columns.Add(col);
            col = new DataColumn("Mã KH", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Ngày Sinh", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("CMND", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Số Điện Thoại", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Điểm", typeof(Int64));
            dshd.Columns.Add(col);
            col = new DataColumn("Ngày cập nhật", typeof(string));
            dshd.Columns.Add(col);
            col = new DataColumn("Địa chỉ", typeof(string));
            dshd.Columns.Add(col);


            if (txt_diem1.Text != "" && txt_diem2.Text != "")
            {
                sCommand = "SELECT diem_cn.*,diachi1 from DIEM_CN,khachhang where khachhang.macn='"+frmMain.cn+"' and khachhang.makh=diem_cn.makh and diem >= " + txt_diem1.Text + " and diem <= " + txt_diem2.Text + " " + " ORDER BY 6 DESC ";

            }
            else
            {
                if (txt_diem1.Text == "" && txt_diem2.Text == "")
                {
                    sCommand = "SELECT diem_cn.*,diachi1 from DIEM_CN,khachhang where khachhang.macn='" + frmMain.cn + "' and khachhang.makh=diem_cn.makh and 1=1 " + "  ORDER BY 6 DESC ";

                }
                else
                {
                    if (txt_diem1.Text == "")
                    {
                        sCommand = "SELECT diem_cn.*,diachi1 from DIEM_CN,khachhang where khachhang.macn='" + frmMain.cn + "' and khachhang.makh=diem_cn.makh and diem <= " + txt_diem2.Text + " " + " ORDER BY 6 DESC ";

                    }
                    else
                    {
                        sCommand = "SELECT diem_cn.*,diachi1 from DIEM_CN ,khachhang where khachhang.macn='" + frmMain.cn + "' and khachhang.makh=diem_cn.makh and diem >= " + txt_diem1.Text + " " + " ORDER BY 6 DESC ";

                    }
                }
            }
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            dt.Clear();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();

            int iRows = dt.Rows.Count;

            for (int i = 0; i < iRows; i++)
            {
                try
                {

                    DataRow row = dshd.NewRow();
                    row[0] = i + 1;

                    row[1] = dt.Rows[i]["makh"].ToString();

                    row[2] = dt.Rows[i]["tenkh"].ToString();
                    row[3] = dt.Rows[i]["ngaysinh"].ToString();
                    row[4] = dt.Rows[i]["cmt"].ToString();



                    row[5] = dt.Rows[i]["sdt"].ToString();

                    row[6] = dt.Rows[i]["diem"].ToString();
                    row[7] = dt.Rows[i]["ngaycapnhat"].ToString();
                    row[8] = dt.Rows[i]["diachi1"].ToString();

                    dshd.Rows.Add(row);



                }
                catch { };

            }
            dgvDanhsach.DataSource = dshd;

            dgvDanhsach.Columns[0].FillWeight = 30;
            dgvDanhsach.Columns[0].Width = 20;
            dgvDanhsach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

    }
}