using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_Project.Models
{
    public class Agencija
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Ime { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DatumOsnovanje { get; set; }

        public int? Provizija { get; set; }

        public int? Prodadeni { get; set; }

        public ICollection<Nedviznosti> Nedviznosti { get; set; }
    }
}