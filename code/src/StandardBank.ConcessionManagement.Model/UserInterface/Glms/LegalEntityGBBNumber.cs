using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Glms
{
    public class LegalEntityGBBNumber
    {
        public int pkLegalEntityGBBNumber { get; set; }

        public int fkLegalEntityAccountId { get; set; }

        public string GBBNumber { get; set; }

        public int legalEntityId { get; set; }

        public int legalEntityAccountId { get; set; }

    }
}
