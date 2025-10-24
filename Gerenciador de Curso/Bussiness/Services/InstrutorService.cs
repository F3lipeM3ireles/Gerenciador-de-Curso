using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Data.Context;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;

namespace Gerenciador_de_Curso.Bussiness.Services
{
    public class InstrutorService : IInstrutorService
    {
        private readonly DataContext _context;

        public InstrutorService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Instrutor>> GetAllAsync()
        {
            return await _context.Instrutores.ToListAsync();
        }

        public async Task<Instrutor> GetByIdAsync(Guid id)
        {
            return await _context.Instrutores.FindAsync(id);
        }

        public async Task<Instrutor> CreateAsync(Instrutor instrutor)
        {
            instrutor.Id = Guid.NewGuid();
            _context.Instrutores.Add(instrutor);
            await _context.SaveChangesAsync();
            return instrutor;
        }

        public async Task<bool> UpdateAsync(Guid id, Instrutor instrutor)
        {
            var existente = await _context.Instrutores.FindAsync(id);
            if (existente == null)
                return false;

            existente.Nome = instrutor.Nome;
            existente.Email = instrutor.Email;
            existente.Telefone = instrutor.Telefone;
            existente.CPF = instrutor.CPF;

            _context.Instrutores.Update(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var instrutor = await _context.Instrutores.FindAsync(id);
            if (instrutor == null)
                return false;

            _context.Instrutores.Remove(instrutor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
