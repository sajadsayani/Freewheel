using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MoviesAPI.Models;

namespace MoviesAPI.DataAccess {
	public class MovieAPIContext : DbContext {

		public MovieAPIContext() : base("MovieContext") {
		}

		public DbSet<Movie> Movies { get; set; }

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}