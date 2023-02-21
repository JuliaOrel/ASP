using ASP_DZ_2_Model.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var sp = scope.ServiceProvider;
                MoviesContext context = sp.GetRequiredService<MoviesContext>();
                var webHostEnv = sp.GetRequiredService<IWebHostEnvironment>();
                var conf = sp.GetRequiredService<IConfiguration>();
                await SeedData.Initialize(
                    serviceProvider: sp,
                    webHostEnvironment: webHostEnv,
                    conf);
            }

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                });
    }
}
