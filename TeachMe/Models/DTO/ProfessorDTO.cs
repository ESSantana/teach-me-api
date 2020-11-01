using System;
using System.Collections.Generic;

namespace TeachMe.API.Models.DTO
{
    public class ProfessorDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public long UsuarioId { get; set; }
        public List<DisciplinaDTO> Disciplinas { get; set; }
        public Guid ModalidadeEnsinoId { get; set; }
        public Guid EscolaridaPubAlvoId { get; set; }
        public string Descricao { get; set; }
    }   
}
