using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Gerenciador_de_Curso.Bussiness.Interfaces;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;

namespace Gerenciador_de_Curso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacaoController(IAvaliacaoService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var avaliacoes = await _avaliacaoService.GetAllAsync();
            return Ok(avaliacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var avaliacao = await _avaliacaoService.GetByIdAsync(id);
            if (avaliacao == null)
                return NotFound();
            return Ok(avaliacao);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Avaliacao avaliacao)
        {
            var novaAvaliacao = await _avaliacaoService.CreateAsync(avaliacao);
            return CreatedAtAction(nameof(GetById), new { id = novaAvaliacao.Id }, novaAvaliacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Avaliacao avaliacao)
        {
            var atualizou = await _avaliacaoService.UpdateAsync(id, avaliacao);
            if (!atualizou)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletou = await _avaliacaoService.DeleteAsync(id);
            if (!deletou)
                return NotFound();

            return NoContent();
        }
    }
}
