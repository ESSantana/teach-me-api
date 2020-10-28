using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace TeachMe.Repository.Repositories
{
    public class EmailRepositorio
    {
        private readonly string ARQUIVO_CONFIG = "appsettings.json";
        private readonly string HOST = "EmailOptions:Host";
        private readonly string PORTA = "EmailOptions:Porta";
        private readonly string LOGIN = "EmailOptions:Login";
        private readonly string SENHA = "EmailOptions:Senha";

        public EmailRepositorio()
        {
        }

        public void NotificarCadastro(string email, string nome)
        {
            var parametros = ParametrosServidor();
            var servidor = IniciarCliente(parametros);
            MandarEmail(servidor, email, nome, parametros[LOGIN]);
        }


        private void MandarEmail(SmtpClient servidor, string email, string nome, string emailRemetente)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(emailRemetente);
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            message.Priority = MailPriority.High;

            message.To.Add(email);

            message.Subject = "Bem vindo ao Teach Me";
            message.Body = "Antes de continuar para o acesso a plataforma, confirme seu cadastro no link abaixo";
            message.IsBodyHtml = true;

            servidor.Send(message);
        }

        private SmtpClient IniciarCliente(IDictionary<string, string> parametros)
        {
            SmtpClient client = new SmtpClient();
            client.Host = parametros[HOST];
            client.Port = int.Parse(parametros[PORTA]);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(parametros[LOGIN], parametros[SENHA]);

            return client;
        }

        private IDictionary<string, string> ParametrosServidor()
        {
            string directory = Directory.GetCurrentDirectory();

            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(directory).AddJsonFile(ARQUIVO_CONFIG);
            IConfigurationRoot configuration = builder.Build();

            IDictionary<string, string> configParametros = new Dictionary<string, string>
            {
                { HOST, configuration.GetSection(HOST)?.Value},
                { PORTA, configuration.GetSection(PORTA)?.Value},
                { LOGIN, configuration.GetSection(LOGIN)?.Value},
                { SENHA, configuration.GetSection(SENHA)?.Value}
            };

            return configParametros;
        }
    }

}
