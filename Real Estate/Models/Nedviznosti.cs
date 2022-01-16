using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_Project.Models
{
    public class Nedviznosti
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ime { get; set; }

        [AllowNull]
        [StringLength(15)]
        public string Lokacija { get; set; }

        [Required]
        public int Golemina { get; set; }

        [Required]
        public int Cena { get; set; }

        [Required]
        [StringLength(15)]
        public string Status { get; set; }

        [Required]
        [StringLength(15)]
        public string Kategorija { get; set; }

        [AllowNull]
        public int? KorisnikId { get; set; }
        public Korisnik Sopstvenik { get; set; }

        [AllowNull]
        public int? AgencijaId { get; set; }
        public Agencija Agencija { get; set; }

        public ICollection<Omileni> Omilen { get; set; }

        [Display(Name = "♥")]
        public int? BrojOmileni { get; set; }

        [AllowNull]
        public string MainImage { get; set; }
    }
}