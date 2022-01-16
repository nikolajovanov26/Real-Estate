using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Real_Estate_Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate.ViewModels
{
    public class NedviznostiVM
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public SelectList Gradovi { get; set; }
        public string Grad { get; set; }
        public int Golemina { get; set; }
        public int Cena { get; set; }
        public string Status { get; set; }
        public SelectList Statusi { get; set; }
        public string Kategorija { get; set; }
        public int? KorisnikId { get; set; }
        public Korisnik Sopstvenik { get; set; }
        public int? AgencijaId { get; set; }
        public Agencija Agencija { get; set; }
        public ICollection<Omileni> Omilen { get; set; }
        public int? BrojOmileni { get; set; }
        public IFormFile MainImage { get; set; }
        public List<Nedviznosti> Nedviznosti { get; set; }
        public string Mimage { get; set; }
        public string SearchString { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}