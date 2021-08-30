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
    public class EvaluationsController : Controller
    {
        enum Types
        {
            Pratical = 1,    // عملي
            Theoretical = 2,   // نظري
            Pratical_Theoretical = 3,    // عملي/نظري

        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Evaluations
        [NonAction]
        public ActionResult Index()
        {
            return View(db.Evaluations.ToList());
        }

        // GET: Evaluations/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluations.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
           
            var Course = db.DaleelPracticalTrainings.Where(D => D.ID == evaluation.CourseID).FirstOrDefault();
            if (Course.Type == 1)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + Course.ID;

            }
            else if (Course.Type == 2)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + Course.ID;
            }
            else
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + Course.ID;

            }
            var userUpdate = db.Users.Find(evaluation.UpdateUserId);
            if (userUpdate != null)
                ViewData["UpdatedName"] = userUpdate.UserName;
            else
                ViewData["UpdatedName"] = null;
            var UserCreate = db.Users.Find(evaluation.CreationUserId);
            if (UserCreate != null)
                ViewData["CreateName"] = UserCreate.UserName;
            else
                ViewData["CreateName"] = null;
            return View(evaluation);
        }

        // GET: Evaluations/Create
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult CreateTheoretical(int id)
        {  
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
            ViewData["CourseNameTheoretical"] = CourseName.CourseName;          
            ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + CourseName.ID;   
            return View();
        }

        // POST: Evaluations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTheoretical(Evaluation evaluation, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    evaluation.IsDeleted = false;
                    evaluation.Practical = false;
                    evaluation.CourseID = id;
                    evaluation.CreationDate = DateTime.Now;
                    evaluation.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    db.Evaluations.Add(evaluation);
                    db.SaveChanges();
                    return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = evaluation.CourseID });
                }

                return View(evaluation);

            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: Evaluations/Create
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult CreatePractical(int id)
        {     
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
            ViewData["CourseNamePractical"] = CourseName.CourseName;
            ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + CourseName.ID;
            return View();
        }

        // POST: Evaluations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePractical(Evaluation evaluation,int id)
        {
        try
        {
            if (ModelState.IsValid)
            {
                evaluation.IsDeleted = false;
                evaluation.Practical = true;
                evaluation.CreationDate = DateTime.Now;
                evaluation.CourseID = id;
                evaluation.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();             
                db.Evaluations.Add(evaluation);
                db.SaveChanges();
                return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = evaluation.CourseID });
            }

            return View(evaluation);
        }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
    }
}

        // GET: Evaluations/Edit/5
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        public ActionResult Edit(int? id)
        {
        try{ 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluations.Find(id);
            var Daleel = db.DaleelPracticalTrainings.Where(D => D.ID == evaluation.CourseID && D.IsDeleted == false).FirstOrDefault();
            ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D => D.ID == evaluation.CourseID).ToList(), "ID", "CourseName");
            if (evaluation == null)
            {
                return HttpNotFound();
            }
                var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == evaluation.CourseID).FirstOrDefault();
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
                return View(evaluation);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // POST: Evaluations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Evaluation evaluation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    evaluation.IsDeleted = false;
                    evaluation.UpdateDate = DateTime.Now;
                    evaluation.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    db.Entry(evaluation).State = EntityState.Modified;
                    db.SaveChanges();
                    var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == evaluation.CourseID).FirstOrDefault();
                    if (daleelPracticalTraining.Type == 1)
                    {
                        return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = evaluation.CourseID });
                    }
                    else if (daleelPracticalTraining.Type == 2)
                    {
                        return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = evaluation.CourseID });
                    }
                    else
                    {
                        return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = evaluation.CourseID });
                    }
                }
                return View(evaluation);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
}

        // GET: Evaluations/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluations.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == evaluation.CourseID).FirstOrDefault();
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
            return View(evaluation);
        }

        // POST: Evaluations/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Evaluation evaluation = db.Evaluations.Find(id);
                evaluation.IsDeleted = true;
                evaluation.UpdateDate = DateTime.Now;
                evaluation.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                // db.Evaluations.Remove(evaluation);
                db.SaveChanges();
                var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == evaluation.CourseID).FirstOrDefault();
                if (daleelPracticalTraining.Type == 1)
                {
                    return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = evaluation.CourseID });
                }
                else if (daleelPracticalTraining.Type == 2)
                {
                    return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = evaluation.CourseID });
                }
                else
                {
                    return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = evaluation.CourseID });
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
