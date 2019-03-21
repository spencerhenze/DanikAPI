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
	[Route("api/Tests")]
	public class TestsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public TestsController(ApplicationDbContext context, IClothingUpdateService clothingService)
		{
			_context = context;
		}

		// GET: api/Tests
		[HttpGet]
		public async Task<IActionResult> GetTests()
		{
			return Ok(await _context.Tests.ToListAsync());
		}

		// GET: api/Tests/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetTest([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var test = await _context.Tests.SingleOrDefaultAsync(m => m.Id == id);

			if (test == null)
			{
				return NotFound();
			}

			return Ok(test);
		}

		// PUT: api/Tests
		[HttpPut]
		public async Task<IActionResult> PutTest([FromBody] Test updatedTest)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var testInDb = await _context.Tests.FirstOrDefaultAsync(g => g.Id == updatedTest.Id);
				if (testInDb == null)
				{
					throw new KeyNotFoundException();
				}
				_context.Entry(testInDb).CurrentValues.SetValues(updatedTest);
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

		// POST: api/Tests
		[HttpPost]
		public async Task<IActionResult> AddTest([FromBody] Test test)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					throw new InvalidDataException();
				}

				_context.Tests.Add(test);
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetTest", new { id = test.Id }, test);
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

		// DELETE: api/Tests/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTest([FromRoute] int id)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					throw new InvalidDataException();
				}

				var test  = await _context.Tests.SingleOrDefaultAsync(m => m.Id == id);
				if (test == null)
				{
					return NotFound();
				}

				_context.Tests.Remove(test);
				await _context.SaveChangesAsync();

				return Ok();
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
	}
}