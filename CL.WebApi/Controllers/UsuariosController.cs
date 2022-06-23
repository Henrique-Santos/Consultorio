using CL.Core.Domain;
using CL.Core.Shared.ModelViews.Usuario;

namespace CL.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioManager manager;

    public UsuariosController(IUsuarioManager manager)
    {
        this.manager = manager;
    }

    [HttpGet]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] Usuario usuario)
    {
        var usuarioLogado = await manager.ValidaUsuarioEGeraToken(usuario);
        if (usuarioLogado != null) return Ok(usuarioLogado);
        return Unauthorized();
    }

    [Authorize(Roles = "Presidente, Diretor")] // Necessita um token valido para usar o endpoint e ter acesso de (Presidente e Diretor)
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var login = User.Identity.Name; // Retorna o nome de Login do usuario
        var usuario = await manager.GetAsync(login);
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Post(NovoUsuario novoUsuario)
    {
        var usuarioInserido = await manager.InsertAsync(novoUsuario);
        return CreatedAtAction(nameof(Get), new { login = novoUsuario.Login }, usuarioInserido);
    }
}
