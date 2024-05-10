using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatosApi.Models;

namespace Patos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatosController : ControllerBase
    {
        private readonly TodoContext _context;

        public PatosController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Patos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatosItem>>> GetPatosItems()
        {
            return await _context.PatosItem.ToListAsync();
        }

        // GET: api/Patos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatosItem>> GetPatosItem(long id)
        {
            var patosItem = await _context.PatosItem.FindAsync(id);

            if (patosItem == null)
            {
                return NotFound();
            }

            return patosItem;
        }

        // PUT: api/Patos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatosItem(long id, PatosItem patosItem)
        {
            if (id != patosItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(patosItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatosItemExists(id))
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


        // DELETE: api/Patos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatosItem(long id)
        {
            var patosItem = await _context.PatosItem.FindAsync(id);
            if (patosItem == null)
            {
                return NotFound();
            }

            _context.PatosItem.Remove(patosItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
                // POST: api/Patos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PatosItem>> PostPatosItem(PatosItem patosItem)
        {
            _context.PatosItem.Add(patosItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatosItem", new { id = patosItem.Id }, patosItem);
        }
        private bool PatosItemExists(long id)
        {
            return _context.PatosItem.Any(e => e.Id == id);
        }
    }
}
