using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;
namespace CRM
{
    public partial class frmCSKH_TKDKH : Form
    {
        ChinhanhBUS cnbus = new ChinhanhBUS();
        public static decimal FDiem=0,TDiem=0;
        public static String cn="";
        public frmCSKH_TKDKH()
        {
            InitializeComponent();
        }

        private void frmCSKH_TKDKH_Load(object sender, EventArgs e)
        {
            DataTable dt = cnbus.DANH_SACH_MA_CHI_NHANH();
            DataRow first_row = dt.NewRow();
            first_row[0] = "Tất cả";
            dt.Rows.InsertAt(first_row, 0);

            //cbCN.DataSource = dt;
            cbCN.DisplayMember = "MACN";
            cbCN.ValueMember = "MACN";
            cbCN.DataSource = dt;
            cbCN.SelectedValue = Thongtindangnhap.macn;

            if (Thongtindangnhap.macn != Thongtindangnhap.ma_hoi_so)
            {
                //cbCN.Text = Thongtindangnhap.macn;
                cbCN.Enabled = false;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            switch (cbDiem.Text)
            {
                case "<1200":
                {
                    FDiem = 0;
                    TDiem = 1200;
                    break;
                }
                case "1200->2400":
                {
                    FDiem = 1200;
                    TDiem = 2400;
                    break;
                }
                case "2400->4800":
                {
                    FDiem=2400;
                    TDiem=4800;
                    break;
                }
                case "4800->7200":
                {
                    FDiem=4800;
                    TDiem=7200;
                    break;
                }
                case "7200->12000":
                {
                    FDiem = 7200;
                    TDiem = 12000;
                    break;
                }
                case "12000->36000":
                {
                    FDiem = 12000;
                    TDiem = 36000;
                    break;
                }
                case "36000->72000":
                {
                    FDiem = 36000;
                    TDiem = 72000;
                    break;
                }
                case ">72000":
                {
                    FDiem = 72000;
                    TDiem = 999999999;
                    break;
                }
                default:
                {
                    FDiem = 0;
                    TDiem = 999999999;
                    break;
                }
            }
            DataTable dt = new DataTable();
            String sCommand = "";
            //sCommand = "select khachhang.MAKH,khachhang.HOTEN,diem_cn.diem as diemthangtruoc,sum(lichsudiem.diem) as diem from LICHSUDIEM,KHACHHANG,diem_cn where lichsudiem.MAKH=KHACHHANG.MAKH and left(Lichsudiem.makh,4)='"+Thongtindangnhap.macn+"' and LICHSUDIEM.MAKH=diem_cn.MAKH and convert(date,'01/'+thang)=( select top 1 convert(date,'01/'+thang) as ngay from LICHSUDIEM order by ngay desc)group by LICHSUDIEM.MAKH,khachhang.MAKH,khachhang.HOTEN,diem_cn.diem";
            if((cbCN.Text=="")||(cbCN.Text=="Tất cả"))
                sCommand = "select khachhang.MAKH,khachhang.HOTEN,diem_cn.diem,diem_cn.ngaycapnhat from KHACHHANG,diem_cn where khachhang.loaikh=1 and khachhang.MAKH = diem_cn.MAKH and diem>=" + FDiem + " and diem<" + TDiem + " order by ma,diem desc";                
            else
                sCommand = "select khachhang.MAKH,khachhang.HOTEN,diem_cn.diem,diem_cn.ngaycapnhat from KHACHHANG,diem_cn where khachhang.loaikh=1 and khachhang.MAKH = diem_cn.MAKH and ma='" + cbCN.Text + "' and diem>=" + FDiem + " and diem<" + TDiem + " order by ma,diem desc";
            if (DataAccess.conn.State == ConnectionState.Open)
            {
                DataAccess.conn.Close();
            }
            DataAccess.conn.Open();
            new SqlDataAdapter(sCommand, DataAccess.conn).Fill(dt);
            DataAccess.conn.Close();

            System.Data.DataTable dskh = new System.Data.DataTable();
            DataColumn col = null;
            col = new DataColumn("STT", typeof(int));
            dskh.Columns.Add(col);
            col = new DataColumn("Mã", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Họ tên", typeof(string));
            dskh.Columns.Add(col);
            col = new DataColumn("Số điểm", typeof(decimal));
            dskh.Columns.Add(col);
            col = new DataColumn("Tháng cập nhật", typeof(string));
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
                    row[3] = dt.Rows[i]["diem"].ToString();
                    if (dt.Rows[i]["ngaycapnhat"].ToString().Length == 7)
                        row[4] = dt.Rows[i]["ngaycapnhat"].ToString();
                    else
                        row[4] = dt.Rows[i]["ngaycapnhat"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["ngaycapnhat"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["ngaycapnhat"].ToString().Substring(6, 4); 
                    dskh.Rows.Add(row);
                }
                catch { };

            }
            dgvDanhsach.DataSource = dskh;
            dgvDanhsach.Columns[0].FillWeight = 30;
            dgvDanhsach.Columns[0].Width = 20;            
            dgvDanhsach.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            
            if((cbCN.Text=="")||(cbCN.Text=="Tất cả"))
                cn="9999";
            else
                cn = cbCN.Text;
            CRM.frmMain.manhinhin = 17;                     
            @In form_in = new @In();
            form_in.Show();

        }

        private void buttonX159_Click(object sender, EventArgs e)
        {
            String temp = "";
            temp = "SoLuongKhachHang.xls";            

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
            Cursor.Current = Cursors.Default;
        }
    }
}