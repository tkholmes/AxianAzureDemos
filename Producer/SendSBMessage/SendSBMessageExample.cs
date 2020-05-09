using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace OCP.Demos.Producer
{
    /*
      Hello World example that puts a message on a Service Bus Topic.
    */
    class SendSBMessageExample
    {
        // Replace this with your SB connection string. Source from Azure. This would normally be pulled from config (seeded via ARM template or KeyVault [secret management]).
        private const string ServiceBusConnectionString = "Endpoint=sb://ocp-webenv-sb-tkh.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hENtya1cp4fZMsHfb1EkrjxQB4zbx3kpECoEPQSakYM=";

        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await Send(123, "Send this message to the Service Bus (Hello World).");
            }); 

            Console.WriteLine("Message sent, press ENTER to quit.");
            Console.Read();
        }

        public static async Task Send(int id, string messageToSend)
        {
            try
            {
                var messageObject = new { Id = id, Message = messageToSend };

                var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageObject)));

                var topicClient = new TopicClient(ServiceBusConnectionString, "product-created");

                await topicClient.SendAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
