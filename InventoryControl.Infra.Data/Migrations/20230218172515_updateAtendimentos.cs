using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryControl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateAtendimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_Cliente_ClienteId",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK_MapServicosAtendimento_Atendimento_AtendimentoId",
                table: "MapServicosAtendimento");

            migrationBuilder.DropForeignKey(
                name: "FK_MapServicosAtendimento_Servicos_ServicoId",
                table: "MapServicosAtendimento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapServicosAtendimento",
                table: "MapServicosAtendimento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Atendimento",
                table: "Atendimento");

            migrationBuilder.RenameTable(
                name: "MapServicosAtendimento",
                newName: "MapServicosAtendimentos");

            migrationBuilder.RenameTable(
                name: "Atendimento",
                newName: "Atendimentos");

            migrationBuilder.RenameIndex(
                name: "IX_MapServicosAtendimento_ServicoId",
                table: "MapServicosAtendimentos",
                newName: "IX_MapServicosAtendimentos_ServicoId");

            migrationBuilder.RenameIndex(
                name: "IX_MapServicosAtendimento_AtendimentoId",
                table: "MapServicosAtendimentos",
                newName: "IX_MapServicosAtendimentos_AtendimentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Atendimento_ClienteId",
                table: "Atendimentos",
                newName: "IX_Atendimentos_ClienteId");

            migrationBuilder.AddColumn<string>(
                name: "AtendimentoIdExterno",
                table: "MapServicosAtendimentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdExterno",
                table: "MapServicosAtendimentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServicoIdExterno",
                table: "MapServicosAtendimentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClienteIdExterno",
                table: "Atendimentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdExterno",
                table: "Atendimentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapServicosAtendimentos",
                table: "MapServicosAtendimentos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Atendimentos",
                table: "Atendimentos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimentos_Cliente_ClienteId",
                table: "Atendimentos",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MapServicosAtendimentos_Atendimentos_AtendimentoId",
                table: "MapServicosAtendimentos",
                column: "AtendimentoId",
                principalTable: "Atendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MapServicosAtendimentos_Servicos_ServicoId",
                table: "MapServicosAtendimentos",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimentos_Cliente_ClienteId",
                table: "Atendimentos");

            migrationBuilder.DropForeignKey(
                name: "FK_MapServicosAtendimentos_Atendimentos_AtendimentoId",
                table: "MapServicosAtendimentos");

            migrationBuilder.DropForeignKey(
                name: "FK_MapServicosAtendimentos_Servicos_ServicoId",
                table: "MapServicosAtendimentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapServicosAtendimentos",
                table: "MapServicosAtendimentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Atendimentos",
                table: "Atendimentos");

            migrationBuilder.DropColumn(
                name: "AtendimentoIdExterno",
                table: "MapServicosAtendimentos");

            migrationBuilder.DropColumn(
                name: "IdExterno",
                table: "MapServicosAtendimentos");

            migrationBuilder.DropColumn(
                name: "ServicoIdExterno",
                table: "MapServicosAtendimentos");

            migrationBuilder.DropColumn(
                name: "ClienteIdExterno",
                table: "Atendimentos");

            migrationBuilder.DropColumn(
                name: "IdExterno",
                table: "Atendimentos");

            migrationBuilder.RenameTable(
                name: "MapServicosAtendimentos",
                newName: "MapServicosAtendimento");

            migrationBuilder.RenameTable(
                name: "Atendimentos",
                newName: "Atendimento");

            migrationBuilder.RenameIndex(
                name: "IX_MapServicosAtendimentos_ServicoId",
                table: "MapServicosAtendimento",
                newName: "IX_MapServicosAtendimento_ServicoId");

            migrationBuilder.RenameIndex(
                name: "IX_MapServicosAtendimentos_AtendimentoId",
                table: "MapServicosAtendimento",
                newName: "IX_MapServicosAtendimento_AtendimentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Atendimentos_ClienteId",
                table: "Atendimento",
                newName: "IX_Atendimento_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapServicosAtendimento",
                table: "MapServicosAtendimento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Atendimento",
                table: "Atendimento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_Cliente_ClienteId",
                table: "Atendimento",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MapServicosAtendimento_Atendimento_AtendimentoId",
                table: "MapServicosAtendimento",
                column: "AtendimentoId",
                principalTable: "Atendimento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MapServicosAtendimento_Servicos_ServicoId",
                table: "MapServicosAtendimento",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
