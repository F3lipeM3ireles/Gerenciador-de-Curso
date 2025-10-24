using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories
{
    public interface IResultadoAvaliacaoRepository
    {
        Task<ResultadoAvaliacao?> GetByIdAsync(Guid id);
        Task<IEnumerable<ResultadoAvaliacao>> GetAllAsync();
        Task<ResultadoAvaliacao> CreateAsync(ResultadoAvaliacao resultado);
        Task<ResultadoAvaliacao> UpdateAsync(ResultadoAvaliacao resultado);
        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<ResultadoAvaliacao>> GetResultadosByAlunoAsync(Guid alunoId);
        Task<IEnumerable<ResultadoAvaliacao>> GetResultadosByAvaliacaoAsync(Guid avaliacaoId);
        Task<ResultadoAvaliacao?> GetResultadoByAlunoAndAvaliacaoAsync(Guid alunoId, Guid avaliacaoId);
        Task<double> GetMediaNotasByAlunoAsync(Guid alunoId);
        Task<int> GetQuantidadeAprovacoesAsync(Guid alunoId);
    }
}
