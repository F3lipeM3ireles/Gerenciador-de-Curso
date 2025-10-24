namespace Gerenciador_de_Curso.Bussiness.Entities
{
    public class AlunoCurso
    {
        public Guid Id { get; set; }


        public Guid CursoId { get; set; }
        public required Curso Curso { get; set; }


        public Guid AlunoId { get; set; }
        public required Aluno Aluno { get; set; }


        public int Progresso { get; set; }
        public bool Concluido { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }

    }
}
