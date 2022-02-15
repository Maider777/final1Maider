using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MisilesController : ControllerBase
    {
        private readonly Context _context;

        public MisilesController(Context context)
        {
            _context = context;
        }

        // GET: api/Misiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Misil>>> Getmisil()
        {
            return await _context.misil.ToListAsync();
        }

        // GET: api/Misiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Misil>> GetMisil(int id)
        {
            var misil = await _context.misil.FindAsync(id);

            if (misil == null)
            {
                return NotFound();
            }

            return misil;
        }

        // PUT: api/Misiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMisil(int id, Misil misil)
        {
            if (id != misil.IdMisil)
            {
                return BadRequest();
            }

            _context.Entry(misil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MisilExists(id))
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

        // POST: api/Misiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Misil>> PostMisil(Misil misil)
        // {
        //     _context.misil.Add(misil);
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateException)
        //     {
        //         if (MisilExists(misil.IdMisil))
        //         {
        //             return Conflict();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return CreatedAtAction("GetMisil", new { id = misil.IdMisil }, misil);
        // }

        [HttpPost("misil")]
        public async Task<ActionResult<Misil>> PostMisil(Misil misil)
        {
            _context.misil.Add(misil);
            await _context.SaveChangesAsync();
            return Ok(misil);
        }

        // DELETE: api/Misiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMisil(int id)
        {
            var misil = await _context.misil.FindAsync(id);
            if (misil == null)
            {
                return NotFound();
            }

            _context.misil.Remove(misil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MisilExists(int id)
        {
            return _context.misil.Any(e => e.IdMisil == id);
        }
    }
}
