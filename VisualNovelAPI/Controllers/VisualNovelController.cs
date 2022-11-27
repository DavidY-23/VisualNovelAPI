using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisualNovelAPI.Models;


namespace VisualNovelAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VisualNovelController : ControllerBase
    {
        private readonly VisualNovelDBContext _context;

        public VisualNovelController(VisualNovelDBContext context)
        {
            _context = context;
        }

        // GET: api/VisualNovel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisualNovel>>> GetVisualNovel()
        {

           
          if (_context.VisualNovel == null)
          {
                string statusDescription = "Error, there are no visual novels";
                return StatusCode(404, statusDescription);
            }
            return await _context.VisualNovel.ToListAsync();
        }

        // GET: api/VisualNovel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisualNovel>> GetVisualNovel(int id)
        {
          if (_context.VisualNovel == null)
          {
              return NotFound();
          }
            var visualNovel = await _context.VisualNovel.FindAsync(id);

            if (visualNovel == null)
            {
                string statusDescription = "Error, this visual novel ID does not exist!";
                return StatusCode(404, statusDescription);
            }

            return visualNovel;
        }

        // PUT: api/VisualNovel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisualNovel(int id, VisualNovel visualNovel)
        {
            if (id != visualNovel.VisualNovelID)
            {
                return BadRequest();
            }

            _context.Entry(visualNovel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisualNovelExists(id))
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

        // POST: api/VisualNovel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VisualNovel>> PostVisualNovel(VisualNovel visualNovel)
        {
          if (_context.VisualNovel == null)
          {
              return Problem("Entity set 'VisualNovelDBContext.VisualNovel'  is null.");
          }
            _context.VisualNovel.Add(visualNovel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisualNovel", new { id = visualNovel.VisualNovelID }, visualNovel);
        }

        // DELETE: api/VisualNovel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisualNovel(int id)
        {
            if (_context.VisualNovel == null)
            {
                return NotFound();
            }
            var visualNovel = await _context.VisualNovel.FindAsync(id);
            if (visualNovel == null)
            {
                string statusDescription = "Error, this visual novel ID does not exist!";
                return StatusCode(404, statusDescription);
            }

            _context.VisualNovel.Remove(visualNovel);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Successfully deleted visual novel entry!");
        }

        private bool VisualNovelExists(int id)
        {
            return (_context.VisualNovel?.Any(e => e.VisualNovelID == id)).GetValueOrDefault();
        }
    }
}
