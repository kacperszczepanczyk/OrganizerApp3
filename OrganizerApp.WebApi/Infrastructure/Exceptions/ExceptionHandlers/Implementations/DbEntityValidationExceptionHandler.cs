using OrganizerApp.WebApi.Infrastructure.Exceptions.ExceptionHandlers.Interfaces;
using OrganizerApp.WebApi.Resources.Languages;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace OrganizerApp.WebApi.Infrastructure.Exceptions.ExceptionHandlers.Implementations
{
    public class DbEntityValidationExceptionHandler : IConcreteExceptionHandler
    {
        private readonly ExceptionHandlerContext _exContext;

        public DbEntityValidationExceptionHandler(ExceptionHandlerContext exContext)
        {
            _exContext = exContext;
        }


        public void Handle()
        {
            var ex = _exContext.Exception as DbEntityValidationException;
            string customExcMsg = LocalizedText.ValidationFailedMainCommunicate;
            StringBuilder valExcInfo = new StringBuilder();
            if (ex != null)
            {
                foreach (var exceptionError in ex.EntityValidationErrors)
                {
                    foreach (var exceptionErr in exceptionError.ValidationErrors)
                    {
                        valExcInfo
                            .Append(exceptionErr.PropertyName)
                            .Append(": ")
                            .Append(exceptionErr.ErrorMessage)
                            .Append("\n");
                    }
                }
            }
            _exContext.Result = new TextErrorResult(System.Net.HttpStatusCode.BadRequest, customExcMsg + "\n" + valExcInfo);
        }
    }
}