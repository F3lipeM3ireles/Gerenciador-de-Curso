using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Data.Context;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;

namespace Gerenciador_de_Curso.Bussiness.Services
{
    public class AlunoCursoService : IAlunoCursoService
    {
        private readonly DataContext _context;
        public AlunoCursoService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlunoCurso>> GetAllAsync()
        {
            return await _context.AlunosCursos
                .Include(ac => ac.Aluno) 
                .Include(ac => ac.Curso) 
                .ToListAsync();
        }
        public async Task<AlunoCurso> GetByIdAsync(Guid id)
        {
            return await _context.AlunosCursos
                .Include(ac => ac.Aluno)
                .Include(ac => ac.Curso)
                .FirstOrDefaultAsync(ac => ac.Id == id);
        }
        public async Task<AlunoCurso>CreateAsync(AlunoCurso alunoCurso)
        {
            _context.AlunosCursos.Add(alunoCurso);
            await _context.SaveChangesAsync();
            return alunoCurso;
        }
        public async Task<AlunoCurso>UpdateAsync(AlunoCurso alunoCurso)
        {
            _context.Entry(alunoCurso).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return alunoCurso;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var alunoCurso = await _context.AlunosCursos.FindAsync(id);
            if (alunoCurso == null)
                return false;

            _context.AlunosCursos.Remove(alunoCurso);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
