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
//using N_MicrosoftExcelClient;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Linq;
using System.Globalization;
using CRM.Entities;
using CRM.DAL;
using CRM.BUS;
using CRM.Utilities;
using Novacode;
using ExcelDataReader;


namespace CRM.Utilities
{
    class CommonMethod
    {
        //Khai báo địa chỉ máy chủ chứa file template
        private static string server_add = "127.0.0.1";
        //private static string server_add = "10.14.0.12";

        //Chữ in hoa ký tự đầu tiên của chuỗi
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
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

        //Kiểm tra string phải là ngày hay không
        public static bool Isdate(string textboxtext)
        {
            DateTime ngay;
            bool isValidDate = false;
            isValidDate = DateTime.TryParseExact(textboxtext, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngay);
            return isValidDate;
        }

        //Kiểm tra có đúng định dạng ngày "dd/MM/yyyy" hay không
        public static bool KiemTraNhapNgay(string textboxtext)
        {
            DateTime ngay;
            bool isValidDate = false;
            isValidDate = DateTime.TryParseExact(textboxtext, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngay);
            return isValidDate;
        }

        //Xóa các dòng có cùng giá trị tại một số cột xác định
        public static void RemoveDuplicatesFromDataTable(ref DataTable table, List<string> keyColumns)
        {

            Dictionary<string, string> uniquenessDict = new Dictionary<string, string>(table.Rows.Count);

            StringBuilder stringBuilder = null;

            int rowIndex = 0;

            DataRow row;

            DataRowCollection rows = table.Rows;

            while (rowIndex < rows.Count)
            {

                row = rows[rowIndex];

                stringBuilder = new StringBuilder();

                foreach (string colname in keyColumns)
                {

                    stringBuilder.Append(((string)row[colname]));

                }

                if (uniquenessDict.ContainsKey(stringBuilder.ToString()))
                {

                    rows.Remove(row);

                }

                else
                {

                    uniquenessDict.Add(stringBuilder.ToString(), string.Empty);

                    rowIndex++;

                }

            }

        }

        //Xóa các dòng có cùng giá tại duy nhất 1 cột xác định
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

        //Kiểm tra xem chuỗi chỉ chứa ký tự số hay không
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        //Lấy đường dẫn file template
        public static string TemplateFileLocation(string file_location)
        {
            //var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            //var thu_muc_goc = Path.Combine(outPutDirectory, "Word_template\\");
            //string thu_muc_goc = @"C:\Word_template\";
            //string thu_muc_goc = Path.GetDirectoryName(Application.ExecutablePath)+@"\Word_template\";
            //string thu_muc_goc = Path.GetDirectoryName(Application.ExecutablePath) + @"\\127.0.0.1\Word_template\";

            string thu_muc_goc = @"\\" + server_add + @"\Word_template\";
            //string thu_muc_goc = @"Word_template\";
            return thu_muc_goc + file_location;
        }
        #region Create word document without table
        //Sử dụng docx dll để tạo file word từ template
        public static void CreateWordDocument(string file_location, string output_location, List<string> list_nguon, List<string> list_dich)
        {
            try
            {
                // Load the document.
                using (DocX document = DocX.Load(file_location))
                {
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
                    document.SaveAs(output_location);
                } // Release this document from memory.
            }
            catch
            {
                MessageBox.Show("File mẫu đang mở, liên hệ bộ phận Điện toán để kiểm tra");
                return;
            }
        }
        #endregion

