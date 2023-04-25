using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Data.Tables;
using UrlShortener.Models;
using System.Linq;

namespace UrlShortener
{
    public static class Function1
    {
        [FunctionName("Set")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Table("ShortUrls")] TableClient tableClient,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string address = req.Query["address"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            address = address ?? data?.name;
            if(string.IsNullOrEmpty(address))
            {
                return new OkObjectResult("Parameter 'address' was not written\n");
            }

            UrlKey urlKey = null;
            var result = await tableClient.GetEntityIfExistsAsync<UrlKey>("1", "Key");
            if(result.HasValue==false)
            {
                urlKey = new UrlKey
                {
                    Id = 1000,
                    PartitionKey = "1",
                    RowKey = "Key"
                };
                await tableClient.UpsertEntityAsync(urlKey);
            }
            else
            {
                urlKey = result.Value;
            }
            int index = urlKey.Id;
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string code = string.Empty;
            while(index>0)
            {
                code += alphabet[index % alphabet.Length];
                index /= alphabet.Length;
            }
            code = string.Join("", code.Reverse());
            urlKey.Id++;
            await tableClient.UpsertEntityAsync<UrlKey>(urlKey);
            UriData uriData = new UriData
            {
                Id = code,
                Url = address,
                Count = 1,
                PartitionKey=$"{code[0]}",
                RowKey=code,
            };

            await tableClient.UpsertEntityAsync<UriData>(uriData);
            return new OkObjectResult("Original url == " + uriData.Url +"\n"+
                "Shorted URL == "+ uriData.PartitionKey);

        }

        [FunctionName("Redirect")]
        public static async Task<IActionResult> Redirect(
            [HttpTrigger(
            AuthorizationLevel.Anonymous,
            "get",
            "post",
            Route ="Redirect/{shortUrl}")] HttpRequest httpRequest,
            string shortUrl,
            [Table("ShortUrls")] TableClient tableClient,
            [Queue("requestCount")] IAsyncCollector<string> queue,
            ILogger logger)
        {
            if(string.IsNullOrWhiteSpace(shortUrl))
            {
                return new BadRequestResult();
            }
            shortUrl = shortUrl.ToUpper();
            var result = await tableClient
                .GetEntityIfExistsAsync<UriData>(shortUrl[0].ToString(), shortUrl);
            string url = "https://www.google.com.ua/?hl=uk";
            if(result.HasValue && result.Value is UriData data)
            {
                url = data.Url;
                await queue.AddAsync(data.RowKey);
            }

            return new RedirectResult(url);
        }

        [FunctionName("QueueShortUrlHandler")]
        public static async Task QueueShortUrlHandler(
            [QueueTrigger("requestsCount")] string shortUrl,
            [Table("ShortUrl")] TableClient tableClient,
            ILogger logger)
        {
            var result = await tableClient
                .GetEntityIfExistsAsync<UriData>(shortUrl[0].ToString(), shortUrl);
            if (result.HasValue && result.Value is UriData data)
            {
                data.Count++;
                await tableClient.UpsertEntityAsync<UriData>(data);

            }
            }
    }
}
