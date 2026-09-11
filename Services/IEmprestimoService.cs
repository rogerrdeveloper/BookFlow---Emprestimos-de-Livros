using EmprestimoLibrary.DTOs;

namespace EmprestimoLibrary.Services
{
    public interface IEmprestimoService
    {
        Task<List<EmprestimoResponseDto>> BuscarTodosAsync();
        Task<EmprestimoResponseDto?> BuscarPorIdAsync(int id);
        Task<EmprestimoResponseDto> AdicionarAsync(EmprestimoRequestDto emprestimoDto);
        Task DevolverAsync(int id);
        Task<List<EmprestimoResponseDto>> BuscarAtivosAsync(); 
        Task<List<EmprestimoResponseDto>> BuscarInativosAsync();
        Task<List<EmprestimoResponseDto>> BuscarAtrasadosAsync();
    }
}
