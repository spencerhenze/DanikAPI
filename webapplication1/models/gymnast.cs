using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DanikAPI.Models.Uniforms.Enums;

namespace DanikAPI.Models
{
    public class Gymnast
    {
	    [Required]
	    public int Id { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
	    public string LastName { get; set; }

		[Required]
		public LevelsEnum Level { get; set; }

	    public string ImgUrl { get; set; }

	    public DateTime AddedDate { get; set; }	

		public bool NeedsJacket { get; set; }

		public bool NeedsLeo { get; set; }

		public bool NeedsPants { get; set; }

	    public int ChestMeasurement { get; set; }

	    public int WaistMeasurement { get; set; }

	    public int TorsoMeasurement { get; set; }

	    public int HipsMeasurement { get; set; }

	    public int InseamMeasurement { get; set; }

		public GkLeoAndJacketSizeEnum LeoAndJacketSize { get; set; }

		public GkTorsoSizeEnum TorsoSize { get; set; }


		// related
		public List<Payment> Payments { get; set; }
    }
}
