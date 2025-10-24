namespace Gerenciador_de_Curso.Bussiness.Entities
{
    public class Conteudo
    {
        public Guid Id { get; set; }
        public required string Titulo { get; set; }
        public required string TextoInformativo { get; set; }
        public byte[]? Midias { get; set; }

        // Tipo de conteúdo (vídeo, PDF, texto, etc)
        public string? TipoMidia { get; set; } // Ex: "video/mp4", "application/pdf", "image/jpeg"

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}