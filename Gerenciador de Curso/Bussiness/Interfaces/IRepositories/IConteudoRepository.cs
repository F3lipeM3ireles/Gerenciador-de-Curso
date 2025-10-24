using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories
{
    public interface IConteudoRepository
    {
        Task<Conteudo?> GetByIdAsync(Guid id);
        Task<IEnumerable<Conteudo>> GetAllAsync();
        Task<Conteudo> CreateAsync(Conteudo conteudo);
        Task<Conteudo> UpdateAsync(Conteudo conteudo);
        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<Conteudo>> GetConteudosByTipoMidiaAsync(string tipoMidia);
    }
}

