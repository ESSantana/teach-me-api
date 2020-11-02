using System;
using System.Collections.Generic;

namespace TeachMe.API.Models.DTO
{
    /// <summary>
    /// Professor
    /// </summary>
    public class ProfessorDTO
    {
        /// <summary>
        /// Email do Usuário cadastrado
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha do Usuário cadastrado
        /// </summary>
        public string Senha { get; set; }
        /// <summary>
        /// Id do Usuário cadastrado
        /// </summary>
        public long UsuarioId { get; set; }
        /// <summary>
        /// Lista de Disciplinas lecionadas
        /// </summary>
        public List<DisciplinaDTO> Disciplinas { get; set; }
        /// <summary>
        /// Id da Modalidade de ensino das aulas
        /// </summary>
        public Guid ModalidadeEnsinoId { get; set; }
        /// <summary>
        /// Id da escolaridade do público alvo
        /// </summary>
        public Guid EscolaridaPubAlvoId { get; set; }
        /// <summary>
        /// Descrição opcional do professor
        /// </summary>
        public string Descricao { get; set; }
    }   
}
