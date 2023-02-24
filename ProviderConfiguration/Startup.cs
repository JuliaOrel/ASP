using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ProviderConfiguration.Extentions;
using ProviderConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderConfiguration
{
    public class Startup
    {
        private readonly IConfiguration AppConfiguration;
        public Startup(IConfiguration configuration)
        {
            AppConfiguration = configuration;
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json");
                //.AddMyTextFileConfig("config.txt");
            //User user = new User();
            //AppConfiguration.Bind(user);
            User user = AppConfiguration.Get<User>();
            //Blog blog = AppConfiguration.Get<Blog>();
            //IList<Post> posts = AppConfiguration.GetSection("posts").Get<IList<Post>>();

            AppConfiguration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Blog>(AppConfiguration); //записывает в конфигурацию этот объект и создает объект IOptions
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(
            //        AppConfiguration["name"]);
            //});

            //app.UseEndpoints(endpoints =>  //???????
            //{
            //    endpoints.MapGet("/", (IOptions <Blog> options) =>
            //    {
            //        Blog blog = options.Value;
            //    });
            //});

            //app.Run (IOptions<Blog> options) =>
            // {
            //     Blog blog = options.Value;
            // });
        }
    }
}
