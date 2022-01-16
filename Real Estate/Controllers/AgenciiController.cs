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
    public class AgenciiController : Controller
    {
        private readonly Real_EstateContext _context;

        public static class CustomRoles
        {
            public const string Administrator = "Admin";
            public const string User = "Agencija";
            public const string AdministratorOrUser = Administrator + "," + User;
        }

        public AgenciiController(Real_EstateContext context)
        {
            _context = context;
        }

        // GET: Agencii
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agencija.ToListAsync());
        }

        // GET: Agencii/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencija = await _context.Agencija
                .Include(m => m.Nedviznosti)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (agencija == null)
            {
                return NotFound();
            }

            return View(agencija);
        }

        // GET: Agencii/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agencii/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Ime,Email,Password,DatumOsnovanje,Provizija,Prodadeni")] Agencija agencija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agencija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agencija);
        }

        // GET: Agencii/Edit/5
        [Authorize(Roles = CustomRoles.AdministratorOrUser)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencija = await _context.Agencija.FindAsync(id);
            if (agencija == null)
            {
                return NotFound();
            }
            return View(agencija);
        }

        // POST: Agencii/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = CustomRoles.AdministratorOrUser)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Email,Password,DatumOsnovanje,Provizija,Prodadeni")] Agencija agencija)
        {
            if (id != agencija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agencija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgencijaExists(agencija.Id))
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
            return View(agencija);
        }

        // GET: Agencii/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencija = await _context.Agencija
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agencija == null)
            {
                return NotFound();
            }

            return View(agencija);
        }

        // POST: Agencii/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agencija = await _context.Agencija.FindAsync(id);
            _context.Agencija.Remove(agencija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgencijaExists(int id)
        {
            return _context.Agencija.Any(e => e.Id == id);
        }
    }
}
