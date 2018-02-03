using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyComposer
{
    public interface IComposer
    {
        void Bind(IKernel kernel);
    }
}
