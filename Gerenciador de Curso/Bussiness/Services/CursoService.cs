using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Bussiness.Services
{
    public class CursoService : ICursoService
    {
        private readonly DataContext _context;

        public CursoService(DataContext context)
        {
            _context = context;
        }

        // Lista todos os cursos cadastrados
        public async Task<IEnumerable<Curso>> GetAllAsync()
        {
            return await _context.Cursos.ToListAsync();
        }

        public async Task<Curso> GetByIdAsync(Guid id)
        {
            return await _context.Cursos.FindAsync(id);
        }

        // Cria um novo curso no banco
        public async Task<Curso> CreateAsync(Curso curso)
        {
            curso.Id = Guid.NewGuid();
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<bool> UpdateAsync(Guid id, Curso curso)
        {
            var existing = await _context.Cursos.FindAsync(id);

            if (existing == null)
                return false;

            // Atualiza os campos do curso
            existing.Nome = curso.Nome;
            existing.Descricao = curso.Descricao;
            existing.CargaHoraria = curso.CargaHoraria;

            _context.Cursos.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
                return false;

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
