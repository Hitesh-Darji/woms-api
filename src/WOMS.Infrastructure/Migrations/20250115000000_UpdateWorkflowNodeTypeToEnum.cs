using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkflowNodeTypeToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add a temporary column for the enum conversion
            migrationBuilder.AddColumn<int>(
                name: "TypeTemp",
                table: "WorkflowNode",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Convert existing string types to enum values
            migrationBuilder.Sql(@"
                UPDATE [WorkflowNode] 
                SET [TypeTemp] = CASE 
                    WHEN [Type] = 'start' THEN 0
                    WHEN [Type] = 'status' THEN 1
                    WHEN [Type] = 'condition' THEN 2
                    WHEN [Type] = 'approval' THEN 3
                    WHEN [Type] = 'notification' THEN 4
                    WHEN [Type] = 'escalation' THEN 5
                    WHEN [Type] = 'end' THEN 6
                    ELSE 0
                END");

            // Drop the old column and rename the temp column
            migrationBuilder.DropColumn(
                name: "Type",
                table: "WorkflowNode");

            migrationBuilder.RenameColumn(
                name: "TypeTemp",
                table: "WorkflowNode",
                newName: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add a temporary string column for the reverse conversion
            migrationBuilder.AddColumn<string>(
                name: "TypeTemp",
                table: "WorkflowNode",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            // Convert enum values back to strings
            migrationBuilder.Sql(@"
                UPDATE [WorkflowNode] 
                SET [TypeTemp] = CASE 
                    WHEN [Type] = 0 THEN 'start'
                    WHEN [Type] = 1 THEN 'status'
                    WHEN [Type] = 2 THEN 'condition'
                    WHEN [Type] = 3 THEN 'approval'
                    WHEN [Type] = 4 THEN 'notification'
                    WHEN [Type] = 5 THEN 'escalation'
                    WHEN [Type] = 6 THEN 'end'
                    ELSE 'start'
                END");

            // Drop the enum column and rename the temp column
            migrationBuilder.DropColumn(
                name: "Type",
                table: "WorkflowNode");

            migrationBuilder.RenameColumn(
                name: "TypeTemp",
                table: "WorkflowNode",
                newName: "Type");
        }
    }
}
