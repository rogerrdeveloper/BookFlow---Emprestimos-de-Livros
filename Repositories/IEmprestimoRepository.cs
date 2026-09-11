using EmprestimoLibrary.Models;

namespace EmprestimoLibrary.Repositories
{
    public interface IEmprestimoRepository
    {
        Task<List<Emprestimo>> BuscarTodosAsync();
        Task<Emprestimo?> BuscarPorIdAsync(int id);
        Task<Emprestimo> AdicionarAsync(Emprestimo emprestimo);
        Task AtualizarAsync(Emprestimo emprestimo);
        Task<List<Emprestimo>> BuscarAtivosAsync();
        Task<List<Emprestimo>> BuscarInativosAsync();
    }
}
