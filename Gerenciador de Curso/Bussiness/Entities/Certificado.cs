namespace Gerenciador_de_Curso.Bussiness.Entities
{
    public class Certificado
    {
        public Guid Id { get; set; }

        public Guid CursoId { get; set; }  
        public required string NomeCurso { get; set; }

        public Guid AlunoId { get; set; }
        public required string NomeAluno { get; set; }

        public int CargaHoraria { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino {  get; set; }


    }
}
