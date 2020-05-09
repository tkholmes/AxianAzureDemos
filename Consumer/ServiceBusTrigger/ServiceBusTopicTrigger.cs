using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Ocp.Demos.Consumer.ServiceBusTopicTrigger
{
    public static class ServiceBusTopicTriggerExample
    {
        [FunctionName("ServiceBusTopicTrigger")]
        public static void Run([ServiceBusTrigger("product-created", "izzy-product-created", Connection = "ConnectionString")]
          string message, int deliveryCount, string lockToken, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {message}");
        }
    }
}
