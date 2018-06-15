using CRM.DAL.DV;
using CRM.Entities.DV;
using CRM.Utilities.DV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRM.GUI_DV
{
    public partial class frmGiayUyQuyen : Form
    {
        NguoiDaiDien[] dsNguoiDaiDien;
        public frmGiayUyQuyen()
        {
            InitializeComponent();
        }

        private void frmGiayUyQuyen_Load(object sender, EventArgs e)
        {
            dsNguoiDaiDien = PhatHanhTheGhiNoDAL.DanhSachNguoiDaiDien(Thong_tin_dang_nhap.ma_pb);

            if (dsNguoiDaiDien != null)
            {
                //sap xep dsNguoiDaiDien
                for (int i = 0; i < dsNguoiDaiDien.Length; i++)
                {
                    var temp = dsNguoiDaiDien[0];
                    if (dsNguoiDaiDien[i].chucVu == "Giám đốc")
                    {
                        dsNguoiDaiDien[0] = dsNguoiDaiDien[i];
                        dsNguoiDaiDien[i] = temp;
                    }
                }

                for (int i = 0; i < dsNguoiDaiDien.Length; i++)
                {
                    cbLanhDao.Items.Add(dsNguoiDaiDien[i].hoTen);
                }
                if (cbLanhDao.Items.Count > 0)
                    cbLanhDao.SelectedIndex = 0;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                PhatHanhTheGhiNoDAL.DV_GIAYUYQUYEN_UPDATE(dsNguoiDaiDien[cbLanhDao.SelectedIndex].maNV, txtGiayUyQuyen.Text);
            }
            catch (Exception ex)
            {
                ErrorMessageDAL.DataAccessError(ex);
            }
            Close();
        }

        private void cbLanhDao_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGiayUyQuyen.Text = dsNguoiDaiDien[cbLanhDao.SelectedIndex].giayUQ;
        }
    }
}
