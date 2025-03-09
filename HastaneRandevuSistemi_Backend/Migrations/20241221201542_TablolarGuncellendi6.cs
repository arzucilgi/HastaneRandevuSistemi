using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjemDotnet.Migrations
{
    /// <inheritdoc />
    public partial class TablolarGuncellendi6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SehirId",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_SehirId",
                table: "Randevular",
                column: "SehirId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Sehirler_SehirId",
                table: "Randevular",
                column: "SehirId",
                principalTable: "Sehirler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Sehirler_SehirId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_SehirId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "SehirId",
                table: "Randevular");
        }
    }
}
