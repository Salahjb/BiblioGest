using BiblioGest.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BiblioGest.Models
{
    public class BiblioDbContext : DbContext
    {
        // Default constructor for runtime
        public BiblioDbContext() { }

        // Constructor for use with design-time tools (like migrations)
        public BiblioDbContext(DbContextOptions<BiblioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Livre> Livres { get; set; } = null!;
        public DbSet<Adherent> Adherents { get; set; } = null!;
        public DbSet<Emprunt> Emprunts { get; set; } = null!;
        public DbSet<Categorie> Categories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "server=localhost;port=3306;database=biblioguest;user=root";
                optionsBuilder.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship between Categorie and Livre
            modelBuilder.Entity<Livre>()
                .HasOne(l => l.Categorie)
                .WithMany(c => c.Livres)
                .HasForeignKey(l => l.CategorieId);

            // Configure one-to-many relationship between Adherent and Emprunt
            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Adherent)
                .WithMany(a => a.Emprunts)
                .HasForeignKey(e => e.AdherentId);

            // Configure one-to-many relationship between Livre and Emprunt
            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Livre)
                .WithMany(l => l.Emprunts)
                .HasForeignKey(e => e.LivreISBN);

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Categorie>().HasData(
                new Categorie { Id = 1, Nom = "Roman", Description = "Œuvres de fiction en prose" },
                new Categorie { Id = 2, Nom = "Science-Fiction", Description = "Fiction basée sur des avancées scientifiques" },
                new Categorie { Id = 3, Nom = "Biographie", Description = "Récit de vie d'une personne" },
                new Categorie { Id = 4, Nom = "Informatique", Description = "Livres techniques sur l'informatique" }
            );

            // Seed some books
            modelBuilder.Entity<Livre>().HasData(
                new Livre
                {
                    ISBN = "9782070368228",
                    Titre = "L'Étranger",
                    Auteur = "Albert Camus",
                    Editeur = "Gallimard",
                    AnneePublication = 1942,
                    CategorieId = 1,
                    NombreExemplaires = 5
                },
                new Livre
                {
                    ISBN = "9782266252584",
                    Titre = "Fondation",
                    Auteur = "Isaac Asimov",
                    Editeur = "Pocket",
                    AnneePublication = 1951,
                    CategorieId = 2,
                    NombreExemplaires = 3
                }
            );
        }
    }
}
