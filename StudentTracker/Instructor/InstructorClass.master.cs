using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace StudentTracker.Instructor
{
    public partial class InstructorClassMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int classID = Convert.ToInt32(Request.QueryString["field1"]);
            testtodd1.HRef = "Homework.aspx?field2=" + classID;

        }
    }
}