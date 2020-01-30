using System;
using System.Collections.Generic;
using System.Text;

namespace Nufarm.Domain
{
    public class Transporter : DomainObject
    {
        public Transporter()
        {

        }
        private string gT_ID = "";

        public string GT_ID
        {
            get { return gT_ID; }
            set { gT_ID = value; }
        }
        private string _address = "";

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _email = "";

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _NPWP = "";

        public string NPWP
        {
            get { return _NPWP; }
            set { _NPWP = value; }
        }
        private string _contactPerson = "";

        public string ContactPerson
        {
            get { return _contactPerson; }
            set { _contactPerson = value; }
        }
        private string _phone = "";

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        private string _FAX = "";

        public string FAX
        {
            get { return _FAX; }
            set { _FAX = value; }
        }
        private string _responsiblePerson = "";

        public string ResponsiblePerson
        {
            get { return _responsiblePerson; }
            set { _responsiblePerson = value; }
        }
        private string _mobile = "";


        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        private object birthDate = null;

        public object BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }
        private string _postalCode = "";

        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }
    }
}
