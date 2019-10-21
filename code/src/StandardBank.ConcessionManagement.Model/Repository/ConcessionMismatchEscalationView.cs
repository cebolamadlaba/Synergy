using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    public class ConcessionMismatchEscalationView
    {
        public int ConcessionId { get; set; }
        public string ConcessionRef { get; set; }
        public DateTime ConcessionDate { get; set; }
        public int ConcessionTypeId { get; set; }
        public string ConcessionType { get; set; }
        public int RiskGroupId { get; set; }
        public string RiskGroupName { get; set; }
        public string RiskGroupNumber { get; set; }
        public int LegalEntityId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public int EnablementTeamUserId { get; set; }
        public string EnablementTeamUserFullname { get; set; }
        public string EnablementTeamUserEmailAddress { get; set; }
        public int MarketSegmentId { get; set; }
        public string MarketSegment { get; set; }
        public DateTime LastEscalationSentDateTime { get; set; }
    }
}
