using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureWebJobsEx
{
    public class Functions
    {
        public static void HandlerQueueMessage(
            [QueueTrigger("some-messages")] string message, ILogger logger)
        {
            logger.LogInformation(message);
        }
    }
}
