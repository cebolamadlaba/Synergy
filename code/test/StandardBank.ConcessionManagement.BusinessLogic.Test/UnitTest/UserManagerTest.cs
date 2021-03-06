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
            _userManager = new UserManager(InstantiatedDependencies.CacheManager, MockUserRepository.Object,
                MockUserRoleRepository.Object, MockRoleRepository.Object, MockCentreRepository.Object,
                MockCentreUserRepository.Object, InstantiatedDependencies.Mapper, MockSUbRoleRepository.Object,
                MockAccountExecutiveAssistantRepository.Object, MockRegionManager.Object,null);
        }

        /// <summary>
        /// Tests that GetUser with an already selected region executes positive.
        /// </summary>
        [Fact]
        public void GetUser_WithAlreadySelectedRegion_Executes_Positive()
        {
            var aNumber = "A1234567";

            MockUserRepository.Setup(_ => _.ReadByANumber(It.IsAny<string>())).Returns(new User
            {
                ANumber = aNumber
            });

            MockUserRoleRepository.Setup(_ => _.ReadByUserId(It.IsAny<int>()))
                .Returns(new[] {new UserRole {RoleId = 1, IsActive = true}});

            MockRoleRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Role {Id = 1, IsActive = true, RoleName = "Test", RoleDescription = "Unit Test"}});

            MockRegionRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Region {Id = 1, IsActive = true, Description = "Test"}});

            MockCentreUserRepository.Setup(_ => _.ReadByUserId(It.IsAny<int>()))
                .Returns(new[] {new CentreUser {CentreId = 1, IsActive = true}});

            MockCentreRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Centre {IsActive = true, CentreName = "Unit Test", Id = 1, RegionId = 1}});

            var result = _userManager.GetUser(aNumber);

            Assert.NotNull(result);
            Assert.Equal(aNumber, result.ANumber);
            Assert.NotNull(result.UserRoles);
            Assert.NotEmpty(result.UserRoles);
            Assert.NotNull(result.SelectedCentre);
        }

        /// <summary>
        /// Tests that GetUser with no selected region executes positive.
        /// </summary>
        [Fact]
        public void GetUser_WithNoSelectedRegion_Executes_Positive()
        {
            var aNumber = "A1234567";

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

            MockRegionRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Region {Id = 1, IsActive = true, Description = "Test"}});

            MockCentreUserRepository.Setup(_ => _.ReadByUserId(It.IsAny<int>()))
                .Returns(new[] {new CentreUser {CentreId = 1, IsActive = true}});

            MockCentreRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Centre {IsActive = true, CentreName = "Unit Test", Id = 1, RegionId = 1}});

            var result = _userManager.GetUser(aNumber);

            Assert.NotNull(result);
            Assert.Equal(aNumber, result.ANumber);
            Assert.NotNull(result.UserRoles);
            Assert.NotEmpty(result.UserRoles);
            Assert.NotNull(result.SelectedCentre);
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
