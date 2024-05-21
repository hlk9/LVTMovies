using Microsoft.EntityFrameworkCore;
using Movies.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Context
{
    public class MovieDbContext:DbContext
    {
        public MovieDbContext()
        {
            
        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=LVTMovies;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Models.Actor> Actors { get; set; }
        public DbSet<Models.Genre> Genres { get; set; }
        public DbSet<Models.Movie> Movies { get; set; }
        public DbSet<Models.MovieActor> MovieActors { get; set; }
        public DbSet<Models.MovieGenre> MovieGenres { get; set; }
        public DbSet<Models.Role> Roles { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Payment> Payments { get; set; }
        public DbSet<Models.Rental> Rentals { get; set; }
        public DbSet<Models.WhishList> WhishLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().HasOne(u => u.User).WithMany(p => p.Payments).OnDelete(DeleteBehavior.Restrict);
        }


    }
}
