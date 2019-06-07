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
	public class PaymentsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PaymentsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/Payments
		[HttpGet]
		public IEnumerable<Payment> GetPayments()
		{
			return _context.Payments;
		}

		// GET: api/Payments/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetPayment([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var payment = await _context.Payments.SingleOrDefaultAsync(m => m.Id == id);

			if (payment == null)
			{
				return NotFound();
			}

			return Ok(payment);
		}

		// PUT: api/Payments
		[HttpPut]
		public async Task<IActionResult> UpdatePayment([FromBody] Payment updatedPayment)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var paymentInDb = await _context.Payments.FirstOrDefaultAsync(g => g.Id == updatedPayment.Id);
				if (paymentInDb == null)
				{
					throw new KeyNotFoundException();
				}
				_context.Entry(paymentInDb).CurrentValues.SetValues(updatedPayment);
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

		// POST: api/Payments
		[HttpPost]
		public async Task<IActionResult> AddPayment([FromBody] Payment payment)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					throw new InvalidDataException();
				}

				_context.Payments.Add(payment);
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetPayment", new { id = payment.Id }, payment);
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

		// DELETE: api/Payments/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePayment([FromRoute] int id)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					throw new InvalidDataException();
				}

				var payment = await _context.Payments.SingleOrDefaultAsync(m => m.Id == id);
				if (payment == null)
				{
					return NotFound();
				}

				_context.Payments.Remove(payment);
				await _context.SaveChangesAsync();

				return Ok(payment);
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

		private bool PaymentExists(int id)
		{
			return _context.Payments.Any(e => e.Id == id);
		}
	}
}