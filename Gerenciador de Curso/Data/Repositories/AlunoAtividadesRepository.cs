using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Data.Repositories
{
    public class AlunoAtividadesRepository : IAlunoAtividadesRepository
    {
        private readonly DataContext _context;

        public AlunoAtividadesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<RespostaAluno?> GetByIdAsync(Guid id)
        {
            return await _context.RespostasAlunos.FindAsync(id);
        }

        public async Task<IEnumerable<RespostaAluno>> GetAllAsync()
        {
            return await _context.RespostasAlunos.ToListAsync();
        }

        public async Task<RespostaAluno> CreateAsync(RespostaAluno respostaAluno)
        {
            await _context.RespostasAlunos.AddAsync(respostaAluno);
            await _context.SaveChangesAsync();
            return respostaAluno;
        }

        public async Task<RespostaAluno> UpdateAsync(RespostaAluno respostaAluno)
        {
            _context.RespostasAlunos.Update(respostaAluno);
            await _context.SaveChangesAsync();
            return respostaAluno;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var respostaAluno = await GetByIdAsync(id);
            if (respostaAluno == null) return false;

            _context.RespostasAlunos.Remove(respostaAluno);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RespostaAluno>> GetRespostasByAlunoAsync(Guid alunoId)
        {
            return await _context.RespostasAlunos
                .Include(resposta => resposta.Questao)
                .Where(resposta => resposta.AlunoId == alunoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RespostaAluno>> GetRespostasByQuestaoAsync(Guid questaoId)
        {
            return await _context.RespostasAlunos
                .Include(resposta => resposta.Aluno)
                .Where(resposta => resposta.QuestaoId == questaoId)
                .ToListAsync();
        }

        public async Task<RespostaAluno?> GetRespostaByAlunoAndQuestaoAsync(Guid alunoId, Guid questaoId)
        {
            return await _context.RespostasAlunos
                .FirstOrDefaultAsync(resposta =>
                    resposta.AlunoId == alunoId && resposta.QuestaoId == questaoId);
        }

        public async Task<IEnumerable<RespostaAluno>> GetRespostasByCursoAsync(Guid cursoId)
        {
            return await _context.RespostasAlunos
                .Include(resposta => resposta.Aluno)
                .Include(resposta => resposta.Questao)
                .Where(resposta => resposta.Questao.CursoId == cursoId)
                .ToListAsync();
        }

        public async Task<double> CalcularMediaNotasByAlunoAsync(Guid alunoId, Guid cursoId)
        {
            var respostas = await _context.RespostasAlunos
                .Include(resposta => resposta.Questao)
                .Where(resposta => resposta.AlunoId == alunoId && resposta.Questao.CursoId == cursoId)
                .ToListAsync();

            if (!respostas.Any()) return 0;

            return respostas.Average(resposta => resposta.Nota);
        }

        public async Task<bool> AlunoJaRespondeuQuestaoAsync(Guid alunoId, Guid questaoId)
        {
            return await _context.RespostasAlunos
                .AnyAsync(resposta =>
                    resposta.AlunoId == alunoId && resposta.QuestaoId == questaoId);
        }

        public async Task<int> GetQuantidadeRespostasConcluidasAsync(Guid alunoId, Guid cursoId)
        {
            return await _context.RespostasAlunos
                .Include(resposta => resposta.Questao)
                .CountAsync(resposta =>
                    resposta.AlunoId == alunoId &&
                    resposta.Questao.CursoId == cursoId &&
                    resposta.Acertou);
        }
    }
}
