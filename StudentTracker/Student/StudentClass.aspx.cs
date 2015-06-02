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
    public partial class StudentClass : System.Web.UI.Page
    {
        StudentTrackerDBContext db = new StudentTrackerDBContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                int classID = Convert.ToInt32(Request.QueryString["courseID"]);
                var assignementList = db.Assignments                       
                       .Where(c => c.CourseID == classID)                       
                       .Select(i => new { Assignment_ID = i.AssignmentID, Assignment_Name = i.AssignmentName })
                       .ToList();

                //load current courses into dropdown
                drpDwn_Assignment.DataValueField = "Assignment_ID";
                drpDwn_Assignment.DataTextField = "Assignment_Name";
                drpDwn_Assignment.DataSource = assignementList;
                drpDwn_Assignment.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ErrorMessage.Text = " ";
            string user = User.Identity.GetUserId();
            int Assignment_ID = Convert.ToInt32(drpDwn_Assignment.SelectedValue);
            string file_name = System.IO.Path.GetFileName(FileUpload1.FileName);
            FileUpload1.SaveAs(Server.MapPath("~/Files/") + file_name);

            //insert new assignment into StudentAssignment table
            var addStudentAssignment = new StudentAssignment
            {
                AssignmentID = Assignment_ID,
                UserId = user
            };

            db.StudentAssignments.Add(addStudentAssignment);
            int StudentAssignment_ID = db.SaveChanges();

            //message status to user
            if (StudentAssignment_ID > 0)
            {
                //insert new assignment into AssignmentFiles table
                var addAssignmentFile = new AssignmentFile
                {
                    StudentAssignmentID = StudentAssignment_ID,
                    FileName = file_name,
                    UploadDate=System.DateTime.Now
                };

                db.AssignmentFiles.Add(addAssignmentFile);
                int AssignmentFile_ID = db.SaveChanges();
                if (AssignmentFile_ID > 0)
                {

                    ErrorMessage.Text += "<br>You have successfully upload your assignment.";
                }
                else
                    ErrorMessage.Text += "<br>System failed to upload your assignment.";
            }
            else
                ErrorMessage.Text += "<br>System failed to upload your assignment.";

        }

        

        protected void drpDwn_Assignment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}