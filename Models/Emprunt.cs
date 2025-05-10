using System;
using System.ComponentModel.DataAnnotations;

namespace BiblioGest.Models
{
    public class Emprunt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LivreISBN { get; set; } = null!;
        public Livre? Livre { get; set; }

        public int AdherentId { get; set; }
        public Adherent? Adherent { get; set; }

        public DateTime DateEmprunt { get; set; }

        public DateTime DateRetourPrevue { get; set; }

        public DateTime? DateRetourEffective { get; set; }

        public bool EstEnRetard => DateRetourEffective == null && DateTime.Now > DateRetourPrevue;
    }
}