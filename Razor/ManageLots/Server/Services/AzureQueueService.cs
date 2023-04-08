using Azure;
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
            QueueClient queueClient = await CreateQueueClientAsync();
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

        public async Task<LotGetModel> AddLot(LotAddModel lotAddModel)
        {
            if(lotAddModel is null)
            {
                return null;
            }
            QueueClient queueClient = await CreateQueueClientAsync();
            string json = JsonSerializer.Serialize(lotAddModel, _jsonSerializerOptions);
            SendReceipt response=await queueClient.SendMessageAsync(messageText: json, timeToLive: TimeSpan.FromDays(1));
            LotGetModel lotGetModel = new LotGetModel
            {
                Currency = lotAddModel.Currency,
                Amount = lotAddModel.Amount,
                Seller = lotAddModel.Seller,
                MessageId = response.MessageId,
                PopReceipt = response.PopReceipt
            };
            return lotGetModel;
        }

        public async Task<Response> DeleteMessageAsync(string messgeId, string popReceipt)
        {
            //QueueServiceClient queueServiceClient =
            //   new QueueServiceClient(AzureStorageConnectionString);
            //QueueClient queueClient = queueServiceClient.GetQueueClient(QueueName);
            //await queueClient.CreateIfNotExistsAsync();
            QueueClient queueClient = await CreateQueueClientAsync();
            Response result = await queueClient.DeleteMessageAsync(messgeId, popReceipt);
            return result;
        }
    }
}
