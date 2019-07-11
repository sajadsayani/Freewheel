using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models {
	public class Movie {

		[Key]
		public Guid Id { get; set; }

		public string Title { get; set; }

		public int YearOfRelease { get; set; }

		public string Genres { get; set; }

		public int RunningTime { get; set; }

		public int Rating { get; set; }

		public virtual User User { get; set; }
	}
}