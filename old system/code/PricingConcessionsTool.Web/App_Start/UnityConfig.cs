using PricingConcessionsTool.Services.Interfaces;
using PricingConcessionsTool.Services.Services;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace PricingConcessionsTool
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            container.RegisterType<IReferenceService, ReferenceDataService>();          
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IConcessionService, ConcessionService>();
            container.RegisterType<IPricingWorkflow, PricingWorkflow>();

        }
    }
}