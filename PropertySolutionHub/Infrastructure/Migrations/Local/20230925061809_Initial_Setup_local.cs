using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionHub.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class Initial_Setup_local : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "data");

            migrationBuilder.CreateTable(
                name: "AdminRoles",
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
                    table.PrimaryKey("PK_AdminRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessUserRoles",
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
                    table.PrimaryKey("PK_BusinessUserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessUsers",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerRoles",
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
                    table.PrimaryKey("PK_CustomerRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DomainKey",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubDomain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainKey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminToRoleMaps",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminToRoleMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminToRoleMaps_AdminRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "data",
                        principalTable: "AdminRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminToRoleMaps_Admins_AdminId",
                        column: x => x.AdminId,
                        principalSchema: "data",
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessUserToRoleMaps",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessUserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUserToRoleMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessUserToRoleMaps_BusinessUserRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "data",
                        principalTable: "BusinessUserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessUserToRoleMaps_BusinessUsers_BusinessUserId",
                        column: x => x.BusinessUserId,
                        principalSchema: "data",
                        principalTable: "BusinessUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerToRoleMaps",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerToRoleMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerToRoleMaps_CustomerRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "data",
                        principalTable: "CustomerRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerToRoleMaps_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "data",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    ConstructionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bedrooms = table.Column<int>(type: "int", nullable: false),
                    Bathrooms = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    IsFurnished = table.Column<bool>(type: "bit", nullable: false),
                    AirConditioning = table.Column<bool>(type: "bit", nullable: false),
                    IsGarage = table.Column<bool>(type: "bit", nullable: false),
                    Heating = table.Column<bool>(type: "bit", nullable: false),
                    WaterFront = table.Column<bool>(type: "bit", nullable: false),
                    ParkingAvailable = table.Column<bool>(type: "bit", nullable: false),
                    PetFriendly = table.Column<bool>(type: "bit", nullable: false),
                    HasSwimmingPool = table.Column<bool>(type: "bit", nullable: false),
                    HasGym = table.Column<bool>(type: "bit", nullable: false),
                    HasFirePlace = table.Column<bool>(type: "bit", nullable: false),
                    IsSmokingAllowed = table.Column<bool>(type: "bit", nullable: false),
                    IsWheelchairAccessible = table.Column<bool>(type: "bit", nullable: false),
                    DomainKeyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Property_DomainKey_DomainKeyId",
                        column: x => x.DomainKeyId,
                        principalSchema: "data",
                        principalTable: "DomainKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lease",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LeaseStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaseTermMonths = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    IsRenewable = table.Column<bool>(type: "bit", nullable: false),
                    RenewedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsTerminated = table.Column<bool>(type: "bit", nullable: false),
                    TerminationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DomainKeyId = table.Column<int>(type: "int", nullable: true),
                    TerminationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lease", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lease_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "data",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lease_DomainKey_DomainKeyId",
                        column: x => x.DomainKeyId,
                        principalSchema: "data",
                        principalTable: "DomainKey",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lease_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "data",
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaseRequests",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DesiredStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DesiredLeaseTermMonths = table.Column<int>(type: "int", nullable: false),
                    ProposedMonthlyRent = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    DomainKeyId = table.Column<int>(type: "int", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaseRequests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "data",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaseRequests_DomainKey_DomainKeyId",
                        column: x => x.DomainKeyId,
                        principalSchema: "data",
                        principalTable: "DomainKey",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaseRequests_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "data",
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    DomainKeyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImages_DomainKey_DomainKeyId",
                        column: x => x.DomainKeyId,
                        principalSchema: "data",
                        principalTable: "DomainKey",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyImages_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "data",
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyReview",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    DomainKeyId = table.Column<int>(type: "int", nullable: true),
                    IsVisibleToAll = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyReview_DomainKey_DomainKeyId",
                        column: x => x.DomainKeyId,
                        principalSchema: "data",
                        principalTable: "DomainKey",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyReview_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "data",
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminToRoleMaps_AdminId",
                schema: "data",
                table: "AdminToRoleMaps",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminToRoleMaps_RoleId",
                schema: "data",
                table: "AdminToRoleMaps",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUserToRoleMaps_BusinessUserId",
                schema: "data",
                table: "BusinessUserToRoleMaps",
                column: "BusinessUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUserToRoleMaps_RoleId",
                schema: "data",
                table: "BusinessUserToRoleMaps",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerToRoleMaps_CustomerId",
                schema: "data",
                table: "CustomerToRoleMaps",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerToRoleMaps_RoleId",
                schema: "data",
                table: "CustomerToRoleMaps",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Lease_CustomerId",
                schema: "data",
                table: "Lease",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lease_DomainKeyId",
                schema: "data",
                table: "Lease",
                column: "DomainKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lease_PropertyId",
                schema: "data",
                table: "Lease",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseRequests_CustomerId",
                schema: "data",
                table: "LeaseRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseRequests_DomainKeyId",
                schema: "data",
                table: "LeaseRequests",
                column: "DomainKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseRequests_PropertyId",
                schema: "data",
                table: "LeaseRequests",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_DomainKeyId",
                schema: "data",
                table: "Property",
                column: "DomainKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_DomainKeyId",
                schema: "data",
                table: "PropertyImages",
                column: "DomainKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyId",
                schema: "data",
                table: "PropertyImages",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyReview_DomainKeyId",
                schema: "data",
                table: "PropertyReview",
                column: "DomainKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyReview_PropertyId",
                schema: "data",
                table: "PropertyReview",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminToRoleMaps",
                schema: "data");

            migrationBuilder.DropTable(
                name: "BusinessUserToRoleMaps",
                schema: "data");

            migrationBuilder.DropTable(
                name: "CustomerToRoleMaps",
                schema: "data");

            migrationBuilder.DropTable(
                name: "Lease",
                schema: "data");

            migrationBuilder.DropTable(
                name: "LeaseRequests",
                schema: "data");

            migrationBuilder.DropTable(
                name: "PropertyImages",
                schema: "data");

            migrationBuilder.DropTable(
                name: "PropertyReview",
                schema: "data");

            migrationBuilder.DropTable(
                name: "AdminRoles",
                schema: "data");

            migrationBuilder.DropTable(
                name: "Admins",
                schema: "data");

            migrationBuilder.DropTable(
                name: "BusinessUserRoles",
                schema: "data");

            migrationBuilder.DropTable(
                name: "BusinessUsers",
                schema: "data");

            migrationBuilder.DropTable(
                name: "CustomerRoles",
                schema: "data");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "data");

            migrationBuilder.DropTable(
                name: "Property",
                schema: "data");

            migrationBuilder.DropTable(
                name: "DomainKey",
                schema: "data");
        }
    }
}
