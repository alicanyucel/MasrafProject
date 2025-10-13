using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasrafProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg23232323323w232323 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "AppUsers");
        }
    }
}
