using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting1.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public AuthenticationMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            string token = httpContext.Request.Query["token"];
            if(string.IsNullOrWhiteSpace(token))
            {
                httpContext.Response.StatusCode = 403;
            }
            else
            {
                await _requestDelegate.Invoke(httpContext);
            }
        }
    }
}
