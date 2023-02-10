using ASP_DZ_4_DI.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_4_DI.Middleware
{
    public class WarriorMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public WarriorMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context, WarriorService warriorService)
        {
            await context.Response.WriteAsync(warriorService.KillMethod());
        }
    }
}
