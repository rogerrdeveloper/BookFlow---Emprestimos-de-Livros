using EmprestimoLibrary.Models;

namespace EmprestimoLibrary.Repositories
{
    public interface IClienteRepository
    {
        //Task significa operação assincrona,
        //List<Cliente> é o tipo de retorno
        Task<List<Cliente>> BuscarTodosAsync();
        Task<Cliente?> BuscarPorIdAsync(int id);
        Task<Cliente> AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
        Task ExcluirAsync(Cliente cliente);
        Task<bool> ExistePorCpfAsync(string cpf, int? idCliente = null);
        //CPF existe?
        //Se existe, ele pertence ao próprio cliente que estou editando?
    }
}
