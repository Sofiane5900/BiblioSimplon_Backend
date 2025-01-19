using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiblioAPI.Migrations
{
    /// <inheritdoc />
    public partial class Employer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employe",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employe", x => x.Id);
                }
            );

            migrationBuilder.InsertData(
                table: "Employe",
                columns: new[] { "Id", "Email", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "admin@biblio.com", "admin123", "Admin", "admin" },
                    { 2, "biblio@tecaire.com", "biblio123", "Bibliothecaire", "biblio" },
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Livre_ISBN",
                table: "Livre",
                column: "ISBN",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Employe_Email",
                table: "Employe",
                column: "Email",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Employe");

            migrationBuilder.DropIndex(name: "IX_Livre_ISBN", table: "Livre");
        }
    }
}
