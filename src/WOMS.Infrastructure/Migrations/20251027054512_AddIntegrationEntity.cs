using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIntegrationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkOrderView");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkOrderColumn");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkOrderAssignment");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkOrder");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowVersion");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowTemplate");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowStatusTransition");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowStatus");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowProgress");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowNotification");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowNode");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowInstance");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowExecutionLog");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowEscalation");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowCondition");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowApproval");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WorkflowAction");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Views");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ViewFilter");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ViewColumn");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ValidationRule");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TieredRate");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "StockTransaction");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "StockRequest");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "SerializedAsset");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "RequestItem");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "RateTable");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "KitItem");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "JobKit");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "InvoiceLineItem");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "FormTemplate");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "FormSubmission");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "FormSignature");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "FormSection");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "FormGeolocation");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "FormField");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "FormAttachment");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DynamicField");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DeliverySetting");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CycleCount");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CountItem");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ConditionalRate");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "BillingTemplate");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "BillingSchedule");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "AssetHistory");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ApprovalRecord");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "AggregationRule");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Address");

            migrationBuilder.CreateTable(
                name: "Integration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IconName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Configuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ConnectedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSyncOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SyncStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_Integration", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Integration");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkOrderView",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkOrderColumn",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkOrderAssignment",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkOrder",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowVersion",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowTemplate",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowStatusTransition",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowStatus",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowProgress",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowNotification",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowNode",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowInstance",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowExecutionLog",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowEscalation",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowCondition",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowApproval",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WorkflowAction",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Workflow",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Views",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ViewFilter",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ViewColumn",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ValidationRule",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TieredRate",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "StockTransaction",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "StockRequest",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Stock",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "SerializedAsset",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "RequestItem",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "RateTable",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Location",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "KitItem",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "JobKit",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "InvoiceLineItem",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Invoice",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "InventoryItem",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "FormTemplate",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "FormSubmission",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "FormSignature",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "FormSection",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "FormGeolocation",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "FormField",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "FormAttachment",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DynamicField",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Department",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DeliverySetting",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CycleCount",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Customer",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CountItem",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Contact",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ConditionalRate",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "BillingTemplate",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "BillingSchedule",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "AssetHistory",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ApprovalRecord",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "AggregationRule",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Address",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }
    }
}
