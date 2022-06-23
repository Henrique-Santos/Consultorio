using CL.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CL.Data.Configuration;

// Usando FluentAPI ao invez de Anotações nos modelos
public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder
            .Property(p => p.Nome)
            .HasMaxLength(200)
            .IsRequired();
        builder
            .Property(p => p.Sexo)
            .HasConversion( // Salvando o valor em texto do Enum ao invez da enumeração em número
                p => p.ToString(), 
                p => (Sexo)Enum.Parse(typeof(Sexo), p)
            );
    }
}
