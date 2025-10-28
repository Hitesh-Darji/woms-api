using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkflowNotificationTypeToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Convert existing string values to enum values
            migrationBuilder.Sql(@"
                UPDATE WorkflowNotification 
                SET Type = CASE 
                    WHEN Type = 'email' THEN 0
                    WHEN Type = 'sms' THEN 1
                    WHEN Type = 'in_app' THEN 2
                    WHEN Type = 'webhook' THEN 3
                    ELSE 0
                END
                WHERE Type IS NOT NULL
            ");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "WorkflowNotification",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "WorkflowNotification",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // Convert enum values back to string
            migrationBuilder.Sql(@"
                UPDATE WorkflowNotification 
                SET Type = CASE 
                    WHEN Type = '0' THEN 'email'
                    WHEN Type = '1' THEN 'sms'
                    WHEN Type = '2' THEN 'in_app'
                    WHEN Type = '3' THEN 'webhook'
                    ELSE 'email'
                END
                WHERE Type IS NOT NULL
            ");
        }
    }
}
