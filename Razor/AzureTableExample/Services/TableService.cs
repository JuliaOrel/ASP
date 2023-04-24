using Azure;
using Azure.Data.Tables;
using AzureTableExample.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTableExample.Services
{
    public class TableService: ITableService
    {
        private readonly string _tableName = "ProductsTable";
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ILogger<TableService> _logger;

        public TableService(IConfiguration configuration, ILogger<TableService> logger)
        {
            _connectionString = _configuration
                .GetSection("AzureStorageConnectionString").Value;
            _logger = logger;
        }

        private async Task<TableClient> GetTableClient()
        {
            TableServiceClient tableServiceClient =
                new TableServiceClient(_connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(_tableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;

        }
        public async Task DeleteProduct(string id, string category)
        {
            TableClient tableClient = await GetTableClient();
            var response = await tableClient.DeleteEntityAsync(category, id);
            _logger.LogInformation($"Response: {response.Status}");
        }

        public async Task<Product> GetProduct(string id, string category)
        {
            TableClient tableClient = await GetTableClient();
            var product = await tableClient.GetEntityAsync<Product>(category, id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts(string category = null)
        {
            TableClient tableClient = await GetTableClient();
            AsyncPageable<Product> results;
            if(category is null)
            {
                results = tableClient
                    .QueryAsync<Product>();
            }
            else
            {
                results = tableClient.QueryAsync<Product>(p => p.PartitionKey == category);
            }
            IList<Product> products = new List<Product>();
            await foreach (Product product in results)
            {
                products.Add(product);
            }
            return products;

        }

        public async Task<Product> UpsertProduct(Product product)
        {
            TableClient tableClient = await GetTableClient();
            var response=await tableClient.UpsertEntityAsync(product);
            _logger.LogInformation($"Response: {response.Status}");
            return product;
        }
    }
}
