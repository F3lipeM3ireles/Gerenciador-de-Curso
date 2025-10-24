using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories
{
    public interface IAlunoRepository
    {
        Task<Aluno?> GetByIdAsync(Guid id);
        Task<IEnumerable<Aluno>> GetAllAsync();
        Task<Aluno> CreateAsync(Aluno aluno);
        Task<Aluno> UpdateAsync(Aluno aluno);
        Task<bool> DeleteAsync(Guid id);

        Task<Aluno?> GetByEmailAsync(string email);
        Task<Aluno?> GetByCPFAsync(string cpf);
        Task<IEnumerable<Aluno>> GetAlunosByCursoAsync(Guid cursoId);
        Task<bool> ExistsEmailAsync(string email);
        Task<bool> ExistsCPFAsync(string cpf);
    }
}




