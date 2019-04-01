namespace StandardBank.ConcessionManagement.Model.Common
{
    /// <summary>
    /// Cache key constants
    /// </summary>
    public static class CacheKey
    {
        public static class UserInterface
        {
            public static class SiteHelper
            {
                public const string LoggedInUser = "CacheKey.UserInterface.SiteHelper.LoggedInUser";

                public const string UserAccess = "CacheKey.UserInterface.SiteHelper.UserAccess";
            }
        }

        public static class BusinessLogic
        {
            public static class ConcessionManager
            {
                public const string GetUserConcessions = "CacheKey.BusinessLogic.ConcessionManager.GetUserConcessions";
            }
        }

        public static class Repository
        {
            public static class AccrualTypeRepository
            {
                public const string ReadAll = "CacheKey.Repository.AccrualTypeRepository.ReadAll";
            }

            public static class AdValoremRepository
            {
                public const string ReadAll = "CacheKey.Repository.AdValoremRepository.ReadAll";
            }

            public static class ApprovalTypeRepository
            {
                public const string ReadAll = "CacheKey.Repository.ApprovalTypeRepository.ReadAll";
            }

            public static class ApprovalWorkflowRepository
            {
                public const string GetApproversByRoles =
                    "CacheKey.Repository.ApprovalWorkflowRepository.GetApproversByRoles";

                public const string GetRegionApproversByRole =
                    "CacheKey.Repository.ApprovalWorkflowRepository.GetRegionApproversByRole";
            }

            public static class BaseRateRepository
            {
                public const string ReadAll = "CacheKey.Repository.BaseRateRepository.ReadAll";
            }

            public static class ChannelTypeRepository
            {
                public const string ReadAll = "CacheKey.Repository.ChannelTypeRepository.ReadAll";
            }

            public static class ConcessionTypeRepository
            {
                public const string ReadAll = "CacheKey.Repository.ConcessionTypeRepository.ReadAll";
            }

            public static class ConditionProductRepository
            {
                public const string ReadAll = "CacheKey.Repository.ConditionProductRepository.ReadAll";
            }

            public static class ConditionTypeRepository
            {
                public const string ReadAll = "CacheKey.Repository.ConditionTypeRepository.ReadAll";
            }

            public static class ConditionTypeProductRepository
            {
                public const string ReadAll = "CacheKey.Repository.ConditionTypeProductRepository.ReadAll";
            }

            public static class MarketSegmentRepository
            {
                public const string ReadAll = "CacheKey.Repository.MarketSegmentRepository.ReadAll";
            }

            public static class ProductRepository
            {
                public const string ReadAll = "CacheKey.Repository.ProductRepository.ReadAll";
            }

            public static class ReviewFeeTypeRepository
            {
                public const string ReadAll = "CacheKey.Repository.ReviewFeeTypeRepository.ReadAll";
            }

            public static class RoleRepository
            {
                public const string ReadAll = "CacheKey.Repository.RoleRepository.ReadAll";
            }

            public static class StatusRepository
            {
                public const string ReadAll = "CacheKey.Repository.StatusRepository.ReadAll";
            }

            public static class SubStatusRepository
            {
                public const string ReadAll = "CacheKey.Repository.SubStatusRepository.ReadAll";
            }

            public static class TransactionGroupRepository
            {
                public const string ReadAll = "CacheKey.Repository.TransactionGroupRepository.ReadAll";
            }

            public static class TransactionTableNumberRepository
            {
                public const string ReadAll = "CacheKey.Repository.TransactionTableNumberRepository.ReadAll";
            }

            public static class TransactionTypeRepository
            {
                public const string ReadAll = "CacheKey.Repository.TransactionTypeRepository.ReadAll";
            }

            public static class ReferenceTypeRepository
            {
                public const string ReadAll = "CacheKey.Repository.ReferenceTypeRepository.ReadAll";
            }

            public static class RegionRepository
            {
                public const string ReadAll = "CacheKey.Repository.RegionRepository.ReadAll";
            }

            public static class PeriodRepository
            {
                public const string ReadAll = "CacheKey.Repository.PeriodRepository.ReadAll";
            }

            public static class PeriodTypeRepository
            {
                public const string ReadAll = "CacheKey.Repository.PeriodTypeRepository.ReadAll";
            }

            public static class TableNumberRepository
            {
                public const string ReadAll = "CacheKey.Repository.TableNumberRepository.ReadAll";
            }

            public static class RelationshipRepository
            {
                public const string ReadAll = "CacheKey.Repository.RelationshipRepository.ReadAll";
            }

            public static class TransactionalTableNumberRepository
            {
                public const string ReadAll = "CacheKey.Repository.TransactionalTableNumberRepository.ReadAll";
            }

            public static class ChannelTypeImportRepository
            {
                public const string ReadAll = "CacheKey.Repository.ChannelTypeImportRepository.ReadAll";
            }

            public static class ProductImportRepository
            {
                public const string ReadAll = "CacheKey.Repository.ProductImportRepository.ReadAll";
            }

            public static class TransactionTypeImportRepository
            {
                public const string ReadAll = "CacheKey.Repository.TransactionTypeImportRepository.ReadAll";
            }

            public static class RiskGroupRepository
            {
                public const string ReadById = "CacheKey.Repository.RiskGroupRepository.ReadById";
                public const string ReadByIdIsActive = "CacheKey.Repository.RiskGroupRepository.ReadByIdIsActive";

                public const string ReadByRiskGroupNumberIsActive =
                    "CacheKey.Repository.RiskGroupRepository.ReadByRiskGroupNumberIsActive";
            }

            public static class MiscPerformanceRepository
            {
                public const string GetClientAccounts = "CacheKey.Repository.MiscPerformanceRepository.GetClientAccounts";

                public const string GetLendingProducts = "CacheKey.Repository.MiscPerformanceRepository.GetLendingProducts";

                public const string GetCashProducts = "CacheKey.Repository.MiscPerformanceRepository.GetCashProducts";

                public const string GetBolProducts = "CacheKey.Repository.MiscPerformanceRepository.GetBolProducts";

                public const string GetTradeProducts = "CacheKey.Repository.MiscPerformanceRepository.GetTradeProducts";

                public const string GetTransactionalProducts = "CacheKey.Repository.MiscPerformanceRepository.GetTransactionalProducts";

                public const string GetInvestmentProducts = "CacheKey.Repository.MiscPerformanceRepository.GetInvestmentProducts";
            }

            public static class LegalEntityAddressRepository
            {
                public const string ReadAll = "CacheKey.Repository.LegalEntityAddress.ReadAll";
            }
        }
    }
}