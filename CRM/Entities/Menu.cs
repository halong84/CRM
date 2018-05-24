using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Globalization;
namespace CRM.Entities
{
    class Menu
    {
        #region Instance Properties

        private string _menu_id;
        public string menu_id
        {
            get { return _menu_id; }
            set { _menu_id = value; }
        }

        private string _menu_title;
        public string menu_title
        {
            get { return _menu_title; }
            set { _menu_title = value; }
        }

        private string _parent_id;
        public string parent_id
        {
            get { return _parent_id; }
            set { _parent_id = value; }
        }

        private Int32 _deep;
        public Int32 deep
        {
            get { return _deep; }
            set { _deep = value; }
        }

        private Int32 _pos;
        public Int32 pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        private string _group_list;
        public string group_list
        {
            get { return _group_list; }
            set { _group_list = value; }
        }

        private string _form_name;
        public string form_name
        {
            get { return _form_name; }
            set { _form_name = value; }
        }

        private Boolean _kich_hoat;
        public Boolean kich_hoat
        {
            get { return _kich_hoat; }
            set { _kich_hoat = value; }
        }

        private Int32 _main_seq;
        public Int32 main_seq
        {
            get { return _main_seq; }
            set { _main_seq = value; }
        }

        private Int32 _sub_seq;
        public Int32 sub_seq
        {
            get { return _sub_seq; }
            set { _sub_seq = value; }
        }

        #endregion Instance Properties

        public Menu(DataRow row)
        {
            this._menu_id = row["MENU_ID"].ToString();
            this._menu_title = row["MENU_TITLE"].ToString();
            this._parent_id = row["PARENT_ID"].ToString();
            this._deep = Convert.ToInt32(row["DEEP"].ToString());
            this._pos = Convert.ToInt32(row["POS"].ToString());
            this._group_list = row["GROUP_LIST"].ToString();
            this._form_name = row["FORM_NAME"].ToString();
            this._kich_hoat = Convert.ToBoolean(row["KICH_HOAT"].ToString());
            this._main_seq = Convert.ToInt32(row["MAIN_SEQ"].ToString());
            this._sub_seq = Convert.ToInt32(row["SUB_SEQ"].ToString());
        }

        public Menu(string[] menu)
         {
             this._menu_id = menu[0];
             this._menu_title = menu[1];
             this._parent_id = menu[2];
             this._deep = Convert.ToInt32(menu[3]);
             this._pos = Convert.ToInt32(menu[4]);
             this._group_list = menu[5];
             this._form_name = menu[6];
             this._kich_hoat = Convert.ToBoolean(menu[7]);
             this._main_seq = Convert.ToInt32(menu[8]);
             this._sub_seq = Convert.ToInt32(menu[9]);
         }
    }
}
