using System;

namespace ServiceBus
{
    class Program
    {
        // Cadena de conexion para el namespace del service bus
        static string connectionString = "";
        // nombre de la cola del service bus
        static string queueName = "";
        // el cliente que posee la conexion y que puede ser usado para crear senders y receivers
        static ServiceBusClient client;
        //El sender encargado de publicar mensajes a la cola
        static ServiceBusSender sender;
        // numero de mensajes que deben ser enviados a la cola
        private const int numOfMessages = 5;


        static async task Main()
        {
            // El service bus client usa un singleton, the best practice cuando publicamos o leemos mensajes
            //Crea un ciente que se usará para enviar y procesar los mensajes
            client = new ServiceBusClient (connectionString);
            sender = client.CreateSender(queueName); //Sender es el obj que utilizartemos para enviar los msn a la queue

            //Creamos el batch (lote de mensajes)

            ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            // Agregamos los mensajes en un bucle

            for (int i = 1; i <= numOfMessages; i++)
            {
                //trataremos de agregar los mensajes al lote, new ServiceBusMessage -> crea un nuevo objeto mensaje para enviar
                //messageBatch.TryAddMessage -> intenta agregar un msn al lote, asegurando que el tamaño del lote no exceda su maximo, regresa un bool
                if(!messageBatch.TryAddMessage(new ServiceBusMessage ($"Message {i}")))
                {
                    //si es muy largo el lote, creamos excepcion
                    throw new Exception($"The message {i} es muy largo para ponerlo en el lote");
                }
            }






            Console.WriteLine("Hello World!");
        }
    }
}
