using AWASP.WebUI.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AWASP.WebUI.Data.EntitiesConfigurations
{
    public class MapPerfilUsuarioAcessosConfiguration : IEntityTypeConfiguration<MapPerfilUsuariosAcessos>
    {
        public void Configure(EntityTypeBuilder<MapPerfilUsuariosAcessos> builder)
        {
            builder.ToTable("MapPerfilUsuariosAcessos");
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.PerfilUsuario)
                .WithMany(perfilUsuario => perfilUsuario.MapPerfilUsuariosAcessos)
                .HasForeignKey(m => m.PerfilUsuarioId);

            builder.HasOne(m => m.Acesso)
                .WithMany(acesso => acesso.MapPerfilUsuariosAcessos)
                .HasForeignKey(m => m.AcessoId);
        }
    }
}
