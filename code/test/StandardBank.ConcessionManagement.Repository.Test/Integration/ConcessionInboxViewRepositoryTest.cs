using System;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Concession inbox view repository tests
    /// </summary>
    public class ConcessionInboxViewRepositoryTest
    {
        /// <summary>
        /// Tests that ReadByRequestorIdStatusIdsIsActive for one status id executes positive.
        /// </summary>
        [Fact]
        public void ReadByRequestorIdStatusIdsIsActive_One_StatusId_Executes_Positive()
        {
            var requestorId = DataHelper.GetUserId();
            var statusId = DataHelper.GetStatusId();
            var isActive = true;

            var result =
                InstantiatedDependencies.ConcessionInboxViewRepository.ReadByRequestorIdStatusIdsIsActive(requestorId,
                    new[] {statusId}, isActive);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
            {
                Assert.Equal(record.RequestorId.Value, requestorId);
                Assert.Equal(record.StatusId, statusId);
                Assert.Equal(record.IsActive, isActive);
            }
        }

        /// <summary>
        /// Tests that ReadByRequestorIdStatusIdsIsActive for multiple status ids executes positive.
        /// </summary>
        [Fact]
        public void ReadByRequestorIdStatusIdsIsActive_Multiple_StatusIds_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRepository.ReadAll();
            var requestorId = results.Last().RequestorId;
            var statusId = results.Last().StatusId;
            var isActive = true;

            var statuses = new List<int> {statusId};

            var secondStatusId = results
                .FirstOrDefault(_ => _.StatusId != statusId && _.IsActive == isActive && _.RequestorId == requestorId)
                ?.StatusId;

            if (secondStatusId.HasValue)
                statuses.Add(secondStatusId.Value);

            var result =
                InstantiatedDependencies.ConcessionInboxViewRepository.ReadByRequestorIdStatusIdsIsActive(requestorId, statuses, isActive);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
            {
                Assert.Equal(record.RequestorId.Value, requestorId);
                Assert.Equal(record.IsActive, isActive);
                Assert.True(statuses.Contains(record.StatusId));
            }
        }

        /// <summary>
        /// Tests that  executes positive.
        /// </summary>
        [Fact]
        public void ReadByCentreIdStatusIdSubStatusIdIsActive_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRepository.ReadAll();
            var centreId = results.First().CentreId;
            var statusId = results.First().StatusId;
            var subStatusId = results.First().SubStatusId;
            var isActive = results.First().IsActive;

            var result =
                InstantiatedDependencies.ConcessionInboxViewRepository.ReadByCentreIdStatusIdSubStatusIdIsActive(
                    centreId, statusId, subStatusId, isActive);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
            {
                Assert.Equal(record.CentreId, centreId);
                Assert.Equal(record.StatusId, statusId);
                Assert.Equal(record.SubStatusId, subStatusId);
                Assert.Equal(record.IsActive, isActive);
            }
        }

        /// <summary>
        /// Tests that ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive with a min start date executes positive.
        /// </summary>
        [Fact]
        public void ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive_Min_StartDate_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRepository.ReadAll();
            var requestorId = results.Last().RequestorId;
            var isActive = results.Last().IsActive;
            var startExpiryDate = new DateTime();
            var endExpiryDate = DateTime.Now.AddYears(10);

            var result =
                InstantiatedDependencies.ConcessionInboxViewRepository
                    .ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive(requestorId, startExpiryDate,
                        endExpiryDate, isActive);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
            {
                Assert.Equal(record.RequestorId.Value, requestorId);
                Assert.Equal(record.IsActive, isActive);

                Assert.True(record.ExpiryDate.Value > startExpiryDate);
                Assert.True(record.ExpiryDate.Value < endExpiryDate);
            }
        }

        /// <summary>
        /// Tests that ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive with a normal start date executes positive.
        /// </summary>
        [Fact]
        public void ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive_Normal_StartDate_Executes_Positive()
        {
            var requestorId = DataHelper.GetUserId();
            var isActive = true;
            var startExpiryDate = new DateTime(2001, 1, 1);
            var endExpiryDate = DateTime.Now.AddYears(10);

            var result =
                InstantiatedDependencies.ConcessionInboxViewRepository
                    .ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive(requestorId, startExpiryDate,
                        endExpiryDate, isActive);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
            {
                Assert.Equal(record.RequestorId.Value, requestorId);
                Assert.Equal(record.IsActive, isActive);

                Assert.True(record.ExpiryDate.Value > startExpiryDate);
                Assert.True(record.ExpiryDate.Value < endExpiryDate);
            }
        }

        /// <summary>
        /// Tests that ReadByRequestorIdIsMismatchedIsActive executes positive.
        /// </summary>
        [Fact]
        public void ReadByRequestorIdIsMismatchedIsActive_Executes_Positive()
        {
            var requestorId = DataHelper.GetUserId();
            var isActive = true;
            var isMismatched = false;

            var result =
                InstantiatedDependencies.ConcessionInboxViewRepository.ReadByRequestorIdIsMismatchedIsActive(
                    requestorId, isMismatched, isActive);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
            {
                Assert.Equal(record.RequestorId.Value, requestorId);
                Assert.Equal(record.IsActive, isActive);
                Assert.Equal(record.IsMismatched, isMismatched);
            }
        }

        /// <summary>
        /// Tests that ReadByBcmUserIdIsActive executes positive.
        /// </summary>
        [Fact]
        public void ReadByBcmUserIdIsActive_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRepository.ReadAll();
            var bcmUserId = results.First(_ => _.BCMUserId.HasValue).BCMUserId.Value;
            var isActive = results.First(_ => _.BCMUserId.HasValue).IsActive;

            var result = InstantiatedDependencies.ConcessionInboxViewRepository.ReadByBcmUserIdIsActive(bcmUserId, isActive);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
            {
                Assert.Equal(record.BCMUserId.Value, bcmUserId);
                Assert.Equal(record.IsActive, isActive);
            }
        }

        /// <summary>
        /// Tests that ReadByPcmUserIdIsActive executes positive.
        /// </summary>
        [Fact]
        public void ReadByPcmUserIdIsActive_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRepository.ReadAll();
            var pcmUserId = results.First(_ => _.PCMUserId.HasValue).PCMUserId.Value;
            var isActive = results.First(_ => _.PCMUserId.HasValue).IsActive;

            var result =
                InstantiatedDependencies.ConcessionInboxViewRepository.ReadByPcmUserIdIsActive(pcmUserId, isActive);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
            {
                Assert.Equal(record.PCMUserId.Value, pcmUserId);
                Assert.Equal(record.IsActive, isActive);
            }
        }

        /// <summary>
        /// Tests that ReadByHoUserIdIsActive executes positive.
        /// </summary>
        [Fact]
        public void ReadByHoUserIdIsActive_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRepository.ReadAll();
            var hoUserId = results.First(_ => _.HOUserId.HasValue).HOUserId.Value;
            var isActive = results.First(_ => _.HOUserId.HasValue).IsActive;

            var result = InstantiatedDependencies.ConcessionInboxViewRepository.ReadByHoUserIdIsActive(hoUserId, isActive);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
            {
                Assert.Equal(record.HOUserId.Value, hoUserId);
                Assert.Equal(record.IsActive, isActive);
            }
        }
    }
}
