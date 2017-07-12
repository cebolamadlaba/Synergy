using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Concession count repository test
    /// </summary>
    public class ConcessionCountRepositoryTest
    {
        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionCountRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
