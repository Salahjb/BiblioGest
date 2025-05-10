using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiblioGest.Models
{
    public class Livre
    {
        [Key]
        public string ISBN { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Titre { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Auteur { get; set; } = null!;

        [MaxLength(100)]
        public string Editeur { get; set; } = null!;

        public int AnneePublication { get; set; }

        public int CategorieId { get; set; }
        public Categorie? Categorie { get; set; }

        public int NombreExemplaires { get; set; }

        // Navigation property
        public ICollection<Emprunt> Emprunts { get; set; } = new List<Emprunt>();
    }
}