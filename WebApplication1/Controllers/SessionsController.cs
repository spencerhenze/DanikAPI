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
	[Route("api/Sessions")]
	public class SessionsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public SessionsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/Sessions
		[HttpGet]
		public async Task<IActionResult> GetSessions()
		{
			return Ok(await _context.Sessions.ToListAsync());
		}

		// GET: api/Sessions/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetSession([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var session = await _context.Sessions.SingleOrDefaultAsync(m => m.Id == id);

			if (session == null)
			{
				return NotFound();
			}

			return Ok(session);
		}

		// PUT: api/Sessions
		[HttpPut]
		public async Task<IActionResult> PutSession([FromBody] Session updatedSession)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var sessionInDb = await _context.Sessions.FirstOrDefaultAsync(g => g.Id == updatedSession.Id);
				if (sessionInDb == null)
				{
					throw new KeyNotFoundException();
				}

				_context.Entry(sessionInDb).CurrentValues.SetValues(updatedSession);
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

		// POST: api/Sessions
		[HttpPost]
		public async Task<IActionResult> AddSession([FromBody] Session session)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					throw new InvalidDataException();
				}

				_context.Sessions.Add(session);
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetSession", new { id = session.Id }, session);
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

		// DELETE: api/Sessions/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSession([FromRoute] int id)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					throw new InvalidDataException();
				}

				var session  = await _context.Sessions.SingleOrDefaultAsync(m => m.Id == id);
				if (session == null)
				{
					return NotFound();
				}

				_context.Sessions.Remove(session);
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