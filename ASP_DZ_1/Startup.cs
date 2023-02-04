using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                //app.Run(async (context) =>
                //{
                //    var rand = new Random();
                //    int letter = rand.Next(65, 90);
                //    char c = Convert.ToChar(letter);
                //    await context.Response.WriteAsync($"{c} {DateTime.Now.Day}");


                //});

                endpoints.MapControllerRoute(

                name: "default",
                pattern: "{Controller=Home}/{Action=Index}/{id?}");

            });
        }
    }
}
