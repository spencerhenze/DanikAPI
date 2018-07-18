using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanikAPI.Models
{
	public class Payment
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public int Amount { get; set; }

		[Required]
		public string PaymentMethod { get; set; }

		[Required]
		public string ReceivedBy { get; set; }

		public int CheckNumber { get; set; }

		public string Notes { get; set; }

		//Relationships
		[Required]
		public int GymnastId { get; set; }

		[Required]
		public int LineItemId { get; set; }
	}
}
