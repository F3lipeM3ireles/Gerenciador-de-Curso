using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Data.Repositories
{
    public class ModuloRepository : IModuloRepository
    {
        private readonly DataContext _context;

        public ModuloRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Modulo?> GetByIdAsync(Guid id)
        {
            return await _context.Modulos.FindAsync(id);
        }

        public async Task<IEnumerable<Modulo>> GetAllAsync()
        {
            return await _context.Modulos
                .OrderBy(modulo => modulo.Ordem)
                .ToListAsync();
        }

        public async Task<Modulo> CreateAsync(Modulo modulo)
        {
            modulo.DataCriacao = DateTime.Now;
            await _context.Modulos.AddAsync(modulo);
            await _context.SaveChangesAsync();
            return modulo;
        }

        public async Task<Modulo> UpdateAsync(Modulo modulo)
        {
            modulo.DataAtualizacao = DateTime.Now;
            _context.Modulos.Update(modulo);
            await _context.SaveChangesAsync();
            return modulo;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var modulo = await GetByIdAsync(id);
            if (modulo == null) return false;

            _context.Modulos.Remove(modulo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Modulo>> GetModulosByCursoAsync(Guid cursoId)
        {
            return await _context.Modulos
                .Where(modulo => modulo.CursoId == cursoId)
                .OrderBy(modulo => modulo.Ordem)
                .ToListAsync();
        }

        public async Task<Modulo?> GetModuloComItensAsync(Guid id)
        {
            return await _context.Modulos
                .Include(modulo => modulo.Itens.OrderBy(item => item.Ordem))
                    .ThenInclude(item => item.Conteudo)
                .Include(modulo => modulo.Itens)
                    .ThenInclude(item => item.Questao)
                        .ThenInclude(questao => questao.Alternativas)
                .Include(modulo => modulo.Itens)
                    .ThenInclude(item => item.Avaliacao)
                .FirstOrDefaultAsync(modulo => modulo.Id == id);
        }

        public async Task<IEnumerable<Modulo>> GetModulosComItensByCursoAsync(Guid cursoId)
        {
            return await _context.Modulos
                .Include(modulo => modulo.Itens.OrderBy(item => item.Ordem))
                    .ThenInclude(item => item.Conteudo)
                .Include(modulo => modulo.Itens)
                    .ThenInclude(item => item.Questao)
                .Include(modulo => modulo.Itens)
                    .ThenInclude(item => item.Avaliacao)
                .Where(modulo => modulo.CursoId == cursoId)
                .OrderBy(modulo => modulo.Ordem)
                .ToListAsync();
        }

        public async Task<int> GetQuantidadeModulosByCursoAsync(Guid cursoId)
        {
            return await _context.Modulos
                .CountAsync(modulo => modulo.CursoId == cursoId);
        }

        public async Task<bool> ReordenarModulosAsync(Guid cursoId, Dictionary<Guid, int> novaOrdem)
        {
            var modulos = await _context.Modulos
                .Where(modulo => modulo.CursoId == cursoId)
                .ToListAsync();

            foreach (var modulo in modulos)
            {
                if (novaOrdem.ContainsKey(modulo.Id))
                {
                    modulo.Ordem = novaOrdem[modulo.Id];
                    modulo.DataAtualizacao = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
