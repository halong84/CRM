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
    public partial class frmCSKH_ChamDiem : Form
    {
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
            DataTable dt = new DataTable();
            String ngaytinhdiem, sCommand;
            decimal diemvnd = 0, tienvnd = 0;
            ngaytinhdiem = dtpThang.Text.Substring(0, 2) + "/02/" + dtpThang.Text.Substring(3, 4);
            //Kiểm tra có dữ liệu để chấm điểm chưa
            dt.Clear();
            sCommand = "select * from sdbqnt where thang= '" + dtpThang.Text + "' and left(makh,4)='" + frmMain.cn + "'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Chưa import dữ liệu tháng này!");
                return;
            }
            //Kiem tra thang da cham diem chua
            dt.Clear();
            sCommand = "select * from lichsudiem where THANG= '" + dtpThang.Text + "' and CCY ='" + cbCCY.Text + "'and left(makh,4)='" + frmMain.cn + "'";            
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
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
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            if (dt.Rows.Count > 0)
            {
                diemvnd = Convert.ToDecimal( dt.Rows[0]["sodiem"].ToString());
                tienvnd = Convert.ToDecimal(dt.Rows[0]["sotien"].ToString());
            }
            dt.Clear();
            sCommand = "select sdbqnt.* from sdbqnt,khachhang where sdbqnt.makh=khachhang.makh and khachhang.loaikh=1 and thang= '" + dtpThang.Text + "' and LOAITIEN ='" + cbCCY.Text + "' and left(sdbqnt.makh,4)='"+frmMain.cn+"'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            new SqlDataAdapter(sCommand, frmMain.conn).Fill(dt);
            frmMain.conn.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal sdbq = 0,diem=0;
                sdbq = Convert.ToDecimal(dt.Rows[i]["SDBQ"].ToString());
                sdbq = sdbq - sdbq % tienvnd;
                diem = sdbq * diemvnd / tienvnd;
                if (diem > 0)
                {
                    try
                    {
                        sCommand = "INSERT INTO lichsudiem(MAKH,THANG,CCY,DIEM) Values ('" + dt.Rows[i]["MAKH"].ToString() + "','" + dtpThang.Text + "','" + cbCCY.Text + "'," + diem + ")";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        frmMain.conn.Close();
                    }
                    catch
                    {
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }

                        sCommand = "Update lichsudiem set DIEM =" + diem + "where makh='" + dt.Rows[i]["MAKH"].ToString() + "' and thang ='" + dtpThang.Text + "' and CCY='" + cbCCY.Text + "'";
                        frmMain.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        frmMain.conn.Close();
                    }
                }
            }
            MessageBox.Show("Đã chấm điểm xong!");           

        }

        private void frmCSKH_ChamDiem_Load(object sender, EventArgs e)
        {

        }

    }
}