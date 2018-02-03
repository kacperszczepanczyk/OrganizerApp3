using Ninject;
using OrganizerApp.WebUI.Constants;
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
            _kernel.Bind<RestClient>()
                   .ToSelf()
                   .WithConstructorArgument(ApiUriInfo.Domain); 
        }
    }
}