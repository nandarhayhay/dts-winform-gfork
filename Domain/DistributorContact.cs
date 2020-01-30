using System;
using System.Collections.Generic;
using System.Text;

namespace Nufarm.Domain
{
    public class DistributorContact : DomainObject
    {

        public DistributorContact()
        {

        }
        private string contact1 = "";

        public string Contact1
        {
            get { return contact1; }
            set { contact1 = value; }
        }
        private string contact2 = "";

        public string Contact2
        {
            get { return contact2; }
            set { contact2 = value; }
        }
        private bool selectAllContact = false;

        public bool SelectAllContact
        {
            get { return selectAllContact; }
            set { selectAllContact = value; }
        }
        private string _Contact_Mobile = "";

        public string Contact_Mobile
        {
            get { return _Contact_Mobile; }
            set { _Contact_Mobile = value; }
        }
    }
}
