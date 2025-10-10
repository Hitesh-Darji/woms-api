using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBillingScheduleEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    DayOfMonth = table.Column<int>(type: "int", nullable: true),
                    DayOfWeek = table.Column<int>(type: "int", nullable: true),
                    DayOfWeekName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastRunDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextRunDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RunCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastRunStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastRunMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ScheduleSettings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationSettings = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_BillingSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillingScheduleRuns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RunDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledRunDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurationSeconds = table.Column<int>(type: "int", nullable: true),
                    TemplatesProcessed = table.Column<int>(type: "int", nullable: false),
                    TemplatesSuccessful = table.Column<int>(type: "int", nullable: false),
                    TemplatesFailed = table.Column<int>(type: "int", nullable: false),
                    InvoicesGenerated = table.Column<int>(type: "int", nullable: false),
                    EmailsSent = table.Column<int>(type: "int", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    RunDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_BillingScheduleRuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingScheduleRuns_BillingSchedules_BillingScheduleId",
                        column: x => x.BillingScheduleId,
                        principalTable: "BillingSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillingScheduleTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TemplateSettings = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_BillingScheduleTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingScheduleTemplates_BillingSchedules_BillingScheduleId",
                        column: x => x.BillingScheduleId,
                        principalTable: "BillingSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillingScheduleTemplates_BillingTemplates_BillingTemplateId",
                        column: x => x.BillingTemplateId,
                        principalTable: "BillingTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingScheduleRuns_BillingScheduleId",
                table: "BillingScheduleRuns",
                column: "BillingScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingScheduleRuns_RunDate",
                table: "BillingScheduleRuns",
                column: "RunDate");

            migrationBuilder.CreateIndex(
                name: "IX_BillingScheduleRuns_ScheduledRunDate",
                table: "BillingScheduleRuns",
                column: "ScheduledRunDate");

            migrationBuilder.CreateIndex(
                name: "IX_BillingScheduleRuns_Status",
                table: "BillingScheduleRuns",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_BillingSchedules_Frequency",
                table: "BillingSchedules",
                column: "Frequency");

            migrationBuilder.CreateIndex(
                name: "IX_BillingSchedules_IsActive",
                table: "BillingSchedules",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_BillingSchedules_NextRunDate",
                table: "BillingSchedules",
                column: "NextRunDate");

            migrationBuilder.CreateIndex(
                name: "IX_BillingSchedules_ScheduleName",
                table: "BillingSchedules",
                column: "ScheduleName");

            migrationBuilder.CreateIndex(
                name: "IX_BillingSchedules_Status",
                table: "BillingSchedules",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_BillingScheduleTemplates_BillingScheduleId_DisplayOrder",
                table: "BillingScheduleTemplates",
                columns: new[] { "BillingScheduleId", "DisplayOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_BillingScheduleTemplates_BillingTemplateId",
                table: "BillingScheduleTemplates",
                column: "BillingTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingScheduleTemplates_IsActive",
                table: "BillingScheduleTemplates",
                column: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingScheduleRuns");

            migrationBuilder.DropTable(
                name: "BillingScheduleTemplates");

            migrationBuilder.DropTable(
                name: "BillingSchedules");
        }
    }
}
