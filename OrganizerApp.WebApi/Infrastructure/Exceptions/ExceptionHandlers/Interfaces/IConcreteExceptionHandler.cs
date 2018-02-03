using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace OrganizerApp.WebApi.Infrastructure.Exceptions.ExceptionHandlers.Interfaces
{
    public interface IConcreteExceptionHandler
    {
        void Handle();
    }
}
