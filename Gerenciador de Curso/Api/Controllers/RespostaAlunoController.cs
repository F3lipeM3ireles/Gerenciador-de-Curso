using System;
using Microsoft.AspNetCore.Mvc;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Services;

namespace Gerenciador_de_Curso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespostaAlunoController : ControllerBase
    {
        private readonly RespostaAlunoService _service;

        public RespostaAlunoController(RespostaAlunoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var respostas = _service.GetAll();
            return Ok(respostas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var resposta = _service.GetById(id);
            if (resposta == null)
                return NotFound();
            return Ok(resposta);
        }

        [HttpPost]
        public IActionResult Add([FromBody] RespostaAluno resposta)
        {
            _service.Add(resposta);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] RespostaAluno resposta)
        {
            if (id != resposta.Id)
                return BadRequest();

            _service.Update(resposta);
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
