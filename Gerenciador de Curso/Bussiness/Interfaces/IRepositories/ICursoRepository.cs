using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories
{
    public interface ICursoRepository
    {
        Task<Curso?> GetByIdAsync(Guid id);
        Task<IEnumerable<Curso>> GetAllAsync();
        Task<Curso> CreateAsync(Curso curso);
        Task<Curso> UpdateAsync(Curso curso);
        Task<bool> DeleteAsync(Guid id);

        Task<Curso?> GetCursoComInstrutorAsync(Guid id);
        Task<Curso?> GetCursoComAlunosAsync(Guid id);
        Task<Curso?> GetCursoCompletoAsync(Guid id); // Aqui é pra trazer tudo, incluindo: instrutor, alunos, módulos
        Task<IEnumerable<Curso>> GetCursosByInstrutorAsync(Guid instrutorId);
        Task<IEnumerable<Curso>> GetCursosDisponiveisAsync(); // Aqui vai ter os cursos que não estão cheios ainda
        Task<int> GetQuantidadeAlunosMatriculadosAsync(Guid cursoId);
        Task<bool> CursoTemVagasAsync(Guid cursoId);
    }
}



