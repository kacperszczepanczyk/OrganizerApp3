using OrganizerApp.WebApi.Infrastructure.Exceptions.ExceptionHandlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.ExceptionHandling;
using FluentValidation;

namespace OrganizerApp.WebApi.Infrastructure.Exceptions.ExceptionHandlers.Implementations
{
    public class ValidationExceptionHandler : IConcreteExceptionHandler
    {
        private ExceptionHandlerContext _exContext;

        public ValidationExceptionHandler(ExceptionHandlerContext exContext)
        {
            _exContext = exContext;
        }


        public void Handle()
        {
            string customExcMsg = "Validation failed for one or more entities. Info below: \n";
            StringBuilder valExcInfo = new StringBuilder(customExcMsg);
            var ex = _exContext.Exception as ValidationException;
            foreach (var error in ex.Errors)
            {
                valExcInfo.Append(error.PropertyName);
                valExcInfo.Append(": ");
                valExcInfo.Append(error.ErrorMessage);
                valExcInfo.Append("\n");
            }

            _exContext.Result =  new TextErrorResult(System.Net.HttpStatusCode.BadRequest, valExcInfo.ToString());
        }
    }
}