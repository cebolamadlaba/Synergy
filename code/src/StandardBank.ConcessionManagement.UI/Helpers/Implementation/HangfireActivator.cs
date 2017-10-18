using System;

namespace StandardBank.ConcessionManagement.UI.Helpers.Implementation
{
    /// <summary>
    /// Hangfire Activator
    /// </summary>
    /// <seealso cref="Hangfire.JobActivator" />
    public class HangfireActivator : Hangfire.JobActivator
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="HangfireActivator"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public HangfireActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Activates the job.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public override object ActivateJob(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}
