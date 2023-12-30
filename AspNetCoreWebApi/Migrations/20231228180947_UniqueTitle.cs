using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect.Migrations
{
    /// <inheritdoc />
    public partial class UniqueTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articol_AspNetUsers_UtilizatorId",
                table: "Articol");

            migrationBuilder.AlterColumn<string>(
                name: "UtilizatorId",
                table: "Articol",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titlu",
                table: "Articol",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Articol_Titlu",
                table: "Articol",
                column: "Titlu",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articol_AspNetUsers_UtilizatorId",
                table: "Articol",
                column: "UtilizatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articol_AspNetUsers_UtilizatorId",
                table: "Articol");

            migrationBuilder.DropIndex(
                name: "IX_Articol_Titlu",
                table: "Articol");

            migrationBuilder.AlterColumn<string>(
                name: "UtilizatorId",
                table: "Articol",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Titlu",
                table: "Articol",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Articol_AspNetUsers_UtilizatorId",
                table: "Articol",
                column: "UtilizatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
