namespace Gerenciador_de_Curso.Bussiness.Entities
{
    public class Alternativa
    {
        public Guid Id { get; set; }

        public Guid QuestaoId { get; set; }
        public required Questao Questao { get; set; }

        public string? AlternativaTitulo { get; set; }
        public int Ordem { get; set; } // Ordem da alternativa (A, B, C, D = 0, 1, 2, 3)
    }
}