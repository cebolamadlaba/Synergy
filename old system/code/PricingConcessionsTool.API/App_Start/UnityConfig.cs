using Microsoft.Practices.Unity;
using PricingConcessionsTool.Services.Interfaces;
using PricingConcessionsTool.Services.Services;
using System.Web.Http;
using Unity.WebApi;

namespace PricingConcessionsTool.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IReferenceService, ReferenceDataService>();
            container.RegisterType<IUserService, UserService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            container.RegisterType<IConcessionService, ConcessionService>();
            container.RegisterType<IPricingWorkflow, PricingWorkflow>();
        }
    }
}