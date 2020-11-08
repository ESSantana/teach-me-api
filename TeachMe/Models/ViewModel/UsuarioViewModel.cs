using System;

namespace TeachMe.API.Models.ViewModel
{
    /// <summary>
    /// Informações do usuário
    /// </summary>
    public class UsuarioViewModel
    {
        /// <summary>
        /// Id do usuário
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Usuário
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Data de Nascimento
        /// </summary>
        public DateTime DataNascimento { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Telefone de Contato
        /// </summary>
        public string Telefone { get; set; }
        /// <summary>
        /// Nível de Ensino
        /// </summary>
        public EscolaridadeViewModel Escolaridade { get; set; }
        /// <summary>
        /// Estado onde vive
        /// </summary>
        public string UF { get; set; }
        /// <summary>
        /// Cidade onde reside
        /// </summary>
        public string Cidade { get; set; }
        /// <summary>
        /// Token de autenticação
        /// </summary>
        public string Token { get; set; }
    }
}
