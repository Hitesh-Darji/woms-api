using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkOrderAssignmentStatusToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First, add a temporary column for the new enum values
            migrationBuilder.AddColumn<int>(
                name: "StatusNew",
                table: "WorkOrderAssignment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Convert existing string values to enum values
            migrationBuilder.Sql(@"
                UPDATE WorkOrderAssignment 
                SET StatusNew = CASE 
                    WHEN Status = 'assigned' THEN 0
                    WHEN Status = 'accepted' THEN 1
                    WHEN Status = 'rejected' THEN 2
                    WHEN Status = 'completed' THEN 3
                    WHEN Status = 'cancelled' THEN 4
                    ELSE 0
                END");

            // Drop the old column
            migrationBuilder.DropColumn(
                name: "Status",
                table: "WorkOrderAssignment");

            // Rename the new column to Status
            migrationBuilder.RenameColumn(
                name: "StatusNew",
                table: "WorkOrderAssignment",
                newName: "Status");

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Category = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianEquipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TechnicianId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_TechnicianEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianEquipment_AspNetUsers_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianEquipment_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderEquipmentRequirement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_WorkOrderEquipmentRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderEquipmentRequirement_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrderEquipmentRequirement_WorkOrder_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianEquipment_EquipmentId",
                table: "TechnicianEquipment",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianEquipment_TechnicianId",
                table: "TechnicianEquipment",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEquipmentRequirement_EquipmentId",
                table: "WorkOrderEquipmentRequirement",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEquipmentRequirement_WorkOrderId",
                table: "WorkOrderEquipmentRequirement",
                column: "WorkOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicianEquipment");

            migrationBuilder.DropTable(
                name: "WorkOrderEquipmentRequirement");

            migrationBuilder.DropTable(
                name: "Equipment");

            // Add temporary string column
            migrationBuilder.AddColumn<string>(
                name: "StatusOld",
                table: "WorkOrderAssignment",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "assigned");

            // Convert enum values back to strings
            migrationBuilder.Sql(@"
                UPDATE WorkOrderAssignment 
                SET StatusOld = CASE 
                    WHEN Status = 0 THEN 'assigned'
                    WHEN Status = 1 THEN 'accepted'
                    WHEN Status = 2 THEN 'rejected'
                    WHEN Status = 3 THEN 'completed'
                    WHEN Status = 4 THEN 'cancelled'
                    ELSE 'assigned'
                END");

            // Drop the enum column
            migrationBuilder.DropColumn(
                name: "Status",
                table: "WorkOrderAssignment");

            // Rename the string column back to Status
            migrationBuilder.RenameColumn(
                name: "StatusOld",
                table: "WorkOrderAssignment",
                newName: "Status");
        }
    }
}
