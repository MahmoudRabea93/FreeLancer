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
    public class PracticalDegreesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PracticalDegrees
        [NonAction]
        public ActionResult Index()
        {
            return View(db.practicalDegree.ToList());
        }

        // GET: PracticalDegrees/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PracticalDegree practicalDegree = db.practicalDegree.Find(id);
                var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == practicalDegree.CourseID).FirstOrDefault();
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + CourseName.ID;
                if (practicalDegree == null)
                {
                    return HttpNotFound();
                }
                var userUpdate = db.Users.Find(practicalDegree.UpdateUserId);
                if (userUpdate != null)
                    ViewData["UpdatedName"] = userUpdate.UserName;
                else
                    ViewData["UpdatedName"] = null;
                var UserCreate = db.Users.Find(practicalDegree.CreationUserId);
                if (UserCreate != null)
                    ViewData["CreateName"] = UserCreate.UserName;
                else
                    ViewData["CreateName"] = null;
                return View(practicalDegree);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: PracticalDegrees/Create
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult Create(int id)
        {
            ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D => D.ID == id).ToList(), "ID", "CourseName");
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
            ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + CourseName.ID;
            return View();
        }

        // POST: PracticalDegrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PracticalDegree practicalDegree, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    practicalDegree.IsDeleted = false;
                    practicalDegree.CourseID = id;
                    practicalDegree.CreationDate = DateTime.Now;
                    practicalDegree.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    db.practicalDegree.Add(practicalDegree);
                    db.SaveChanges();
                    return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = practicalDegree.CourseID });
                }

                return View(practicalDegree);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: PracticalDegrees/Edit/5
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracticalDegree practicalDegree = db.practicalDegree.Find(id);
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == practicalDegree.CourseID).FirstOrDefault();
            ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + CourseName.ID;
            if (practicalDegree == null)
            {
                return HttpNotFound();
            }
            return View(practicalDegree);
        }

        // POST: PracticalDegrees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PracticalDegree practicalDegree)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    practicalDegree.IsDeleted = false;
                    practicalDegree.UpdateDate = DateTime.Now;
                    practicalDegree.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    db.Entry(practicalDegree).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = practicalDegree.CourseID });

                }
                return View(practicalDegree);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: PracticalDegrees/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PracticalDegree practicalDegree = db.practicalDegree.Find(id);
                var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == practicalDegree.CourseID).FirstOrDefault();
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + CourseName.ID;
                if (practicalDegree == null)
                {
                    return HttpNotFound();
                }
                return View(practicalDegree);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // POST: PracticalDegrees/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PracticalDegree practicalDegree = db.practicalDegree.Find(id);
            practicalDegree.IsDeleted = true;
            practicalDegree.UpdateDate = DateTime.Now;
            practicalDegree.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            //db.practicalDegree.Remove(practicalDegree);
            db.SaveChanges();
            return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = practicalDegree.CourseID });
           
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
