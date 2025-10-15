using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WorkflowEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkflowId",
                table: "WorkflowStatusTransition",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStatusTransition_WorkflowId",
                table: "WorkflowStatusTransition",
                column: "WorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowStatusTransition_Workflow_WorkflowId",
                table: "WorkflowStatusTransition",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowStatusTransition_Workflow_WorkflowId",
                table: "WorkflowStatusTransition");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowStatusTransition_WorkflowId",
                table: "WorkflowStatusTransition");

            migrationBuilder.DropColumn(
                name: "WorkflowId",
                table: "WorkflowStatusTransition");
        }
    }
}
