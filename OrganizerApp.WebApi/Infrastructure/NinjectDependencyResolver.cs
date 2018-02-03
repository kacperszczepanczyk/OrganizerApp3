using DependencyComposer;
using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace OrganizerApp.WebApi.Infrastructure
{
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        readonly IKernel kernel;
        private readonly IComposer _independentProjectsDependencyComposer;


        public NinjectDependencyResolver(IKernel kernel , IComposer dependencyComposer = null) : base(kernel)
        {
            this.kernel = kernel;
            _independentProjectsDependencyComposer = dependencyComposer ?? new Composer();
            AddIndependentProjectBindings(kernel);
        }


        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }

        private void AddIndependentProjectBindings(IKernel kernel)
        {
            _independentProjectsDependencyComposer.Bind(kernel);
        }
    }



    public class NinjectDependencyScope : IDependencyScope
    {
        IResolutionRoot resolver;


        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }


        public object GetService(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolver.TryGet(serviceType);
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolver.GetAll(serviceType);
        }

        public void Dispose()
        {
            IDisposable disposable = resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            resolver = null;
        }
    }

}