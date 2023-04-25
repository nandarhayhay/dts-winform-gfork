using System;
using System.Collections.Generic;
using System.Text;


namespace Nufarm.Domain
{
    public class SPPBHeader:DomainObject 
    {
        public SPPBHeader()
        {

        }
        private string _PONumber = "";

        public string PONumber
        {
            get { return _PONumber; }
            set { _PONumber = value; }
        }
        private object _PODate = DBNull.Value;

        public object PODate
        {
            get { return _PODate; }
            set { _PODate = value; }
        } 
        private string _SPPBNO = "";

        public string SPPBNO
        {
            get { return _SPPBNO; }
            set { _SPPBNO = value; }
        }
        private object _SPPBDate = DBNull.Value;

        public object SPPBDate
        {
            get { return _SPPBDate; }
            set { _SPPBDate = value; }
        }
        private object _sppBReceived = DBNull.Value;

        //public object SppBReceived
        //{
        //    get { return _sppBReceived; }
        //    set { _sppBReceived = value; }
        //}
    }
}
