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
    public partial class UploadGrade : System.Web.UI.Page
    {
        //see IdentityModels.cs
        StudentTrackerDBContext db = new StudentTrackerDBContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            //upload student grade for particular class required CourseID
            if (Request.QueryString["CourseID"] == null)
            {
                //page has been attempt without CourseID, redirect user back to Instructor Homepage
                Response.Redirect("~/Instructor");
            }
                
            //CourseID is found, let determine what class about to upload student grade
            int CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);
            //CourseID = 14;
            var msg = (from c in db.Courses where c.ID == CourseID select c.Name).SingleOrDefault();
            
            //Class not found, redirect to instructor homepage
            if (msg == null)
            {
                ErrorMessage.Text = "Class is not found, make sure your browser session still valid then try again.";
                DisableUpload();
                ClassName.Text = "Course Not Found!";
            }else
                ClassName.Text = msg.ToString();
            
        }

        protected void UploadGrade_Click(object sender, EventArgs e)
        {
            //verify that the FileUpload control contains a file.
            if (StudentGradeFile.HasFile)
            {
            }
            else
                ErrorMessage.Text = "You did not select Student Grade to upload.";
        }

        //disable all buttons
        protected void DisableUpload()
        {
            btnUploadGrade.Enabled = false;
            StudentGradeFile.Enabled = false;
        }
    }
}