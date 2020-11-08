using System;

namespace TeachMe.API.Models.ViewModel
{
    /// <summary>
    /// Contrato de Aula 
    /// </summary>
    public class ContratoAulaViewModel
    {
        /// <summary>
        /// Id do Contrato
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Id do Aluno
        /// </summary>
        public long AlunoId { get; set; }
        /// <summary>
        /// Id do Professor
        /// </summary>
        public long ProfessorId { get; set; }
        /// <summary>
        /// Data de quando foi feito o contrato
        /// </summary>
        public DateTime? DataContrato { get; set; }
        /// <summary>
        /// Data em que acontecerá a aula
        /// </summary>
        public DateTime? DataInicioPrestacao { get; set; }
        /// <summary>
        /// Data em que encerrará a aula
        /// </summary>
        public DateTime? DataFimPrestacao { get; set; }
        /// <summary>
        /// Quantidade de horas contratadas
        /// </summary>
        public int HorasContratadas { get; set; }
        /// <summary>
        /// Valor por hora contratada
        /// </summary>
        public decimal ValorHora { get; set; }
        /// <summary>
        /// Valor total do contrato de aula
        /// </summary>
        public decimal ValorTotal { get; set; }
        /// <summary>
        /// O serviço foi prestado?
        /// </summary>
        public bool Avaliado { get; set; }
    }
}
