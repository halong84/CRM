using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Globalization;
namespace CRM.Entities
{
    class Tinh
    {
        #region Instance Properties

        private string _matinh;
        public string matinh
        {
            get { return _matinh; }
            set { _matinh = value; }
        }

        private string _tentinh;
        public string tentinh
        {
            get { return _tentinh; }
            set { _tentinh = value; }
        }

        #endregion Instance Properties

        public Tinh(DataRow row)
        {
            this._matinh = row["MaTinh"].ToString();
            this._tentinh = row["TenTinh"].ToString();
        }

        public Tinh(string[] tinh)
        {
            this._matinh = tinh[0];
            this._tentinh = tinh[1];
        }
    }
}
