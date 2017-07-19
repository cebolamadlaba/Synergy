using System;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// SMTRawData repository tests
    /// </summary>
    public class SMTRawDataRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new SMTRawData
            {
                DealID = "6e5c72ff12",
                DealSubmitDate = DateTime.Now,
                Status = "98cb7551d4",
                Segment = "d2b0e17e76",
                USERNAME = "55026c351f",
                Risk_Group_Cd = 1,
                Risk_Group_Type = "ab7af3f7b6",
                Risk_Group_Name = "7d41c520f5",
                RGHeadlineEarnings = 8347,
                AfterDealHE = 3590,
                RGROE = 9582,
                RGCapital = 1510,
                RGOI = 7047,
                DealROE = 8388,
                DealCapital = 1333,
                DealOI = 3318,
                BASEQUADRANT = "fec8a9fa4a",
                DEALQUADRANT = "144c44f15c",
                MandateQuadrantPass = "Yes",
                MandateLendingPass = "Yes",
                MandateBranchR100mExceeded = "Yes",
                MandateBranchADVLPass = "Yes",
                MandateCentreR100mExceeded = "Yes",
                MandateCentreADVLPass = "Yes",
                MandateAutoR100mExceeded = "Yes",
                MandateAutoADVLPass = "Yes",
                MandateBOLPass = "Yes",
                LendingProfit = 4365,
                LendingROE = 4666,
                LendingEquity = 9234,
                LendingOI = 5680,
                BOLInterchange = 9891,
                BOLProfit = 6349,
                BOLRevenue = 5431,
                BOLCapital = 319,
                CASHBranchVolume = 1,
                CASHCentreVolume = 1,
                CASHAutoVolume = 3,
                CASHBranchValue = 4166,
                CASHCentreValue = 962,
                CASHAutoValue = 8639,
                CASHBranchBase = 1,
                CASHCentreBase = 4,
                CASHAutoBase = 1,
                CASHBranchAdValorem = 4908,
                CASHCentreAdValorem = 1844,
                CASHAutoAdValorem = 2701,
                CASHBranchProfit = 4787,
                CASHCentreProfit = 6877,
                CASHAutoProfit = 9335,
                CASHBranchCapital = 1841,
                CASHCentreCapital = 5317,
                CASHAutoCapital = 6437,
                CASHBranchOI = 8945,
                CASHCentreOI = 9457,
                CASHAutoOI = 8737
            };

            var result = InstantiatedDependencies.SMTRawDataRepository.Create(model);

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.SMTRawDataRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
