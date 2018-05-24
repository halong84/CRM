using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace CRM.Entities
{
    class Huyen
    {
        #region Instance Properties

        private string _mahuyen;
        public string mahuyen
        {
            get { return _mahuyen; }
            set { _mahuyen = value; }
        }

        private string _tenhuyen;
        public string tenhuyen
        {
            get { return _tenhuyen; }
            set { _tenhuyen = value; }
        }

        #endregion Instance Properties

        public Huyen(DataRow row)
        {
            this._mahuyen = row["MaHuyen"].ToString();
            this._tenhuyen = row["TenHuyen"].ToString();
        }

        public Huyen(string[] huyen)
        {
            this._mahuyen = huyen[0];
            this._tenhuyen = huyen[1];
        }
    }
}
