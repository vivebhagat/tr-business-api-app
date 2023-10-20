using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionHub.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_BusinessUsers_BusinessUserId",
                schema: "data",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "BusinessUserId",
                schema: "data",
                table: "Property",
                newName: "PropertyManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_BusinessUserId",
                schema: "data",
                table: "Property",
                newName: "IX_Property_PropertyManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_BusinessUsers_PropertyManagerId",
                schema: "data",
                table: "Property",
                column: "PropertyManagerId",
                principalSchema: "data",
                principalTable: "BusinessUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_BusinessUsers_PropertyManagerId",
                schema: "data",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "PropertyManagerId",
                schema: "data",
                table: "Property",
                newName: "BusinessUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_PropertyManagerId",
                schema: "data",
                table: "Property",
                newName: "IX_Property_BusinessUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_BusinessUsers_BusinessUserId",
                schema: "data",
                table: "Property",
                column: "BusinessUserId",
                principalSchema: "data",
                principalTable: "BusinessUsers",
                principalColumn: "Id");
        }
    }
}
