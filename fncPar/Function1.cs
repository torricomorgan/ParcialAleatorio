using System;
using System.Threading.Tasks;
using fncPar.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace fncPar
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task RunAsync(
            [ServiceBusTrigger("qpar", Connection = "MyConn")] string myQueueItem,
            [CosmosDB(databaseName: "dbPar", collectionName: "Pares", ConnectionStringSetting = "strCosmos")] IAsyncCollector<object> aleatorio,
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
