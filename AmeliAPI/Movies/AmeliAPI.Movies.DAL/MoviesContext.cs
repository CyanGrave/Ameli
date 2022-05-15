using AmeliAPI.Movies.Model.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace AmeliAPI.Movies.DAL
{
    public class MoviesContext : Context
    {
        public override string SchemaName => "Movies";

        public MoviesContext() : base() { }

        public MoviesContext(DbContextOptions options) : base(options) { }



        public virtual DbSet<AssociatedPerson> AssociatedPerson { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieAssociatedPerson> MovieAssociatedPerson { get; set; }
        public virtual DbSet<MovieGenre> MovieGenre { get; set; }

    }
}
