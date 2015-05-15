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

        protected void Page_Load(object sender, EventArgs e)
        {
            int yr = DateTime.Now.Year;
            string qr = StringQuarterYear();

            //get current quarter courses by joining courses and quarterYears tables        
            var courseList = db.Courses
                .Join(db.QuarterYears, q => q.QuarterYearID, qm => qm.ID, (q, qm) => new { q, qm })
                .Where(c => c.qm.Quarter.Equals(qr) && c.qm.Year == yr)
                .Select(i => new { Course_ID = i.q.ID, Course_Name = i.q.Name })
                .ToList();

            //load current courses into dropdown
            drpDwn_Join.DataValueField = "Course_ID";
            drpDwn_Join.DataTextField = "Course_Name";
            drpDwn_Join.DataSource = courseList;
            drpDwn_Join.DataBind();

        }
        protected void btn_Join_Click(object sender, EventArgs e)
        {
            string user = User.Identity.GetUserId();


            //insert new class into UsersCourse
            var userClass = new UsersCourse
            {
                UserId = user,
                CourseId = Convert.ToInt32(drpDwn_Join.SelectedValue)

            };
        }


        protected String StringQuarterYear()
        {
            int month = DateTime.Now.Month;
            if (month >= 0 && month <= 3) return "Winter";
            else if (month >= 4 && month <= 6) return "Spring";
            else if (month >= 7 && month <= 9) return "Summer";
            else return "Fall";
        }


    }
}