using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionHub.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0017 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemoteId",
                schema: "data",
                table: "CommunityToPropertyMaps");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                schema: "data",
                table: "Communities",
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
                table: "Communities");

            migrationBuilder.AddColumn<int>(
                name: "RemoteId",
                schema: "data",
                table: "CommunityToPropertyMaps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
