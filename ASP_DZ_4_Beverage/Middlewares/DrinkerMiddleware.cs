using ASP_DZ_4_Beverage.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_4_Beverage.Middlewares
{
    public class DrinkerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public DrinkerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context, DrinkerService drinkerService)
        {
            await context.Response
                .WriteAsync(drinkerService.DrinkMethod());
        }
    }
}
