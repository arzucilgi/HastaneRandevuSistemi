using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjemDotnet.Migrations
{
    /// <inheritdoc />
    public partial class tablolarGuncellendiiii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hastaneler_Hastaneler_HastaneId",
                table: "Hastaneler");

            migrationBuilder.DropIndex(
                name: "IX_Hastaneler_HastaneId",
                table: "Hastaneler");

            migrationBuilder.DropColumn(
                name: "HastaneId",
                table: "Hastaneler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HastaneId",
                table: "Hastaneler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hastaneler_HastaneId",
                table: "Hastaneler",
                column: "HastaneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hastaneler_Hastaneler_HastaneId",
                table: "Hastaneler",
                column: "HastaneId",
                principalTable: "Hastaneler",
                principalColumn: "Id");
        }
    }
}
