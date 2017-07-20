namespace StandardBank.ConcessionManagement.Test.Helpers
{
    /// <summary>
    /// Configuration class
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// The connection string
        /// </summary>
        /// 
        /// **NB: DO NOT EVER POINT THIS CONNECTION STRING TO A DEV / TEST / PROD DATABASE. The tests insert and delete data at random **
        /// 
        public static string ConnectionString = "Server=.;Database=ConcessionPricingTool;Integrated Security=true";
    }
}
