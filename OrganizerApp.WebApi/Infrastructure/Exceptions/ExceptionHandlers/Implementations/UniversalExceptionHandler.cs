using OrganizerApp.WebApi.Infrastructure.Exceptions.ExceptionHandlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace OrganizerApp.WebApi.Infrastructure.Exceptions.ExceptionHandlers.Implementations
{
    public class UniversalExceptionHandler : IConcreteExceptionHandler
    {
        private ExceptionHandlerContext _exContext;
        private HttpStatusCode _statusCode;
        private string _description;

        public UniversalExceptionHandler(ExceptionHandlerContext exContext , string description = Messages.InternalServerErrorMessage , HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            _exContext = exContext;
            _statusCode = statusCode;
            _description = description;
        }

        public void Handle()
        {
            _exContext.Result = new TextErrorResult(_statusCode, _description);
        }
    }
}