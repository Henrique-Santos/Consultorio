using AutoMapper;
using CL.Core.Domain;
using CL.Core.Shared.ModelViews.Usuario;
using CL.Manager.Interfaces.Managers;
using CL.Manager.Interfaces.Repositories;
using CL.Manager.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CL.Manager.Implementation;

public class UsuarioManager : IUsuarioManager
{
    private readonly IUsuarioRepository repository;
    private readonly IMapper mapper;
    private readonly IJWTService jwtService;

    public UsuarioManager(IUsuarioRepository repository, IMapper mapper, IJWTService jwtService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.jwtService = jwtService;
    }

    public async Task<IEnumerable<UsuarioView>> GetAsync()
    {
        return mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioView>>(await repository.GetAsync());
    }

    public async Task<UsuarioView> GetAsync(string login)
    {
        return mapper.Map<Usuario, UsuarioView>(await repository.GetAsync(login));
    }

    public async Task<UsuarioView> InsertAsync(NovoUsuario novoUsuario)
    {
        var usuario = mapper.Map<NovoUsuario, Usuario>(novoUsuario);
        ConverteSenhaEmHash(usuario);
        return mapper.Map<Usuario, UsuarioView>(await repository.InsertAsync(usuario));
    }

    private void ConverteSenhaEmHash(Usuario usuario)
    {
        var passwordHasher = new PasswordHasher<Usuario>();
        usuario.Senha = passwordHasher.HashPassword(usuario, usuario.Senha); // Adicionando hash a senha do usuario
    }

    public async Task<UsuarioView> UpdateUsuarioAsync(Usuario usuario)
    {
        ConverteSenhaEmHash(usuario);
        return mapper.Map<Usuario, UsuarioView>(await repository.UpdateAsync(usuario));
    }

    public async Task<UsuarioLogado> ValidaUsuarioEGeraToken(Usuario usuario)
    {
        var usuarioConsultado = await repository.GetAsync(usuario.Login);
        if (usuarioConsultado == null) return null;
        var ehValido = await ValidaEAtualizaHashAsync(usuario, usuarioConsultado);
        if (!ehValido) return null;
        var usuarioLogado = mapper.Map<UsuarioLogado>(usuarioConsultado);
        usuarioLogado.Token = jwtService.GerarToken(usuarioConsultado);
        return usuarioLogado;
    }

    private async Task<bool> ValidaEAtualizaHashAsync(Usuario usuario, Usuario usuarioConsultado)
    {
        var passwordHasher = new PasswordHasher<Usuario>();
        var status = passwordHasher.VerifyHashedPassword(usuario, usuarioConsultado.Senha, usuario.Senha); // Verifica se a senha é igual a que está cadastrada com base no hash
        switch (status)
        {
            case PasswordVerificationResult.Failed:
                return false;
            case PasswordVerificationResult.Success:
                return true;
            case PasswordVerificationResult.SuccessRehashNeeded:
                await UpdateUsuarioAsync(usuario);
                return true;
            default:
                throw new InvalidOperationException();
        }
    }
}
