using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryControl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Acesso]([Nome], [Descricao], [Situacao]) VALUES('CADASTRO_PRODUTOS', 'Cadastro de produtos', 1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[PerfilUsuario]([Nome], [Situacao]) VALUES('ADMIN',1)");
            migrationBuilder.Sql("INSERT INTO MapPerfilUsuariosAcessos ([AcessoId], [PerfilUsuarioId]) VALUES((SELECT TOP(1) Id FROM Acesso WHERE Nome = 'CADASTRO_PRODUTOS'), (SELECT TOP(1) Id FROM PerfilUsuario WHERE Nome = 'ADMIN'))");
            migrationBuilder.Sql("INSERT INTO [dbo].[Usuarios]([Login],[Senha],[CpfCnpj],[Nome],[Email],[DataNascimento],[PerfilUsuarioId],[Situacao])VALUES('admin' , 'admin@123' , null , 'admin', 'admin@devscansados.com', '2022-01-01', (SELECT TOP(1) Id FROM PerfilUsuario WHERE Nome = 'ADMIN'),  1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Usuarios WHERE Login = 'admin'");
            migrationBuilder.Sql("DELETE FROM MapPerfilUsuariosAcessos WHERE AcessoId = (SELECT TOP(1) Id FROM Acesso WHERE Nome = 'ADMIN') AND PerfilUsuarioId = (SELECT TOP(1) Id FROM PerfilUsuario WHERE Nome = 'CADASTRO_PRODUTOS')");
            migrationBuilder.Sql("DELETE FROM PerfilUsuario WHERE Nome = 'ADMIN'");
            migrationBuilder.Sql("DELETE FROM Acesso WHERE Nome = 'CADASTRO_PRODUTOS'");
        }
    }
}
