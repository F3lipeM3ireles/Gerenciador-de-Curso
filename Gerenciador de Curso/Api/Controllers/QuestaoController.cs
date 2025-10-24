using System;
using Microsoft.AspNetCore.Mvc;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Services;

namespace Gerenciador_de_Curso.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestaoController : ControllerBase
    {
        private readonly QuestaoService _service;

        public QuestaoController(QuestaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var questoes = _service.GetAll();
            return Ok(questoes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var questao = _service.GetById(id);
            if (questao == null)
                return NotFound();
            return Ok(questao);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Questao questao)
        {
            _service.Add(questao);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Questao questao)
        {
            if (id != questao.Id)
                return BadRequest();

            _service.Update(questao);
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
