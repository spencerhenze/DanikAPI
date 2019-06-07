using DanikAPI.Models;
using DanikAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DanikAPI.Controllers
{
	[Produces("application/json")]
    [Route("[controller]")]
	public class GymnastsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IClothingUpdateService _clothingUpdateService;

		public GymnastsController(ApplicationDbContext context, IClothingUpdateService clothingService)
		{
			_context = context;
			_clothingUpdateService = clothingService;
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

		// PUT: api/Gymnasts
		[HttpPut]
		public async Task<IActionResult> PutGymnast([FromBody] Gymnast updatedGymnast)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var gymnastInDb = await _context.Gymnasts.FirstOrDefaultAsync(g => g.Id == updatedGymnast.Id);
				if (gymnastInDb == null)
				{
					throw new KeyNotFoundException();
				}
				// If training level has changed, check to see if new clothing is needed
				//				if (updatedGymnast.Level != gymnastInDb.Level) _clothingUpdateService.SetGymnastClothingNeedsFlags(gymnastInDb, updatedGymnast);

				// If measurements have changed, check to see if new clothing is needed
				_context.Entry(gymnastInDb).State = EntityState.Modified;
				_context.Entry(gymnastInDb).CurrentValues.SetValues(updatedGymnast);
				await _context.SaveChangesAsync();

				return Ok();
			}
			catch (Exception ex)
			{
				if (ex is KeyNotFoundException)
				{
					return NotFound();
				}
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		// POST: api/Gymnasts
		[HttpPost]
		public async Task<IActionResult> AddGymnast([FromBody] Gymnast gymnast)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					throw new InvalidDataException();
				}

				_context.Gymnasts.Add(gymnast);
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetGymnast", new { id = gymnast.Id }, gymnast);
			}
			catch (Exception ex)
			{
				if (ex is InvalidDataException)
				{
					return BadRequest();
				}
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		// DELETE: api/Gymnasts/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGymnast([FromRoute] int id)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					throw new InvalidDataException();
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
			catch (Exception ex)
			{
				if (ex is InvalidDataException)
				{
					return BadRequest(ModelState);
				}
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		private bool GymnastExists(int id)
		{
			return _context.Gymnasts.Any(e => e.Id == id);
		}
	}
}