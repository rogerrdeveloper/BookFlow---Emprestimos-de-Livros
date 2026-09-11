using EmprestimoLibrary.DTOs;
using EmprestimoLibrary.Exceptions;
using EmprestimoLibrary.Models;
using EmprestimoLibrary.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace EmprestimoLibrary.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepositoy;
        private readonly IClienteRepository _clienteRepository;
        private readonly ILivroRepository _livroRepository;
        private const int PrazoEmprestimoDias = 7;

        public EmprestimoService(IEmprestimoRepository EmprestimoRepository,
            IClienteRepository clienteRepository, 
            ILivroRepository livroRepository)
        {
            _emprestimoRepositoy = EmprestimoRepository;
            _clienteRepository = clienteRepository;
            _livroRepository = livroRepository;
        }

        private EmprestimoResponseDto MapearResponseDto(Emprestimo emprestimo)
        {
            return new EmprestimoResponseDto(
                emprestimo.IdEmprestimo,
                emprestimo.IdCliente,
                emprestimo.IdLivro,
                emprestimo.DataEmprestimo,
                emprestimo.DataDevolucao,
                emprestimo.Devolvido
                );
        }
        public async Task<EmprestimoResponseDto> AdicionarAsync(EmprestimoRequestDto emprestimoDto)
        {
            var cliente = await _clienteRepository.BuscarPorIdAsync(emprestimoDto.IdCliente);

            if (cliente is null)
            {
                throw new BusinessException("Cliente não encontrado");
            }

            var livro = await _livroRepository.BuscarPorIdAsync(emprestimoDto.IdLivro);

            if (livro is null)
            {
                throw new BusinessException("Livro não encontrado.");
            }

            if (livro.LivroQuantidade <= 0)
            {
                throw new BusinessException("Livro indisponível para empréstimo.");
            }

            livro.LivroQuantidade--;

            await _livroRepository.AtualizarAsync(livro);

            var emprestimo = new Emprestimo
            {
                IdCliente = emprestimoDto.IdCliente,
                IdLivro = emprestimoDto.IdLivro,
                DataEmprestimo = DateTime.Now,
                Devolvido = false
            };

            var emprestimoAdd = await _emprestimoRepositoy.AdicionarAsync(emprestimo);

            return MapearResponseDto(emprestimoAdd);
        }

        public async Task<EmprestimoResponseDto?> BuscarPorIdAsync(int id)
        {
            var emprestimo = await _emprestimoRepositoy.BuscarPorIdAsync(id);

            if (emprestimo is null)
            {
                return null;
            }

            return MapearResponseDto(emprestimo);
        }

        public async Task<List<EmprestimoResponseDto>> BuscarTodosAsync()
        {
            var emprestimo = await _emprestimoRepositoy.BuscarTodosAsync();

            return emprestimo
                .Select(MapearResponseDto)
                .ToList();
        }

        public async Task DevolverAsync(int id)
        {
            var emprestimo = await _emprestimoRepositoy.BuscarPorIdAsync(id);

            if (emprestimo is null)
            {
                throw new BusinessException("Empréstimo não encontrado.");
            }

            if (emprestimo.Devolvido)
            {
                throw new BusinessException("Esse empréstimo já foi devolvido.");
            }
            var livro = await _livroRepository.BuscarPorIdAsync(emprestimo.IdLivro);

            if (livro is null)
            {
                throw new BusinessException("Livro não encontrado.");
            }

            livro.LivroQuantidade++;

            emprestimo.Devolvido = true;
            emprestimo.DataDevolucao = DateTime.Now;

            await _livroRepository.AtualizarAsync(livro);
            await _emprestimoRepositoy.AtualizarAsync(emprestimo);
        }

        public async Task<List<EmprestimoResponseDto>> BuscarAtivosAsync()
        {
            var ativos = await _emprestimoRepositoy.BuscarAtivosAsync();
            return ativos.
                Select(MapearResponseDto)
                .ToList();
        }

        public async Task<List<EmprestimoResponseDto>> BuscarInativosAsync()
        {
            var inativos = await _emprestimoRepositoy.BuscarInativosAsync();

            return inativos
                .Select(MapearResponseDto).ToList();
        }


        public async Task<List<EmprestimoResponseDto>> BuscarAtrasadosAsync()
        {
            var dataLimite = DateTime.Now.AddDays(-PrazoEmprestimoDias);

            var emprestimos = await _emprestimoRepositoy.BuscarAtrasadosAsync(dataLimite);
                

            return emprestimos
                .Select(MapearResponseDto)
                .ToList();
        }
    }
}
