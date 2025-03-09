using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjemDotnet.Migrations
{
    /// <inheritdoc />
    public partial class TablolarGuncellendi5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Kullanicilar",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Kullanicilar");
        }
    }
}
