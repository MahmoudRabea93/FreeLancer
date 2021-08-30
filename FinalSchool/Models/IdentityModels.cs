using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinalSchool.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<DaleelPracticalTraining> DaleelPracticalTrainings { get; set; }
        public DbSet<Coursedistribution> CourseDistributions { get; set; }
        public DbSet<teacher> Teachers { get; set; }
        public DbSet<Degree> Degree { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Supervisors> supervisor { get; set; }
        public DbSet<TheoreticalDegree> theoreticalDegree { get; set; }
        public DbSet<PracticalDegree> practicalDegree { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
       

    }
}