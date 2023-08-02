using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjekatWeb2.Infrastructure;
using ProjekatWeb2.Models;

namespace ProjekatWeb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PorudzbinaController : ControllerBase
    {
        private readonly OnlineProdavnicaDbContext _context;

        public PorudzbinaController(OnlineProdavnicaDbContext context)
        {
            _context = context;
        }

        // GET: api/Porudzbina
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Porudzbina>>> GetPorudzbine()
        {
          if (_context.Porudzbine == null)
          {
              return NotFound();
          }
            return await _context.Porudzbine.ToListAsync();
        }

        // GET: api/Porudzbina/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Porudzbina>> GetPorudzbina(long id)
        {
          if (_context.Porudzbine == null)
          {
              return NotFound();
          }
            var porudzbina = await _context.Porudzbine.FindAsync(id);

            if (porudzbina == null)
            {
                return NotFound();
            }

            return porudzbina;
        }

        // PUT: api/Porudzbina/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPorudzbina(long id, Porudzbina porudzbina)
        {
            if (id != porudzbina.Id)
            {
                return BadRequest();
            }

            _context.Entry(porudzbina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PorudzbinaExists(id))
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

        // POST: api/Porudzbina
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Porudzbina>> PostPorudzbina(Porudzbina porudzbina)
        {
          if (_context.Porudzbine == null)
          {
              return Problem("Entity set 'OnlineProdavnicaDbContext.Porudzbine'  is null.");
          }
            _context.Porudzbine.Add(porudzbina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPorudzbina", new { id = porudzbina.Id }, porudzbina);
        }

        // DELETE: api/Porudzbina/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePorudzbina(long id)
        {
            if (_context.Porudzbine == null)
            {
                return NotFound();
            }
            var porudzbina = await _context.Porudzbine.FindAsync(id);
            if (porudzbina == null)
            {
                return NotFound();
            }

            _context.Porudzbine.Remove(porudzbina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PorudzbinaExists(long id)
        {
            return (_context.Porudzbine?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
