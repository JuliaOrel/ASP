using CognitiveServices.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<AzureBlobService>(factory =>
            {
                string azureConnectionString = Configuration.GetValue<string>("Azure: BlobStorage:ConnectionString");
                string blobContainerName = Configuration.GetValue<string>("Azure: BlobStorage:ConnectionString");

                return new AzureBlobService(azureConnectionString, blobContainerName);
            });

            services.AddScoped<AzureComputerVisionService>(factory =>
            {
                string key = Configuration.GetValue<string>("Azure: ComputerVision:Key");
                string endPoint = Configuration.GetValue<string>("Azure: ComputerVision:EndPoint");

                return new AzureComputerVisionService(key, endPoint);
            });

            services.AddScoped<TranslationService>(FACTORY =>
            {
                string key = Configuration.GetValue<string>("Azure:Translator:Key");
                string region = Configuration.GetValue<string>("Azure:Translator:Region");
                string endpoint = Configuration.GetValue<string>("Azure:Translator:Endpoint");

                return new TranslationService(key, region, endpoint);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
