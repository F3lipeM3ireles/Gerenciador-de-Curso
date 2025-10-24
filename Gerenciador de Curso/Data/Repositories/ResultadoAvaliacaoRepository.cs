using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Data.Repositories
{
    public class ResultadoAvaliacaoRepository : IResultadoAvaliacaoRepository
    {
        private readonly DataContext _context;

        public ResultadoAvaliacaoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ResultadoAvaliacao?> GetByIdAsync(Guid id)
        {
            return await _context.ResultadosAvaliacoes.FindAsync(id);
        }

        public async Task<IEnumerable<ResultadoAvaliacao>> GetAllAsync()
        {
            return await _context.ResultadosAvaliacoes
                .Include(resultado => resultado.Aluno)
                .Include(resultado => resultado.Avaliacao)
                .ToListAsync();
        }

        public async Task<ResultadoAvaliacao> CreateAsync(ResultadoAvaliacao resultado)
        {
            await _context.ResultadosAvaliacoes.AddAsync(resultado);
            await _context.SaveChangesAsync();
            return resultado;
        }

        public async Task<ResultadoAvaliacao> UpdateAsync(ResultadoAvaliacao resultado)
        {
            _context.ResultadosAvaliacoes.Update(resultado);
            await _context.SaveChangesAsync();
            return resultado;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var resultado = await GetByIdAsync(id);
            if (resultado == null) return false;

            _context.ResultadosAvaliacoes.Remove(resultado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ResultadoAvaliacao>> GetResultadosByAlunoAsync(Guid alunoId)
        {
            return await _context.ResultadosAvaliacoes
                .Include(resultado => resultado.Avaliacao)
                .Where(resultado => resultado.AlunoId == alunoId)
                .OrderByDescending(resultado => resultado.DataConclusao)
                .ToListAsync();
        }

        public async Task<IEnumerable<ResultadoAvaliacao>> GetResultadosByAvaliacaoAsync(Guid avaliacaoId)
        {
            return await _context.ResultadosAvaliacoes
                .Include(resultado => resultado.Aluno)
                .Where(resultado => resultado.AvaliacaoId == avaliacaoId)
                .OrderByDescending(resultado => resultado.NotaFinal)
                .ToListAsync();
        }

        public async Task<ResultadoAvaliacao?> GetResultadoByAlunoAndAvaliacaoAsync(Guid alunoId, Guid avaliacaoId)
        {
            return await _context.ResultadosAvaliacoes
                .Include(resultado => resultado.Avaliacao)
                .Include(resultado => resultado.Aluno)
                .FirstOrDefaultAsync(resultado =>
                    resultado.AlunoId == alunoId && resultado.AvaliacaoId == avaliacaoId);
        }

        public async Task<IEnumerable<ResultadoAvaliacao>> GetResultadosAprovadosByAlunoAsync(Guid alunoId)
        {
            return await _context.ResultadosAvaliacoes
                .Include(resultado => resultado.Avaliacao)
                .Where(resultado => resultado.AlunoId == alunoId && resultado.Aprovado)
                .OrderByDescending(resultado => resultado.DataConclusao)
                .ToListAsync();
        }

        public async Task<IEnumerable<ResultadoAvaliacao>> GetResultadosReprovadosByAlunoAsync(Guid alunoId)
        {
            return await _context.ResultadosAvaliacoes
                .Include(resultado => resultado.Avaliacao)
                .Where(resultado => resultado.AlunoId == alunoId && !resultado.Aprovado)
                .OrderByDescending(resultado => resultado.DataConclusao)
                .ToListAsync();
        }

        public async Task<double> GetMediaNotasByAlunoAsync(Guid alunoId)
        {
            var resultados = await _context.ResultadosAvaliacoes
                .Where(resultado => resultado.AlunoId == alunoId)
                .ToListAsync();

            if (!resultados.Any()) return 0;

            return resultados.Average(resultado => resultado.NotaFinal);
        }

        public async Task<int> GetQuantidadeAprovacoesAsync(Guid alunoId)
        {
            return await _context.ResultadosAvaliacoes
                .CountAsync(resultado => resultado.AlunoId == alunoId && resultado.Aprovado);
        }

        public async Task<int> GetQuantidadeReprovacoesAsync(Guid alunoId)
        {
            return await _context.ResultadosAvaliacoes
                .CountAsync(resultado => resultado.AlunoId == alunoId && !resultado.Aprovado);
        }
    }
}