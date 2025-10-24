using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Data.Repositories
{
    public class InstrutorRepository : IInstrutorRepository
    {
        private readonly DataContext _context;

        public InstrutorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Instrutor?> GetByIdAsync(Guid id)
        {
            return await _context.Instrutores.FindAsync(id);
        }

        public async Task<IEnumerable<Instrutor>> GetAllAsync()
        {
            return await _context.Instrutores.ToListAsync();
        }

        public async Task<Instrutor> CreateAsync(Instrutor instrutor)
        {
            await _context.Instrutores.AddAsync(instrutor);
            await _context.SaveChangesAsync();
            return instrutor;
        }

        public async Task<Instrutor> UpdateAsync(Instrutor instrutor)
        {
            _context.Instrutores.Update(instrutor);
            await _context.SaveChangesAsync();
            return instrutor;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var instrutor = await GetByIdAsync(id);
            if (instrutor == null) return false;

            _context.Instrutores.Remove(instrutor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Instrutor?> GetByEmailAsync(string email)
        {
            return await _context.Instrutores
                .FirstOrDefaultAsync(instrutor => instrutor.Email == email);
        }

        public async Task<Instrutor?> GetByCPFAsync(string cpf)
        {
            return await _context.Instrutores
                .FirstOrDefaultAsync(instrutor => instrutor.CPF.ToString() == cpf);
        }

        public async Task<IEnumerable<Instrutor>> GetInstrutoresComCursosAsync()
        {
            return await _context.Instrutores
                .Include(instrutor => instrutor.Cursos)
                .ToListAsync();
        }

        public async Task<Instrutor?> GetInstrutorComCursosAsync(Guid id)
        {
            return await _context.Instrutores
                .Include(instrutor => instrutor.Cursos)
                .FirstOrDefaultAsync(instrutor => instrutor.Id == id);
        }

        public async Task<bool> ExistsEmailAsync(string email)
        {
            return await _context.Instrutores
                .AnyAsync(instrutor => instrutor.Email == email);
        }

        public async Task<bool> ExistsCPFAsync(string cpf)
        {
            return await _context.Instrutores
                .AnyAsync(instrutor => instrutor.CPF.ToString() == cpf);
        }
    }
}
