namespace Gerenciador_de_Curso.Bussiness.Entities
{
    public class Modulo
    {
        public Guid Id { get; set; }
        public required string Titulo { get; set; }
        public string? Descricao { get; set; }
        public int Ordem { get; set; } // Ordem pra ordenar os módulos (Módulo 1, 2, 3...)

        public Guid CursoId { get; set; }
        public required Curso Curso { get; set; }

        public ICollection<ItemModulo> Itens { get; set; } = new List<ItemModulo>();

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}