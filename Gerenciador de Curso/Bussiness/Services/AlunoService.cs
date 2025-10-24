using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;
using Gerenciador_de_Curso.Data.Context;

namespace Gerenciador_de_Curso.Bussiness.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly DataContext _context;

        public AlunoService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Aluno> GetAll()
        {
            return _context.Alunos.ToList(); 

        }
        public Aluno GetById(Guid id)
        {
            return _context.Alunos.Find(id); 
        }
        public void Add(Aluno aluno)
        {
            _context.Alunos.Add(aluno); 
            _context.SaveChanges();
        }
        public void Update(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
        }
        public void Delete(Guid id)
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
                _context.SaveChanges();
            }
        }

    }
}
