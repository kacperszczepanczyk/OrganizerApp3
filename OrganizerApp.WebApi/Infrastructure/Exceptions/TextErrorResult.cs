using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OrganizerApp.WebApi.Infrastructure.Exceptions
{
    public class TextErrorResult : IHttpActionResult
    {
        public readonly HttpStatusCode StatusCode;
        public readonly string Description;


        public TextErrorResult(HttpStatusCode statusCode, string description)
        {
            Description = description;
            StatusCode = statusCode;
        }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = StatusCode,
                Content = new StringContent("status code: " + (int)StatusCode + "\ndescription: " + Description)
            };

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(response);
        }
    }
}