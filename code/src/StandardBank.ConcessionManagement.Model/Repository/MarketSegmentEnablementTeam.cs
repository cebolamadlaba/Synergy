using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// MarketSegmentEnablementTeam entity
    /// </summary>
    public class MarketSegmentEnablementTeam
    {

        public int Id { get; set; }

        public int fkMarketSegmentId { get; set; }

        public string EnablementTeamUserEmail { get; set; }

        public bool IsActive { get; set; }
    }
}
