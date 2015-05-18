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
    public partial class QuarterYearClass : System.Web.UI.Page
    {
        StudentTrackerDBContext db = new StudentTrackerDBContext();
        GetQuarter getQuarter = new GetQuarter();

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                int yr = DateTime.Now.Year;
                selectYear.Items.Add(yr.ToString());
                selectYear.Items.Add((yr + 1).ToString());
                selectQuarter.SelectedValue = getQuarter.CurrentQuart();
            }

            LoadQuarterYear();
        }

        protected void LoadQuarterYear()
        {
            //loading quarter year from database to gridview
            int yr = DateTime.Now.Year;
            var yrArr = new int[] { yr, yr + 1 };
            var qrtYearList = db.QuarterYears
                .Where(c => yrArr.Contains(c.Year))
                .OrderByDescending(c => c.Year)
                .ToList();

            GridViewQuarterYear.DataSource = qrtYearList;
            GridViewQuarterYear.DataBind();
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
                LoadQuarterYear();
            }
            else
            {
                ErrorMessage.Text = "Quarter Year entry already existed in database.";
            }
        }
    }
}