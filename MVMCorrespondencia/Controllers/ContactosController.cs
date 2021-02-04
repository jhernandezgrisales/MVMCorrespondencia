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
    public class ContactosController : Controller
    {
        private readonly BdmvmcorrespondenciaContext _context;

        public ContactosController(BdmvmcorrespondenciaContext context)
        {
            _context = context;
        }

        // GET: Contactos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contactos.ToListAsync());
        }

        // GET: Contactos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactos = await _context.Contactos
                .FirstOrDefaultAsync(m => m.Idcontacto == id);
            if (contactos == null)
            {
                return NotFound();
            }

            return View(contactos);
        }

        // GET: Contactos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contactos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idcontacto,Documento,Nombre,Direccion,Celular,Correo")] Contactos contactos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactos);
        }

        // GET: Contactos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           try
           {
                if (id == null)
                {
                    return NotFound();
                }

                var contactos = await _context.Contactos.FindAsync(id);
                if (contactos == null)
                {
                    return NotFound();
                }
                return View(contactos);
           }
           catch
           {
                return NotFound();
           }
        }

        // POST: Contactos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idcontacto,Documento,Nombre,Direccion,Celular,Correo")] Contactos contactos)
        {
            if (id != contactos.Idcontacto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactosExists(contactos.Idcontacto))
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
            return View(contactos);
        }

        // GET: Contactos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactos = await _context.Contactos
                .FirstOrDefaultAsync(m => m.Idcontacto == id);
            if (contactos == null)
            {
                return NotFound();
            }

            return View(contactos);
        }

        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactos = await _context.Contactos.FindAsync(id);
            _context.Contactos.Remove(contactos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactosExists(int id)
        {
            return _context.Contactos.Any(e => e.Idcontacto == id);
        }
    }
}
