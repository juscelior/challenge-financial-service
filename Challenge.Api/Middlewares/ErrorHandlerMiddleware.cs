using Challenge.Api.Model;
using Challenge.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Challenge.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ErrorHandlerMiddleware(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            var contextException = context.Features.Get<IExceptionHandlerFeature>();

            if (contextException != null)
            {
                if (contextException.Error is AccountException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync(new ErrorDetailsModel()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextException.Error.Message
                    }.ToString());
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(new ErrorDetailsModel()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error."
                    }.ToString());
                }
            }
        }
    }
}
