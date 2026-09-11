using EmprestimoLibrary.DTOs;
using EmprestimoLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        //ActionResult = passar tipo de retorno
        //IActionResult = nao passar tipo de retorno

        [HttpGet]
        public async Task<ActionResult<List<ClienteResponseDto>>> BuscarTodos()
        {
            var clientes = await _clienteService.BuscarTodosAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> BuscarPorId(int id)
        {
            var cliente = await _clienteService.BuscarPorIdAsync(id);

            if (cliente is null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteResponseDto>> Adicionar(ClienteRequestDto clienteDto)
        {
            var cliente = await _clienteService.AdicionarAsync(clienteDto);
            //createdaction esta dizendo quee o recurso foi criado e podes encontrar por esse endpoint
            //busca o nome do metodo pra acha-lo por id com o nameof
            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = cliente.IdCliente }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, ClienteRequestDto clienteDto)
        {
            var cliente = await _clienteService.BuscarPorIdAsync(id);

            if (cliente is null )
            {
                return NotFound();
            }

            await _clienteService.AtualizarAsync(id, clienteDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var cliente = await _clienteService.BuscarPorIdAsync(id);

            if (cliente is null)
            {
                return NotFound();
            }

            await _clienteService.ExcluirAsync(id);
            return NoContent();
        }
    }
}
