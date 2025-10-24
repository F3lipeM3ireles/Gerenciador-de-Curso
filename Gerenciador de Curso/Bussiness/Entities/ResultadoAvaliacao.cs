namespace Gerenciador_de_Curso.Bussiness.Entities
{
    public class ResultadoAvaliacao
    {
        public Guid Id { get; set; }

        public Guid AvaliacaoId { get; set; }
        public required Avaliacao Avaliacao { get; set; }

        public Guid AlunoId { get; set; }
        public required Aluno Aluno { get; set; }

        public double NotaFinal { get; set; }
        public bool Aprovado { get; set; }

        public int QuestoesCorretas { get; set; }
        public int TotalQuestoes { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime DataConclusao { get; set; }
        public int TempoGastoMinutos { get; set; }
    }
}