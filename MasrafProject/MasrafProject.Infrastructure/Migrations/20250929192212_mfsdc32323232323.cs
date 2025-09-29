using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasrafProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mfsdc32323232323 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "approvalStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Onay = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approvalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCenterCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasrafMerkeziKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasrafMerkeziAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCenterCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasrafId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HizmetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasrafMerkeziId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManagerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Miktar = table.Column<double>(type: "float", nullable: false),
                    BirimFiyat = table.Column<double>(type: "float", nullable: false),
                    Tutar = table.Column<double>(type: "float", nullable: false),
                    KdvOran = table.Column<double>(type: "float", nullable: false),
                    SatirAciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YoneticiOnay = table.Column<int>(type: "int", nullable: false),
                    YoneticiTutar = table.Column<double>(type: "float", nullable: false),
                    YoneticiAciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MuhasebeOnay = table.Column<int>(type: "int", nullable: false),
                    MuhasebeTutar = table.Column<double>(type: "float", nullable: false),
                    MuhasebeAciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoAktarim = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasrafNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BelgeNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToplamTutar = table.Column<double>(type: "float", nullable: false),
                    ToplamKdvTutar = table.Column<double>(type: "float", nullable: false),
                    GenelToplam = table.Column<double>(type: "float", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MuhasebeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MuhasebeOnayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjeKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjeAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HizmetKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HizmetAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KdvOrani = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCards", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approvalStatuses");

            migrationBuilder.DropTable(
                name: "ExpenseCenterCards");

            migrationBuilder.DropTable(
                name: "ExpenseDetails");

            migrationBuilder.DropTable(
                name: "expenses");

            migrationBuilder.DropTable(
                name: "ProjectCards");

            migrationBuilder.DropTable(
                name: "ServiceCards");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");
        }
    }
}
