using CL.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        // GET: api/<ClientesController>
        [HttpGet]
        public IActionResult Get()
        {
            var clients = new List<Cliente>
            {
                new Cliente
                {
                    Id = 1,
                    Nome = "João do Caminhão",
                    DataNascimento = new DateTime(1970, 10, 01)
                },
                new Cliente 
                {
                    Id = 2,
                    Nome = "Maria da Vizinha",
                    DataNascimento = new DateTime(1970, 10, 01)
                }
            };
            return Ok(clients);
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
