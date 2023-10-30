using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionHub.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0018 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
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
                name: "IsPublished",
                schema: "data",
                table: "Property");
        }
    }
}
