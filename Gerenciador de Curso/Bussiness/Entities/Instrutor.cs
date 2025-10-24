namespace Gerenciador_de_Curso.Bussiness.Entities
{
    public class Instrutor
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Endereco { get; set; }
        public required string Telefone { get; set; }
        public required string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }

        public ICollection<Curso>? Cursos { get; set; }
    }
}