        #region Create word document with table
        //Tham số
        //1. file_location: đường dẫn file mẫu biểu
        //2. output_location: đường dẫn file xuất ra
        //3. list_nguon: danh sách các vị trí trong file mẫu biểu để thay thế
        //4. list_dich: danh sách các từ/cụm từ thay thế vào list_nguon
        //5. data: bảng dữ liệu cần chèn vào mẫu biểu
        //6. list_title: danh sách tiêu đề bảng
        //7. font_family: kiểu font trong bảng
        //8. font_size: kích cỡ font trong bảng
        //9. last_row_bold: có in đậm dòng cuối cùng của bảng hay không
        //10. Optional parameter: merge_row_index: dòng có ô cần gộp
        //11. Optional parameter: start_index: index của ô bắt đầu gộp
        //12. Optional parameter: end_index = index của ô kết thúc gộp
        //13. Optional parameter: p_footer: thông tin dưới footer của mẫu biểu
        //
        public static void CreateWordDocumentWithTable(string file_location, string output_location, List<string> list_nguon, List<string> list_dich, DataTable data, List<string> list_title, string font_family, double font_size, bool last_row_bold, int merge_row_index = -1, int start_index = -1, int end_index = -1, string p_footer = "")
        {
            try
            {
                // Load the document.
                using (DocX document = DocX.Load(file_location))
                {
                    if (list_nguon.Count == list_dich.Count)
                    {
                        for (int i = 0; i < list_nguon.Count; i++)
                        {
                            // Replace text in this document.
                            document.ReplaceText(list_nguon[i], list_dich[i]);
                        }
                    }

                    //Tạo bảng mới sau bảng trống
                    Table t = document.Tables[1];

                    Table table1 = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);
                    //Định dạng cho title
                    for (int i = 0; i < list_title.Count; i++)
                    {
                        //table1.Rows[0].Cells[i].Paragraphs.First().Append(list_title[i]).Bold().Alignment = Alignment.center;
                        table1.Rows[0].Cells[i].Paragraphs.First().Append(list_title[i]).Bold().Font(new FontFamily(font_family)).FontSize(font_size).Alignment = Alignment.center;
                        table1.Rows[0].Cells[i].VerticalAlignment = VerticalAlignment.Center;
                    }

                    //Điền thông tin vào phần nội dung của bảng
                    for (int row = 1; row < table1.RowCount; row++)
                    {
                        
                        //Nếu bôi đậm hàng dưới cùng
                        if (last_row_bold == true)
                        {
                            if (row == table1.RowCount -1)
                            {
                                for (int cell = 0; cell < table1.Rows[row].Cells.Count; cell++)
                                {
                                    table1.Rows[row].Cells[cell].Paragraphs.First().Append(data.Rows[row - 1][cell].ToString()).Bold().Font(new FontFamily(font_family)).FontSize(font_size).Alignment = Alignment.center;
                                    table1.Rows[row].Cells[cell].VerticalAlignment = VerticalAlignment.Center;
                                    table1.Rows[row].Height = 30;
                                    
                                }
                            }
                            else
                            {
                                for (int cell = 0; cell < table1.Rows[row].Cells.Count; cell++)
                                {
                                    table1.Rows[row].Cells[cell].Paragraphs.First().Append(data.Rows[row - 1][cell].ToString()).Font(new FontFamily(font_family)).FontSize(font_size).Alignment = Alignment.center;
                                    table1.Rows[row].Cells[cell].VerticalAlignment = VerticalAlignment.Center;
                                    table1.Rows[row].Height = 30;

                                }
                            }                           
                        }
                        //Không bôi đậm hàng cuối cùng
                        else
                        {
                            for (int cell = 0; cell < table1.Rows[row].Cells.Count; cell++)
                            {
                                table1.Rows[row].Cells[cell].Paragraphs.First().Append(data.Rows[row - 1][cell].ToString()).Font(new FontFamily(font_family)).FontSize(font_size).Alignment = Alignment.center;
                                table1.Rows[row].Cells[cell].VerticalAlignment = VerticalAlignment.Center;
                                table1.Rows[row].Height = 30;
                            }
                        }
                    }
                    //Merger cells in a row
                    if (merge_row_index >=0 && start_index >=0 && end_index >=0 && start_index < end_index )
                    {
                        table1.Rows[merge_row_index].MergeCells(start_index, end_index);
                        table1.Rows[merge_row_index].Height = 30;
                        Paragraph cell_paragraph = table1.Rows[merge_row_index].Cells[start_index].Paragraphs.Last();
                        table1.Rows[merge_row_index].Cells[start_index].RemoveParagraph(cell_paragraph);
                        table1.Rows[merge_row_index].Cells[start_index].VerticalAlignment = VerticalAlignment.Center;
                    }
                    
                    //table1.AutoFit = AutoFit.Contents;

                    // Center the Table
                    table1.Alignment = Alignment.center;
                    
               
                    //xóa bảng tạm trong file
                    t.Remove();

                    //Chèn footer
                    if (p_footer != "")
                    {
                        Footer footer_default = document.Footers.odd;                      
                        footer_default.InsertParagraph().Append(p_footer).FontSize(10).Font(new FontFamily(font_family));
                        footer_default.RemoveParagraph(footer_default.Paragraphs.First());
                    }
                   
                    //Lưu file
                    document.SaveAs(output_location);
                } // Release this document from memory.
            }
            catch
            {
                MessageBox.Show("File mẫu đang mở, liên hệ bộ phận Điện toán để kiểm tra");
                return;
            }
        }

        #endregion
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

        //Chuyển số sang chữ tiếng Việt
        public static string ChuyenSoSangChu(string number)
        {
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

        //Chuyển tiếng Việt có dấu sang không dấu
        public static string convertToUnSign(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        #region Currency_to_word_english
        private static String ones(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }

        private static String tens(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }

        private static String ConvertWholeNumber(String Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX   
                bool isDone = false;//test if already translated   
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))   
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric   
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping   
                    String place = "";//digit grouping name:hundres,thousand,etc...   
                    switch (numDigits)
                    {
                        case 1://ones' range   

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range   
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range   
                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";
                            break;
                        case 4://thousands' range   
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 7://millions' range   
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Million ";
                            break;
                        case 10://Billions's range   
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " Billion ";
                            break;
                        //add extra case options for anything above Billion...   
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)   
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                        }


                    }
                    //ignore digit grouping names   
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }

        private static String ConvertDecimals(String number)
        {
            String cd = "", digit = "", engOne = "";
            for (int i = 0; i < number.Length; i++)
            {
                digit = number[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cd += " " + engOne;
            }
            return cd;
        }  

        private static String ConvertToWords(String numb, string ccy)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = "Only";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "and";// just to separate whole numbers from points/cents   
                        endStr = " Cents " + endStr;//   
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = String.Format("{0} {1} {2}{3}{4}", ConvertWholeNumber(wholeNo).Trim(), ccy, andStr, pointStr, endStr);
            }
            catch { }
            return val;
        }

        public static string ChuyenSoSangChuEN(string number, string ccy)
        {
            string isNegative = "";
            try
            {
                number = Convert.ToDouble(number).ToString();

                if (number.Contains("-"))
                {
                    isNegative = "Minus ";
                    number = number.Substring(1, number.Length - 1);
                }
                return isNegative + ConvertToWords(number, ccy);
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        } 
        #endregion

        public static string GetServerAdd()
        {
            return server_add;
        }
    }
}
