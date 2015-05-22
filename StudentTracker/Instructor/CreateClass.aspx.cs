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
    public partial class CreateClass : System.Web.UI.Page
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

                // loads the quarters for the current year and the next year into qrtYearList
                var qrtYearList = db.QuarterYears
                    .Where(c => yrArr.Contains(c.Year) && qrtArry.Contains(c.Quarter))
                    .OrderByDescending(c => c.Year)
                    .Select(i => new { _ID = i.ID, _QrtYr = i.Year + " - " + i.Quarter })
                    .ToList();

                selectQuarterYear.DataValueField = "_ID";
                selectQuarterYear.DataTextField = "_QrtYr";
                selectQuarterYear.DataSource = qrtYearList;
                selectQuarterYear.DataBind();

                //if a quarter wasn't found for this year or the next (qrtYearList) an error message is given
                if (qrtYearList.Count == 0)
                {
                    ErrorMessage.Text = "No quarter was found for the current year or next year. A quarter needs to be created before you can create a class.";
                    btnClassCreate.Enabled = false;
                    ClassListMessage.Text = "";
                }

                //loads the Classes List that link to an Instructor
                LoadInstructorClassList(getQuarter.CurrentQuarter());
                LoadAllInstructorClassList(getQuarter.CurrentQuarter());

                //loading default course number
                int BIT = 1;
                LoadCourseNumber(BIT);

                //gets the User's full name when the page loads
                var manager = new UserManager<User>(new UserStore<User>(new StudentTrackerDBContext()));
                var currentUser = manager.FindById(Context.User.Identity.GetUserId());
                FullName.Text = currentUser.FirstName + ", "+currentUser.LastName;
            }
        }

        //loads All Classes that link to ALL Instructors
        //per Quarter Year selected
        protected void LoadAllInstructorClassList(string qrt)
        {
            int yr = DateTime.Now.Year;
            var yrArr = new int[] { yr, yr + 1 };
            string userID = User.Identity.GetUserId();

            //loads all the instructor userIDs into 'users'
            var users = roleManager.ReturnAllInstructor(); 

            //loads "CourseLists" with the UserID, course ID, course Name, year and quarter (by joining tables) and orders them in descending order by name 
            var CourseLists = db.UsersCourses
                .Join(db.Courses, c => c.CourseId, cm => cm.ID, (c, cm) => new { c, cm })
                .Join(db.QuarterYears, q => q.cm.QuarterYearID, qm => qm.ID, (q, qm) => new { q, qm })
                .Where(w => w.qm.Quarter.Equals(qrt) && users.Contains(w.q.c.UserId) && !w.q.c.UserId.Equals(userID))
                .OrderByDescending(q => q.q.cm.Name)
                .Select(i => new {i.q.c.UserId, CourseID = i.q.cm.ID, CourseName = i.q.cm.Name, Year = i.qm.Year, Quarter = i.qm.Quarter });

            //loads into "list" (by joining CourseLists with the UserID table where IDs match) 
            //the course ID, full name, year, quarter and coursename for all instructors (for display)
            var list = (from u in db.Users
                        join a in CourseLists on u.Id equals a.UserId
                        select new { a.CourseID, FullName = u.FirstName + " " + u.LastName, a.Year, a.Quarter, a.CourseName }
                       ).ToList();

            GridViewClassList.DataSource = list;
            GridViewClassList.DataBind();
        }

        //load Classes List that link to Instructore
        //per Quarter Year selected
        protected void LoadInstructorClassList(string qrt)
        {
            int yr = DateTime.Now.Year;
            var yrArr = new int[] { yr, yr + 1 };
            string userID = User.Identity.GetUserId();
            var CourseLists = db.UsersCourses
                .Join(db.Courses, c => c.CourseId, cm => cm.ID, (c, cm) => new { c, cm })
                .Join(db.QuarterYears, q => q.cm.QuarterYearID, qm => qm.ID, (q, qm) => new { q, qm })
                .Where(w => w.qm.Quarter.Equals(qrt) && w.q.c.UserId.Equals(userID))
                .OrderByDescending(q => q.q.cm.Name)
                .Select(i => new { CourseID = i.q.cm.ID, CourseName = i.q.cm.Name, Year = i.qm.Year, Quarter = i.qm.Quarter })
                .ToList();

            GridViewInstructorClassList.DataSource = CourseLists;
            GridViewInstructorClassList.DataBind();
        }

        protected void CreateClass_Click(object sender, EventArgs e)
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
                    LoadInstructorClassList(getQuarter.CurrentQuarter());
                    LoadAllInstructorClassList(getQuarter.CurrentQuarter());
                }
                else
                    ErrorMessage.Text += "System failed to insert new Class to database.";
            }
            else
            {
                ErrorMessage.Text = "Similar class entry already existed in database.";
            }
        }

        protected void selectQuarterYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            string qrt = selectQuarterYear.SelectedItem.Text;
            string[] temp = qrt.Split('-');
            LoadInstructorClassList(temp[1].Trim());
            LoadAllInstructorClassList(temp[1].Trim());
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