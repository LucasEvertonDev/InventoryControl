using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryControl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthenticateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acesso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acesso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfilUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapPerfilUsuariosAcessos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilUsuarioId = table.Column<int>(type: "int", nullable: true),
                    AcessoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapPerfilUsuariosAcessos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapPerfilUsuariosAcessos_Acesso_AcessoId",
                        column: x => x.AcessoId,
                        principalTable: "Acesso",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MapPerfilUsuariosAcessos_PerfilUsuario_PerfilUsuarioId",
                        column: x => x.PerfilUsuarioId,
                        principalTable: "PerfilUsuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CpfCnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PerfilUsuarioId = table.Column<int>(type: "int", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_PerfilUsuario_PerfilUsuarioId",
                        column: x => x.PerfilUsuarioId,
                        principalTable: "PerfilUsuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MapPerfilUsuariosAcessos_AcessoId",
                table: "MapPerfilUsuariosAcessos",
                column: "AcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_MapPerfilUsuariosAcessos_PerfilUsuarioId",
                table: "MapPerfilUsuariosAcessos",
                column: "PerfilUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PerfilUsuarioId",
                table: "Usuarios",
                column: "PerfilUsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MapPerfilUsuariosAcessos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Acesso");

            migrationBuilder.DropTable(
                name: "PerfilUsuario");
        }
    }
}
