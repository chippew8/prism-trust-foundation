using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace prism_trust_foundation.Models
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        //public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options){ }
        public AuthDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("AuthConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<ApplicationUser> AspNetUser { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Inventory>Inventory { get; set; }
		public DbSet<VolunteerShift> VolunteerShift { get; set; }
		public DbSet<VolunteerShiftBook> VolunteerShiftBook { get; set; }

        /*tristan's db*/
        public DbSet<itemRequest> itemRequests { get; set; }

        public DbSet<cart> cart { get; set; }
        public DbSet<donationRecipient> donationRecipients { get; set; }
        /*---------*/
        /*public DbSet<Product> Products { get; set; }*/

        
	}
}
