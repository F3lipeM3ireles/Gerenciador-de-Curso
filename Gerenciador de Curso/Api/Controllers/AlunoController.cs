using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gerenciador_de_Curso.Bussiness.Entities;
using Gerenciador_de_Curso.Bussiness.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gerenciador_de_Curso.Bussiness.Interfaces.IServices;
using Microsoft.Extensions.FileProviders;

namespace Gerenciador_de_Curso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService; 
        
        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var alunos = _alunoService.GetAll();
            return Ok(alunos);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var aluno = _alunoService.GetById(id);
            if (aluno == null)
                return NotFound();
            return Ok(aluno);
        }
        [HttpPost]
        public IActionResult Add([FromBody] Aluno aluno)
        {
            if (aluno == null)
                return BadRequest();

            _alunoService.Add(aluno);
            return CreatedAtAction(nameof(GetById), new { id = aluno.Id }, aluno);
        }
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Aluno aluno)
        {
            if (aluno == null || aluno.Id != id)
                return BadRequest();

            var existente = _alunoService.GetById(id);
            if (aluno == null)
                return NotFound();

            _alunoService.Delete(id);
            return NoContent();
        }

    }
}
