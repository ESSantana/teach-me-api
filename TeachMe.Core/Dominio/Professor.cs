using System;
using System.Collections.Generic;

namespace TeachMe.Core.Dominio
{
    public class Professor
    {
        public Professor() { }
        public Professor(Professor professor) : this()
        {
            Id = professor.Id;
            UsuarioId = professor.UsuarioId;
            Usuario = professor.Usuario;
            ModalidadeEnsinoId = professor.ModalidadeEnsinoId;
            ModalidadeEnsino = professor.ModalidadeEnsino;
            EscolaridaPubAlvoId = professor.EscolaridaPubAlvoId;
            EscolaridaPubAlvo = professor.EscolaridaPubAlvo;
            Descricao = professor.Descricao;
        }

        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public Guid ModalidadeEnsinoId { get; set; }
        public Guid EscolaridaPubAlvoId { get; set; }
        public string Descricao { get; set; }

        public Usuario Usuario { get; set; }
        public ModalidadeEnsino ModalidadeEnsino { get; set; }
        public Escolaridade EscolaridaPubAlvo { get; set; }

        public List<Disciplina> Disciplinas { get; set; }
        public List<ProfessorDisciplina> ProfessorDisciplina { get; set; }
        public List<ContratoAula> ContratoAulas { get; set; }
        public List<AvaliacaoProfessor> AvaliacaoProfessor { get; set; }
    }
}
