using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterMetersMonitor.Infrastructure.Migrations
{
    public partial class AdditionalColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Payment",
                table: "MainWaterMeterValues",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Payment",
                table: "MainWaterMeters",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payment",
                table: "MainWaterMeterValues");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "MainWaterMeters");
        }
    }
}
