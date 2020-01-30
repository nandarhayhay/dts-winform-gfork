using System;
using System.Collections.Generic;
using System.Text;

namespace Nufarm.Domain
{
    public class RetailerContact:DomainObject 
    {
        public RetailerContact()
        { 
        }
        private string iDKios = "";

        public string IDKios
        {
            get { return iDKios; }
            set { iDKios = value; }
        }
        private string contactMobile = "";

        public string ContactMobile
        {
            get { return contactMobile; }
            set { contactMobile = value; }
        }
    }
    
}
