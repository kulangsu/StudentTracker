using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StudentTracker.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentTracker.Models
{
    // You can add User data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public int SID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime LastLogin { get; set; }
        public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }

        public virtual ICollection<Course> Courses { get; set; }

    }

    //definition Quarter Year table
    public class QuarterYear
    {
        [Key]
        public int ID { get; set; }
        public int Year { get; set; }
        public string Quarter { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }

    //definition Classes table
    public class Course
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int QuarterYearID { get; set; }
        public virtual QuarterYear QuarterYear { get; set; }

        //public virtual ICollection<User> Users { get; set; }

    }

    public class UsersCourse
    {
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
    }

    //Course Prefix object
    public class CoursePrefix
    {
        [Key]
        public int PrefixID { get; set; }
        public string PrefixName { get; set; }
    }

    //Course Number object
    public class CourseNumber
    {
        [Key]
        public int NumberID { get; set; }
        public string Number { get; set; }
        public int PrefixID { get; set; }
    }

    //definition AssignmentGroup table
    public class AssignmentGroup
    {
        [Key]
        public int AssignmentGroupID { get; set; }
        public string AssignmentGroupName { get; set; }           
    }

    //definition Assignment table
    public class Assignment
    {
        [Key]
        public int AssignmentID { get; set; }
        public int AssignmentGroupID { get; set; }
        public int CourseID { get; set; }
        public string AssignmentName { get; set; }
        public System.DateTime DueDate { get; set; }
        public decimal MaxPoint { get; set; }
        public int activate { get; set; } 
    }

    public class StudentAssignment
    {
        [Key]
        public int StudentAssignmentID { get; set; }
        public int AssignmentID { get; set; }
        public string UserId { get; set; }
        public decimal Grade { get; set; }       
    }
    public class AssignmentFile
    {
        [Key]
        public int AssignmentFileID { get; set; }
        public int StudentAssignmentID { get; set; }
        public string FileName { get; set; }
        public System.DateTime UploadDate { get; set; }
    }
    /*
    public class YourNextTableHere
    {
        [Key]
        public int ID { get; set; }
        public datatype name { get; set; }
      
        //any FK or one-many or many-many relationship below here
        public virtual NextTableName TableName { get; set; }
    }
    */

    //Database connection EF code first
    public class StudentTrackerDBContext : IdentityDbContext<User>
    {
        public StudentTrackerDBContext()
            : base("dbStudentTracker")
        {
        }

        public static StudentTrackerDBContext Create()
        {
            return new StudentTrackerDBContext();
        }

        //add more DbSet table below here inside this class
        public DbSet<QuarterYear> QuarterYears { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UsersCourse> UsersCourses { get; set; }
        public DbSet<CoursePrefix> CoursePrefixs { get; set; }
        public DbSet<CourseNumber> CourseNumbers { get; set; }
        public DbSet<AssignmentGroup> AssignmentGroups { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<StudentAssignment> StudentAssignments { get; set; }
        public DbSet<AssignmentFile> AssignmentFiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            //configure model with fluent API
            modelBuilder.Entity<Course>().HasRequired(q => q.QuarterYear).WithMany(c => c.Courses).HasForeignKey(c => c.QuarterYearID);

            modelBuilder.Entity<User>().ToTable("Users", "dbo").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "dbo").Property(p => p.Id).HasColumnName("UserClaimId");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "dbo").Property(p => p.Id).HasColumnName("RoleId");
            modelBuilder.Ignore<IdentityUser>();
            /*
            //create many-to-many relationship between Instructor and Course
            modelBuilder.Entity<Course>().
              HasMany(u => u.Users).
              WithMany(c => c.Courses).
              Map(
               m =>
               {
                   m.MapLeftKey("UserId");
                   m.MapRightKey("ID");
                   m.ToTable("UsersCourses");
               });
            */
        }
    }
    /*
    public class UserDbContext : IdentityDbContext<User>
    {
        public UserDbContext()
            : base("dbStudentTracker", throwIfV1Schema: false)
        {
        }

        public static UserDbContext Create()
        {
            return new UserDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!



            modelBuilder.Entity<User>().ToTable("Users", "dbo").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "dbo").Property(p => p.Id).HasColumnName("UserClaimId");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "dbo").Property(p => p.Id).HasColumnName("RoleId");
            modelBuilder.Ignore<IdentityUser>();

            //modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            //modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }*/
}

#region Helpers
namespace StudentTracker
{
    public static class IdentityHelper
    {
        // Used for XSRF when linking external logins
        public const string XsrfKey = "XsrfId";

        public const string ProviderNameKey = "providerName";
        public static string GetProviderNameFromRequest(HttpRequest request)
        {
            return request.QueryString[ProviderNameKey];
        }

        public const string CodeKey = "code";
        public static string GetCodeFromRequest(HttpRequest request)
        {
            return request.QueryString[CodeKey];
        }

        public const string UserIdKey = "userId";
        public static string GetUserIdFromRequest(HttpRequest request)
        {
            return HttpUtility.UrlDecode(request.QueryString[UserIdKey]);
        }

        public static string GetResetPasswordRedirectUrl(string code, HttpRequest request)
        {
            var absoluteUri = "/Account/ResetPassword?" + CodeKey + "=" + HttpUtility.UrlEncode(code);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        public static string GetUserConfirmationRedirectUrl(string code, string userId, HttpRequest request)
        {
            var absoluteUri = "/Account/Confirm?" + CodeKey + "=" + HttpUtility.UrlEncode(code) + "&" + UserIdKey + "=" + HttpUtility.UrlEncode(userId);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        private static bool IsLocalUrl(string url)
        {
            return !string.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        public static void RedirectToReturnUrl(string returnUrl, HttpResponse response)
        {
            if (!String.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
            {
                response.Redirect(returnUrl);
            }
            else
            {
                response.Redirect("~/");
            }
        }
    }
}
#endregion
