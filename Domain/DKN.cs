using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Nufarm.Domain
{
    public class DKN:DomainObject 
    {
        private object _startDate = null;
        private object _endDate = null;
        public object  StartDate
        {
            get { return _startDate; }
            set {_startDate = value; }
        }
        public object EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        string _ProductToGive = "";
        public string ProductToGive
        {
            get { return _ProductToGive; }
            set {_ProductToGive = value; }
        }
        string _ProductRule = "";
        public string ProductRule
        {
            get { return _ProductRule; }
            set { _ProductRule = value; }
        }
       
    }
}
