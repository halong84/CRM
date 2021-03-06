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
    public partial class frmCSKH_ChamDiem : Form
    {
        LICHSUDIEMBUS lichsudiem_bus = new LICHSUDIEMBUS();
        public frmCSKH_ChamDiem()
        {
            InitializeComponent();
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

        private void btnChamdiem_Click(object sender, EventArgs e)
        {
            DataTable dt_temp2 = new DataTable();
            dt_temp2.Columns.AddRange
            (
                new DataColumn[10] 
                { 
                    new DataColumn("MAKH", typeof(string)),
                    new DataColumn("THANG", typeof(string)),
                    new DataColumn("CCY", typeof(string)),
                    new DataColumn("DIEM", typeof(decimal)),
                    new DataColumn("PHEDUYET", typeof(bool)),
                    new DataColumn("NGAYPHEDUYET", typeof(string)),
                    new DataColumn("NGUOIPHEDUYET", typeof(string)),
                    new DataColumn("NGAYPDTT", typeof(string)),
                    new DataColumn("NGUOIPDTT", typeof(string)),
                    new DataColumn("PDTT", typeof(bool))
                }
            );
            DataRow dr2;

            DataTable dt = new DataTable();
            String ngaytinhdiem, sCommand;
            decimal diemvnd = 0, tienvnd = 0;
            ngaytinhdiem = dtpThang.Text.Substring(0, 2) + "/02/" + dtpThang.Text.Substring(3, 4);
            //Kiểm tra có dữ liệu để chấm điểm chưa
            dt.Clear();
            sCommand = "select * from sdbqnt where thang= '" + dtpThang.Text + "' and left(makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Chưa import dữ liệu tháng này!");
                return;
            }
            //Kiem tra thang da cham diem chua
            dt.Clear();
            sCommand = "select * from lichsudiem where THANG= '" + dtpThang.Text + "' and CCY ='" + cbCCY.Text + "'and left(makh,4)='" + Thongtindangnhap.macn + "'";            
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            if (dt.Rows.Count > 0)
            {
                if (MessageBox.Show("Tháng này đã chấm điểm! Chấm lại không? ", "cham diem khach hang ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) 
                {
                    return;
                }
                if (dt.Rows[0]["Pheduyet"].ToString() == "True")
                {
                    MessageBox.Show("Tháng này đã phê duyệt,không chấm lại được!");
                    return;
                }
            }

            //Can cu vao sdbqct tinh diem cho khach hang thang tinh diem luu vao lich su diem            
            dt.Clear();
            sCommand = "select top 1 * from cauhinhdiem where LOAITIEN ='"+cbCCY.Text+"' order by thoigian desc";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            if (dt.Rows.Count > 0)
            {
                diemvnd = Convert.ToDecimal(dt.Rows[0]["sodiem"].ToString());
                tienvnd = Convert.ToDecimal(dt.Rows[0]["sotien"].ToString());
            }
            dt.Clear();
            sCommand = "select sdbqnt.SDBQ, sdbqnt.MAKH from sdbqnt,khachhang where sdbqnt.makh=khachhang.makh and khachhang.loaikh=1 and thang= '" + dtpThang.Text + "' and LOAITIEN ='" + cbCCY.Text + "' and left(sdbqnt.makh,4)='" + Thongtindangnhap.macn + "'";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal sdbq = 0,diem=0;
                sdbq = Convert.ToDecimal(dt.Rows[i]["SDBQ"].ToString());
                sdbq = sdbq - sdbq % tienvnd;
                diem = sdbq * diemvnd / tienvnd;
                if (diem > 0)
                {
                    dr2 = dt_temp2.NewRow();
                    dr2["MAKH"] = dt.Rows[i]["MAKH"].ToString();
                    dr2["THANG"] = dtpThang.Text;
                    dr2["CCY"] = cbCCY.Text;
                    dr2["DIEM"] = diem;
                    dr2["PHEDUYET"] = false;
                    dr2["NGAYPHEDUYET"] = "01/01/1990";
                    dr2["NGUOIPHEDUYET"] = "";
                    dr2["NGAYPDTT"] = "01/01/1990";
                    dr2["NGUOIPDTT"] = "";
                    dr2["PDTT"] = false;
                    dt_temp2.Rows.Add(dr2);

                    //try
                    //{
                    //    sCommand = "INSERT INTO lichsudiem(MAKH,THANG,CCY,DIEM) Values ('" + dt.Rows[i]["MAKH"].ToString() + "','" + dtpThang.Text + "','" + cbCCY.Text + "'," + diem + ")";
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }
                    //    DataAccess.conn.Open();
                    //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //    frmMain.myCommand.ExecuteNonQuery();
                    //    DataAccess.conn.Close();
                    //}
                    //catch
                    //{
                    //    if (DataAccess.conn.State == ConnectionState.Open)
                    //    {
                    //        DataAccess.conn.Close();
                    //    }

                    //    sCommand = "Update lichsudiem set DIEM =" + diem + "where makh='" + dt.Rows[i]["MAKH"].ToString() + "' and thang ='" + dtpThang.Text + "' and CCY='" + cbCCY.Text + "'";
                    //    DataAccess.conn.Open();
                    //    frmMain.myCommand = new SqlCommand(sCommand, DataAccess.conn);
                    //    frmMain.myCommand.ExecuteNonQuery();
                    //    DataAccess.conn.Close();
                    //}
                }
            }

            if (lichsudiem_bus.UPDATE_LICHSUDIEM(dt_temp2))
            {
                //MessageBox.Show("Cập nhật bảng LICHSUDIEM tháng " + dtpThang.Text + " thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật bảng LICHSUDIEM tháng " + dtpThang.Text);
            }

            MessageBox.Show("Hoàn thành!");           

        }

        private void frmCSKH_ChamDiem_Load(object sender, EventArgs e)
        {

        }

    }
}