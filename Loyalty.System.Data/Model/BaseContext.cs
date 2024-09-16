

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Loyalty.System.Data.Model
{
    public class BaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _hasher;
        private readonly string _connectionString;

        public BaseContext(IConfiguration configuration, IPasswordHasher<User> hasher)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _connectionString = _configuration.GetConnectionString("DefaultConnectionString");
        }

        public BaseContext(DbContextOptions<BaseContext> options, IConfiguration configuration, IPasswordHasher<User> hasher)
            : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _connectionString = _configuration.GetConnectionString("DefaultConnectionString");
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPoints> UserPoints { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPoints>().ToTable("UserPoints");
            modelBuilder.Entity<User>().ToTable("User");

            // Seeding the initial admin user
            User adminUser = new User
            {
                Id=1,
                PublicId = Guid.NewGuid().ToString(),
                IsAdmin = true,
                Email = "admin@admin.pl",
                EmailConfirmed = true,
                GivenName = "admin",
                Surname = "admin",
                UserName = "admin@admin.pl"
            };
            
            adminUser.Password = _hasher.HashPassword(adminUser, "zaq1@WSX");
            
            modelBuilder.Entity<User>().HasData(adminUser);
            modelBuilder.Entity<UserPoints>().HasData(new UserPoints() { CountOfPrize = 0,Id=adminUser.PublicId,Points=0,QrCodeToken="" });
        }
    }
}
