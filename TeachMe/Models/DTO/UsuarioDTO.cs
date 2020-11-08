using System;

namespace TeachMe.API.Models.DTO
{
    /// <summary>
    /// Usuario
    /// </summary>
    public class UsuarioDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Data de Nascimento
        /// </summary>
        public DateTime DataNascimento { get; set; }
        /// <summary>
        /// Email para contato
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha cadastrada
        /// </summary>
        public string Senha { get; set; }
        /// <summary>
        /// Telefone para contato
        /// </summary>
        public string Telefone { get; set; }
        /// <summary>
        /// Nivel de Escolaridade
        /// </summary>
        public Guid EscolaridadeId { get; set; }
        /// <summary>
        /// Tipo do documento: CPF ou RG
        /// </summary>
        public string TipoDocumento { get; set; }
        /// <summary>
        /// Número do documento
        /// </summary>
        public string NuDocumento { get; set; }
        /// <summary>
        /// Estado onde vive
        /// </summary>
        public string UF { get; set; }
        /// <summary>
        /// Cidade onde reside
        /// </summary>
        public string Cidade { get; set; }
    }
}
