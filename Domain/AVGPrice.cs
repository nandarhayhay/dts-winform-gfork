using System;
using System.Collections.Generic;
using System.Text;
namespace Nufarm.Domain
{
   public class AVGPrice:DomainObject 
    {
        private decimal _AvgPrice = 0;

        public decimal AvgPrice_FM
        {
            get { return _AvgPrice; }
            set { _AvgPrice = value; }
        }
       private List<string> _listBrandID = new List<string>();

        public List<string> ListBrandID
        {
            get { return _listBrandID; }
            set { _listBrandID = value; }
        }
        private object _startPeriode = null;

        public object StartPeriode
        {
            get { return _startPeriode; }
            set { _startPeriode = value; }
        }
        private decimal _avgPrice_PL = 0;

        public decimal AvgPrice_PL
        {
            get { return _avgPrice_PL; }
            set { _avgPrice_PL = value; }
        }
    }
}
