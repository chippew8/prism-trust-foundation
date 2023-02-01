using EDP_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EDP_Project
{
    public class MyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public MyDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("MyConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponRedemption> CouponRedemptions { get; set; }
    }
 }
