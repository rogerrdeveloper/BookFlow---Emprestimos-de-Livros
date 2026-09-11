using EmprestimoLibrary.DTOs;
using EmprestimoLibrary.Models;

namespace EmprestimoLibrary.Repositories
{
    public interface ILivroRepository
    {
        Task<List<Livro>> BuscarTodosAsync();
        Task<Livro?> BuscarPorIdAsync(int id);
        Task<Livro> AdicionarAsync(Livro livro);
        Task AtualizarAsync(Livro livro);
        Task ExcluirAsync(Livro livro);
    }
}
