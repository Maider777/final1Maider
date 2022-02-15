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
    public class FrentesController : ControllerBase
    {
        private readonly Context _context;

        public FrentesController(Context context)
        {
            _context = context;
        }

        // GET: api/Frentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Frente>>> Getfrente()
        {
            return await _context.frente.ToListAsync();
        }

        // GET: api/Frentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Frente>> GetFrente(int id)
        {
            var frente = await _context.frente.FindAsync(id);

            if (frente == null)
            {
                return NotFound();
            }

            return frente;
        }

        // PUT: api/Frentes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFrente(int id, Frente frente)
        {
            if (id != frente.IdFrente)
            {
                return BadRequest();
            }

            _context.Entry(frente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FrenteExists(id))
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

        // POST: api/Frentes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // POST: api/Misiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Frente>> PostFrente(Frente frente)
        {
            _context.frente.Add(frente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FrenteExists(frente.IdFrente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFrente", new { id = frente.IdFrente }, frente);
        }

        // [HttpPost("frente")]
        // public async Task<ActionResult<Frente>> PostFrente(Frente frente)
        // {
        //     _context.frente.Add(frente);
        //     await _context.SaveChangesAsync();
        //     return Ok(frente);
        // }

        // DELETE: api/Frentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFrente(int id)
        {
            var frente = await _context.frente.FindAsync(id);
            if (frente == null)
            {
                return NotFound();
            }

            _context.frente.Remove(frente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> GetPotencia()
        {
            /**
            para consultar el total potencia en cada frente(de todos y de uno en concreto)
            */
            var resultado= await _context.contrato.Join(_context.misil, c=> c.IdMisil, o=> o.IdMisil, (c, o) => new {
                    frente = c.idFrente,
                    valorTotal = c.Cantidad * o.Megatones
                }).GroupBy(g=>g.frente).Select(g=>new{
                    Frente = g.Key,
                    ValorTotal = g.Sum(g => g.valorTotal),
                }).ToListAsync();

            return resultado;
        }

        // GET: api/Contratos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetPotencia(int id)
        {
            /**
            para consultar el total potencia en cada frente(de todos y de uno en concreto)
            */
            var lista= await _context.contrato.Join(_context.misil, c=> c.IdMisil, o=> o.IdMisil, (c, o) => new {
                    frente = c.idFrente,
                    valorTotal = c.Cantidad * o.Megatones
                }).GroupBy(g=>g.frente).Select(g=>new{
                    Frente = g.Key,
                    ValorTotal = g.Sum(g => g.valorTotal),
                }).ToListAsync();
            var resultado = lista.Find(e=>e.Frente== id);
            return resultado;
        }

        // [Route("total")]
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Object>>> GetTotal()
        // {
        //     /**
        //     el total de potencia por pais, y de cuantas zonas proviene(de todos)
        //     */
        //     var resultado= await _context.contrato.Join(_context.misil, c=> c.IdMisil, o=> o.IdMisil, (c, o) => new {
        //             Frente = c.frente,
        //             valorTotal = c.Cantidad * o.Megatones
        //         }).Join(_context.frente, c=> c.Frente.IdFrente, o=> o.IdFrente, (c, o) => new {
        //             Pais = c.Frente.Pais,
        //             valorTotal = c.valorTotal
        //         }).GroupBy(g=>g.Pais).Select(g=>new{
        //             Pais = g.Key,
        //             ValorTotal = g.Sum(g => g.valorTotal),
        //             Zonas = g.Count(),
        //         }).ToListAsync();

        //     return resultado;
        // }

        // [Route("total")]
        // [HttpGet]
        // public async Task<ActionResult<Object>> GetTotal(string pais)
        // {
        //     /**
        //     el total de potencia por pais, y de cuantas zonas proviene(de todos)
        //     */
        //     var lista= await _context.contrato.Join(_context.misil, c=> c.IdMisil, o=> o.IdMisil, (c, o) => new {
        //             Frente = c.frente,
        //             valorTotal = c.Cantidad * o.Megatones
        //         }).Join(_context.frente, c=> c.Frente.IdFrente, o=> o.IdFrente, (c, o) => new {
        //             Pais = c.Frente.Pais,
        //             valorTotal = c.valorTotal
        //         }).GroupBy(g=>g.Pais).Select(g=>new{
        //             Pais = g.Key,
        //             ValorTotal = g.Sum(g => g.valorTotal),
        //             Zonas = g.Count(),
        //         }).ToListAsync();
        //     var resultado = lista.Find(e=>e.Pais== pais);
        //     return resultado;
        // }

        private bool FrenteExists(int id)
        {
            return _context.frente.Any(e => e.IdFrente == id);
        }
    }
}
