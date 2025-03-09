using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjemDotnet.Migrations
{
    /// <inheritdoc />
    public partial class TablolarOlusturdu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doktorlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoktorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoktorSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoliklinikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doktorlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cinsiyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BabaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumYeri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sehirler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SehirName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehirler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hastaneler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HastaneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SehirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hastaneler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hastaneler_Sehirler_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poliklinikler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoliklinikName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HastaneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliklinikler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poliklinikler_Hastaneler_HastaneId",
                        column: x => x.HastaneId,
                        principalTable: "Hastaneler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoktorPoliklinik",
                columns: table => new
                {
                    DoktorlarId = table.Column<int>(type: "int", nullable: false),
                    PolikliniklerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoktorPoliklinik", x => new { x.DoktorlarId, x.PolikliniklerId });
                    table.ForeignKey(
                        name: "FK_DoktorPoliklinik_Doktorlar_DoktorlarId",
                        column: x => x.DoktorlarId,
                        principalTable: "Doktorlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoktorPoliklinik_Poliklinikler_PolikliniklerId",
                        column: x => x.PolikliniklerId,
                        principalTable: "Poliklinikler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoktorPoliklinik_PolikliniklerId",
                table: "DoktorPoliklinik",
                column: "PolikliniklerId");

            migrationBuilder.CreateIndex(
                name: "IX_Hastaneler_SehirId",
                table: "Hastaneler",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Poliklinikler_HastaneId",
                table: "Poliklinikler",
                column: "HastaneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoktorPoliklinik");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Doktorlar");

            migrationBuilder.DropTable(
                name: "Poliklinikler");

            migrationBuilder.DropTable(
                name: "Hastaneler");

            migrationBuilder.DropTable(
                name: "Sehirler");
        }
    }
}
