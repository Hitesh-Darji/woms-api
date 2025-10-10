using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkOrderIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_CreatedOn",
                table: "WorkOrder",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_IsDeleted",
                table: "WorkOrder",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_IsDeleted_AssignedTechnicianId",
                table: "WorkOrder",
                columns: new[] { "IsDeleted", "AssignedTechnicianId" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_IsDeleted_CreatedOn",
                table: "WorkOrder",
                columns: new[] { "IsDeleted", "CreatedOn" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_IsDeleted_Priority",
                table: "WorkOrder",
                columns: new[] { "IsDeleted", "Priority" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_IsDeleted_Priority_CreatedOn",
                table: "WorkOrder",
                columns: new[] { "IsDeleted", "Priority", "CreatedOn" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_IsDeleted_ScheduledDate",
                table: "WorkOrder",
                columns: new[] { "IsDeleted", "ScheduledDate" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_IsDeleted_Status",
                table: "WorkOrder",
                columns: new[] { "IsDeleted", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_IsDeleted_Status_CreatedOn",
                table: "WorkOrder",
                columns: new[] { "IsDeleted", "Status", "CreatedOn" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_IsDeleted_WorkOrderTypeId",
                table: "WorkOrder",
                columns: new[] { "IsDeleted", "WorkOrderTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_ScheduledDate",
                table: "WorkOrder",
                column: "ScheduledDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_CreatedOn",
                table: "WorkOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_IsDeleted",
                table: "WorkOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_IsDeleted_AssignedTechnicianId",
                table: "WorkOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_IsDeleted_CreatedOn",
                table: "WorkOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_IsDeleted_Priority",
                table: "WorkOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_IsDeleted_Priority_CreatedOn",
                table: "WorkOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_IsDeleted_ScheduledDate",
                table: "WorkOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_IsDeleted_Status",
                table: "WorkOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_IsDeleted_Status_CreatedOn",
                table: "WorkOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_IsDeleted_WorkOrderTypeId",
                table: "WorkOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_ScheduledDate",
                table: "WorkOrder");
        }
    }
}
