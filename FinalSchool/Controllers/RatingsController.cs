using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalSchool.Models;
using Microsoft.AspNet.Identity;

namespace FinalSchool.Controllers
{
    public class RatingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ratings
        [NonAction]
        public ActionResult Index()
        {
            return View(db.Ratings.ToList());
        }

        // GET: Ratings/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            var Course = db.DaleelPracticalTrainings.Where(D => D.ID == rating.CourseID).FirstOrDefault();
            ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + Course.ID;
            var userUpdate = db.Users.Find(rating.UpdateUserId);
            if (userUpdate != null)
                ViewData["UpdatedName"] = userUpdate.UserName;
            else
                ViewData["UpdatedName"] = null;
            var UserCreate = db.Users.Find(rating.CreationUserId);
            if (UserCreate != null)
                ViewData["CreateName"] = UserCreate.UserName;
            else
                ViewData["CreateName"] = null;
            return View(rating);
        }

        // GET: Ratings/Create
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult Create(int id)
        {
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
            ViewData["CourseName"] = CourseName.CourseName;
            ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + CourseName.ID;
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rating rating,int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    rating.IsDeleted = false;
                    rating.CourseID = id;
                    rating.CreationDate = DateTime.Now;
                    rating.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    db.Ratings.Add(rating);
                    db.SaveChanges();
                    return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = rating.CourseID });
                  
                }

                return View(rating);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: Ratings/Edit/5
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult Edit(int? id)
        {
            try
            {
                
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Rating rating = db.Ratings.Find(id);
                ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D => D.ID == rating.CourseID && D.IsDeleted == false).ToList(), "ID", "CourseName");
                if (rating == null)
                {
                    return HttpNotFound();
                }
                var Course = db.DaleelPracticalTrainings.Where(D => D.ID == rating.CourseID).FirstOrDefault();
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + Course.ID;
                return View(rating);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Rating rating)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    rating.IsDeleted = false;
                    rating.UpdateDate = DateTime.Now;
                    rating.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    db.Entry(rating).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = rating.CourseID });
                }
                return View(rating);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: Ratings/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Rating rating = db.Ratings.Find(id);
                if (rating == null)
                {
                    return HttpNotFound();
                }
                var Course = db.DaleelPracticalTrainings.Where(D => D.ID == rating.CourseID).FirstOrDefault();
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + Course.ID;
                return View(rating);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // POST: Ratings/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
               
                Rating rating = db.Ratings.Find(id);
                //db.Ratings.Remove(rating);
                rating.UpdateDate = DateTime.Now;
                rating.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                rating.IsDeleted = true;
                db.SaveChanges();
                var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == rating.CourseID && D.IsDeleted == false).FirstOrDefault();
                if (daleelPracticalTraining.Type == 1)
                {
                    return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = rating.CourseID });
                }
                else if (daleelPracticalTraining.Type == 2)
                {
                    return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = rating.CourseID });
                }
                else
                {
                    return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = rating.CourseID });
                }
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
