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
    public class ConteudoController : ControllerBase
    {
        private readonly IConteudoService _conteudoService;

        public ConteudoController(IConteudoService conteudoService)
        {
            _conteudoService = conteudoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var conteudos = await _conteudoService.GetAllAsync();
            return Ok(conteudos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var conteudo = await _conteudoService.GetByIdAsync(id);
            if (conteudo == null)
                return NotFound();

            return Ok(conteudo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Conteudo conteudo)
        {
            var novoConteudo = await _conteudoService.CreateAsync(conteudo);
            return CreatedAtAction(nameof(GetById), new { id = novoConteudo.Id }, novoConteudo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Conteudo conteudo)
        {
            var atualizou = await _conteudoService.UpdateAsync(id, conteudo);
            if (!atualizou)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletou = await _conteudoService.DeleteAsync(id);
            if (!deletou)
                return NotFound();

            return NoContent();
        }
    }
}
