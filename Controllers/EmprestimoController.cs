using EmprestimoLibrary.DTOs;
using EmprestimoLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;

        public EmprestimoController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmprestimoResponseDto>>> BuscarTodos()
        {
            var emprestimo = await _emprestimoService.BuscarTodosAsync();

            return Ok(emprestimo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmprestimoResponseDto>> BuscarPorId(int id)
        {
            var emprestimo = await _emprestimoService.BuscarPorIdAsync(id);


            if (emprestimo is null)
            {
                return NotFound();
            }

            return Ok(emprestimo);
        }

        [HttpPost]
        public async Task<ActionResult<EmprestimoResponseDto>> Adicionar(EmprestimoRequestDto emprestimoDto)
        {
            var emprestimo = await _emprestimoService.AdicionarAsync(emprestimoDto);

            return CreatedAtAction(nameof(BuscarPorId)
                , new { id = emprestimo.IdEmprestimo }, emprestimo);
        }

        [HttpPut("{id}/devolver")]
        public async Task<IActionResult> Devolver(int id)
        {
            await _emprestimoService.DevolverAsync(id);

            return NoContent();
        }

        [HttpGet("ativos")]
        public async Task<ActionResult> BuscarAtivosAsync()
        {
            var emprestimo = await _emprestimoService.BuscarAtivosAsync();

            return Ok(emprestimo);
        }

        [HttpGet("inativos")]
        public async Task<ActionResult> BuscarInativosAsync()
        {
            var inativos = await _emprestimoService.BuscarInativosAsync();

            return Ok(inativos);
        }

        [HttpGet("atrasados")]
        public async Task<ActionResult<List<EmprestimoResponseDto>>> BuscarAtrasadosAsync()
        {
            var emprestimos = await _emprestimoService.BuscarAtrasadosAsync();

            return Ok(emprestimos);
        }
    }
}
