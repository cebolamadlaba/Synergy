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
            public static class AdValoremRepository
            {
                public const string ReadAll = "CacheKey.Repository.AdValoremRepository.ReadAll";
            }

            public static class ApprovalTypeRepository
            {
                public const string ReadAll = "CacheKey.Repository.ApprovalTypeRepository.ReadAll";
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

            public static class MarketSegmentRepository
            {
                public const string ReadAll = "CacheKey.Repository.MarketSegmentRepository.ReadAll";
            }

            public static class ProductRepository
            {
                public const string ReadAll = "CacheKey.Repository.ProductRepository.ReadAll";
            }

            public static class ProvinceRepository
            {
                public const string ReadAll = "CacheKey.Repository.ProvinceRepository.ReadAll";
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
        }
    }
}
