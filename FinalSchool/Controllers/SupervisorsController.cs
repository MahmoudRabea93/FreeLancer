using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalSchool.Models;
using FinalSchool.Models.UnitOfWork;
using Microsoft.AspNet.Identity;

namespace FinalSchool.Controllers
{
    public class SupervisorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        IUnitOfWork _Uow;
        public SupervisorsController()
        {
            _Uow = new UnitOfWork();
        }
        enum Types
        {
            Pratical = 1,    // عملي
            Theoretical = 2,   // نظري
            Pratical_Theoretical = 3,    // عملي/نظري

        }
      
        #region Functions
        public void CreateDeatails(Supervisors Obj)
        {
            Obj.IsDeleted = false;
            Obj.CreationDate = DateTime.Now;
            Obj.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public void UpdateAndDeleteDeatails(Supervisors Obj, bool Value)
        {
            Obj.IsDeleted = Value;
            Obj.UpdateDate = DateTime.Now;
            Obj.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public ActionResult Redirect(DaleelPracticalTraining daleelPracticalTraining, Supervisors Obj)
        {
            if (daleelPracticalTraining.Type == 1)
            {
                return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = Obj.CourseID });
            }
            else if (daleelPracticalTraining.Type == 2)
            {
                return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = Obj.CourseID });
            }
            else
            {
                return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = Obj.CourseID });
            }
        }
        #endregion

        #region Actions
        // GET: Supervisors
        [NonAction]
        public ActionResult Index()
        {
            return View(db.supervisor.Where(s => s.IsDeleted == false).ToList());
        }

        // GET: Supervisors/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisors supervisors = _Uow.supervisors.Display(id);
            if (supervisors == null)
            {
                return HttpNotFound();
            }
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == supervisors.CourseID).FirstOrDefault();
            if (CourseName.Type == 1)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + CourseName.ID;

            }
            else if (CourseName.Type == 2)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + CourseName.ID;
            }
            else
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + CourseName.ID;
            }

            var userUpdate = db.Users.Find(supervisors.UpdateUserId);
            if (userUpdate != null)
                ViewData["UpdatedName"] = userUpdate.UserName;
            else
                ViewData["UpdatedName"] = null;
            var UserCreate = db.Users.Find(supervisors.CreationUserId);
            if (UserCreate != null)
                ViewData["CreateName"] = UserCreate.UserName;
            else
                ViewData["CreateName"] = null;
            return View(supervisors);
        }

        // GET: Supervisors/Create
        public ActionResult Create(int id)
        {
            ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D => D.ID == id).ToList(), "ID", "CourseName");
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
            if (CourseName.Type == 1)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + CourseName.ID;

            }
            else if (CourseName.Type == 2)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + CourseName.ID;
            }
            else
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + CourseName.ID;
            }
            return View();
        }

        // POST: Supervisors/Create    
        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supervisors supervisors)
        {
            if (ModelState.IsValid)
            {
                CreateDeatails(supervisors);
                _Uow.supervisors.Add(supervisors);
                _Uow.Save();
                var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == supervisors.CourseID).FirstOrDefault();
                return Redirect(daleelPracticalTraining, supervisors);
            }
            return View(supervisors);
        }

        #region Commented
        //[Authorize(Roles = "Admin,Manger")]
        //public ActionResult CreatePractical(int id)
        //{
        //    ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D => D.ID == id).ToList(), "ID", "CourseName");
        //    return View();
        //}

        //// POST: Supervisors/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin,Manger")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreatePractical(Supervisors supervisors)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        supervisors.IsDeleted = false;
        //        supervisors.CreationDate = DateTime.Now;
        //        supervisors.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //        db.supervisor.Add(supervisors);
        //        db.SaveChanges();
        //        var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == supervisors.CourseID).FirstOrDefault();
        //        return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = supervisors.CourseID });

        //    }

        //    return View(supervisors);
        //}
        //[Authorize(Roles = "Admin,Manger")]
        //public ActionResult CreateTheoretical(int id)
        //{
        //    ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D => D.ID == id).ToList(), "ID", "CourseName");
        //    return View();
        //}

        //// POST: Supervisors/CreateTheoretical
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin,Manger")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateTheoretical(Supervisors supervisors)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        supervisors.IsDeleted = false;
        //        supervisors.CreationDate = DateTime.Now;
        //        supervisors.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //        db.supervisor.Add(supervisors);
        //        db.SaveChanges();
        //        var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == supervisors.CourseID).FirstOrDefault();
        //        return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = supervisors.CourseID });

        //    }

        //    return View(supervisors);
        //}
        //[Authorize(Roles = "Admin,Manger")]
        //public ActionResult CreatePraticalTheoretical(int id)
        //{
        //    ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D => D.ID ==id).ToList(), "ID", "CourseName");
        //    return View();
        //}

        //// POST: Supervisors/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin,Manger")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreatePraticalTheoretical(Supervisors supervisors)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        supervisors.IsDeleted = false;
        //        supervisors.CreationDate = DateTime.Now;
        //        supervisors.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //        db.supervisor.Add(supervisors);
        //        db.SaveChanges();
        //        var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == supervisors.CourseID).FirstOrDefault();
        //        return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = supervisors.CourseID });

        //    }

        //    return View(supervisors);
        //} 
        #endregion
        // GET: Supervisors/Edit/5
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisors supervisors = db.supervisor.Find(id);
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == supervisors.CourseID).FirstOrDefault();
            if (CourseName.Type == 1)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + CourseName.ID;

            }
            else if (CourseName.Type == 2)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + CourseName.ID;
            }
            else
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + CourseName.ID;
            }
            ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D => D.ID == supervisors.CourseID && D.IsDeleted == false).ToList(), "ID", "CourseName");
            if (supervisors == null)
            {
                return HttpNotFound();
            }

            return View(supervisors);
        }

        // POST: Supervisors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supervisors supervisors)
        {
            if (ModelState.IsValid)
            {
                UpdateAndDeleteDeatails(supervisors, false);
                _Uow.supervisors.Update(supervisors.ID, supervisors);
                _Uow.Save();
                var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == supervisors.CourseID).FirstOrDefault();
                return Redirect(daleelPracticalTraining, supervisors);
            }
            return View(supervisors);
        }

        // GET: Supervisors/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisors supervisors = db.supervisor.Find(id);
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == supervisors.CourseID).FirstOrDefault();
            if (CourseName.Type == 1)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + CourseName.ID;

            }
            else if (CourseName.Type == 2)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + CourseName.ID;
            }
            else
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + CourseName.ID;
            }
            if (supervisors == null)
            {
                return HttpNotFound();
            }
            return View(supervisors);
        }

        // POST: Supervisors/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supervisors supervisors = db.supervisor.Find(id);
            UpdateAndDeleteDeatails(supervisors, true);
            _Uow.supervisors.Delete(id, supervisors);
            _Uow.Save();
            var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == supervisors.CourseID).FirstOrDefault();
            return Redirect(daleelPracticalTraining, supervisors);
        }
    } 
    #endregion
}
