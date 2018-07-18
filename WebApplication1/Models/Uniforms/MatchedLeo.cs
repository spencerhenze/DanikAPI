using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanikAPI.Models.Uniforms
{
	public class MatchedLeo
    {
	    [Required]
	    public int Id { get; set; }

		[Required]
		public int SeasonYear { get; set; }

		[Required]
		public int SoldByGymnastId { get; set; }

	    [Required]
	    public int MatchedToGymnastId { get; set; }

		[Required]
	    public int LeoId { get; set; }
    }
}
