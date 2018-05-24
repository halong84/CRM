using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace CRM.Entities
{
    class Phong
    {
        #region Instance Properties

        private string _mapb;
        public string mapb
        {
            get { return _mapb; }
            set { _mapb = value; }
        }

        private string _macn;
        public string macn
        {
            get { return _macn; }
            set { _macn = value; }
        }

        private string _tenpb;
        public string tenpb
        {
            get { return _tenpb; }
            set { _tenpb = value; }
        }

        #endregion Instance Properties

        public Phong(DataRow row)
        {
            this._mapb = row["MAPB"].ToString();
            this._macn = row["MACN"].ToString();
            this._tenpb = row["TENPB"].ToString();
        }

        public Phong(string[] phong)
        {
            this._mapb = phong[0];
            this._macn= phong[1];
            this._tenpb = phong[2];
        }
    }
}
