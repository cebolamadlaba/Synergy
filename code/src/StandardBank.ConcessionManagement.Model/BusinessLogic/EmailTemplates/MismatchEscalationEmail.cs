using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates
{
    public class MismatchEscalationEmail
    {
        public bool Is24HourEscalation { get; set; }
        public IEnumerable<string> RecipientEmailList { get; set; }
        public IEnumerable<ConcessionMismatchEscalationView> ConcessionMismatchEscalationViews { get; set; }
        public string CmsServerLocation { get; set; }
    }
}
