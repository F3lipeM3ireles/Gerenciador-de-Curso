using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories
{
    public interface IAlunoAtividadesRepository
    {
        Task<RespostaAluno?> GetByIdAsync(Guid id);
        Task<IEnumerable<RespostaAluno>> GetAllAsync();
        Task<RespostaAluno> CreateAsync(RespostaAluno respostaAluno);
        Task<RespostaAluno> UpdateAsync(RespostaAluno respostaAluno);
        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<RespostaAluno>> GetRespostasByAlunoAsync(Guid alunoId);
        Task<IEnumerable<RespostaAluno>> GetRespostasByQuestaoAsync(Guid questaoId);
        Task<RespostaAluno?> GetRespostaByAlunoAndQuestaoAsync(Guid alunoId, Guid questaoId);
        Task<IEnumerable<RespostaAluno>> GetRespostasByCursoAsync(Guid cursoId);
        Task<double> CalcularMediaNotasByAlunoAsync(Guid alunoId, Guid cursoId);
        Task<bool> AlunoJaRespondeuQuestaoAsync(Guid alunoId, Guid questaoId);
        Task<int> GetQuantidadeRespostasConcluidasAsync(Guid alunoId, Guid cursoId);
    }
}
