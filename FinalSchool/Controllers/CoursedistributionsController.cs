using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalSchool.Models;
using Microsoft.AspNet.Identity;

namespace FinalSchool.Controllers
{
    public class CoursedistributionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string stringWeek(int weekNum)
        {
                if (weekNum == 1)
                    return "الاول";
                else if (weekNum == 2)
                    return "الثاني";
                else if (weekNum == 3)
                    return "الثالث";
                else if (weekNum == 4)
                    return "الرابع";
                else if (weekNum == 5)
                    return "الخامس";
                else if (weekNum == 6)
                    return "السادس";
                else if (weekNum == 7)
                    return "السابع";
                else if (weekNum == 8)
                    return "الثامن";
                else if (weekNum == 9)
                    return "التاسع";
                else if (weekNum == 10)
                    return "العاشر";
                else if (weekNum == 11)
                    return "الحادي عشر";
                else if (weekNum == 12)
                    return "الثاني عشر";
                else if (weekNum == 13)
                    return "الثالث عشر";
                else if (weekNum == 14)
                    return "الرابع عشر";
                else if (weekNum == 15)
                    return "الخامس عشر";
                else if (weekNum == 16)
                    return "السادس عشر";
                else if (weekNum == 17)
                    return "السابع عشر";
                else if (weekNum == 18)
                    return "الثامن عشر";
                else if (weekNum == 19)
                    return "التاسع عشر";
                else if (weekNum == 20)
                    return "العشرون";
                else
                    return "";

        }
        enum Types
        {
            Pratical = 1,    // عملي
            Theoretical = 2,   // نظري
            Pratical_Theoretical = 3,    // عملي/نظري

        }

