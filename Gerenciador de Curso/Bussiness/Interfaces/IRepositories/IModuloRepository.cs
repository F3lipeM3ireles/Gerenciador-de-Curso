using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories
{
    public interface IModuloRepository
    {
        Task<Modulo?> GetByIdAsync(Guid id);
        Task<IEnumerable<Modulo>> GetAllAsync();
        Task<Modulo> CreateAsync(Modulo modulo);
        Task<Modulo> UpdateAsync(Modulo modulo);
        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<Modulo>> GetModulosByCursoAsync(Guid cursoId);
        Task<Modulo?> GetModuloComItensAsync(Guid id);
        Task<IEnumerable<Modulo>> GetModulosComItensByCursoAsync(Guid cursoId);
        Task<int> GetQuantidadeModulosByCursoAsync(Guid cursoId);
        Task<bool> ReordenarModulosAsync(Guid cursoId, Dictionary<Guid, int> novaOrdem);
    }
}
