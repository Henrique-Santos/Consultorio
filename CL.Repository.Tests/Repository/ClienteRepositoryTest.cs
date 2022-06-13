using CL.Core.Domain;
using CL.Data.Context;
using CL.Data.Repository;
using CL.FakeData.ClienteData;
using CL.Manager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using System.Linq;
using NSubstitute;
using CL.FakeData.TelefoneData;

namespace CL.Repository.Tests.Repository
{
    public class ClienteRepositoryTest : IDisposable // Extendo o IDisposable para fazer o cleanup dos testes.
    {
        private readonly IClienteRepository _repository;
        private readonly CLContext _context;
        private readonly Cliente _cliente;
        private ClienteFaker _clienteFaker;

        public ClienteRepositoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CLContext>();
            optionsBuilder.UseInMemoryDatabase("Db_Teste");
            _context = new CLContext(optionsBuilder.Options);
            _repository = new ClienteRepository(_context);
            _clienteFaker = new ClienteFaker();
            _cliente = _clienteFaker.Generate();
        }

        private async Task<List<Cliente>> InsereRegistros()
        {
            var clientes = _clienteFaker.Generate(100);
            foreach (var cliente in clientes)
            {
                cliente.Id = 0; // Para o BD fazer o autoincremento
                await _context.Clientes.AddAsync(cliente);
            }
            await _context.SaveChangesAsync();
            return clientes;
        }

        [Fact]
        public async Task GetClientesAsync_ComRetorno()
        {
            var registros = await InsereRegistros();
            var retorno = await _repository.GetClientesAsync();

            retorno.Should().HaveCount(registros.Count); // Verificando a quantidade de items retornados
            retorno.First().Endereco.Should().NotBeNull(); // Testando o Join da busca de clientes
            retorno.First().Telefones.Should().NotBeNull();
        }

        [Fact]
        public async Task GetClientesAsync_Vazio()
        {
            var retorno = await _repository.GetClientesAsync();

            retorno.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetClienteAsync_Encontrado()
        {
            var registros = await InsereRegistros();

            var retorno = await _repository.GetClienteAsync(registros.First().Id);

            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        public async Task GetClienteAsync_NaoEncontrado()
        {
            var retorno = await _repository.GetClienteAsync(1);

            retorno.Should().BeNull();
        }

        [Fact]
        public async Task InsertClienteAsync_Sucesso()
        {
            var retorno = await _repository.InsertClienteAsync(_cliente); // Não é necessario o clone, pois é pego do BD

            retorno.Should().BeEquivalentTo(_cliente);
        }

        [Fact]
        public async Task UpdateClienteAsync_Sucesso()
        {
            var registros = await InsereRegistros();
            var clienteAlterado = _clienteFaker.Generate();
            clienteAlterado.Id = registros.First().Id;

            var retorno = await _repository.UpdateClienteAsync(clienteAlterado);

            retorno.Should().BeEquivalentTo(clienteAlterado);
        }

        [Fact]
        public async Task UpdateClienteAsync_AdicionaTelefone()
        {
            var registros = await InsereRegistros();
            var clienteAlterado = registros.First();
            clienteAlterado.Telefones.Add(new TelefoneFaker(clienteAlterado.Id).Generate());

            var retorno = await _repository.UpdateClienteAsync(clienteAlterado);

            retorno.Should().BeEquivalentTo(clienteAlterado);
        }

        [Fact]
        public async Task UpdateClienteAsync_RemoveTelefone()
        {
            var registros = await InsereRegistros();
            var clienteAlterado = registros.First();
            clienteAlterado.Telefones.Remove(clienteAlterado.Telefones.First());

            var retorno = await _repository.UpdateClienteAsync(clienteAlterado);

            retorno.Should().BeEquivalentTo(clienteAlterado);
        }

        [Fact]
        public async Task UpdateClienteAsync_RemoveTodosTelefone()
        {
            var registros = await InsereRegistros();
            var clienteAlterado = registros.First();
            clienteAlterado.Telefones.Clear();

            var retorno = await _repository.UpdateClienteAsync(clienteAlterado);

            retorno.Should().BeEquivalentTo(clienteAlterado);
        }

        [Fact]
        public async Task UpdateClienteAsync_NaoEncontrado()
        {
            var retorno = await _repository.UpdateClienteAsync(_cliente);

            retorno.Should().BeNull();
        }

        [Fact]
        public async Task DeleteClienteAsync_Sucesso()
        {
            var registros = await InsereRegistros();
            var retorno = await _repository.DeleteClienteAsync(registros.First().Id);

            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        public async Task DeleteClienteAsync_NaoEncontrado()
        {
            var retorno = await _repository.DeleteClienteAsync(1);

            retorno.Should().BeNull();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Apaga a BD após a execução dos testes
        }
    }
}