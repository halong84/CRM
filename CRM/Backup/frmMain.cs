using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using N_MicrosoftExcelClient;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using System.Collections;

namespace CRM
{
    public partial class frmMain : Form
    {
        MicrosoftExcelClient m_ExcelClient = null;
        public static SqlConnection conn;
        public static SqlCommand myCommand;
        public static SqlDataAdapter adapter;
        public static int loaiDM = 0, manhinhin = 0, flagSearch = 0;
        public static string cn = "", line = "", ddimport = "";
        Thread thr_tudong;
        string uid;
        string[] group_list;
        int group_len;
        DataTable dt;
        DataTable dtResult = new DataTable();

        #region Cac thuoc tinh trong chuong trinh
        public string _uid
        {
            set { uid = value; }
            get { return uid; }
        }
        public string[] _group_list
        {
            set { group_list = value; }
            get { return group_list; }
        }
        public SqlConnection _sqlcon
        {
            set { conn = value; }
            get { return conn; }
        }

        #endregion

        bool isContain(string[] array, string s)
        {
            foreach (string t in array)
            {
                if (t == s) return true;
            }
            return false;
        }

        bool isContain(myStruct[] _struct, string s)
        {
            for (int i = 0; i < _struct.Length; i++)
            {
                if (_struct[i].key == s) return true;
            }
            return false;
        }

        void HopTapHop(ref myStruct[] ketqua, myStruct[] struct1, myStruct[] struct2)
        {

            if (struct1 != null)
            {
                myStruct[] _myStruct = new myStruct[struct1.Length + struct2.Length];
                int count = 0;

                //lay cac phan tu tu tap hop A
                for (int i = 0; i < struct1.Length; i++)
                {
                    if (struct1[i].key != null && struct1[i].val != null)
                    {
                        _myStruct[count] = struct1[i];
                        count++;
                    }
                }

                for (int j = 0; j < struct2.Length; j++)
                {
                    string key = struct2[j].key;
                    string val = struct2[j].val;


                    //neu key trong struct 2 khong chua trong struct1
                    if (!isContain(struct1, struct2[j].key))
                    {
                        if (key != null && val != null)
                        {
                            _myStruct[count++] = struct2[j];
                        }
                    }
                }

                //sort lai theo vi tri tren menu
                for (int i = 0; i < _myStruct.Length - 1; i++)
                    for (int j = i + 1; j < _myStruct.Length; j++)
                    {
                        if (_myStruct[i].val != null && _myStruct[i].key != null && _myStruct[j].val != null && _myStruct[j].key != null)
                        {
                            if (_myStruct[i].id > _myStruct[j].id)
                            {
                                myStruct temp = new myStruct();
                                temp = _myStruct[i];
                                _myStruct[i] = _myStruct[j];
                                _myStruct[j] = temp;

                            }
                        }
                    }

                ketqua = _myStruct;
            }
            else
            {
                ketqua = struct2;
            }


        }

        myStruct[] hopTapHop(ref myHashtable ketqua, myHashtable h1, myHashtable h2)
        {
            IDictionaryEnumerator ide1 = h1.GetEnumerator();
            IDictionaryEnumerator ide2 = h2.GetEnumerator();

            //do khi tra ve getEnumerator se theo thu tu tu duoi len
            //nen cho i =h1.Count-1
            int i = h1.Count - 1;
            int count = h1.Count + h2.Count - 1;
            myStruct[] _myStruct = new myStruct[count + 1];


            while (ide1.MoveNext())
            {
                //neu nhu key do chua co thi moi add vao
                //				if(!ketqua.ContainsKey(ide1.Key)) 
                //				{

                _myStruct[count] = new myStruct();
                _myStruct[count].key = (string)ide1.Key;
                _myStruct[count].val = (string)ide1.Value;
                _myStruct[count].id = (int)h1.uu_tien[i];
                count--;
                //						ketqua.Add(ide1.Key,ide1.Value);
                //					    //them vao do uu tien de sort menu
                //					    ketqua.uu_tien.Add(h1.uu_tien[i]);
                //				}
                i--;
            }

            i = h2.Count - 1;


            while (ide2.MoveNext())
            {
                //neu tap hop 1 khong chua 1 phan tu cua tap hop 2 thi add vao hop
                if (!h1.ContainsValue(ide2.Value))
                {

                    _myStruct[count] = new myStruct();
                    _myStruct[count].key = (string)ide2.Key;
                    _myStruct[count].val = (string)ide2.Value;
                    _myStruct[count].id = (int)h2.uu_tien[i];
                    count--;

                    //                   ketqua.Add(ide2.Key,ide2.Value);
                    //				   ketqua.uu_tien.Add(h2.uu_tien[i]);
                }
                i--;
            }
            //bay gio moi sort lai roi dua vao ketqua
            for (i = 0; i < _myStruct.Length - 1; i++)
                for (int j = i + 1; j < _myStruct.Length; j++)
                {
                    if (_myStruct[i].id > _myStruct[j].id && _myStruct[i].key != null)
                    {
                        myStruct temp = new myStruct();
                        temp = _myStruct[i];
                        _myStruct[i] = _myStruct[j];
                        _myStruct[j] = temp;

                    }
                }
            myHashtable myhash = new myHashtable();

            //bat dau gan vao hashtable
            for (i = 0; i < _myStruct.Length; i++)
            {
                if (_myStruct[i].key != null && _myStruct[i].val != null)
                {
                    myhash.Add(_myStruct[i].key, _myStruct[i].val);
                    myhash.uu_tien.Add(_myStruct[i].id);
                }
            }


            ketqua = myhash;
            return _myStruct;
        }

