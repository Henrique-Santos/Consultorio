using CL.Core.Domain;
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
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _clienteRepository.GetClientesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _clienteRepository.GetClienteAsync(id));
        }

        // Não precisa do [FromBody] por ser um objeto complexo, isso é implicito. É necessário colocar em tipos primitivos.
        [HttpPost]
        public async Task<IActionResult> Post(Cliente cliente) 
        {
            var clienteInserido = await _clienteRepository.InsertClienteAsync(cliente);
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Cliente cliente)
        {
            var clienteAtualizado = await _clienteRepository.UpdateClienteAsync(cliente);
            if (clienteAtualizado == null) return NotFound();
            return Ok(clienteAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteRepository.DeleteClienteAsync(id);
            return NoContent();
        }
    }
}
