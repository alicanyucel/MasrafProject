using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasrafProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mgjrfjg3u4938934893348394 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ServiceCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ProjectCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ExpenseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ExpenseCenterCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ApprovalStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ServiceCards");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ProjectCards");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ExpenseDetails");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ExpenseCenterCards");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ApprovalStatuses");
        }
    }
}
