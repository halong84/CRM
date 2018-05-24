using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace CRM.Entities
{
    class User
    {
        #region Instance Properties

        private string _user_id;
        public string user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        private string _user_pass;
        public string user_pass
        {
            get { return _user_pass; }
            set { _user_pass = value; }
        }

        private string _group_list;
        public string group_list
        {
            get { return _group_list; }
            set { _group_list = value; }
        }

        private string _manv;
        public string manv
        {
            get { return _manv; }
            set { _manv = value; }
        }

        private string _tennv;
        public string tennv
        {
            get { return _tennv; }
            set { _tennv = value; }
        }

        private string _chucvu;
        public string chucvu
        {
            get { return _chucvu; }
            set { _chucvu = value; }
        }

        private string _macn;
        public string macn
        {
            get { return _macn; }
            set { _macn = value; }
        }

        private string _ghichu;
        public string ghichu
        {
            get { return _ghichu; }
            set { _ghichu = value; }
        }

        private string _mapb;
        public string mapb
        {
            get { return _mapb; }
            set { _mapb = value; }
        }

        private bool _kichhoat;

        public bool kichhoat
        {
            get { return _kichhoat; }
            set { _kichhoat = value; }
        }

        #endregion Instance Properties

        public User(DataRow row)
        {
            this._user_id = row["USER_ID"].ToString();
            this._user_pass = row["USER_PASS"].ToString();
            this._group_list = row["GROUP_LIST"].ToString();
            this._manv = row["MANV"].ToString();
            this._tennv = row["TENNV"].ToString();
            this._chucvu = row["CHUCVU"].ToString();
            this._macn = row["MACN"].ToString();
            this._ghichu = row["GHICHU"].ToString();
            this._mapb = row["MAPB"].ToString();
            this._kichhoat = Convert.ToBoolean(row["KICHHOAT"].ToString());
        }

        public User(string[] user)
         {
             this._user_id = user[0];
             this._user_pass = user[1];
             this._group_list = user[2];
             this._manv = user[3];
             this._tennv = user[4];
             this._chucvu = user[5];
             this._macn = user[6];
             this._ghichu = user[7];
             this._mapb = user[8];
             this._kichhoat = Convert.ToBoolean(user[9]);
         }
    }
}