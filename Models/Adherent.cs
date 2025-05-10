using BiblioGest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiblioGest.Models
{
    public class Adherent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Prenom { get; set; } = null!;

        [MaxLength(200)]
        public string Adresse { get; set; } = null!;

        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Phone]
        [MaxLength(15)]
        public string Telephone { get; set; } = null!;

        public DateTime DateInscription { get; set; }

        public bool EstActif { get; set; } = true;

        // Navigation property
        public ICollection<Emprunt> Emprunts { get; set; } = new List<Emprunt>();
    }
}