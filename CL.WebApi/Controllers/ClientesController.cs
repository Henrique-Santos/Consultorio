using CL.Core.Domain;
using CL.Core.Shared.ModelViews.Cliente;
using SerilogTimings;

namespace CL.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IClienteManager _clienteManager;
    private readonly ILogger<ClientesController> _logger;

    public ClientesController(IClienteManager clienteManager, ILogger<ClientesController> logger)
    {
        _clienteManager = clienteManager;
        _logger = logger;
    }

    /// <summary>
    /// Retorna todos os cliente
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)] // Documentando os possiveis retornos para o swagger
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var clientes = await _clienteManager.GetClientesAsync();
        if (!clientes.Any()) return NotFound(); 
        return Ok(clientes);
    }

    /// <summary>
    /// Retorna um cliente com base no identificador 
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var cliente = await _clienteManager.GetClienteAsync(id);
        if (cliente.Id == 0) return NotFound();
        return Ok(cliente);
    }

    /// <summary>
    /// Adiciona um cliente
    /// </summary>
    /// <param name="novoCliente"></param>
    [HttpPost] // Não precisa do [FromBody] por ser um objeto complexo, isso é implicito. É necessário colocar em tipos primitivos.
    [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(NovoCliente novoCliente) 
    {
        _logger.LogInformation("Objeto recebido {@novoCliente}", novoCliente);
        ClienteView clienteInserido;
        using (Operation.Time("Tempo de adição de um novo cliente")) 
        {
            _logger.LogInformation("Foi requisitada a inserção de um novo cliente");
            clienteInserido = await _clienteManager.InsertClienteAsync(novoCliente);
        }
        return CreatedAtAction(nameof(Get), new { id = clienteInserido.Id }, clienteInserido);
    }

    /// <summary>
    /// Atualiza um cliente
    /// </summary>
    /// <param name="alteraCliente"></param>
    [HttpPut]
    [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(AlteraCliente alteraCliente)
    {
        var clienteAtualizado = await _clienteManager.UpdateClienteAsync(alteraCliente);
        if (clienteAtualizado == null) return NotFound();
        return Ok(clienteAtualizado);
    }

    /// <summary>
    /// Remove um cliente
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Cliente), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var clienteExcluido = await _clienteManager.DeleteClienteAsync(id);
        if (clienteExcluido == null) return NotFound();
        return NoContent();
    }
}