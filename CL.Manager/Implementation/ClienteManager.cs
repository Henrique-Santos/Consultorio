using AutoMapper;
using CL.Core.Domain;
using CL.Core.Shared.ModelViews;
using CL.Manager.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ClienteManager> _logger;

        public ClienteManager(IClienteRepository clienteRepository, IMapper mapper, ILogger<ClienteManager> logger)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _logger = logger;
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
            _logger.LogInformation("Chamada de negócio para inserir um cliente.");
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
