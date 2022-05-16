using System;

namespace depInjec
{
    public class Cliente
    {
        public void SaludarAUnaPersona(AbstraccionSaludo abs)
        {
            string nombre = "";

            while (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Ingrese su nombre");
                nombre = Console.ReadLine();
            }

            string saludo = abs.Saludar(nombre);
            Console.WriteLine(saludo);
        }
    }
}