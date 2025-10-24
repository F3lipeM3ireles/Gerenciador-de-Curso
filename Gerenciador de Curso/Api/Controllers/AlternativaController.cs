using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Gerenciador_de_Curso.Bussiness.Entities;

using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;

namespace Gerenciador_de_Curso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlternativaController : ControllerBase
    {
        private readonly IAlternativaService _alternativaService;

        public AlternativaController(IAlternativaService alternativaService)
        {
            _alternativaService = alternativaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alternativas = await _alternativaService.GetAllAsync();
            return Ok(alternativas);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var alternativa = await _alternativaService.GetByIdAsync(id);
            if (alternativa == null)
                return NotFound("Alternativa não encontrada.");
            return Ok(alternativa);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Alternativa alternativa)
        {
            var criada = await _alternativaService.CreateAsync(alternativa);
            return CreatedAtAction(nameof(GetById), new { id = criada.Id }, criada);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sucesso = await _alternativaService.DeleteAsync(id);
            if (!sucesso)
                return NotFound("Alternativa não encontrada para exclusão.");
            return NoContent();
        }
    }
}
