using System.Linq;
using Moq;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    /// <summary>
    /// Region manager tests
    /// </summary>
    public class RegionManagerTest
    {
        /// <summary>
        /// Tests that ValidateRegion returns no errors.
        /// </summary>
        [Fact]
        public void ValidateRegion_Returns_No_Errors()
        {
            var mockRegionRepository = new Mock<IRegionRepository>();

            var regionManager = new RegionManager(mockRegionRepository.Object, InstantiatedDependencies.Mapper);

            var regions = new[]
            {
                new Model.Repository.Region
                {
                    Id = 1,
                    Description = "Gauteng",
                    IsActive = true
                },
                new Model.Repository.Region
                {
                    Id = 2,
                    Description = "KZN",
                    IsActive = true
                }
            };

            mockRegionRepository.Setup(_ => _.ReadAll()).Returns(regions);

            var regionToValidate = new Region
            {
                Id = 0,
                Description = "Western Cape",
                IsActive = true
            };

            var result = regionManager.ValidateRegion(regionToValidate);

            Assert.Empty(result);
        }

        /// <summary>
        /// Tests that ValidateRegion returns the no description error.
        /// </summary>
        [Fact]
        public void ValidateRegion_Returns_No_Description_Error()
        {
            var mockRegionRepository = new Mock<IRegionRepository>();

            var regionManager = new RegionManager(mockRegionRepository.Object, InstantiatedDependencies.Mapper);

            var regions = new[]
            {
                new Model.Repository.Region
                {
                    Id = 1,
                    Description = "Gauteng",
                    IsActive = true
                },
                new Model.Repository.Region
                {
                    Id = 2,
                    Description = "KZN",
                    IsActive = true
                }
            };

            mockRegionRepository.Setup(_ => _.ReadAll()).Returns(regions);

            var regionToValidate = new Region
            {
                Id = 0,
                IsActive = true
            };

            var result = regionManager.ValidateRegion(regionToValidate);

            Assert.NotEmpty(result);
            Assert.True(result.First() == "Please supply a valid description");
        }

        /// <summary>
        /// Tests that ValidateRegion returns the duplicate error when creating a record.
        /// </summary>
        [Fact]
        public void ValidateRegion_Returns_Duplicate_Error_For_Create()
        {
            var mockRegionRepository = new Mock<IRegionRepository>();

            var regionManager = new RegionManager(mockRegionRepository.Object, InstantiatedDependencies.Mapper);

            var regions = new[]
            {
                new Model.Repository.Region
                {
                    Id = 1,
                    Description = "Gauteng",
                    IsActive = true
                },
                new Model.Repository.Region
                {
                    Id = 2,
                    Description = "KZN",
                    IsActive = true
                }
            };

            mockRegionRepository.Setup(_ => _.ReadAll()).Returns(regions);

            var regionToValidate = new Region
            {
                Id = 0,
                Description = "KZN",
                IsActive = true
            };

            var result = regionManager.ValidateRegion(regionToValidate);

            Assert.NotEmpty(result);
            Assert.True(result.First() == "There is already a region with the same description, please use another description");
        }

        /// <summary>
        /// Tests that ValidateRegion returns the duplicate error when updating a record.
        /// </summary>
        [Fact]
        public void ValidateRegion_Returns_Duplicate_Error_For_Update()
        {
            var mockRegionRepository = new Mock<IRegionRepository>();

            var regionManager = new RegionManager(mockRegionRepository.Object, InstantiatedDependencies.Mapper);

            var regions = new[]
            {
                new Model.Repository.Region
                {
                    Id = 1,
                    Description = "Gauteng",
                    IsActive = true
                },
                new Model.Repository.Region
                {
                    Id = 2,
                    Description = "KZN",
                    IsActive = true
                }
            };

            mockRegionRepository.Setup(_ => _.ReadAll()).Returns(regions);

            var regionToValidate = new Region
            {
                Id = 2,
                Description = "Gauteng",
                IsActive = true
            };

            var result = regionManager.ValidateRegion(regionToValidate);

            Assert.NotEmpty(result);
            Assert.True(result.First() == "There is already a region with the same description, please use another description");
        }
    }
}
