using System;

namespace Nufarm.Domain
{
    public class OtherProduct : DomainObject
    {
        private string itemName = "";

        private string unit1 = "";

        private decimal vol1;

        private string unit2 = "";

        private decimal vol2;

        private string uOM = "";

        private string remark = "";

        private decimal _dev_Qty;

        public decimal Dev_Qty
        {
            get
            {
                return this._dev_Qty;
            }
            set
            {
                this._dev_Qty = value;
            }
        }

        public string ItemName
        {
            get
            {
                return this.itemName;
            }
            set
            {
                this.itemName = value;
            }
        }

        public string Remark
        {
            get
            {
                return this.remark;
            }
            set
            {
                this.remark = value;
            }
        }

        public string Unit1
        {
            get
            {
                return this.unit1;
            }
            set
            {
                this.unit1 = value;
            }
        }

        public string Unit2
        {
            get
            {
                return this.unit2;
            }
            set
            {
                this.unit2 = value;
            }
        }

        public string UOM
        {
            get
            {
                return this.uOM;
            }
            set
            {
                this.uOM = value;
            }
        }

        public decimal Vol1
        {
            get
            {
                return this.vol1;
            }
            set
            {
                this.vol1 = value;
            }
        }

        public decimal Vol2
        {
            get
            {
                return this.vol2;
            }
            set
            {
                this.vol2 = value;
            }
        }

        public OtherProduct()
        {
        }
    }
}