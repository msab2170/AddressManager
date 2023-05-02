using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AddressManager.Models;
using System.Reflection.Metadata;

namespace AddressManager.Data
{
    public class ManagerContext : DbContext
    {
        
        public ManagerContext (DbContextOptions<ManagerContext> options)
            : base(options)
        {
        }

        public DbSet<AddressManager.Models.User> Users { get; set; } = default!;
        public DbSet<AddressManager.Models.Address> Addresses { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Address>().ToTable("Address");
        }
    }
}
