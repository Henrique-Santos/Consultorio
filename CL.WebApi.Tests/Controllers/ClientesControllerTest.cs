using CL.Core.Domain;
using CL.Core.Shared.ModelViews.Cliente;
using CL.FakeData.ClienteData;
using CL.Manager.Interfaces.Managers;
using CL.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CL.WebApi.Tests.Controllers
{
    public class ClientesControllerTest
    {
        private readonly IClienteManager _manager;
        private readonly ILogger<ClientesController> _logger;
        private readonly ClientesController _controller;
        private readonly List<ClienteView> _clientes;
        private readonly ClienteView _cliente;

        public ClientesControllerTest()
        {
            _manager = Substitute.For<IClienteManager>(); // Mockando IClienteManager com o NSubstitute
            _logger = Substitute.For<ILogger<ClientesController>>();
            _controller = new ClientesController(_manager, _logger);
            _cliente = new ClienteViewFaker().Generate();
            _clientes = new ClienteViewFaker().Generate(10);
        }

        [Fact]
        public async Task Get_Ok()
        {
            var controle = new List<ClienteView>();
            _clientes.ForEach(c => controle.Add(c.CloneTipado())); // O clone foi necessario para que ao comparar o resultado, a lista esteja apontando para outra referencia

            _manager.GetClientesAsync().Returns(_clientes);
            var resultado = (ObjectResult)await _controller.Get();

            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(controle);
        }
    }
}