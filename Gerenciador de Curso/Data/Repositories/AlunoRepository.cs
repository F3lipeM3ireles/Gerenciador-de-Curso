using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Data.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly DataContext _context;

        public AlunoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Aluno?> GetByIdAsync(Guid id)
        {
            return await _context.Alunos.FindAsync(id);
        }

        public async Task<IEnumerable<Aluno>> GetAllAsync()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task<Aluno> CreateAsync(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        public async Task<Aluno> UpdateAsync(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var aluno = await GetByIdAsync(id);
            if (aluno == null) return false;

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Aluno?> GetByEmailAsync(string email)
        {
            return await _context.Alunos
                .FirstOrDefaultAsync(aluno => aluno.Email == email);
        }

        public async Task<Aluno?> GetByCPFAsync(string cpf)
        {
            return await _context.Alunos
                .FirstOrDefaultAsync(aluno => aluno.CPF.ToString() == cpf);
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByCursoAsync(Guid cursoId)
        {
            return await _context.AlunosCursos
                .Where(alunoCurso => alunoCurso.CursoId == cursoId)
                .Select(alunoCurso => alunoCurso.Aluno)
                .ToListAsync();
        }

        public async Task<bool> ExistsEmailAsync(string email)
        {
            return await _context.Alunos
                .AnyAsync(aluno => aluno.Email == email);
        }

        public async Task<bool> ExistsCPFAsync(string cpf)
        {
            return await _context.Alunos
                .AnyAsync(aluno => aluno.CPF.ToString() == cpf);
        }
    }
}
