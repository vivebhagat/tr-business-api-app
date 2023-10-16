using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionHub.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lease_DomainKey_DomainKeyId",
                schema: "data",
                table: "Lease");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaseRequests_DomainKey_DomainKeyId",
                schema: "data",
                table: "LeaseRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_DomainKey_DomainKeyId",
                schema: "data",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyImages_DomainKey_DomainKeyId",
                schema: "data",
                table: "PropertyImages");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyReview_DomainKey_DomainKeyId",
                schema: "data",
                table: "PropertyReview");

            migrationBuilder.DropTable(
                name: "DomainKey",
                schema: "data");

            migrationBuilder.DropIndex(
                name: "IX_PropertyReview_DomainKeyId",
                schema: "data",
                table: "PropertyReview");

            migrationBuilder.DropIndex(
                name: "IX_PropertyImages_DomainKeyId",
                schema: "data",
                table: "PropertyImages");

            migrationBuilder.DropIndex(
                name: "IX_Property_DomainKeyId",
                schema: "data",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_LeaseRequests_DomainKeyId",
                schema: "data",
                table: "LeaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_Lease_DomainKeyId",
                schema: "data",
                table: "Lease");

            migrationBuilder.DropColumn(
                name: "DomainKeyId",
                schema: "data",
                table: "PropertyReview");

            migrationBuilder.DropColumn(
                name: "DomainKeyId",
                schema: "data",
                table: "PropertyImages");

            migrationBuilder.DropColumn(
                name: "DomainKeyId",
                schema: "data",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "DomainKeyId",
                schema: "data",
                table: "LeaseRequests");

            migrationBuilder.DropColumn(
                name: "DomainKeyId",
                schema: "data",
                table: "Lease");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DomainKeyId",
                schema: "data",
                table: "PropertyReview",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DomainKeyId",
                schema: "data",
                table: "PropertyImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DomainKeyId",
                schema: "data",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DomainKeyId",
                schema: "data",
                table: "LeaseRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DomainKeyId",
                schema: "data",
                table: "Lease",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DomainKey",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubDomain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainKey", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyReview_DomainKeyId",
                schema: "data",
                table: "PropertyReview",
                column: "DomainKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_DomainKeyId",
                schema: "data",
                table: "PropertyImages",
                column: "DomainKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_DomainKeyId",
                schema: "data",
                table: "Property",
                column: "DomainKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseRequests_DomainKeyId",
                schema: "data",
                table: "LeaseRequests",
                column: "DomainKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lease_DomainKeyId",
                schema: "data",
                table: "Lease",
                column: "DomainKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lease_DomainKey_DomainKeyId",
                schema: "data",
                table: "Lease",
                column: "DomainKeyId",
                principalSchema: "data",
                principalTable: "DomainKey",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseRequests_DomainKey_DomainKeyId",
                schema: "data",
                table: "LeaseRequests",
                column: "DomainKeyId",
                principalSchema: "data",
                principalTable: "DomainKey",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_DomainKey_DomainKeyId",
                schema: "data",
                table: "Property",
                column: "DomainKeyId",
                principalSchema: "data",
                principalTable: "DomainKey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyImages_DomainKey_DomainKeyId",
                schema: "data",
                table: "PropertyImages",
                column: "DomainKeyId",
                principalSchema: "data",
                principalTable: "DomainKey",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyReview_DomainKey_DomainKeyId",
                schema: "data",
                table: "PropertyReview",
                column: "DomainKeyId",
                principalSchema: "data",
                principalTable: "DomainKey",
                principalColumn: "Id");
        }
    }
}
