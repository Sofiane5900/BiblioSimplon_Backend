using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjoutDisponibilite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstDisponible",
                table: "Livre",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstDisponible",
                table: "Livre");
        }
    }
}
