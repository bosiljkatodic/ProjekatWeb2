using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjekatWeb2.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementPorudzbine_Porudzbine_PorudzbinaId",
                table: "ElementPorudzbine");

            migrationBuilder.DropIndex(
                name: "IX_ElementPorudzbine_PorudzbinaId",
                table: "ElementPorudzbine");

            migrationBuilder.DropColumn(
                name: "PorudzbinaId",
                table: "ElementPorudzbine");

            migrationBuilder.CreateIndex(
                name: "IX_ElementPorudzbine_IdPorudzbina",
                table: "ElementPorudzbine",
                column: "IdPorudzbina");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementPorudzbine_Porudzbine_IdPorudzbina",
                table: "ElementPorudzbine",
                column: "IdPorudzbina",
                principalTable: "Porudzbine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementPorudzbine_Porudzbine_IdPorudzbina",
                table: "ElementPorudzbine");

            migrationBuilder.DropIndex(
                name: "IX_ElementPorudzbine_IdPorudzbina",
                table: "ElementPorudzbine");

            migrationBuilder.AddColumn<long>(
                name: "PorudzbinaId",
                table: "ElementPorudzbine",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ElementPorudzbine_PorudzbinaId",
                table: "ElementPorudzbine",
                column: "PorudzbinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementPorudzbine_Porudzbine_PorudzbinaId",
                table: "ElementPorudzbine",
                column: "PorudzbinaId",
                principalTable: "Porudzbine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
