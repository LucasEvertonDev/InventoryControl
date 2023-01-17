using AWASP.WebUI.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AWASP.WebUI.Data.EntitiesConfigurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Login).HasMaxLength(30).IsRequired();
            builder.Property(u => u.Senha).HasMaxLength(20).IsRequired();
            builder.Property(u => u.CpfCnpj).HasMaxLength(14);
            builder.Property(u => u.Email).HasMaxLength(60).IsRequired();
            builder.Property(u => u.Situacao).IsRequired();
            builder.HasOne(u => u.PerfilUsuario).WithMany(e => e.Usuarios).HasForeignKey(a => a.PerfilUsuarioId);
        }
    }
}

