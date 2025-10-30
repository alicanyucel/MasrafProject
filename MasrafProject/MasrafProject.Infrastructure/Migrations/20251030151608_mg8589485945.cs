using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasrafProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg8589485945 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BorcTutar",
                table: "ExpenseDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorcTutar",
                table: "ExpenseDetails");
        }
    }
}
