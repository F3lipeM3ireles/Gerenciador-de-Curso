using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Data.Repositories
{
    public class ItemModuloRepository : IItemModuloRepository
    {
        private readonly DataContext _context;

        public ItemModuloRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ItemModulo?> GetByIdAsync(Guid id)
        {
            return await _context.ItensModulos.FindAsync(id);
        }

        public async Task<IEnumerable<ItemModulo>> GetAllAsync()
        {
            return await _context.ItensModulos
                .OrderBy(item => item.Ordem)
                .ToListAsync();
        }

        public async Task<ItemModulo> CreateAsync(ItemModulo itemModulo)
        {
            itemModulo.DataCriacao = DateTime.Now;
            await _context.ItensModulos.AddAsync(itemModulo);
            await _context.SaveChangesAsync();
            return itemModulo;
        }

        public async Task<ItemModulo> UpdateAsync(ItemModulo itemModulo)
        {
            _context.ItensModulos.Update(itemModulo);
            await _context.SaveChangesAsync();
            return itemModulo;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var itemModulo = await GetByIdAsync(id);
            if (itemModulo == null) return false;

            _context.ItensModulos.Remove(itemModulo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ItemModulo>> GetItensByModuloAsync(Guid moduloId)
        {
            return await _context.ItensModulos
                .Where(item => item.ModuloId == moduloId)
                .OrderBy(item => item.Ordem)
                .ToListAsync();
        }

        public async Task<ItemModulo?> GetItemCompletoAsync(Guid id)
        {
            return await _context.ItensModulos
                .Include(item => item.Conteudo)
                .Include(item => item.Questao)
                    .ThenInclude(questao => questao.Alternativas.OrderBy(alt => alt.Ordem))
                .Include(item => item.Avaliacao)
                    .ThenInclude(avaliacao => avaliacao.Questoes.OrderBy(aq => aq.Ordem))
                        .ThenInclude(aq => aq.Questao)
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<IEnumerable<ItemModulo>> GetItensCompletosModuloAsync(Guid moduloId)
        {
            return await _context.ItensModulos
                .Include(item => item.Conteudo)
                .Include(item => item.Questao)
                    .ThenInclude(questao => questao.Alternativas.OrderBy(alt => alt.Ordem))
                .Include(item => item.Avaliacao)
                .Where(item => item.ModuloId == moduloId)
                .OrderBy(item => item.Ordem)
                .ToListAsync();
        }

        public async Task<int> GetQuantidadeItensByModuloAsync(Guid moduloId)
        {
            return await _context.ItensModulos
                .CountAsync(item => item.ModuloId == moduloId);
        }

        public async Task<bool> ReordenarItensAsync(Guid moduloId, Dictionary<Guid, int> novaOrdem)
        {
            var itens = await _context.ItensModulos
                .Where(item => item.ModuloId == moduloId)
                .ToListAsync();

            foreach (var item in itens)
            {
                if (novaOrdem.ContainsKey(item.Id))
                {
                    item.Ordem = novaOrdem[item.Id];
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ItemModulo>> GetItensByTipoAsync(Guid moduloId, TipoItemModulo tipo)
        {
            return await _context.ItensModulos
                .Where(item => item.ModuloId == moduloId && item.Tipo == tipo)
                .OrderBy(item => item.Ordem)
                .ToListAsync();
        }
    }
}
