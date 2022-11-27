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
    public class CharacterController : ControllerBase
    {
        private readonly VisualNovelDBContext _context;

        public CharacterController(VisualNovelDBContext context)
        {
            _context = context;
        }

        // GET: api/Character
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
        {
          if (_context.Characters == null)
          {
                string statusDescription = "Error, there are no characters";
                return StatusCode(404, statusDescription);
            }
            return await _context.Characters.ToListAsync();
        }

        // GET: api/Character/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
          if (_context.Characters == null)
          {

              return NotFound();
          }
            var character = await _context.Characters.FindAsync(id);

            if (character == null)
            {
                string statusDescription = "Error this character ID does not exist!";
                return StatusCode(404, statusDescription);
            }

            return character;
        }

        // PUT: api/Character/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            if (id != character.CharacterID)
            {
                return BadRequest();
            }

            _context.Entry(character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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

        // POST: api/Character
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
          if (_context.Characters == null)
          {
              return Problem("Entity set 'VisualNovelDBContext.Characters'  is null.");
          }
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = character.CharacterID }, character);
        }

        // DELETE: api/Character/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (_context.Characters == null)
            {
                return NotFound();
            }
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                string statusDescription = "Error this character ID does not exist!";
                return StatusCode(404, statusDescription);
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Successfully deleted character!");
        }

        private bool CharacterExists(int id)
        {
            return (_context.Characters?.Any(e => e.CharacterID == id)).GetValueOrDefault();
        }
    }
}
