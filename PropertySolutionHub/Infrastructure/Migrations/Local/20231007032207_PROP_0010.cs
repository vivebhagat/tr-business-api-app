using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionHub.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0010 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                schema: "data",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "State",
                schema: "data",
                table: "Property");

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                schema: "data",
                table: "Property",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                schema: "data",
                table: "Property");

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "data",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "data",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
