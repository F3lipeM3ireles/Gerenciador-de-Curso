using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices
{
    public interface ICursoService
    {
        Task<IEnumerable<Curso>> GetAllAsync();
        Task<Curso> GetByIdAsync(Guid id);
        Task<Curso> CreateAsync(Curso curso);
        Task<bool> UpdateAsync(Guid id, Curso curso);
        Task<bool> DeleteAsync(Guid id);
    }
}
