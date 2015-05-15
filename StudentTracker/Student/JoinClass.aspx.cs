using StudentTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudentTracker.Student
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        StudentTrackerDBContext db = new StudentTrackerDBContext();
        GetQuarter getQuarter = new GetQuarter();

        protected void Page_Load(object sender, EventArgs e)
        {
            int yr = DateTime.Now.Year;
           
            if (!IsPostBack)
            {
                string qrt = getQuarter.CurrentQuart();

                //get current quarter courses by joining courses and quarterYears tables        
                var courseList = db.Courses
                    .Join(db.QuarterYears, q => q.QuarterYearID, qm => qm.ID, (q, qm) => new { q, qm })
                    .Where(c => c.qm.Quarter.Equals(qrt) && c.qm.Year == yr)
                    .Select(i => new { Course_ID = i.q.ID, Course_Name = i.q.Name })
                    .ToList();

                //load current courses into dropdown
                drpDwn_Join.DataValueField = "Course_ID";
                drpDwn_Join.DataTextField = "Course_Name";
                drpDwn_Join.DataSource = courseList;
                drpDwn_Join.DataBind();
            }
        }
        protected void btn_Join_Click(object sender, EventArgs e)
        {
            ErrorMessage.Text = " ";
            string user = User.Identity.GetUserId();            

            //insert new class into UsersCourses table
            var addUserClass = new UsersCourse
            {
                UserId = user,
                CourseId = Convert.ToInt32(drpDwn_Join.SelectedValue)

            };
            db.UsersCourses.Add(addUserClass);
            int classID = db.SaveChanges(); 

            //message status to user
            if (classID > 0)
            {
                ErrorMessage.Text += "<br>You have successfully joined a class.";

            }
            else
                ErrorMessage.Text += "System failed to join you to this class.";
        }


    }
}