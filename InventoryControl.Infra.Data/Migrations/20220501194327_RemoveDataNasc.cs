using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryControl.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDataNasc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
