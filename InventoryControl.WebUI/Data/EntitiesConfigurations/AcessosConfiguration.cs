﻿using AWASP.WebUI.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AWASP.WebUI.Data.EntitiesConfigurations
{
    public class AcessosConfiguration : IEntityTypeConfiguration<Acesso>
    {
        public void Configure(EntityTypeBuilder<Acesso> builder)
        {
            builder.ToTable("Acesso");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).HasMaxLength(30).IsRequired();
            builder.Property(u => u.Descricao).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Situacao).IsRequired();
        }
    }
}
