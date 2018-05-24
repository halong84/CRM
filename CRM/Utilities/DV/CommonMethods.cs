using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using Novacode;
using CRM.Properties;
using CRM.DAL.DV;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using ExcelDataReader;

namespace CRM.Utilities.DV
{
    class CommonMethods 
    {
        //private static string server_add = "127.0.0.1";
        private static string server_add = CRM.Utilities.CommonMethod.GetServerAdd();

        //Xóa dữ liệu toàn bộ các textbox
        public static void ClearTextBoxes(Control control, string[] name_of_textbox)
        {
            foreach (Control c in control.Controls)
            {
                if ((c is TextBox) & (!name_of_textbox.Contains(c.Name)))
                {
                    ((TextBox)c).Clear();
                }
                else if (c is GroupBox)
                {
                    foreach (Control c1 in c.Controls)
                        if ((c1 is TextBox) & (!name_of_textbox.Contains(c1.Name)))
                        {
                            ((TextBox)c1).Clear();
                        }
                }
            }
        }

        //Xóa dữ liệu toàn bộ các textbox trong từng groupbox
        public static void ClearTextBoxesInGroupBox(GroupBox grb)
        {
            foreach (Control c in grb.Controls)
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                else if (c is MaskedTextBox)
                {
                    ((MaskedTextBox)c).Clear();
                }
                else if (c is RichTextBox)
                {
                    ((RichTextBox)c).Clear();
                }
                else if (c is GroupBox)
                {
                    foreach (Control c1 in c.Controls)
                        if (c1 is TextBox)
                        {
                            ((TextBox)c1).Clear();
                        }
                        else if (c1 is MaskedTextBox)
                        {
                            ((MaskedTextBox)c1).Clear();
                        }
                        else if (c1 is RichTextBox)
                        {
                            ((RichTextBox)c1).Clear();
                        }
                }
        }

        public static void ClearTextBoxesInTabPage(TabPage tab)
        {
            foreach (Control c in tab.Controls)
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                else if (c is MaskedTextBox)
                {
                    ((MaskedTextBox)c).Clear();
                }
                else if (c is RichTextBox)
                {
                    ((RichTextBox)c).Clear();
                }
                else if (c is GroupBox)
                {
                    foreach (Control c1 in c.Controls)
                        if (c1 is TextBox)
                        {
                            ((TextBox)c1).Clear();
                        }
                        else if (c1 is MaskedTextBox)
                        {
                            ((MaskedTextBox)c1).Clear();
                        }
                        else if (c1 is RichTextBox)
                        {
                            ((RichTextBox)c1).Clear();
                        }
                }
        }

        //Khóa toàn bộ các textbox trong groupbox
        public static void SetReadOnlyToTextBoxesInGroupBox(GroupBox grb)
        {
            foreach (Control c in grb.Controls)
                if (c is TextBox)
                {
                    ((TextBox)c).ReadOnly = true;
                }
                else if (c is MaskedTextBox)
                {
                    ((MaskedTextBox)c).Enabled = false;
                }
        }

        //Khóa toàn bộ các combo box trong groupbox
        public static void DisableAllComboBoxInGroupBox(GroupBox grb)
        {
            foreach (Control c in grb.Controls)
                if (c is ComboBox)
                {
                    ((ComboBox)c).Enabled = false;
                }
        }

        //Kiểm tra dữ liệu nhập vào textbox có phải dạng số hay không

        public static bool KiemTraNhapSo_Int64(string chkNumeric)
        {
            Int64 intOutVal;
            bool isValidNumeric = false;
            isValidNumeric = Int64.TryParse(chkNumeric, out intOutVal);
            return isValidNumeric;
        }

        public static bool KiemTraNhapSo_Int32(string chkNumeric)
        {
            Int32 intOutVal;
            bool isValidNumeric = false;
            isValidNumeric = Int32.TryParse(chkNumeric, out intOutVal);
            return isValidNumeric;

        }

        public static bool KiemTraNhapSo_Decimal(string chkNumeric)
        {
            decimal decOutVal;
            bool isValidDecimal = false;
            isValidDecimal = decimal.TryParse(chkNumeric, out decOutVal);
            return isValidDecimal;
        }

