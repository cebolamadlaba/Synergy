using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// GlmsTierDataView entity
    /// </summary>
    public class GlmsTierDataView
    {
        public string Rate { get; set; }

        public string BaseRate { get; set; }

        public decimal TierTo { get; set; }

        public decimal TierFrom { get; set; }

        public decimal Spread { get; set; }

        public decimal Value { get; set; }
    }
}
