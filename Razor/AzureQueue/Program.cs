using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureQueue
{
    internal class Program
    {
        public class Member
        {
            public string MemberName { get; set; }
        }

        public class MemberToGet
        {
            public string MemberName { get; set; }
            public string MessageId { get; set; }
            public string PopReceipt { get; set; }
        }
        static async Task CreateQueueAsync(string cs, string queueName)
        {
            QueueServiceClient queueServiceClient = new QueueServiceClient(cs);
            QueueClient queueClient = await queueServiceClient.CreateQueueAsync(queueName);
        }
        static QueueClient GetQueueClient(string cs, string queueName)
        {
            QueueServiceClient queueServiceClient = new QueueServiceClient(cs);
            QueueClient queueClient = queueServiceClient.GetQueueClient(queueName);
            return queueClient;
        }
        static async Task SendMessageAsync(string cs, string queueName, string message)
        {
            QueueClient queueClient = GetQueueClient(cs, queueName);
            SendReceipt receipt = await queueClient.SendMessageAsync(messageText: message,
                 visibilityTimeout: TimeSpan.FromSeconds(0),
                 timeToLive: TimeSpan.FromDays(1));
            Console.WriteLine("Message was sent");
            Console.WriteLine(receipt.MessageId);
            Console.WriteLine(receipt.PopReceipt);

        }
        static  async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string connSString = "DefaultEndpointsProtocol=https;AccountName=teststoraccount14;AccountKey=ZiHjvVDLZjg3UtfVLGi5nzVrBS62YvmCaN+XmaksNsOok586GpRNo9V1zYw0ZjN4gJsbgRW98CA/+ASt7cP2Mw==;BlobEndpoint=https://teststoraccount14.blob.core.windows.net/;QueueEndpoint=https://teststoraccount14.queue.core.windows.net/;TableEndpoint=https://teststoraccount14.table.core.windows.net/;FileEndpoint=https://teststoraccount14.file.core.windows.net/";
            string queueName = "test-queue";
            await CreateQueueAsync(connSString, queueName);
            //await SendMessageAsync(connSString, queueName, "Message1");
            //await SendMessageAsync(connSString, queueName, "Message2");
            //await SendMessageAsync(connSString, queueName, "Message3");
            //await PeekMessageesAsync(connSString, queueName);
            //await ReceiveMessageAsync(connSString, queueName);
            //await DeleteMessageAsync(connSString, queueName);
            //await DeleteQueueAsync(connSString, queueName);
            Member member1 = new Member { MemberName = "Message1" };
            Member member2 = new Member { MemberName = "Message2" };
            Member member3 = new Member { MemberName = "Message3" };
            await SendMemberAsync(connSString, queueName, member1);
            await SendMemberAsync(connSString, queueName, member2);
            await SendMemberAsync(connSString, queueName, member3);

            IEnumerable<MemberToGet> members =
                await GetFromReceiveMemberAsync(connSString, queueName);
            foreach (MemberToGet item in members)
            {
                await DeleteMemberAsync(connSString, queueName, item.MessageId, item.PopReceipt);
            }
        }

        static async Task PeekMessageesAsync(string cs, string queueName)
        {
            QueueClient queueClient = GetQueueClient(cs, queueName);
            foreach (var message in (await queueClient.PeekMessagesAsync(maxMessages:10)).Value)
            {
                Console.WriteLine(message.Body);
                Console.WriteLine(message.MessageText);
                Console.WriteLine(message.MessageId);
                Console.WriteLine(message.DequeueCount);
                Console.WriteLine("----------------------------------\n");
            }
        }

        static async Task ReceiveMessageAsync(string cs, string queueName)
        {
            QueueClient queueClient = GetQueueClient(cs, queueName);
            foreach (var message in (await queueClient.ReceiveMessagesAsync(maxMessages:10, visibilityTimeout:TimeSpan.FromSeconds(1))).Value)
            {
                Console.WriteLine(message.ExpiresOn);
                Console.WriteLine(message.InsertedOn);
                Console.WriteLine(message.MessageText);
                Console.WriteLine(message.MessageId);
                Console.WriteLine(message.PopReceipt);

                //await queueClient.UpdateMessageAsync(
                //    messageId: message.MessageId,
                //    popReceipt: message.PopReceipt,
                //    messageText: message.MessageText+". New added text");
            }
        }
        static async Task DeleteMessageAsync(string cs, string queueName)
        {
            QueueClient queueClient = GetQueueClient(cs, queueName);
            foreach (var message in (await queueClient.ReceiveMessagesAsync(maxMessages:10, visibilityTimeout: TimeSpan.FromSeconds(1))).Value)
            {
                await queueClient.DeleteMessageAsync(messageId: message.MessageId, popReceipt: message.PopReceipt);
            }
        }

        static async Task DeleteQueueAsync(string cs, string queueName)
        {
            QueueClient queueClient = GetQueueClient(cs, queueName);
            await queueClient.DeleteIfExistsAsync();
        }

        static async Task SendMemberAsync(string cs, string queueName, Member message)
        {
            QueueClient queueClient = GetQueueClient(cs, queueName);
            string json = JsonSerializer.Serialize(message);
            await queueClient.SendMessageAsync(json);

        }

        static async Task<IEnumerable<MemberToGet>>GetFromReceiveMemberAsync(string cs, string queueName)
        {
            QueueClient queueClient = GetQueueClient(cs, queueName);
            Response<QueueMessage[]> response =
                await queueClient.ReceiveMessagesAsync(
                    maxMessages: 10,
                    visibilityTimeout: TimeSpan.FromSeconds(1));

            QueueMessage[] queueMessages = response.Value;
            IEnumerable<MemberToGet> members = queueMessages.Select(qm =>
              {
                  MemberToGet member =
                  JsonSerializer.Deserialize<MemberToGet>(qm.Body.ToString());
                  member.MessageId = qm.MessageId;
                  member.PopReceipt = qm.PopReceipt;
                  return member;

              });
            return members;
        }

        static async Task DeleteMemberAsync(string cs, string queueName, string messageId, string popReceipt)
        {
            QueueClient queueClient = GetQueueClient(cs, queueName);
            Response responce=await queueClient.DeleteMessageAsync(messageId, popReceipt);
            Console.WriteLine(responce.Status);
        }

    }
    
   

 
}
