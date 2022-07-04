using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterMetersMonitor.Infrastructure.Migrations
{
    public partial class AdditionalColumns1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payment",
                table: "MainWaterMeters");

            migrationBuilder.AddColumn<float>(
                name: "Payment",
                table: "WaterMeterValues",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payment",
                table: "WaterMeterValues");

            migrationBuilder.AddColumn<float>(
                name: "Payment",
                table: "MainWaterMeters",
                type: "real",
                nullable: true);
        }
    }
}
