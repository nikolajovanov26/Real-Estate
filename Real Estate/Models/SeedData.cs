using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Real_Estate.Areas.Identity.Data;
using Real_Estate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_Project.Models
{
    public class SeedData
    {

        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<Real_EstateUser>>();
            IdentityResult roleResult;

            //Add admin role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }

            var SroleCheck = await RoleManager.RoleExistsAsync("Student");
            if (!SroleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Korisnik")); }

            var TroleCheck = await RoleManager.RoleExistsAsync("Teacher");
            if (!TroleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Agencija")); }

            Real_EstateUser user = await UserManager.FindByEmailAsync("admin@feit.com");
            if (user == null)
            {
                var User = new Real_EstateUser();
                User.Email = "admin@feit.com";
                User.UserName = "admin@feit.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //add default user to role admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }
            Real_EstateUser Suser = await UserManager.FindByEmailAsync("korisnik@feit.com");
            if (Suser == null)
            {
                var User = new Real_EstateUser();
                User.Email = "korisnik@feit.com";
                User.UserName = "korisnik@feit.com";
                string userPWD = "Korisnik123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //add default user to role admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Korisnik"); }
            }
            Real_EstateUser Tuser = await UserManager.FindByEmailAsync("agencija@feit.com");
            if (Tuser == null)
            {
                var User = new Real_EstateUser();
                User.Email = "agencija@feit.com";
                User.UserName = "agencija@feit.com";
                string userPWD = "Agencija123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //add default user to role admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Agencija"); }
            }

        }



        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Real_EstateContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<Real_EstateContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();

                if (context.Korisnik.Any() || context.Agencija.Any() || context.Nedviznosti.Any() || context.Omileni.Any())
                {
                    return; // DB has been seeded
                }


                context.Korisnik.AddRange(
                    new Korisnik { Ime = "Nikola", Prezime = "Jovanov", Email = "nikolajovanov@feit.com", Password = "password" },
                    new Korisnik { Ime = "Kostadin", Prezime = "Kostov", Email = "kostadinkostov@feit.com", Password = "password" },
                    new Korisnik { Ime = "Darko", Prezime = "Tasevski", Email = "darkotasevski@feit.com", Password = "password" },
                    new Korisnik { Ime = "Igor", Prezime = "Lazarov", Email = "igorlazarov@feit.com", Password = "password" },
                    new Korisnik { Ime = "Martin", Prezime = "Stoimenov", Email = "martinstoimenov@feit.com", Password = "password" },
                    new Korisnik { Ime = "Petar", Prezime = "Angelov", Email = "petarangelov@feit.com", Password = "password" }
                );
                context.SaveChanges();

                context.Agencija.AddRange(
                    new Agencija { Ime = "Impuls", Email = "sales@impils.com", Password = "impuls123", DatumOsnovanje = DateTime.Parse("2000-5-2"), Provizija = 40, Prodadeni = 10 },
                    new Agencija { Ime = "Novel", Email = "sales@novel.com", Password = "novel123", DatumOsnovanje = DateTime.Parse("2004-5-2"), Prodadeni = 10 },
                    new Agencija { Ime = "Darma", Email = "sales@darma.com", Password = "darma123", DatumOsnovanje = DateTime.Parse("2008-5-2"), Provizija = 10, Prodadeni = 20 }
                );
                context.SaveChanges();

                context.Nedviznosti.AddRange(
                    new Nedviznosti { Ime = "Kuka vo Veles", Lokacija = "Veles", Golemina = 100, Cena = 100000, Kategorija = "Kuka", Status = "Se prodava", BrojOmileni = 2, AgencijaId = 1 },
                    new Nedviznosti { Ime = "Hotel Romantik", Lokacija = "Veles", Golemina = 3000, Cena = 5000000, Kategorija = "Hotel", Status = "Se prodava", BrojOmileni = 3, KorisnikId = 4 },
                    new Nedviznosti { Ime = "Plac vo Veles", Lokacija = "Veles", Golemina = 2000, Cena = 50000, Kategorija = "Plac", Status = "Se prodava", BrojOmileni = 1, KorisnikId = 2 },
                    new Nedviznosti { Ime = "Kuka vo Veles", Lokacija = "Veles", Golemina = 50, Cena = 250, Kategorija = "Kuka", Status = "Se izdava", BrojOmileni = 0, KorisnikId = 1 },
                    new Nedviznosti { Ime = "Stan vo Skopje", Lokacija = "Skopje", Golemina = 70, Cena = 350, Kategorija = "Stan", Status = "Se izdava", BrojOmileni = 0, AgencijaId = 1 },
                    new Nedviznosti { Ime = "Kuka vo Stip", Lokacija = "Stip", Golemina = 120, Cena = 60000, Kategorija = "Kuka", Status = "Se prodava", BrojOmileni = 1, AgencijaId = 2 },
                    new Nedviznosti { Ime = "Stan vo Ohrid", Lokacija = "Ohrid", Golemina = 50, Cena = 200, Kategorija = "Stan", Status = "Se izdava", BrojOmileni = 0, AgencijaId = 3 }

                );
                context.SaveChanges();

                context.Omileni.AddRange(
                    new Omileni { KorisnikId = 1, NedviznostiId = 2 },
                    new Omileni { KorisnikId = 1, NedviznostiId = 2 },
                    new Omileni { KorisnikId = 1, NedviznostiId = 3 },
                    new Omileni { KorisnikId = 3, NedviznostiId = 2 },
                    new Omileni { KorisnikId = 4, NedviznostiId = 2 },
                    new Omileni { KorisnikId = 5, NedviznostiId = 3 },
                    new Omileni { KorisnikId = 5, NedviznostiId = 6 }
                );
                context.SaveChanges();
            }
        }
    }
}