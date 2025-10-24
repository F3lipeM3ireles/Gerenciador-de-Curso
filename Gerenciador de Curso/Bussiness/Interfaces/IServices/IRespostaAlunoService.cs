using System;
using System.Collections.Generic;
using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices
{
    public interface IRespostaAlunoService
    {
        IEnumerable<RespostaAluno> GetAll();
        RespostaAluno GetById(Guid id);
        void Add(RespostaAluno respostaAluno);
        void Update(RespostaAluno respostaAluno);
        void Delete(Guid id);
    }
}
