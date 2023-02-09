using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting1.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public ErrorMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;

        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _requestDelegate.Invoke(httpContext);
            if(httpContext.Response.StatusCode==StatusCodes.Status403Forbidden)
            {
                await httpContext.Response.WriteAsync("Access Denied");
            }
            else if(httpContext.Response.StatusCode==StatusCodes.Status404NotFound)
            {
                await httpContext.Response.WriteAsync("Not found");
            }
        }

    }
}
