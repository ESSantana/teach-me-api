using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using TeachMe.Repository.Repositories.Interfaces;

namespace TeachMe.Repository.Repositories
{
    public class EmailRepositorio : IEmailRepositorio
    {
        #region Constantes Email
        private readonly string ARQUIVO_CONFIG = "appsettings.json";
        private readonly string FRONTEND_URL = "AplicacaoConfig:Front";
        private readonly string HOST = "EmailOptions:Host";
        private readonly string PORTA = "EmailOptions:Porta";
        private readonly string LOGIN = "EmailOptions:Login";
        private readonly string SENHA = "EmailOptions:Senha";
        private readonly string MENSAGEM_CADASTRO = "Seja bem vindo ao Teach Me {0}. Antes de continuar para o acesso a plataforma, confirme seu cadastro clicando no link a seguir <a href=\"{1}/validarCadastro?cadastro={2}\">Validar cadastro</a> ";
        #endregion

        public EmailRepositorio() { }

        public void NotificarCadastro(string email, string nome, Guid validacaoId)
        {
            var parametros = ParametrosServidor();
            var servidor = IniciarCliente(parametros);
            MandarEmail(servidor, email, nome, parametros[LOGIN], validacaoId);
        }

        private void MandarEmail(SmtpClient servidor, string email, string nome, string emailRemetente, Guid validacaoId)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(emailRemetente);
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            message.Priority = MailPriority.High;

            message.To.Add(email);

            message.Subject = "Bem vindo ao Teach Me";
            message.Body = string.Format(MENSAGEM_CADASTRO, nome, ParametrosServidor(FRONTEND_URL)[FRONTEND_URL], validacaoId);
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

        private IDictionary<string, string> ParametrosServidor(string propriedade = "")
        {
            string directory = Directory.GetCurrentDirectory();

            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(directory).AddJsonFile(ARQUIVO_CONFIG);
            IConfigurationRoot configuration = builder.Build();

            if (!string.IsNullOrEmpty(propriedade))
            {
                return new Dictionary<string, string> { { propriedade, configuration.GetSection(propriedade)?.Value } };
            }

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
