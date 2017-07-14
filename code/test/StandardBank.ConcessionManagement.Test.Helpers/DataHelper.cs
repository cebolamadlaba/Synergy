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
            var model = new ChannelType
            {
                Description = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.ChannelTypeRepository.Create(model);

            return model.Id;
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
            var model = new BaseRate
            {
                Amount = 10000,
                IsActive = false
            };

            InstantiatedDependencies.BaseRateRepository.Create(model);

            return model.Id;
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
            var model = new TransactionGroup
            {
                Description = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.TransactionGroupRepository.Create(model);

            return model.Id;
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
            var model = new Province
            {
                Description = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.ProvinceRepository.Create(model);

            return model.Id;
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
            var model = new ConcessionType
            {
                Description = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                Code = $"{Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)}",
                IsActive = false
            };

            InstantiatedDependencies.ConcessionTypeRepository.Create(model);

            return model.Id;
        }

        public static int GetAlternateConcessionId(int? modelConcessionId)
        {
            throw new System.NotImplementedException();
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

        public static DateTime ChangeDate(DateTime? modelSystemDate)
        {
            throw new NotImplementedException();
        }

        public static int GetConcessionId()
        {
            throw new NotImplementedException();
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

        public static int GetAlternateProductTypeId(int? modelProductTypeId)
        {
            throw new NotImplementedException();
        }

        public static int GetProductTypeId()
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateReviewFeeTypeId(int? modelReviewFeeTypeId)
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateRoleId(int? modelRoleId)
        {
            throw new NotImplementedException();
        }

        public static int GetRoleId()
        {
            throw new NotImplementedException();
        }

        public static int GetAlternateTransactionTypeId(int? modelTransactionTypeId)
        {
            throw new NotImplementedException();
        }

        public static int GetTransactionTypeId()
        {
            throw new NotImplementedException();
        }

        public static int GetReviewFeeTypeId()
        {
            throw new NotImplementedException();
        }
    }
}
