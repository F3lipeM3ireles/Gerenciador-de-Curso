using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Data.Context;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;

namespace Gerenciador_de_Curso.Bussiness.Services
{
    public class AlternativaService : IAlternativaService
    {
        private readonly DataContext _context;

        public AlternativaService(DataContext context)
        {
            _context = context;
        }
        public async Task<Alternativa> GetByIdAsync(Guid id)
        {
            return await _context.Alternativas.FindAsync(id);
        }

        public async Task<Alternativa> CreateAsync(Alternativa alternativa)
        {
            _context.Alternativas.Add(alternativa);
            await _context.SaveChangesAsync();
            return alternativa;
        }

        public async Task<Alternativa> UpdateAsync(Alternativa alternativa)
        {
            _context.Entry(alternativa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return alternativa;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var alternativa = await _context.Alternativas.FindAsync(id);
                if (alternativa == null)
                    return false;

            _context.Alternativas.Remove(alternativa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Alternativa>> GetAllAsync()
        {
            return await _context.Alternativas.ToListAsync();
        }
    }
}