        //ham tra ve day cac menu item cung 1 level
        //vi du nhu : New, Open, Save, Save As cung 1 cap : 1
        myStruct[] getOneLevelMenu(string dieukien, string group_id)
        {

            DataRow[] rows = dt.Select(dieukien, "pos");

            int count = 0;
            myStruct[] _myStruct = new myStruct[rows.Length];

            foreach (DataRow row in rows)
            {
                string[] g = ((string)(row["group_list"])).Split(',');

                //neu trong group list co chua group_id thi moi tra ve menu_id
                if (isContain(g, group_id))
                {
                    try
                    {
                        _myStruct[count] = new myStruct();
                        _myStruct[count].key = (string)row["menu_id"];
                        _myStruct[count].val = (string)row["menu_title"];
                        _myStruct[count].id = (int)row["pos"];
                        //_myStruct[count].name = (string)row["menu_name"];
                        count++;
                    }
                    catch { }

                    //ht.Add((string)row["menu_id"],(string)row["menu_title"]);
                    //int i=(int)row["pos"];
                    //ht.uu_tien.Add(i);
                }
            }
            return _myStruct;
        }

        void LoadData()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            adapter = new SqlDataAdapter("select * from _Menu order by pos asc", conn);
            conn.Close();
            adapter.Fill(dt);
        }

