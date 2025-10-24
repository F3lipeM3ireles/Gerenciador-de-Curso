using System;
using System.Collections.Generic;
using System.Linq;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_de_Curso.Bussiness.Services
{
    public class RespostaAlunoService
    {
        private readonly DataContext _context;

        public RespostaAlunoService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<RespostaAluno> GetAll()
        {
            return _context.RespostasAlunos
                .Include(resposta => resposta.Aluno)
                .Include(resposta => resposta.Avaliacao)
                .Include(resposta => resposta.Questao)
                .ToList();
        }

        public RespostaAluno GetById(Guid id)
        {
            return _context.RespostasAlunos
                .Include(resposta => resposta.Aluno)
                .Include(resposta => resposta.Avaliacao)
                .Include(resposta => resposta.Questao)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Add(RespostaAluno respostaAluno)
        {
            respostaAluno.Id = Guid.NewGuid();
            _context.RespostasAlunos.Add(respostaAluno);
            _context.SaveChanges();
        }

        public void Update(RespostaAluno respostaAluno)
        {
            var existing = _context.RespostasAlunos.Find(respostaAluno.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(respostaAluno);
                _context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var resposta = _context.RespostasAlunos.Find(id);
            if (resposta != null)
            {
                _context.RespostasAlunos.Remove(resposta);
                _context.SaveChanges();
            }
        }
    }
}
