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
using NSubstitute.ReturnsExtensions;
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
        private readonly List<ClienteView> _clientesView;
        private readonly ClienteView _clienteView;
        private readonly NovoCliente _novoCliente;

        public ClientesControllerTest()
        {
            _manager = Substitute.For<IClienteManager>(); // Mockando IClienteManager com o NSubstitute
            _logger = Substitute.For<ILogger<ClientesController>>();
            _controller = new ClientesController(_manager, _logger);
            _clienteView = new ClienteViewFaker().Generate();
            _clientesView = new ClienteViewFaker().Generate(10);
            _novoCliente = new NovoClienteFaker().Generate();
        }

        [Fact]
        public async Task Get_Ok()
        {
            var controle = new List<ClienteView>();
            _clientesView.ForEach(c => controle.Add(c.CloneTipado())); // O clone foi necessario para que ao comparar o resultado, a lista esteja apontando para outra referencia
            _manager.GetClientesAsync().Returns(_clientesView);

            var resultado = (ObjectResult)await _controller.Get();

            await _manager.Received().GetClientesAsync(); // Verifica se o método foi chamado
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK); // Verifica o status code retornado
            resultado.Value.Should().BeEquivalentTo(controle); // Verifica se a lista retornada é igual a que foi gerada pelo Bogus
        }

        [Fact]
        public async Task Get_NotFound()
        {
            _manager.GetClientesAsync().Returns(new List<ClienteView>());

            var resultado = (StatusCodeResult)await _controller.Get();

            await _manager.Received().GetClientesAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetById_Ok()
        {
            _manager.GetClienteAsync(Arg.Any<int>()).Returns(_clienteView.CloneTipado());

            var resultado = (ObjectResult)await _controller.Get(_clienteView.Id);

            await _manager.Received().GetClienteAsync(Arg.Any<int>());
            resultado.Value.Should().BeEquivalentTo(_clienteView);
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _manager.GetClienteAsync(Arg.Any<int>()).Returns(new ClienteView());

            var resultado = (StatusCodeResult)await _controller.Get(1);

            await _manager.Received().GetClienteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Post_Created()
        {
            _manager.InsertClienteAsync(Arg.Any<NovoCliente>()).Returns(_clienteView.CloneTipado());

            var resultado = (ObjectResult)await _controller.Post(_novoCliente);

            await _manager.Received().InsertClienteAsync(Arg.Any<NovoCliente>());
            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            resultado.Value.Should().BeEquivalentTo(_clienteView);
        }

        [Fact]
        public async Task Put_Ok()
        {
            _manager.UpdateClienteAsync(Arg.Any<AlteraCliente>()).Returns(_clienteView.CloneTipado());

            var resultado = (ObjectResult)await _controller.Put(new AlteraCliente());

            await _manager.Received().UpdateClienteAsync(Arg.Any<AlteraCliente>());
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(_clienteView);
        }

        [Fact]
        public async Task Put_NotFound()
        {
            _manager.UpdateClienteAsync(Arg.Any<AlteraCliente>()).ReturnsNull();

            var resultado = (StatusCodeResult)await _controller.Put(new AlteraCliente());

            await _manager.Received().UpdateClienteAsync(Arg.Any<AlteraCliente>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Delete_NoContent()
        {
            _manager.DeleteClienteAsync(Arg.Any<int>()).Returns(_clienteView);

            var resultado = (StatusCodeResult)await _controller.Delete(1);

            await _manager.Received().DeleteClienteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task Delete_NotFound()
        {
            _manager.DeleteClienteAsync(Arg.Any<int>()).ReturnsNull();

            var resultado = (StatusCodeResult)await _controller.Delete(1);

            await _manager.Received().DeleteClienteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}