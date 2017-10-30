using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// SapDataImport repository tests
    /// </summary>
    public class SapDataImportRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new SapDataImport
            {
                PricepointId = "39a49764a1",
                CustomerId = "ec179a717c",
                AccountName = "f8edfc0384",
                ProductId = "e4730e4082",
                Description = "00500f2bdc",
                GroupId = "950b97e015",
                SubGroupId = "102fb73ef7",
                BankIdentifierId = "00bbba89bd",
                AccountNo = "b3813a0554",
                OptionId = "e898c13164",
                UserId = "e2d51fd88d",
                TierFromValue = "7da53fe5cc",
                TierToValue = "cefb87eadf",
                AdvaloremFee = "a2e562481e",
                MinimumFee = "d5476ef251",
                MaximumFee = "fae3eda926",
                FlatFee = "f2d2be156d",
                CommunicationFee = "3232c3b1e1",
                TableNo = "01357ec6b0",
                TransactionVolume = "24f60de98b",
                TransactionRevenue = "22440e7795",
                ProductName = "507b98b7ae",
                Channel = "a5a34494d9",
                MarketSegment = "a1232474b4",
                SequenceId = "8fe5566bb1",
                EntryDate = "ffad3535f1",
                EffectiveDate = "6e84f605cf",
                ExpiryDate = "469cab6736",
                TerminationDate = "5198caf639",
                Status = "052b12a9fc",
                ImportDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now
            };

            var result = InstantiatedDependencies.SapDataImportRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.SapDataImportRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.SapDataImportRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.SapDataImportRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.SapDataImportRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.SapDataImportRepository.ReadById(id);

            model.PricepointId = "d69717aac4";
            model.CustomerId = "701a0b16b6";
            model.AccountName = "f172218822";
            model.ProductId = "49cbeda592";
            model.Description = "eb26ab7d41";
            model.GroupId = "6a49897791";
            model.SubGroupId = "2acbb377d3";
            model.BankIdentifierId = "d9495e98f8";
            model.AccountNo = "df44624a09";
            model.OptionId = "433886b5a7";
            model.UserId = "271a2e47ef";
            model.TierFromValue = "5c98352fa8";
            model.TierToValue = "ef0a4a752f";
            model.AdvaloremFee = "20b8311647";
            model.MinimumFee = "64574abcb3";
            model.MaximumFee = "984b9bf416";
            model.FlatFee = "5ab5fba882";
            model.CommunicationFee = "71fec36faa";
            model.TableNo = "4bf1f2e123";
            model.TransactionVolume = "f93dbaca95";
            model.TransactionRevenue = "0323bf540c";
            model.ProductName = "c5b1db1daf";
            model.Channel = "467bfe5661";
            model.MarketSegment = "f8cbebda5d";
            model.SequenceId = "a7e22b1aba";
            model.EntryDate = "95dee0a40f";
            model.EffectiveDate = "1eb8cb5b96";
            model.ExpiryDate = "c0d0d5c70e";
            model.TerminationDate = "7ffdf88bf6";
            model.Status = "1cd911ed29";
            model.ImportDate = DataHelper.ChangeDate(model.ImportDate);
            model.LastUpdatedDate = DataHelper.ChangeDate(model.LastUpdatedDate);

            InstantiatedDependencies.SapDataImportRepository.Update(model);

            var updatedModel = InstantiatedDependencies.SapDataImportRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.PricepointId, model.PricepointId);
            Assert.Equal(updatedModel.CustomerId, model.CustomerId);
            Assert.Equal(updatedModel.AccountName, model.AccountName);
            Assert.Equal(updatedModel.ProductId, model.ProductId);
            Assert.Equal(updatedModel.Description, model.Description);
            Assert.Equal(updatedModel.GroupId, model.GroupId);
            Assert.Equal(updatedModel.SubGroupId, model.SubGroupId);
            Assert.Equal(updatedModel.BankIdentifierId, model.BankIdentifierId);
            Assert.Equal(updatedModel.AccountNo, model.AccountNo);
            Assert.Equal(updatedModel.OptionId, model.OptionId);
            Assert.Equal(updatedModel.UserId, model.UserId);
            Assert.Equal(updatedModel.TierFromValue, model.TierFromValue);
            Assert.Equal(updatedModel.TierToValue, model.TierToValue);
            Assert.Equal(updatedModel.AdvaloremFee, model.AdvaloremFee);
            Assert.Equal(updatedModel.MinimumFee, model.MinimumFee);
            Assert.Equal(updatedModel.MaximumFee, model.MaximumFee);
            Assert.Equal(updatedModel.FlatFee, model.FlatFee);
            Assert.Equal(updatedModel.CommunicationFee, model.CommunicationFee);
            Assert.Equal(updatedModel.TableNo, model.TableNo);
            Assert.Equal(updatedModel.TransactionVolume, model.TransactionVolume);
            Assert.Equal(updatedModel.TransactionRevenue, model.TransactionRevenue);
            Assert.Equal(updatedModel.ProductName, model.ProductName);
            Assert.Equal(updatedModel.Channel, model.Channel);
            Assert.Equal(updatedModel.MarketSegment, model.MarketSegment);
            Assert.Equal(updatedModel.SequenceId, model.SequenceId);
            Assert.Equal(updatedModel.EntryDate, model.EntryDate);
            Assert.Equal(updatedModel.EffectiveDate, model.EffectiveDate);
            Assert.Equal(updatedModel.ExpiryDate, model.ExpiryDate);
            Assert.Equal(updatedModel.TerminationDate, model.TerminationDate);
            Assert.Equal(updatedModel.Status, model.Status);
            Assert.Equal(updatedModel.ImportDate, model.ImportDate);
            Assert.Equal(updatedModel.LastUpdatedDate, model.LastUpdatedDate);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new SapDataImport
            {
                PricepointId = "39a49764a1",
                CustomerId = "ec179a717c",
                AccountName = "f8edfc0384",
                ProductId = "e4730e4082",
                Description = "00500f2bdc",
                GroupId = "950b97e015",
                SubGroupId = "102fb73ef7",
                BankIdentifierId = "00bbba89bd",
                AccountNo = "b3813a0554",
                OptionId = "e898c13164",
                UserId = "e2d51fd88d",
                TierFromValue = "7da53fe5cc",
                TierToValue = "cefb87eadf",
                AdvaloremFee = "a2e562481e",
                MinimumFee = "d5476ef251",
                MaximumFee = "fae3eda926",
                FlatFee = "f2d2be156d",
                CommunicationFee = "3232c3b1e1",
                TableNo = "01357ec6b0",
                TransactionVolume = "24f60de98b",
                TransactionRevenue = "22440e7795",
                ProductName = "507b98b7ae",
                Channel = "a5a34494d9",
                MarketSegment = "a1232474b4",
                SequenceId = "8fe5566bb1",
                EntryDate = "ffad3535f1",
                EffectiveDate = "6e84f605cf",
                ExpiryDate = "469cab6736",
                TerminationDate = "5198caf639",
                Status = "052b12a9fc",
                ImportDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now
            };

            var temporaryEntity = InstantiatedDependencies.SapDataImportRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.SapDataImportRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.SapDataImportRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
