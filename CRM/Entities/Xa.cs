using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace CRM.Entities
{
    class Xa
    {
        #region Instance Properties

        private string _maxa;
        public string maxa
        {
            get { return _maxa; }
            set { _maxa = value; }
        }

        private string _tenxa;
        public string tenxa
        {
            get { return _tenxa; }
            set { _tenxa = value; }
        }

        #endregion Instance Properties

        public Xa(DataRow row)
        {
            this._maxa = row["MaXa"].ToString();
            this._tenxa = row["TenXa"].ToString();
        }

        public Xa(string[] xa)
        {
            this._maxa = xa[0];
            this._tenxa = xa[1];
        }
    }
}
