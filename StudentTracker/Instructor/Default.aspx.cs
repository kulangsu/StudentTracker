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
    {   // the StudentTracker database is named "db" for future functions
        StudentTrackerDBContext db = new StudentTrackerDBContext();
        GetQuarter getQuarter = new GetQuarter();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            int yr = DateTime.Now.Year;

            // If the user isn't both authenticated and validated as an instructor, they are sent to the AccessDeny page.
            if (User.Identity.IsAuthenticated && !User.IsInRole("Instructor"))
                Response.Redirect("~/AccessDeny");

            if (!IsPostBack)
            {
                //loading the quarter year from the database to the gridview
                string userID = User.Identity.GetUserId();
                var yrArr = new int[] { yr, yr + 1 };
                string qrt = getQuarter.CurrentQuarter();

                // (to delete) var query = from q in db.QuarterYears where (q => yrArr.Contains(q.Year)) orderby(q => q.);

                // This saves the instructors courses from the database "db" to CourseLists
                var CourseLists = db.UsersCourses
                    .Join(db.Courses, c => c.CourseId, cm => cm.ID, (c, cm) => new { c, cm })
                    .Join(db.QuarterYears, q => q.cm.QuarterYearID, qm => qm.ID, (q, qm) => new {q, qm})
                    .Where(w => yrArr.Contains(w.qm.Year) && w.qm.Quarter.Equals(qrt) && w.q.c.UserId.Equals(userID))
                    .OrderByDescending(q => q.qm.Year)
                    .Select(i => new { CourseID=i.q.cm.ID, CourseName=i.q.cm.Name, Year=i.qm.Year, Quarter=i.qm.Quarter })
                    .ToList();

                //the instructor's courses are bound to the CourseListView
                CourseListView.DataSource = CourseLists;
                CourseListView.DataBind();
            

                schoolYear.Text = DateTime.Now.Year.ToString();

                Label mylabel = (Label)CourseListView.FindControl("quarterYear");
                mylabel.Text = getQuarter.CurrentQuarter() + " " + DateTime.Now.Year.ToString();

                LoadPreviousCourses();
                LoadNextQuarterCourses();

            }
        }

        //load previous courses Year only 
        protected void LoadNextQuarterCourses()
        {
            string userID = User.Identity.GetUserId();
            int yr = DateTime.Now.Year;
            //loading the quarter (QuarterYears) from the database to the gridview
            int[] temp = null;
            var yrArr = temp;
            string[] tempStr = null;
            var qrtArry = tempStr;

            //if current quarter is Fall, then increase year+1 and quarter jump to Winter
            if (getQuarter.InQuarter() == 3)
            {
                yrArr = new int[] { yr + 1 };
                qrtArry = new string[] { getQuarter.GetQuarters(0) };
                nextQuarterYear.Text = getQuarter.GetQuarters(0) + " " + (yr + 1).ToString();
            }
            else  //stay in current year then just increate quarter to next season
            {
                yrArr = new int[] { yr };
                qrtArry = new string[] { getQuarter.GetQuarters(getQuarter.InQuarter() + 1) };
                nextQuarterYear.Text = getQuarter.GetQuarters(getQuarter.InQuarter() + 1) + " " + yr.ToString();
            }

            var CourseListsNextQrt = db.UsersCourses
                .Join(db.Courses, c => c.CourseId, cm => cm.ID, (c, cm) => new { c, cm })
                .Join(db.QuarterYears, q => q.cm.QuarterYearID, qm => qm.ID, (q, qm) => new { q, qm })
                // (to delete) .Where(w => yrArr.Contains(w.qm.Year) && w.qm.Quarter.Equals(qrt) && w.q.c.UserId.Equals(userID))
                // (to delete) .Where(w => yrArr.Contains(w.qm.Year) && w.q.c.UserId.Equals(userID))
                .Where(w => w.q.c.UserId.Equals(userID) && yrArr.Contains(w.qm.Year) && qrtArry.Contains(w.qm.Quarter))
                .OrderByDescending(q => q.qm.Year)
                .Select(i => new { CourseID = i.q.cm.ID, CourseName = i.q.cm.Name, Year = i.qm.Year, Quarter = i.qm.Quarter })
                .ToList();

            ListViewNextCourses.DataSource = CourseListsNextQrt;
            ListViewNextCourses.DataBind();
        }

        //load previous courses Year only
        protected void LoadPreviousCourses()
        {
            string userID = User.Identity.GetUserId();
            var quarterID = GetQuarterYearID();

            var CourseLists = db.UsersCourses
                .Join(db.Courses, c => c.CourseId, cm => cm.ID, (c, cm) => new { c, cm })
                .Join(db.QuarterYears, q => q.cm.QuarterYearID, qm => qm.ID, (q, qm) => new { q, qm })
                // (to delete) .Where(w => yrArr.Contains(w.qm.Year) && w.qm.Quarter.Equals(qrt) && w.q.c.UserId.Equals(userID))
                // (to delete) .Where(w => yrArr.Contains(w.qm.Year) && w.q.c.UserId.Equals(userID))
                .Where(w => w.q.c.UserId.Equals(userID) && !quarterID.Contains(w.qm.ID))
                .OrderByDescending(q => q.qm.Year)
                .Select(i => new { CourseID = i.q.cm.ID, CourseName = i.q.cm.Name, Year = i.qm.Year, Quarter = i.qm.Quarter })
                .ToList();

            ListViewPreCourses.DataSource = CourseLists;
            ListViewPreCourses.DataBind();
        }

        //load quarter year id
        protected int[] GetQuarterYearID()
        {
            int yr = DateTime.Now.Year;
            //loading the quarter (QuarterYears) from the database to the gridview
            int[] temp = null;
            var yrArr = temp;
            string[] tempStr = null;
            var qrtArry = tempStr;

            if (getQuarter.InQuarter() == 3)
            {
                yrArr = new int[] { yr, yr + 1 };
                qrtArry = new string[] { getQuarter.GetQuarters(3), getQuarter.GetQuarters(0) };
            }
            else
            {
                yrArr = new int[] { yr };
                qrtArry = new string[] { getQuarter.GetQuarters(getQuarter.InQuarter()), getQuarter.GetQuarters(getQuarter.InQuarter() + 1) };
            }
            return db.QuarterYears
                .Where(q => yrArr.Contains(q.Year) && qrtArry.Contains(q.Quarter))
                .Select(s=>s.ID)
                .ToArray();
        }

        //delete class from instructor
        protected void DeleteClass_Click(object sender, CommandEventArgs e)
        {
            // the variable "deleteIndivCourse" is assigned to the course that matches the ID pulled in through the 
            // CammandEventArgs e when the "delete course" option is confirmed
            var deleteIndivCourse =
                from course in db.Courses
                where course.ID.ToString() == (String)e.CommandArgument
                select course;

            //if the course is present (and wasn't deleted from a duplicate viewing on the browser) it is removed from
            //the database and the screen is refreshed with the course removed.
            if (deleteIndivCourse.Count() > 0)
            {
                db.Courses.Remove(deleteIndivCourse.First());
                db.SaveChanges();
                Response.Redirect("~/Instructor/");
            }

        }

        //loads the student enrollment into class
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

        //load all Assignments that have been created into the Class
        protected string LoadAllAssignments(int CourseID)
        {
            
            return "00";
        }

        //load all Projects that link to this Class
        protected string LoadAllProjects(int CourseID)
        {

            return "00";
        }

        //load all ICEs that link to this Class
        protected string LoadAllICE(int CourseID)
        {

            return "00";
        }

        //load all Exams and Finals that link to this Class
        protected string LoadAllExamFinal(int CourseID)
        {

            return "00";
        }

        //load all ExtraCredits that link to this Class
        protected string LoadAllExtraCredit(int CourseID)
        {

            return "00";
        }
    }
}