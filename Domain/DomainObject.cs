using System;
using System.Collections.Generic;
using System.Text;

namespace Nufarm.Domain
{
    public abstract class DomainObject
    {

        string _CodeApp = "";
        string _OwnerApp = "";
        string _DescriptionApp = "";
        int  _IDApp = 0;
        string _NameApp = "";
        string _TypeApp = "";
        object _CrDate = null ;
        string _CreatedBy = null ;
        object  _ModifiedDate = null ;
        string _ModifiedBy = "";
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        public object ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        public object CreatedDate
        {
            get { return _CrDate; }
            set { _CrDate = value; }
        }

        public string TypeApp
        {
            get { return _TypeApp; }
            set { _TypeApp = value; }
        }

        public string NameApp
        {
            get { return _NameApp; }
            set { _NameApp = value; }
        }

        public int IDApp
        {
            get { return _IDApp; }
            set { _IDApp = value; }
        }

        public string DescriptionApp
        {
            get { return _DescriptionApp; }
            set { _DescriptionApp = value; }
        }

        public string OwnerApp
        {
            get { return _OwnerApp; }
            set { _OwnerApp = value; }
        }

        public string CodeApp
        {
            get { return _CodeApp; }
            set { _CodeApp = value; }
        }
    }
}
