using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiExample.Data;
using WebApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1;

namespace WebApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TantargyakController : ControllerBase
    {
        private readonly AlkalmazasDbContext _context;

        public TantargyakController(AlkalmazasDbContext context)
        {
            _context = context;
        }

        // GET: api/Tantargyak
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tantargy>>> GetTantargyak()
        {
            return await _context.Tantargyak.ToListAsync();
        }

        // GET: api/Tantargyak/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tantargy>> GetTantargy(int id)
        {
            var tantargy = await _context.Tantargyak.FindAsync(id);

            if (tantargy == null)
            {
                return NotFound();
            }

            return tantargy;
        }

        // POST: api/Tantargyak
        [HttpPost]
        public async Task<ActionResult<Tantargy>> PostTantargy(Tantargy tantargy)
        {
            tantargy.Letrehozva = DateTime.Now;
            tantargy.UtoljaraFrissitve = DateTime.Now;
            _context.Tantargyak.Add(tantargy);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTantargy), new { id = tantargy.Id }, tantargy);
        }

        // PUT: api/Tantargyak/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTantargy(int id, Tantargy tantargy)
        {
            if (id != tantargy.Id)
            {
                return BadRequest();
            }

            tantargy.UtoljaraFrissitve = DateTime.Now;
            _context.Entry(tantargy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TantargyLetezik(id))
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

        // DELETE: api/Tantargyak/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTantargy(int id)
        {
            var tantargy = await _context.Tantargyak.FindAsync(id);
            if (tantargy == null)
            {
                return NotFound();
            }

            _context.Tantargyak.Remove(tantargy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TantargyLetezik(int id)
        {
            return _context.Tantargyak.Any(e => e.Id == id);
        }
    }
}
