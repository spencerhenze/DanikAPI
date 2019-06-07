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
	public class LineItemsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public LineItemsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/LineItems
//		[HttpGet]
//		public async Task<IActionResult> GetLineItems()
//		{
//			return Ok(await _context.LineItems.ToListAsync());
//		}

		// GET: api/LineItems/5
		[HttpGet("{gymnastId}")]
		public async Task<IActionResult> GetLineItemsByGymnastId([FromRoute] int gymnastId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var lineItems = await _context.LineItems.Where(l => l.GymnastId == gymnastId).ToListAsync();

			return Ok(lineItems);
		}

		// PUT: api/LineItems
		[HttpPut]
		public async Task<IActionResult> UpdateLineItem([FromBody] LineItem updatedLineItem)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var lineItemInDb = await _context.LineItems.FirstOrDefaultAsync(g => g.Id == updatedLineItem.Id);
				if (lineItemInDb == null)
				{
					throw new KeyNotFoundException();
				}
				_context.Entry(lineItemInDb).CurrentValues.SetValues(updatedLineItem);
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

		// POST: api/LineItems
		[HttpPost]
		public async Task<IActionResult> AddLineItem([FromBody] LineItem lineItem)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					throw new InvalidDataException();
				}

				_context.LineItems.Add(lineItem);
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

		// DELETE: api/LineItems/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteLineItem([FromRoute] int id)
		{
			try
			{
				var lineItem = await _context.LineItems.SingleOrDefaultAsync(l => l.Id == id);
				if (lineItem == null)
				{
					return NotFound();
				}

				_context.LineItems.Remove(lineItem);
				await _context.SaveChangesAsync();

				return Ok(lineItem);
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