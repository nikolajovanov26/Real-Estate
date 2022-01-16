using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_Project.Models
{
    public class Omileni
    {
        public int Id { get; set; }

        public int? KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }


        public int? NedviznostiId { get; set; }
        public Nedviznosti Nedviznosti { get; set; }
    }
}