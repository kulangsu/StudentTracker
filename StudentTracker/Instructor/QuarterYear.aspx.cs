using StudentTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentTracker.Instructor
{
    public partial class QuarterYearClass : System.Web.UI.Page
    {
        StudentTrackerDBContext db = new StudentTrackerDBContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            int yr = DateTime.Now.Year;
            if (!IsPostBack)
            {
 
                selectYear.Items.Add(yr.ToString());
                selectYear.Items.Add((yr + 1).ToString());
                selectQuarter.SelectedValue = StringQuarterYear();
            }
            //loading quarter year from database to gridview
            var yrArr = new int[] { yr, yr + 1 };

            var qrtYearList = db.QuarterYears
                .Where(c => yrArr.Contains(c.Year))
                .OrderByDescending(c => c.Year)
                .ToList();

            GridViewQuarterYear.DataSource = qrtYearList;
            GridViewQuarterYear.DataBind();
        }

        protected String StringQuarterYear()
        {
            int month = DateTime.Now.Month;
            if (month >= 0 && month <= 3) return "Winter";
            else if (month >= 4 && month <= 6) return "Spring";
            else if (month >= 7 && month <= 9) return "Summer";
            else return "Fall";
        }

        protected void CreateQrtYear_Click(object sender, EventArgs e)
        {
            //quick check to see if Year & QuarterYear already exist
            int thisYr = Convert.ToInt32(selectYear.SelectedValue.ToString());
            string thisQrt = selectQuarter.SelectedValue.ToString();
            
            var quarteryear = db.QuarterYears
                              .Where(q => q.Year == thisYr && q.Quarter.Equals(thisQrt))
                              .ToList();

            if (quarteryear.Count == 0)
            {
                //insert new quarteryear into database
                var QrtYear = new QuarterYear
                {
                    Year = Convert.ToInt32(selectYear.SelectedValue),
                    Quarter = selectQuarter.SelectedValue
                };
                db.QuarterYears.Add(QrtYear);
                db.SaveChanges();
                

                ErrorMessage.Text = "Quarter Year inserted successful.";
            }
            else
            {
                ErrorMessage.Text = "Quarter Year entry already existed in database.";
            }
        }
    }
}