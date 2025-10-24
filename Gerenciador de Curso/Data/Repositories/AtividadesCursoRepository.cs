using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Data.Repositories
{
    public class AtividadesCursoRepository : IAtividadesCursoRepository
    {
        private readonly DataContext _context;

        public AtividadesCursoRepository(DataContext context)
        {
            _context = context;
        }

        // CRUD de Questao
        public async Task<Questao?> GetByIdAsync(Guid id)
        {
            return await _context.Questoes.FindAsync(id);
        }

        public async Task<IEnumerable<Questao>> GetAllAsync()
        {
            return await _context.Questoes.ToListAsync();
        }

        public async Task<Questao> CreateAsync(Questao questao)
        {
            questao.DataCriacao = DateTime.Now;
            await _context.Questoes.AddAsync(questao);
            await _context.SaveChangesAsync();
            return questao;
        }

        public async Task<Questao> UpdateAsync(Questao questao)
        {
            _context.Questoes.Update(questao);
            await _context.SaveChangesAsync();
            return questao;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var questao = await GetByIdAsync(id);
            if (questao == null) return false;

            _context.Questoes.Remove(questao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Questao>> GetQuestoesByCursoAsync(Guid cursoId)
        {
            return await _context.Questoes
                .Where(questao => questao.CursoId == cursoId)
                .ToListAsync();
        }

        public async Task<Questao?> GetQuestaoComAlternativasAsync(Guid questaoId)
        {
            return await _context.Questoes
                .Include(questao => questao.Alternativas.OrderBy(alt => alt.Ordem))
                .Include(questao => questao.Curso)
                .FirstOrDefaultAsync(questao => questao.Id == questaoId);
        }

        public async Task<IEnumerable<Questao>> GetQuestoesComAlternativasByCursoAsync(Guid cursoId)
        {
            return await _context.Questoes
                .Include(questao => questao.Alternativas.OrderBy(alt => alt.Ordem))
                .Where(questao => questao.CursoId == cursoId)
                .ToListAsync();
        }

        public async Task<int> GetQuantidadeQuestoesByCursoAsync(Guid cursoId)
        {
            return await _context.Questoes
                .CountAsync(questao => questao.CursoId == cursoId);
        }

        // CRUD de Alternativa
        public async Task<Alternativa?> GetAlternativaByIdAsync(Guid id)
        {
            return await _context.Alternativas.FindAsync(id);
        }

        public async Task<IEnumerable<Alternativa>> GetAlternativasByQuestaoAsync(Guid questaoId)
        {
            return await _context.Alternativas
                .Where(alternativa => alternativa.QuestaoId == questaoId)
                .OrderBy(alternativa => alternativa.Ordem)
                .ToListAsync();
        }

        public async Task<Alternativa> CreateAlternativaAsync(Alternativa alternativa)
        {
            await _context.Alternativas.AddAsync(alternativa);
            await _context.SaveChangesAsync();
            return alternativa;
        }

        public async Task<Alternativa> UpdateAlternativaAsync(Alternativa alternativa)
        {
            _context.Alternativas.Update(alternativa);
            await _context.SaveChangesAsync();
            return alternativa;
        }

        public async Task<bool> DeleteAlternativaAsync(Guid id)
        {
            var alternativa = await GetAlternativaByIdAsync(id);
            if (alternativa == null) return false;

            _context.Alternativas.Remove(alternativa);
            await _context.SaveChangesAsync();
            return true;
        }

        // CRUD de Avaliacao
        public async Task<Avaliacao?> GetAvaliacaoByIdAsync(Guid id)
        {
            return await _context.Avaliacoes.FindAsync(id);
        }

        public async Task<IEnumerable<Avaliacao>> GetAvaliacoesByCursoAsync(Guid cursoId)
        {
            return await _context.Avaliacoes
                .Where(avaliacao => avaliacao.CursoId == cursoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Avaliacao>> GetAvaliacoesByAlunoAsync(Guid alunoId)
        {
            return await _context.ResultadosAvaliacoes
                .Where(resultado => resultado.AlunoId == alunoId)
                .Select(resultado => resultado.Avaliacao)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Avaliacao> CreateAvaliacaoAsync(Avaliacao avaliacao)
        {
            avaliacao.DataCriacao = DateTime.Now;
            await _context.Avaliacoes.AddAsync(avaliacao);
            await _context.SaveChangesAsync();
            return avaliacao;
        }

        public async Task<Avaliacao> UpdateAvaliacaoAsync(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Update(avaliacao);
            await _context.SaveChangesAsync();
            return avaliacao;
        }

        public async Task<bool> DeleteAvaliacaoAsync(Guid id)
        {
            var avaliacao = await GetAvaliacaoByIdAsync(id);
            if (avaliacao == null) return false;

            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}