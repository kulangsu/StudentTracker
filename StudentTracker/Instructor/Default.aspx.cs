using StudentTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Web.UI.HtmlControls;


namespace StudentTracker.Instructor
{
    public partial class Default : System.Web.UI.Page
    {
        StudentTrackerDBContext db = new StudentTrackerDBContext();
        GetQuarter getQuarter = new GetQuarter();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated && !User.IsInRole("Instructor"))
                Response.Redirect("~/AccessDeny");

            if (!IsPostBack)
            {
                //loading quarter year from database to gridview
                int yr = DateTime.Now.Year;
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
            }

            schoolYear.Text = DateTime.Now.Year.ToString();
        }

    }
}