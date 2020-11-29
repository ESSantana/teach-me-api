namespace TeachMe.API.Models.DTO
{
    /// <summary>
    /// Dados para fazer recuperação de senha
    /// </summary>
    public class AlteracaoSenhaDTO
    {
        /// <summary>
        /// Email usado para fazer login
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// RG ou CPFw
        /// </summary>
        public string TipoDocumento { get; set; }
        /// <summary>
        /// Número do RG ou CPF
        /// </summary>
        public string Documento { get; set; }
    }
}
