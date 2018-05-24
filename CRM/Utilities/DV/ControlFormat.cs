using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace CRM.Utilities.DV
{
           class ControlFormat
        {
            
            //Hide columns with specified name
            public static void DataGridViewFormat(DataGridView dgv, string[] columns)
            {
                foreach (DataGridViewColumn cl in dgv.Columns)
                {
                    if (!columns.Contains(cl.Name))
                        cl.Visible = false;
                }
            }

            //Định dạng hàng cột trong gridview
            public static void DataGridViewFormat(DataGridView dgv)
            {
                //DataGridViewRow row = dgv.RowTemplate;
                //dgv.RowTemplate.DefaultCellStyle.BackColor = Color.Bisque;
                //dgv.RowTemplate.DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                //row.Height = 35;
                //row.MinimumHeight = 20;
                //dgv.DefaultCellStyle.Font.Bold = false;
                int rowNumber = 1;
                dgv.Font = new Font("Microsoft Sans Serif",10, FontStyle.Regular);
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (rowNumber%2==0)
                    { 
                        row.DefaultCellStyle.BackColor = Color.White;
                        //row.DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Regular);
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.SkyBlue;
                        //row.DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Regular);
                        //row.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                    }
                    rowNumber = rowNumber + 1;
                }
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                //DataGridViewColumn column = dgv.Columns[0];
                //column.Width = 100;
            }

            public static void DataGridViewColumnFormat(DataGridView dgv, List<string> columns)
            {
                foreach (DataGridViewColumn cl in dgv.Columns)
                {
                    if (columns.Contains(cl.Name))
                        cl.Visible = false;
                }
            }

            //Định dạng cho các cột số trong datagridview
            public static void DataGridView_NumberColumnFormat(DataGridView dgv, List<string> columns)
            {
                foreach (DataGridViewColumn cl in dgv.Columns)
                {
                    if (columns.Contains(cl.Name))
                    {
                        cl.DefaultCellStyle.Format = "N0";
                    }
                       
                }
            }
            
            //Hiện tất cả các cột

            public static void DataGridView_ShowAllColumn(DataGridView dgv)
            {
                foreach (DataGridViewColumn cl in dgv.Columns)
                {
                    cl.Visible = true;
                }
            }
            //Ẩn những cột không có dữ liệu trong datagridview
            public static void DataGridView_RemoveEmptyColumns(DataGridView dgv)
            {
                foreach (DataGridViewColumn clm in dgv.Columns)
                {
                    bool notAvailable = true;

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (!string.IsNullOrEmpty(row.Cells[clm.Index].Value.ToString()))
                        {
                            notAvailable = false;
                            break;
                        }
                    }
                    if (notAvailable)
                    {
                        dgv.Columns[clm.Index].Visible = false;
                    }
                }
            }

            //public static DataGridView RemoveEmptyColumns(this DataGridView grdView)
            //{
            //    foreach (DataGridViewColumn clm in grdView.Columns)
            //    {
            //        bool notAvailable = true;

            //        foreach (DataGridViewRow row in grdView.Rows)
            //        {
            //            if (!string.IsNullOrEmpty(row.Cells[clm.Index].Value.ToString()))
            //            {
            //                notAvailable = false;
            //                break;
            //            }
            //        }
            //        if (notAvailable)
            //        {
            //            grdView.Columns[clm.Index].Visible = false;
            //        }
            //    }

            //    return grdView;
            //}

            public static int ToIntPrice(string price)
            {
                string s = price.Replace(",", "");

                return int.Parse(s);
            }

            public static string ToFormatPrice(Int64 price)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("es-ES", true);
                string s = price.ToString("0,0", culture);
                //string s = price.ToString("0,0.00", culture);
                return s;
            }
            public static string ToFormatDientich(decimal dien_tich)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("es-ES", true);
                //string s = price.ToString("0,0", culture);
                string s;
                if ((dien_tich % 1) == 0)
                {
                    s = Convert.ToString((int)Decimal.ToInt32(dien_tich));
                }
                else
                {
                    s = dien_tich.ToString("0,0.00", culture);
                }
                return s;
            }

            //Bỏ qua dấu phẩy ngăn cách phần nghìn trong chữ số
            public static string skipComma(string str)
            {

                string[] ss = null;
                string strnew = "";
                if (str == "")
                {
                    strnew = "0";
                }
                else
                {
                    ss = str.Split(',');
                    for (int i = 0; i < ss.Length; i++)
                    {
                        strnew += ss[i];
                    }
                }
                return strnew;
            }
            //
        }
}
