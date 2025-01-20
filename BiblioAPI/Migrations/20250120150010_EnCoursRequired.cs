using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioAPI.Migrations
{
    /// <inheritdoc />
    public partial class EnCoursRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EnCours",
                table: "Emprunt",
                type: "INTEGER",
                nullable: false,
                defaultValue: false
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "EnCours", table: "Emprunt");
        }
    }
}
