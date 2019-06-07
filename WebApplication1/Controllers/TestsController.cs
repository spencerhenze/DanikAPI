using DanikAPI.Models;
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

		public TestsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/Tests
		//		[HttpGet]
		//		public async Task<IActionResult> GetTests()
		//		{
		//			return Ok(await _context.Tests.ToListAsync());
		//		}

		// GET: api/Tests/Gymnasts/5
		[HttpGet("{Gymnasts/gymnastId}")]
		public async Task<IActionResult> GetGymnastTests([FromRoute] int gymnastId)
		{
			var tests = await _context.Tests.Where(t => t.GymnastId == gymnastId).ToListAsync();

			return Ok(tests);
		}

		// GET: api/Tests/Sessions/5
		[HttpGet("Sessions/{sessionId}")]
		public async Task<IActionResult> GetSessionTests([FromRoute] int sessionId)
		{
			var tests = await _context.Tests.Where(t => t.SessionId == sessionId).ToListAsync();

			return Ok(tests);
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

				return Ok();
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

				var test = await _context.Tests.SingleOrDefaultAsync(m => m.Id == id);
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