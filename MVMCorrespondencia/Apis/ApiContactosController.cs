using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVMCorrespondencia.Datos;
using MVMCorrespondencia.Models;

namespace MVMCorrespondencia.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiContactosController : ControllerBase
    {
        private readonly BdmvmcorrespondenciaContext _context;

        public ApiContactosController(BdmvmcorrespondenciaContext context)
        {
            _context = context;
        }

        // GET: api/ApiContactos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contactos>>> GetContactos()
        {
            return await _context.Contactos.ToListAsync();
        }

        // GET: api/ApiContactos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contactos>> GetContactos(int id)
        {
            var contactos = await _context.Contactos.FindAsync(id);

            if (contactos == null)
            {
                return NotFound();
            }

            return contactos;
        }

        // PUT: api/ApiContactos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactos(int id, Contactos contactos)
        {
            if (id != contactos.Idcontacto)
            {
                return BadRequest();
            }

            _context.Entry(contactos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiContactos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Contactos>> PostContactos(Contactos contactos)
        {
            _context.Contactos.Add(contactos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactos", new { id = contactos.Idcontacto }, contactos);
        }

        // DELETE: api/ApiContactos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contactos>> DeleteContactos(int id)
        {
            var contactos = await _context.Contactos.FindAsync(id);
            if (contactos == null)
            {
                return NotFound();
            }

            _context.Contactos.Remove(contactos);
            await _context.SaveChangesAsync();

            return contactos;
        }

        private bool ContactosExists(int id)
        {
            return _context.Contactos.Any(e => e.Idcontacto == id);
        }
    }
}
