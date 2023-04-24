using Azure;
using Azure.Data.Tables;
using AzureTableDoctors.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTableDoctors.Services
{
    public class TableService : ITableService
    {
        private readonly string _tableName = "MedicinesTable";
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ILogger<TableService> _logger;
        public TableService(IConfiguration configuration, ILogger<TableService> logger)
        {
            _configuration = configuration;
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
        public async Task DeleteMedicine(string id, string category)
        {
            TableClient tableClient = await GetTableClient();
            var response = await tableClient.DeleteEntityAsync(category, id);
            _logger.LogInformation($"Response: {response.Status}");
        }
    

        public async Task<Medicine> GetMedicine(string id, string category)
        {
            TableClient tableClient = await GetTableClient();
            var medicine = await tableClient.GetEntityAsync<Medicine>(category, id);
            return medicine;
        }

        public async Task<IEnumerable<Medicine>> GetMedicines(string category = null)
        {
            TableClient tableClient = await GetTableClient();
            AsyncPageable<Medicine> results;
            if (category is null)
            {
                results = tableClient
                    .QueryAsync<Medicine>();
            }
            else
            {
                results = tableClient.QueryAsync<Medicine>(p => p.PartitionKey == category);
            }
            IList<Medicine> medicines = new List<Medicine>();
            await foreach (Medicine medicine in results)
            {
                medicines.Add(medicine);
            }
            return medicines;
        }

        public async Task<Medicine> UpsertMedicine(Medicine medicine)
        {
            TableClient tableClient = await GetTableClient();
            var response = await tableClient.UpsertEntityAsync(medicine);
            _logger.LogInformation($"Response: {response.Status}");
            return medicine;
        }
    }
}
