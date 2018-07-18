using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanikAPI.Models
{
	public class Test
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string GymnastName { get; set; }

		[Required]
		public int GymnastLevel { get; set; }

		[Required]
		public int SessionName { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public int Type { get; set; }

		[Required]
		public string VersionName { get; set; }

		// [Required]
		// public int Data { get; set; }
		// this is a custom object that should have a type. Like a DTO or something?

		[Required]
		public int FinalScore { get; set; }

		[Required]
		public bool Active { get; set; }


		//Relationships
		[Required]
		public int GymnastId { get; set; }

		[Required]
		public int SessionId { get; set; }
	}
}