        public static bool KiemTraNhapNgay(string textboxtext)
        {
            DateTime ngay;
            bool isValidDate = false;
            isValidDate = DateTime.TryParseExact(textboxtext, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngay);
            return isValidDate;
        }

        public static string NgayHienTai()
        {
            string NgayHienTai;
            NgayHienTai = DateTime.Now.ToString("dd/MM/yyyy");
            return NgayHienTai;
        }

        //Thêm năm hiện tại vào trước mã hợp đồng vay để tránh trùng hợp giữa các năm
        public static string NhapMaHopDong(string ma_hd_vay)
        {
            DateTime date_now = DateTime.Now;
            return date_now.Year.ToString() + ma_hd_vay;
        }

        //Tách năm hiện tại ở phía trước mã hợp đồng vay
        public static string TachMaHopDong(string ma_hd_vay)
        {
            return ma_hd_vay.Substring(4, ma_hd_vay.Length - 4);
        }

        //Sử dụng Microsoft.Office.Interop.Word để tạo file word từ template
        //public static void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
        //{
        //    object matchCase = true;
        //    object matchWholeWord = true;
        //    object matchWildCards = false;
        //    object matchSoundLike = false;
        //    object nmatchAllForms = false;
        //    object forward = true;
        //    object format = false;
        //    object matchKashida = false;
        //    object matchDiactitics = false;
        //    object matchAlefHamza = false;
        //    object matchControl = false;
        //    object read_only = false;
        //    object visible = true;
        //    object replace = 2;
        //    object wrap = 1;

        //    wordApp.Selection.Find.Execute(ref findText,
        //                ref matchCase, ref matchWholeWord,
        //                ref matchWildCards, ref matchSoundLike,
        //                ref nmatchAllForms, ref forward,
        //                ref wrap, ref format, ref replaceWithText,
        //                ref replace, ref matchKashida,
        //                ref matchDiactitics, ref matchAlefHamza,
        //                ref matchControl);
        //}



        //string pathImage = null;
        //Methode Create the document :
        //private void CreateWordDocument(object filename, object savaAs, object image    )
        //public static void CreateWordDocument(object filename, object saveAs, List<string> list_nguon, List<string> list_dich)
        //{
        //    object missing = Missing.Value;
        //    //string tempPath = null;

        //    Word.Application wordApp = new Word.Application();

        //    Word.Document aDoc = null;

        //    if (File.Exists((string)filename))
        //    {
        //        DateTime today = DateTime.Now;

        //        object readOnly = false; //default
        //        object isVisible = false;

        //        wordApp.Visible = false;

        //        aDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
        //                                    ref missing, ref missing, ref missing,
        //                                    ref missing, ref missing, ref missing,
        //                                    ref missing, ref missing, ref missing,
        //                                    ref missing, ref missing, ref missing, ref missing);

        //        aDoc.Activate();

        //        if (list_nguon.Count == list_dich.Count)
        //        {
        //            for (int i = 0; i < list_nguon.Count; i++)
        //            {
        //                //this.FindAndReplace(wordApp, list_nguon[i], list_dich[i]);
        //                CommonMethods.FindAndReplace(wordApp, list_nguon[i], list_dich[i]);

        //                //Console.WriteLine(list[i]);
        //            }

        //        }
        //        //this.FindAndReplace(wordApp, "<Date>", DateTime.Now.ToShortDateString());

        //        //insert the picture:
        //        //Image img = resizeImage(pathImage, new Size(200, 90));
        //        //tempPath = System.Windows.Forms.Application.StartupPath + "\\Images\\~Temp\\temp.jpg";
        //        //img.Save(tempPath);

        //        Object oMissed = aDoc.Paragraphs[1].Range; //the position you want to insert
        //        Object oLinkToFile = false;  //default
        //        Object oSaveWithDocument = true;//default
        //        //aDoc.InlineShapes.AddPicture(tempPath, ref  oLinkToFile, ref  oSaveWithDocument, ref oMissed);

        //        #region Print Document :
        //        /*object copies = "1";
        //        object pages = "1";
        //        object range = Word.WdPrintOutRange.wdPrintCurrentPage;
        //        object items = Word.WdPrintOutItem.wdPrintDocumentContent;
        //        object pageType = Word.WdPrintOutPages.wdPrintAllPages;
        //        object oTrue = true;
        //        object oFalse = false;

