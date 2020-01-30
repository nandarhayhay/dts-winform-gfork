using System;
using System.Collections.Generic;
using System.Text;

namespace Nufarm.Domain
{
   public  class Adjustment : DomainObject 
    {
        //IDApp,DISTRIBUTOR_ID,BRANDPACK_ID,QUANTITY,ADJUSTMENT_FOR,START_PERIODE,END_PERIODE,ADJ_DESCRIPTION,CREATE_BY,CREATE_DATE
        private string _DistributorID = "";

        public string DistributorID
        {
            get { return _DistributorID; }
            set { _DistributorID = value; }
        }
        private string _BrandPackID = "";

        public string BrandPackID
        {
            get { return _BrandPackID; }
            set { _BrandPackID = value; }
        }
        private decimal _Quantity = 0M;

        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        private object _StartDate = null;

        public object StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private object _EndDate = null;

        public object EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        bool _IsGroup = false;

        public bool IsGroup
        {
            get { return _IsGroup; }
            set { _IsGroup = value; }
        }
        string groupCode = "PAN";

        public string GroupCode
        {
            get { return groupCode; }
            set { groupCode = value; }
        }
        private List<String> _listDistributors = new List<string>();

        public List<String> ListDistributors
        {
            get { return _listDistributors; }
            set { _listDistributors = value; }
        }
    }
}
