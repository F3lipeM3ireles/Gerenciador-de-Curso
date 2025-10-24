namespace Gerenciador_de_Curso.Bussiness.Entities
{
    public class Questao
    {
        public Guid Id { get; set; }

        public Guid CursoId { get; set; }
        public required Curso Curso { get; set; }

        public required string Titulo { get; set; }
        public string? Descricao { get; set; }
        public required string Enunciado { get; set; }

        public ICollection<Alternativa> Alternativas { get; set; } = new List<Alternativa>();

        public int RespostaCorreta { get; set; } // Posição da alternativa correta na lista(0, 1, 2, 3...)

        public float Pontuacao { get; set; } // Quantos pontos essa questão tá valendo

        public DateTime? PrazoEnvio { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}