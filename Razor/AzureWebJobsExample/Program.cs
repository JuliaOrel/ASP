using Microsoft.Extensions.Hosting;
using System;

namespace AzureWebJobsExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HostBuilder builder = new HostBuilder();
            builder.ConfigureWebJobs(configure =>
            {
                configure.AddAzureStorageServices();         
            });
        }
    }
}
