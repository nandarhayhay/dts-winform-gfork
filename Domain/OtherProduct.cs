using System;
using System.Collections.Generic;
using System.Text;

namespace Nufarm.Domain
{
   public class OtherProduct: DomainObject
    {
       public OtherProduct()
       { 
       
       }
       private string itemName = "";

       public string ItemName
       {
           get { return itemName; }
           set { itemName = value; }
       }
       private string unit1 = "";

       public string Unit1
       {
           get { return unit1; }
           set { unit1 = value; }
       }
       private decimal vol1 = 0M;

       public decimal Vol1
       {
           get { return vol1; }
           set { vol1 = value; }
       }
       private string unit2 = "";

       public string Unit2
       {
           get { return unit2; }
           set { unit2 = value; }
       }
       private decimal  vol2 = 0M;

       public decimal Vol2
       {
           get { return vol2; }
           set { vol2 = value; }
       }
       private string uOM = "";

       public string UOM
       {
           get { return uOM; }
           set { uOM = value; }
       }
       private string remark = "";

       public string Remark
       {
           get { return remark; }
           set { remark = value; }
       }
       private decimal _dev_Qty = 0M;

       public decimal Dev_Qty
       {
           get { return _dev_Qty; }
           set { _dev_Qty = value; }
       }
    }
}
