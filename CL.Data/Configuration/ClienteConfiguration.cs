using CL.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CL.Data.Configuration
{
    // Usando FluentAPI ao invez de Anotações nos modelos
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder
                .Property(p => p.Nome)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
