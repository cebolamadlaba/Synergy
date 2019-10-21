namespace StandardBank.ConcessionManagement.Model.BusinessLogic
{
    public class Constants
    {
        public static class EmailTemplates
        {
            public const string NewConcession = "NewConcession";
            public const string ConcessionApproved = "Approved";
            public const string ConcessionApprovedWithChanges = "ApprovedWithChanges";
            public const string ConcessionDeclined = "Declined";
            public const string ConcessionForwarded = "Forwarded";

        }

        public static class Roles
        {
            public const string Requestor = "Requestor";
            public const string BCM = "BCM";
            public const string PCM = "PCM";
            public const string HeadOffice = "Head Office";
            public const string AA = "AA";
        }

        public enum ApprovalStep
        {
            BCMApproval,
            PCMApproval,
            RequestorApproval
        }

        public enum RoleSubRole
        {
            BolUser = 1,
            TradeUser = 2,
            PCMSnIUser = 4
        }

        public static class ConcessionType
        {
            public const string Lending = "Lending";
            public const string Investment = "Investment";
            public const string MerchantAcquiring = "Merchant Acquiring";
            public const string Trade = "Trade";
            public const string Transactional = "Transactional";
            public const string BusinessOnline = "Bol";
            public const string BusinessOnlineDesc = "Business Online";
            public const string Cash = "Cash";

        }

        public static class SubRoleType
        {
            public const string TradeBanker = "Trade Banker";
            public const string BOLConsultant = "BOL Consultant";
        }



        public static class ReferenceType
        {
            public const string New = "New";
            public const string Existing = "Existing";
        }

        public static class ConcessionStatus
        {
            public const string Pending = "Pending";
            public const string Approved = "Approved";
            public const string ApprovedWithChanges = "Approved With Changes";
            public const string Declined = "Declined";
            public const string Removed = "Removed";
        }

        public static class ConcessionSubStatus
        {
            public const string NewSubmission = "New submission";
            public const string BcmPending = "BCM Pending";
            public const string BcmApproved = "BCM Approved";
            public const string BcmDeclined = "BCM Declined";
            public const string PcmPending = "PCM Pending";
            public const string PcmApproved = "PCM Approved";
            public const string PcmDeclined = "PCM Declined";
            public const string PcmSnIPending = "PCM S&I Pending";
            public const string PcmSnIApproved = "PCM S&I Approved";
            public const string PcmSnIDeclined = "PCM S&I Declined";
            public const string HoPending = "HO Pending";
            public const string HoApproved = "HO Approved";
            public const string HoDeclined = "HO Declined";
            public const string RequestorAcceptedChanges = "Requestor Accepted Changes";
            public const string RequestorDeclinedChanges = "Requestor Declined Changes";
            public const string PcmApprovedWithChanges = "PCM Approved With Changes";
            public const string HoApprovedWithChanges = "HO Approved With Changes";
        }

        public static class Period
        {
            public const string ThreeMonths = "3 Months";
            public const string SixMonths = "6 Months";
            public const string NineMonths = "9 Months";
            public const string TwelveMonths = "12 Months";
        }

        public static class PeriodType
        {
            public const string Ongoing = "Ongoing";
            public const string Standard = "Once-off";
        }

        public static class ConditionType
        {
            public const string FullTransactionalBanking = "Full Transactional Banking";
            public const string MininumAverageCreditBalance = "Mininum Average Credit Balance";
            public const string MininumTurnover = "Mininum Turnover";
            public const string CreditFacility = "Credit Facility";
        }

        public static class RagStatusResult
        {
            public const string Red = "red";
            public const string Green = "green";
            public const string Yellow = "yellow";
        }

        public static class RelationshipType
        {
            public const string Extension = "Extension";
            public const string Renewal = "Renewal";
            public const string Resubmit = "Resubmit";
            public const string Update = "Update";
        }

        public static class Lending
        {
            public static class ProductType
            {
                public const string Overdraft = "Overdraft";
                public const string TempOverdraft = "Temporary Overdraft";
                public const string VAF = "VAF";
            }
        }

        public static class Transactional
        {
            public static class TransactionType
            {
                public const string ChequeEncashmentFee = "Cheque Encashment Fee";
            }
        }

        public static class SapDataImport
        {
            public const string PricepointId = "PricepointId";
        }

        public static class Investment
        {
            public static class ProductType
            {
                public const string CallDeposit = "Tiered rate call deposit (BCB) ";
                public const string NoticeDeposit = "Notice deposit (BND)";
                public const string RetailCallDeposit = "Retail wholesale call deposit (BCL) ";
            }
        }

        public static class Trade
        {
            public static class TradeProductType
            {
                public const string InwardTT = "Inward TT";
                public const string LocalGuarantee = "Local guarantee";
                public const string OutwardTT = "Outward TT";
            }
        }
    }
}
