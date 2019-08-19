using Challenge.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Challenge.Api.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        readonly string apiKey;

        public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            apiKey = configuration["ApiKey"];
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers["Api-Key"].FirstOrDefault() == apiKey)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(new ErrorDetailsModel()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Check the ApiKey."
                }.ToString());
            }
        }
    }

}
