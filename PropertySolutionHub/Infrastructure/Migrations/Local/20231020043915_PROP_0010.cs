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

            migrationBuilder.AddColumn<int>(
                name: "BusinessUserId",
                schema: "data",
                table: "Property",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                schema: "data",
                table: "Property",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "PurchasePrice",
                schema: "data",
                table: "Contracts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ProposedPurchasePrice",
                schema: "data",
                table: "ContractRequests",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Property_BusinessUserId",
                schema: "data",
                table: "Property",
                column: "BusinessUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_BusinessUsers_BusinessUserId",
                schema: "data",
                table: "Property",
                column: "BusinessUserId",
                principalSchema: "data",
                principalTable: "BusinessUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_BusinessUsers_BusinessUserId",
                schema: "data",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_BusinessUserId",
                schema: "data",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "BusinessUserId",
                schema: "data",
                table: "Property");

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

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasePrice",
                schema: "data",
                table: "Contracts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProposedPurchasePrice",
                schema: "data",
                table: "ContractRequests",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