        private void Hienthi(string formName)
        {
            if (formName == "frmDoimatkhau")
            {
                frmDoimatkhau frmMK = new frmDoimatkhau();
                frmMK.ShowDialog();
            }
            else if (formName == "frmHT_Thamso")
            {
                frmHT_Thamso frmThso = new frmHT_Thamso();
                frmThso.ShowDialog();
            }
            else if (formName == "frmTygia")
            {
                frmTygia frmTG = new frmTygia();
                frmTG.ShowDialog();
            }
            else if (formName == "frmHT_DMImport")
            {
                frmHT_DMImport frmHTImport = new frmHT_DMImport();
                frmHTImport.MdiParent = this;
                frmHTImport.Show();
            }
            else if (formName == "frmHT_KCCSKH")
            {
                frmHT_KCCSKH frmHTKC = new frmHT_KCCSKH();
                frmHTKC.MdiParent = this;
                frmHTKC.Show();
            }
            else if (formName == "frmHT_ChuyenDTKH")
            {
                frmHT_ChuyenDTKH frmHTCDTKH = new frmHT_ChuyenDTKH();
                frmHTCDTKH.MdiParent = this;
                frmHTCDTKH.Show();
            }
            else if (formName == "frmGroup")
            {
                frmGroup frmGr = new frmGroup();
                frmGr.MdiParent = this;
                frmGr.Show();
            }
            else if (formName == "frmUser")
            {
                frmUser frmUs = new frmUser();
                frmUs.MdiParent = this;
                frmUs.Show();
            }
            else if (formName == "frmAD_Huypheduyet")
            {
                frmAD_Huypheduyet frmCancel = new frmAD_Huypheduyet();
                frmCancel.MdiParent = this;
                frmCancel.Show();
            }
            else if (formName == "frmMenu")
            {
                frmMenu frmMnu = new frmMenu();
                frmMnu.MdiParent = this;
                frmMnu.Show();
            }
            else if (formName == "frmHT_Tienich")
            {
                frmHT_TienIch frmTI = new frmHT_TienIch();
                frmTI.MdiParent = this;
                frmTI.Show();
            }
            else if (formName == "ht_exit")
            {
                Application.Exit();
            }
            else if (formName == "frmHT_ImportKH")
            {
                frmHT_ImportKH frmKH_Imp = new frmHT_ImportKH();
                frmKH_Imp.MdiParent = this;
                frmKH_Imp.Show();
            }
            else if (formName == "frmDM_Import")
            {
                frmDM_Import frmDM_Imp = new frmDM_Import();
                frmDM_Imp.MdiParent = this;
                frmDM_Imp.Show();
            }
            else if (formName == "frmPhongban")
            {
                frmPhongban frmPB = new frmPhongban();
                frmPB.MdiParent = this;
                frmPB.Show();
            }
            else if (formName == "frmDM_LoaiGD")
            {
                frmDM_LoaiGD frmNV = new frmDM_LoaiGD();
                frmNV.MdiParent = this;
                frmNV.Show();
            }
            else if (formName == "frmNganhang")
            {
                frmNganhang frmNH = new frmNganhang();
                frmNH.MdiParent = this;
                frmNH.Show();
            }
            else if (formName == "frmDanso")
            {
                frmDanso frmDS = new frmDanso();
                frmDS.MdiParent = this;
                frmDS.Show();
            }
            else if (formName == "frmKSKhachHang")
            {
                frmKSKhachHang frmKSKH = new frmKSKhachHang();
                frmKSKH.MdiParent = this;
                frmKSKH.Show();
            }
            else if (formName == "frmKSDTKH")
            {
                frmKSDTKH frmKSDTKH = new frmKSDTKH();
                frmKSDTKH.MdiParent = this;
                frmKSDTKH.Show();
            }
            else if (formName == "frmKHCMS")
            {
                frmKHCMS frmKHPOS = new frmKHCMS();
                frmKHPOS.MdiParent = this;
                frmKHPOS.Show();
            }
            else if (formName == "frmKhachhangHH")
            {
                frmKhachhangHH frmKHHH = new frmKhachhangHH();
                frmKHHH.MdiParent = this;
                frmKHHH.Show();
            }
            else if (formName == "frmKhachhangTN")
            {
                frmKhachhangTN frmKHTN = new frmKhachhangTN();
                frmKHTN.MdiParent = this;
                frmKHTN.Show();
            }
            else if (formName == "frmKHWU")
            {
                frmKHWU frmKH = new frmKHWU();
                frmKH.MdiParent = this;
                frmKH.Show();
            }
            else if (formName == "frmTKKHNH")
            {
                frmTKKHNH frmTKKHNH = new frmTKKHNH();
                frmTKKHNH.MdiParent = this;
                frmTKKHNH.Show();
            }
            else if (formName == "frmKieuHoi")
            {
                frmKieuHoi frmKHNH = new frmKieuHoi();
                frmKHNH.MdiParent = this;
                frmKHNH.Show();
            }
            else if (formName == "frmKH_GiaoDich")
            {
                frmKH_Giaodich frmKHGD = new frmKH_Giaodich();
                frmKHGD.MdiParent = this;
                frmKHGD.Show();
            }
            else if (formName == "frmNhomKH")
            {
                frmNhomKH frmGrKH = new frmNhomKH();
                frmGrKH.MdiParent = this;
                frmGrKH.Show();
            }
            else if (formName == "frmNhomKHTN")
            {
                frmNhomKHTN frmGrKH = new frmNhomKHTN();
                frmGrKH.MdiParent = this;
                frmGrKH.Show();
            }
            else if (formName == "frmKH_NV")
            {
                frmKH_NV frmKH_NV = new frmKH_NV();
                frmKH_NV.MdiParent = this;
                frmKH_NV.Show();
            }
            else if (formName == "Chinhsach")
            {
                Chinhsach frmCS = new Chinhsach();
                frmCS.MdiParent = this;
                frmCS.Show();
            }
            else if (formName == "frmKH_TKKHHH")
            {
                frmKH_TKKHHH frmTKKH = new frmKH_TKKHHH();
                frmTKKH.MdiParent = this;
                frmTKKH.Show();
            }
            else if (formName == "frmTKKHHH_DB")
            {
                frmTKKHHH_DB frmTKKH = new frmTKKHHH_DB();
                frmTKKH.MdiParent = this;
                frmTKKH.Show();
            }
            else if (formName == "frmKH_TKKHHHCN")
            {
                frmKH_TKKHHHCN frmTKKH = new frmKH_TKKHHHCN();
                frmTKKH.MdiParent = this;
                frmTKKH.Show();
            }
            else if (formName == "frmKH_TKKHTN")
            {
                frmKH_TKKHTN frmTKKH = new frmKH_TKKHTN();
                frmTKKH.MdiParent = this;
                frmTKKH.Show();
            }
            else if (formName == "frmKH_TKKHTNCN")
            {
                frmKH_TKKHTNCN frmTKKH = new frmKH_TKKHTNCN();
                frmTKKH.MdiParent = this;
                frmTKKH.Show();
            }
            else if (formName == "frmKH_TKKHTN_HH")
            {
                frmKH_TKKHTN_HH frmTKKH = new frmKH_TKKHTN_HH();
                frmTKKH.MdiParent = this;
                frmTKKH.Show();
            }
            else if (formName == "frmSearch_KHTT")
            {
                frmSearch_KHTT frmKHTT = new frmSearch_KHTT();
                frmKHTT.MdiParent = this;
                frmKHTT.Show();
            }
            else if (formName == "frmHH_TKVIPCT")
            {
                frmHH_TKVIPCT frmTKVIP = new frmHH_TKVIPCT();
                frmTKVIP.MdiParent = this;
                frmTKVIP.Show();
            }
            else if (formName == "frmTK2890")
            {
                frmTK2890 frmTK2890 = new frmTK2890();
                frmTK2890.MdiParent = this;
                frmTK2890.Show();
            }
            else if (formName == "frmTK19892b")
            {
                frmTK19892b frmTK19892b = new frmTK19892b();
                frmTK19892b.MdiParent = this;
                frmTK19892b.Show();
            }
            else if (formName == "frmTKSMS")
            {
                frmTKSMS frmTKSMS = new frmTKSMS();
                frmTKSMS.MdiParent = this;
                frmTKSMS.Show();
            }
            else if (formName == "frmTKThe")
            {
                frmTKThe frmTKThe = new frmTKThe();
                frmTKThe.MdiParent = this;
                frmTKThe.Show();
            }
            else if (formName == "frmTKKHSPDV")
            {
                frmTKKHSPDV frmTKKHSPDV = new frmTKKHSPDV();
                frmTKKHSPDV.MdiParent = this;
                frmTKKHSPDV.Show();
            }
            else if (formName == "frmKH_TKKHTN_NV")
            {
                frmKH_TKKHTN_NV frmTKKH = new frmKH_TKKHTN_NV();
                frmTKKH.MdiParent = this;
                frmTKKH.Show();
            }
            else if (formName == "frmKH_TKGD")
            {
                frmKH_TKGD frmTKKH = new frmKH_TKGD();
                frmTKKH.MdiParent = this;
                frmTKKH.Show();
            }

            else if (formName == "frmTKKHHH_TH")
            {
                frmTKKHHH_TH frmTKKH = new frmTKKHHH_TH();
                frmTKKH.MdiParent = this;
                frmTKKH.Show();
            }
            else if (formName == "frmTKKHTN_TH")
            {
                frmTKKHTN_TH frmTKKH = new frmTKKHTN_TH();
                frmTKKH.MdiParent = this;
                frmTKKH.Show();
            }
            else if (formName == "frmImport_auto")
            {
                frmImport_auto frmImpA = new frmImport_auto();
                frmImpA.MdiParent = this;
                frmImpA.Show();
            }
            else if (formName == "frmImport")
            {
                frmImport frmImp = new frmImport();
                frmImp.MdiParent = this;
                frmImp.Show();
            }
            else if (formName == "frmDM_NhomCT")
            {
                frmDM_NhomCT frmNhomCT = new frmDM_NhomCT();
                frmNhomCT.MdiParent = this;
                frmNhomCT.Show();
            }
            else if (formName == "frmDM_Chitieu")
            {
                frmDM_Chitieu frmCT = new frmDM_Chitieu();
                frmCT.MdiParent = this;
                frmCT.Show();
            }
            else if (formName == "frmDM_Diem")
            {
                frmDM_Diem frmDCT = new frmDM_Diem();
                frmDCT.MdiParent = this;
                frmDCT.Show();
            }
            else if (formName == "frmDM_Tytrong")
            {
                frmDM_Tytrong frmTT = new frmDM_Tytrong();
                frmTT.MdiParent = this;
                frmTT.Show();
            }
            else if (formName == "frmHH_DMThe")
            {
                frmHH_DMThe frmTh = new frmHH_DMThe();
                frmTh.MdiParent = this;
                frmTh.Show();
            }
            else if (formName == "frmDM_XeploaiKH")
            {
                frmDM_XeploaiKH frmXLKH = new frmDM_XeploaiKH();
                frmXLKH.MdiParent = this;
                frmXLKH.Show();
            }
            else if (formName == "frmDM_DiemXL")
            {
                frmDM_DiemXL frmDXLKH = new frmDM_DiemXL();
                frmDXLKH.MdiParent = this;
                frmDXLKH.Show();
            }
            else if (formName == "frmCCDiem")
            {
                frmCCDiem frmCCD = new frmCCDiem();
                frmCCD.MdiParent = this;
                frmCCD.Show();
            }
            else if (formName == "frmCSKH_ChamDiem")
            {
                frmCSKH_ChamDiem frmCSKHCD = new frmCSKH_ChamDiem();
                frmCSKHCD.MdiParent = this;
                frmCSKHCD.Show();
            }
            else if (formName == "frmCSKH_TraCuuDiem")
            {
                frmCSKH_TraCuuDiem frmCSKHTCD = new frmCSKH_TraCuuDiem();
                frmCSKHTCD.MdiParent = this;
                frmCSKHTCD.Show();
            }
            else if (formName == "frmCSKH_TKQuaTang")
            {
                frmCSKH_TKQuaTang frmCSKHTKQT = new frmCSKH_TKQuaTang();
                frmCSKHTKQT.MdiParent = this;
                frmCSKHTKQT.Show();
            }
            else if (formName == "frmCSKH_TKQT")
            {
                frmCSKH_TKQT frmCSKHTKQT1 = new frmCSKH_TKQT();
                frmCSKHTKQT1.MdiParent = this;
                frmCSKHTKQT1.Show();
            }
            else if (formName == "frmCSKH_TKD")
            {
                frmCSKH_TKD frmCSKHTKD = new frmCSKH_TKD();
                frmCSKHTKD.MdiParent = this;
                frmCSKHTKD.Show();
            }
            else if (formName == "frmCSKH_TKDKH")
            {
                frmCSKH_TKDKH frmCSKHTKDKH = new frmCSKH_TKDKH();
                frmCSKHTKDKH.MdiParent = this;
                frmCSKHTKDKH.Show();
            }
            else if (formName == "frmCSKH_PheDuyet")
            {
                frmCSKH_PheDuyet frmCSKHPD = new frmCSKH_PheDuyet();
                frmCSKHPD.MdiParent = this;
                frmCSKHPD.Show();
            }
            else if (formName == "frmCSKH_GiamSat")
            {
                frmCSKH_GiamSat frmCSKHGS = new frmCSKH_GiamSat();
                frmCSKHGS.MdiParent = this;
                frmCSKHGS.Show();
            }
            else if (formName == "frmCCThuong")
            {
                frmCCThuong frmCCT = new frmCCThuong();
                frmCCT.MdiParent = this;
                frmCCT.Show();
            }
            else if (formName == "frmHH_ChamdiemKH")
            {
                frmHH_ChamdiemKH frmCDXLKH = new frmHH_ChamdiemKH();
                frmCDXLKH.MdiParent = this;
                frmCDXLKH.Show();
            }
            else if (formName == "frmHH_XeploaiThang")
            {
                frmHH_XeploaiThang frmXLKHT = new frmHH_XeploaiThang();
                frmXLKHT.MdiParent = this;
                frmXLKHT.Show();
            }
            else if (formName == "frmHH_XeploaiKy")
            {
                frmHH_XeploaiKy frmXLKHK = new frmHH_XeploaiKy();
                frmXLKHK.MdiParent = this;
                frmXLKHK.Show();
            }
            else if (formName == "frmXacnhan")
            {
                frmXacnhan frmXacnhan = new frmXacnhan();
                frmXacnhan.MdiParent = this;
                frmXacnhan.Show();
            }
            else if (formName == "frmPheduyet")
            {
                frmPheduyet frmduyet = new frmPheduyet();
                frmduyet.MdiParent = this;
                frmduyet.Show();
            }
            else if (formName == "frmPDTT")
            {
                frmPDTT frmduyetTT = new frmPDTT();
                frmduyetTT.MdiParent = this;
                frmduyetTT.Show();
            }
            else if (formName == "frmTKXLKH_TH")
            {
                frmTKXLKH_TH frmTKXLKH = new frmTKXLKH_TH();
                frmTKXLKH.MdiParent = this;
                frmTKXLKH.Show();
            }
            else if (formName == "frmTKXLKH_TH_VIP")
            {
                frmTKXLKH_TH_VIP frmTKXLKH = new frmTKXLKH_TH_VIP();
                frmTKXLKH.MdiParent = this;
                frmTKXLKH.Show();
            }
            else if (formName == "frmHH_TKXLKH")
            {
                frmHH_TKXLKH frmTKXLKH = new frmHH_TKXLKH();
                frmTKXLKH.MdiParent = this;
                frmTKXLKH.Show();
            }
            else if (formName == "frmTKXLKH_VIP")
            {
                frmTKXLKH_VIP frmTKXLKH = new frmTKXLKH_VIP();
                frmTKXLKH.MdiParent = this;
                frmTKXLKH.Show();
            }
            else if (formName == "frmTKSDBQ")
            {
                frmTKSDBQ frmTKXLKH = new frmTKSDBQ();
                frmTKXLKH.MdiParent = this;
                frmTKXLKH.Show();
            }
            else if (formName == "capnhat_phanhoi")
            {
                capnhat_phanhoi();
            }
            else if (formName == "frmTT_Ketquathamdo")
            {
                frmTT_Ketquathamdo frmKq = new frmTT_Ketquathamdo();
                frmKq.MdiParent = this;
                frmKq.Show();
            }
            else if (formName == "frmTK_Ketquathamdo")
            {
                frmTK_Ketquathamdo frmTKKq = new frmTK_Ketquathamdo();
                frmTKKq.MdiParent = this;
                frmTKKq.Show();
            }
            else if (formName == "frmTT_KetquathamdoTC")
            {
                frmTT_KetquathamdoTC frmKqtc = new frmTT_KetquathamdoTC();
                frmKqtc.MdiParent = this;
                frmKqtc.Show();
            }
            else if (formName == "frmTK_CLSPDV")
            {
                frmTK_CLSPDV frmTKCL = new frmTK_CLSPDV();
                frmTKCL.MdiParent = this;
                frmTKCL.Show();
            }
            else if (formName == "frmTK_SPDV")
            {
                frmTK_SPDV frmTKSP = new frmTK_SPDV();
                frmTKSP.MdiParent = this;
                frmTKSP.Show();
            }
            else if (formName == "frmTK_YTQT")
            {
                frmTK_YTQT frmTKQT = new frmTK_YTQT();
                frmTKQT.MdiParent = this;
                frmTKQT.Show();
            }
            else if (formName == "frmTKTGSPDV")
            {
                frmTKTGSPDV frmTKTGSPDV = new frmTKTGSPDV();
                frmTKTGSPDV.MdiParent = this;
                frmTKTGSPDV.Show();
            }
            else if (formName == "frmMK_SMS")
            {
                frmMK_SMS frmSMS= new frmMK_SMS();
                frmSMS.MdiParent = this;
                frmSMS.Show();
            }
            else if (formName == "frmMK_Email")
            {
                frmMK_Email frmEmail = new frmMK_Email();
                frmEmail.MdiParent = this;
                frmEmail.Show();
            }
            else if (formName == "frmLichchamsoc")
            {
                frmLichchamsoc frmCS = new frmLichchamsoc();
                frmCS.MdiParent = this;
                frmCS.Show();
            }
            else if (formName == "frmPheduyet_Lichchamsoc")
            {
                frmPheduyet_Lichchamsoc frmPDLCS = new frmPheduyet_Lichchamsoc();
                frmPDLCS.MdiParent = this;
                frmPDLCS.Show();
            }
            else if (formName == "frmTK_KeHoach")
            {
                frmTK_KeHoach frmTKKHCS = new frmTK_KeHoach();
                frmTKKHCS.MdiParent = this;
                frmTKKHCS.Show();
            }
            else if (formName == "frmHH_THKH")
            {
                frmHH_THKH frmTHKH = new frmHH_THKH();
                frmTHKH.MdiParent = this;
                frmTHKH.Show();
            }
            else if (formName == "frmTK_THKH")
            {
                frmTK_THKH frmTKTHKH = new frmTK_THKH();
                frmTKTHKH.MdiParent = this;
                frmTKTHKH.Show();
            }
            else if (formName == "frmBHHDBH")
            {
                frmBHHDBH frmBHHDBH = new frmBHHDBH();
                frmBHHDBH.MdiParent = this;
                frmBHHDBH.Show();
            }
            else if (formName == "frmBHThu")
            {
                frmBHThu frmBH_Thu = new frmBHThu();
                frmBH_Thu.MdiParent = this;
                frmBH_Thu.Show();
            }
            else if (formName == "frmTK_BATD")
            {
                frmTK_BATD frmTKBATD = new frmTK_BATD();
                frmTKBATD.MdiParent = this;
                frmTKBATD.Show();
            }
            else if (formName == "frmPHI_BATD")
            {
                frmPHI_BATD frmP_BATD = new frmPHI_BATD();
                frmP_BATD.MdiParent = this;
                frmP_BATD.Show();
            }
            else if (formName == "frmHH_BATD")
            {
                frmHH_BATD frmH_BATD = new frmHH_BATD();
                frmH_BATD.MdiParent = this;
                frmH_BATD.Show();
            }
        }

