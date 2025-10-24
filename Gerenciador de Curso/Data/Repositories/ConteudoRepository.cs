using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Data.Repositories
{
    public class ConteudoRepository : IConteudoRepository
    {
        private readonly DataContext _context;

        public ConteudoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Conteudo?> GetByIdAsync(Guid id)
        {
            return await _context.Conteudos.FindAsync(id);
        }

        public async Task<IEnumerable<Conteudo>> GetAllAsync()
        {
            return await _context.Conteudos.ToListAsync();
        }

        public async Task<Conteudo> CreateAsync(Conteudo conteudo)
        {
            conteudo.DataCriacao = DateTime.Now;
            await _context.Conteudos.AddAsync(conteudo);
            await _context.SaveChangesAsync();
            return conteudo;
        }

        public async Task<Conteudo> UpdateAsync(Conteudo conteudo)
        {
            conteudo.DataAtualizacao = DateTime.Now;
            _context.Conteudos.Update(conteudo);
            await _context.SaveChangesAsync();
            return conteudo;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var conteudo = await GetByIdAsync(id);
            if (conteudo == null) return false;

            _context.Conteudos.Remove(conteudo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Conteudo>> GetConteudosByTipoMidiaAsync(string tipoMidia)
        {
            return await _context.Conteudos
                .Where(conteudo => conteudo.TipoMidia == tipoMidia)
                .ToListAsync();
        }
    }
}
