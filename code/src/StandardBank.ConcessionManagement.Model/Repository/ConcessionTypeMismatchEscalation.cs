using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    public class ConcessionTypeMismatchEscalation
    {
        public ConcessionTypeMismatchEscalation() { }

        public int ConcessionTypeMismatchEscalationId { get; set; }
        public int ConcessionTypeId { get; set; }
        public DateTime LastEscalationSentDateTime { get; set; }
        public string ConcessionType { get; set; }

    }
}
