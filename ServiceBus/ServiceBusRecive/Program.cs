using Sysmtem.Threading.Taks;
using System;
using Azure.Messaging.Servicebus;

namespace ServiceBusRecive
{
    class Program
    {
        //Es la cadena de conexion de Namespace de service Bus
        static string connectionString = "";
        //Nombre de la cola del Service Bus
        static string queueName = "";  
        // El cliente que posee la conexion y puede ser usada para crear senders y recivers      
        static ServiceBusClient client;
        //El procesador que lee y procesa los mensajes que vienen de la queue
        static ServiceBusProcessor processor;
        
        static void Main(string[] args)
        {
            Console.WriteLine("");
        }

        //Controlador que recibe los mensajes
        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString(); // aqui está el cuerpo del mensaje en tipo string
            Console.WriteLine($"Recieved {body}");
            //Una vez completado el mensaje, este es borrado de la queue 
            await args.CompleteMessageAsync(args.Message);
        }

        // Controlador de cualquier eroor cuando se reciben los mensajes
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString()); // Lo imprime en tipo string
            return Task.CompletedTask;
        }
        
    }
}
