using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanikAPI.Models
{
    public class LineItem
    {
	    [Required]
	    public int Id { get; set; }

		[Required]
	    public string Name { get; set; }

		[Required]
	    public int Amount { get; set; }

	    [Required]
	    public int Paid { get; set; }

	    [Required]
	    public bool PaidInFull { get; set; }

		//Relationships
	    [Required]
	    public int GymnastId { get; set; }
		public Gymnast Gymnast { get; set; }
	}
}
