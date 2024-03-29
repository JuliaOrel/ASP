using CarsBlazorClient.HttpServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarsBlazorClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient("CarsHttpClient", configure =>
            {
                configure.BaseAddress = new Uri("https://localhost:5001/api/");
            });

            builder.Services.AddHttpClient<ICompanyHttpService, CompanyHttpService>(configure =>
            {
                configure.BaseAddress= new Uri("https://localhost:5001/api/");
            });
            builder.Services.AddScoped<ICarHttpService, CarHttpService>();
            await builder.Build().RunAsync();
        }
    }
}
