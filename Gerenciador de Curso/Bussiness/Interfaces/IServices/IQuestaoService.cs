using System;
using System.Collections.Generic;
using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices
{
    public interface IQuestaoService
    {
        IEnumerable<Questao> GetAll();
        Questao GetById(Guid id);
        void Add(Questao questao);
        void Update(Questao questao);
        void Delete(Guid id);
    }
}
