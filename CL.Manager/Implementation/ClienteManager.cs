﻿using CL.Core.Domain;
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

        public ClienteManager(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
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

        public Task<Cliente> InsertClienteAsync(Cliente cliente)
        {
            return _clienteRepository.InsertClienteAsync(cliente);
        }

        public Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            return _clienteRepository.UpdateClienteAsync(cliente);
        }
    }
}