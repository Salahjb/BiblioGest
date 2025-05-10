using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiblioGest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Adherents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prenom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Adresse = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telephone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateInscription = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstActif = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adherents", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Livres",
                columns: table => new
                {
                    ISBN = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Titre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Auteur = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Editeur = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnneePublication = table.Column<int>(type: "int", nullable: false),
                    CategorieId = table.Column<int>(type: "int", nullable: false),
                    NombreExemplaires = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livres", x => x.ISBN);
                    table.ForeignKey(
                        name: "FK_Livres_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Emprunts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LivreISBN = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AdherentId = table.Column<int>(type: "int", nullable: false),
                    DateEmprunt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateRetourPrevue = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateRetourEffective = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprunts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emprunts_Adherents_AdherentId",
                        column: x => x.AdherentId,
                        principalTable: "Adherents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emprunts_Livres_LivreISBN",
                        column: x => x.LivreISBN,
                        principalTable: "Livres",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Nom" },
                values: new object[,]
                {
                    { 1, "Œuvres de fiction en prose", "Roman" },
                    { 2, "Fiction basée sur des avancées scientifiques", "Science-Fiction" },
                    { 3, "Récit de vie d'une personne", "Biographie" },
                    { 4, "Livres techniques sur l'informatique", "Informatique" }
                });

            migrationBuilder.InsertData(
                table: "Livres",
                columns: new[] { "ISBN", "AnneePublication", "Auteur", "CategorieId", "Editeur", "NombreExemplaires", "Titre" },
                values: new object[,]
                {
                    { "9782070368228", 1942, "Albert Camus", 1, "Gallimard", 5, "L'Étranger" },
                    { "9782266252584", 1951, "Isaac Asimov", 2, "Pocket", 3, "Fondation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_AdherentId",
                table: "Emprunts",
                column: "AdherentId");

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_LivreISBN",
                table: "Emprunts",
                column: "LivreISBN");

            migrationBuilder.CreateIndex(
                name: "IX_Livres_CategorieId",
                table: "Livres",
                column: "CategorieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emprunts");

            migrationBuilder.DropTable(
                name: "Adherents");

            migrationBuilder.DropTable(
                name: "Livres");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
