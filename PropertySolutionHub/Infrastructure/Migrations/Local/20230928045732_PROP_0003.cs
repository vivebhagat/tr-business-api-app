using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionHub.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientId",
                schema: "data",
                table: "Property",
                newName: "RemoteId");

            migrationBuilder.AddColumn<int>(
                name: "RemoteId",
                schema: "data",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemoteId",
                schema: "data",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "RemoteId",
                schema: "data",
                table: "Property",
                newName: "ClientId");
        }
    }
}
