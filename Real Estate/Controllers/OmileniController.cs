using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate.Models;
using Real_Estate_Project.Models;

namespace Real_Estate.Controllers
{
    public class OmileniController : Controller
    {
        private readonly Real_EstateContext _context;

        public OmileniController(Real_EstateContext context)
        {
            _context = context;
        }

        // GET: Omileni
        public async Task<IActionResult> Index()
        {
            var real_EstateContext = _context.Omileni.Include(o => o.Korisnik).Include(o => o.Nedviznosti);
            return View(await real_EstateContext.ToListAsync());
        }

        // GET: Omileni/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var omileni = await _context.Omileni
                .Include(o => o.Korisnik)
                .Include(o => o.Nedviznosti)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (omileni == null)
            {
                return NotFound();
            }

            return View(omileni);
        }

        // GET: Omileni/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Email");
            ViewData["NedviznostiId"] = new SelectList(_context.Nedviznosti, "Id", "Ime");
            return View();
        }

        // POST: Omileni/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KorisnikId,NedviznostiId")] Omileni omileni)
        {
            if (ModelState.IsValid)
            {
                _context.Add(omileni);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Email", omileni.KorisnikId);
            ViewData["NedviznostiId"] = new SelectList(_context.Nedviznosti, "Id", "Ime", omileni.NedviznostiId);
            return View(omileni);
        }

        // GET: Omileni/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var omileni = await _context.Omileni.FindAsync(id);
            if (omileni == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Email", omileni.KorisnikId);
            ViewData["NedviznostiId"] = new SelectList(_context.Nedviznosti, "Id", "Ime", omileni.NedviznostiId);
            return View(omileni);
        }

        // POST: Omileni/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KorisnikId,NedviznostiId")] Omileni omileni)
        {
            if (id != omileni.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(omileni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OmileniExists(omileni.Id))
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
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Email", omileni.KorisnikId);
            ViewData["NedviznostiId"] = new SelectList(_context.Nedviznosti, "Id", "Ime", omileni.NedviznostiId);
            return View(omileni);
        }

        // GET: Omileni/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var omileni = await _context.Omileni
                .Include(o => o.Korisnik)
                .Include(o => o.Nedviznosti)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (omileni == null)
            {
                return NotFound();
            }

            return View(omileni);
        }

        // POST: Omileni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var omileni = await _context.Omileni.FindAsync(id);
            _context.Omileni.Remove(omileni);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OmileniExists(int id)
        {
            return _context.Omileni.Any(e => e.Id == id);
        }
    }
}