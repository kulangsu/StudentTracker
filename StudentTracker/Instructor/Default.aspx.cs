using StudentTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Web.UI.HtmlControls;
using Microsoft.AspNet.Identity.EntityFramework;




namespace StudentTracker.Instructor
{
    public partial class Default : System.Web.UI.Page
    {
        StudentTrackerDBContext db = new StudentTrackerDBContext();
        GetQuarter getQuarter = new GetQuarter();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            int yr = DateTime.Now.Year;

            if (User.Identity.IsAuthenticated && !User.IsInRole("Instructor"))
                Response.Redirect("~/AccessDeny");

            if (!IsPostBack)
            {
                //loading quarter year from database to gridview
                string userID = User.Identity.GetUserId();
                var yrArr = new int[] { yr, yr + 1 };
                string qrt = getQuarter.CurrentQuart();
                //var query = from q in db.QuarterYears where (q => yrArr.Contains(q.Year)) orderby(q => q.);

                var CourseLists = db.UsersCourses
                    .Join(db.Courses, c => c.CourseId, cm => cm.ID, (c, cm) => new { c, cm })
                    .Join(db.QuarterYears, q => q.cm.QuarterYearID, qm => qm.ID, (q, qm) => new {q, qm})
                    .Where(w => yrArr.Contains(w.qm.Year) && w.qm.Quarter.Equals(qrt) && w.q.c.UserId.Equals(userID))
                    .OrderByDescending(q => q.qm.Year)
                    .Select(i => new { CourseID=i.q.cm.ID, CourseName=i.q.cm.Name, Year=i.qm.Year, Quarter=i.qm.Quarter })
                    .ToList();

                CourseListView.DataSource = CourseLists;
                CourseListView.DataBind();
            

                schoolYear.Text = DateTime.Now.Year.ToString();

                LoadPreviousCourses();

            }
        }


        //load previous courses Year only
        protected void LoadPreviousCourses()
        {
            string userID = User.Identity.GetUserId();

            var CourseLists = db.UsersCourses
                .Join(db.Courses, c => c.CourseId, cm => cm.ID, (c, cm) => new { c, cm })
                .Join(db.QuarterYears, q => q.cm.QuarterYearID, qm => qm.ID, (q, qm) => new { q, qm })
                //.Where(w => yrArr.Contains(w.qm.Year) && w.qm.Quarter.Equals(qrt) && w.q.c.UserId.Equals(userID))
                //.Where(w => yrArr.Contains(w.qm.Year) && w.q.c.UserId.Equals(userID))
                .Where(w => w.q.c.UserId.Equals(userID))
                .OrderByDescending(q => q.qm.Year)
                .Select(i => new { CourseID = i.q.cm.ID, CourseName = i.q.cm.Name, Year = i.qm.Year, Quarter = i.qm.Quarter })
                .ToList();

            ListViewPreCourses.DataSource = CourseLists;
            ListViewPreCourses.DataBind();
        }

        // clicking Edit button redirects user to EditClass page
        protected void btnEditClass_Click(object sender, CommandEventArgs e)
        {
            Response.Redirect("EditClass.aspx?classID="+ e.CommandArgument);
        }

        //delete class from instructor
        protected void DeleteClass_Click(object sender, CommandEventArgs e)
        {
           // Response.Redirect("~/Instructor/");
            var deleteIndivCourse =
                from course in db.Courses
                where course.ID.ToString() == (String)e.CommandArgument
                select course;
            if (deleteIndivCourse.Count() > 0)
            {
                db.Courses.Remove(deleteIndivCourse.First());
                db.SaveChanges();
                Response.Redirect("~/Instructor/");
            }

        }

        //load student enrollment into class
        protected string LoadStudentEnroll(int CourseID)
        {
            string userID = User.Identity.GetUserId();
            if(CourseID > 0)
            {
                var allEnrollStudent = db.UsersCourses
                .Where(u => u.CourseId == CourseID && !u.UserId.Equals(userID))
                .Count();

                if (allEnrollStudent > 0 && allEnrollStudent < 10) return "0" + allEnrollStudent.ToString();
                else if (allEnrollStudent > 9) return allEnrollStudent.ToString();
                else return "00";
            }
            
            return "00";                           
        }

        //load all Assignments that created to it Class
        protected string LoadAllAssignments(int CourseID)
        {
            
            return "00";
        }

        //load all Projects that link to this Class
        protected string LoadAllProjects(int CourseID)
        {

            return "00";
        }

        //load all ICE that link to this Class
        protected string LoadAllICE(int CourseID)
        {

            return "00";
        }

        //load all Exam and Final that link to this Class
        protected string LoadAllExamFinal(int CourseID)
        {

            return "00";
        }

        //load all ExtraCredit that link to this Class
        protected string LoadAllExtraCredit(int CourseID)
        {

            return "00";
        }
    }
}