        void menu_Click(object sender, System.EventArgs e)
        {
            string strCmd = "";
            strCmd = "Select * from _Menu Where Menu_Title=N'" + ((ToolStripMenuItem)sender).Text + "' order by pos asc ";

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                adapter.SelectCommand = new SqlCommand(strCmd, conn);
                adapter.SelectCommand.ExecuteReader();
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dtResult = ds.Tables[0];                
            
            string formName = dtResult.Rows[0]["Form_Name"].ToString();

            foreach (Form frmChild in this.MdiChildren)
            {
                if (frmChild.Name == formName)
                {
                    frmChild.Activate();
                    if (frmChild.WindowState == FormWindowState.Minimized)
                    {
                        frmChild.ResumeLayout();
                    }
                    return;
                }
            }
            Hienthi(formName);            
        }

        void SetMenu(string dieukien, ref ToolStripMenuItem mi, int level)
        {
            object[] htp = new object[group_len];
            //myHashtable ght=new myHashtable();
            myStruct[] ketqua = null;
            int i = 0;

            //load du lieu doi voi tung group trong quyen cua user
            foreach (string s in group_list)
            {
                myStruct[] _struct = getOneLevelMenu(dieukien, s);
                htp[i] = new object();
                htp[i] = _struct;
                i++;
            }

            //neu user co nhieu hon 2 group
            if (group_len >= 2)
            {
                HopTapHop(ref ketqua, (myStruct[])htp[0], (myStruct[])htp[1]);

                for (i = 2; i < group_len; i++)
                {
                    HopTapHop(ref ketqua, ketqua, (myStruct[])htp[i]);
                }
            }
            else
            {
                HopTapHop(ref ketqua, null, (myStruct[])htp[0]);
            }

            for (i = 0; i < ketqua.Length; i++)
            {
                string key = ketqua[i].key;
                string val = ketqua[i].val;
                string name = ketqua[i].name;
                if (key != null && val != null)
                {
                    ToolStripMenuItem mic = new ToolStripMenuItem(val, null, new System.EventHandler(menu_Click));

                    SetMenu("parent_id ='" + key + "' and menu_id<>parent_id", ref mic, 1);

                    if (level == 0) //neu la menu o bac 0 thi gan vao MainMenu
                    {
                        this.MainMenuStrip.Items.Add(mic);
                        //Menu.MenuItems.Add(mic);
                    }
                    else        //neu la menu o bac thu > 0 thi gan vao mic
                    {
                        //mi.MenuItems.Add(mic);
                        if (val != "-")
                        {
                            mi.DropDownItems.Add(mic);
                        }
                        else
                        {
                            mi.DropDownItems.Add("-");
                        }
                    }
                }
            }

        }               

