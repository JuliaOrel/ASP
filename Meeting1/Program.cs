using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //.Run(async (HttpContext context) =>
            //{
            //    var response = context.Response;
            //    response.Headers.ContentLanguage = "uk-UK";
            //    response.Headers.ContentType = "text/plain; charset=utf-8";
            //    response.Headers.ContentType = "application/json; charset=utf-8";
            //    string path = context.Request.Path;
            //    string query = context.Request.QueryString.Value;
            //    string cat = context.Request.Query["cat"];
            //    await context.Response.WriteAsync(cat);
            //});
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
