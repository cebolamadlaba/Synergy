﻿using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;
using StandardBank.ConcessionManagement.Test.Helpers;
using StandardBank.ConcessionManagement.UI.Controllers.Inbox;
using Xunit;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Inbox
{
    /// <summary>
    /// Concessions summary controller tests
    /// </summary>
    public class UserConcessionsControllerTest
    {
        /// <summary>
        /// The concessions summary controller
        /// </summary>
        private readonly UserConcessionsController _userConcessionsController;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserConcessionsControllerTest"/> class.
        /// </summary>
        public UserConcessionsControllerTest()
        {
            _userConcessionsController =
                new UserConcessionsController(MockedDependencies.MockConcessionManager.Object,
                    MockedDependencies.MockSiteHelper.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            MockedDependencies.MockConcessionManager.Setup(_ => _.GetUserConcessions(It.IsAny<User>()))
                .Returns(new UserConcessions());

            var result = _userConcessionsController.Get();

            Assert.NotNull(result);
        }
    }
}