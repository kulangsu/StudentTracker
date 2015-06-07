using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentTracker.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web.UI.HtmlControls;

namespace StudentTracker.Instructor
{
    public partial class ClassHomepage : System.Web.UI.Page
    {
        StudentTrackerDBContext db = new StudentTrackerDBContext();


        /// <summary>
        /// if we have a class id, load the name of the class that we are looking at
        /// else, take us to the Insttuctor page to choose a class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            int classID = Convert.ToInt32(Request.QueryString["field1"]);
            var dbClassID = db.Courses.SingleOrDefault(i => i.ID.Equals(classID));
            if (dbClassID != null)
            {
                Lbl_pageTitle.Text = dbClassID.Name;
            }
            else
            {
                Response.Redirect("~/Instructor"); // Return to Instuctor homepage
            }

        }

        /// <summary>
        /// On button click, if the user input is valid, collect data and add a new student
        /// to the UsersCourses table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Submit_Click(object sender, EventArgs e)
        {
            Validate();

            if (IsValid)
            {

                //capture the course ID and the new student's SID#
                int classID = Convert.ToInt32(Request.QueryString["field1"]);
                int sid = Convert.ToInt32(TxtBx_sid.Text);

                //check to see if there is a match for the entered SID# in Users table
                var newStudent = db.Users.SingleOrDefault(i => i.SID.Equals(sid));

                //if there is a matching User, begin process of enrolling
                if (newStudent != null)
                {
                    //check to see if the student is already in the class
                    //if not enrolled, create a new UsersCourse object 
                    var isEnrolled = db.UsersCourses.SingleOrDefault(u => u.UserId.Equals(newStudent.Id));
                    if (isEnrolled != null)
                    {
                        var newClassStudent = new UsersCourse
                        {
                            UserId = newStudent.Id.ToString(),
                            CourseId = classID

                        };

                        //add new UsersCourse object to the UsersCourses table
                        db.UsersCourses.Add(newClassStudent);
                        var isAdded = db.SaveChanges(); //Commit to database

                        //If we have successfully added to the database, 
                        //let the user know and update the Gridview with the new information.
                        if (isAdded == 1)
                        {
                            //TODO: give message that new student has been added and bind the new data to the gridview
                            GrdView_Students.DataBind();
                            Lbl_Message.Text = "You have successfully added a student!";
                        }
                        else
                        {
                            //Give message that adding new student was unsuccessful
                        }
                    }
                    else
                    {
                        //TODO: give message student is already enrolled in this class
                    }
                }
                else
                {
                    //TODO: give message that SID is not in the database
                }


            }
            else
            {
                //Give messaage that data could not be validated
            }

        }

        protected void Btn_Reset_Click(object sender, EventArgs e)
        {

        }


    }
}