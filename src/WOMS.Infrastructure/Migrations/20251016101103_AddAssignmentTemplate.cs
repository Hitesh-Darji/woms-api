using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignmentTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DaysOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredTechnicians = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillsRequired = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutoAssignmentRules = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsageCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LastUsed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentTemplates_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTemplates_CreatedBy",
                table: "AssignmentTemplates",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTemplates_Name",
                table: "AssignmentTemplates",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTemplates_Status",
                table: "AssignmentTemplates",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentTemplates");
        }
    }
}
