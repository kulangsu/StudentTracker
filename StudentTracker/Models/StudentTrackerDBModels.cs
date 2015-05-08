using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudentTracker.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTracker.Models
{
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
    public class StudentTrackerDBContext : DbContext
    {
        public StudentTrackerDBContext()
            : base("dbStudentTracker") { }

        //add more DbSet table below here inside this class
        public DbSet<QuarterYear> QuarterYears { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UsersCourse> UsersCourses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            //configure model with fluent API
            modelBuilder.Entity<Course>().HasRequired(q => q.QuarterYear).WithMany(c => c.Courses).HasForeignKey(c => c.QuarterYearID);

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

}