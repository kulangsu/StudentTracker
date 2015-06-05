using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.UI.HtmlControls;

namespace StudentTracker.Instructor
{
    public partial class AddHomework : System.Web.UI.Page
    {
        StudentTrackerDBContext db = new StudentTrackerDBContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                {

                    int classID = Convert.ToInt32(Request.QueryString["CourseID"]);
                    var dbClassID = db.Courses.SingleOrDefault(i => i.ID.Equals(classID));
                    if (dbClassID != null)
                    {
                        CourseName.Text = dbClassID.Name; // Display course name
                    }
                    else
                    {
                        Response.Redirect("~/Instructor"); // Return to Instuctor homepage

                    }

                    var AssigmentGroupList = db.AssignmentGroups.ToList()
                   .Select(i => new { AssignmentGroupID = i.AssignmentGroupID, AssignmentGroupName = i.AssignmentGroupName })
                   .ToList();

                    DropDownList1.DataValueField = "AssignmentGroupID";
                    DropDownList1.DataTextField = "AssignmentGroupName";
                    DropDownList1.DataSource = AssigmentGroupList;
                    DropDownList1.DataBind();
                }
            }
        }

        //Reset button changes all values in the feilds interested back to blank
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtHmwName.Text = "";
            LabelEndDate.Text = "";
            TxtHmwPoints.Text = "";
            DropDownList1.SelectedValue = "1";
        }

        //Changes Due date text when a new date is selected on the calendar
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
           LabelEndDate.Text = Calendar1.SelectedDate.Date.ToShortDateString();
        }
     
        //If valid this will save the homework strings into the database, otherwise it will return an error
        //NEEDS MORE WORK!!!
        protected void btnAddHm_Click(object sender, EventArgs e)
        {
            Validate();
            if (IsValid)
            {
                int classID = Convert.ToInt32(Request.QueryString["CourseID"]);
 
                string homeworkName = txtHmwName.Text;
                System.DateTime dueDate = Calendar1.SelectedDate.Date;
                decimal possiblePoints = Convert.ToDecimal(TxtHmwPoints.Text);
                int assignmentType = Convert.ToInt32(DropDownList1.SelectedValue);

                if (homeworkName != null || dueDate != null)
                {
                    var addHomework = new Assignment
                    {
                        AssignmentName = homeworkName,
                        CourseID = classID,
                        AssignmentGroupID = assignmentType,
                        DueDate = dueDate,
                        MaxPoint = possiblePoints,
                        activate = 1  //auto-sets activation to true
                    };
                    db.Assignments.Add(addHomework);
                    int Assignment_ID = db.SaveChanges();
                    if (Assignment_ID > 0)
                    {
                        AddActionLabel.Visible = true;
                        AddActionLabel.Text = "A new assignment was created!";

                    }
                    else
                    {
                        AddActionLabel.Visible = true;
                        AddActionLabel.Text = "Assignment failed to create.";
                    }
                }
                else
                {
                    AddActionLabel.Visible = true;
                    AddActionLabel.Text = "Assignment failed to create.";
                }
            }
        }
    }
}