        //        Word.Document document = aDoc;
        //        object nullobj = Missing.Value;
        //        int dialogResult = wordApp.Dialogs[Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint].Show(ref nullobj);
        //        wordApp.Visible = false;
        //        if (dialogResult == 1)
        //        {
        //            document.PrintOut(
        //            ref oTrue, ref oFalse, ref range, ref missing, ref missing, ref missing,
        //            ref items, ref copies, ref pages, ref pageType, ref oFalse, ref oTrue,
        //            ref missing, ref oFalse, ref missing, ref missing, ref missing, ref missing);
        //        }
        //        */
        //        #endregion

        //    }
        //    else
        //    {
        //        MessageBox.Show("file dose not exist.");
        //        return;
        //    }

        //    //Save as: filename
        //    //aDoc.SaveAs2(ref savaAs, ref missing, ref missing, ref missing,
        //    //        ref missing, ref missing, ref missing,
        //    //        ref missing, ref missing, ref missing,
        //    //        ref missing, ref missing, ref missing,
        //    //        ref missing, ref missing, ref missing);

        //    aDoc.SaveAs2(ref saveAs, ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing,
        //            ref missing);

        //    //Close Document:
        //    aDoc.Close(ref missing, ref missing, ref missing);
        //    //File.Delete(tempPath);
        //    MessageBox.Show("File created.");
        //}

        //private void btnTao_file_Click(object sender, EventArgs e)
        //{
        //    saveDoc.Filter = "Word Documents|*.doc";
        //    List<string> nguon = new List<string> { "<ma_hop_dong>", "<ho_ten_kh_chong>", "<nam_sinh_chong>", "<so_cmt_chong>", "<ho_ten_kh_vo>", "<nam_sinh_vo>", "<so_cmt_vo>", "<dia_chi_kh>", "<hktt_kh>" };
        //    List<string> dich = new List<string> { txtMa_hop_dong.Text, txtHo_ten_chong.Text, txtNam_sinh_chong.Text, txtCMT_chong.Text, txtHo_ten_vo.Text, txtNam_sinh_vo.Text, txtCMT_vo.Text, txtDia_chi.Text, txtHKTT.Text };

        //    if (saveDoc.ShowDialog() == DialogResult.OK)
        //    {
        //        //MessageBox.Show(saveDoc.FileName);
        //        string Template_file = @"C:\Word_template\TAI SAN BEN THU 3\QSD DAT\Bien_ban_dinh_gia_TS.doc";
        //        //string savedFile = @"C:\Hop dong tin dung\";
        //        CreateWordDocument(Template_file, saveDoc.FileName, nguon, dich);
        //        //tEnabled(false);
        //        //printDocument1.DocumentName = SaveDoc.FileName;
        //    }
        //

