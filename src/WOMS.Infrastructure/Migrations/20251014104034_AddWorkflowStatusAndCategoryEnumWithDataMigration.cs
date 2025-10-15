using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkflowStatusAndCategoryEnumWithDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowProgress_WorkflowStatus_WorkflowStatusId",
                table: "WorkflowProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowStatus_Workflow_WorkflowId",
                table: "WorkflowStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrder_WorkflowStatus_WorkflowStatusId",
                table: "WorkOrder");

            migrationBuilder.DropTable(
                name: "WorkflowTransition");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrder_WorkflowStatusId",
                table: "WorkOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowProgress_WorkflowStatusId",
                table: "WorkflowProgress");

            migrationBuilder.DropColumn(
                name: "WorkflowStatusId",
                table: "WorkOrder");

            migrationBuilder.DropColumn(
                name: "IsFinal",
                table: "WorkflowStatus");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "WorkflowStatus");

            migrationBuilder.DropColumn(
                name: "WorkflowStatusId",
                table: "WorkflowProgress");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "WorkflowStatus",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "IsInitial",
                table: "WorkflowStatus",
                newName: "IsActive");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkflowId",
                table: "WorkflowStatus",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WorkflowStatus",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            // Drop the index on Category before modifying the column
            migrationBuilder.DropIndex(
                name: "IX_Workflow_Category",
                table: "Workflow");

            // Add a temporary column for the enum conversion
            migrationBuilder.AddColumn<int>(
                name: "CategoryTemp",
                table: "Workflow",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Convert existing string categories to enum values
            migrationBuilder.Sql(@"
                UPDATE [Workflow] 
                SET [CategoryTemp] = CASE 
                    WHEN [Category] = 'General' THEN 0
                    WHEN [Category] = 'Maintenance' THEN 1
                    WHEN [Category] = 'Safety' THEN 2
                    WHEN [Category] = 'Compliance' THEN 3
                    ELSE 0
                END");

            // Drop the old column and rename the temp column
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Workflow");

            migrationBuilder.RenameColumn(
                name: "CategoryTemp",
                table: "Workflow",
                newName: "Category");

            // Recreate the index on the new enum column
            migrationBuilder.CreateIndex(
                name: "IX_Workflow_Category",
                table: "Workflow",
                column: "Category");

            migrationBuilder.CreateTable(
                name: "WorkflowStatusTransition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_WorkflowStatusTransition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowStatusTransition_WorkflowStatus_FromStatusId",
                        column: x => x.FromStatusId,
                        principalTable: "WorkflowStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowStatusTransition_WorkflowStatus_ToStatusId",
                        column: x => x.ToStatusId,
                        principalTable: "WorkflowStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStatus_IsActive",
                table: "WorkflowStatus",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStatus_Name",
                table: "WorkflowStatus",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStatus_Order",
                table: "WorkflowStatus",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStatusTransition_FromStatusId",
                table: "WorkflowStatusTransition",
                column: "FromStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStatusTransition_ToStatusId",
                table: "WorkflowStatusTransition",
                column: "ToStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowStatus_Workflow_WorkflowId",
                table: "WorkflowStatus",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowStatus_Workflow_WorkflowId",
                table: "WorkflowStatus");

            migrationBuilder.DropTable(
                name: "WorkflowStatusTransition");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowStatus_IsActive",
                table: "WorkflowStatus");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowStatus_Name",
                table: "WorkflowStatus");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowStatus_Order",
                table: "WorkflowStatus");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "WorkflowStatus",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "WorkflowStatus",
                newName: "IsInitial");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkflowStatusId",
                table: "WorkOrder",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkflowId",
                table: "WorkflowStatus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WorkflowStatus",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinal",
                table: "WorkflowStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "WorkflowStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkflowStatusId",
                table: "WorkflowProgress",
                type: "uniqueidentifier",
                nullable: true);

            // Add a temporary string column for the reverse conversion
            migrationBuilder.AddColumn<string>(
                name: "CategoryTemp",
                table: "Workflow",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            // Convert enum values back to strings
            migrationBuilder.Sql(@"
                UPDATE [Workflow] 
                SET [CategoryTemp] = CASE 
                    WHEN [Category] = 0 THEN 'General'
                    WHEN [Category] = 1 THEN 'Maintenance'
                    WHEN [Category] = 2 THEN 'Safety'
                    WHEN [Category] = 3 THEN 'Compliance'
                    ELSE 'General'
                END");

            // Drop the enum column and rename the temp column
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Workflow");

            migrationBuilder.RenameColumn(
                name: "CategoryTemp",
                table: "Workflow",
                newName: "Category");

            migrationBuilder.CreateTable(
                name: "WorkflowTransition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkflowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RequiresValidation = table.Column<bool>(type: "bit", nullable: false),
                    TransitionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowTransition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowTransition_WorkflowStatus_FromStatusId",
                        column: x => x.FromStatusId,
                        principalTable: "WorkflowStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowTransition_WorkflowStatus_ToStatusId",
                        column: x => x.ToStatusId,
                        principalTable: "WorkflowStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowTransition_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_WorkflowStatusId",
                table: "WorkOrder",
                column: "WorkflowStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowProgress_WorkflowStatusId",
                table: "WorkflowProgress",
                column: "WorkflowStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowTransition_FromStatusId",
                table: "WorkflowTransition",
                column: "FromStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowTransition_ToStatusId",
                table: "WorkflowTransition",
                column: "ToStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowTransition_WorkflowId",
                table: "WorkflowTransition",
                column: "WorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowProgress_WorkflowStatus_WorkflowStatusId",
                table: "WorkflowProgress",
                column: "WorkflowStatusId",
                principalTable: "WorkflowStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowStatus_Workflow_WorkflowId",
                table: "WorkflowStatus",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrder_WorkflowStatus_WorkflowStatusId",
                table: "WorkOrder",
                column: "WorkflowStatusId",
                principalTable: "WorkflowStatus",
                principalColumn: "Id");
        }
    }
}
