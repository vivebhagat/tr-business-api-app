using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionHub.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0014 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "data",
                table: "Property");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                schema: "data",
                table: "Communities",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                schema: "data",
                table: "Communities");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "data",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