        //Sử dụng docx dll để tạo file word từ template
        public static bool CreateWordDocument(string file_location, string output_location, List<string> list_nguon, List<string> list_dich)
        {
            // Load the document.
            DocX document = null;
            try
            {
                document = DocX.Load(file_location);
                if (list_nguon.Count == list_dich.Count)
                {
                    for (int i = 0; i < list_nguon.Count; i++)
                    {
                        // Replace text in this document.
                        document.ReplaceText(list_nguon[i], list_dich[i]);
                    }
                }
                //Remove empty paragraph
                //document.RemoveParagraphAt
                //document.ReplaceText("^p^p", "^p");
                // Save changes made to this document.
                //document.Save();
                try
                {
                    document.SaveAs(output_location);
                    return true;
                }
                catch
                {
                    MessageBox.Show("Không thể tạo file!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch
            {
                MessageBox.Show("File đang được sử dụng!", "Thông báo", MessageBoxButtons.OK);
            }// Release this document from memory.
            return false;
        }

        public static void CreateWordDocument(string file_location, string output_location, List<string> list_nguon, List<string> list_dich, List<int> empty_para_index)
        {
            // Load the document.
            DocX document = null;
            try
            {
                document = DocX.Load(file_location);
                if (list_nguon.Count == list_dich.Count)
                {
                    for (int i = 0; i < list_nguon.Count; i++)
                    {
                        // Replace text in this document.
                        document.ReplaceText(list_nguon[i], list_dich[i]);
                    }
                }
                //Remove empty paragraph
                for (int j = 0; j < empty_para_index.Count; j++)
                {
                    document.RemoveParagraphAt(empty_para_index[j]);
                }
                
                try
                {
                    document.SaveAs(output_location);
                }
                catch
                {
                    MessageBox.Show("Không thể tạo file!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch
            {
                MessageBox.Show("File đang được sử dụng!", "Thông báo", MessageBoxButtons.OK);
            }
            
        }

        public static void CreateWordDocument2(string file_location, string output_location, List<string> list_nguon1, List<string> list_dich1, List<string> list_nguon2, List<string> list_dich2)
        {
            // Load the document.
            using (DocX document = DocX.Load(file_location))
            {
                if (list_nguon1.Count == list_dich1.Count)
                {
                    for (int i = 0; i < list_nguon1.Count; i++)
                    {
                        // Replace text in this document.
                        document.ReplaceText(list_nguon1[i], list_dich1[i]);
                    }
                }
                if (list_nguon2.Count == list_dich2.Count)
                {
                    for (int i = 0; i < list_nguon2.Count; i++)
                    {
                        // Replace text in this document.
                        document.ReplaceText(list_nguon2[i], list_dich2[i]);
                    }
                }
                // Save changes made to this document.
                //document.Save();
                try
                {
                    document.SaveAs(output_location);
                }
                catch
                {
                    MessageBox.Show("File đang được sử dụng!", "Thông báo", MessageBoxButtons.OK);
                }
            } // Release this document from memory.
        }

        public static bool FileExist(string file_name)
        {
            FileInfo myFile = new FileInfo(@"\\" + server_add + @"\Word_template\" + file_name);
            bool exists = myFile.Exists;
            return exists;
        }

       
        public static string TemplateFileLocation(string file_location)
        {
            //var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            //var thu_muc_goc = Path.Combine(outPutDirectory, "Word_template\\");
            //string thu_muc_goc = @"C:\Word_template\";
            //string thu_muc_goc = Path.GetDirectoryName(Application.ExecutablePath)+@"\Word_template\";
            //string thu_muc_goc = Path.GetDirectoryName(Application.ExecutablePath) + @"\\127.0.0.1\Word_template\";
            string thu_muc_goc = @"\\" + server_add + @"\Word_template\DV\";
            //string thu_muc_goc = Directory.GetCurrentDirectory() + @"\Word_template\";
            return thu_muc_goc + file_location;
        }

        public static string SaveFileLocation(string fileName)
        {
            string thu_muc_goc = @"C:\TTKH\DV\";
            if(!Directory.Exists(thu_muc_goc))
            {
                Directory.CreateDirectory(thu_muc_goc);
            }
            return thu_muc_goc + fileName;
        }

        public static void CreateSubFolder(string folderPath)
        {
            string thu_muc_goc = @"C:\TTKH\DV\";
            Directory.CreateDirectory(thu_muc_goc + folderPath);
        }

        public static bool SubFolderExist(string folder)
        {
            string thu_muc_goc = @"C:\TTKH\DV\";
            return Directory.Exists(thu_muc_goc + folder);
        }

        //Chữ in hoa ký tự đầu tiên của chuỗi
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                return "";
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        //Chuyển số sang chữ
        public static string ChuyenSoSangChu(string number)
        {
            if (string.IsNullOrEmpty(number)) return "";
            string[] strTachPhanSauDauPhay;
            if (number.Contains('.') || number.Contains(','))
            {
                strTachPhanSauDauPhay = number.Split(',', '.');
                return (ChuyenSoSangChu(strTachPhanSauDauPhay[0]) + "phẩy " + ChuyenSoSangChu(strTachPhanSauDauPhay[1]));
            }

            string[] dv = { "", "mươi", "trăm", "nghìn", "triệu", "tỉ" };
            string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string doc;
            int i, j, k, n, len, found, ddv, rd;

            len = number.Length;
            number += "ss";
            doc = "";
            found = 0;
            ddv = 0;
            rd = 0;

            i = 0;
            while (i < len)
            {
                //So chu so o hang dang duyet
                n = (len - i + 2) % 3 + 1;

                //Kiem tra so 0
                found = 0;
                for (j = 0; j < n; j++)
                {
                    if (number[i + j] != '0')
                    {
                        found = 1;
                        break;
                    }
                }

                //Duyet n chu so
                if (found == 1)
                {
                    rd = 1;
                    for (j = 0; j < n; j++)
                    {
                        ddv = 1;
                        switch (number[i + j])
                        {
                            case '0':
                                if (n - j == 3) doc += cs[0] + " ";
                                if (n - j == 2)
                                {
                                    if (number[i + j + 1] != '0') doc += "linh ";
                                    ddv = 0;
                                }
                                break;
                            case '1':
                                if (n - j == 3) doc += cs[1] + " ";
                                if (n - j == 2)
                                {
                                    doc += "mười ";
                                    ddv = 0;
                                }
                                if (n - j == 1)
                                {
                                    if (i + j == 0) k = 0;
                                    else k = i + j - 1;

                                    if (number[k] != '1' && number[k] != '0')
                                        doc += "mốt ";
                                    else
                                        doc += cs[1] + " ";
                                }
                                break;
                            case '5':
                                if ((i + j == len - 1) || (i + j + 3 == len - 1))
                                    doc += "lăm ";
                                else
                                    doc += cs[5] + " ";
                                break;
                            default:
                                doc += cs[(int)number[i + j] - 48] + " ";
                                break;
                        }

                        //Doc don vi nho
                        if (ddv == 1)
                        {
                            doc += ((n - j) != 1) ? dv[n - j - 1] + " " : dv[n - j - 1];
                        }
                    }
                }


                //Doc don vi lon
                if (len - i - n > 0)
                {
                    if ((len - i - n) % 9 == 0)
                    {
                        if (rd == 1)
                            for (k = 0; k < (len - i - n) / 9; k++)
                                doc += "tỉ ";
                        rd = 0;
                    }
                    else
                        if (found != 0) doc += dv[((len - i - n + 1) % 9) / 3 + 2] + " ";
                }

                i += n;
            }

            if (len == 1)
                if (number[0] == '0' || number[0] == '5') return cs[(int)number[0] - 48];

            return doc;
            //return CommonMethods.FirstCharToUpper(doc);
        }

        //Kiểm tra kết nối đến cơ sở dữ liệu
        public static bool IsServerConnected()
        {
            try
            {
                DAL.DataAccess.conn.Open();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",  
    "đ",  
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",  
    "í","ì","ỉ","ĩ","ị",  
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",  
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",  
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",  
    "d",  
    "e","e","e","e","e","e","e","e","e","e","e",  
    "i","i","i","i","i",  
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",  
    "u","u","u","u","u","u","u","u","u","u","u",  
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public static string ThemDauPhay(string source)
        {
            if (string.IsNullOrEmpty(source)) return "";
            string goal = source;
            int index = 0;
            if (source.Length <= 3)
                return source;
            else
            {
                for (int i = 0; i < source.Length; i++)
                    if ((source.Length - i) % 3 == 0 && i != 0)
                    {
                        goal = goal.Insert(i + index, ",");
                        index++;
                    }
                return goal;
            }
        }

        public static DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        public static DataTable read_excel(string excel_path)
        {
            DataTable dt = new DataTable();
            var file = new FileInfo(excel_path);
            if (File.Exists(excel_path))
            {
                using (
                var stream = File.Open(excel_path, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader reader;

                    if (file.Extension.Equals(".xls") || file.Extension.Equals(".XLS"))
                        reader = ExcelDataReader.ExcelReaderFactory.CreateBinaryReader(stream);
                    else if (file.Extension.Equals(".xlsx") || file.Extension.Equals(".XLSX"))
                        reader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(stream);
                    else
                        throw new Exception("Invalid FileName");

                    //// reader.IsFirstRowAsColumnNames
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };

                    var dataSet = reader.AsDataSet(conf);
                    dt = dataSet.Tables[0];
                }
            }
            else dt = null;

            return dt;
        }


    }
}
