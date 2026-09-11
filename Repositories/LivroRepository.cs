using EmprestimoLibrary.Data;
using EmprestimoLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLibrary.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly ControleEmprestimoLivroContext _context;

        public LivroRepository(ControleEmprestimoLivroContext context)
        {
            _context = context;
        }
        public async Task<Livro> AdicionarAsync(Livro livro)
        {
            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task AtualizarAsync(Livro livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
        }

        public async Task<Livro?> BuscarPorIdAsync(int id)
        {
            return await _context.Livros.FindAsync(id);
        }

        public async Task<List<Livro>> BuscarTodosAsync()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task ExcluirAsync(Livro livro)
        {
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
        }
    }
}
