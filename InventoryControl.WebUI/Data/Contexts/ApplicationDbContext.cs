using AWASP.WebUI.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace AWASP.WebUI.Data.Contexts
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }

        public DbSet<Usuario> Users { get; set; }
        public DbSet<Acesso> Acessos { get; set; }
        public DbSet<MapPerfilUsuariosAcessos> MapPerfilUsuariosAcessos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}