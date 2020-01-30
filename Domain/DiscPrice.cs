using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Nufarm.Domain
{
   public class DiscPrice : DomainObject, IDisposable 
    {
        public DiscPrice()
        {
            _listDistributors = new List<string>();
            _listBrands = new List<string>();
            _listGroupDist = new List<string>();
            _listBrandPacks = new List<string>();
            _dsProgDesc = new DataSet();
        }
        private bool _hasChangedDistr = false;

        public bool HasChangedDistr
        {
            get { return _hasChangedDistr; }
            set { _hasChangedDistr = value; }
        }
        private bool hasChangedBrands = false;

        public bool HasChangedBrands
        {
            get { return hasChangedBrands; }
            set { hasChangedBrands = value; }
        }
        private bool _hasChangedBrandPacks = false;

        public bool HasChangedBrandPacks
        {
            get { return _hasChangedBrandPacks; }
            set { _hasChangedBrandPacks = value; }
        }
        private bool hasChangedGroups = false;

        public bool HasChangedGroups
        {
            get { return hasChangedGroups; }
            set { hasChangedGroups = value; }
        }
        private List<string> _listBrandPacks = null;

        public List<string> ListBrandPacks
        {
            get { return _listBrandPacks; }
            set { _listBrandPacks = value; }
        }
        private List<string> _listDistributors = null;

        public List<string> ListDistributors
        {
            get { return _listDistributors; }
            set { _listDistributors = value; }
        }
        private List<string> _listBrands = null;

        public List<string> ListBrands
        {
            get { return _listBrands; }
            set { _listBrands = value; }
        }
        private List<string> _listGroupDist = null;

        public List<string> ListGroupDist
        {
            get { return _listGroupDist; }
            set { _listGroupDist = value; }
        }
        private DataSet _dsProgDesc = null;

        public DataSet DsProgDesc
        {
            get { return _dsProgDesc; }
            set { _dsProgDesc = value; }
        }
        private decimal _priceFM = 0M;

        public decimal PriceFM
        {
            get { return _priceFM; }
            set { _priceFM = value; }
        }
        private DateTime _applyDate = DateTime.Now;

        public DateTime ApplyDate
        {
            get { return _applyDate; }
            set { _applyDate = value; }
        }
       private DateTime _endDate = DateTime.Now;
       public DateTime EndDate
       {
           get { return _endDate; }
           set { _endDate = value; }
       }
        private string _applyTo = "All";

        public string ApplyTo
        {
            get { return _applyTo; }
            set { _applyTo = value; }
        }
       private object _programID = "";

        public object  ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }
        #region IDisposable Members

        public void Dispose()
        {
            this._dsProgDesc.Dispose();
            this._listBrands.Clear();
            this._listDistributors.Clear();
            this._listGroupDist.Clear(); 
        }

        #endregion
    }
}
