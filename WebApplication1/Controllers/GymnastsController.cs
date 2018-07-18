using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DanikAPI.Models;
using DanikAPI.Helpers;

namespace DanikAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Gymnasts")]
    public class GymnastsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GymnastsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Gymnasts
        [HttpGet]
        public IEnumerable<Gymnast> GetGymnasts()
        {
            return _context.Gymnasts;
        }

        // GET: api/Gymnasts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGymnast([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gymnast = await _context.Gymnasts.SingleOrDefaultAsync(m => m.Id == id);

            if (gymnast == null)
            {
                return NotFound();
            }

            return Ok(gymnast);
        }

        // PUT: api/Gymnasts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGymnast([FromRoute] int id, [FromBody] Gymnast updatedGymnast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedGymnast.Id)
            {
                return BadRequest();
            }

	        var gymnast = _context.Gymnasts.Find(updatedGymnast.Id);
			// If training level has changed, check to see if new clothing is needed
	        if (gymnast.Level != updatedGymnast.Level)
	        {
		        gymnast.NeedsLeo = ClothingUpdateHelper.CheckIfLeoNeeded(gymnast, updatedGymnast);
		        gymnast.NeedsJacket = ClothingUpdateHelper.ChekIfJacketNeeded(gymnast, updatedGymnast);
	        }

			// If measurements have changed, check to see if new clothing is needed

            _context.Entry(gymnast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GymnastExists(id))
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

        // POST: api/Gymnasts
        [HttpPost]
        public async Task<IActionResult> PostGymnast([FromBody] Gymnast gymnast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Gymnasts.Add(gymnast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGymnast", new { id = gymnast.Id }, gymnast);
        }

        // DELETE: api/Gymnasts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGymnast([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gymnast = await _context.Gymnasts.SingleOrDefaultAsync(m => m.Id == id);
            if (gymnast == null)
            {
                return NotFound();
            }

            _context.Gymnasts.Remove(gymnast);
            await _context.SaveChangesAsync();

            return Ok(gymnast);
        }

        private bool GymnastExists(int id)
        {
            return _context.Gymnasts.Any(e => e.Id == id);
        }
    }
}