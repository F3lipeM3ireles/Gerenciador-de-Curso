namespace Gerenciador_de_Curso.Bussiness.Entities
{
    /// Tabela pra juntar a Avaliação e a Questão
    /// Assim, dá pra usar a mesma questão em várias avaliações diferentes
   
    public class AvaliacaoQuestao
    {
        public Guid Id { get; set; }

        public Guid AvaliacaoId { get; set; }
        public required Avaliacao Avaliacao { get; set; }

        public Guid QuestaoId { get; set; }
        public required Questao Questao { get; set; }

        public int Ordem { get; set; } // Ordem da questão na avaliação
        public float Peso { get; set; } // Peso da questão na nota final (padrão 1.0)
    }
}