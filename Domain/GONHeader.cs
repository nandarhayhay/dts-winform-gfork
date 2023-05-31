using System;
using System.Collections.Generic;
using System.Text;

namespace Nufarm.Domain
{
    public class GONHeader : DomainObject
    {
        public GONHeader()
        {

        }
        private string _GON_NO = "";

        public string GON_NO
        {
            get { return _GON_NO; }
            set { _GON_NO = value; }
        }
        private object _GON_DATE = null;

        public object GON_DATE
        {
            get { return _GON_DATE; }
            set { _GON_DATE = value; }
        }
        /// <summary>
        /// Transporter ID who'll send the goods to distributor
        /// </summary>
        private string _GT_ID = "";
        /// <summary>
        /// Transporter ID who'll send the goods to distributor
        /// </summary>
        public string GT_ID
        {
            get { return _GT_ID; }
            set { _GT_ID = value; }
        }
        private string _GON_ID_AREA = "";

        public string GON_ID_AREA
        {
            get { return _GON_ID_AREA; }
            set { _GON_ID_AREA = value; }
        }
        private string _SPPBNO = "";

        public string SPPBNO
        {
            get { return _SPPBNO; }
            set { _SPPBNO = value; }
        }
        private string _statusToBecome = "";

        public string StatusToBecome
        {
            get { return _statusToBecome; }
            set { _statusToBecome = value; }
        }
        private string _distributorID = "";

        public string DistributorID
        {
            get { return _distributorID; }
            set { _distributorID = value; }
        }
        private string _customerName = "";

        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }
        private string _customerAddress = "";

        public string CustomerAddress
        {
            get { return _customerAddress; }
            set { _customerAddress = value; }
        }
        private string _warhouseCode = "";

        public string WarhouseCode
        {
            get { return _warhouseCode; }
            set { _warhouseCode = value; }
        }
        private string _policeNoTrans = "";

        public string PoliceNoTrans
        {
            get { return _policeNoTrans; }
            set { _policeNoTrans = value; }
        }
        private string _driverTrans = "";

        public string DriverTrans
        {
            get { return _driverTrans; }
            set { _driverTrans = value; }
        }
        private string _shipTo = "";

        public string ShipTo
        {
            get { return _shipTo; }
            set { _shipTo = value; }
        }
    }
}
