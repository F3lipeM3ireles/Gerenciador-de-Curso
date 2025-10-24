namespace Gerenciador_de_Curso.Bussiness.Entities
{
    public class Avaliacao
    {
        public Guid Id { get; set; }

        public Guid CursoId { get; set; }
        public required Curso Curso { get; set; }

        public required string Titulo { get; set; }
        public required string Descricao { get; set; }

        // Questões da avaliação (relacionamento N:N)
        public ICollection<AvaliacaoQuestao> Questoes { get; set; } = new List<AvaliacaoQuestao>();

        public double NotaMinimaAprovacao { get; set; } 
        public int TempoLimiteMinutos { get; set; } 
        //public DateTime? DataInicio { get; set; }
        //public DateTime? DataFim { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}