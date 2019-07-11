using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using MoviesAPI.DataAccess;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers {
	public class MovieController : Controller {
		private MovieAPIContext db = new MovieAPIContext();

		// GET: Movies
		public ActionResult Index() {
			return View(db.Movies.ToList());
		}

		// GET: Movies/Details/5
		public ActionResult Details(Guid? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Movie movie = db.Movies.Find(id);
			if (movie == null) {
				return HttpNotFound();
			}

			return View(movie);
		}

		public ActionResult Details(string title, int yearOfRelease, string genre) {
			if (string.IsNullOrEmpty(title) || yearOfRelease == 0 || string.IsNullOrEmpty(genre)) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var movieExists = db.Movies.FirstOrDefault(x => x.Title == title && x.Genres == genre && x.YearOfRelease == yearOfRelease);
			if (movieExists == null) {
				return HttpNotFound();
			}

			var ratingAverage =
				from y in db.Movies
				group y by y.Title into average
				select new {
					key = average.Key,
					AverageScore = average.Average(x => x.Rating),
				};

			var movie = from x in db.Movies
						where x.Title == title & x.Genres == genre & x.YearOfRelease == yearOfRelease
						select new {
							x.Id,
							x.Title,
							x.YearOfRelease,
							x.RunningTime,
							x.Rating
						};

			return View((Movie)movie);
		}

		public ActionResult Details(string firstName, string surname) {
			if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(surname)) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var movieExists = db.Movies.FirstOrDefault(x => x.User.Firstname + " " + x.User.Surname == firstName + " " + surname);
			if (movieExists == null) {
				return HttpNotFound();
			}

			var movie = (from x in db.Movies
						 where x.User.Firstname + " " + x.User.Surname == firstName + " " + surname
						 orderby x.User.Firstname + " " + x.User.Surname descending
						 select new {
							 x.Id,
							 x.Title,
							 x.YearOfRelease,
							 x.RunningTime,
							 x.Rating
						 }).Take(5);

			return View((Movie)movie);
		}

		public ActionResult Details() {
			var averageRatingByUser =
				from x in db.Movies
				group x by x.User.Firstname + " " + x.User.Surname into average
				select new {
					Key = average.Key,
					AverageScore = average.Average(x => x.Rating),
				};

			var movie = (from x in db.Movies
						 orderby x.User.Firstname + " " + x.User.Surname descending
						 select new {
							 x.Id,
							 x.Title,
							 x.YearOfRelease,
							 x.RunningTime,
							 x.Rating,
							 x.User.Firstname,
							 x.User.Surname
						 })
				.Take(5);


			return View((Movie)movie);
		}

		// GET: Movies/Create
		public ActionResult Create() {
			return View();
		}

		// POST: Movies/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Title,YearOfRelease,Genres,RunningTime,Rating")] Movie movie) {
			if (ModelState.IsValid) {
				movie.Id = Guid.NewGuid();
				db.Movies.Add(movie);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(movie);
		}

		// GET: Movies/Edit/5
		public ActionResult Edit(Guid? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Movie movie = db.Movies.Find(id);
			if (movie == null) {
				return HttpNotFound();
			}

			return View(movie);
		}

		// POST: Movies/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Title,YearOfRelease,Genres,RunningTime,Rating")] Movie movie) {

			var movieExists = db.Movies.FirstOrDefault(x => x.Id == movie.Id);
			if (movieExists == null) {
				return HttpNotFound();
			}

			if (((movie.Rating >= 'a' && movie.Rating <= 'z') || (movie.Rating >= 'A' && movie.Rating <= 'Z'))) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (ModelState.IsValid) {
				db.Entry(movie).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(movie);
		}

		// GET: Movies/Delete/5
		public ActionResult Delete(Guid? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Movie movie = db.Movies.Find(id);
			if (movie == null) {
				return HttpNotFound();
			}
			return View(movie);
		}

		// POST: Movies/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(Guid id) {
			Movie movie = db.Movies.Find(id);
			db.Movies.Remove(movie);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
