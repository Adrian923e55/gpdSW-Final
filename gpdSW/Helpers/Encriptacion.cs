using System.Security.Cryptography;
using System.Text;

namespace gpdSW.Helpers
{
    public class Encriptacion
    {
        public static string Encriptar(string cadena)
        {
            cadena = cadena + "Qu35o5";
            byte[] datos = Encoding.UTF8.GetBytes(cadena);
            var alg = SHA512.Create();
            byte[] encriptar = alg.ComputeHash(datos);
            string salida = BitConverter.ToString(encriptar).Replace("-", "");

            return salida;
        }
    }
}
