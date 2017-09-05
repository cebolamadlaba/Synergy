using System;
using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    /// <summary>
    /// User manager tests
    /// </summary>
    public class UserManagerTest
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManagerTest"/> class.
        /// </summary>
        public UserManagerTest()
        {
            _userManager = new UserManager(InstantiatedDependencies.CacheManager, MockLookupTableManager.Object,
                MockUserRepository.Object, MockUserRoleRepository.Object, MockRoleRepository.Object,
                MockUserRegionRepository.Object, MockRegionRepository.Object, MockCentreRepository.Object,
                MockCentreUserRepository.Object, InstantiatedDependencies.Mapper);
        }

        /// <summary>
        /// Tests that GetUser with an already selected region executes positive.
        /// </summary>
        [Fact]
        public void GetUser_WithAlreadySelectedRegion_Executes_Positive()
        {
            var aNumber = "A1234567";

            MockLookupTableManager.Setup(_ => _.GetProvinceName(It.IsAny<int>())).Returns("Unit Test Province");

            MockUserRepository.Setup(_ => _.ReadByANumber(It.IsAny<string>())).Returns(new User
            {
                ANumber = aNumber
            });

            MockUserRoleRepository.Setup(_ => _.ReadByUserId(It.IsAny<int>()))
                .Returns(new[] {new UserRole {RoleId = 1, IsActive = true}});

            MockRoleRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Role {Id = 1, IsActive = true, RoleName = "Test", RoleDescription = "Unit Test"}});

            //this is where we set whether or not there is an already selected region
            MockUserRegionRepository.Setup(_ => _.ReadByUserId(It.IsAny<int>()))
                .Returns(new[] {new UserRegion {RegionId = 1, IsActive = true, IsSelected = true}});

            MockRegionRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Region {Id = 1, IsActive = true, Description = "Test"}});

            MockCentreUserRepository.Setup(_ => _.ReadByUserId(It.IsAny<int>()))
                .Returns(new[] {new CentreUser {CentreId = 1, IsActive = true}});

            MockCentreRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Centre {IsActive = true, CentreName = "Unit Test", Id = 1, ProvinceId = 1}});

            var result = _userManager.GetUser(aNumber);

            Assert.NotNull(result);
            Assert.Equal(aNumber, result.ANumber);
            Assert.NotNull(result.UserRoles);
            Assert.NotEmpty(result.UserRoles);
            Assert.NotNull(result.SelectedRegion);
            Assert.True(result.SelectedRegion.IsSelected);
            Assert.NotNull(result.UserRegions);
            Assert.NotEmpty(result.UserRegions);
            Assert.NotNull(result.SelectedCentre);
        }

        /// <summary>
        /// Tests that GetUser with no selected region executes positive.
        /// </summary>
        [Fact]
        public void GetUser_WithNoSelectedRegion_Executes_Positive()
        {
            var aNumber = "A1234567";

            MockLookupTableManager.Setup(_ => _.GetProvinceName(It.IsAny<int>())).Returns("Unit Test Province");

            MockUserRepository.Setup(_ => _.ReadByANumber(It.IsAny<string>())).Returns(new User
            {
                ANumber = aNumber
            });

            MockUserRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new User
            {
                ANumber = aNumber
            });

            MockUserRoleRepository.Setup(_ => _.ReadByUserId(It.IsAny<int>()))
                .Returns(new[] {new UserRole {RoleId = 1, IsActive = true}});

            MockRoleRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Role {Id = 1, IsActive = true, RoleName = "Test", RoleDescription = "Unit Test"}});

            //this is where we set whether or not there is an already selected region
            MockUserRegionRepository.Setup(_ => _.ReadByUserId(It.IsAny<int>()))
                .Returns(new[] {new UserRegion {RegionId = 1, IsActive = true, IsSelected = false}});

            MockRegionRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Region {Id = 1, IsActive = true, Description = "Test"}});

            MockCentreUserRepository.Setup(_ => _.ReadByUserId(It.IsAny<int>()))
                .Returns(new[] {new CentreUser {CentreId = 1, IsActive = true}});

            MockCentreRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Centre {IsActive = true, CentreName = "Unit Test", Id = 1, ProvinceId = 1}});

            var result = _userManager.GetUser(aNumber);

            Assert.NotNull(result);
            Assert.Equal(aNumber, result.ANumber);
            Assert.NotNull(result.UserRoles);
            Assert.NotEmpty(result.UserRoles);
            Assert.NotNull(result.SelectedRegion);
            Assert.True(result.SelectedRegion.IsSelected);
            Assert.NotNull(result.UserRegions);
            Assert.NotEmpty(result.UserRegions);
            Assert.NotNull(result.SelectedCentre);
        }

        /// <summary>
        /// Tests that SetUserSelectedRegion executes positive
        /// </summary>
        [Fact]
        public void SetUserSelectedRegion_Executes_Positive()
        {
            var aNumber = "A1234567";

            MockUserRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new User
            {
                ANumber = aNumber
            });

            _userManager.SetUserSelectedRegion(1, 1);

            Assert.True(true);
        }

        /// <summary>
        /// Tests that GetUserName executes positive.
        /// </summary>
        [Fact]
        public void GetUserName_Executes_Positive()
        {
            MockUserRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new User {FirstName = "Part1", Surname = "Part2"});

            var result = _userManager.GetUserName(1);

            Assert.NotNull(result);
            Assert.Equal(result, "Part1 Part2");
        }
    }
}
