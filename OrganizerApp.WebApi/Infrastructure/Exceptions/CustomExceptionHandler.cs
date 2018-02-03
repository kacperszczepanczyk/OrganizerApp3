using FluentValidation;
using OrganizerApp.WebApi.Infrastructure.Exceptions.ExceptionHandlers.Implementations;
using OrganizerApp.WebApi.Infrastructure.Exceptions.ExceptionHandlers.Interfaces;
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

            if (exception is KeyNotFoundException || exception is ArgumentOutOfRangeException)
            {
                exceptionHandler = new UniversalExceptionHandler(context, context.Exception.Message, HttpStatusCode.BadRequest);
            }
            else if (context.Exception is DbEntityValidationException)
            {
                exceptionHandler = new DbEntityValidationExceptionHandler(context);
            }
            else if (context.Exception is ValidationException)
            {
                exceptionHandler = new ValidationExceptionHandler(context);
            }
            else
            {
                exceptionHandler = new UniversalExceptionHandler(context);
            }

            exceptionHandler.Handle();
        }
    }
}