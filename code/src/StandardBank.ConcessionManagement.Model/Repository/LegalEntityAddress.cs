using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    public partial class LegalEntityAddress
    {
        public LegalEntityAddress() { }

        public int Id { get; set; }
        public int LegalEntityId { get; set; }
        public string ContactPerson { get; set; }
        public string CustomerName { get; set; }
        public string PostalAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? Datemodified { get; set; }
    }
}
