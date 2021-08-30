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
    public class DegreesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Degrees
        [NonAction]
        public ActionResult Index()
        {
            return View(db.Degree.ToList());
        }

        // GET: Degrees/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Degree degree = db.Degree.Find(id);
            if (degree == null)
            {
                return HttpNotFound();
            }
            var Course = db.DaleelPracticalTrainings.Where(D => D.ID == degree.CourseID).FirstOrDefault();
            ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + Course.ID;
            var userUpdate = db.Users.Find(degree.UpdateUserId);
            if (userUpdate != null)
                ViewData["UpdatedName"] = userUpdate.UserName;
            else
                ViewData["UpdatedName"] = null;
            var UserCreate = db.Users.Find(degree.CreationUserId);
            if (UserCreate != null)
                ViewData["CreateName"] = UserCreate.UserName;
            else
                ViewData["CreateName"] = null;
            return View(degree);
        }

        // GET: Degrees/Create
        public ActionResult Create(int id)
        {
            try
            {
                ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D=>D.ID==id).ToList(), "ID", "CourseName");
                var Course = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + Course.ID;
                return View();
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // POST: Degrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Degree degree)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    degree.IsDeleted = false;
                    degree.CreationDate = DateTime.Now;
                    degree.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    db.Degree.Add(degree);
                    db.SaveChanges();
                    return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = degree.CourseID });
                    
                }

                return View(degree);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: Degrees/Edit/5
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        public ActionResult Edit(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Degree degree = db.Degree.Find(id);
            var Course = db.DaleelPracticalTrainings.Where(D => D.ID == degree.CourseID).FirstOrDefault();
            ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + Course.ID;
            ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D => D.ID == degree.CourseID && D.IsDeleted == false).ToList(), "ID", "CourseName");
            if (degree == null)
            {
                return HttpNotFound();
            }
            return View(degree);
        }

        // POST: Degrees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Degree degree)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    degree.IsDeleted = false;
                    degree.UpdateDate = DateTime.Now;
                    degree.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    db.Entry(degree).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = degree.CourseID });
                    
                }
                return View(degree);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: Degrees/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Degree degree = db.Degree.Find(id);
                var Course = db.DaleelPracticalTrainings.Where(D => D.ID == degree.CourseID).FirstOrDefault();
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + Course.ID;
                if (degree == null)
                {
                    return HttpNotFound();
                }
                return View(degree);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // POST: Degrees/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Degree degree = db.Degree.Find(id);
                degree.UpdateDate = DateTime.Now;
                degree.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                degree.IsDeleted = true;
                //db.Degree.Remove(degree);
                db.SaveChanges();
                return RedirectToAction("DetailsPraticalTheoretical", "Coursedistributions", new { id = degree.CourseID });
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
