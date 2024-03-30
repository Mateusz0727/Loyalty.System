
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Loyalty.System.Data.Model
{
    public class BaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private IConfiguration Configuration { get; }

        private string ConnectionString { get; set; }
        public BaseContext()
        {
        }

        public BaseContext(DbContextOptions<BaseContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DefaultConnectionString");

        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserPoints> UserPoints { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 => optionsBuilder.UseSqlServer(ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
                modelBuilder.Entity<UserPoints>().ToTable("UserPoints");
                modelBuilder.Entity<User>().ToTable("User");
           
      
        }
    }
}
