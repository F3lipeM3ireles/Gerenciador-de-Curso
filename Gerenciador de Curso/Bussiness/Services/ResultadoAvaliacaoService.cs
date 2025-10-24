using System;
using System.Collections.Generic;
using System.Linq;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_de_Curso.Bussiness.Services
{
    public class ResultadoAvaliacaoService
    {
        private readonly DataContext _context;

        public ResultadoAvaliacaoService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<ResultadoAvaliacao> GetAll()
        {
            return _context.ResultadosAvaliacoes
                .Include(resultados => resultados.Aluno)
                .Include(resultados => resultados.Avaliacao)
                .ToList();
        }

        public ResultadoAvaliacao GetById(Guid id)
        {
            return _context.ResultadosAvaliacoes
                .Include(resultados => resultados.Aluno)
                .Include(resultados => resultados.Avaliacao)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Add(ResultadoAvaliacao resultado)
        {
            resultado.Id = Guid.NewGuid();
            _context.ResultadosAvaliacoes.Add(resultado);
            _context.SaveChanges();
        }

        public void Update(ResultadoAvaliacao resultado)
        {
            var existing = _context.ResultadosAvaliacoes.Find(resultado.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(resultado);
                _context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var resultado = _context.ResultadosAvaliacoes.Find(id);
            if (resultado != null)
            {
                _context.ResultadosAvaliacoes.Remove(resultado);
                _context.SaveChanges();
            }
        }
    }
}
