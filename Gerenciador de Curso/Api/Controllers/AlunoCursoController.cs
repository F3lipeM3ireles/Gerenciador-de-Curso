using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Services;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;

namespace Gerenciador_de_Curso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoCursoController : ControllerBase
    {
        private readonly IAlunoCursoService _alunoCursoService;

        // Construtor injeta o serviço de AlunoCurso
        public AlunoCursoController(IAlunoCursoService alunoCursoService)
        {
            _alunoCursoService = alunoCursoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var registros = await _alunoCursoService.GetAllAsync();
            return Ok(registros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var registro = await _alunoCursoService.GetByIdAsync(id);
            if (registro == null)
                return NotFound("Vínculo não encontrado.");
            return Ok(registro);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlunoCurso alunoCurso)
        {
            var criado = await _alunoCursoService.CreateAsync(alunoCurso);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AlunoCurso alunoCurso)
        {
            if (id != alunoCurso.Id)
                return BadRequest("O ID informado não corresponde ao vínculo enviado.");

            var atualizado = await _alunoCursoService.UpdateAsync(alunoCurso);
            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sucesso = await _alunoCursoService.DeleteAsync(id);
            if (!sucesso)
                return NotFound("Vínculo não encontrado para exclusão.");

            return NoContent();
        }
    }
}

