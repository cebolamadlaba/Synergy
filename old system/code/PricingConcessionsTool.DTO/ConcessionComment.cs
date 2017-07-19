using PricingConcessionsTool.DTO.Enums;
using System;

namespace PricingConcessionsTool.DTO
{
    public class ConcessionComment
    {
        public int ConcessionCommentId { get; set; }
        public ConcessionSubStatuses SubStatus { get; set; }
        public string Comment { get; set; }
        public DateTime SystemDate { get; set; }
        public UserProfile User { get; set; }
    }
}
