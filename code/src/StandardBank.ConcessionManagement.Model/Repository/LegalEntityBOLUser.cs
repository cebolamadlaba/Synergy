using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    public class LegalEntityBOLUser
    {
        public int pkLegalEntityBOLUserId { get; set; }

        public int fkLegalEntityAccountId { get; set; }

        public string BOLUserId { get; set; }

        public int legalEntityId { get; set; }

        public int legalEntityAccountId { get; set; }

    }
}
