using EmprestimoLibrary.DTOs;

namespace EmprestimoLibrary.Services
{
    public interface IClienteService
    {
        Task<List<ClienteResponseDto>> BuscarTodosAsync();
        // o ? significa que o resultado pode ser nulo
        // , ou seja, não encontrou o cliente
        Task<ClienteResponseDto?> BuscarPorIdAsync(int id);
        Task<ClienteResponseDto> AdicionarAsync(ClienteRequestDto clienteDto);
        Task AtualizarAsync(int id, ClienteRequestDto cliente);
        Task ExcluirAsync(int id);
    }
}
