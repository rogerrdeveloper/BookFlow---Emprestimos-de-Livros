using EmprestimoLibrary.Data;
using EmprestimoLibrary.Models;
using Microsoft.EntityFrameworkCore;    

namespace EmprestimoLibrary.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ControleEmprestimoLivroContext _context;

        public ClienteRepository(ControleEmprestimoLivroContext context)
        {
            _context = context;
        }

        public async Task<Cliente> AdicionarAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente?> BuscarPorIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<List<Cliente>> BuscarTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task ExcluirAsync(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistePorCpfAsync(string cpf, int? IdCliente)
        {
            //chamo a tabela do entity framework e
            //pergunto se tem cliente que realize essa condição
            return await _context.Clientes
                .AnyAsync(c => c.CpfCliente == cpf && 
                (IdCliente == null || c.IdCliente != IdCliente));
            //para cada cliente, verifique se é igual ao cpf informado.

        }
    }
}
