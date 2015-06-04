using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;
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
          //  Document doc = new Document();
            Microsoft.Office.Interop.Word._Application oWord = new Application();

            oWord.Visible = true;

            var oDoc = oWord.Documents.Add();

            //Insert a paragraph at the beginning of the document.
            var paragraph1 = oDoc.Content.Paragraphs.Add();

            paragraph1.Range.Text = "Testing Testing";
            paragraph1.Range.Font.Bold = 1;
            paragraph1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.

            oDoc.SaveAs2(Server.MapPath(FolderPath + "feedback.docx"));
           
            oWord.Quit();
            
            

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