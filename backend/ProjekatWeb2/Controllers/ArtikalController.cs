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
    [Route("api/articles")]
    [ApiController]
    public class ArtikalController : ControllerBase
    {
        private readonly OnlineProdavnicaDbContext _context;

        public ArtikalController(OnlineProdavnicaDbContext context)
        {
            _context = context;
        }

        // GET: api/Artikal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artikal>>> GetArtikli()
        {
          if (_context.Artikli == null)
          {
              return NotFound();
          }
            return await _context.Artikli.ToListAsync();
        }

        // GET: api/Artikal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artikal>> GetArtikal(long id)
        {
          if (_context.Artikli == null)
          {
              return NotFound();
          }
            var artikal = await _context.Artikli.FindAsync(id);

            if (artikal == null)
            {
                return NotFound();
            }

            return artikal;
        }

        // PUT: api/Artikal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtikal(long id, Artikal artikal)
        {
            if (id != artikal.Id)
            {
                return BadRequest();
            }

            _context.Entry(artikal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtikalExists(id))
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

        // POST: api/Artikal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artikal>> PostArtikal(Artikal artikal)
        {
          if (_context.Artikli == null)
          {
              return Problem("Entity set 'OnlineProdavnicaDbContext.Artikli'  is null.");
          }
            _context.Artikli.Add(artikal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtikal", new { id = artikal.Id }, artikal);
        }

        // DELETE: api/Artikal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtikal(long id)
        {
            if (_context.Artikli == null)
            {
                return NotFound();
            }
            var artikal = await _context.Artikli.FindAsync(id);
            if (artikal == null)
            {
                return NotFound();
            }

            _context.Artikli.Remove(artikal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtikalExists(long id)
        {
            return (_context.Artikli?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
