using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FinalSchool.Models;
using Microsoft.AspNet.Identity;

namespace FinalSchool.Controllers
{
    public class DaleelPracticalTrainingsController : Controller
    {
        enum Types
        {
            Pratical = 1,    // عملي
            Theoretical =2,   // نظري
            Pratical_Theoretical = 3,    // عملي/نظري
           
        }
        private ApplicationDbContext db = new ApplicationDbContext();
        public int LastCouesAdd;
        // GET: DaleelPracticalTrainings
        public ActionResult NotAdded()
        {
            return View();
        }
        #region تقرير 
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaleelPracticalTraining daleelPracticalTraining = db.DaleelPracticalTrainings.Find(id);
            if (daleelPracticalTraining == null)
            {
                return HttpNotFound();
            }
            if (daleelPracticalTraining.Type == 1)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + daleelPracticalTraining.ID;

            }
            else if (daleelPracticalTraining.Type == 2)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + daleelPracticalTraining.ID;
            }
            else
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + daleelPracticalTraining.ID;

            }

            var userUpdate = db.Users.Find(daleelPracticalTraining.UpdateUserId);
            if (userUpdate != null)
                ViewData["UpdatedName"] = userUpdate.UserName;
            else
                ViewData["UpdatedName"] = null;
            var UserCreate = db.Users.Find(daleelPracticalTraining.CreationUserId);
            if (UserCreate != null)
                ViewData["CreateName"] = UserCreate.UserName;
            else
                ViewData["CreateName"] = null;
            return View(daleelPracticalTraining);
        }
        #endregion

        //المواد النظري
        [Authorize]
        public ActionResult CoursesListTheoretical()
        {
            return View(db.DaleelPracticalTrainings.Where(C=>C.Type== (int)Types.Theoretical && C.IsDeleted == false).ToList());
        }
        //المواد العملي
        [Authorize]
        public ActionResult CoursesPracticalList()
        {
            return View(db.DaleelPracticalTrainings.Where(C => C.Type == (int)Types.Pratical && C.IsDeleted == false).ToList());
        }
        //المواد العملي النظري
        [Authorize]
        public ActionResult CoursesPraticalTheoreticalList()
        {
            return View(db.DaleelPracticalTrainings.Where(C => C.Type == (int)Types.Pratical_Theoretical && C.IsDeleted == false).ToList());
        }

        #region تفاصيل عملي نظري
        [Authorize]
        public ActionResult DetailsPraticalTheoretical(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int item = db.CourseDistributions.Where(S => S.IsDeleted == false && S.CourseID == id).Count();
            Session["RowcountPraticalTheoretical"] = item;
            ViewData["Courses"] = db.DaleelPracticalTrainings.Find(id);
            ViewData["Degree"] = db.Degree.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["Teacher"] = db.Teachers.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["supervisors"] = db.supervisor.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["Ratings"] = db.Ratings.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["WeekCourses"] = db.CourseDistributions.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["CoursesPracticalList"] = db.DaleelPracticalTrainings.Where(C => C.Type == (int)Types.Pratical_Theoretical && C.IsDeleted == false).ToList();
            DaleelPracticalTraining daleelPracticalTraining = db.DaleelPracticalTrainings.Find(id);
            if (daleelPracticalTraining == null)
            {
                return View("NotAdded");
            }
            return View(daleelPracticalTraining);

        }
        #endregion

        #region تفاصيل عملي
        [Authorize]
        public ActionResult PracticalDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int item = db.CourseDistributions.Where(S => S.IsDeleted == false && S.CourseID == id).Count();
            Session["RowcountPractical"] = item;
            ViewData["Courses"] = db.DaleelPracticalTrainings.Find(id);
            ViewData["Degree"] = db.Degree.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["Teacher"] = db.Teachers.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["supervisors"] = db.supervisor.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["practicalDegrees"] = db.practicalDegree.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["Evaluation"] = db.Evaluations.Where(E => E.IsDeleted == false && E.Practical == true && E.CourseID == id).ToList();
            ViewData["WeekCourses"] = db.CourseDistributions.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["CoursesList"] = db.DaleelPracticalTrainings.Where(C => C.Type == (int)Types.Pratical && C.IsDeleted == false).ToList();
            DaleelPracticalTraining daleelPracticalTraining = db.DaleelPracticalTrainings.Find(id);
            if (daleelPracticalTraining == null)
            {
                return View("NotAdded");
            }
            return View(daleelPracticalTraining);
        }
        #endregion

        #region تفاصيل نظري
        [Authorize]
        public ActionResult TheoreticalDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int item = db.CourseDistributions.Where(S => S.IsDeleted == false && S.CourseID == id).Count();
            Session["RowcountTheoretical"] = item;
            ViewData["Courses"] = db.DaleelPracticalTrainings.Find(id);
            ViewData["Degree"] = db.Degree.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["Teacher"] = db.Teachers.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["supervisors"] = db.supervisor.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["theoreticalDegrees"] = db.theoreticalDegree.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["Evaluation"] = db.Evaluations.Where(E => E.IsDeleted == false && E.Practical == false && E.CourseID == id).ToList();
            ViewData["WeekCourses"] = db.CourseDistributions.Where(S => S.IsDeleted == false && S.CourseID == id).ToList();
            ViewData["CoursesList"] = db.DaleelPracticalTrainings.Where(C => C.Type == (int)Types.Theoretical && C.IsDeleted == false).ToList();
            DaleelPracticalTraining daleelPracticalTraining = db.DaleelPracticalTrainings.Find(id);
            if (daleelPracticalTraining == null)
            {
                return View("NotAdded");
            }
            return View(daleelPracticalTraining);
        } 
        #endregion

        #region أضاقة مقرر نظري
        // GET: DaleelPracticalTrainings/Create
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult CreateTheoretical()
        {
            return View();
        }

        // POST: DaleelPracticalTrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTheoretical(DaleelPracticalTraining daleelPracticalTraining)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    daleelPracticalTraining.CreationDate = DateTime.Now;
                    daleelPracticalTraining.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    daleelPracticalTraining.IsDeleted = false;
                    daleelPracticalTraining.Type = (int)Types.Theoretical;
                    bool found = db.DaleelPracticalTrainings.Any(D => D.CourseName == daleelPracticalTraining.CourseName && D.IsDeleted == false && D.Specialty == daleelPracticalTraining.Specialty);
                    if (found)
                    {
                        ModelState.AddModelError("", "هذا المقرر موجود من قبل يرجي تغيير اسم المقرر");
                        return View();
                    }

                    db.DaleelPracticalTrainings.Add(daleelPracticalTraining);
                    db.SaveChanges();
                    return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = daleelPracticalTraining.ID });

                }

                return View(daleelPracticalTraining);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        #endregion

        #region أضافة مقرر عملي
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult CreatePratical()
        {
            return View();
        }
        // POST: DaleelPracticalTrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePratical(DaleelPracticalTraining daleelPracticalTraining)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    daleelPracticalTraining.CreationDate = DateTime.Now;
                    daleelPracticalTraining.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    daleelPracticalTraining.IsDeleted = false;
                    daleelPracticalTraining.Type = (int)Types.Pratical;
                    bool found = db.DaleelPracticalTrainings.Any(D => D.CourseName == daleelPracticalTraining.CourseName && D.IsDeleted == false && D.Specialty == daleelPracticalTraining.Specialty);
                    if (found)
                    {
                        ModelState.AddModelError("", "هذا المقرر موجود من قبل يرجي تغيير اسم المقرر");
                        return View();
                    }
                    db.DaleelPracticalTrainings.Add(daleelPracticalTraining);
                    db.SaveChanges();
                    return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = daleelPracticalTraining.ID });

                }

                return View(daleelPracticalTraining);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }
        #endregion

        #region أضاقة مقرر عملي / نظري
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult CreatePraticalTheoretical()
        {
            return View();
        }
        // POST: DaleelPracticalTrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePraticalTheoretical(DaleelPracticalTraining daleelPracticalTraining)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    daleelPracticalTraining.CreationDate = DateTime.Now;
                    daleelPracticalTraining.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    daleelPracticalTraining.IsDeleted = false;
                    daleelPracticalTraining.Type = (int)Types.Pratical_Theoretical;
                    bool found = db.DaleelPracticalTrainings.Any(D => D.CourseName == daleelPracticalTraining.CourseName && D.IsDeleted == false && D.Specialty == daleelPracticalTraining.Specialty);
                    if (found)
                    {
                        ModelState.AddModelError("", "هذا المقرر موجود من قبل يرجي تغيير اسم المقرر");
                        return View();
                    }
                    db.DaleelPracticalTrainings.Add(daleelPracticalTraining);
                    db.SaveChanges();
                    return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = daleelPracticalTraining.ID });

                }

                return View(daleelPracticalTraining);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }
        #endregion

        #region تعديل المقرر
        // GET: DaleelPracticalTrainings/Edit/5
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaleelPracticalTraining daleelPracticalTraining = db.DaleelPracticalTrainings.Find(id);
            if (daleelPracticalTraining.Type == 1)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + daleelPracticalTraining.ID;
            }
            else if (daleelPracticalTraining.Type == 2)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + daleelPracticalTraining.ID;
            }
            else
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + daleelPracticalTraining.ID;
            }
            if (daleelPracticalTraining == null)
            {
                return HttpNotFound();
            }
            return View(daleelPracticalTraining);
        }

        // POST: DaleelPracticalTrainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DaleelPracticalTraining daleelPracticalTraining)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    daleelPracticalTraining.UpdateDate = DateTime.Now;
                    daleelPracticalTraining.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    daleelPracticalTraining.IsDeleted = false;
                    db.Entry(daleelPracticalTraining).State = EntityState.Modified;
                    db.SaveChanges();
                    if (daleelPracticalTraining.Type == 1)
                    {
                        return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = daleelPracticalTraining.ID });
                    }
                    else if (daleelPracticalTraining.Type == 2)
                    {
                        return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = daleelPracticalTraining.ID });
                    }
                    else
                    {
                        return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = daleelPracticalTraining.ID });
                    }
                }
                return View(daleelPracticalTraining);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        } 
        #endregion

        // GET: DaleelPracticalTrainings/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaleelPracticalTraining daleelPracticalTraining = db.DaleelPracticalTrainings.Find(id);
            if (daleelPracticalTraining.Type == 1)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + daleelPracticalTraining.ID;

            }
            else if (daleelPracticalTraining.Type == 2)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + daleelPracticalTraining.ID;
            }
            else
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + daleelPracticalTraining.ID;

            }
            if (daleelPracticalTraining == null)
            {
                return HttpNotFound();
            }
            return View(daleelPracticalTraining);
        }

        // POST: DaleelPracticalTrainings/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            DaleelPracticalTraining daleelPracticalTraining = db.DaleelPracticalTrainings.Find(id);
            //db.DaleelPracticalTrainings.Remove(daleelPracticalTraining);
            daleelPracticalTraining.UpdateDate = DateTime.Now;
            daleelPracticalTraining.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            daleelPracticalTraining.IsDeleted = true;
            db.SaveChanges();
            return RedirectToAction("IndexTuplePraticalTheoretical", "Coursedistributions");
           
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
