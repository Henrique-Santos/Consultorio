using AutoMapper;
using CL.Core.Domain;
using CL.Core.Shared.ModelViews;
using CL.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CL.Manager.Implementation
{
    public class ClienteManager : IClienteManager
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteManager(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            return await _clienteRepository.GetClientesAsync();
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            return await _clienteRepository.GetClienteAsync(id);
        }

        public async Task DeleteClienteAsync(int id)
        {
            await _clienteRepository.DeleteClienteAsync(id);
        }

        public Task<Cliente> InsertClienteAsync(NovoCliente novoCliente)
        {
            var cliente = _mapper.Map<Cliente>(novoCliente);
            return _clienteRepository.InsertClienteAsync(cliente);
        }

        public Task<Cliente> UpdateClienteAsync(AlteraCliente alteraCliente)
        {
            var cliente = _mapper.Map<Cliente>(alteraCliente);
            return _clienteRepository.UpdateClienteAsync(cliente);
        }
    }
}
