using Ninject;
using OrganizerApp.WebUI.Helpers.Api;
using OrganizerApp.WebUI.Helpers.Api.OrganizerApp;
using OrganizerApp.WebUI.Helpers.Api.OrganizerApp.Projects;
using OrganizerApp.WebUI.Helpers.Api.OrganizerApp.Tasks;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OrganizerApp.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IOrganizerAppProjectApiRequestHandler>()
                    .To<OrganizerAppProjectApiRequestHandler>();
            _kernel.Bind<IOrganizerAppTaskApiRequestHandler>()
                    .To<OrganizerAppTaskApiRequestHandler>();
            _kernel.Bind<IApiRequestHandler>()
                    .To<ApiRequestHandler>()
                    .WithConstructorArgument("baseUri", ApiUriInfo.Domain.ToString());
        }
    }
}