using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Test.Helpers
{
    /// <summary>
    /// Data helper
    /// </summary>
    public static class DataHelper
    {
        /// <summary>
        /// Changes the date value from whatever it is to something different
        /// </summary>
        /// <param name="modelSystemDate"></param>
        /// <returns></returns>
        public static DateTime ChangeDate(DateTime? modelSystemDate)
        {
            if (!modelSystemDate.HasValue)
                return DateTime.Now;

            return modelSystemDate.Value.AddHours(1);
        }

        /// <summary>
        /// Gets the channel type id
        /// </summary>
        /// <returns></returns>
        public static int GetChannelTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ChannelTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            //if there are none, insert one
            return InsertChannelType();
        }

        /// <summary>
        /// Inserts a channel type and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertChannelType()
        {
            var model = new ChannelType
            {
                Description = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.ChannelTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate channel type id
        /// </summary>
        /// <param name="modelChannelTypeId"></param>
        /// <returns></returns>
        public static int GetAlternateChannelTypeId(int? modelChannelTypeId)
        {
            if (!modelChannelTypeId.HasValue)
                return GetChannelTypeId();

            //read all and return the first one that doesn't match the passed in id
            var models = InstantiatedDependencies.ChannelTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != modelChannelTypeId.Value))
                return models.First(_ => _.Id != modelChannelTypeId.Value).Id;

            //if there are none, insert one
            return InsertChannelType();
        }

        /// <summary>
        /// Gets the base rate id
        /// </summary>
        /// <returns></returns>
        public static int GetBaseRateId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.BaseRateRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            //if there are none, insert one
            return InsertBaseRate();
        }

        /// <summary>
        /// Inserts a base rate and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertBaseRate()
        {
            var model = new BaseRate
            {
                Amount = 10000,
                IsActive = false
            };

            InstantiatedDependencies.BaseRateRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate base rate id
        /// </summary>
        /// <param name="modelBaseRateId"></param>
        /// <returns></returns>
        public static int GetAlternateBaseRateId(int? modelBaseRateId)
        {
            if (!modelBaseRateId.HasValue)
                return GetBaseRateId();

            //read all and return the first one that doesn't match the passed in id
            var models = InstantiatedDependencies.BaseRateRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != modelBaseRateId.Value))
                return models.First(_ => _.Id != modelBaseRateId.Value).Id;

            //if there are none, insert one
            return InsertBaseRate();
        }

        /// <summary>
        /// Gets the transaction group id
        /// </summary>
        /// <returns></returns>
        public static int GetTransactionGroupId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.TransactionGroupRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            //if there are none, insert one
            return InsertTransactionGroup();
        }

        /// <summary>
        /// Insert the transaction group and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertTransactionGroup()
        {
            var model = new TransactionGroup
            {
                Description = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.TransactionGroupRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate transaction group id
        /// </summary>
        /// <param name="modelTransactionGroupId"></param>
        /// <returns></returns>
        public static int GetAlternateTransactionGroupId(int? modelTransactionGroupId)
        {
            if (!modelTransactionGroupId.HasValue)
                return GetTransactionGroupId();

            //read all and return the first one
            var models = InstantiatedDependencies.TransactionGroupRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != modelTransactionGroupId.Value))
                return models.First(_ => _.Id != modelTransactionGroupId.Value).Id;

            //if there are none, insert one
            return InsertTransactionGroup();
        }

        /// <summary>
        /// Gets the province id
        /// </summary>
        /// <returns></returns>
        public static int GetProvinceId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ProvinceRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            //if there are none, insert one
            return InsertProvince();
        }

        /// <summary>
        /// Inserts a province and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertProvince()
        {
            var model = new Province
            {
                Description = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.ProvinceRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate province id
        /// </summary>
        /// <param name="modelProvinceId"></param>
        /// <returns></returns>
        public static int GetAlternateProvinceId(int? modelProvinceId)
        {
            if (!modelProvinceId.HasValue)
                return GetProvinceId();

            //read all and return the first one
            var models = InstantiatedDependencies.ProvinceRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != modelProvinceId.Value))
                return models.First(_ => _.Id != modelProvinceId.Value).Id;

            //if there are none, insert one
            return InsertProvince();
        }

        /// <summary>
        /// Gets the centre id
        /// </summary>
        /// <returns></returns>
        public static int GetCentreId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.CentreRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            //if there are none, insert one
            return InsertCentre();
        }

        /// <summary>
        /// Inserts a centre and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertCentre()
        {
            var model = new Centre
            {
                ProvinceId = GetProvinceId(),
                CentreName = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.CentreRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate centre id
        /// </summary>
        /// <param name="modelCentreId"></param>
        /// <returns></returns>
        public static int GetAlternateCentreId(int? modelCentreId)
        {
            if (!modelCentreId.HasValue)
                return GetCentreId();

            //read all and return the first one
            var models = InstantiatedDependencies.CentreRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != modelCentreId.Value))
                return models.First(_ => _.Id != modelCentreId.Value).Id;

            //if there are none, insert one
            return InsertCentre();
        }

        /// <summary>
        /// Gets the user id
        /// </summary>
        /// <returns></returns>
        public static int GetUserId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.UserRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            //if there are none, insert one
            return InsertUser();
        }

        /// <summary>
        /// Inserts a user and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertUser()
        {
            var model = new User
            {
                ANumber = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                EmailAddress = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}@gmail.com",
                FirstName = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                Surname = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.UserRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate user id
        /// </summary>
        /// <param name="modelUserId"></param>
        /// <returns></returns>
        public static int GetAlternateUserId(int? modelUserId)
        {
            if (!modelUserId.HasValue)
                return GetUserId();

            //read all and return the first one
            var models = InstantiatedDependencies.UserRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != modelUserId.Value))
                return models.First(_ => _.Id != modelUserId.Value).Id;

            //if there are none, insert one
            return InsertUser();
        }

        /// <summary>
        /// Gets the concession type id
        /// </summary>
        /// <returns></returns>
        public static int GetConcessionTypeId()
        {
            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionTypeRepository.ReadAll();

            if (models != null && models.Any())
                return models.First().Id;

            //if there are none, insert one
            return InsertConcessionType();
        }

        /// <summary>
        /// Inserts a concession type and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcessionType()
        {
            var model = new ConcessionType
            {
                Description = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                Code = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.ConcessionTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate concession type id
        /// </summary>
        /// <param name="modelConcessionTypeId"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionTypeId(int? modelConcessionTypeId)
        {
            if (!modelConcessionTypeId.HasValue)
                return GetConcessionTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != modelConcessionTypeId.Value))
                return models.First(_ => _.Id != modelConcessionTypeId.Value).Id;

            //if there are none, insert one
            return InsertConcessionType();
        }

        /// <summary>
        /// Gets the concession id
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
        /// Inserts a concession and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertConcession()
        {
            //if there are none, insert one
            var model = new Concession
            {
                TypeId = GetTypeId(),
                BCMUserId = GetBCMUserId(),
                CentreId = GetCentreId(),
                ConcessionDate = DateTime.Now,
                ConcessionRef = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                ConcessionTypeId = GetConcessionTypeId(),
                DateActionedByBCM = DateTime.Now,
                DateActionedByHO = DateTime.Now,
                DateActionedByPCM = DateTime.Now,
                DateApproved = DateTime.Now,
                DatesentForApproval = DateTime.Now,
                ExpiryDate = DateTime.Now,
                HOUserId = GetHOUserId(),
                IsActive = false,
                IsCurrent = false,
                LegalEntityId = GetLegalEntityId(),
                Motivation = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                PCMUserId = GetPCMUserId(),
                RequestorId = GetRequestorId(),
                SMTDealNumber = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                StatusId = GetStatusId(),
                SubStatusId = GetSubStatusId()
            };

            InstantiatedDependencies.ConcessionRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelConcessionId"></param>
        /// <returns></returns>
        public static int GetAlternateConcessionId(int? modelConcessionId)
        {
            if (!modelConcessionId.HasValue)
                GetConcessionId();

            //read all and return the first one
            var models = InstantiatedDependencies.ConcessionRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != modelConcessionId.Value))
                return models.First(_ => _.Id != modelConcessionId.Value).Id;

            return InsertConcession();
        }

        /// <summary>
        /// Gets the review fee type id
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
        /// Inserts a review fee type and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertReviewFeeType()
        {
            var model = new ReviewFeeType
            {
                Description = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.ReviewFeeTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate review fee type id
        /// </summary>
        /// <param name="modelReviewFeeTypeId"></param>
        /// <returns></returns>
        public static int GetAlternateReviewFeeTypeId(int? modelReviewFeeTypeId)
        {
            if (!modelReviewFeeTypeId.HasValue)
                return GetReviewFeeTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.ReviewFeeTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != modelReviewFeeTypeId.Value))
                return models.First(_ => _.Id != modelReviewFeeTypeId.Value).Id;

            return InsertReviewFeeType();
        }

        /// <summary>
        /// Gets the transaction type id
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
        /// Inserts a transaction type and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertTransactionType()
        {
            var model = new TransactionType
            {
                Description = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false,
                ConcessionTypeId = GetConcessionTypeId()
            };

            InstantiatedDependencies.TransactionTypeRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate transaction type id
        /// </summary>
        /// <param name="modelTransactionTypeId"></param>
        /// <returns></returns>
        public static int GetAlternateTransactionTypeId(int? modelTransactionTypeId)
        {
            if (!modelTransactionTypeId.HasValue)
                return GetTransactionTypeId();

            //read all and return the first one
            var models = InstantiatedDependencies.TransactionTypeRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != modelTransactionTypeId.Value))
                return models.First(_ => _.Id != modelTransactionTypeId.Value).Id;

            return InsertTransactionType();
        }

        /// <summary>
        /// Gets the role id
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
        /// Inserts a role and returns the id
        /// </summary>
        /// <returns></returns>
        private static int InsertRole()
        {
            var model = new Role
            {
                RoleDescription = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                RoleName = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.RoleRepository.Create(model);

            return model.Id;
        }

        /// <summary>
        /// Gets the alternate role id
        /// </summary>
        /// <param name="modelRoleId"></param>
        /// <returns></returns>
        public static int GetAlternateRoleId(int? modelRoleId)
        {
            if (!modelRoleId.HasValue)
                return GetRoleId();

            //read all and return the first one
            var models = InstantiatedDependencies.RoleRepository.ReadAll();

            if (models != null && models.Any(_ => _.Id != modelRoleId.Value))
                return models.First(_ => _.Id != modelRoleId.Value).Id;

            return InsertRole();
        }

        public static int GetAlternateProductTypeId(int? modelProductTypeId)
        {
            throw new NotImplementedException();
        }

        public static int GetProductTypeId()
        {
            throw new NotImplementedException();
        }


        public static int GetAlternateBCMUserId(int? modelBcmUserId)
        {
            throw new System.NotImplementedException();
        }

        public static int GetAlternatePCMUserId(int? modelPcmUserId)
        {
            throw new System.NotImplementedException();
        }

        public static int GetAlternateHOUserId(int? modelHoUserId)
        {
            throw new System.NotImplementedException();
        }

        public static int GetAlternateSubStatusId(int? modelSubStatusId)
        {
            throw new System.NotImplementedException();
        }

        public static int GetBCMUserId()
        {
            throw new NotImplementedException();
        }

        public static int GetPCMUserId()
        {
            throw new NotImplementedException();
        }

        public static int GetHOUserId()
        {
            throw new NotImplementedException();
        }

        public static int GetSubStatusId()
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateBusinesOnlineTransactionTypeId(int? modelBusinesOnlineTransactionTypeId)
        {
            throw new NotImplementedException();
        }

        public static int GetBusinesOnlineTransactionTypeId()
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateConcessionSubStatusId(int? modelConcessionSubStatusId)
        {
            throw new NotImplementedException();
        }

        public static int GetConcessionSubStatusId()
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateTypeId(int? modelTypeId)
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateLegalEntityId(int? modelLegalEntityId)
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateStatusId(int? modelStatusId)
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateRequestorId(int? modelRequestorId)
        {
            throw new NotImplementedException();
        }

        public static int GetTypeId()
        {
            throw new NotImplementedException();
        }

        public static int GetLegalEntityId()
        {
            throw new NotImplementedException();
        }

        public static int GetStatusId()
        {
            throw new NotImplementedException();
        }

        public static int GetRequestorId()
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateConditionTypeId(int? modelConditionTypeId)
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateConditionProductId(int? modelConditionProductId)
        {
            throw new NotImplementedException();
        }

        public static int GetConditionTypeId()
        {
            throw new NotImplementedException();
        }

        public static int GetConditionProductId()
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateMarketSegmentId(int? modelMarketSegmentId)
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateRiskGroupId(int? modelRiskGroupId)
        {
            throw new NotImplementedException();
        }

        public static int GetMarketSegmentId()
        {
            throw new NotImplementedException();
        }

        public static int GetRiskGroupId()
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateOldSubStatusId(int? modelOldSubStatusId)
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateNewSubStatusId(int? modelNewSubStatusId)
        {
            throw new NotImplementedException();
        }

        public static int GetOldSubStatusId()
        {
            throw new NotImplementedException();
        }

        public static int GetNewSubStatusId()
        {
            throw new NotImplementedException();
        }
    }
}
