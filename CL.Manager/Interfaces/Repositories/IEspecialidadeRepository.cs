using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CL.Manager.Interfaces.Repositories
{
    public interface IEspecialidadeRepository
    {
        Task<bool> ExisteAsync(int id);
    }
}
