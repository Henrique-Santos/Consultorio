using CL.Core.Domain;
using CL.Data.Context;
using CL.Manager.Interfaces;
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
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }
    }
}
