using EmprestimoLibrary.Data;
using EmprestimoLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLibrary.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly ControleEmprestimoLivroContext _context;

        public EmprestimoRepository(ControleEmprestimoLivroContext context)
        {
            _context = context;
        }
        public async Task<Emprestimo> AdicionarAsync(Emprestimo emprestimo)
        {
            await _context.Emprestimos.AddAsync(emprestimo);
            await _context.SaveChangesAsync();

            return emprestimo;
        }

        public async Task AtualizarAsync(Emprestimo emprestimo)
        {
            _context.Emprestimos.Update(emprestimo);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Emprestimo>> BuscarAtivosAsync()
        {
            return await _context.Emprestimos
                .Where(e => !e.Devolvido)
                .ToListAsync();
        }

        public async Task<List<Emprestimo>> BuscarInativosAsync()
        {
            return await _context.Emprestimos
                .Where(e => e.Devolvido)
                .ToListAsync();
        }

        public async Task<Emprestimo?> BuscarPorIdAsync(int id)
        {
            return await _context.Emprestimos.FindAsync(id);
        }

        public async Task<List<Emprestimo>> BuscarTodosAsync()
        {
            return await _context.Emprestimos.ToListAsync();
        }

        public async Task<List<Emprestimo>> BuscarAtrasadosAsync(DateTime dataLimite)
        {
            return await _context.Emprestimos
                .Where(e => !e.Devolvido && e.DataEmprestimo < dataLimite)
                .ToListAsync();
        }

    }
}
