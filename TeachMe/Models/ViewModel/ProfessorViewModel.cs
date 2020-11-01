using System;
using System.Collections.Generic;
using TeachMe.API.Models.DTO;

namespace TeachMe.API.Models.ViewModel
{
    /// <summary>
    /// Professor
    /// </summary>
    public class ProfessorViewModel
    {
        /// <summary>
        /// Id do Professor
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
        public string Escolaridade { get; set; }
        /// <summary>
        /// Estado onde vive
        /// </summary>
        public string UF { get; set; }
        /// <summary>
        /// Cidade onde reside
        /// </summary>
        public string Cidade { get; set; }
        /// <summary>
        /// Disciplinas lecionadas
        /// </summary>
        public List<DisciplinaDTO> Disciplinas { get; set; }
    }
}
