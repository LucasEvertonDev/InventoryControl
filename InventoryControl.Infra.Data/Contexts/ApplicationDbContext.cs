using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Infra.Data.Context
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Acesso> Acessos { get; set; }
        public DbSet<MapPerfilUsuariosAcessos> MapPerfilUsuariosAcessos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}