using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Real_Estate.Areas.Identity.Data;
using Real_Estate_Project.Models;

namespace Real_Estate.Models
{
    public class Real_EstateContext : IdentityDbContext<Real_EstateUser>
    {
        public Real_EstateContext (DbContextOptions<Real_EstateContext> options)
            : base(options)
        {
        }

        public DbSet<Korisnik> Korisnik { get; set; }

        public DbSet<Agencija> Agencija { get; set; }

        public DbSet<Nedviznosti> Nedviznosti { get; set; }

        public DbSet<Omileni> Omileni { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Omileni>()
                .HasOne<Korisnik>(p => p.Korisnik)
                .WithMany(p => p.Omileni)
                .HasForeignKey(p => p.KorisnikId);

            builder.Entity<Omileni>()
                .HasOne<Nedviznosti>(p => p.Nedviznosti)
                .WithMany(p => p.Omilen)
                .HasForeignKey(p => p.NedviznostiId);

            builder.Entity<Nedviznosti>()
                .HasOne<Korisnik>(p => p.Sopstvenik)
                .WithMany(p => p.Nedviznosti)
                .HasForeignKey(p => p.KorisnikId);

            builder.Entity<Nedviznosti>()
                .HasOne<Agencija>(p => p.Agencija)
                .WithMany(p => p.Nedviznosti)
                .HasForeignKey(p => p.AgencijaId);

            base.OnModelCreating(builder);

        }
    }
}
