﻿using CL.Core.Domain;
using CL.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CL.Data.Context
{
    public class CLContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        public CLContext(DbContextOptions option): base(option) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ClienteConfiguration());
            builder.ApplyConfiguration(new EnderecoConfiguration());
            builder.ApplyConfiguration(new TelefoneConfiguration());
        }
    }
}
