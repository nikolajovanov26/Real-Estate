using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_Project.Models
{
    public class Korisnik
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Ime { get; set; }

        [Required]
        [StringLength(20)]
        public string Prezime { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string Password { get; set; }

        public ICollection<Nedviznosti> Nedviznosti { get; set; }

        public ICollection<Omileni> Omileni { get; set; }

        [Display(Name = "Korisnik")]
        public string ImePrezime
        {
            get { return String.Format("{0} {1}", Ime, Prezime); }
        }


    }
}