using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices
{
    public interface IConteudoService
    {
        Task<IEnumerable<Conteudo>> GetAllAsync(); 
        Task<Conteudo> GetByIdAsync(Guid id); 
        Task<Conteudo> CreateAsync(Conteudo conteudo); 
        Task<bool> UpdateAsync(Guid id, Conteudo conteudo); 
        Task<bool> DeleteAsync(Guid id); 
    }
}
