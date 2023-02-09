using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting1.Middlewares
{
    public class RoutingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public RoutingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            string path = httpContext.Request.Path;
            if(path=="/blog")
            {
                await httpContext.Response.WriteAsync("blog page");
            }
            else if(path=="/post")
            {
                await httpContext.Response.WriteAsync("post page");
            }
            else
            {
                httpContext.Response.StatusCode = 404;
            }
        }
    }
}
