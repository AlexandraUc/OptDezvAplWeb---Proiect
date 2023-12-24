using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilizator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titlu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Continut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilizatorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articol_Utilizator_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "Utilizator",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Profil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilizatorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profil", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profil_Utilizator_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "Utilizator",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArticolProfil",
                columns: table => new
                {
                    ArticoleId = table.Column<int>(type: "int", nullable: false),
                    ProfiluriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticolProfil", x => new { x.ArticoleId, x.ProfiluriId });
                    table.ForeignKey(
                        name: "FK_ArticolProfil_Articol_ArticoleId",
                        column: x => x.ArticoleId,
                        principalTable: "Articol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticolProfil_Profil_ProfiluriId",
                        column: x => x.ProfiluriId,
                        principalTable: "Profil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articol_UtilizatorId",
                table: "Articol",
                column: "UtilizatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticolProfil_ProfiluriId",
                table: "ArticolProfil",
                column: "ProfiluriId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_UtilizatorId",
                table: "Profil",
                column: "UtilizatorId",
                unique: true,
                filter: "[UtilizatorId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticolProfil");

            migrationBuilder.DropTable(
                name: "Articol");

            migrationBuilder.DropTable(
                name: "Profil");

            migrationBuilder.DropTable(
                name: "Utilizator");
        }
    }
}
