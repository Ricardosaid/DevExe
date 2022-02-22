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

        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
