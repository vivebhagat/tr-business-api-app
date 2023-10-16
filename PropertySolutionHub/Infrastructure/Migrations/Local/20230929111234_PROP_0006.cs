using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionHub.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractRequests_Customers_CustomerId",
                schema: "data",
                table: "ContractRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Customers_CustomerId",
                schema: "data",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_CustomerId",
                schema: "data",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_ContractRequests_CustomerId",
                schema: "data",
                table: "ContractRequests");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                schema: "data",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                schema: "data",
                table: "ContractRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                schema: "data",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                schema: "data",
                table: "ContractRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerId",
                schema: "data",
                table: "Contracts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractRequests_CustomerId",
                schema: "data",
                table: "ContractRequests",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractRequests_Customers_CustomerId",
                schema: "data",
                table: "ContractRequests",
                column: "CustomerId",
                principalSchema: "data",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Customers_CustomerId",
                schema: "data",
                table: "Contracts",
                column: "CustomerId",
                principalSchema: "data",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
