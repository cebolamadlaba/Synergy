using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Repository;

namespace StandardBank.ConcessionManagement.Test.Helpers
{
    /// <summary>
    /// Instantiated dependencies
    /// </summary>
    public static class InstantiatedDependencies
    {
        /// <summary>
        /// Concession count repository
        /// </summary>
        public static IConcessionCountRepository ConcessionCountRepository = new ConcessionCountRepository();
    }
}
