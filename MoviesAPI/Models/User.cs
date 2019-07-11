
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models {
	public class User {

		[Key]
		public Guid Id { get; set; }

		public string Firstname { get; set; }

		public string Surname { get; set; }

		public virtual ICollection<Movie> Movies { get; set; }

	}
}