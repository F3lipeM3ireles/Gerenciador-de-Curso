using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Data.Repositories
{
    public class AlunoCursoRepository : IAlunoCursoRepository
    {
        private readonly DataContext _context;

        public AlunoCursoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AlunoCurso?> GetByIdAsync(Guid id)
        {
            return await _context.AlunosCursos.FindAsync(id);
        }

        public async Task<IEnumerable<AlunoCurso>> GetAllAsync()
        {
            return await _context.AlunosCursos.ToListAsync();
        }

        public async Task<AlunoCurso> CreateAsync(AlunoCurso alunoCurso)
        {
            await _context.AlunosCursos.AddAsync(alunoCurso);
            await _context.SaveChangesAsync();
            return alunoCurso;
        }

        public async Task<AlunoCurso> UpdateAsync(AlunoCurso alunoCurso)
        {
            _context.AlunosCursos.Update(alunoCurso);
            await _context.SaveChangesAsync();
            return alunoCurso;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var alunoCurso = await GetByIdAsync(id);
            if (alunoCurso == null) return false;

            _context.AlunosCursos.Remove(alunoCurso);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AlunoCurso?> GetByAlunoAndCursoAsync(Guid alunoId, Guid cursoId)
        {
            return await _context.AlunosCursos
                .FirstOrDefaultAsync(alunoCurso =>
                    alunoCurso.AlunoId == alunoId && alunoCurso.CursoId == cursoId);
        }

        public async Task<IEnumerable<AlunoCurso>> GetCursosByAlunoAsync(Guid alunoId)
        {
            return await _context.AlunosCursos
                .Include(alunoCurso => alunoCurso.Curso)
                .Where(alunoCurso => alunoCurso.AlunoId == alunoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlunoCurso>> GetAlunosByCursoAsync(Guid cursoId)
        {
            return await _context.AlunosCursos
                .Include(alunoCurso => alunoCurso.Aluno)
                .Where(alunoCurso => alunoCurso.CursoId == cursoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlunoCurso>> GetCursosConcluidosByAlunoAsync(Guid alunoId)
        {
            return await _context.AlunosCursos
                .Include(alunoCurso => alunoCurso.Curso)
                .Where(alunoCurso => alunoCurso.AlunoId == alunoId && alunoCurso.Concluido)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlunoCurso>> GetCursosEmAndamentoByAlunoAsync(Guid alunoId)
        {
            return await _context.AlunosCursos
                .Include(alunoCurso => alunoCurso.Curso)
                .Where(alunoCurso => alunoCurso.AlunoId == alunoId && !alunoCurso.Concluido)
                .ToListAsync();
        }

        public async Task<bool> AlunoJaMatriculadoAsync(Guid alunoId, Guid cursoId)
        {
            return await _context.AlunosCursos
                .AnyAsync(alunoCurso =>
                    alunoCurso.AlunoId == alunoId && alunoCurso.CursoId == cursoId);
        }

        public async Task<int> GetProgressoAsync(Guid alunoId, Guid cursoId)
        {
            var alunoCurso = await GetByAlunoAndCursoAsync(alunoId, cursoId);
            return alunoCurso?.Progresso ?? 0;
        }

        public async Task<bool> UpdateProgressoAsync(Guid alunoId, Guid cursoId, int progresso)
        {
            var alunoCurso = await GetByAlunoAndCursoAsync(alunoId, cursoId);
            if (alunoCurso == null) return false;

            alunoCurso.Progresso = progresso;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarcarComoConcluidoAsync(Guid alunoId, Guid cursoId)
        {
            var alunoCurso = await GetByAlunoAndCursoAsync(alunoId, cursoId);
            if (alunoCurso == null) return false;

            alunoCurso.Concluido = true;
            alunoCurso.Progresso = 100;
            alunoCurso.Termino = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
