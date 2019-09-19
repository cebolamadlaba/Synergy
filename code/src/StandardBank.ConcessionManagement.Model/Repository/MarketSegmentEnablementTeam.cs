using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// MarketSegmentEnablementTeamUser entity
    /// </summary>
    public class MarketSegmentEnablementTeamUser
    {

        public int Id { get; set; }

        public int MarketSegmentId { get; set; }

        public string UserId { get; set; }

        public bool IsActive { get; set; }

        public string Fullname { get; set; }

        public string EmailAddress { get; set; }
    }

    public class MarketSegmentEnablementTeamUserView
    {
        public string ConcessionRef { get; set; }
        public DateTime ConcessionDate { get; set; }
        public string ConcessionType { get; set; }
        public string RiskGroupName { get; set; }
        public string RiskGroupNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string Fullname { get; set; }
        public string EmailAddress { get; set; }
        public int MarketSegmentId { get; set; }
        public DateTime LastEscalationSentDateTime { get; set; }
    }
}
