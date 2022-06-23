using CL.Core.Domain;
using CL.Data.Context;
using CL.Manager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CL.Data.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly CLContext context;

    public UsuarioRepository(CLContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAsync()
    {
        return await context.Usuarios
            .Include(u => u.Funcoes)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Usuario> GetAsync(string login)
    {
        return await context.Usuarios
            .Include(u => u.Funcoes)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Login == login);
    }

    public async Task<Usuario> InsertAsync(Usuario usuario)
    {
        await InsertUsuarioFuncaoAsync(usuario);
        await context.Usuarios.AddAsync(usuario);
        await context.SaveChangesAsync();
        return usuario;
    }

    private async Task InsertUsuarioFuncaoAsync(Usuario usuario)
    {
        var funcoesConsultadas = new List<Funcao>();
        foreach (var funcao in usuario.Funcoes)
        {
            var funcaoConsultada = await context.Funcoes.FindAsync(funcao.Id);
            funcoesConsultadas.Add(funcaoConsultada);
        }
        usuario.Funcoes = funcoesConsultadas;
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        var usuarioConsultado = await context.Usuarios.FindAsync(usuario.Login);
        if (usuarioConsultado == null)
        {
            return null;
        }
        context.Entry(usuarioConsultado).CurrentValues.SetValues(usuario);
        await context.SaveChangesAsync();
        return usuarioConsultado;
    }
}