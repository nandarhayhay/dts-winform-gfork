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
    }
}
