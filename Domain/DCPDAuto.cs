using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Nufarm.Domain
{
    public class DCPDAuto : DomainObject,IDisposable 
    {
        public DCPDAuto()
        {
            if (this._TDiscProgress != null) { this._TDiscProgress.Rows.Clear(); }
            if (this._TDiscTerms != null) { this._TDiscTerms.Rows.Clear(); }
            if (this._TProduct != null) { this._TProduct.Rows.Clear(); }
        }
        private object _startPeriode = null;

        public object StartPeriode
        {
            get { return _startPeriode; }
            set { _startPeriode = value; }
        }
        private object _endPeriode = null;

        public object EndPeriode
        {
            get { return _endPeriode; }
            set { _endPeriode = value; }
        }
        private DataTable _TProduct = null;

        public DataTable TProduct
        {
            get { return _TProduct; }
            set { _TProduct = value; }
        }
        private DataTable _TDiscProgress = null;

        public DataTable TDiscProgress
        {
            get { return _TDiscProgress; }
            set { _TDiscProgress = value; }
        }

        private DataTable _TDiscTerms = null;

        public DataTable TDiscTerms
        {
            get { return _TDiscTerms; }
            set { _TDiscTerms = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_TDiscProgress != null) { _TDiscProgress.Dispose(); _TDiscProgress = null; }
            if (_TDiscTerms != null) { _TDiscTerms.Dispose(); _TDiscTerms = null; }
            if (_TProduct != null) { _TProduct.Dispose(); _TProduct = null; }
        }

        #endregion
    }
}
