using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories
{
    public interface IAlunoCursoRepository
    {
        Task<AlunoCurso?> GetByIdAsync(Guid id);
        Task<IEnumerable<AlunoCurso>> GetAllAsync();
        Task<AlunoCurso> CreateAsync(AlunoCurso alunoCurso);
        Task<AlunoCurso> UpdateAsync(AlunoCurso alunoCurso);
        Task<bool> DeleteAsync(Guid id);

        Task<AlunoCurso?> GetByAlunoAndCursoAsync(Guid alunoId, Guid cursoId);
        Task<IEnumerable<AlunoCurso>> GetCursosByAlunoAsync(Guid alunoId);
        Task<IEnumerable<AlunoCurso>> GetAlunosByCursoAsync(Guid cursoId);
        Task<IEnumerable<AlunoCurso>> GetCursosConcluidosByAlunoAsync(Guid alunoId);
        Task<IEnumerable<AlunoCurso>> GetCursosEmAndamentoByAlunoAsync(Guid alunoId);
        Task<bool> AlunoJaMatriculadoAsync(Guid alunoId, Guid cursoId);
        Task<int> GetProgressoAsync(Guid alunoId, Guid cursoId);
        Task<bool> UpdateProgressoAsync(Guid alunoId, Guid cursoId, int progresso);
        Task<bool> MarcarComoConcluidoAsync(Guid alunoId, Guid cursoId);
    }
}
