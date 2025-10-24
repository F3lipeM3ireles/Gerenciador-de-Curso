using System;
using Microsoft.AspNetCore.Mvc;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Services;

namespace Gerenciador_de_Curso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultadoAvaliacaoController : ControllerBase
    {
        private readonly ResultadoAvaliacaoService _service;

        public ResultadoAvaliacaoController(ResultadoAvaliacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var resultados = _service.GetAll();
            return Ok(resultados);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var resultado = _service.GetById(id);
            if (resultado == null)
                return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ResultadoAvaliacao resultado)
        {
            _service.Add(resultado);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] ResultadoAvaliacao resultado)
        {
            if (id != resultado.Id)
                return BadRequest();

            _service.Update(resultado);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