        public frmMain()
        {
            InitializeComponent();
            dt = new DataTable();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = "CRM - phien ban " + frmDangnhap.phienban + " [" + frmDangnhap.macn + " - " + frmDangnhap.UserID + "]";
            try
            {
                StreamReader reader = new StreamReader("database.txt");
                line = reader.ReadLine();
                reader.Close();
            }
            catch
            {
                MessageBox.Show("Không đọc được file database.txt");
                this.Close();
            }

            try
            {
                conn = new SqlConnection("user id=sa;" +
                                          "password=qaz@123;server=" + line + ";" +
                                          "Trusted_Connection=no;" +
                                          "database=CRM; " +
                                          "connection timeout=30");

                conn.Open();
                DataTable dt = new DataTable();
                String strCmd = "SELECT * from HeThong Where MaCN='" + frmDangnhap.macn + "'";
                new SqlDataAdapter(strCmd, conn).Fill(dt);
                int i = dt.Rows.Count;
                if (i != 0)
                {
                    cn = dt.Rows[0]["MACN"].ToString();
                    ddimport = dt.Rows[0]["DDImport"].ToString();
                }
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Không kết nối được máy chủ.");
                this.Close();
            }

            group_len = group_list.Length;
            LoadData();
            
            //Menu = new MainMenu();
            this.MainMenuStrip = new MenuStrip();
            ToolStripMenuItem mi = new ToolStripMenuItem();
            SetMenu("menu_id=parent_id", ref mi, 0);

            this.MainMenuStrip.Items.Add(mi);
            Controls.Add(this.MainMenuStrip);
            this.MainMenuStrip.Font = new System.Drawing.Font("Arial", 12F);
            //Chay cac ham tu dong
            thr_tudong = new Thread(tudong);
            thr_tudong.Start();
            timer1.Enabled = true;
            timer1.Start();
            //sau 1h chay lai
            timer1.Interval = 360000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            //thr_tudong.IsBackground= true;

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(3000);
            Cursor.Current = Cursors.Default;
        }
                
