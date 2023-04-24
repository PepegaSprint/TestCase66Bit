using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase66bit.Models;

namespace TestCase66Bit.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
        {

        }


        public DbSet<FootballPlayerModel> FootballPlayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FootballPlayerModel>()
                .HasIndex(c => new { c.firstName, c.lastName })
                .IsUnique();
        }

    }
}
