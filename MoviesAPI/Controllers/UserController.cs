using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MoviesAPI.DataAccess;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers {
	public class UserController : Controller {
		private MovieAPIContext db = new MovieAPIContext();

		// GET: Users
		public ActionResult Index() {
			return View(db.Users.ToList());
		}

		// GET: Users/Details/5
		public ActionResult Details(Guid? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			User user = db.Users.Find(id);
			if (user == null) {
				return HttpNotFound();
			}

			return View(user);
		}

		// GET: Users/Create
		public ActionResult Create() {
			return View();
		}

		// POST: Users/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Firstname,Surname")] User user) {
			if (ModelState.IsValid) {
				user.Id = Guid.NewGuid();
				db.Users.Add(user);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(user);
		}

		// GET: Users/Edit/5
		public ActionResult Edit(Guid? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User user = db.Users.Find(id);
			if (user == null) {
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: Users/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Firstname,Surname")] User user) {
			var userExists = db.Users.FirstOrDefault(x => x.Firstname + " " + x.Surname == user.Firstname + " " + user.Surname);
			if (userExists == null) {
				return HttpNotFound();
			}

			if (ModelState.IsValid) {
				db.Entry(user).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(user);
		}

		// GET: Users/Delete/5
		public ActionResult Delete(Guid? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User user = db.Users.Find(id);
			if (user == null) {
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: Users/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(Guid id) {
			User user = db.Users.Find(id);
			db.Users.Remove(user);
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
