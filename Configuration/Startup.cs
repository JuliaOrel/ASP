using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Configuration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppConfiguration = configuration;
            //var builder = new ConfigurationBuilder()
            //    .AddInMemoryCollection(new Dictionary<string, string>
            //    {
            //        {"firstname", "Julia"},
            //        {"age", "34" }
            //    });
            //AppConfiguration = builder.Build();
        }
        public IConfiguration AppConfiguration;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //AddInMemoryCollection

            //app.Run(async (context) => //���� ��������� AddInMemoryCollection
            //{
            //    await context.Response.WriteAsync(AppConfiguration["firstname"]);
            //});

            //������ �� ������������

            //AppConfiguration["blog"] = "MyBlog";
            //AppConfiguration["post"] = "MyPost";

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync($"{AppConfiguration["blog"]}");
            //    });
            //});

            app.UseEndpoints(endpoints =>
            {
                app.endpoints.MapGet("/", (IConfiguration configuration)=>$"{configuration["name"]}");
            });
        }
    }
}
