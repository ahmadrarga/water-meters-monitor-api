using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterMetersMonitor.Infrastructure.Migrations
{
    public partial class ChangeGroupMainRelatioship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MainWaterMeters_GroupId",
                table: "MainWaterMeters");

            migrationBuilder.CreateIndex(
                name: "IX_MainWaterMeters_GroupId",
                table: "MainWaterMeters",
                column: "GroupId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MainWaterMeters_GroupId",
                table: "MainWaterMeters");

            migrationBuilder.CreateIndex(
                name: "IX_MainWaterMeters_GroupId",
                table: "MainWaterMeters",
                column: "GroupId");
        }
    }
}
