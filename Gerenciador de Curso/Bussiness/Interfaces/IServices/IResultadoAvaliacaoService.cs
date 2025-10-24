using System;
using System.Collections.Generic;
using Gerenciador_de_Curso.Bussiness.Entities;

namespace Gerenciador_de_Curso.Bussiness.Interfaces.IServices
{
    public interface IResultadoAvaliacaoService
    {
        IEnumerable<ResultadoAvaliacao> GetAll();
        ResultadoAvaliacao GetById(Guid id);
        void Add(ResultadoAvaliacao resultado);
        void Update(ResultadoAvaliacao resultado);
        void Delete(Guid id);
    }
}
