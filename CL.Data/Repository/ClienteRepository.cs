using CL.Core.Domain;
using CL.Core.Shared.ModelViews.Cliente;
using CL.Data.Context;
using CL.Manager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CL.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly CLContext _context;

        public ClienteRepository(CLContext context)
        {
            _context = context;
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Telefones)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes
                .AsNoTracking()
                .Include(c => c.Endereco)
                .Include(c => c.Telefones)
                .ToListAsync();
        }

        public async Task<Cliente> InsertClienteAsync(Cliente cliente)
        {
            await _context.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            var clienteConsultado = await _context.Clientes.FindAsync(cliente.Id);
            if (clienteConsultado == null) return null;
            _context.Entry(clienteConsultado).CurrentValues.SetValues(cliente);
            await _context.SaveChangesAsync();
            return clienteConsultado;
        }

        public async Task<Cliente> DeleteClienteAsync(int id)
        {
            var clienteConsultado = await _context.Clientes.FindAsync(id);
            if (clienteConsultado == null) return null;
            var clienteRemovido = _context.Clientes.Remove(clienteConsultado);
            await _context.SaveChangesAsync();
            return clienteRemovido.Entity;
        }
    }
}
