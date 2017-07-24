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
                MockUserRoleRepository.Object, MockRoleRepository.Object);
        }

        /// <summary>
        /// Tests that GetUser executes positive.
        /// </summary>
        [Fact]
        public void GetUser_Executes_Positive()
        {
            var aNumber = "A1234567";

            MockUserRepository.Setup(_ => _.ReadByANumber(It.IsAny<string>())).Returns(new User
            {
                ANumber = aNumber
            });

            MockUserRoleRepository.Setup(_ => _.ReadByUserId(It.IsAny<int>())).Returns(new[] {new UserRole { RoleId = 1}});

            MockRoleRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new Role());

            var result = _userManager.GetUser(aNumber);

            Assert.NotNull(result);
            Assert.Equal(aNumber, result.ANumber);
        }
    }
}
