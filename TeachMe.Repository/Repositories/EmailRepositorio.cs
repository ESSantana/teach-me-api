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
        private string REGISTER_VALIDATION_MESSAGE
        {
            get
            {
                return "<strong>Seja bem vindo ao Teach Me Sr(a).{0}.</strong><br>" +
                    "Antes de acessar a nossa plataforma, confirme seu cadastro clicando no link a abaixo.<br>" +
                    "Valide seu cadastro clicando <a href=\"{1}/validarCadastro?cadastro={2}\">aqui</a>!";
            }
        }
        private string BECOME_A_TEACHER_MESSAGE
        {
            get
            {
                return "  <strong>Alteração concluída.</strong><br>" +
                    "Prezado Sr(a). {0}, gostaríamos de informar que a sua conta foi alterada e agora possui permissões de professor.<br>" +
                    "A partir de agora você conseguirá ser contratato para prestar serviços.<br>" +
                    "Desejamos sucesso!";
            }
        }

        private string CHANGE_PASSWORD_MESSAGE
        {
            get
            {
                return "  <strong>Recuperação de Senha</strong><br>" +
                    "Conforme solicitado, a sua senha foi alterada, para acessar a plataforma utilize as credenciais abaixo.<br>" +
                    "E-mail: {0}<br>" +
                    "Senha: {1}";
            }
        }

        #endregion

        private IDictionary<string, string> Parametros;
        private SmtpClient Servidor;
        public EmailRepositorio()
        {
            Parametros = ParametrosServidor();
            Servidor = IniciarCliente(Parametros);
        }


        public void EnviarEmail(string email, string titulo, string mensagem)
        {
            MailMessage message = new MailMessage();

            message.To.Add(email);
            message.Subject = titulo;
            message.Body = mensagem;

            MandarEmail(message);
        }

        public void NotificarCadastro(string email, string nome, Guid validacaoId)
        {
            MailMessage message = new MailMessage();

            message.To.Add(email);
            message.Subject = "Boas Vindas - Teach Me";
            message.Body = string.Format(REGISTER_VALIDATION_MESSAGE, nome, ParametrosServidor(FRONTEND_URL)[FRONTEND_URL], validacaoId);

            MandarEmail(message);
        }

        public void NotificarMudancaPerfilProfessor(string email, string nome)
        {
            MailMessage message = new MailMessage();

            message.To.Add(email);
            message.Subject = "Alteração de perfil da conta";
            message.Body = string.Format(BECOME_A_TEACHER_MESSAGE, nome);

            MandarEmail(message);
        }

        public void NotificarAlteracaoSenha(string email, string novaSenha)
        {
            MailMessage message = new MailMessage();

            message.To.Add(email);
            message.Subject = "Solicitação de alteração de senha";
            message.Body = string.Format(CHANGE_PASSWORD_MESSAGE, email, novaSenha);

            MandarEmail(message);
        }

        private void MandarEmail(MailMessage message)
        {
            message.From = new MailAddress(Parametros[LOGIN]);
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            message.Priority = MailPriority.High;
            message.IsBodyHtml = true;

            Servidor.Send(message);
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
