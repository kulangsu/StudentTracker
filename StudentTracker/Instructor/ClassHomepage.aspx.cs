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
    }
}