        private DataTable read_excel(String file_excel)
        {
            Excel.Application ExcelObj = new Excel.Application();

            Excel.Workbook theWorkbook = null;



            theWorkbook = ExcelObj.Workbooks.Open(file_excel, Missing.Value, Missing.Value, Missing.Value
                                                  , Missing.Value, Missing.Value, Missing.Value, Missing.Value
                                                 , Missing.Value, Missing.Value, Missing.Value, Missing.Value
                                                , Missing.Value, Missing.Value, Missing.Value);
            Excel.Sheets sheets = theWorkbook.Worksheets;

            Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);//Get the reference of second worksheet

            //MessageBox.Show(worksheet.Name);//Get the name of worksheet.
            this.m_ExcelClient = new MicrosoftExcelClient(file_excel);



            //Reset & Reopen Connection
            this.m_ExcelClient.openConnection();

            //Update the message window
            //this.updateMessageWindow(1);

            DataTable result = this.m_ExcelClient.readEntireSheet(worksheet.Name);
            this.m_ExcelClient.closeConnection();

            //ExcelObj.Quit();

            return result;
        }
                
        private void capnhat_phanhoi()
        {
            OleDbConnection myConnection1;
            String strConnect;
            //OleDbCommand myCommand1;
            strConnect = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=G:\\DIEMWEB\\CRMWEB.mdb";
            myConnection1 = new OleDbConnection(strConnect);
            try
            {
                myConnection1.Open();
                myConnection1.Close();


                Cursor.Current = Cursors.WaitCursor;
                //Cap nhat du lieu bang khachhangphanhoi
                DataTable dt = new DataTable();
                String sCommand = "SELECT * from Khachhangphanhoi";
                myConnection1.Open();
                new OleDbDataAdapter(sCommand, myConnection1).Fill(dt);
                myConnection1.Close();
                String qyery_temp = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        qyery_temp = "INSERT INTO khachhangphanhoi(MAKH,CMND,HOTEN,EMAIL,DIENTHOAI,DIACHI,GIOITINH,TUOI,TRINHDO,THUNHAP) Values ('" + dt.Rows[i]["MAKH"].ToString() + "','" + dt.Rows[i]["CMND"].ToString() + "',N'" + dt.Rows[i]["TEN"].ToString() + "','" + dt.Rows[i]["EMAIL"].ToString() + "','" + dt.Rows[i]["DIENTHOAI"].ToString() + "',N'" + dt.Rows[i]["DIACHI"].ToString() + "'," + Convert.ToInt16(dt.Rows[i]["GIOITINH"].ToString()) + ",N'" + dt.Rows[i]["TUOI"].ToString() + "',N'" + dt.Rows[i]["TRINHDO"].ToString() + "',N'" + dt.Rows[i]["THUNHAP"].ToString() + "')";
                        conn.Open();
                        myCommand = new SqlCommand(qyery_temp, conn);
                        myCommand.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }

                }
                //Cap nhat du lieu bang ketquathamdo
                dt.Clear();
                sCommand = "select * from ketquathamdo";
                myConnection1.Open();
                new OleDbDataAdapter(sCommand, myConnection1).Fill(dt);
                myConnection1.Close();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        String tgian = "";
                        tgian = dt.Rows[i]["THOIGIAN"].ToString().Substring(3, 2) + "/" + dt.Rows[i]["THOIGIAN"].ToString().Substring(0, 2) + "/" + dt.Rows[i]["THOIGIAN"].ToString().Substring(6, 4);
                        qyery_temp = "INSERT INTO ketquathamdo(SOPHIEU,MAKH,GDVIEN,THOIGIAN,HINHTHUC,KHACHHANG,GHICHU) Values ('" + dt.Rows[i]["SOPHIEU"].ToString() + "','" + dt.Rows[i]["MAKH"].ToString() + "',N'" + dt.Rows[i]["GDVIEN"].ToString() + "','" + tgian + "'," + Convert.ToInt16(dt.Rows[i]["HINHTHUC"].ToString()) + ",1,N'" + dt.Rows[i]["GHICHU"].ToString() + "')";
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        conn.Open();
                        myCommand = new SqlCommand(qyery_temp, conn);
                        myCommand.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }

                }
                //Cap nhat du lieu bang ketquathamdochitiet
                dt.Clear();
                sCommand = "select * from ketquathamdoct";
                myConnection1.Open();
                new OleDbDataAdapter(sCommand, myConnection1).Fill(dt);
                myConnection1.Close();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        qyery_temp = "INSERT INTO ketquathamdoct(SOPHIEU,MACT,LUACHON,GHICHU) Values ('" + dt.Rows[i]["SOPHIEU"].ToString() + "','" + dt.Rows[i]["MACT"].ToString() + "',N'" + dt.Rows[i]["LUACHON"].ToString() + "',N'" + dt.Rows[i]["GHICHU"].ToString() + "')";
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        conn.Open();
                        myCommand = new SqlCommand(qyery_temp, conn);
                        myCommand.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }

                }
                MessageBox.Show("Đã cập nhật xong!");
                Cursor.Current = Cursors.Default;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch
            {
                MessageBox.Show("Không kết nối được dữ liệu");
                //this.Close();
            }
        }

        private void tt_capnhatphanhoi_Click(object sender, EventArgs e)
        {
            capnhat_phanhoi();
        }
        private void xuatsinhnhat()
        {
            SqlDataAdapter ada = new SqlDataAdapter();
            String sCommand = "", ngaycapnhat;
            ngaycapnhat = DateTime.Now.ToString().Substring(3, 2) + "/" + DateTime.Now.ToString().Substring(0, 2) + "/" + DateTime.Now.ToString().Substring(6, 4);
            DataTable dt = new DataTable();
            dt.Clear();
            sCommand = "select * from khachhang where left(CONVERT(VARCHAR(10),ngaysinh,103),5) = '" + DateTime.Now.ToString().Substring(0, 5) + "' and CONVERT(VARCHAR(10),ngaysinh,103)<>'01/01/1990'";
            if (frmMain.conn.State == ConnectionState.Open)
            {
                frmMain.conn.Close();
            }
            frmMain.conn.Open();
            myCommand = new SqlCommand(sCommand, frmMain.conn);
            myCommand.CommandTimeout = 0;
            ada.SelectCommand = myCommand;
            ada.Fill(dt);
            frmMain.conn.Close();
            int iRows = dt.Rows.Count;
            if (iRows > 0)
            {
                //Insert dữ liệu vào bảng sinhnhat_cn
                for (int i = 0; i < iRows; i++)
                {
                    try
                    {
                        sCommand = "insert sinhnhat_cn values('" + dt.Rows[i]["makh"].ToString() + "','" + CRM.frmCSKH_PheDuyet.VietNamese2English(dt.Rows[i]["hoten"].ToString()) + "','" + dt.Rows[i]["dienthoai1"].ToString() + "','" + dt.Rows[i]["ngaysinh"].ToString() + "','" + ngaycapnhat + "',N'" + dt.Rows[i]["diachi1"].ToString() + "','F','')";
                        if (frmMain.conn.State == ConnectionState.Open)
                        {
                            frmMain.conn.Close();
                        }
                        frmMain.conn.Open();
                        frmMain.myCommand = new SqlCommand(sCommand, frmMain.conn);
                        frmMain.myCommand.ExecuteNonQuery();
                        frmMain.conn.Close();
                    }
                    catch { }
                }
            }
        }

        private void tudong()
        {
            //Thread.Sleep(1000);
            if (cn == "4800")
                xuatsinhnhat();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dtpTudong.Value = DateTime.Now;
            tudong();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            thr_tudong.Abort();
        }
    }
}
