using System.Text.RegularExpressions;

namespace TeachMe.Core.Utils
{
    public static class EmailUtils
    {
        // Código retirado desse thread: https://stackoverflow.com/questions/5342375/regex-email-validation 
        public static bool EmailValido(string email)
        {
            var reg = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            return reg.IsMatch(email);
        }
    }
}
