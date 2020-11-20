using System.Collections.Generic;

namespace TeachMe.API.Models.ViewModel
{
    /// <summary>
    /// Professor
    /// </summary>
    public class ProfessorViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Descrição
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Valor cobrado por hora/aula
        /// </summary>
        public decimal ValorHora { get; set; }
        /// <summary>
        /// Dados do usuário
        /// </summary>
        public UsuarioViewModel Usuario { get; set; }
        /// <summary>
        /// Modalidade de ensino
        /// </summary>
        public ModalidadeEnsinoViewModel ModalidadeEnsino { get; set; }
        /// <summary>
        /// Escolaridade do público alvo
        /// </summary>
        public EscolaridadeViewModel EscolaridaPubAlvo { get; set; }
        /// <summary>
        /// Disciplinas lecionadas
        /// </summary>
        public List<DisciplinaViewModel> Disciplinas { get; set; }
        ///<summary>
        /// Nota Média do professor
        ///</summary>
        public float NotaMedia { get; set; }
        ///<summary>
        /// Quantidade de avaliações
        ///</summary>
        public int QtdAvaliacoes { get; set; }
    }
}
