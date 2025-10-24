using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices
{
   
    public interface IAvaliacaoService
    {
        Task<IEnumerable<Avaliacao>> GetAllAsync();
        Task<Avaliacao> GetByIdAsync(Guid id);
        Task<Avaliacao> CreateAsync(Avaliacao avaliacao); 
        Task<bool> UpdateAsync(Guid id, Avaliacao avaliacao); 
        Task<bool> DeleteAsync(Guid id); 
    }
}
