﻿// <auto-generated />
using System;
using InventoryControl.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryControl.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220827185754_update-max-lenght")]
    partial class updatemaxlenght
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.3.22175.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InventoryControl.Domain.Entities.Acesso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("Situacao")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Acesso", (string)null);
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.Atendimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("ClienteAtrasado")
                        .HasColumnType("bit");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("ObservacaoAtendimento")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<decimal?>("ValorAtendimento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("ValorPago")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Atendimento");
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.MapPerfilUsuariosAcessos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AcessoId")
                        .HasColumnType("int");

                    b.Property<int?>("PerfilUsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AcessoId");

                    b.HasIndex("PerfilUsuarioId");

                    b.ToTable("MapPerfilUsuariosAcessos", (string)null);
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.MapServicosAtendimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AtendimentoId")
                        .HasColumnType("int");

                    b.Property<int>("ServicoId")
                        .HasColumnType("int");

                    b.Property<decimal?>("ValorCobrado")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AtendimentoId");

                    b.HasIndex("ServicoId");

                    b.ToTable("MapServicosAtendimento");
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.PerfilUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("Situacao")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PerfilUsuario", (string)null);
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.Servico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Servicos");
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CpfCnpj")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PerfilUsuarioId")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("Situacao")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PerfilUsuarioId");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.Atendimento", b =>
                {
                    b.HasOne("InventoryControl.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Atendimento")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.MapPerfilUsuariosAcessos", b =>
                {
                    b.HasOne("InventoryControl.Domain.Entities.Acesso", "Acesso")
                        .WithMany("MapPerfilUsuariosAcessos")
                        .HasForeignKey("AcessoId");

                    b.HasOne("InventoryControl.Domain.Entities.PerfilUsuario", "PerfilUsuario")
                        .WithMany("MapPerfilUsuariosAcessos")
                        .HasForeignKey("PerfilUsuarioId");

                    b.Navigation("Acesso");

                    b.Navigation("PerfilUsuario");
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.MapServicosAtendimento", b =>
                {
                    b.HasOne("InventoryControl.Domain.Entities.Atendimento", "Atendimento")
                        .WithMany("MapServicosAtendimentos")
                        .HasForeignKey("AtendimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryControl.Domain.Entities.Servico", "Servico")
                        .WithMany("MapServicosAtendimentos")
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendimento");

                    b.Navigation("Servico");
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.Usuario", b =>
                {
                    b.HasOne("InventoryControl.Domain.Entities.PerfilUsuario", "PerfilUsuario")
                        .WithMany("Usuarios")
                        .HasForeignKey("PerfilUsuarioId");

                    b.Navigation("PerfilUsuario");
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.Acesso", b =>
                {
                    b.Navigation("MapPerfilUsuariosAcessos");
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.Atendimento", b =>
                {
                    b.Navigation("MapServicosAtendimentos");
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.Cliente", b =>
                {
                    b.Navigation("Atendimento");
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.PerfilUsuario", b =>
                {
                    b.Navigation("MapPerfilUsuariosAcessos");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("InventoryControl.Domain.Entities.Servico", b =>
                {
                    b.Navigation("MapServicosAtendimentos");
                });
#pragma warning restore 612, 618
        }
    }
}
