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
    public class TheoreticalDegreesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        #region Functions
        public void CreateDeatails(TheoreticalDegree Obj)
        {
            Obj.IsDeleted = false;
            Obj.CreationDate = DateTime.Now;
            Obj.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public void UpdateAndDeleteDeatails(TheoreticalDegree Obj, bool Value)
        {
            Obj.IsDeleted = Value;
            Obj.UpdateDate = DateTime.Now;
            Obj.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }
        #endregion
        // GET: TheoreticalDegrees/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TheoreticalDegree theoreticalDegree = db.theoreticalDegree.Find(id);
                var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == theoreticalDegree.CourseID).FirstOrDefault();
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + CourseName.ID;

                if (theoreticalDegree == null)
                {
                    return HttpNotFound();
                }
                var userUpdate = db.Users.Find(theoreticalDegree.UpdateUserId);
                if (userUpdate != null)
                    ViewData["UpdatedName"] = userUpdate.UserName;
                else
                    ViewData["UpdatedName"] = null;
                var UserCreate = db.Users.Find(theoreticalDegree.CreationUserId);
                if (UserCreate != null)
                    ViewData["CreateName"] = UserCreate.UserName;
                else
                    ViewData["CreateName"] = null;
                return View(theoreticalDegree);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: TheoreticalDegrees/Create
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult Create(int id)
        {
            ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D => D.ID == id).ToList(), "ID", "CourseName");
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
            ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + CourseName.ID;
            return View();
        }

        // POST: TheoreticalDegrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TheoreticalDegree theoreticalDegree,int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CreateDeatails(theoreticalDegree);
                    theoreticalDegree.CourseID = id;                 
                    db.theoreticalDegree.Add(theoreticalDegree);
                    db.SaveChanges();
                    return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = theoreticalDegree.CourseID });
                }

                return View(theoreticalDegree);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: TheoreticalDegrees/Edit/5
        [Authorize(Roles = "Admin,Manger,Supervisor")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheoreticalDegree theoreticalDegree = db.theoreticalDegree.Find(id);
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == theoreticalDegree.CourseID).FirstOrDefault();
            ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + CourseName.ID;
            if (theoreticalDegree == null)
            {
                return HttpNotFound();
            }
            return View(theoreticalDegree);
        }

        // POST: TheoreticalDegrees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TheoreticalDegree theoreticalDegree)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UpdateAndDeleteDeatails(theoreticalDegree, false);
                    db.Entry(theoreticalDegree).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = theoreticalDegree.CourseID });
                }
                return View(theoreticalDegree);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: TheoreticalDegrees/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TheoreticalDegree theoreticalDegree = db.theoreticalDegree.Find(id);
                var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == theoreticalDegree.CourseID).FirstOrDefault();
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + CourseName.ID;
                if (theoreticalDegree == null)
                {
                    return HttpNotFound();
                }
                return View(theoreticalDegree);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // POST: TheoreticalDegrees/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TheoreticalDegree theoreticalDegree = db.theoreticalDegree.Find(id);
            UpdateAndDeleteDeatails(theoreticalDegree, true);
            db.SaveChanges();
            return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = theoreticalDegree.CourseID });
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
