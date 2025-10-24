using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories
{
    public interface IAtividadesCursoRepository
    {
        Task<Questao?> GetByIdAsync(Guid id);
        Task<IEnumerable<Questao>> GetAllAsync();
        Task<Questao> CreateAsync(Questao questao);
        Task<Questao> UpdateAsync(Questao questao);
        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<Questao>> GetQuestoesByCursoAsync(Guid cursoId);
        Task<Questao?> GetQuestaoComAlternativasAsync(Guid questaoId);
        Task<IEnumerable<Questao>> GetQuestoesComAlternativasByCursoAsync(Guid cursoId);
        Task<int> GetQuantidadeQuestoesByCursoAsync(Guid cursoId);

        // CRUD para Alternativa
        Task<Alternativa?> GetAlternativaByIdAsync(Guid id);
        Task<IEnumerable<Alternativa>> GetAlternativasByQuestaoAsync(Guid questaoId);
        Task<Alternativa> CreateAlternativaAsync(Alternativa alternativa);
        Task<Alternativa> UpdateAlternativaAsync(Alternativa alternativa);
        Task<bool> DeleteAlternativaAsync(Guid id);

        // CRUD para Avaliacao
        Task<Avaliacao?> GetAvaliacaoByIdAsync(Guid id);
        Task<IEnumerable<Avaliacao>> GetAvaliacoesByCursoAsync(Guid cursoId);
        Task<IEnumerable<Avaliacao>> GetAvaliacoesByAlunoAsync(Guid alunoId);
        Task<Avaliacao> CreateAvaliacaoAsync(Avaliacao avaliacao);
        Task<Avaliacao> UpdateAvaliacaoAsync(Avaliacao avaliacao);
        Task<bool> DeleteAvaliacaoAsync(Guid id);
    }
}