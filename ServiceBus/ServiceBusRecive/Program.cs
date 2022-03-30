using System.Threading.Tasks;
using System;
using Azure.Messaging.ServiceBus;

namespace ServiceBusRecive
{
    class Program
    {
        //Es la cadena de conexion de Namespace de service Bus
        static string connectionString = "Endpoint=sb://servicebusdemobfirst.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=41N/K1X1KnwoofRLbkqUOnkid/BGwwASITO1K8heJPs=";
        //Nombre de la cola del Service Bus
        static string queueName = "demoqueue";  
        // El cliente que posee la conexion y puede ser usada para crear senders y recivers      
        static ServiceBusClient client;
        //El procesador que lee y procesa los mensajes que vienen de la queue
        static ServiceBusProcessor processor;
        
        static async Task Main()
        {
            // creamos el objeto cliente del service bus
            client =  new ServiceBusClient(connectionString);

            //creamos el processor para manage los mensaje

            processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

            try
            {
                // se agrega el controlador para procesar los mensajes
                processor.ProcessMessageAsync += MessageHandler;

                // se agrega controlador para processar cualquier error
                processor.ProcessErrorAsync += ErrorHandler;

                //Se empieza el procesamiento
                await processor.StartProcessingAsync();

                Console.WriteLine("Espere un minuto y presione una tecla pata finalizar el proceso");
                Console.ReadKey();

                //Paramos el proceso
                Console.WriteLine("\n Parando el reciver");
                await processor.StopProcessingAsync();
                Console.WriteLine("Reciver de mensajes detenido");

            }
            finally
            {
                // garantizar que los rexursos de red y otros objetos no administrados se limpien correctamente
                await processor.DisposeAsync();
                await client.DisposeAsync();
                
            }
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
