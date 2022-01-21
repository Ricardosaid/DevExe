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

            //Obtenemos la referencia para el blob

            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            Console.WriteLine("Actualizando el blob stora de la direccion {0}", blobClient.Uri);

            //Abrimos el archivo y actualizamos la data

            using FileStream uploadFileStream = File.OpenRead(localFilePath);
            await blobClient.UploadAsync(uploadFileStream, true);
            //No olvidar cerrar el archivo localfilepath
            uploadFileStream.Close();

            Console.WriteLine("\nEl archivo ha sido actualizado\n");
            Console.WriteLine("Presione una tecla para continuar");
            Console.ReadLine();

            /*****************************************************************************/

            //Listamos los blob dentro del contenedor

            Console.WriteLine("\n Listando los blob ... \n");

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine("\t " + blobItem.Name);

            }

            Console.WriteLine("presione una tecla para continuar");
            Console.ReadLine();

            /************************************************************************************************/

            //Descargar el blbob creado a mi sistema local

            // agregamos el Downloaded para diferenciarlo 
            string downloadFilePath = localFilePath.Replace(".txt","Downloaded.txt");

            Console.WriteLine("\n Descargando el blob \n ", downloadFilePath);

            BlobDownloadInfo download = await blobClient.DownloadAsync();

            using (FileStream downloadFileStream = File.OpenWrite(downloadFilePath))
            {
                await download.Content.CopyToAsync(downloadFileStream);
                //cerrar file
                downloadFileStream.Close();
            }

            Console.WriteLine("\nCuando presione se va a borrar el contendor\n");

            Console.WriteLine("Presiona una tecla para continuar");
            Console.ReadLine();


            /********************************************************************************************/

            //Eliminamos un contenddr
            
            await containerClient.DeleteAsync();

            Console.WriteLine("Eliminando la fuente local y archivos descargados");

            File.Delete(localFilePath);
            File.Delete(downloadFilePath);

            Console.WriteLine("Terminado y limpio");



        }
    }
}
