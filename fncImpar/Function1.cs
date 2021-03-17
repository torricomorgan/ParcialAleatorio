using System;
using System.Threading.Tasks;
using fncImpar.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace fncImpar
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task RunAsync(
            [ServiceBusTrigger("qimpar", Connection = "MyConn")] string myQueueItem,
            [CosmosDB(databaseName: "dbImpar", collectionName: "Impares", ConnectionStringSetting = "strCosmos")] IAsyncCollector<object> aleatorio,
            ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
                var data = JsonConvert.DeserializeObject<Aleatorio>(myQueueItem);
                await aleatorio.AddAsync(data);
            }
            catch (Exception e)
            {
                log.LogError($"No fue posible insertar datos: {e.Message}");
            }
        }
    }
}
