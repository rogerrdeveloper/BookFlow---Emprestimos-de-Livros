using EmprestimoLibrary.DTOs;
using EmprestimoLibrary.Exceptions;
using EmprestimoLibrary.Models;
using EmprestimoLibrary.Repositories;

namespace EmprestimoLibrary.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        
        //metodo privado criado para evitar duplicação de mapeamento
        private ClienteResponseDto MapearResponseDto(Cliente cliente) 
        {
            return new ClienteResponseDto(
                cliente.IdCliente,
                cliente.NomeCliente,
                cliente.CpfCliente,
                cliente.EnderecoCliente,
                cliente.TelefoneCliente
            );
        }

        public async Task<ClienteResponseDto> AdicionarAsync(ClienteRequestDto clienteDto)
        {
            // precisa retornar true(existe), ou false(nao existe).
            var cpfExistente = await _clienteRepository.
                ExistePorCpfAsync(clienteDto.CpfCliente);

            //se cpf ja existir (for true)
            if (cpfExistente) 
            {
                throw new BusinessException("CPF já cadastrado");
            }


            var cliente = new Cliente
            {
                NomeCliente = clienteDto.NomeCliente,
                CpfCliente = clienteDto.CpfCliente,
                EnderecoCliente = clienteDto.EnderecoCliente,
                TelefoneCliente = clienteDto.TelefoneCliente
            };

            var clienteAdd = await _clienteRepository.AdicionarAsync(cliente);

            return MapearResponseDto(clienteAdd);
        }

        public async Task AtualizarAsync(int id, ClienteRequestDto clienteDto)
        {
            var cliente = await _clienteRepository.BuscarPorIdAsync(id);

            if (cliente is null)
            {
                return;
            }

            var cpfExiste = await _clienteRepository
                .ExistePorCpfAsync(clienteDto.CpfCliente, id);

            if (cpfExiste)
            {
                throw new BusinessException("CPF já cadastrado para outro cliente");
            }

            cliente.NomeCliente = clienteDto.NomeCliente;
            cliente.CpfCliente = clienteDto.CpfCliente;
            cliente.EnderecoCliente = clienteDto.EnderecoCliente;
            cliente.TelefoneCliente = clienteDto.TelefoneCliente;

            await _clienteRepository.AtualizarAsync(cliente);

        }

        public async Task<ClienteResponseDto?> BuscarPorIdAsync(int id)
        {
            var cliente = await _clienteRepository.BuscarPorIdAsync(id);

            if (cliente is null) 
            {
                return null;
            }

            return MapearResponseDto(cliente);
        }

        public async Task<List<ClienteResponseDto>> BuscarTodosAsync()
        {
            var clientes = await _clienteRepository.BuscarTodosAsync();

            //esse select comunica que cada cliente vai ser criado um responseDTO
            return clientes
                .Select(MapearResponseDto)
                .ToList();
        }

        public async Task ExcluirAsync(int id)
        {
            var cliente = await _clienteRepository.BuscarPorIdAsync(id);

            if (cliente is null)
            {
                return;
            }

            await _clienteRepository.ExcluirAsync(cliente);
        }
    }
}
