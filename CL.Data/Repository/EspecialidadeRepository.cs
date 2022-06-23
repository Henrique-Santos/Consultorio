using CL.Data.Context;
using CL.Manager.Interfaces.Repositories;
using System.Threading.Tasks;

namespace CL.Data.Repository;

public class EspecialidadeRepository : IEspecialidadeRepository
{
    private readonly CLContext _context;

    public EspecialidadeRepository(CLContext context)
    {
        _context = context;
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Especialidades.FindAsync(id) != null;
    }
}
