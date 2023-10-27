using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionHub.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0013 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitType",
                schema: "data",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "data",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "data",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "data",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommunityTypes",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionStatus",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Communities",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PriceFrom = table.Column<double>(type: "float", nullable: false),
                    PriceTo = table.Column<double>(type: "float", nullable: false),
                    BedFrom = table.Column<int>(type: "int", nullable: false),
                    BedTo = table.Column<int>(type: "int", nullable: false),
                    BathFrom = table.Column<int>(type: "int", nullable: false),
                    BathTo = table.Column<int>(type: "int", nullable: false),
                    AreaFrom = table.Column<int>(type: "int", nullable: false),
                    AreaTo = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    CommunityTypeId = table.Column<int>(type: "int", nullable: false),
                    LandArea = table.Column<double>(type: "float", nullable: false),
                    NumberOfUnits = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Communities_CommunityTypes_CommunityTypeId",
                        column: x => x.CommunityTypeId,
                        principalSchema: "data",
                        principalTable: "CommunityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Communities_ConstructionStatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "data",
                        principalTable: "ConstructionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Communities_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "data",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunityToPropertyMaps",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommunityId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityToPropertyMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunityToPropertyMaps_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalSchema: "data",
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityToPropertyMaps_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "data",
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Communities_CommunityTypeId",
                schema: "data",
                table: "Communities",
                column: "CommunityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_OrganizationId",
                schema: "data",
                table: "Communities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_StatusId",
                schema: "data",
                table: "Communities",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityToPropertyMaps_CommunityId",
                schema: "data",
                table: "CommunityToPropertyMaps",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityToPropertyMaps_PropertyId",
                schema: "data",
                table: "CommunityToPropertyMaps",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunityToPropertyMaps",
                schema: "data");

            migrationBuilder.DropTable(
                name: "Communities",
                schema: "data");

            migrationBuilder.DropTable(
                name: "CommunityTypes",
                schema: "data");

            migrationBuilder.DropTable(
                name: "ConstructionStatus",
                schema: "data");

            migrationBuilder.DropColumn(
                name: "UnitType",
                schema: "data",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Address",
                schema: "data",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "data",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "data",
                table: "Organizations");
        }
    }
}
