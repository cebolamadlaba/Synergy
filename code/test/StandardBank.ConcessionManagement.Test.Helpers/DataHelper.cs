using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Test.Helpers
{
    /// <summary>
    /// Data helper
    /// </summary>
    public static class DataHelper
    {
        /// <summary>
        /// Returns a different date from the one passed in
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ChangeDate(DateTime? value)
        {
            if (!value.HasValue)
                return DateTime.Now;

            return value.Value.AddDays(1);
        }

        /// <summary>
        /// Gets the AdValorem id
        /// </summary>
        /// <returns></returns>
        public static int GetAdValoremId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.AdValoremRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertAdValorem();
        }

        /// <summary>
        /// Inserts a AdValorem and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertAdValorem()
        {
            var model = new AdValorem
            {
                Amount = 6505,
                IsActive = false
            };

            InstantiatedDependencies.AdValoremRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate AdValorem id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateAdValoremId(int? model)
        {
            if (!model.HasValue)
                return GetAdValoremId();

            //read all and return the first one
            var models = InstantiatedDependencies.AdValoremRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertAdValorem();
        }

        /// <summary>
        /// Gets the ApprovalType id
        /// </summary>
        /// <returns></returns>
        public static int GetApprovalTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ApprovalTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertApprovalType();
        }

        /// <summary>
        /// Inserts a ApprovalType and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertApprovalType()
        {
            var model = new ApprovalType
            {
                Description = "a03cbfd2e4",
                IsActive = false
            };

            InstantiatedDependencies.ApprovalTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ApprovalType id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateApprovalTypeId(int? model)
        {
            if (!model.HasValue)
                return GetApprovalTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.ApprovalTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertApprovalType();
        }

        /// <summary>
        /// Gets the BaseRate id
        /// </summary>
        /// <returns></returns>
        public static int GetBaseRateId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.BaseRateRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertBaseRate();
        }

        /// <summary>
        /// Inserts a BaseRate and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertBaseRate()
        {
            var model = new BaseRate
            {
                Amount = 6510,
                IsActive = false
            };

            InstantiatedDependencies.BaseRateRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate BaseRate id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateBaseRateId(int? model)
        {
            if (!model.HasValue)
                return GetBaseRateId();

            //read all and return the first one
            var models = InstantiatedDependencies.BaseRateRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertBaseRate();
        }

        /// <summary>
        /// Gets the ChannelType id
        /// </summary>
        /// <returns></returns>
        public static int GetChannelTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ChannelTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertChannelType();
        }

        /// <summary>
        /// Inserts a ChannelType and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertChannelType()
        {
            var model = new ChannelType
            {
                Description = "d6e6724f8a",
                IsActive = false
            };

            InstantiatedDependencies.ChannelTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ChannelType id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateChannelTypeId(int? model)
        {
            if (!model.HasValue)
                return GetChannelTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.ChannelTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertChannelType();
        }

        /// <summary>
        /// Gets the ConcessionType id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionType();
        }

        /// <summary>
        /// Inserts a ConcessionType and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionType()
        {
            var model = new ConcessionType
            {
                Description = "0377eef177",
                Code = "6e63328f23",
                IsActive = false
            };

            InstantiatedDependencies.ConcessionTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionType id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionTypeId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionType();
        }

        /// <summary>
        /// Gets the ConditionProduct id
        /// </summary>
        /// <returns></returns>
        public static int GetConditionProductId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConditionProductRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConditionProduct();
        }

        /// <summary>
        /// Inserts a ConditionProduct and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConditionProduct()
        {
            var model = new ConditionProduct
            {
                Description = "58f1b54973",
                IsActive = false
            };

            InstantiatedDependencies.ConditionProductRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConditionProduct id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConditionProductId(int? model)
        {
            if (!model.HasValue)
                return GetConditionProductId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConditionProductRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConditionProduct();
        }

        /// <summary>
        /// Gets the ConditionType id
        /// </summary>
        /// <returns></returns>
        public static int GetConditionTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConditionTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConditionType();
        }

        /// <summary>
        /// Inserts a ConditionType and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConditionType()
        {
            var model = new ConditionType
            {
                Description = "202494a7c6",
                IsActive = false
            };

            InstantiatedDependencies.ConditionTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConditionType id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConditionTypeId(int? model)
        {
            if (!model.HasValue)
                return GetConditionTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConditionTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConditionType();
        }

        /// <summary>
        /// Gets the MarketSegment id
        /// </summary>
        /// <returns></returns>
        public static int GetMarketSegmentId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.MarketSegmentRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertMarketSegment();
        }

        /// <summary>
        /// Inserts a MarketSegment and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertMarketSegment()
        {
            var model = new MarketSegment
            {
                Description = "4f97ca5b36",
                IsActive = false
            };

            InstantiatedDependencies.MarketSegmentRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate MarketSegment id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateMarketSegmentId(int? model)
        {
            if (!model.HasValue)
                return GetMarketSegmentId();

            //read all and return the first one
            var models = InstantiatedDependencies.MarketSegmentRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertMarketSegment();
        }

        /// <summary>
        /// Gets the Product id
        /// </summary>
        /// <returns></returns>
        public static int GetProductId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ProductRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertProduct();
        }

        /// <summary>
        /// Inserts a Product and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertProduct()
        {
            var model = new Product
            {
                ConcessionTypeId = GetConcessionTypeId(),
                Description = "d81ef0b458",
                IsActive = false
            };

            InstantiatedDependencies.ProductRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate Product id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateProductId(int? model)
        {
            if (!model.HasValue)
                return GetProductId();

            //read all and return the first one
            var models = InstantiatedDependencies.ProductRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertProduct();
        }

        /// <summary>
        /// Gets the ReviewFeeType id
        /// </summary>
        /// <returns></returns>
        public static int GetReviewFeeTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ReviewFeeTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertReviewFeeType();
        }

        /// <summary>
        /// Inserts a ReviewFeeType and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertReviewFeeType()
        {
            var model = new ReviewFeeType
            {
                Description = "10c2445739",
                IsActive = false
            };

            InstantiatedDependencies.ReviewFeeTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ReviewFeeType id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateReviewFeeTypeId(int? model)
        {
            if (!model.HasValue)
                return GetReviewFeeTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.ReviewFeeTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertReviewFeeType();
        }

        /// <summary>
        /// Gets the Role id
        /// </summary>
        /// <returns></returns>
        public static int GetRoleId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.RoleRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertRole();
        }

        /// <summary>
        /// Inserts a Role and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertRole()
        {
            var model = new Role
            {
                RoleName = "232cafaaa8",
                RoleDescription = "980fa3827c",
                IsActive = false
            };

            InstantiatedDependencies.RoleRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate Role id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateRoleId(int? model)
        {
            if (!model.HasValue)
                return GetRoleId();

            //read all and return the first one
            var models = InstantiatedDependencies.RoleRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertRole();
        }

        /// <summary>
        /// Gets the Status id
        /// </summary>
        /// <returns></returns>
        public static int GetStatusId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.StatusRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertStatus();
        }

        /// <summary>
        /// Inserts a Status and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertStatus()
        {
            var model = new Status
            {
                Description = "31c7caa3ae",
                IsActive = false
            };

            InstantiatedDependencies.StatusRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate Status id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateStatusId(int? model)
        {
            if (!model.HasValue)
                return GetStatusId();

            //read all and return the first one
            var models = InstantiatedDependencies.StatusRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertStatus();
        }

        /// <summary>
        /// Gets the SubStatus id
        /// </summary>
        /// <returns></returns>
        public static int GetSubStatusId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.SubStatusRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertSubStatus();
        }

        /// <summary>
        /// Inserts a SubStatus and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertSubStatus()
        {
            var model = new SubStatus
            {
                Description = "5903516faf",
                IsActive = false
            };

            InstantiatedDependencies.SubStatusRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate SubStatus id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateSubStatusId(int? model)
        {
            if (!model.HasValue)
                return GetSubStatusId();

            //read all and return the first one
            var models = InstantiatedDependencies.SubStatusRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertSubStatus();
        }

        /// <summary>
        /// Gets the TransactionGroup id
        /// </summary>
        /// <returns></returns>
        public static int GetTransactionGroupId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.TransactionGroupRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertTransactionGroup();
        }

        /// <summary>
        /// Inserts a TransactionGroup and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertTransactionGroup()
        {
            var model = new TransactionGroup
            {
                Description = "9f13d87162",
                IsActive = false
            };

            InstantiatedDependencies.TransactionGroupRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate TransactionGroup id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateTransactionGroupId(int? model)
        {
            if (!model.HasValue)
                return GetTransactionGroupId();

            //read all and return the first one
            var models = InstantiatedDependencies.TransactionGroupRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertTransactionGroup();
        }

        /// <summary>
        /// Gets the TransactionType id
        /// </summary>
        /// <returns></returns>
        public static int GetTransactionTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.TransactionTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertTransactionType();
        }

        /// <summary>
        /// Inserts a TransactionType and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertTransactionType()
        {
            var model = new TransactionType
            {
                ConcessionTypeId = GetConcessionTypeId(),
                Description = "14c6862dfb",
                IsActive = false
            };

            InstantiatedDependencies.TransactionTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate TransactionType id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateTransactionTypeId(int? model)
        {
            if (!model.HasValue)
                return GetTransactionTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.TransactionTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertTransactionType();
        }

        /// <summary>
        /// Gets the ReferenceType id
        /// </summary>
        /// <returns></returns>
        public static int GetReferenceTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ReferenceTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertReferenceType();
        }

        /// <summary>
        /// Inserts a ReferenceType and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertReferenceType()
        {
            var model = new ReferenceType
            {
                Description = "739bf7c260",
                IsActive = false
            };

            InstantiatedDependencies.ReferenceTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ReferenceType id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateReferenceTypeId(int? model)
        {
            if (!model.HasValue)
                return GetReferenceTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.ReferenceTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertReferenceType();
        }

        /// <summary>
        /// Gets the BolUser id
        /// </summary>
        /// <returns></returns>
        public static int GetBolUserId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.BolUserRepository.GetLegalEntityBOLUsers(0);

            if (models != null && models.Any())
                return models.First().pkLegalEntityBOLUserId;

            return InsertBolUser();
        }

        /// <summary>
        /// Inserts a BolUser and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertBolUser()
        {
            var model = new BolUser
            {
                UserName = "ef77f24d0f",
                IsActive = false
            };

           // InstantiatedDependencies.BolUserRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate BolUser id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateBolUserId(int? model)
        {
            if (!model.HasValue)
                return GetBolUserId();

            //read all and return the first one
            var models = InstantiatedDependencies.BolUserRepository.GetLegalEntityBOLUsers(0);

            if (models != null && models.Any(_ => _.pkLegalEntityBOLUserId != model.Value))
                return models.First(_ => _.pkLegalEntityBOLUserId != model.Value).pkLegalEntityBOLUserId;

            return InsertBolUser();
        }

        /// <summary>
        /// Gets the BusinesOnlineTransactionType id
        /// </summary>
        /// <returns></returns>
        public static int GetBusinesOnlineTransactionTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertBusinesOnlineTransactionType();
        }

        /// <summary>
        /// Inserts a BusinesOnlineTransactionType and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertBusinesOnlineTransactionType()
        {
            var model = new BusinesOnlineTransactionType
            {
                TransactionGroupId = GetTransactionGroupId(),
                Description = "6b7c7b0e73",
                IsActive = false
            };

            InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate BusinesOnlineTransactionType id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateBusinesOnlineTransactionTypeId(int? model)
        {
            if (!model.HasValue)
                return GetBusinesOnlineTransactionTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertBusinesOnlineTransactionType();
        }

        /// <summary>
        /// Gets the Centre id
        /// </summary>
        /// <returns></returns>
        public static int GetCentreId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.CentreRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertCentre();
        }

        /// <summary>
        /// Inserts a Centre and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertCentre()
        {
            var model = new Centre
            {
                RegionId = GetRegionId(),
                CentreName = "37bde50be7",
                IsActive = false
            };

            InstantiatedDependencies.CentreRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate Centre id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateCentreId(int? model)
        {
            if (!model.HasValue)
                return GetCentreId();

            //read all and return the first one
            var models = InstantiatedDependencies.CentreRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertCentre();
        }

        /// <summary>
        /// Gets the CentreBusinessManager id
        /// </summary>
        /// <returns></returns>
        public static int GetCentreBusinessManagerId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.CentreBusinessManagerRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertCentreBusinessManager();
        }

        /// <summary>
        /// Inserts a CentreBusinessManager and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertCentreBusinessManager()
        {
            var model = new CentreBusinessManager
            {
                CentreId = GetCentreId(),
                UserId = GetUserId(),
                IsActive = false
            };

            InstantiatedDependencies.CentreBusinessManagerRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate CentreBusinessManager id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateCentreBusinessManagerId(int? model)
        {
            if (!model.HasValue)
                return GetCentreBusinessManagerId();

            //read all and return the first one
            var models = InstantiatedDependencies.CentreBusinessManagerRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertCentreBusinessManager();
        }

        /// <summary>
        /// Gets the CentreUser id
        /// </summary>
        /// <returns></returns>
        public static int GetCentreUserId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.CentreUserRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertCentreUser();
        }

        /// <summary>
        /// Inserts a CentreUser and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertCentreUser()
        {
            var model = new CentreUser
            {
                CentreId = GetCentreId(),
                UserId = GetUserId(),
                IsActive = false
            };

            InstantiatedDependencies.CentreUserRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate CentreUser id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateCentreUserId(int? model)
        {
            if (!model.HasValue)
                return GetCentreUserId();

            //read all and return the first one
            var models = InstantiatedDependencies.CentreUserRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertCentreUser();
        }

        /// <summary>
        /// Gets the ChannelTypeBaseRate id
        /// </summary>
        /// <returns></returns>
        public static int GetChannelTypeBaseRateId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ChannelTypeBaseRateRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertChannelTypeBaseRate();
        }

        /// <summary>
        /// Inserts a ChannelTypeBaseRate and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertChannelTypeBaseRate()
        {
            var model = new ChannelTypeBaseRate
            {
                ChannelTypeId = GetChannelTypeId(),
                BaseRateId = GetBaseRateId()
            };

            InstantiatedDependencies.ChannelTypeBaseRateRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ChannelTypeBaseRate id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateChannelTypeBaseRateId(int? model)
        {
            if (!model.HasValue)
                return GetChannelTypeBaseRateId();

            //read all and return the first one
            var models = InstantiatedDependencies.ChannelTypeBaseRateRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertChannelTypeBaseRate();
        }

        /// <summary>
        /// Gets the Concession id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcession();
        }

        /// <summary>
        /// Inserts a Concession and returns the id
        /// </summary>
        /// <returns></returns>
        public static int InsertConcession()
        {
            var model = new Concession
            {
                TypeId = GetReferenceTypeId(),
                ConcessionRef = "e552f7ceaf",
                ConcessionTypeId = GetConcessionTypeId(),
                SMTDealNumber = "b708e70719",
                StatusId = GetStatusId(),
                SubStatusId = GetSubStatusId(),
                ConcessionDate = DateTime.Now,
                DatesentForApproval = DateTime.Now,
                Motivation = "9007645562",
                AAUserId = GetUserId(),
                RequestorId = GetUserId(),
                BCMUserId = GetUserId(),
                DateActionedByBCM = DateTime.Now,
                PCMUserId = GetUserId(),
                DateActionedByPCM = DateTime.Now,
                HOUserId = GetUserId(),
                DateActionedByHO = DateTime.Now,
                CentreId = GetCentreId(),
                IsCurrent = false,
                IsActive = false,
                MRS_CRS = 4354,
                RegionId = GetRegionId(),
                RiskGroupId = GetRiskGroupId()
            };

            InstantiatedDependencies.ConcessionRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate Concession id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcession();
        }

        /// <summary>
        /// Gets the ConcessionAccount id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionAccountId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionAccountRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionAccount();
        }

        /// <summary>
        /// Inserts a ConcessionAccount and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionAccount()
        {
            var model = new ConcessionAccount
            {
                ConcessionId = GetConcessionId(),
                AccountNumber = "5ce7c1a3b4",
                IsActive = false
            };

            InstantiatedDependencies.ConcessionAccountRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionAccount id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionAccountId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionAccountId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionAccountRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionAccount();
        }

        /// <summary>
        /// Gets the ConcessionApproval id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionApprovalId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionApprovalRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionApproval();
        }

        /// <summary>
        /// Inserts a ConcessionApproval and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionApproval()
        {
            var model = new ConcessionApproval
            {
                ConcessionId = GetConcessionId(),
                OldSubStatusId = GetSubStatusId(),
                NewSubStatusId = GetSubStatusId(),
                UserId = GetUserId(),
                SystemDate = DateTime.Now,
                IsActive = false
            };

            InstantiatedDependencies.ConcessionApprovalRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionApproval id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionApprovalId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionApprovalId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionApprovalRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionApproval();
        }

        /// <summary>
        /// Gets the ConcessionBol id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionBolId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionBolRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionBol();
        }

        /// <summary>
        /// Inserts a ConcessionBol and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionBol()
        {
            var model = new ConcessionBol
            {
                ConcessionId = GetConcessionId()
               
            };

            InstantiatedDependencies.ConcessionBolRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionBol id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionBolId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionBolId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionBolRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionBol();
        }

        /// <summary>
        /// Gets the ConcessionCash id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionCashId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionCashRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionCash();
        }

        /// <summary>
        /// Inserts a ConcessionCash and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionCash()
        {
            var tableNumberId = GetTableNumberId();

            var model = new ConcessionCash
            {
                ConcessionId = GetConcessionId(),
                ChannelTypeId = GetChannelTypeId(),
                AdValorem = 8669,
                BaseRate = 1263,
                AccrualTypeId = GetAccrualTypeId(),
                TableNumberId = tableNumberId,
                ApprovedTableNumberId = tableNumberId,
                LoadedTableNumberId = tableNumberId
            };

            InstantiatedDependencies.ConcessionCashRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionCash id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionCashId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionCashId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionCashRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionCash();
        }

        /// <summary>
        /// Gets the ConcessionComment id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionCommentId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionCommentRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionComment();
        }

        /// <summary>
        /// Inserts a ConcessionComment and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionComment()
        {
            var model = new ConcessionComment
            {
                ConcessionId = GetConcessionId(),
                UserId = GetUserId(),
                ConcessionSubStatusId = GetSubStatusId(),
                Comment = "b687fdfee5",
                SystemDate = DateTime.Now,
                IsActive = false
            };

            InstantiatedDependencies.ConcessionCommentRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionComment id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionCommentId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionCommentId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionCommentRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionComment();
        }

        /// <summary>
        /// Gets the ConcessionCondition id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionConditionId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionConditionRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionCondition();
        }

        /// <summary>
        /// Inserts a ConcessionCondition and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionCondition()
        {
            var model = new ConcessionCondition
            {
                ConcessionId = GetConcessionId(),
                ConditionTypeId = GetConditionTypeId(),
                ConditionProductId = GetConditionProductId(),
                InterestRate = 507,
                Volume = 8,
                Value = 2525,
                IsActive = false,
                PeriodTypeId =  GetPeriodTypeId(),
                PeriodId = GetPeriodId(),
                ExpiryDate = DateTime.Now.AddDays(100)
            };

            InstantiatedDependencies.ConcessionConditionRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionCondition id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionConditionId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionConditionId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionConditionRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionCondition();
        }

        /// <summary>
        /// Gets the ConcessionInvestment id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionInvestmentId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionInvestmentRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionInvestment();
        }

        /// <summary>
        /// Inserts a ConcessionInvestment and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionInvestment()
        {
            var model = new ConcessionInvestment
            {
                ConcessionId = GetConcessionId(),
                ProductTypeId = GetProductId(),
                Balance = 163,
                Term = 3
               
            };

            InstantiatedDependencies.ConcessionInvestmentRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionInvestment id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionInvestmentId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionInvestmentId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionInvestmentRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionInvestment();
        }

        /// <summary>
        /// Gets the ConcessionLending id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionLendingId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionLendingRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionLending();
        }

        /// <summary>
        /// Inserts a ConcessionLending and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionLending()
        {
            var model = new ConcessionLending
            {
                ConcessionId = GetConcessionId(),
                ProductTypeId = GetProductId(),
                Limit = 5337,
                Term = 5,
                MarginToPrime = 6756,
                ApprovedMarginToPrime = 9876,
                InitiationFee = 3112,
                ReviewFee = 7441,
                UFFFee = 1095,
                ReviewFeeTypeId = GetReviewFeeTypeId(),
                AverageBalance = 234.23m,
                LoadedMarginToPrime = 2134
            };

            InstantiatedDependencies.ConcessionLendingRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionLending id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionLendingId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionLendingId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionLendingRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionLending();
        }

        /// <summary>
        /// Gets the ConcessionMas id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionMasId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionMasRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionMas();
        }

        /// <summary>
        /// Inserts a ConcessionMas and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionMas()
        {
            var model = new ConcessionMas
            {
                ConcessionId = GetConcessionId(),
                TransactionTypeId = GetTransactionTypeId(),
                MerchantNumber = "83e024fd85",
                Turnover = 9752,
                CommissionRate = 2366
            };

            InstantiatedDependencies.ConcessionMasRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionMas id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionMasId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionMasId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionMasRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionMas();
        }

        /// <summary>
        /// Gets the ConcessionTrade id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionTradeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionTradeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionTrade();
        }

        /// <summary>
        /// Inserts a ConcessionTrade and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionTrade()
        {
            var model = new ConcessionTrade
            {
                //ConcessionId = GetConcessionId(),
                //TransactionTypeId = GetTransactionTypeId(),
                //ChannelTypeId = GetChannelTypeId(),
                //TableNumber = 2,
                //TransactionVolume = 6,
                //TransactionValue = 9451,
                //BaseRateId = GetBaseRateId(),
                //AdValorem = 4407
            };

            InstantiatedDependencies.ConcessionTradeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionTrade id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionTradeId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionTradeId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionTradeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionTrade();
        }

        /// <summary>
        /// Gets the ConcessionTransactional id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionTransactionalId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionTransactionalRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionTransactional();
        }

        /// <summary>
        /// Inserts a ConcessionTransactional and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionTransactional()
        {
            var tableNumberId = GetTransactionTableNumberId();

            var model = new ConcessionTransactional
            {
                ConcessionId = GetConcessionId(),
                TransactionTypeId = GetTransactionTypeId(),
                AdValorem = 5344,
                Fee = 8369,
                TransactionTableNumberId = tableNumberId,
                ApprovedTransactionTableNumberId = tableNumberId,
                LoadedTransactionTableNumberId = tableNumberId
            };

            InstantiatedDependencies.ConcessionTransactionalRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionTransactional id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionTransactionalId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionTransactionalId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionTransactionalRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionTransactional();
        }

        /// <summary>
        /// Gets the ConditionTypeProduct id
        /// </summary>
        /// <returns></returns>
        public static int GetConditionTypeProductId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConditionTypeProductRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConditionTypeProduct();
        }

        /// <summary>
        /// Inserts a ConditionTypeProduct and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConditionTypeProduct()
        {
            var model = new ConditionTypeProduct
            {
                ConditionTypeId = GetConditionTypeId(),
                ConditionProductId = GetConditionProductId(),
                IsActive = false
            };

            InstantiatedDependencies.ConditionTypeProductRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConditionTypeProduct id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConditionTypeProductId(int? model)
        {
            if (!model.HasValue)
                return GetConditionTypeProductId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConditionTypeProductRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConditionTypeProduct();
        }

        /// <summary>
        /// Gets the LegalEntity id
        /// </summary>
        /// <returns></returns>
        public static int GetLegalEntityId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.LegalEntityRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertLegalEntity();
        }

        /// <summary>
        /// Inserts a LegalEntity and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertLegalEntity()
        {
            var model = new LegalEntity
            {
                MarketSegmentId = GetMarketSegmentId(),
                RiskGroupId = GetRiskGroupId(),
                CustomerName = "969efde63e",
                CustomerNumber = "b925857761",
                IsActive = false,
                City = "Pretoria",
                ContactPerson = "George",
                PostalAddress = "555 Republic Street",
                PostalCode = "9999"
            };

            InstantiatedDependencies.LegalEntityRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate LegalEntity id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateLegalEntityId(int? model)
        {
            if (!model.HasValue)
                return GetLegalEntityId();

            //read all and return the first one
            var models = InstantiatedDependencies.LegalEntityRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertLegalEntity();
        }

        /// <summary>
        /// Gets the LegalEntityAccount id
        /// </summary>
        /// <returns></returns>
        public static int GetLegalEntityAccountId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.LegalEntityAccountRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertLegalEntityAccount();
        }

        /// <summary>
        /// Inserts a LegalEntityAccount and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertLegalEntityAccount()
        {
            var model = new LegalEntityAccount
            {
                LegalEntityId = GetLegalEntityId(),
                AccountNumber = "00f6d77999",
                IsActive = false
            };

            InstantiatedDependencies.LegalEntityAccountRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate LegalEntityAccount id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateLegalEntityAccountId(int? model)
        {
            if (!model.HasValue)
                return GetLegalEntityAccountId();

            //read all and return the first one
            var models = InstantiatedDependencies.LegalEntityAccountRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertLegalEntityAccount();
        }

        /// <summary>
        /// Gets the RiskGroup id
        /// </summary>
        /// <returns></returns>
        public static int GetRiskGroupId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.RiskGroupRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertRiskGroup();
        }

        /// <summary>
        /// Inserts a RiskGroup and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertRiskGroup()
        {
            var model = new RiskGroup
            {
                RiskGroupNumber = 1,
                RiskGroupName = "f48353dfd5",
                IsActive = false,
                MarketSegmentId = GetMarketSegmentId()
            };

            InstantiatedDependencies.RiskGroupRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate RiskGroup id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateRiskGroupId(int? model)
        {
            if (!model.HasValue)
                return GetRiskGroupId();

            //read all and return the first one
            var models = InstantiatedDependencies.RiskGroupRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertRiskGroup();
        }

        /// <summary>
        /// Gets the ScenarioManagerToolDeal id
        /// </summary>
        /// <returns></returns>
        public static int GetScenarioManagerToolDealId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ScenarioManagerToolDealRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertScenarioManagerToolDeal();
        }

        /// <summary>
        /// Inserts a ScenarioManagerToolDeal and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertScenarioManagerToolDeal()
        {
            var model = new ScenarioManagerToolDeal
            {
                DealNumber = "a0e533846b",
                IsActive = false
            };

            InstantiatedDependencies.ScenarioManagerToolDealRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ScenarioManagerToolDeal id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateScenarioManagerToolDealId(int? model)
        {
            if (!model.HasValue)
                return GetScenarioManagerToolDealId();

            //read all and return the first one
            var models = InstantiatedDependencies.ScenarioManagerToolDealRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertScenarioManagerToolDeal();
        }

        /// <summary>
        /// Gets the User id
        /// </summary>
        /// <returns></returns>
        public static int GetUserId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.UserRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertUser();
        }

        /// <summary>
        /// Inserts a User and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertUser()
        {
            var model = new User
            {
                ANumber = "a7ae886e47",
                EmailAddress = "0a4a104423",
                FirstName = "e11ee71428",
                Surname = "f20ac7c952",
                IsActive = false,
                ContactNumber = "011 555 1234",
                CanApprove = true
            };

            InstantiatedDependencies.UserRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate User id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateUserId(int? model)
        {
            if (!model.HasValue)
                return GetUserId();

            //read all and return the first one
            var models = InstantiatedDependencies.UserRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertUser();
        }

        /// <summary>
        /// Gets the UserRole id
        /// </summary>
        /// <returns></returns>
        public static int GetUserRoleId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.UserRoleRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertUserRole();
        }

        /// <summary>
        /// Inserts a UserRole and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertUserRole()
        {
            var model = new UserRole
            {
                UserId = GetUserId(),
                RoleId = GetRoleId(),
                IsActive = false
            };

            InstantiatedDependencies.UserRoleRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate UserRole id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateUserRoleId(int? model)
        {
            if (!model.HasValue)
                return GetUserRoleId();

            //read all and return the first one
            var models = InstantiatedDependencies.UserRoleRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertUserRole();
        }

        /// <summary>
        /// Gets the Region id
        /// </summary>
        /// <returns></returns>
        public static int GetRegionId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.RegionRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertRegion();
        }

        /// <summary>
        /// Inserts a Region and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertRegion()
        {
            var model = new Region
            {
                Description = "19901ab209",
                IsActive = false
            };

            InstantiatedDependencies.RegionRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate Region id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateRegionId(int? model)
        {
            if (!model.HasValue)
                return GetRegionId();

            //read all and return the first one
            var models = InstantiatedDependencies.RegionRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertRegion();
        }

        /// <summary>
        /// Gets the Period id
        /// </summary>
        /// <returns></returns>
        public static int GetPeriodId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.PeriodRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertPeriod();
        }

        /// <summary>
        /// Inserts a Period and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertPeriod()
        {
            var model = new Period
            {
                Description = "e4c25acce2",
                IsActive = false
            };

            InstantiatedDependencies.PeriodRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate Period id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternatePeriodId(int? model)
        {
            if (!model.HasValue)
                return GetPeriodId();

            //read all and return the first one
            var models = InstantiatedDependencies.PeriodRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertPeriod();
        }

        /// <summary>
        /// Gets the PeriodType id
        /// </summary>
        /// <returns></returns>
        public static int GetPeriodTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.PeriodTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertPeriodType();
        }

        /// <summary>
        /// Inserts a PeriodType and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertPeriodType()
        {
            var model = new PeriodType
            {
                Description = "a71b3deb80",
                IsActive = false
            };

            InstantiatedDependencies.PeriodTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate PeriodType id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternatePeriodTypeId(int? model)
        {
            if (!model.HasValue)
                return GetPeriodTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.PeriodTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertPeriodType();
        }

        /// <summary>
        /// Gets the AccrualType id
        /// </summary>
        /// <returns></returns>
        public static int GetAccrualTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.AccrualTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertAccrualType();
        }

        /// <summary>
        /// Inserts a AccrualType and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertAccrualType()
        {
            var model = new AccrualType
            {
                Description = "47d720c8a6",
                IsActive = false
            };

            InstantiatedDependencies.AccrualTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate AccrualType id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateAccrualTypeId(int? model)
        {
            if (!model.HasValue)
                return GetAccrualTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.AccrualTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertAccrualType();
        }

        /// <summary>
        /// Gets the TableNumber id
        /// </summary>
        /// <returns></returns>
        public static int GetTableNumberId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.TableNumberRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertTableNumber();
        }

        /// <summary>
        /// Inserts a TableNumber and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertTableNumber()
        {
            var model = new TableNumber
            {
                TariffTable = 9,
                AdValorem = 1321,
                BaseRate = 6770,
                IsActive = false,
                ConcessionTypeId = GetConcessionTypeId()
            };

            InstantiatedDependencies.TableNumberRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate TableNumber id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateTableNumberId(int? model)
        {
            if (!model.HasValue)
                return GetTableNumberId();

            //read all and return the first one
            var models = InstantiatedDependencies.TableNumberRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertTableNumber();
        }

        /// <summary>
        /// Gets the Relationship id
        /// </summary>
        /// <returns></returns>
        public static int GetRelationshipId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.RelationshipRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertRelationship();
        }

        /// <summary>
        /// Inserts a Relationship and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertRelationship()
        {
            var model = new Relationship
            {
                Description = "455fcbf184",
                IsActive = false
            };

            InstantiatedDependencies.RelationshipRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate Relationship id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateRelationshipId(int? model)
        {
            if (!model.HasValue)
                return GetRelationshipId();

            //read all and return the first one
            var models = InstantiatedDependencies.RelationshipRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertRelationship();
        }

        /// <summary>
        /// Gets the ConcessionRelationship id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionRelationshipId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionRelationshipRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertConcessionRelationship();
        }

        /// <summary>
        /// Inserts a ConcessionRelationship and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionRelationship()
        {
            var concessionId = GetConcessionId();

            var model = new ConcessionRelationship
            {
                ParentConcessionId = concessionId,
                ChildConcessionId = GetAlternateConcessionId(concessionId),
                RelationshipId = GetRelationshipId(),
                CreationDate = DateTime.Now,
                UserId = GetUserId()
            };

            InstantiatedDependencies.ConcessionRelationshipRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ConcessionRelationship id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionRelationshipId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionRelationshipId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionRelationshipRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertConcessionRelationship();
        }

        /// <summary>
        /// Gets the ProductLending id
        /// </summary>
        /// <returns></returns>
        public static int GetProductLendingId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ProductLendingRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertProductLending();
        }

        /// <summary>
        /// Inserts a ProductLending and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertProductLending()
        {
            var model = new ProductLending
            {
                RiskGroupId = GetRiskGroupId(),
                LegalEntityId = GetLegalEntityId(),
                LegalEntityAccountId = GetLegalEntityAccountId(),
                ProductId = GetProductId(),
                Limit = 7330,
                AverageBalance = 1973,
                LoadedMap = 7893
            };

            InstantiatedDependencies.ProductLendingRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ProductLending id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateProductLendingId(int? model)
        {
            if (!model.HasValue)
                return GetProductLendingId();

            //read all and return the first one
            var models = InstantiatedDependencies.ProductLendingRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertProductLending();
        }

        /// <summary>
        /// Gets the FinancialLending id
        /// </summary>
        /// <returns></returns>
        public static int GetFinancialLendingId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.FinancialLendingRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertFinancialLending();
        }

        /// <summary>
        /// Inserts a FinancialLending and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertFinancialLending()
        {
            var model = new FinancialLending
            {
                RiskGroupId = GetRiskGroupId(),
                TotalExposure = 6686,
                WeightedAverageMap = 2282,
                WeightedCrsOrMrs = 4607,
                LatestCrsOrMrs = 123.34m
            };

            InstantiatedDependencies.FinancialLendingRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate FinancialLending id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateFinancialLendingId(int? model)
        {
            if (!model.HasValue)
                return GetFinancialLendingId();

            //read all and return the first one
            var models = InstantiatedDependencies.FinancialLendingRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertFinancialLending();
        }

        /// <summary>
        /// Gets the FinancialCash id
        /// </summary>
        /// <returns></returns>
        public static int GetFinancialCashId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.FinancialCashRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertFinancialCash();
        }

        /// <summary>
        /// Inserts a FinancialCash and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertFinancialCash()
        {
            var model = new FinancialCash
            {
                RiskGroupId = GetRiskGroupId(),
                WeightedAverageBranchPrice = 3429,
                TotalCashCentrCashTurnover = 8149,
                TotalCashCentrCashVolume = 5721,
                TotalBranchCashTurnover = 7316,
                TotalBranchCashVolume = 2487,
                TotalAutosafeCashTurnover = 8111,
                TotalAutosafeCashVolume = 5266,
                WeightedAverageCCPrice = 7745,
                WeightedAverageAFPrice = 2965,
                LatestCrsOrMrs = 2385
            };

            InstantiatedDependencies.FinancialCashRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate FinancialCash id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateFinancialCashId(int? model)
        {
            if (!model.HasValue)
                return GetFinancialCashId();

            //read all and return the first one
            var models = InstantiatedDependencies.FinancialCashRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertFinancialCash();
        }

        /// <summary>
        /// Gets the ProductCash id
        /// </summary>
        /// <returns></returns>
        public static int GetProductCashId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ProductCashRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertProductCash();
        }

        /// <summary>
        /// Inserts a ProductCash and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertProductCash()
        {
            var model = new ProductCash
            {
                RiskGroupId = GetRiskGroupId(),
                LegalEntityId = GetLegalEntityId(),
                LegalEntityAccountId = GetLegalEntityAccountId(),
                TableNumberId = GetTableNumberId(),
                Channel = "9deb561718",
                BpId = 1,
                Volume = 3364,
                Value = 3187,
                LoadedPrice = "2405"
            };

            InstantiatedDependencies.ProductCashRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ProductCash id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateProductCashId(int? model)
        {
            if (!model.HasValue)
                return GetProductCashId();

            //read all and return the first one
            var models = InstantiatedDependencies.ProductCashRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertProductCash();
        }

        /// <summary>
        /// Gets the FinancialTransactional id
        /// </summary>
        /// <returns></returns>
        public static int GetFinancialTransactionalId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.FinancialTransactionalRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertFinancialTransactional();
        }

        /// <summary>
        /// Inserts a FinancialTransactional and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertFinancialTransactional()
        {
            var model = new FinancialTransactional
            {
                RiskGroupId = GetRiskGroupId(),
                TotalNumberOfAccounts = 6331,
                AverageAccountManagementFee = 769,
                AverageMinimumMonthlyFee = 4508,
                TotalChequeIssuingVolumes = 9016,
                TotalChequeDepositVolumes = 8481,
                TotalChequeEncashmentVolumes = 2864,
                TotalChequeEncashmentValues = 5629,
                TotalCashWithdrawalVolumes = 2723,
                TotalCashWithdrawalValues = 9746,
                AverageChequeIssuingValue = 6026,
                AverageChequeIssuingPrice = 5665,
                AverageChequeDepositValue = 4032,
                AverageChequeDepositPrice = 9247,
                AverageChequeEncashmentPrice = 2680,
                AverageCashWithdrawalPrice = 5134
            };

            InstantiatedDependencies.FinancialTransactionalRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate FinancialTransactional id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateFinancialTransactionalId(int? model)
        {
            if (!model.HasValue)
                return GetFinancialTransactionalId();

            //read all and return the first one
            var models = InstantiatedDependencies.FinancialTransactionalRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertFinancialTransactional();
        }

        /// <summary>
        /// Gets the ProductTransactional id
        /// </summary>
        /// <returns></returns>
        public static int GetProductTransactionalId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ProductTransactionalRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertProductTransactional();
        }

        /// <summary>
        /// Inserts a ProductTransactional and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertProductTransactional()
        {
            var model = new ProductTransactional
            {
                RiskGroupId = GetRiskGroupId(),
                LegalEntityId = GetLegalEntityId(),
                LegalEntityAccountId = GetLegalEntityAccountId(),
                TransactionTableNumberId = GetTransactionTableNumberId(),
                TransactionTypeId = GetTransactionTypeId(),
                Volume = 845,
                Value = 711,
                LoadedPrice = "b8dcd15e0b"
            };

            InstantiatedDependencies.ProductTransactionalRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ProductTransactional id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateProductTransactionalId(int? model)
        {
            if (!model.HasValue)
                return GetProductTransactionalId();

            //read all and return the first one
            var models = InstantiatedDependencies.ProductTransactionalRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertProductTransactional();
        }

        /// <summary>
        /// Gets the LoadedPriceCash id
        /// </summary>
        /// <returns></returns>
        public static int GetLoadedPriceCashId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.LoadedPriceCashRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertLoadedPriceCash();
        }

        /// <summary>
        /// Inserts a LoadedPriceCash and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertLoadedPriceCash()
        {
            var model = new LoadedPriceCash
            {
                ChannelTypeId = GetChannelTypeId(),
                LegalEntityAccountId = GetLegalEntityAccountId(),
                TableNumberId = GetTableNumberId()
            };

            InstantiatedDependencies.LoadedPriceCashRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate LoadedPriceCash id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateLoadedPriceCashId(int? model)
        {
            if (!model.HasValue)
                return GetLoadedPriceCashId();

            //read all and return the first one
            var models = InstantiatedDependencies.LoadedPriceCashRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertLoadedPriceCash();
        }

        /// <summary>
        /// Gets the LoadedPriceLending id
        /// </summary>
        /// <returns></returns>
        public static int GetLoadedPriceLendingId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.LoadedPriceLendingRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertLoadedPriceLending();
        }

        /// <summary>
        /// Inserts a LoadedPriceLending and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertLoadedPriceLending()
        {
            var model = new LoadedPriceLending
            {
                ProductTypeId = GetProductId(),
                LegalEntityAccountId = GetLegalEntityAccountId(),
                MarginToPrime = 2382
            };

            InstantiatedDependencies.LoadedPriceLendingRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate LoadedPriceLending id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateLoadedPriceLendingId(int? model)
        {
            if (!model.HasValue)
                return GetLoadedPriceLendingId();

            //read all and return the first one
            var models = InstantiatedDependencies.LoadedPriceLendingRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertLoadedPriceLending();
        }

        /// <summary>
        /// Gets the LoadedPriceTransactional id
        /// </summary>
        /// <returns></returns>
        public static int GetLoadedPriceTransactionalId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.LoadedPriceTransactionalRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertLoadedPriceTransactional();
        }

        /// <summary>
        /// Inserts a LoadedPriceTransactional and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertLoadedPriceTransactional()
        {
            var model = new LoadedPriceTransactional
            {
                TransactionTypeId = GetTransactionTypeId(),
                LegalEntityAccountId = GetLegalEntityAccountId(),
                TransactionTableNumberId = GetTransactionTableNumberId()
            };

            InstantiatedDependencies.LoadedPriceTransactionalRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate LoadedPriceTransactional id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateLoadedPriceTransactionalId(int? model)
        {
            if (!model.HasValue)
                return GetLoadedPriceTransactionalId();

            //read all and return the first one
            var models = InstantiatedDependencies.LoadedPriceTransactionalRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertLoadedPriceTransactional();
        }

        /// <summary>
        /// Gets the ConcessionDetail id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionDetailId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionDetailRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().ConcessionDetailId;

            return InsertConcessionDetail();
        }

        /// <summary>
        /// Inserts a ConcessionDetail and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionDetail()
        {
            var model = new ConcessionDetail
            {
                ConcessionId = GetConcessionId(),
                LegalEntityId = GetLegalEntityId(),
                LegalEntityAccountId = GetLegalEntityAccountId(),
                ExpiryDate = DateTime.Now,
                DateApproved = DateTime.Now,
                IsMismatched = false,
                PriceExported = false,
                PriceExportedDate = DateTime.Now
            };

            InstantiatedDependencies.ConcessionDetailRepository.Create(model);

            return model.ConcessionDetailId;
        }

        /// <summary>
        /// Gets the alternate ConcessionDetail id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionDetailId(int? model)
        {
            if (!model.HasValue)
                return GetConcessionDetailId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionDetailRepository.ReadAll();

            if (models != null && models.Any(_ => _.ConcessionDetailId != model.Value))
                return models.First(_ => _.ConcessionDetailId != model.Value).ConcessionDetailId;

            return InsertConcessionDetail();
        }

        /// <summary>
        /// Gets the FinancialInvestment id
        /// </summary>
        /// <returns></returns>
        public static int GetFinancialInvestmentId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.FinancialInvestmentRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertFinancialInvestment();
        }

        /// <summary>
        /// Inserts a FinancialInvestment and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertFinancialInvestment()
        {
            var model = new FinancialInvestment
            {
                RiskGroupId = GetRiskGroupId(),
                TotalLiabilityBalances = 7011,
                WeightedAverageMTP = 5813,
                WeightedAverageNetMargin = 584,
                LatestCrsOrMrs = 2400
            };

            InstantiatedDependencies.FinancialInvestmentRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate FinancialInvestment id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateFinancialInvestmentId(int? model)
        {
            if (!model.HasValue)
                return GetFinancialInvestmentId();

            //read all and return the first one
            var models = InstantiatedDependencies.FinancialInvestmentRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertFinancialInvestment();
        }

        /// <summary>
        /// Gets the ProductInvestment id
        /// </summary>
        /// <returns></returns>
        public static int GetProductInvestmentId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ProductInvestmentRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertProductInvestment();
        }

        /// <summary>
        /// Inserts a ProductInvestment and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertProductInvestment()
        {
            var model = new ProductInvestment
            {
                RiskGroupId = GetRiskGroupId(),
                LegalEntityId = GetLegalEntityId(),
                LegalEntityAccountId = GetLegalEntityAccountId(),
                ProductId = GetProductId(),
                AverageBalance = 4451,
                LoadedCustomerRate = 7405
            };

            InstantiatedDependencies.ProductInvestmentRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ProductInvestment id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateProductInvestmentId(int? model)
        {
            if (!model.HasValue)
                return GetProductInvestmentId();

            //read all and return the first one
            var models = InstantiatedDependencies.ProductInvestmentRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertProductInvestment();
        }

        /// <summary>
        /// Gets the TransactionTableNumber id
        /// </summary>
        /// <returns></returns>
        public static int GetTransactionTableNumberId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.TransactionTableNumberRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertTransactionTableNumber();
        }

        /// <summary>
        /// Inserts a TransactionTableNumber and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertTransactionTableNumber()
        {
            var model = new TransactionTableNumber
            {
                TransactionTypeId = GetTransactionTypeId(),
                TariffTable = 7,
                Fee = 8592,
                AdValorem = 445,
                IsActive = false
            };

            InstantiatedDependencies.TransactionTableNumberRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate TransactionTableNumber id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateTransactionTableNumberId(int? model)
        {
            if (!model.HasValue)
                return GetTransactionTableNumberId();

            //read all and return the first one
            var models = InstantiatedDependencies.TransactionTableNumberRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertTransactionTableNumber();
        }

        /// <summary>
        /// Gets the SapDataImport id
        /// </summary>
        /// <returns></returns>
        public static int GetSapDataImportId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.SapDataImportRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().PricepointId;

            return InsertSapDataImport();
        }

        /// <summary>
        /// Inserts a SapDataImport and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertSapDataImport()
        {
            var model = new SapDataImport
            {
                PricepointId = GetUniquePricePointId(),
                CustomerId = "d17f6dacb3",
                AccountName = "fd59d4d781",
                ProductId = "9d99faefa9",
                Description = "11424abe13",
                GroupId = "79b149c5a2",
                SubGroupId = "c7b82de119",
                BankIdentifierId = "c1cd5f710a",
                AccountNo = "e6226be72c",
                OptionId = "3d2430f35e",
                UserId = "56d85333bf",
                TierFromValue = "6d0a6e313d",
                TierToValue = "850a885999",
                AdvaloremFee = "5bb6933505",
                MinimumFee = "3684922146",
                MaximumFee = "bcc8412776",
                FlatFee = "95a815371d",
                CommunicationFee = "dbca65674d",
                TableNo = "e43a092c49",
                TransactionVolume = "7118bca0c9",
                TransactionRevenue = "bd8492c359",
                ProductName = "e5c7d89e49",
                Channel = "882ccc8c24",
                MarketSegment = "6d024d1345",
                SequenceId = "c8d1222165",
                EntryDate = "bebb8c1458",
                EffectiveDate = "1baacae614",
                ExpiryDate = "1f83957e83",
                TerminationDate = "7e692bc149",
                Status = "1f293fac65",
                ImportDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now,
                ExportRow = true
            };

            InstantiatedDependencies.SapDataImportRepository.Create(model);

            return model.PricepointId;
        }

        /// <summary>
        /// Gets the alternate SapDataImport id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateSapDataImportId(int? model)
        {
            if (!model.HasValue)
                return GetSapDataImportId();

            //read all and return the first one
            var models = InstantiatedDependencies.SapDataImportRepository.ReadAll();

            if (models != null && models.Any(_ => _.PricepointId != model.Value))
                return models.First(_ => _.PricepointId != model.Value).PricepointId;

            return InsertSapDataImport();
        }

        /// <summary>
        /// Gets the unique int.
        /// </summary>
        /// <returns></returns>
        public static int GetUniquePricePointId()
        {
            using (var db = new SqlConnection(Configuration.ConnectionString))
            {
                return db.Query<int>(
                        "SELECT CASE WHEN MAX(PricepointId) IS NULL THEN 1 ELSE MAX(PricepointId) + 1 END FROM [dbo].[tblSapDataImport]")
                    .Single();
            }
        }

        /// <summary>
        /// Gets the SapDataImportConfiguration id
        /// </summary>
        /// <returns></returns>
        public static int GetSapDataImportConfigurationId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.SapDataImportConfigurationRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertSapDataImportConfiguration();
        }

        /// <summary>
        /// Inserts a SapDataImportConfiguration and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertSapDataImportConfiguration()
        {
            var model = new SapDataImportConfiguration
            {
                FileImportLocation = "72800b5f74",
                FileExportLocation = "f01bbd0fd2",
                SupportEmailAddress = "bf12bb082b"
            };

            InstantiatedDependencies.SapDataImportConfigurationRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate SapDataImportConfiguration id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateSapDataImportConfigurationId(int? model)
        {
            if (!model.HasValue)
                return GetSapDataImportConfigurationId();

            //read all and return the first one
            var models = InstantiatedDependencies.SapDataImportConfigurationRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertSapDataImportConfiguration();
        }

        /// <summary>
        /// Gets the ChannelTypeImport id
        /// </summary>
        /// <returns></returns>
        public static int GetChannelTypeImportId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ChannelTypeImportRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertChannelTypeImport();
        }

        /// <summary>
        /// Inserts a ChannelTypeImport and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertChannelTypeImport()
        {
            var model = new ChannelTypeImport
            {
                ChannelTypeId = GetChannelTypeId(),
                ImportFileChannel = "2041b19443"
            };

            InstantiatedDependencies.ChannelTypeImportRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ChannelTypeImport id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateChannelTypeImportId(int? model)
        {
            if (!model.HasValue)
                return GetChannelTypeImportId();

            //read all and return the first one
            var models = InstantiatedDependencies.ChannelTypeImportRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertChannelTypeImport();
        }

        /// <summary>
        /// Gets the ProductImport id
        /// </summary>
        /// <returns></returns>
        public static int GetProductImportId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ProductImportRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertProductImport();
        }

        /// <summary>
        /// Inserts a ProductImport and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertProductImport()
        {
            var model = new ProductImport
            {
                ProductId = GetProductId(),
                ImportFileChannel = "b5c0697217"
            };

            InstantiatedDependencies.ProductImportRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate ProductImport id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateProductImportId(int? model)
        {
            if (!model.HasValue)
                return GetProductImportId();

            //read all and return the first one
            var models = InstantiatedDependencies.ProductImportRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertProductImport();
        }

        /// <summary>
        /// Gets the TransactionTypeImport id
        /// </summary>
        /// <returns></returns>
        public static int GetTransactionTypeImportId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.TransactionTypeImportRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertTransactionTypeImport();
        }

        /// <summary>
        /// Inserts a TransactionTypeImport and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertTransactionTypeImport()
        {
            var model = new TransactionTypeImport
            {
                TransactionTypeId = GetTransactionTypeId(),
                ImportFileChannel = "eb729859bf"
            };

            InstantiatedDependencies.TransactionTypeImportRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate TransactionTypeImport id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateTransactionTypeImportId(int? model)
        {
            if (!model.HasValue)
                return GetTransactionTypeImportId();

            //read all and return the first one
            var models = InstantiatedDependencies.TransactionTypeImportRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertTransactionTypeImport();
        }

        /// <summary>
        /// Gets the AccountExecutiveAssistant id
        /// </summary>
        /// <returns></returns>
        public static int GetAccountExecutiveAssistantId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.AccountExecutiveAssistantRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            return InsertAccountExecutiveAssistant();
        }

        /// <summary>
        /// Inserts a AccountExecutiveAssistant and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertAccountExecutiveAssistant()
        {
            var aaUserId = GetUserId();

            var model = new AccountExecutiveAssistant
            {
                AccountAssistantUserId = aaUserId,
                AccountExecutiveUserId = GetAlternateUserId(aaUserId),
                IsActive = false
            };

            InstantiatedDependencies.AccountExecutiveAssistantRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate AccountExecutiveAssistant id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int GetAlternateAccountExecutiveAssistantId(int? model)
        {
            if (!model.HasValue)
                return GetAccountExecutiveAssistantId();

            //read all and return the first one
            var models = InstantiatedDependencies.AccountExecutiveAssistantRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != model.Value))
                return models.First(_ => _.Id != model.Value).Id;

            return InsertAccountExecutiveAssistant();
        }
    }
}
