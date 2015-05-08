using StudentTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace StudentTracker.Instructor
{
    public partial class CreateClass : System.Web.UI.Page
    {
        StudentTrackerDBContext db = new StudentTrackerDBContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            int yr = DateTime.Now.Year;
            if (!IsPostBack)
            {
                //loading quarter year from database to gridview
                var yrArr = new int[] { yr, yr + 1 };

                //var query = from q in db.QuarterYears where (q => yrArr.Contains(q.Year)) orderby(q => q.);

                var qrtYearList = db.QuarterYears
                    .Where(c => yrArr.Contains(c.Year))
                    .OrderByDescending(c => c.Year)
                    .Select(i => new {_ID = i.ID, _QrtYr = i.Year +" - "+ i.Quarter })
                    .ToList();

                selectQuarterYear.DataValueField = "_ID";
                selectQuarterYear.DataTextField = "_QrtYr";
                selectQuarterYear.DataSource = qrtYearList;
                selectQuarterYear.DataBind();

                if(qrtYearList.Count == 0)
                {
                    ErrorMessage.Text = "No QuarterYear found for curreent year or next year. Quarter Year need to create before can create class.";
                    btnClassCreate.Enabled = false;
                    ClassListMessage.Text = "";
                }
            }

            ClassListMessage.Text = "lap to: "+User.Identity.GetUserId();
        }

        protected void CreateClass_Click(object sender, EventArgs e)
        {
            //quick check to see if Year & QuarterYear already exist
            var quarteryear = db.Courses
                              .Where(q => q.QuarterYearID == selectQuarterYear.SelectedIndex && q.Name.Equals(ClassName.Text))
                              .ToList();

            if (quarteryear.Count == 0)
            {
                //insert new quarteryear into database
                var addClass = new Course
                {
                    QuarterYearID = Convert.ToInt32(selectQuarterYear.SelectedValue),
                    Name = ClassName.Text.Trim()
                };
                db.Courses.Add(addClass);
                db.SaveChanges();
                int classID = addClass.ID;
                if(classID > 0)
                {
                    var addClassToIntructor = new UsersCourse
                    {
                        CourseId = classID,
                        UserId = User.Identity.GetUserId()
                    };
                    db.UsersCourses.Add(addClassToIntructor);
                    classID = db.SaveChanges();
                }

                if (classID > 0)
                    ErrorMessage.Text += "New Class created successful.";
                else
                    ErrorMessage.Text += "System failed to insert new Class to database.";
            }
            else
            {
                ErrorMessage.Text = "Similar class entry already existed in database.";
            }            
        }
    }
}