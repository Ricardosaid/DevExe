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

            // Corremos el ejemplo asincronicamente, recordar que cada vez que corremos este metodo cremos un nuevo contenedor
            //Poner condicional para no crear inncecesarios

            ProcessAsync().GetAwaiter().GetResult();

            Console.WriteLine("Presiona una tecla para salir del ejercicio");
            Console.ReadLine();
        }

        private static async Task ProcessAsync()
        {
            string storageConnectionString = "CONNECTION STRING";

            //Crea un cliente que pueda autenticarce con la cadena de conexión
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);

            //Creamos un unico nombre para el contenedor (tomar en cuenta que el contenedor debe tener minusculas)

            string containerName = "demoblob" + Guid.NewGuid().ToString();

            //Creamos el contenedor y regresamos un objeto de cliente contenedor

            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

            //Mandamos a imprimir el nombre del contenedor y finalizamos

            Console.WriteLine("El contenedor" + containerName + "se ha creado " + 
            "\n Tomara algunos minutos verificarlo");
            Console.WriteLine("Presione una llave para continuar");
            Console.ReadLine();

            /************************************************************************************************/

            //Creamos un archivo local en el directorio ./data/ para actualizar y descargarlo

            string localPath = "./data/";
            //creamos el archivo
            string fileName = "demofile" + Guid.NewGuid().ToString() + ".txt";
            //Concatenamos los valores de arriba
            string localFilePath = Path.Combine(localPath,fileName);

            //Exribimos texto al archivo

            await File.WriteAllTextAsync(localFilePath,"Escribiendo texto ...");




        }
    }
}
