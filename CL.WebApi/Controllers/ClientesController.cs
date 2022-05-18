using CL.Core.Domain;
using CL.Core.Shared.ModelViews;
using CL.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public ClientesController(IClienteManager clienteManager)
        {
            _clienteManager = clienteManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _clienteManager.GetClientesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _clienteManager.GetClienteAsync(id));
        }

        // Não precisa do [FromBody] por ser um objeto complexo, isso é implicito. É necessário colocar em tipos primitivos.
        [HttpPost]
        public async Task<IActionResult> Post(NovoCliente novoCliente) 
        {
            var clienteInserido = await _clienteManager.InsertClienteAsync(novoCliente);
            return CreatedAtAction(nameof(Get), new { id = clienteInserido.Id }, clienteInserido);
        }

        [HttpPut]
        public async Task<IActionResult> Put(AlteraCliente alteraCliente)
        {
            var clienteAtualizado = await _clienteManager.UpdateClienteAsync(alteraCliente);
            if (clienteAtualizado == null) return NotFound();
            return Ok(clienteAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteManager.DeleteClienteAsync(id);
            return NoContent();
        }
    }
}