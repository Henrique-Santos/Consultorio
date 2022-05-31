using CL.Core.Domain;
using CL.Core.Shared.ModelViews.Cliente;
using CL.Manager.Interfaces.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerilogTimings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CL.WebApi.Controllers
{
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
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)] // Documentando os possiveis retornos
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _clienteManager.GetClientesAsync());
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
            return Ok(await _clienteManager.GetClienteAsync(id));
        }

        /// <summary>
        /// Adiciona um cliente
        /// </summary>
        /// <param name="novoCliente"></param>
        [HttpPost] // Não precisa do [FromBody] por ser um objeto complexo, isso é implicito. É necessário colocar em tipos primitivos.
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(NovoCliente novoCliente) 
        {
            _logger.LogInformation("Objeto recebido {@novoCliente}", novoCliente);
            Cliente clienteInserido;
            using (Operation.Time("Tempo de adição de um novo cliente")) 
            {
                _logger.LogInformation("Foi requisitada a inserção de um novo cliente");
                clienteInserido = await _clienteManager.InsertClienteAsync(novoCliente);
            }
            return CreatedAtAction(nameof(Get), new { id = clienteInserido.Id }, clienteInserido);
            _logger.LogInformation("Fim da requisição de um novo cliente");
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
            await _clienteManager.DeleteClienteAsync(id);
            return NoContent();
        }
    }
}