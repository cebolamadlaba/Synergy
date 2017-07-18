using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Authorizing user repository tests
    /// </summary>
    public class AuthorizingUserRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new AuthorizingUser
            {
                Center = Convert.ToString(DataHelper.GetCentreId()),
                ProvincialUserId = Convert.ToString(DataHelper.GetUserId()),
                AuthorizingUserId = Convert.ToString(DataHelper.GetUserId()),
                PricingUserId = Convert.ToString(DataHelper.GetUserId()),
                Segment = Convert.ToString(DataHelper.GetMarketSegmentId())
            };

            var result = InstantiatedDependencies.AuthorizingUserRepository.Create(model);

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.AuthorizingUserRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var centreId = Convert.ToString(DataHelper.GetAlternateCentreId(DataHelper.GetCentreId()));

            var userId = DataHelper.GetUserId();
            var alternateUserId = DataHelper.GetAlternateUserId(userId);

            var provincialUserId = Convert.ToString(alternateUserId);
            var authorizingUserId = Convert.ToString(userId);
            var pricingUserId = Convert.ToString(alternateUserId);

            var segmentId = Convert.ToString(DataHelper.GetAlternateMarketSegmentId(DataHelper.GetMarketSegmentId()));

            var model = new AuthorizingUser
            {
                Center = centreId,
                ProvincialUserId = provincialUserId,
                AuthorizingUserId = authorizingUserId,
                PricingUserId = pricingUserId,
                Segment = segmentId
            };

            var temporaryEntity = InstantiatedDependencies.AuthorizingUserRepository.Create(model);

            Assert.NotNull(temporaryEntity);

            InstantiatedDependencies.AuthorizingUserRepository.Delete(temporaryEntity);

            var allRecords = InstantiatedDependencies.AuthorizingUserRepository.ReadAll();

            var result = allRecords.FirstOrDefault(_ => _.AuthorizingUserId == authorizingUserId &&
                                                        _.Center == centreId &&
                                                        _.PricingUserId == pricingUserId &&
                                                        _.ProvincialUserId == provincialUserId &&
                                                        _.Segment == segmentId);

            Assert.Null(result);
        }
    }
}
