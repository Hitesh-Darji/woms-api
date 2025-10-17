using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRouteOptimization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptimizationSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrioritizeDistanceReduction = table.Column<bool>(type: "bit", nullable: false),
                    RespectTimeWindows = table.Column<bool>(type: "bit", nullable: false),
                    BalanceWorkload = table.Column<bool>(type: "bit", nullable: false),
                    ConsiderTrafficPatterns = table.Column<bool>(type: "bit", nullable: false),
                    MaxRouteTimeHours = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptimizationSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptimizationSettings");
        }
    }
}
