using apiDoble.Models;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiDoble.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AleatorioController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> EnviarAsync([FromBody] Aleatorio aleatorio)
        {

            string connectionStringImpar = "Endpoint=sb://serviceparcial.servicebus.windows.net/;SharedAccessKeyName=enviar;SharedAccessKey=uTr8jT99xnTdzaCOWQGyIUrxJgZgnWtGUrfdHTry/M4=;EntityPath=qimpar";
            string queueNameImpar = "qimpar";
            string connectionStringPar = "Endpoint=sb://serviceparcial.servicebus.windows.net/;SharedAccessKeyName=enviar;SharedAccessKey=pOIXetI2MmqLgsi9jxvH5f/evOy2HWXMljqB7jipUoE=;EntityPath=qpar";
            string queueNamePar = "qpar";
            string mensaje = JsonConvert.SerializeObject(aleatorio);

            Console.WriteLine(mensaje);
            if (parImpar(aleatorio.ValorRandom)) {
                // create a Service Bus client 
                await using (ServiceBusClient client = new ServiceBusClient(connectionStringPar))
                {
                    // create a sender for the queue 
                    ServiceBusSender sender = client.CreateSender(queueNamePar);

                    // create a message that we can send
                    ServiceBusMessage message = new ServiceBusMessage(mensaje);

                    // send the message
                    await sender.SendMessageAsync(message);
                    Console.WriteLine($"Sent a single message to the queue: {queueNamePar}");
                }
            }//Par
            else
            {
                await using (ServiceBusClient client = new ServiceBusClient(connectionStringImpar))
                {
                    // create a sender for the queue 
                    ServiceBusSender sender = client.CreateSender(queueNameImpar);

                    // create a message that we can send
                    ServiceBusMessage message = new ServiceBusMessage(mensaje);

                    // send the message
                    await sender.SendMessageAsync(message);
                    Console.WriteLine($"Sent a single message to the queue: {queueNameImpar}");
                }
            } //Impar
            return true;
        }

        public Boolean parImpar(int v) {
            if (v % 2 == 0)
            {
               return true;
            }
            else
            {
                return false;
           }
        } //Funcion par impar
    }
}
