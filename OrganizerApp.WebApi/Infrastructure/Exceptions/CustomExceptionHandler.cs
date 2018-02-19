using FluentValidation;
using OrganizerApp.WebApi.Infrastructure.Exceptions.ExceptionHandlers.Implementations;
using OrganizerApp.WebApi.Infrastructure.Exceptions.ExceptionHandlers.Interfaces;
using OrganizerApp.WebApi.Resources.Languages;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net;
using System.Text;
using System.Web.Http.ExceptionHandling;


namespace OrganizerApp.WebApi.Infrastructure.Exceptions
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            IConcreteExceptionHandler exceptionHandler = null;
            var exception = context.Exception;

            if (exception is Helpers.ValidationException)
            {
                string message = new StringBuilder().Append(LocalizedText.ValidationFailedMainCommunicate)
                                                    .Append(" \n")
                                                    .Append(context.Exception.Message)
                                                    .ToString();

                exceptionHandler = new UniversalExceptionHandler(context, message, HttpStatusCode.BadRequest);
            }
            else if (context.Exception is DbEntityValidationException)
            {
                exceptionHandler = new DbEntityValidationExceptionHandler(context);
            }
            else if (context.Exception is ValidationException)
            {
                exceptionHandler = new FluentValidationExceptionHandler(context);
            }
            else
            {
                exceptionHandler = new UniversalExceptionHandler(context);
            }

            exceptionHandler.Handle();
        }
    }
}