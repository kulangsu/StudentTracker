 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Hosting;
using System.IO.Compression;
using System.IO;
using System.Net;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Office.Interop.Word;
using System.Threading;
using System.Configuration;





namespace StudentTracker.Student
{
    public partial class testingupload : System.Web.UI.Page
    {
        string filename;
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

        protected void Button2_Click1(object sender, EventArgs e)
        {

            
            createFeedback();
            
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
            int milliseconds = 5000;
            Thread.Sleep(milliseconds);
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