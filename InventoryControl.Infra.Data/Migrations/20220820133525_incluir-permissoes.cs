using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryControl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class incluirpermissoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Acesso]([Nome], [Descricao], [Situacao]) VALUES('VISUALIZAR_DASHBOARD', 'Vizualizar DashBoard', 1)");
            migrationBuilder.Sql("INSERT INTO MapPerfilUsuariosAcessos ([AcessoId], [PerfilUsuarioId]) VALUES((SELECT TOP(1) Id FROM Acesso WHERE Nome = 'VISUALIZAR_DASHBOARD'), (SELECT TOP(1) Id FROM PerfilUsuario WHERE Nome = 'ADMIN'))");

            migrationBuilder.Sql("INSERT INTO [dbo].[Acesso]([Nome], [Descricao], [Situacao]) VALUES('MANTER_CLIENTES', 'CADASTRO CONSULTA E EDIÇÃO DE CLIENTES', 1)");
            migrationBuilder.Sql("INSERT INTO MapPerfilUsuariosAcessos ([AcessoId], [PerfilUsuarioId]) VALUES((SELECT TOP(1) Id FROM Acesso WHERE Nome = 'MANTER_CLIENTES'), (SELECT TOP(1) Id FROM PerfilUsuario WHERE Nome = 'ADMIN'))");

            migrationBuilder.Sql("INSERT INTO [dbo].[Acesso]([Nome], [Descricao], [Situacao]) VALUES('MANTER_ATENDIMENTOS', 'CADASTRO CONSULTA E EDIÇÃO DE ATENDIMENTOS', 1)");
            migrationBuilder.Sql("INSERT INTO MapPerfilUsuariosAcessos ([AcessoId], [PerfilUsuarioId]) VALUES((SELECT TOP(1) Id FROM Acesso WHERE Nome = 'MANTER_ATENDIMENTOS'), (SELECT TOP(1) Id FROM PerfilUsuario WHERE Nome = 'ADMIN'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MapPerfilUsuariosAcessos WHERE AcessoId = (SELECT TOP(1) Id FROM Acesso WHERE Nome = 'ADMIN') AND PerfilUsuarioId = (SELECT TOP(1) Id FROM PerfilUsuario WHERE Nome = 'VISUALIZAR_DASHBOARD')");
            migrationBuilder.Sql("DELETE FROM Acesso WHERE Nome = 'VISUALIZAR_DASHBOARD'");

            migrationBuilder.Sql("DELETE FROM MapPerfilUsuariosAcessos WHERE AcessoId = (SELECT TOP(1) Id FROM Acesso WHERE Nome = 'ADMIN') AND PerfilUsuarioId = (SELECT TOP(1) Id FROM PerfilUsuario WHERE Nome = 'MANTER_CLIENTES')");
            migrationBuilder.Sql("DELETE FROM Acesso WHERE Nome = 'MANTER_CLIENTES'");

            migrationBuilder.Sql("DELETE FROM MapPerfilUsuariosAcessos WHERE AcessoId = (SELECT TOP(1) Id FROM Acesso WHERE Nome = 'ADMIN') AND PerfilUsuarioId = (SELECT TOP(1) Id FROM PerfilUsuario WHERE Nome = 'MANTER_ATENDIMENTOS')");
            migrationBuilder.Sql("DELETE FROM Acesso WHERE Nome = 'MANTER_ATENDIMENTOS'");
        }
    }
}
