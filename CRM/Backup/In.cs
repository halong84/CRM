using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;  

namespace CRM
{
    public partial class In : Form
    {
        public In()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            //connect
            crConnectionInfo.ServerName = frmMain.line;
            crConnectionInfo.DatabaseName = "CRM";
            crConnectionInfo.UserID = "sa";
            crConnectionInfo.Password = "qaz@123";
            Tables CrTables;
            //In thong ke khach hang hien huu theo tinh, huyen, xa
            if (CRM.frmMain.manhinhin == 1)
            {
                rptTKKHHH rptTKKH = new rptTKKHHH();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@loaikh", CRM.frmKH_TKKHHH.loaikh);
                rptTKKH.SetParameterValue("@tinh", CRM.frmKH_TKKHHH.tinh);
                rptTKKH.SetParameterValue("@huyen", CRM.frmKH_TKKHHH.huyen);
                rptTKKH.SetParameterValue("@xa", CRM.frmKH_TKKHHH.xa);
                rptTKKH.SetParameterValue("@tungay", CRM.frmKH_TKKHHH.tungay);
                rptTKKH.SetParameterValue("@tuthang", CRM.frmKH_TKKHHH.tuthang);
                rptTKKH.SetParameterValue("@denngay", CRM.frmKH_TKKHHH.denngay);
                rptTKKH.SetParameterValue("@denthang", CRM.frmKH_TKKHHH.denthang);
                rptTKKH.SetParameterValue("@cn", CRM.frmKH_TKKHHH.cn);
                rptTKKH.SetParameterValue("@nhomDT", CRM.frmKH_TKKHHH.nhomDT);
                rptTKKH.SetParameterValue("@doituongKH", CRM.frmKH_TKKHHH.doituongKH);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke tong hop xep loai khach hang
            if (CRM.frmMain.manhinhin == 2)
            {
                rptTKXLKH_TH rptTKXLKH = new rptTKXLKH_TH();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKXLKH;
                CrTables = rptTKXLKH.Database.Tables;
                rptTKXLKH.SetParameterValue("@tungay", CRM.frmTKXLKH_TH.tungay);
                rptTKXLKH.SetParameterValue("@denngay", CRM.frmTKXLKH_TH.denngay);
                rptTKXLKH.SetParameterValue("@cn", CRM.frmTKXLKH_TH.cn);
                
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke chi tiet xep loai khach hang
            if (CRM.frmMain.manhinhin == 3)
            {
                rptTKXLKH rptTKXLKH = new rptTKXLKH();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKXLKH;
                CrTables = rptTKXLKH.Database.Tables;
                rptTKXLKH.SetParameterValue("@tungay", CRM.frmHH_TKXLKH.tungay);
                rptTKXLKH.SetParameterValue("@denngay", CRM.frmHH_TKXLKH.denngay);
                rptTKXLKH.SetParameterValue("@loaikh", CRM.frmHH_TKXLKH.loaikh);
                rptTKXLKH.SetParameterValue("@dinhtinh", CRM.frmHH_TKXLKH.dinhtinh);
                rptTKXLKH.SetParameterValue("@pheduyet", CRM.frmHH_TKXLKH.pheduyet);
                rptTKXLKH.SetParameterValue("@xeploai", CRM.frmHH_TKXLKH.xeploai);
                rptTKXLKH.SetParameterValue("@cn", CRM.frmHH_TKXLKH.cn);
                
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke khach hang tiem nang theo tinh, huyen, xa
            if (CRM.frmMain.manhinhin == 4)
            {
                rptTKKHTN rptTKKH = new rptTKKHTN();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@loaikh", CRM.frmKH_TKKHTN.loaikh);
                rptTKKH.SetParameterValue("@tinh", CRM.frmKH_TKKHTN.tinh);
                rptTKKH.SetParameterValue("@huyen", CRM.frmKH_TKKHTN.huyen);
                rptTKKH.SetParameterValue("@xa", CRM.frmKH_TKKHTN.xa);
                rptTKKH.SetParameterValue("@tungay", CRM.frmKH_TKKHTN.tungay);
                rptTKKH.SetParameterValue("@tuthang", CRM.frmKH_TKKHTN.tuthang);
                rptTKKH.SetParameterValue("@denngay", CRM.frmKH_TKKHTN.denngay);
                rptTKKH.SetParameterValue("@denthang", CRM.frmKH_TKKHTN.denthang);
                rptTKKH.SetParameterValue("@cn", CRM.frmKH_TKKHTN.cn);
                rptTKKH.SetParameterValue("@nhomDT", CRM.frmKH_TKKHTN.nhomDT);
                rptTKKH.SetParameterValue("@doituongKH", CRM.frmKH_TKKHTN.doituongKH);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //Danh sach chi nhanh da thuc hien xep loai khach hang
            if (CRM.frmMain.manhinhin == 5)
            {
                rptDSCN rptTKXLKH = new rptDSCN();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKXLKH;
                CrTables = rptTKXLKH.Database.Tables;
                rptTKXLKH.SetParameterValue("@tungay", CRM.frmTKXLKH_TH.tungay);
                rptTKXLKH.SetParameterValue("@denngay", CRM.frmTKXLKH_TH.denngay);
                rptTKXLKH.SetParameterValue("@cn", CRM.frmTKXLKH_TH.cn);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke khach hang tiem nang da chuyen thanh hien huu theo tinh, huyen, xa
            if (CRM.frmMain.manhinhin == 6)
            {
                rptTKKHTN_HH rptTKKH = new rptTKKHTN_HH();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@loaikh", CRM.frmKH_TKKHTN_HH.loaikh);
                rptTKKH.SetParameterValue("@tinh", CRM.frmKH_TKKHTN_HH.tinh);
                rptTKKH.SetParameterValue("@huyen", CRM.frmKH_TKKHTN_HH.huyen);
                rptTKKH.SetParameterValue("@xa", CRM.frmKH_TKKHTN_HH.xa);
                rptTKKH.SetParameterValue("@cn", CRM.frmKH_TKKHTN_HH.cn);
                rptTKKH.SetParameterValue("@tungay", CRM.frmKH_TKKHTN_HH.tungay);
                rptTKKH.SetParameterValue("@tuthang", CRM.frmKH_TKKHTN_HH.tuthang);
                rptTKKH.SetParameterValue("@denngay", CRM.frmKH_TKKHTN_HH.denngay);
                rptTKKH.SetParameterValue("@denthang", CRM.frmKH_TKKHTN_HH.denthang);
                rptTKKH.SetParameterValue("@nhomDT", CRM.frmKH_TKKHTN_HH.nhomDT);
                rptTKKH.SetParameterValue("@doituongKH", CRM.frmKH_TKKHTN_HH.doituongKH);
                
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke khach hang tiem nang da duoc phan cong theo doi theo tinh, huyen, xa
            if (CRM.frmMain.manhinhin == 7)
            {
                rptTKKHTN_NV rptTKKH = new rptTKKHTN_NV();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                //rptTKKH.SetParameterValue("@loaikh", CRM.frmKH_TKKHTN_NV.loaikh);
                rptTKKH.SetParameterValue("@tinh", CRM.frmKH_TKKHTN_NV.tinh);
                rptTKKH.SetParameterValue("@huyen", CRM.frmKH_TKKHTN_NV.huyen);
                rptTKKH.SetParameterValue("@xa", CRM.frmKH_TKKHTN_NV.xa);
                rptTKKH.SetParameterValue("@canbo", CRM.frmKH_TKKHTN_NV.canbo);
                rptTKKH.SetParameterValue("@cn", CRM.frmKH_TKKHTN_NV.cn);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke khach hang da phan hoi ve chat luong SPDV
            if (CRM.frmMain.manhinhin == 8)
            {
                rptTKCLSPDV rptTKKH = new rptTKCLSPDV();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@tungay", CRM.frmTK_CLSPDV.tuthang);
                rptTKKH.SetParameterValue("@denngay", CRM.frmTK_CLSPDV.denthang);
                rptTKKH.SetParameterValue("@maHM", CRM.frmTK_CLSPDV.mahm);
                rptTKKH.SetParameterValue("@chitietHM", CRM.frmTK_CLSPDV.chitietHM);
                //rptTKKH.SetParameterValue("@cn", CRM.frmMain.cn);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke khach hang da phan hoi ve SPDV dang su dung
            if (CRM.frmMain.manhinhin == 9)
            {
                rptTKSPDV rptTKKH = new rptTKSPDV();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@tungay", CRM.frmTK_SPDV.tuthang);
                rptTKKH.SetParameterValue("@denngay", CRM.frmTK_SPDV.denthang);
                rptTKKH.SetParameterValue("@luachon", CRM.frmTK_SPDV.luachon);
                rptTKKH.SetParameterValue("@chitietHM", CRM.frmTK_SPDV.chitietHM);
                //rptTKKH.SetParameterValue("@cn", CRM.frmMain.cn);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke khach hang da phan hoi ve YTQT
            if (CRM.frmMain.manhinhin == 10)
            {
                rptTKYTQT rptTKKH = new rptTKYTQT();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@tungay", CRM.frmTK_YTQT.tuthang);
                rptTKKH.SetParameterValue("@denngay", CRM.frmTK_YTQT.denthang);
                rptTKKH.SetParameterValue("@maCT", CRM.frmTK_YTQT.maCT);
                rptTKKH.SetParameterValue("@chitietHM", CRM.frmTK_YTQT.chitietHM);
                //rptTKKH.SetParameterValue("@cn", CRM.frmMain.cn);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke chi tiet hoat dong khach hang hien huu
            if (CRM.frmMain.manhinhin == 11)
            {
                rptTKGDKH rptTKKH = new rptTKGDKH();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@cn", frmKH_TKGD.cn);
                rptTKKH.SetParameterValue("@canbo", frmKH_TKGD.canbo);
                rptTKKH.SetParameterValue("@maKH", frmKH_TKGD.maKH);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke chi tiet nhan vien giao dich voi khach hang hien huu
            if (CRM.frmMain.manhinhin == 12)
            {
                rptTKGD rptTKKH = new rptTKGD();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@cn", frmKH_TKGD.cn);
                rptTKKH.SetParameterValue("@canbo", frmKH_TKGD.canbo);
                rptTKKH.SetParameterValue("@maKH", frmKH_TKGD.maKH);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke chi tiet hoat dong khach hang tiem nang
            if (CRM.frmMain.manhinhin == 13)
            {
                rptTKGDKH_TN rptTKKH = new rptTKGDKH_TN();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@cn", frmKH_TKGD.cn);
                rptTKKH.SetParameterValue("@canbo", frmKH_TKGD.canbo);
                rptTKKH.SetParameterValue("@maKH", frmKH_TKGD.maKH);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke chi tiet nhan vien giao dich voi khach hang tiem nang
            if (CRM.frmMain.manhinhin == 14)
            {
                rptTKGD_TN rptTKKH = new rptTKGD_TN();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@cn", frmKH_TKGD.cn);
                rptTKKH.SetParameterValue("@canbo", frmKH_TKGD.canbo);
                rptTKKH.SetParameterValue("@maKH", frmKH_TKGD.maKH);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }

            //In thong ke thong hop khach hang hien huu
            if (CRM.frmMain.manhinhin == 15)
            {
                rptTKKHHH_TH rptTKKH = new rptTKKHHH_TH();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@cn", frmTKKHHH_TH.cn);
                

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke thong hop khach hang hien huu
            if (CRM.frmMain.manhinhin == 16)
            {
                rptTKKHTN_TH rptTKKH = new rptTKKHTN_TH();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@cn", frmTKKHTN_TH.cn);


                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 17)
            {
                rptCSKH_DKH rptCSKHDKH = new rptCSKH_DKH();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptCSKHDKH;
                CrTables = rptCSKHDKH.Database.Tables;
                rptCSKHDKH.SetParameterValue("@cn", frmCSKH_TKDKH.cn);
                rptCSKHDKH.SetParameterValue("@tudiem", frmCSKH_TKDKH.FDiem);
                rptCSKHDKH.SetParameterValue("@dendiem", frmCSKH_TKDKH.TDiem);
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }

            //In thong ke SDBQ khach hang
            if (CRM.frmMain.manhinhin == 18)
            {
                rptTKSDBQ rptTKKH = new rptTKSDBQ();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@cn", frmTKSDBQ.cn);
                rptTKKH.SetParameterValue("@tuthang", frmTKSDBQ.tuthang);
                rptTKKH.SetParameterValue("@denthang", frmTKSDBQ.denthang);
                //rptTKKH.SetParameterValue("@thang", frmTKSDBQ.thang);
                rptTKKH.SetParameterValue("@loaiKH", frmTKSDBQ.loaikh);
                rptTKKH.SetParameterValue("@tusdbq", frmTKSDBQ.tusdbq);
                rptTKKH.SetParameterValue("@densdbq", frmTKSDBQ.densdbq);
                rptTKKH.SetParameterValue("@maKH", frmTKSDBQ.maKH);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke khach hang nhan tien WU
            if (CRM.frmMain.manhinhin == 19)
            {
                rptTKKHWU rptTKKH = new rptTKKHWU();

                Cursor.Current = Cursors.WaitCursor;
                dsKHWU ds = new dsKHWU();
                ds.Tables.Add(frmKHWU.dtKHWU.Copy());

                rptTKKH.SetDataSource(ds.Tables[1]);

                crystalReportViewer1.ReportSource = rptTKKH;
                rptTKKH.SetParameterValue("cn", frmKHWU.cn);
                rptTKKH.SetParameterValue("tuthang", frmKHWU.tuthang);
                rptTKKH.SetParameterValue("denthang", frmKHWU.denthang);

                crystalReportViewer1.Refresh();
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 20)
            {
                rptTKKHCS rptTKKH_CS = new rptTKKHCS();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH_CS;
                CrTables = rptTKKH_CS.Database.Tables;
                rptTKKH_CS.SetParameterValue("@cn", frmTK_KeHoach.cn);
                rptTKKH_CS.SetParameterValue("@thang", frmTK_KeHoach.thang);
                rptTKKH_CS.SetParameterValue("@xeploai", frmTK_KeHoach.xeploai);
                rptTKKH_CS.SetParameterValue("@sukien", frmTK_KeHoach.sukien);
                rptTKKH_CS.SetParameterValue("@loaikh", frmTK_KeHoach.loaikh);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 21)
            {
                rptTKTHKH rptTKTHKH = new rptTKTHKH();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKTHKH;
                CrTables = rptTKTHKH.Database.Tables;
                rptTKTHKH.SetParameterValue("@cn", frmTK_THKH.cn);
                rptTKTHKH.SetParameterValue("@thang", frmTK_THKH.thang);
                rptTKTHKH.SetParameterValue("@xeploai", frmTK_THKH.xeploai);
                rptTKTHKH.SetParameterValue("@sukien", frmTK_THKH.sukien);
                rptTKTHKH.SetParameterValue("@kehoach", frmTK_THKH.kehoach);
                rptTKTHKH.SetParameterValue("@nhanqua", frmTK_THKH.nhanqua);
                rptTKTHKH.SetParameterValue("@loaikh", frmTK_THKH.loaikh);               

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }

            //In thong ke khach hang hien huu theo tinh, huyen, xa
            if (CRM.frmMain.manhinhin == 22)
            {
                rptTKKHHH_DB rptTKKH = new rptTKKHHH_DB();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKKH;
                CrTables = rptTKKH.Database.Tables;
                rptTKKH.SetParameterValue("@loaikh", frmTKKHHH_DB.loaikh);
                rptTKKH.SetParameterValue("@tinh", frmTKKHHH_DB.tinh);
                rptTKKH.SetParameterValue("@huyen", frmTKKHHH_DB.huyen);
                rptTKKH.SetParameterValue("@xa", frmTKKHHH_DB.xa);
                rptTKKH.SetParameterValue("@tungay", frmTKKHHH_DB.tungay);
                rptTKKH.SetParameterValue("@tuthang", frmTKKHHH_DB.tuthang);
                rptTKKH.SetParameterValue("@denngay", frmTKKHHH_DB.denngay);
                rptTKKH.SetParameterValue("@denthang", frmTKKHHH_DB.denthang);
                rptTKKH.SetParameterValue("@nhomDT", CRM.frmTKKHHH_DB.nhomDT);
                rptTKKH.SetParameterValue("@doituongKH", CRM.frmTKKHHH_DB.doituongKH);
                
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 23)
            {
                rptTK_VIPCT rptTKVIPCT = new rptTK_VIPCT();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKVIPCT;
                CrTables = rptTKVIPCT.Database.Tables;
                rptTKVIPCT.SetParameterValue("@cn", frmHH_TKVIPCT.cn);
                rptTKVIPCT.SetParameterValue("@tuthang", frmHH_TKVIPCT.tungay);
                rptTKVIPCT.SetParameterValue("@denthang", frmHH_TKVIPCT.denngay);                

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 24)
            {
                rptTKDKH_IN rptTKDKHI = new rptTKDKH_IN();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKDKHI;
                CrTables = rptTKDKHI.Database.Tables;
                rptTKDKHI.SetParameterValue("@cn", frmCSKH_TKD.cn);              

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 25)
            {
                rptKHTK rptKHTK_IN = new rptKHTK();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptKHTK_IN;
                CrTables = rptKHTK_IN.Database.Tables;
                rptKHTK_IN.SetParameterValue("@cn", frmKHThamkhao.cn);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 26)
            {
                rpt2890 rptTK2890 = new rpt2890();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTK2890;
                CrTables = rptTK2890.Database.Tables;
                rptTK2890.SetParameterValue("@cn", frmTK2890.cn);
                rptTK2890.SetParameterValue("@thang", frmTK2890.thang);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke tong hop xep loai khach hang (VIP)
            if (CRM.frmMain.manhinhin == 27)
            {
                rptTKXLKH_TH_VIP rptTKXLKH = new rptTKXLKH_TH_VIP();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKXLKH;
                CrTables = rptTKXLKH.Database.Tables;
                rptTKXLKH.SetParameterValue("@tungay", CRM.frmTKXLKH_TH_VIP.tungay);
                rptTKXLKH.SetParameterValue("@denngay", CRM.frmTKXLKH_TH_VIP.denngay);
                rptTKXLKH.SetParameterValue("@cn", CRM.frmTKXLKH_TH_VIP.cn);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke chi tiet xep loai khach hang VIP
            if (CRM.frmMain.manhinhin == 28)
            {
                rptTKXLKH_VIP rptTKXLKH = new rptTKXLKH_VIP();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKXLKH;
                CrTables = rptTKXLKH.Database.Tables;
                rptTKXLKH.SetParameterValue("@tungay", CRM.frmTKXLKH_VIP.tungay);
                rptTKXLKH.SetParameterValue("@denngay", CRM.frmTKXLKH_VIP.denngay);
                rptTKXLKH.SetParameterValue("@loaikh", CRM.frmTKXLKH_VIP.loaikh);
                rptTKXLKH.SetParameterValue("@dinhtinh", CRM.frmTKXLKH_VIP.dinhtinh);
                rptTKXLKH.SetParameterValue("@pheduyet", CRM.frmTKXLKH_VIP.pheduyet);
                rptTKXLKH.SetParameterValue("@cn", CRM.frmTKXLKH_VIP.cn);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //Danh sach chi nhanh da thuc hien xep loai khach hang
            if (CRM.frmMain.manhinhin == 29)
            {
                rptDSCN rptTKXLKH = new rptDSCN();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKXLKH;
                CrTables = rptTKXLKH.Database.Tables;
                rptTKXLKH.SetParameterValue("@tungay", CRM.frmTKXLKH_TH_VIP.tungay);
                rptTKXLKH.SetParameterValue("@denngay", CRM.frmTKXLKH_TH_VIP.denngay);
                rptTKXLKH.SetParameterValue("@cn", CRM.frmTKXLKH_TH_VIP.cn);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In phieu diem cua khach hang
            if (CRM.frmMain.manhinhin == 30)
            {
                DataSet1 ds = new DataSet1();

                ds.Tables.Add(CRM.frmCSKH_TraCuuDiem.st_table);
                rptPhieudiem objRpt = new rptPhieudiem();                

                objRpt.SetDataSource(ds.Tables[1]);
                crystalReportViewer1.ReportSource = objRpt;
                crystalReportViewer1.Refresh();
            }
            //In bao cao thong ke khach hang nhan qua
            if (CRM.frmMain.manhinhin == 31)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (frmCSKH_TKQT.loaibc == "1")
                {
                    rptTKQTTH rptQTTH = new rptTKQTTH();
                    crystalReportViewer1.ReportSource = rptQTTH;
                    CrTables = rptQTTH.Database.Tables;
                    rptQTTH.SetParameterValue("@tungay", CRM.frmCSKH_TKQT.tungay);
                    rptQTTH.SetParameterValue("@denngay", CRM.frmCSKH_TKQT.denngay);
                    rptQTTH.SetParameterValue("@macn", CRM.frmCSKH_TKQT.macn);
                    rptQTTH.SetParameterValue("@loaibc", CRM.frmCSKH_TKQT.loaibc);
                    rptQTTH.SetParameterValue("@quatang", CRM.frmCSKH_TKQT.quatang);
                }
                else
                {
                    rptCSKH_TKQTCT rptTKQTCT = new rptCSKH_TKQTCT();
                    crystalReportViewer1.ReportSource = rptTKQTCT;
                    CrTables = rptTKQTCT.Database.Tables;
                    rptTKQTCT.SetParameterValue("@tungay", CRM.frmCSKH_TKQT.tungay);
                    rptTKQTCT.SetParameterValue("@denngay", CRM.frmCSKH_TKQT.denngay);
                    rptTKQTCT.SetParameterValue("@macn", CRM.frmCSKH_TKQT.macn);
                    rptTKQTCT.SetParameterValue("@loaibc", CRM.frmCSKH_TKQT.loaibc);
                    rptTKQTCT.SetParameterValue("@quatang", CRM.frmCSKH_TKQT.quatang);
                }
                
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke khách hàng chưa sử dụng SMS
            if (CRM.frmMain.manhinhin == 32)
            {
                rptTKSMS rptTKS = new rptTKSMS();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKS;
                CrTables = rptTKS.Database.Tables;
                rptTKS.SetParameterValue("@cn", CRM.frmTKSMS.cn);
                rptTKS.SetParameterValue("@loaikh", CRM.frmTKSMS.loaikh);
                rptTKS.SetParameterValue("@dthoai", CRM.frmTKSMS.dthoai);
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 33)
            {
                rptTKThe rptTKT= new rptTKThe();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTKT;
                CrTables = rptTKT.Database.Tables;
                rptTKT.SetParameterValue("@cn", CRM.frmTKThe.cn);
               
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 34)
            {
                //Thong ke spdv cua khach hang la doanh nghiep
                if (frmTKKHSPDV.loaikh == 2)
                {
                    rptTKKHSPDVDN rptSPDVDN = new rptTKKHSPDVDN();

                    Cursor.Current = Cursors.WaitCursor;
                    crystalReportViewer1.ReportSource = rptSPDVDN;
                    CrTables = rptSPDVDN.Database.Tables;
                    rptSPDVDN.SetParameterValue("@cn", CRM.frmTKKHSPDV.cn);
                    rptSPDVDN.SetParameterValue("@thang", CRM.frmTKKHSPDV.thang);
                    rptSPDVDN.SetParameterValue("@loaikh", CRM.frmTKKHSPDV.loaikh);
                    rptSPDVDN.SetParameterValue("@makh", CRM.frmTKKHSPDV.makh);
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                        CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                    }
                }
                //Khach hang tien vay
                if (frmTKKHSPDV.loaikh == 5)
                {
                    rptTKKHVAYSPDV rptSPDVVAY = new rptTKKHVAYSPDV();

                    Cursor.Current = Cursors.WaitCursor;
                    crystalReportViewer1.ReportSource = rptSPDVVAY;
                    CrTables = rptSPDVVAY.Database.Tables;
                    rptSPDVVAY.SetParameterValue("@cn", CRM.frmTKKHSPDV.cn);
                    rptSPDVVAY.SetParameterValue("@thang", CRM.frmTKKHSPDV.thang);
                    rptSPDVVAY.SetParameterValue("@loaikh", CRM.frmTKKHSPDV.loaikh);
                    rptSPDVVAY.SetParameterValue("@makh", CRM.frmTKKHSPDV.makh);
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                        CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                    }
                }
                //Thong ke spdv cua khach hang la ca nhan hoac nhan vien
                if ((frmTKKHSPDV.loaikh == 1)||(frmTKKHSPDV.loaikh == 3)||(frmTKKHSPDV.loaikh == 4))
                {
                    rptTKKHSPDVCN rptSPDVCN = new rptTKKHSPDVCN();

                    Cursor.Current = Cursors.WaitCursor;
                    crystalReportViewer1.ReportSource = rptSPDVCN;
                    CrTables = rptSPDVCN.Database.Tables;
                    rptSPDVCN.SetParameterValue("@cn", CRM.frmTKKHSPDV.cn);
                    rptSPDVCN.SetParameterValue("@thang", CRM.frmTKKHSPDV.thang);
                    rptSPDVCN.SetParameterValue("@loaikh", CRM.frmTKKHSPDV.loaikh);
                    rptSPDVCN.SetParameterValue("@makh", CRM.frmTKKHSPDV.makh);
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                        CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 35)
            {
                rptTK_DSBATD rptDSBATD = new rptTK_DSBATD();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptDSBATD;
                CrTables = rptDSBATD.Database.Tables;
                rptDSBATD.SetParameterValue("@cn", CRM.frmTK_BATD.chinhanh);
                rptDSBATD.SetParameterValue("@tungay", CRM.frmTK_BATD.tungay);
                rptDSBATD.SetParameterValue("@denngay", CRM.frmTK_BATD.denngay);
                rptDSBATD.SetParameterValue("@lhbh", CRM.frmTK_BATD.lhbh);
                rptDSBATD.SetParameterValue("@cbtd", CRM.frmTK_BATD.cbtd);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 36)
            {
                rptPHI_BATD rptP_BATD = new rptPHI_BATD();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptP_BATD;
                CrTables = rptP_BATD.Database.Tables;
                rptP_BATD.SetParameterValue("@cn", CRM.frmPHI_BATD.chinhanh);
                rptP_BATD.SetParameterValue("@tungay", CRM.frmPHI_BATD.tungay);
                rptP_BATD.SetParameterValue("@denngay", CRM.frmPHI_BATD.denngay);
                rptP_BATD.SetParameterValue("@lhbh", CRM.frmPHI_BATD.lhbh);
                rptP_BATD.SetParameterValue("@makh", CRM.frmPHI_BATD.makh);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            if (CRM.frmMain.manhinhin == 37)
            {
                rptHHBATD rptHH_BATD = new rptHHBATD();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptHH_BATD;
                CrTables = rptHH_BATD.Database.Tables;
                rptHH_BATD.SetParameterValue("@cn", CRM.frmHH_BATD.chinhanh);
                rptHH_BATD.SetParameterValue("@tungay", CRM.frmHH_BATD.tungay);
                rptHH_BATD.SetParameterValue("@denngay", CRM.frmHH_BATD.denngay);
                rptHH_BATD.SetParameterValue("@lhbh", CRM.frmHH_BATD.lhbh);
                rptHH_BATD.SetParameterValue("@cbtd", CRM.frmHH_BATD.cbtd);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In khach hang kieu hoi
            if (CRM.frmMain.manhinhin == 38)
            {
                rptTKKHNH rptTK_KHNH = new rptTKKHNH();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptTK_KHNH;
                CrTables = rptTK_KHNH.Database.Tables;
                rptTK_KHNH.SetParameterValue("@cn", CRM.frmTKKHNH.cn);
                rptTK_KHNH.SetParameterValue("@tuthang", CRM.frmTKKHNH.tungay);
                rptTK_KHNH.SetParameterValue("@denthang", CRM.frmTKKHNH.denngay);
                rptTK_KHNH.SetParameterValue("@loaibc", CRM.frmTKKHNH.loaibc);               

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In lich su diem cua khach hang
            if (CRM.frmMain.manhinhin == 39)
            {
                rptLichSuDiem rptLSD = new rptLichSuDiem();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptLSD;
                CrTables = rptLSD.Database.Tables;
                rptLSD.SetParameterValue("@makh", CRM.frmCSKH_ChiTiet.strmakh);               

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
            //In thong ke nhan qua cua khach hang
            if (CRM.frmMain.manhinhin == 40)
            {
                rptLichSuQT rptLSQT = new rptLichSuQT();

                Cursor.Current = Cursors.WaitCursor;
                crystalReportViewer1.ReportSource = rptLSQT;
                CrTables = rptLSQT.Database.Tables;
                rptLSQT.SetParameterValue("@makh", CRM.frmCSKH_TKQuaTang.strmakh);
                rptLSQT.SetParameterValue("@tungay", CRM.frmCSKH_TKQuaTang.tungay);
                rptLSQT.SetParameterValue("@denngay", CRM.frmCSKH_TKQuaTang.denngay);

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    CrTable.Location = "CRM" + ".dbo." + CrTable.Name;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void In_Load(object sender, EventArgs e)
        {

        }
    }
}