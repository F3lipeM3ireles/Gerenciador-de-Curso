using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices
{
    public interface IInstrutorService
    {
        Task<IEnumerable<Instrutor>> GetAllAsync();
        Task<Instrutor> GetByIdAsync(Guid id);
        Task<Instrutor> CreateAsync(Instrutor instrutor);
        Task<bool> UpdateAsync(Guid id, Instrutor instrutor);
        Task<bool> DeleteAsync(Guid id);
    }
}
