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
    public class teachersController : Controller
    {
        IUnitOfWork _Uow;
        public teachersController()
        {
            _Uow = new UnitOfWork();
        }
        enum Types
        {
            Pratical = 1,    // عملي
            Theoretical = 2,   // نظري
            Pratical_Theoretical = 3,    // عملي/نظري
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Functions
        public void CreateDeatails(teacher Obj, int id)
        {
            Obj.CourseID = id;
            Obj.IsDeleted = false;
            Obj.CreationDate = DateTime.Now;
            Obj.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public void UpdateAndDeleteDeatails(teacher Obj, bool Value)
        {
            Obj.IsDeleted = Value;
            Obj.UpdateDate = DateTime.Now;
            Obj.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public ActionResult Redirect(DaleelPracticalTraining daleelPracticalTraining, teacher Teacher)
        {
            if (daleelPracticalTraining.Type == 1)
            {
                return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = Teacher.CourseID });
            }
            else if (daleelPracticalTraining.Type == 2)
            {
                return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = Teacher.CourseID });
            }
            else
            {
                return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = Teacher.CourseID });
            }
        }
        #endregion

        #region Actions
        // GET: teachers/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teacher teacherEntity = _Uow.Teacher.Display(id);
            teacher teacher = db.Teachers.Find(id);
            var Course = db.DaleelPracticalTrainings.Where(D => D.ID == teacher.CourseID).FirstOrDefault();
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
            if (teacherEntity == null)
            {
                return HttpNotFound();
            }
            var userUpdate = db.Users.Find(teacherEntity.UpdateUserId);
            if (userUpdate != null)
                ViewData["UpdatedName"] = userUpdate.UserName;
            else
                ViewData["UpdatedName"] = null;
            var UserCreate = db.Users.Find(teacherEntity.CreationUserId);
            if (UserCreate != null)
                ViewData["CreateName"] = UserCreate.UserName;
            else
                ViewData["CreateName"] = null;
            return View(teacherEntity);
        }

        #region Create
        // GET: teachers/Create
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult Create(int id)
        {
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
            ViewData["CourseNamePractical"] = CourseName.CourseName;
            //var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();     
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

        // POST: teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(teacher teacher, int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CreateDeatails(teacher, id);
                    _Uow.Teacher.Add(teacher);
                    _Uow.Save();
                    var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == teacher.CourseID).FirstOrDefault();
                    return Redirect(daleelPracticalTraining, teacher);

                }
                catch (Exception ex) // catches all exceptions
                {
                    return View(ex.Message);
                }
            }

            return View(teacher);
        }
        #endregion

        #region Commented
        //#region CreatePratical
        //// GET: teachers/Create
        //[Authorize(Roles = "Admin,Manger")]
        //public ActionResult CreatePratical(int id)
        //{
        //    var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
        //    ViewData["CourseNamePractical"] = CourseName.CourseName;
        //    return View();
        //}

        //// POST: teachers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin,Manger")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreatePratical(teacher teacher,int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {                 
        //            teacher.IsDeleted = false;
        //            teacher.CreationDate = DateTime.Now;
        //            teacher.CourseID = id;
        //            teacher.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //            db.Teachers.Add(teacher);
        //            db.SaveChanges();
        //            return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = teacher.CourseID });

        //        }
        //        catch (Exception ex) // catches all exceptions
        //        {
        //            return View(ex.Message);
        //        }
        //    }

        //    return View(teacher);
        //}
        //#endregion

        //#region CreateTheoretical
        //// GET: teachers/Create
        //[Authorize(Roles = "Admin,Manger")]
        //public ActionResult CreateTheoretical(int id)
        //{
        //    var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
        //    ViewData["CourseNameTheoretical"] = CourseName.CourseName;
        //    return View();
        //}

        //// POST: teachers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin,Manger")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateTheoretical(teacher teacher,int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            teacher.CourseID = id;
        //            teacher.IsDeleted = false;
        //            teacher.CreationDate = DateTime.Now;
        //            teacher.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //            db.Teachers.Add(teacher);
        //            db.SaveChanges();
        //            return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = teacher.CourseID });

        //        }
        //        catch (Exception ex) // catches all exceptions
        //        {
        //            return View(ex.Message);
        //        }
        //    }

        //    return View(teacher);
        //}
        //#endregion

        //#region CreatePraticalTheoretical
        //// GET: teachers/CreatePraticalTheoretical
        //[Authorize(Roles = "Admin,Manger")]
        //public ActionResult CreatePraticalTheoretical(int id )
        //{
        //    var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
        //    ViewData["CourseName"] = CourseName.CourseName;
        //    return View();
        //}

        //// POST: teachers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin,Manger")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreatePraticalTheoretical(teacher teacher,int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            teacher.IsDeleted = false;
        //            teacher.CreationDate = DateTime.Now;
        //            teacher.CourseID = id;
        //            teacher.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //            db.Teachers.Add(teacher);
        //            db.SaveChanges();
        //            return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = teacher.CourseID });

        //        }
        //        catch (Exception ex) // catches all exceptions
        //        {
        //            return View(ex.Message);
        //        }
        //    }

        //    return View(teacher);
        //}
        //#endregion
        // GET: teachers/Edit/5 
        #endregion

        [Authorize(Roles = "Admin,Manger,Supervisor")]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            var Course = db.DaleelPracticalTrainings.Where(D => D.ID == teacher.CourseID).FirstOrDefault();     
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
            return View(teacher);
        }

        // POST: teachers/Edit/5   
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    UpdateAndDeleteDeatails(teacher, false);
                    _Uow.Teacher.Update(teacher.ID, teacher);
                    _Uow.Save();
                    var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == teacher.CourseID).FirstOrDefault();
                    return Redirect(daleelPracticalTraining, teacher);

                }
                return View(teacher);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: teachers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            var Course = db.DaleelPracticalTrainings.Where(D => D.ID == teacher.CourseID).FirstOrDefault();
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
            return View(teacher);
        }
        [Authorize(Roles = "Admin")]
        // POST: teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            teacher teacher = db.Teachers.Find(id);
            UpdateAndDeleteDeatails(teacher, true);
            _Uow.Teacher.Delete(id, teacher);
            _Uow.Save();
            var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == teacher.CourseID).FirstOrDefault();
            return Redirect(daleelPracticalTraining, teacher);



        }
    }

    #endregion
}
