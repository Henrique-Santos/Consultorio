using System;
using System.Collections.Generic;
using System.Text;

namespace CL.Core.Shared.ModelViews
{
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
        public char Sexo { get; set; }
        /// <summary>
        /// Telefone do cliente
        /// </summary>
        /// <example>229999999999</example>
        public string Telefone { get; set; }
        /// <summary>
        /// Documento do cliente: CNH, CPF ou RG
        /// </summary>
        /// <example>123456789</example>
        public string Documento { get; set; }
        public NovoEndereco Endereco { get; set; }
    }
}
