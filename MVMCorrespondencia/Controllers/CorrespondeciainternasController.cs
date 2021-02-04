using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVMCorrespondencia.Datos;
using MVMCorrespondencia.Models;

namespace MVMCorrespondencia.Controllers
{
    public class CorrespondeciainternasController : Controller
    {
        private readonly BdmvmcorrespondenciaContext _context;

        public CorrespondeciainternasController(BdmvmcorrespondenciaContext context)
        {
            _context = context;
        }

        // GET: Correspondeciainternas
        public async Task<IActionResult> Index()
        {
            var bdmvmcorrespondenciaContext = _context.Correspondeciainterna.Include(c => c.IdcontactodestinatarioNavigation).Include(c => c.IdcontactoremitenteNavigation);
            return View(await bdmvmcorrespondenciaContext.ToListAsync());
        }

        // GET: Correspondeciainternas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correspondeciainterna = await _context.Correspondeciainterna
                .Include(c => c.IdcontactodestinatarioNavigation)
                .Include(c => c.IdcontactoremitenteNavigation)
                .FirstOrDefaultAsync(m => m.Consecutivointerna == id);
            if (correspondeciainterna == null)
            {
                return NotFound();
            }

            return View(correspondeciainterna);
        }

        // GET: Correspondeciainternas/Create
        public IActionResult Create()
        {
            ViewData["Idcontactodestinatario"] = new SelectList(_context.Contactos, "Idcontacto", "Nombre");
            ViewData["Idcontactoremitente"] = new SelectList(_context.Contactos, "Idcontacto", "Nombre");
            return View();
        }

        // POST: Correspondeciainternas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Consecutivointerna,Idcontactoremitente,Idcontactodestinatario,Fecha,Radicadointerna")] Correspondeciainterna correspondeciainterna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(correspondeciainterna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcontactodestinatario"] = new SelectList(_context.Contactos, "Idcontacto", "Idcontacto", correspondeciainterna.Idcontactodestinatario);
            ViewData["Idcontactoremitente"] = new SelectList(_context.Contactos, "Idcontacto", "Idcontacto", correspondeciainterna.Idcontactoremitente);
            return View(correspondeciainterna);
        }

        // GET: Correspondeciainternas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correspondeciainterna = await _context.Correspondeciainterna.FindAsync(id);
            if (correspondeciainterna == null)
            {
                return NotFound();
            }
            ViewData["Idcontactodestinatario"] = new SelectList(_context.Contactos, "Idcontacto", "Nombre", correspondeciainterna.Idcontactodestinatario);
            ViewData["Idcontactoremitente"] = new SelectList(_context.Contactos, "Idcontacto", "Nombre", correspondeciainterna.Idcontactoremitente);
            return View(correspondeciainterna);
        }

        // POST: Correspondeciainternas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Consecutivointerna,Idcontactoremitente,Idcontactodestinatario,Fecha,Radicadointerna")] Correspondeciainterna correspondeciainterna)
        {
            if (id != correspondeciainterna.Consecutivointerna)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(correspondeciainterna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorrespondeciainternaExists(correspondeciainterna.Consecutivointerna))
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
            ViewData["Idcontactodestinatario"] = new SelectList(_context.Contactos, "Idcontacto", "Idcontacto", correspondeciainterna.Idcontactodestinatario);
            ViewData["Idcontactoremitente"] = new SelectList(_context.Contactos, "Idcontacto", "Idcontacto", correspondeciainterna.Idcontactoremitente);
            return View(correspondeciainterna);
        }

        // GET: Correspondeciainternas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correspondeciainterna = await _context.Correspondeciainterna
                .Include(c => c.IdcontactodestinatarioNavigation)
                .Include(c => c.IdcontactoremitenteNavigation)
                .FirstOrDefaultAsync(m => m.Consecutivointerna == id);
            if (correspondeciainterna == null)
            {
                return NotFound();
            }

            return View(correspondeciainterna);
        }

        // POST: Correspondeciainternas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var correspondeciainterna = await _context.Correspondeciainterna.FindAsync(id);
            _context.Correspondeciainterna.Remove(correspondeciainterna);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorrespondeciainternaExists(int id)
        {
            return _context.Correspondeciainterna.Any(e => e.Consecutivointerna == id);
        }
    }
}
