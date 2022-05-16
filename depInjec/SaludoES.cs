namespace depInjec
{
    public class SaludoES : ISaludos
    {
        public string Hola(string nombre)
        {
            return "Hola " + nombre + "!";
        }
    }
}