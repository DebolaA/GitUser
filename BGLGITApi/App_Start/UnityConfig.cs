using Logger;
using Microsoft.Owin.Logging;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace BGLGITApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<ILog, Log>();
            container.RegisterType<IGitHubUserRepo, GitHubUserRepo>(new InjectionConstructor(
                container.Resolve<ILog>()));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}