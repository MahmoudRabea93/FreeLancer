using FinalSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalSchool.Controllers
{
    public class UserRolesVMController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin,Manger")]
        public ActionResult Index()
        {
            try
            {
                var usersWithRoles = (from user in db.Users
                                      from userRole in user.Roles
                                      join role in db.Roles on userRole.RoleId equals
                                      role.Id
                                      select new UserRolesVM()
                                      {
                                          Username = user.UserName,
                                          Email = user.Email,
                                          PhoneNumber=user.PhoneNumber,
                                          Role = role.Name
                                      }).ToList();
                return View(usersWithRoles);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(ex.Message);
            }
        }
    }
}
        // GET: UserRolesVM/Details/5
     