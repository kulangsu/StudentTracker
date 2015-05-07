using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentTracker
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.IsInRole("Student"))
                Response.Redirect("~/Student");
            else if (Context.User.IsInRole("Admin"))
                Response.Redirect("~/Admin");
            else if (Context.User.IsInRole("Instructor"))
                Response.Redirect("~/Instructor");
        }
    }
}