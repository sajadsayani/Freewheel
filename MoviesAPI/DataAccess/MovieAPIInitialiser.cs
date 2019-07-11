using System;
using System.Collections.Generic;
using MoviesAPI.Models;

namespace MoviesAPI.DataAccess {
	public class MovieAPIInitialiser : System.Data.Entity.DropCreateDatabaseIfModelChanges<MovieAPIContext> {
		protected override void Seed(MovieAPIContext context) {
			var movies = new List<Movie> {
				new Movie{Id = Guid.Parse("8FCF65EC-410A-4F55-990F-49A810C97E79"), Title = "Marvel Spiderman - Far from Home", Genres = "Action", RunningTime = 120, YearOfRelease = 2019, Rating = 5},
				new Movie{Id = Guid.Parse("1E7C000D-CED3-404D-9070-93338EA11D3C"), Title = "Marvel Avengers End Game", Genres = "Action", RunningTime = 150, YearOfRelease = 2018, Rating = 4},
				new Movie{Id = Guid.Parse("07D1CA5A-B2E4-4543-8DB0-69ADA73CF643"), Title = "Marvel Ironman", Genres = "Action", RunningTime = 120, YearOfRelease = 2008, Rating = 5}
			};
			movies.ForEach(s => context.Movies.Add(s));
			context.SaveChanges();

			var users = new List<User> {
				new User {Id = new Guid(), Firstname = "Carson", Surname = "Alexander"},
				new User {Id = new Guid(), Firstname = "Meredith", Surname = "Alonso"},
				new User {Id = new Guid(), Firstname = "Arturo", Surname = "Anand"}
			};

			users.ForEach(s => context.Users.Add(s));
			context.SaveChanges();

		}
	}
}