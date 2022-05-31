using CL.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CL.Data.Configuration
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(p => p.ClienteId);
            builder
                .Property(p => p.Estado)
                .HasConversion(
                    p => p.ToString(),
                    p => (Estado)Enum.Parse(typeof(Estado), p)
                );
        }
    }
}
