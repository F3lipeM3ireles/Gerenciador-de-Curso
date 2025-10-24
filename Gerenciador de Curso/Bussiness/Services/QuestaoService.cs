using System;
using System.Collections.Generic;
using System.Linq;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_de_Curso.Bussiness.Services
{
    public class QuestaoService
    {
        private readonly DataContext _context;

        public QuestaoService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Questao> GetAll()
        {
            return _context.Questoes.Include(q => q.Curso).ToList();
        }

        public Questao GetById(Guid id)
        {
            return _context.Questoes.Include(q => q.Curso).FirstOrDefault(q => q.Id == id);
        }

        public void Add(Questao questao)
        {
            questao.Id = Guid.NewGuid();
            _context.Questoes.Add(questao);
            _context.SaveChanges();
        }

        public void Update(Questao questao)
        {
            var existing = _context.Questoes.Find(questao.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(questao);
                _context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var questao = _context.Questoes.Find(id);
            if (questao != null)
            {
                _context.Questoes.Remove(questao);
                _context.SaveChanges();
            }
        }
    }
}
