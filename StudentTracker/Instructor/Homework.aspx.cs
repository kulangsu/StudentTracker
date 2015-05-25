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
    public partial class Homework : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StudentTrackerDBContext db = new StudentTrackerDBContext();

            int assignmentID = Convert.ToInt32(Request.QueryString["field"]);
            var dbAssignmentID = db.Assignment
                .SingleOrDefault(i => i.ID.Equals(assignmentID));

            if (dbAssignmentID != null)
            {
                Lbl_pageTitle.Text = dbAssignmentID.Name;

                var assignmentList = db.Assignment
               .Join(db.Courses, c => c.CourseId, cm => cm.ID, (c, cm) => new { c, cm })
               .Join(db.AssignmentGroup, ag => ag.cm.AssignmentGroupID, qm => qm.ID, (q, qm) => new { q, qm })
               .Where(w => w.qm.Assignment.Equals(qrt) && users.Contains(w.q.c.UserId) && !w.q.c.UserId.Equals(userID))
               .OrderByDescending(q => q.q.cm.Name)
               .Select(i => new { i.q.c.UserId, CourseID = i.q.cm.ID, CourseName = i.q.cm.Name, });


                GriveViewAssignmentList.DataSource = assignmentList;
                GriveViewAssignmentList.DataBind();
            }
        }


        protected void btnAddHmw_Click(object sender, EventArgs e)
        {
            Server.Transfer("AddHomework.aspx");
        }

        protected void btnRemoveHmw_Click(object sender, EventArgs e)
        {
            //Delete selected row
            GriveViewAssignmentList.Assignment.Rows[0].Delete();
        }

        protected void GriveViewAssignmentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the currently selected row using the SelectedRow property.
            GridViewRow row = GriveViewAssignmentList.SelectedRow;

            // Display the name from the selected row.
            MessageLabel.Text = "You selected " + row + ".";
        }
    }
}