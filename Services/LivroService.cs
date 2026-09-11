using EmprestimoLibrary.DTOs;
using EmprestimoLibrary.Models;
using EmprestimoLibrary.Repositories;

namespace EmprestimoLibrary.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        private LivroResponseDto MapearResponseDto(Livro livro)
        {
            return new LivroResponseDto(
                livro.IdLivro,
                livro.LivroTitulo,
                livro.LivroAutor,
                livro.LivroEditora,
                livro.LivroEdicao,
                livro.LivroQuantidade
                );
        }
        public async Task<LivroResponseDto> AdicionarAsync(LivroRequestDto livroDto)
        {
            var livro = new Livro

            {
                LivroTitulo = livroDto.LivroTitulo,
                LivroAutor = livroDto.LivroAutor,
                LivroEditora = livroDto.LivroEditora,
                LivroEdicao = livroDto.LivroEdicao,
                LivroQuantidade = livroDto.LivroQuantidade
            };

            var livroAdd = await _livroRepository.AdicionarAsync(livro);

            return MapearResponseDto(livroAdd);
        }

        public async Task AtualizarAsync(int id, LivroRequestDto livroDto)
        {
            var livro = await _livroRepository.BuscarPorIdAsync(id);

            if (livro is null)
            {
                return;
            }

            livro.LivroTitulo = livroDto.LivroTitulo;
            livro.LivroAutor = livroDto.LivroAutor;
            livro.LivroEditora = livroDto.LivroEditora;
            livro.LivroEdicao = livroDto.LivroEdicao;
            livro.LivroQuantidade = livroDto.LivroQuantidade;

            await _livroRepository.AtualizarAsync(livro);
        }

        public async Task<LivroResponseDto?> BuscarPorIdAsync(int id)
        {
            var livro = await _livroRepository.BuscarPorIdAsync(id);

            if (livro is null)
            {
                return null;
            }

            return MapearResponseDto(livro);
        }

        public async Task<List<LivroResponseDto>> BuscarTodosAsync()
        {
            var livros = await _livroRepository.BuscarTodosAsync();
            return livros
                .Select(MapearResponseDto)
                .ToList();
        }

        public async Task ExcluirAsync(int id)
        {
            var livros = await _livroRepository.BuscarPorIdAsync(id);

            if (livros is null)
            {
                return;
            }

            await _livroRepository.ExcluirAsync(livros);

        }
    }
}
