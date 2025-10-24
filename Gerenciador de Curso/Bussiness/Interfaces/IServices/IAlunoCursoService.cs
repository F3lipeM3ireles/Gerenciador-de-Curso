using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices

{
    public interface IAlunoCursoService
    {
        Task<IEnumerable<AlunoCurso>> GetAllAsync();
        Task<AlunoCurso> GetByIdAsync(Guid id);
        Task<AlunoCurso> CreateAsync(AlunoCurso alunoCurso);
        Task<AlunoCurso> UpdateAsync(AlunoCurso alunoCurso);
        Task<bool> DeleteAsync(Guid id);
    }
}
