using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TeachMe.Core.Utils
{
    public static class SenhaUtils
    {
        public static string EncriptarSenha(string senha)
        {
            var encriptador = SHA256.Create();

            var hash = encriptador.ComputeHash(Encoding.ASCII.GetBytes(senha)).ToList();

            return string.Join("", hash.Select(x => x.ToString("X2")).ToList()).ToLower();
        }
    }
}
