using Moq;
using StandardBank.ConcessionManagement.Interface.Repository;

namespace StandardBank.ConcessionManagement.Test.Helpers
{
    /// <summary>
    /// Mocked dependencies
    /// </summary>
    public static class MockedDependencies
    {
        /// <summary>
        /// The mock concession count repository
        /// </summary>
        public static Mock<IConcessionCountRepository> MockConcessionCountRepository = new Mock<IConcessionCountRepository>();
    }
}
