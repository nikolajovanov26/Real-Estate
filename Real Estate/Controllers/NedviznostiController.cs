using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate.Models;
using Real_Estate.ViewModels;
using Real_Estate_Project.Models;

namespace Real_Estate.Controllers
{
    public class NedviznostiController : Controller
    {
        private readonly Real_EstateContext _context;
        private readonly IHostingEnvironment webHostEnvironment;

        public NedviznostiController(Real_EstateContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Nedviznosti
        public async Task<IActionResult> Index(string SearchString, string Grad, string Status, int Cena)
        {
            IQueryable<string> grad = from m in _context.Nedviznosti select m.Lokacija;
            IQueryable<string> status = from m in _context.Nedviznosti select m.Status;

            var nedviznosti = from n in _context.Nedviznosti select n;

            switch (Cena)
            {
                case 0:
                    //nedviznosti = nedviznosti.Where(s => s.Ime.Contains(SearchString));
                    break;

                case 1:
                    nedviznosti = nedviznosti.Where(s => s.Cena < 1000);
                    break;

                case 2:
                    nedviznosti = nedviznosti.Where(s => s.Cena > 1000 && s.Cena < 10000);
                    break;

                case 3:
                    nedviznosti = nedviznosti.Where(s => s.Cena > 10000 && s.Cena < 50000);
                    break;

                case 4:
                    nedviznosti = nedviznosti.Where(s => s.Cena > 50000);
                    break;

                default:

                    break;
            }



            if (!string.IsNullOrEmpty(SearchString))
            {
                nedviznosti = nedviznosti.Where(s => s.Ime.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(Grad))
            {
                nedviznosti = nedviznosti.Where(x => x.Lokacija == Grad);
            }
            if (!string.IsNullOrEmpty(Status))
            {
                nedviznosti = nedviznosti.Where(x => x.Status == Status);
            }

            var nedviznostiVM = new NedviznostiVM
            {
                Gradovi = new SelectList(await grad.Distinct().ToListAsync()),
                Statusi = new SelectList(await status.Distinct().ToListAsync()),
                Nedviznosti = await nedviznosti.ToListAsync()
            };


            //var real_EstateContext = _context.Nedviznosti.Include(n => n.Agencija).Include(n => n.Sopstvenik);
            //return View(await real_EstateContext.ToListAsync());
            //return View(await nedviznosti.ToListAsync());
            return View(nedviznostiVM);
        }


        // GET: Nedviznosti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nedviznosti = await _context.Nedviznosti
                .Include(n => n.Agencija)
                .Include(n => n.Sopstvenik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nedviznosti == null)
            {
                return NotFound();
            }

            return View(nedviznosti);
        }

        // GET: Nedviznosti/Create
        public IActionResult Create()
        {
            ViewData["AgencijaId"] = new SelectList(_context.Agencija, "Id", "Email");
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Email");
            return View();
        }

        // POST: Nedviznosti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NedviznostiVM model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Nedviznosti nedviznosti = new Nedviznosti
                {
                    Ime = model.Ime,
                    Lokacija = model.Grad,
                    Golemina = model.Golemina,
                    Cena = model.Cena,
                    Status = model.Status,
                    Kategorija = model.Kategorija,
                    KorisnikId = model.KorisnikId,
                    Sopstvenik = model.Sopstvenik,
                    AgencijaId = model.AgencijaId,
                    Agencija = model.Agencija,
                    Omilen = model.Omilen,
                    BrojOmileni = model.BrojOmileni,
                    MainImage = uniqueFileName
                };

                _context.Add(nedviznosti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        private string UploadedFile(NedviznostiVM model)
        {
            string fileName = null;

            if (model.MainImage != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "-" + model.MainImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.MainImage.CopyTo(fileStream);
                }
            }

            return fileName;
        }

        // GET: Nedviznosti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nedviznosti = await _context.Nedviznosti.FindAsync(id);
            if (nedviznosti == null)
            {
                return NotFound();
            }

            NedviznostiVM nedv;
            nedv = new NedviznostiVM
            {
                Id = nedviznosti.Id,
                Ime = nedviznosti.Ime,
                Grad = nedviznosti.Lokacija,
                Golemina = nedviznosti.Golemina,
                Cena = nedviznosti.Cena,
                Status = nedviznosti.Status,
                Kategorija = nedviznosti.Kategorija,
                KorisnikId = nedviznosti.KorisnikId,
                AgencijaId = nedviznosti.AgencijaId,
                BrojOmileni = nedviznosti.BrojOmileni,
                Mimage = nedviznosti.MainImage
            };



            ViewData["AgencijaId"] = new SelectList(_context.Agencija, "Id", "Email", nedviznosti.AgencijaId);
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Email", nedviznosti.KorisnikId);
            return View(nedv);
        }

        // POST: Nedviznosti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NedviznostiVM nedviznosti)
        {


            //string uniqueFileName = UploadedFile(model);

            if (ModelState.IsValid)
            {



                try
                {
                    Nedviznosti model = _context.Nedviznosti.FirstOrDefault(n => n.Id == nedviznosti.Id);
                    if (model != default(Nedviznosti))
                    {
                        string uniqueFileName = UploadedFile(nedviznosti);
                        model.Ime = nedviznosti.Ime;
                        model.Lokacija = nedviznosti.Grad;
                        model.Golemina = nedviznosti.Golemina;
                        model.Cena = nedviznosti.Cena;
                        model.Status = nedviznosti.Status;
                        model.Kategorija = nedviznosti.Kategorija;
                        model.KorisnikId = nedviznosti.KorisnikId;
                        model.Sopstvenik = nedviznosti.Sopstvenik;
                        model.AgencijaId = nedviznosti.AgencijaId;
                        model.Agencija = nedviznosti.Agencija;
                        model.Omilen = nedviznosti.Omilen;
                        model.BrojOmileni = nedviznosti.BrojOmileni;
                        model.MainImage = uniqueFileName;
                    }
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NedviznostiExists(nedviznosti.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            ViewData["AgencijaId"] = new SelectList(_context.Agencija, "Id", "Email", nedviznosti.AgencijaId);
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Email", nedviznosti.KorisnikId);
            return View(nedviznosti);
        }

        // GET: Nedviznosti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nedviznosti = await _context.Nedviznosti
                .Include(n => n.Agencija)
                .Include(n => n.Sopstvenik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nedviznosti == null)
            {
                return NotFound();
            }

            return View(nedviznosti);
        }

        // POST: Nedviznosti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nedviznosti = await _context.Nedviznosti.FindAsync(id);
            _context.Nedviznosti.Remove(nedviznosti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NedviznostiExists(int id)
        {
            return _context.Nedviznosti.Any(e => e.Id == id);
        }
    }
}