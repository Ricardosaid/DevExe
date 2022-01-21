﻿using System;
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

            //Creamos un unico nombre para el contenedor

            string containerName = "Demoblob" + Guid.NewGuid().ToString();

            //Creamos el contenedor y regresamos un objeto de cliente contenedor

            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

            //Mandamos a imprimir el nombre del contenedor y finalizamos

            Console.WriteLine("El contenedor" + containerName + "se ha creado " + 
            "\n Tomara algunos minutos verificarlo");
            Console.WriteLine("Presione una llave para continuar");
            Console.ReadLine();

        }
    }
}
