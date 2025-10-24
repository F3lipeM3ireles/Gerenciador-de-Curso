namespace Gerenciador_de_Curso.Bussiness.Entities
{
    public class RespostaAluno
    {
        public Guid Id { get; set; }

        public Guid AlunoId { get; set; }
        public required Aluno Aluno { get; set; }

        public Guid QuestaoId { get; set; }
        public required Questao Questao { get; set; }

        public Guid? AvaliacaoId { get; set; } 
        public Avaliacao? Avaliacao { get; set; }

        public int RespostaEscolhida { get; set; } // Índice da alternativa escolhida
        public bool Acertou { get; set; }
        public float Nota { get; set; }

        public DateTime DataResposta { get; set; }
    }
}