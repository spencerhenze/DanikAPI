using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanikAPI.Models.Uniforms
{
	public class Order
    {
	    [Required]
	    public int Id { get; set; }

		[Required]
		public string CompanyName { get; set; }

		[Required]
		public int SeasonYear { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }

	    [Required]
		public int CreatedBy { get; set; }
    }
}
