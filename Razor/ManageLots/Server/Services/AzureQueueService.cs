using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using ManageLots.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ManageLots.Server.Services
{
    public class AzureQueueService
    {
        public string AzureStorageConnectionString { get; init; }
        public string QueueName { get; set; }
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public AzureQueueService()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented=true
            };
        }
        private async Task<QueueClient> CreateQueueClientAsync()
        {
            QueueServiceClient queueServiceClient =
                new QueueServiceClient(AzureStorageConnectionString);
            QueueClient queueClient = queueServiceClient.GetQueueClient(QueueName);
            await queueClient.CreateIfNotExistsAsync();
            return queueClient;
        }

        public async Task <IEnumerable<LotGetModel>> GetAllLots()
        {
            QueueClient queueClient = await GetQueueClientAsync();
            QueueMessage[] queueMessages = (await queueClient.ReceiveMessagesAsync(
                maxMessages: 10,
                visibilityTimeout: TimeSpan.FromSeconds(1))).Value;
            IEnumerable<LotGetModel> lots = queueMessages.Select(qm =>
               {
                   LotGetModel lotGetModel = JsonSerializer.Deserialize<LotGetModel>(qm.Body.ToString());
                   lotGetModel.MessageId = qm.MessageId;
                   lotGetModel.PopReceipt = qm.PopReceipt;
                   return lotGetModel;
               });
            return lots;
        }

        private Task<QueueClient> GetQueueClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}
