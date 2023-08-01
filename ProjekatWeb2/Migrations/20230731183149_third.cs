using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjekatWeb2.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CijenaDostave",
                table: "Korisnici",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CijenaDostave",
                table: "Korisnici");
        }
    }
}
