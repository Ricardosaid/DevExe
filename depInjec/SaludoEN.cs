namespace depInjec
{
    public class SaludoEN : ISaludos
    {
        public string Hola(string nombre)
        {
            return "Hello " + nombre + "!";
        }
    }
}