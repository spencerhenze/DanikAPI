using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DanikAPI.Models
{
    public class ApplicationDbContext: IdentityDbContext
    {
        private IConfiguration _config;

	    public ApplicationDbContext(DbContextOptions options) : base(options)
	    {
	    }

        public ApplicationDbContext(IConfiguration config, DbContextOptions options) : base(options)
        {
            // Used only for derived test db context setup
            this._config = config;
        }

        public DbSet<Gymnast> Gymnasts { get; set; }
	    public DbSet<LineItem> LineItems { get; set; }
	    public DbSet<Payment> Payments { get; set; }
	    public DbSet<Session> Sessions { get; set; }
	    public DbSet<Test> Tests { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var connectionString = "Server = (localdb)\\mssqllocaldb;Database=danik-local;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // put alternate keys and indexes in here

            base.OnModelCreating(modelBuilder);
        }

	}
}
