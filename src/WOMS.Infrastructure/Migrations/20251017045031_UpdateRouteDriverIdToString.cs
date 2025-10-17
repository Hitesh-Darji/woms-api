using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRouteDriverIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RouteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDistance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalStops = table.Column<int>(type: "int", nullable: false),
                    Efficiency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Constraints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptimizationSettings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispatchedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Route_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteStop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    EstimatedDuration = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ScheduledStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScheduledEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TravelTime = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Distance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteStop_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteStop_WorkOrder_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Route_DriverId",
                table: "Route",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStop_RouteId",
                table: "RouteStop",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStop_WorkOrderId",
                table: "RouteStop",
                column: "WorkOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteStop");

            migrationBuilder.DropTable(
                name: "Route");
        }
    }
}
