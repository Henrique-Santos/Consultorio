using CL.Core.Shared.ModelViews.Endereco;
using CL.Core.Shared.ModelViews.Telefone;
using System;
using System.Collections.Generic;

namespace CL.Core.Shared.ModelViews.Cliente;

/// <summary>
/// Objeto utilizado para inserção de um novo cliente
/// </summary>
public class NovoCliente
{
    /// <summary>
    /// Nome do cliente
    /// </summary>
    /// <example>João do Caminhão</example>
    public string Nome { get; set; }
    /// <summary>
    /// Data de nascimento do cliente
    /// </summary>
    /// <example>2000-04-08</example>
    public DateTime DataNascimento { get; set; }
    /// <summary>
    /// Sexo do cliente
    /// </summary>
    /// <example>M</example>
    public SexoView Sexo { get; set; }
    /// <summary>
    /// Documento do cliente: CNH, CPF ou RG
    /// </summary>
    /// <example>123456789</example>
    public string Documento { get; set; }
    public NovoEndereco Endereco { get; set; }
    public ICollection<NovoTelefone> Telefones { get; set; }
}
