namespace Gerenciador_de_Curso.Bussiness.Entities
{
    public class Curso
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public int CargaHoraria { get; set; }

        public Guid InstrutorId { get; set; }
        public Instrutor? Instrutor { get; set; }

        public int LimiteTurma { get; set; }

        public ICollection<AlunoCurso> Alunos { get; set; } = new List<AlunoCurso>();
        public ICollection<Modulo> Modulos { get; set; } = new List<Modulo>();

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}