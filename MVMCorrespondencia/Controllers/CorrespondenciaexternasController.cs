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
    public class CorrespondenciaexternasController : Controller
    {
        private readonly BdmvmcorrespondenciaContext _context;

        public CorrespondenciaexternasController(BdmvmcorrespondenciaContext context)
        {
            _context = context;
        }

        // GET: Correspondenciaexternas
        public async Task<IActionResult> Index()
        {
            var bdmvmcorrespondenciaContext = _context.Correspondenciaexterna.Include(c => c.IdcontactodestinatarioNavigation).Include(c => c.IdcontactoremitenteNavigation);
            return View(await bdmvmcorrespondenciaContext.ToListAsync());
        }

        // GET: Correspondenciaexternas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correspondenciaexterna = await _context.Correspondenciaexterna
                .Include(c => c.IdcontactodestinatarioNavigation)
                .Include(c => c.IdcontactoremitenteNavigation)
                .FirstOrDefaultAsync(m => m.Consecutivoexterna == id);
            if (correspondenciaexterna == null)
            {
                return NotFound();
            }

            return View(correspondenciaexterna);
        }

        // GET: Correspondenciaexternas/Create
        public IActionResult Create()
        {
            ViewData["Idcontactodestinatario"] = new SelectList(_context.Contactos, "Idcontacto", "Nombre");
            ViewData["Idcontactoremitente"] = new SelectList(_context.Contactos, "Idcontacto", "Nombre");
            return View();
        }

        // POST: Correspondenciaexternas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Consecutivoexterna,Idcontactoremitente,Idcontactodestinatario,Fecha,Radicadoexterno")] Correspondenciaexterna correspondenciaexterna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(correspondenciaexterna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcontactodestinatario"] = new SelectList(_context.Contactos, "Idcontacto", "Idcontacto", correspondenciaexterna.Idcontactodestinatario);
            ViewData["Idcontactoremitente"] = new SelectList(_context.Contactos, "Idcontacto", "Idcontacto", correspondenciaexterna.Idcontactoremitente);
            return View(correspondenciaexterna);
        }

        // GET: Correspondenciaexternas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correspondenciaexterna = await _context.Correspondenciaexterna.FindAsync(id);
            if (correspondenciaexterna == null)
            {
                return NotFound();
            }
            ViewData["Idcontactodestinatario"] = new SelectList(_context.Contactos, "Idcontacto", "Nombre", correspondenciaexterna.Idcontactodestinatario);
            ViewData["Idcontactoremitente"] = new SelectList(_context.Contactos, "Idcontacto", "Nombre", correspondenciaexterna.Idcontactoremitente);
            return View(correspondenciaexterna);
        }

        // POST: Correspondenciaexternas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Consecutivoexterna,Idcontactoremitente,Idcontactodestinatario,Fecha,Radicadoexterno")] Correspondenciaexterna correspondenciaexterna)
        {
            if (id != correspondenciaexterna.Consecutivoexterna)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(correspondenciaexterna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorrespondenciaexternaExists(correspondenciaexterna.Consecutivoexterna))
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
            ViewData["Idcontactodestinatario"] = new SelectList(_context.Contactos, "Idcontacto", "Idcontacto", correspondenciaexterna.Idcontactodestinatario);
            ViewData["Idcontactoremitente"] = new SelectList(_context.Contactos, "Idcontacto", "Idcontacto", correspondenciaexterna.Idcontactoremitente);
            return View(correspondenciaexterna);
        }

        // GET: Correspondenciaexternas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correspondenciaexterna = await _context.Correspondenciaexterna
                .Include(c => c.IdcontactodestinatarioNavigation)
                .Include(c => c.IdcontactoremitenteNavigation)
                .FirstOrDefaultAsync(m => m.Consecutivoexterna == id);
            if (correspondenciaexterna == null)
            {
                return NotFound();
            }

            return View(correspondenciaexterna);
        }

        // POST: Correspondenciaexternas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var correspondenciaexterna = await _context.Correspondenciaexterna.FindAsync(id);
            _context.Correspondenciaexterna.Remove(correspondenciaexterna);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorrespondenciaexternaExists(int id)
        {
            return _context.Correspondenciaexterna.Any(e => e.Consecutivoexterna == id);
        }
    }
}
