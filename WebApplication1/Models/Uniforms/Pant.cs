using DanikAPI.Models.Uniforms.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanikAPI.Models.Uniforms
{
	public class Pant
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string Manufacturer { get; set; }

		[Required]
		public int SeasonYear { get; set; }

		[Required]
		public Enum Size { get; set; }

		[Required]
		public ClothingItemStatusEnum AvailabilityStatus { get; set; }

		public int GymnastId { get; set; }

		public int OrderId { get; set; }
	}
}
