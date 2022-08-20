using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryControl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class incluirpermissaoservico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Acesso]([Nome], [Descricao], [Situacao]) VALUES('MANTER_SERVICOS', 'CADASTRO CONSULTA E EDIÇÃO DE ATENDIMENTOS', 1)");
            migrationBuilder.Sql("INSERT INTO MapPerfilUsuariosAcessos ([AcessoId], [PerfilUsuarioId]) VALUES((SELECT TOP(1) Id FROM Acesso WHERE Nome = 'MANTER_SERVICOS'), (SELECT TOP(1) Id FROM PerfilUsuario WHERE Nome = 'ADMIN'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MapPerfilUsuariosAcessos WHERE AcessoId = (SELECT TOP(1) Id FROM Acesso WHERE Nome = 'ADMIN') AND PerfilUsuarioId = (SELECT TOP(1) Id FROM PerfilUsuario WHERE Nome = 'MANTER_SERVICOS')");
            migrationBuilder.Sql("DELETE FROM Acesso WHERE Nome = 'MANTER_SERVICOS'");
        }
    }
}
