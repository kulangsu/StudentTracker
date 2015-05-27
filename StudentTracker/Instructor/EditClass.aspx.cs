using StudentTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudentTracker.Instructor
{
    public partial class EditClass : System.Web.UI.Page
    {
        //see IdentityModels.cs
        StudentTrackerDBContext db = new StudentTrackerDBContext();

        //see STLib.cs file
        GetQuarter getQuarter = new GetQuarter();
        RoleManager roleManager = new RoleManager();
        CapFirstLetter capFirstLetter = new CapFirstLetter();

        protected void Page_Load(object sender, EventArgs e)
        {
            int yr = DateTime.Now.Year;
            if (!IsPostBack)
            {
                //// display course name
                //CourseName.Text = "blah blah";

                int classID = Convert.ToInt32(Request.QueryString["field1"]);
                var dbClassID = db.Courses.SingleOrDefault(i => i.ID.Equals(classID));
                if (dbClassID != null)
                {
                    CourseName.Text = dbClassID.Name; // Display course name
                }
                else
                {
                    Response.Redirect("~/Instructor"); // Return to Instuctor homepage

                }
 

                //loading quarter year from database to gridview
                var yrArr = new int[] { yr, yr + 1 };

                //var query = from q in db.QuarterYears where (q => yrArr.Contains(q.Year)) orderby(q => q.);

                var qrtYearList = db.QuarterYears
                    .Where(c => yrArr.Contains(c.Year))
                    .OrderByDescending(c => c.Year)
                    .Select(i => new { _ID = i.ID, _QrtYr = i.Year + " - " + i.Quarter })
                    .ToList();

                selectQuarterYear.DataValueField = "_ID";
                selectQuarterYear.DataTextField = "_QrtYr";
                selectQuarterYear.DataSource = qrtYearList;
                selectQuarterYear.DataBind();

                selectQuarterYear.Items.FindByValue(dbClassID.QuarterYearID.ToString()).Selected = true;

                // Split the selected class name!
                string[] nameSplit = dbClassID.Name.Split();
                string first = nameSplit[0]; // Course Prefix like BIT
                string second = nameSplit[1]; // Course Number like 115
                string third = nameSplit[2]; // Course Name like Java
                string fourth = nameSplit[3]; // Section Number like Sec01

                // Course Pre-fix dropdown list
                var prefixList = db.CoursePrefixs

                    .Select(i => new { _ID = i.PrefixID, _Prefix = i.PrefixName })
                    .ToList();

                CourseArea.DataValueField = "_ID";
                CourseArea.DataTextField = "_Prefix";
                CourseArea.DataSource = prefixList;
                CourseArea.DataBind();

                if (first != null)
                {
                    CourseArea.Items.FindByValue(first).Selected = true;
                }
                
                // Get Course Number from the array
                if (second != null)
                {
                    //CourseNumber.Items.FindByValue(second).Selected = true;
                }

                // Get Course Name from the array
                if (third != null)
                {
                    ClassName.Text = third;
                }

                // Get Section number
                if (fourth != null)
                {
                    // CourseSection.Items.FindByValue(fourth).Selected = true;
                }
            }
        }


        protected void UpdateClass_Click(object sender, EventArgs e)
        {
            ErrorMessage.Text = "";

            string CoursePre = CourseArea.SelectedItem.Text;
            string CourseNum = CourseNumber.SelectedItem.Text;
            string courseName = ClassName.Text;
            string CourseSec = CourseSection.SelectedItem.Text;

            courseName = CoursePre + " " + CourseNum + " " + capFirstLetter.CapLetterString(ClassName.Text, ' ') + " " + CourseSec;
            //quick check to see if Year & QuarterYear already exist
            int qrtyrid = Convert.ToInt32(selectQuarterYear.SelectedValue);
            var quarteryear = db.Courses
                              .Where(q => q.QuarterYearID == qrtyrid && q.Name.Equals(courseName))
                              .ToList();

            if (quarteryear.Count == 0)
            {
                //insert new quarteryear into database
                var addClass = new Course
                {
                    QuarterYearID = Convert.ToInt32(selectQuarterYear.SelectedValue),
                    Name = courseName
                };
                db.Courses.Add(addClass);
                db.SaveChanges();
                int classID = addClass.ID;
                if (classID > 0)
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
                {
                    ErrorMessage.Text += "<br>New Class created successful.";
                    //load Classes List that link to Instructor
                    //LoadInstructorClassList(getQuarter.CurrentQuart());
                    //LoadAllInstructorClassList(getQuarter.CurrentQuart());
                }
                else
                    ErrorMessage.Text += "System failed to insert new Class to database.";
            }

        }

        protected void selectQuarterYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            string qrt = selectQuarterYear.SelectedItem.Text;
            string[] temp = qrt.Split('-');
            //LoadInstructorClassList(temp[1].Trim());
            //LoadAllInstructorClassList(temp[1].Trim());
        }

        protected void CourseArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coursePrefix = Convert.ToInt32(CourseArea.SelectedValue);
            LoadCourseNumber(coursePrefix);
        }

        protected void LoadCourseNumber(int num)
        {           
            var courseNumberList = db.CourseNumbers
                .Where(c => c.PrefixID == num)
                .OrderBy(c => c.Number)
                .ToList();

            CourseNumber.DataValueField = "Number";
            CourseNumber.DataTextField = "Number";
            CourseNumber.DataSource = courseNumberList;
            CourseNumber.DataBind();
        }

        
    }
}