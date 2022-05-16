using System;

namespace depInjec
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstraccionSaludo abs;

            Cliente cli = new Cliente();

            abs = new AbstraccionSaludo(new SaludoES());
            cli.SaludarAUnaPersona(abs);

            abs = new AbstraccionSaludo(new SaludoEN());
            cli.SaludarAUnaPersona(abs);

        }
    }
}
