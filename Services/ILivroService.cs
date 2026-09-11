using EmprestimoLibrary.DTOs;

namespace EmprestimoLibrary.Services
{
    public interface ILivroService
    {
        Task<List<LivroResponseDto>> BuscarTodosAsync();
        Task<LivroResponseDto?> BuscarPorIdAsync(int id);
        Task<LivroResponseDto> AdicionarAsync(LivroRequestDto livroRequest);
        Task AtualizarAsync(int id, LivroRequestDto livroDto);
        Task ExcluirAsync(int id); 
    }
}
