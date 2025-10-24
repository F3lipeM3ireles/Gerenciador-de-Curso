using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;

namespace Gerenciador_de_Curso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstrutorController : ControllerBase
    {
        private readonly IInstrutorService _instrutorService;

        public InstrutorController(IInstrutorService instrutorService)
        {
            _instrutorService = instrutorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var instrutores = await _instrutorService.GetAllAsync();
            return Ok(instrutores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var instrutor = await _instrutorService.GetByIdAsync(id);
            if (instrutor == null)
                return NotFound();

            return Ok(instrutor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Instrutor instrutor)
        {
            var novoInstrutor = await _instrutorService.CreateAsync(instrutor);
            return CreatedAtAction(nameof(GetById), new { id = novoInstrutor.Id }, novoInstrutor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Instrutor instrutor)
        {
            var atualizado = await _instrutorService.UpdateAsync(id, instrutor);
            if (!atualizado)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletado = await _instrutorService.DeleteAsync(id);
            if (!deletado)
                return NotFound();

            return NoContent();
        }
    }
}
