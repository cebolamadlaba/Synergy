using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    public class Condition
    {
        public int ConcessionId { get; set; }
        public decimal? InterestRate { get; set; }
        public int? Volume { get; set; }
        public decimal? Value { get; set; }
        public string RiskGroupName { get; set; }
        public int RiskGroupNumber { get; set; }
        public string ConditionType { get; set; }
        public string ProductType { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string PeriodName { get; set; }
        public string RagStatus { get; set; }
    }
}
