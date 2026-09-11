using EmprestimoLibrary.DTOs;
using EmprestimoLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LivroResponseDto>>> BuscarTodos()
        {
            var livro = await _livroService.BuscarTodosAsync();

            return Ok(livro);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroResponseDto>> BuscarPorId(int id)
        {
            var livro = await _livroService.BuscarPorIdAsync(id);

            if (livro is null)
            {
                return NotFound();
            }

            return Ok(livro);
        }

        [HttpPost]
        public async Task<ActionResult<LivroResponseDto>> Adicionar(LivroRequestDto livroDto)
        {
            var livro = await _livroService.AdicionarAsync(livroDto);

            return CreatedAtAction(nameof(BuscarPorId),
                new { id = livro.IdLivro}, 
                livro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, LivroRequestDto livroDto)
        {
            var livro = await _livroService.BuscarPorIdAsync(id);

            if (livro is null)
            {
                return NotFound();
            }

            await _livroService.AtualizarAsync(id, livroDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            var livro = await _livroService.BuscarPorIdAsync(id);

            if (livro is null)
            {
                return NotFound();
            }

            await _livroService.ExcluirAsync(id);

            return NoContent();
        }
    }
}
