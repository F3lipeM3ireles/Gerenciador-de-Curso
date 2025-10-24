using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories
{
    public interface IInstrutorRepository
    {
        Task<Instrutor?> GetByIdAsync(Guid id);
        Task<IEnumerable<Instrutor>> GetAllAsync();
        Task<Instrutor> CreateAsync(Instrutor instrutor);
        Task<Instrutor> UpdateAsync(Instrutor instrutor);
        Task<bool> DeleteAsync(Guid id);

        Task<Instrutor?> GetByEmailAsync(string email);
        Task<Instrutor?> GetByCPFAsync(string cpf);
        Task<IEnumerable<Instrutor>> GetInstrutoresComCursosAsync();
        Task<Instrutor?> GetInstrutorComCursosAsync(Guid id);
        Task<bool> ExistsEmailAsync(string email);
        Task<bool> ExistsCPFAsync(string cpf);
    }
}
