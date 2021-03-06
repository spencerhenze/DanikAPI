﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanikAPI.Models
{
	public class Session
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public bool Active { get; set; }

		// Relationships
		public List<Test> Tests { get; set; }
	}
}
