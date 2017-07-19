using PricingConcessionsTool.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class Concession
    {
        public Concession()
        {
            AccountList = new List<string>();
            CommentList = new List<ConcessionComment>();
        }

        public Types Type { get; set; }

        public UserProfile User { get; set; }

        public int ConcessionId { get; set; }

        public string UserName { get; set; }

        public string ReferenceNumber { get; set; }

        public string DealNumber { get; set; }

        public DateTime? ConcessionDate { get; set; }

        public string Motivation { get; set; }
        public ConcessionStatuses Status { get; set; }

        public ConcessionSubStatuses SubStatus { get; set; }

        public ConcessionTypes ConcessionType { get; set; }

        public string StatusDescription { get; set; }

        public string SubStatusDescription { get; set; }

        public int TotalDaysOpen { get; set; }
        public List<ConcessionCondition> ConditionList { get; set; }

        public Customer Customer { get; set; }

        public DateTime? DatesentForApproval { get; set; }

        public DateTime? DateApproved { get; set; }

        public string RequestorANumber { get; set; }

        public List<string> AccountList { get; set; }

        public List<ConcessionComment> CommentList { get; set; }

        public string ConcessionTypeDescription { get; set; }

        public string AccountNumber
        {
            get
            {
                if (AccountList == null)
                {
                    return null;
                }
                else
                {
                    var sb = new StringBuilder();

                    foreach (var acc in AccountList)
                    {
                        sb.Append(acc);
                    }

                    return sb.ToString();
                }
            }
        }

        public bool CanDelete
        {
            get
            {
                return Status == ConcessionStatuses.Approved;
            }
        }

        public FinancialInfo FinancialInfo { get; set; }

        public UserProfile Requestor { get; set; }

        public UserProfile BusinessCentreManager { get; set; }

        public UserProfile PricingManager { get; set; }
        public int RequestorId { get; set; }
        public int? BusinessCentreManagerId { get; set; }
        public int? PricingManagerId { get; set; }
        public string NewComment { get; set; }
        public DateTime ? ExpiryDate { get; set; }
        public string ConcessionTypeCode { get; set; }
        public bool IsCurrent { get; set; }
        public int CenterIId { get; set; }
    }
}
