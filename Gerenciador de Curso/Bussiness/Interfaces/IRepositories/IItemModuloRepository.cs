using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories
{
    public interface IItemModuloRepository
    {
        Task<ItemModulo?> GetByIdAsync(Guid id);
        Task<IEnumerable<ItemModulo>> GetAllAsync();
        Task<ItemModulo> CreateAsync(ItemModulo itemModulo);
        Task<ItemModulo> UpdateAsync(ItemModulo itemModulo);
        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<ItemModulo>> GetItensByModuloAsync(Guid moduloId);
        Task<ItemModulo?> GetItemCompletoAsync(Guid id);
        Task<IEnumerable<ItemModulo>> GetItensCompletosModuloAsync(Guid moduloId);
        Task<int> GetQuantidadeItensByModuloAsync(Guid moduloId);
        Task<bool> ReordenarItensAsync(Guid moduloId, Dictionary<Guid, int> novaOrdem);
        Task<IEnumerable<ItemModulo>> GetItensByTipoAsync(Guid moduloId, TipoItemModulo tipo);
    }
}
