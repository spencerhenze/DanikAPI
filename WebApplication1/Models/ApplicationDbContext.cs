using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DanikAPI.Models
{
    public class ApplicationDbContext: IdentityDbContext
    {
	    public ApplicationDbContext(DbContextOptions options) : base(options)
	    {
		    
	    }

		public DbSet<Gymnast> Gymnasts { get; set; }
	    public DbSet<LineItem> LineItems { get; set; }
	    public DbSet<Payment> Payments { get; set; }
	    public DbSet<Session> Sessions { get; set; }
	    public DbSet<Test> Tests { get; set; }
	}
}
