using System.Collections.Generic;
using System.Threading.Tasks;
using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices
{
    public interface IAlternativaService
    {
        Task<IEnumerable<Alternativa>> GetAllAsync(); 
        Task<Alternativa> GetByIdAsync(Guid id); 
        Task<Alternativa> CreateAsync(Alternativa alternativa); 
        Task<Alternativa> UpdateAsync(Alternativa alternativa); 
        Task<bool> DeleteAsync(Guid id); 
    }
}
