using CL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CL.Manager.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAsync();

        Task<Usuario> GetAsync(string login);

        Task<Usuario> InsertAsync(Usuario usuario);

        Task<Usuario> UpdateAsync(Usuario usuario);
    }
}
