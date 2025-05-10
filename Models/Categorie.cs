using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiblioGest.Models
{
    public class Categorie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; } = null!;

        [MaxLength(200)]
        public string? Description { get; set; }

        // Navigation property
        public ICollection<Livre> Livres { get; set; } = new List<Livre>();
    }
}