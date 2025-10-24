using Microsoft.AspNetCore.Mvc;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;

namespace Gerenciador_de_Curso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cursos = await _cursoService.GetAllAsync();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var curso = await _cursoService.GetByIdAsync(id);
            if (curso == null)
                return NotFound();
            return Ok(curso);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Curso curso)
        {
            var novoCurso = await _cursoService.CreateAsync(curso);
            return CreatedAtAction(nameof(GetById), new { id = novoCurso.Id }, novoCurso);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Curso curso)
        {
            var updated = await _cursoService.UpdateAsync(id, curso);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _cursoService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
