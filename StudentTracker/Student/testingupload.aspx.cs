using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Novacode;
using System.Diagnostics;

using System.Configuration;





namespace StudentTracker.Student
{
    public partial class testingupload : System.Web.UI.Page
    {
        string FolderPath = ConfigurationManager.AppSettings["AppDataFolderPath"];
        protected void Page_Load(object sender, EventArgs e)
        {
           


        }

        protected void drpDwnSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBrowse_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

       

        private void createFeedback()
        {

             // Modify to suit your machine:
            string fileName = Server.MapPath(FolderPath + "feedback.docx");
  
            // Create a document in memory:
            var doc = DocX.Create(fileName);
  
            // Insert a paragrpah:
            doc.InsertParagraph("This is my first paragraph");
  
            // Save to the output directory:
            doc.Save();
  
            // Open in Word:
           
        



        }

        
        
       

        protected void Assingments_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            createFeedback();
        }
    }
}