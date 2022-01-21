using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BlobConnection
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Azure blob strogae exercise");

            // Corremos el ejemplo asincronicamente

            ProcessAsync().GetAwaiter().GetResult();

            Console.WriteLine("Presiona una tecla para salir del ejercicio");
            Console.ReadLine();
        }

        private static async Task ProcessAsync()
        {
            string storageConnectionString = "CONNECTION STRING";

            //Crea un cliente que pueda autenticarce con la cadena de conexión
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);

        }
    }
}