        #region Functions
        public void CreateDeatails(Coursedistribution Obj)
        {
            Obj.IsDeleted = false;
            Obj.CreationDate = DateTime.Now;
            Obj.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public void UpdateAndDeleteDeatails(Coursedistribution Obj, bool Value)
        {
            Obj.IsDeleted = Value;
            Obj.UpdateDate = DateTime.Now;
            Obj.UpdateUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public ActionResult Redirect(DaleelPracticalTraining daleelPracticalTraining, Coursedistribution Obj)
        {
            if (daleelPracticalTraining.Type == 1)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + Obj.CourseID;
                return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = Obj.CourseID });
              
            }
            else if (daleelPracticalTraining.Type == 2)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + Obj.CourseID;
                return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = Obj.CourseID });
            }
            else
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + Obj.CourseID;
                return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = Obj.CourseID });
            }
        }
        #endregion

        #region Actions
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult ListOfStudent()
        {
            ViewData["RoleName"] = db.Roles.ToList();
            return View(db.Users.ToList());
        }
        [Authorize]
        public ActionResult IndexTupleTheoretical()
        {
            var FirstCouersAdd = db.DaleelPracticalTrainings.Where(D => D.Type == 2 && D.IsDeleted == false).FirstOrDefault();
            if (FirstCouersAdd==null)
            {
                return RedirectToAction("NotAdded", "DaleelPracticalTrainings");
            }
            #region Old
            //ViewData["Courses"] = db.DaleelPracticalTrainings.Find(LastCouersAdd.ID);
            //ViewData["CoursesList"] = db.DaleelPracticalTrainings.Where(C => C.Type == (int)Types.Theoretical && C.IsDeleted == false).ToList();
            //ViewData["DegreeVB"] = db.Degree.Where(D => D.CourseID == LastCouersAdd.ID && D.IsDeleted == false).ToList();
            //ViewData["Evaluation"] = db.Evaluations.Where(E => E.IsDeleted == false && E.Practical == false && E.CourseID == LastCouersAdd.ID).ToList();
            //ViewData["supervisors"] = db.supervisor.Where(S => S.IsDeleted == false && S.CourseID == LastCouersAdd.ID).ToList();
            //ViewData["theoreticalDegrees"] = db.theoreticalDegree.Where(S => S.IsDeleted == false && S.CourseID == LastCouersAdd.ID).ToList();
            //int item = db.CourseDistributions.Where(s => s.CourseID == LastCouersAdd.ID && s.IsDeleted == false).Count();
            //Session["Theoreticalcount"] = item;
            //var tupleModel1 = new Tuple
            //    <List<Coursedistribution>,
            //    List<teacher>,
            //    List<Rating> >
            //    (
            //    db.CourseDistributions.Where(D => D.CourseID == LastCouersAdd.ID && D.IsDeleted == false).ToList(),
            //    db.Teachers.Where(D => D.CourseID == LastCouersAdd.ID && D.IsDeleted == false).ToList(),
            //    db.Ratings.Where(D => D.CourseID == LastCouersAdd.ID && D.IsDeleted == false).ToList()
            //    );
            //return View(tupleModel1); 
            #endregion
            return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = FirstCouersAdd.ID });
        }
        [Authorize]
        public ActionResult IndexTuplePratical()
        {
            var FirstCouersAdd = db.DaleelPracticalTrainings.Where(D => D.Type == 1 && D.IsDeleted == false).FirstOrDefault();
            if (FirstCouersAdd == null)
            {
                return RedirectToAction("NotAdded", "DaleelPracticalTrainings");
            }
            #region Old
            //ViewData["Courses"] = db.DaleelPracticalTrainings.Find(LastCouersAdd.ID);
            //ViewData["CoursesList"] = db.DaleelPracticalTrainings.Where(C => C.Practical == false).ToList();
            //ViewData["DegreeVB"] = db.Degree.Where(D => D.CourseID == LastCouersAdd.ID && D.IsDeleted == false).ToList();
            //ViewData["Evaluation"] = db.Evaluations.Where(E => E.IsDeleted == false && E.Practical == true && E.CourseID == LastCouersAdd.ID).ToList();
            //ViewData["practicalDegrees"] = db.practicalDegree.Where(S => S.IsDeleted == false && S.CourseID == LastCouersAdd.ID).ToList();

            //ViewData["supervisors"] = db.supervisor.Where(S => S.IsDeleted == false && S.CourseID == LastCouersAdd.ID).ToList();
            //int item = db.CourseDistributions.Where(s => s.CourseID == LastCouersAdd.ID && s.IsDeleted == false).Count();
            //Session["Praticalcount"] = item;
            //var tupleModel1 = new Tuple
            //    <List<Coursedistribution>,
            //    List<teacher>,
            //    List<Rating>>
            //    (
            //    db.CourseDistributions.Where(D => D.CourseID == LastCouersAdd.ID && D.IsDeleted == false).ToList(),
            //    db.Teachers.Where(D => D.CourseID == LastCouersAdd.ID && D.IsDeleted == false).ToList(),
            //    db.Ratings.Where(D => D.CourseID == LastCouersAdd.ID && D.IsDeleted == false).ToList()
            //    );
            //return View(tupleModel1); 
            #endregion
            return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = FirstCouersAdd.ID });
        }
        [Authorize]
        public ActionResult IndexTuplePraticalTheoretical()
        {
            var FirstCouersAdd = db.DaleelPracticalTrainings.Where(D => D.Type == 3 && D.IsDeleted == false).FirstOrDefault();
            if (FirstCouersAdd == null)
            {
                return RedirectToAction("NotAdded", "DaleelPracticalTrainings");
            }
            #region Old
            //ViewData["Courses"] = db.DaleelPracticalTrainings.Find(LastCouersAdd.ID);     
            //ViewData["CoursesPracticalList"] = db.DaleelPracticalTrainings.Where(C => C.Practical == true).ToList();
            //ViewData["DegreeVB"] = db.Degree.Where(D=>D.CourseID== LastCouersAdd.ID && D.IsDeleted == false).ToList();
            //int item = db.CourseDistributions.Where(s=>s.CourseID== LastCouersAdd.ID && s.IsDeleted == false).Count();
            //ViewData["supervisors"] = db.supervisor.Where(S => S.IsDeleted == false && S.CourseID == LastCouersAdd.ID).ToList();
            //Session["PraticalTheoreticalcount"] = item;        
            //var tupleModel1 = new Tuple
            //    <List<Coursedistribution>,
            //    List<teacher>,
            //    List<Rating> >

            //    (
            //    db.CourseDistributions.Where(D=>D.CourseID == LastCouersAdd.ID && D.IsDeleted == false).ToList(),
            //    db.Teachers.Where(D => D.CourseID == LastCouersAdd.ID && D.IsDeleted == false).ToList(),
            //    db.Ratings.Where(D => D.CourseID == LastCouersAdd.ID && D.IsDeleted == false).ToList()
            //    );
            //return View(tupleModel1); 
            #endregion
            return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = FirstCouersAdd.ID });
        }

        // GET: Coursedistributions/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coursedistribution coursedistribution = db.CourseDistributions.Find(id);
            if (coursedistribution == null)
            {
                return HttpNotFound();
            }
            var userUpdate = db.Users.Find(coursedistribution.UpdateUserId);
            if (userUpdate != null)
                ViewData["UpdatedName"] = userUpdate.UserName;
            else
                ViewData["UpdatedName"] = null;
            var UserCreate = db.Users.Find(coursedistribution.CreationUserId);
            if (UserCreate != null)
                ViewData["CreateName"] = UserCreate.UserName;
            else
                ViewData["CreateName"] = null;

            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == coursedistribution.CourseID).FirstOrDefault();
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


            return View(coursedistribution);
        }

        // GET: Coursedistributions/Create

        [Authorize(Roles = "Admin,Manger")]
        public ActionResult Create(int id)
        {
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
            ViewData["CourseNamePractical"] = CourseName.CourseName;
            var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
            if (daleelPracticalTraining.Type == 1)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/PracticalDetails/" + CourseName.ID;
             
            }
            else if (daleelPracticalTraining.Type == 2)
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/TheoreticalDetails/" + CourseName.ID;              
            }
            else
            {
                ViewData["LastPage"] = "/DaleelPracticalTrainings/DetailsPraticalTheoretical/" + CourseName.ID;
             
            }
            return View();
            
        }
        // POST: Coursedistributions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598. 
        [Authorize(Roles = "Admin,Manger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Coursedistribution coursedistribution, int WeekFrom, int WeekTo, string HjrDateTextBox, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (WeekFrom != 0 && WeekTo != 0)
                    {
                        int start = WeekFrom;
                        int End = WeekTo;
                        for (int i = start; i <= End; i++)
                        {
                            coursedistribution.Content = null;
                            coursedistribution.Week = stringWeek(i);
                            coursedistribution.Weekdate = null;
                            coursedistribution.IsDeleted = false;
                            coursedistribution.CourseID = id;
                            coursedistribution.CreationDate = DateTime.Now;
                            coursedistribution.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                            db.CourseDistributions.Add(coursedistribution);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        coursedistribution.Weekdate = HjrDateTextBox;
                        coursedistribution.IsDeleted = false;
                        coursedistribution.CourseID = id;
                        coursedistribution.CreationDate = DateTime.Now;
                        coursedistribution.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                        db.CourseDistributions.Add(coursedistribution);
                        db.SaveChanges();
                    }
                    var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == coursedistribution.CourseID).FirstOrDefault();
                    return Redirect(daleelPracticalTraining, coursedistribution);

                }
                return View(coursedistribution);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        #region Commented
        //#region CreatePractical
        //[Authorize(Roles = "Admin,Manger")]
        //public ActionResult CreatePractical(int id)
        //{
        //    var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
        //    ViewData["CourseNamePractical"] = CourseName.CourseName;
        //    return View();
        //}

        //// POST: Coursedistributions/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598. 
        //[Authorize(Roles = "Admin,Manger")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreatePractical(Coursedistribution coursedistribution, int WeekFrom, int WeekTo, string HjrDateTextBox, int id)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {

        //            if (WeekFrom != 0 && WeekTo !=0)
        //            {
        //                int start = WeekFrom;
        //                int End = WeekTo;
        //                for (int i = start; i <= End; i++)                           
        //                {
        //                    coursedistribution.Content = null;
        //                    coursedistribution.Week = stringWeek(i);
        //                    coursedistribution.Weekdate = null;
        //                    coursedistribution.IsDeleted = false;
        //                    coursedistribution.CourseID = id;
        //                    coursedistribution.CreationDate = DateTime.Now;
        //                    coursedistribution.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //                    db.CourseDistributions.Add(coursedistribution);
        //                    db.SaveChanges();
        //                }
        //            }
        //            else
        //            {
        //                coursedistribution.Weekdate = HjrDateTextBox;
        //                coursedistribution.IsDeleted = false;
        //                coursedistribution.CourseID = id;
        //                coursedistribution.CreationDate = DateTime.Now;
        //                coursedistribution.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //                db.CourseDistributions.Add(coursedistribution);
        //                db.SaveChanges();
        //            }
        //            var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == coursedistribution.CourseID).FirstOrDefault();
        //            return RedirectToAction("PracticalDetails", "DaleelPracticalTrainings", new { id = coursedistribution.CourseID });

        //        }


        //        return View(coursedistribution);

        //    }
        //    catch (Exception ex) // catches all exceptions
        //    {
        //        return View(ex.Message);
        //    }
        //}
        //#endregion

        //#region CreateTheoretical
        //[Authorize(Roles = "Admin,Manger")]
        //public ActionResult CreateTheoretical(int id)
        //{
        //    var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
        //    ViewData["CourseNameTheoretical"] = CourseName.CourseName;
        //    return View();
        //}

        //// POST: Coursedistributions/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598. 
        //[Authorize(Roles = "Admin,Manger")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateTheoretical(Coursedistribution coursedistribution,int WeekFrom,int WeekTo, string HjrDateTextBox,int id)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (WeekFrom != 0 && WeekTo != 0)
        //            {
        //                int start = WeekFrom;
        //                int End = WeekTo;
        //                for (int i = start; i <= End; i++)
        //                {
        //                    coursedistribution.Content = null;
        //                    coursedistribution.Week = stringWeek(i);
        //                    coursedistribution.Weekdate = null;
        //                    coursedistribution.CourseID = id;
        //                    coursedistribution.IsDeleted = false;
        //                    coursedistribution.CreationDate = DateTime.Now;
        //                    coursedistribution.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //                    db.CourseDistributions.Add(coursedistribution);
        //                    db.SaveChanges();
        //                }
        //            }
        //            else
        //            {
        //                coursedistribution.IsDeleted = false;
        //                coursedistribution.CreationDate = DateTime.Now;
        //                coursedistribution.CourseID = id;
        //                coursedistribution.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //                coursedistribution.Weekdate = HjrDateTextBox;
        //                db.CourseDistributions.Add(coursedistribution);
        //                db.SaveChanges();
        //            }
        //            var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == coursedistribution.CourseID).FirstOrDefault();
        //            return RedirectToAction("TheoreticalDetails", "DaleelPracticalTrainings", new { id = coursedistribution.CourseID });

        //        }


        //        return View(coursedistribution);

        //    }
        //    catch (Exception ex) // catches all exceptions
        //    {
        //        return View(ex.Message);
        //    }
        //}
        //#endregion

        //#region CreatePraticalTheoretical
        //[Authorize(Roles = "Admin,Manger")]

        //public ActionResult CreatePraticalTheoretical(int id)
        //{
        //    var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
        //    ViewData["CourseName"] = CourseName.CourseName;
        //    return View();
        //}

        //// POST: Coursedistributions/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598. 
        //[Authorize(Roles = "Admin,Manger")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreatePraticalTheoretical(Coursedistribution coursedistribution, int WeekFrom, int WeekTo, string HjrDateTextBox,int id)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (WeekFrom != 0 && WeekTo != 0)
        //            {
        //                int start = WeekFrom;
        //                int End = WeekTo;
        //                for (int i = start; i <= End; i++)
        //                {
        //                    coursedistribution.Content = null;
        //                    coursedistribution.Week = stringWeek(i);
        //                    coursedistribution.Weekdate = null;
        //                    coursedistribution.CourseID = id;
        //                    coursedistribution.IsDeleted = false;
        //                    coursedistribution.CreationDate = DateTime.Now;
        //                    coursedistribution.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //                    db.CourseDistributions.Add(coursedistribution);
        //                    db.SaveChanges();
        //                }
        //            }
        //            else
        //            {
        //                coursedistribution.IsDeleted = false;
        //                coursedistribution.CreationDate = DateTime.Now;
        //                coursedistribution.CourseID = id;
        //                coursedistribution.CreationUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //                coursedistribution.Weekdate = HjrDateTextBox;
        //                db.CourseDistributions.Add(coursedistribution);
        //                db.SaveChanges();
        //            }
        //            var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == coursedistribution.CourseID).FirstOrDefault();
        //            return RedirectToAction("DetailsPraticalTheoretical", "DaleelPracticalTrainings", new { id = coursedistribution.CourseID });

        //        }


        //        return View(coursedistribution);

        //    }
        //    catch (Exception ex) // catches all exceptions
        //    {
        //        return View(ex.Message);
        //    }
        //}
        //#endregion 
        #endregion

        [Authorize(Roles = "Admin,Manger,Supervisor")]
        // GET: Coursedistributions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coursedistribution coursedistribution = db.CourseDistributions.Find(id);
            var Daleel = db.DaleelPracticalTrainings.Where(D => D.ID == coursedistribution.CourseID && D.IsDeleted == false).FirstOrDefault();
            ViewData["HjrDate"] = coursedistribution.Weekdate;
            ViewBag.CoursesName = new SelectList(db.DaleelPracticalTrainings.Where(D => D.ID == coursedistribution.CourseID && D.IsDeleted == false).ToList(), "ID", "CourseName");
            if (coursedistribution == null)
            {
                return HttpNotFound();
            }
            var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == coursedistribution.CourseID).FirstOrDefault();
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
            return View(coursedistribution);
        }
        // POST: Coursedistributions/Edit/5    
        [Authorize(Roles = "Admin,Manger,Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Coursedistribution coursedistribution, string HjrDateTextBox)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UpdateAndDeleteDeatails(coursedistribution, false);
                    coursedistribution.Weekdate = HjrDateTextBox;
                    db.Entry(coursedistribution).State = EntityState.Modified;
                    db.SaveChanges();
                    var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == coursedistribution.CourseID && D.IsDeleted == false).FirstOrDefault();
                    return Redirect(daleelPracticalTraining, coursedistribution);
                }
                return View(coursedistribution);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }

        // GET: Coursedistributions/Delete/5
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coursedistribution coursedistribution = db.CourseDistributions.Find(id);
            if (coursedistribution == null)
            {
                return HttpNotFound();
            }
            var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == id).FirstOrDefault();
            var CourseName = db.DaleelPracticalTrainings.Where(D => D.ID == coursedistribution.CourseID).FirstOrDefault();
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
            return View(coursedistribution);
        }

        // POST: Coursedistributions/Delete/5
        [Authorize(Roles = "Admin,Manger")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Coursedistribution coursedistribution = db.CourseDistributions.Find(id);
                UpdateAndDeleteDeatails(coursedistribution, true);
                db.SaveChanges();
                var daleelPracticalTraining = db.DaleelPracticalTrainings.Where(D => D.ID == coursedistribution.CourseID && D.IsDeleted == false).FirstOrDefault();
                return Redirect(daleelPracticalTraining, coursedistribution);
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
    #endregion
}
