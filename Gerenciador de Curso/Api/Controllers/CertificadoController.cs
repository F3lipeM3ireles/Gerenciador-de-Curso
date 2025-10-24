using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Services;

namespace Gerenciador_de_Curso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificadoController : ControllerBase
    {
        private readonly CertificadoService _service;
        private readonly CertificadoPdfService _pdfService;

        public CertificadoController (CertificadoService service)
        {
            _service = service;
            _pdfService = _pdfService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var certificados = await _service.GetAllAsync(); // chama o método
            return Ok(certificados);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var certificado = await _service.GetByIdAsync(id); 
            if (certificado == null)
                return NotFound();

            return Ok(certificado);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Context certificado) // cria o certif. novo, o From faz o Json do corpo da requisição
        {
            await _service.AddAsync(certificado); // salva banco
            return CreatedAtAction(nameof(GetById), new { id = certificado.Id }, certificado);
        }
        [HttpPut ("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Context certificado) // att certif.
        {
            if (id != certificado.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(certificado);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> DownloadPdf(Guid id)
        {
            var certificado = await _service.GetByIdAsync(id); // busca no banco 
            if (certificado == null) return NotFound();

            var bytes = _pdfService.GerarPdf(certificado);

            var fileName = $"Certificado_{certificado.NomeAluno.Replace(" ", "_")}.pdf";
            return File(bytes, "application/pdf", fileName);
        }
    }
}
