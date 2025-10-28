using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IRepositories;
using Gerenciador_de_Curso.Data.Context;
using iText.Commons.Actions.Contexts;

namespace Gerenciador_de_Curso.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly DataContext _context;

        public CursoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Curso?> GetByIdAsync(Guid id)
        {
            return await _context.Cursos.FindAsync(id);
        }

        public async Task<IEnumerable<Curso>> GetAllAsync()
        {
            return await _context.Cursos.ToListAsync();
        }

        public async Task<Curso> CreateAsync(Curso curso)
        {
            curso.DataCriacao = DateTime.Now;
            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<Curso> UpdateAsync(Curso curso)
        {
            curso.DataAtualizacao = DateTime.Now;
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var curso = await GetByIdAsync(id);
            if (curso == null) return false;

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Curso?> GetCursoComInstrutorAsync(Guid id)
        {
            return await _context.Cursos
                .Include(curso => curso.Instrutor)
                .FirstOrDefaultAsync(curso => curso.Id == id);
        }

        public async Task<Curso?> GetCursoComAlunosAsync(Guid id)
        {
            return await _context.Cursos
                .Include(curso => curso.Alunos)
                    .ThenInclude(alunoCurso => alunoCurso.Aluno)
                .FirstOrDefaultAsync(curso => curso.Id == id);
        }

        public async Task<Curso?> GetCursoCompletoAsync(Guid id)
        {
            return await _context.Cursos
                .Include(curso => curso.Instrutor)
                .Include(curso => curso.Alunos)
                    .ThenInclude(alunoCurso => alunoCurso.Aluno)
                .Include(curso => curso.Modulos)
                .FirstOrDefaultAsync(curso => curso.Id == id);
        }

        public async Task<IEnumerable<Curso>> GetCursosByInstrutorAsync(Guid instrutorId)
        {
            return await _context.Cursos
                .Where(curso => curso.InstrutorId == instrutorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Curso>> GetCursosDisponiveisAsync()
        {
            return await _context.Cursos
                .Include(curso => curso.Alunos)
                .Where(curso => curso.Alunos.Count < curso.LimiteTurma)
                .ToListAsync();
        }

        public async Task<int> GetQuantidadeAlunosMatriculadosAsync(Guid cursoId)
        {
            return await _context.AlunosCursos
                .CountAsync(alunoCurso => alunoCurso.CursoId == cursoId);
        }

        public async Task<bool> CursoTemVagasAsync(Guid cursoId)
        {
            var curso = await _context.Cursos
                .Include(curso => curso.Alunos)
                .FirstOrDefaultAsync(curso => curso.Id == cursoId);

            if (curso == null) return false;

            return curso.Alunos.Count < curso.LimiteTurma;
        }



        public class Paginador<TEntity>
        {
            public List<TEntity> Itens;
            public int Pagina;
            public int ItensPorPagina;
            public int TotalItens;
        }


        public Paginador<Curso> BuscarCursos(string Categoria, string Nome, int Pagina, int ItensPorPagina) //puxar por categoria
        {
            var query = _context.Cursos.Select(curso => curso);

            if (Categoria.Length > 0)
            {
                query = query.Where(curso => curso.Categorias.Contains(Categoria));
            }

            if (Nome.Length > 0)
            {
                query = query.Where(curso => curso.Nome.Contains(Nome));
            }

            var list = query
                .OrderBy(curso => curso.Nome)
                .Skip((Pagina - 1) * ItensPorPagina)
                .Take(ItensPorPagina)
                .ToList();

            int TotalItens = query.Count();

            Paginador<Curso> paginador = new Paginador<Curso>();

            paginador.Itens = list;
            paginador.Pagina = Pagina;
            paginador.ItensPorPagina = ItensPorPagina;
            paginador.TotalItens = TotalItens;

            return paginador;
        }
    }

}
