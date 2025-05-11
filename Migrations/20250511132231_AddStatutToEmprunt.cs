using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioGest.Migrations
{
    /// <inheritdoc />
    public partial class AddStatutToEmprunt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Statut",
                table: "Emprunts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Emprunts");
        }
    }
}
