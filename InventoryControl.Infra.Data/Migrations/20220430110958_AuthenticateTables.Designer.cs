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
    [Migration("20220430110958_AuthenticateTables")]
    partial class AuthenticateTables
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

                    b.ToTable("Acesso");
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

                    b.ToTable("MapPerfilUsuariosAcessos");
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

                    b.ToTable("PerfilUsuario");
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

                    b.Property<DateTime?>("DataNascimento")
                        .IsRequired()
                        .HasColumnType("datetime2");

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

                    b.ToTable("Usuario");
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

            modelBuilder.Entity("InventoryControl.Domain.Entities.PerfilUsuario", b =>
                {
                    b.Navigation("MapPerfilUsuariosAcessos");

                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
