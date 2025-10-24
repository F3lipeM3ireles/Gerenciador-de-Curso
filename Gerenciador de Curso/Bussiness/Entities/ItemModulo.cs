namespace Gerenciador_de_Curso.Bussiness.Entities
{

    // Representa um item dentro do Módulo. Pode ser um Conteúdo, uma Questão ou uma Avaliação.

    public class ItemModulo
    {
        public Guid Id { get; set; }

        public Guid ModuloId { get; set; }
        public required Modulo Modulo { get; set; }

        // Aqui eu defino qual dos três que é, conteúdo, questão ou avaliação
        public TipoItemModulo Tipo { get; set; }

        // Ordem do item dentro do módulo
        public int Ordem { get; set; }

        // Precisa também de chave estrangeira.
        // Sempre vai ser um desses três, então, só um vai ser preenchido por vez
        public Guid? ConteudoId { get; set; }
        public Conteudo? Conteudo { get; set; }

        public Guid? QuestaoId { get; set; }
        public Questao? Questao { get; set; }

        public Guid? AvaliacaoId { get; set; }
        public Avaliacao? Avaliacao { get; set; }

        public bool Obrigatorio { get; set; } = true;
        public DateTime DataCriacao { get; set; }
    }
}