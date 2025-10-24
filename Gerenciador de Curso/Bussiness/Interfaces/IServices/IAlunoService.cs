using System.Collections.Generic;
using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices
{
    public interface IAlunoService
    {
        IEnumerable<Aluno> GetAll();
        Aluno GetById(Guid id);
        void Add(Aluno aluno);
        void Update(Aluno aluno);
        void Delete(Guid id);
    }
}
