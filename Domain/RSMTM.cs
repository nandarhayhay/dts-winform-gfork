using System;
using System.Collections.Generic;
using System.Text;

namespace Nufarm.Domain
{
    public class RSMTM : DomainObject
    {
        private string _HP = "";

        public string HP
        {
            get { return _HP; }
            set { _HP = value; }
        }
    }
}
