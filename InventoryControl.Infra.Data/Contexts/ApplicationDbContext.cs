﻿using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Infra.Data.Context
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }

        public DbSet<Usuario> Users { get; set; }
        public DbSet<Acesso> Acessos { get; set; }
        public DbSet<MapPerfilUsuariosAcessos> MapPerfilUsuariosAcessos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<MapServicosAtendimento> MapServicosAtendimentos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}