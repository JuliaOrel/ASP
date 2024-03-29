﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace AzureWebJobsEx
{
    class Program
    {
        static void Main(string[] args)
        {
            HostBuilder builder = new HostBuilder();
            builder.ConfigureWebJobs(configure =>
            {
                configure.AddAzureStorageCoreServices();
                configure.AddAzureStorageBlobs();
                configure.AddAzureStorageQueues();
            });
            builder.ConfigureLogging((context, configure) =>
            {
                configure.AddConsole();
                string appInsightsKey = context.Configuration.GetSection("AppInsightsKey").Value;
                if(string.IsNullOrEmpty(appInsightsKey))
                {
                    configure.AddApplicationInsightsWebJobs(a => a.InstrumentationKey = appInsightsKey);
                }
            });

            IHost host = builder.Build();
            using(host)
            {
                host.Run();
            }
        }
    }
}
