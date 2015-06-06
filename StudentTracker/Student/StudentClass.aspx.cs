using StudentTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using Novacode;

namespace StudentTracker.Student
{
    public partial class StudentClass : System.Web.UI.Page
    {
        StudentTrackerDBContext db = new StudentTrackerDBContext();
        //gets the path of the local storage on the server
        string FolderPath = ConfigurationManager.AppSettings["AppDataFolderPath"];
       // name to be used in Insertion into student assignment files
        string fileName;
        
        protected void Page_Load(object sender, EventArgs e)
        {

            int classID = Convert.ToInt32(Request.QueryString["CourseID"]);
            var dbClassID = db.Courses.SingleOrDefault(i => i.ID.Equals(classID));
            if (dbClassID != null)
            {
                Lbl_pageTitle.Text = dbClassID.Name;
            }
            else
            {
                Lbl_pageTitle.Text = "Please return to the Student homepage to choose a class";

            }
            if (!IsPostBack)
            {
                var assignementList = db.Assignments                       
                       .Where(c => c.CourseID == classID)                       
                       .Select(i => new { Assignment_ID = i.AssignmentID, Assignment_Name = i.AssignmentName })
                       .ToList();

                //load current courses into dropdown
                drpDwn_Assignment.DataValueField = "Assignment_ID";
                drpDwn_Assignment.DataTextField = "Assignment_Name";
                drpDwn_Assignment.DataSource = assignementList;
                drpDwn_Assignment.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ErrorMessage.Text = " ";
            string user = User.Identity.GetUserId();
            int Assignment_ID = Convert.ToInt32(drpDwn_Assignment.SelectedValue);
            string file_name = System.IO.Path.GetFileName(FileUpload1.FileName);
            FileUpload1.SaveAs(Server.MapPath(FolderPath) + file_name);

            //insert new assignment into StudentAssignment table
            var addStudentAssignment = new StudentAssignment
            {
                AssignmentID = Assignment_ID,
                UserId = user
            };

            db.StudentAssignments.Add(addStudentAssignment);
            db.SaveChanges();
            int StudentAssignment_ID = addStudentAssignment.StudentAssignmentID;
            //message status to user
            if (StudentAssignment_ID > 0)
            {
                //insert new assignment into AssignmentFiles table
                var addAssignmentFile = new AssignmentFile
                {
                    StudentAssignmentID = StudentAssignment_ID,
                    FileName = file_name,
                    UploadDate=System.DateTime.Now
                };

                db.AssignmentFiles.Add(addAssignmentFile);
                int AssignmentFile_ID = db.SaveChanges();
                if (AssignmentFile_ID > 0)
                {

                    ErrorMessage.Text += "<br>You have successfully upload your assignment.";
                    //to create the feedback and insert the infor into the tabels for tracking
                    createFeedback();
                    insertFeedbackToTable();
                }
                else
                    ErrorMessage.Text += "<br>System failed to upload your assignment.";
            }
            else
                ErrorMessage.Text += "<br>System failed to upload your assignment.";

        }
        //this method inserts the information for the feedback document so it can be tracked and retrieved
        public void insertFeedbackToTable(){
            //get userID
            string user = User.Identity.GetUserId();
            int Assignment_ID = Convert.ToInt32(drpDwn_Assignment.SelectedValue);
            //Adds the file uploaded into the student assignment table
            var addStudentAssignment = new StudentAssignment
            {
                AssignmentID = Assignment_ID,
                UserId = user
            };
            //adds and saves
            db.StudentAssignments.Add(addStudentAssignment);
            int StudentAssignment_ID = db.SaveChanges();
            //this next line gets the Id of the resulting insertion from above.  this is then passed to the studentassignmentfile table
            StudentAssignment_ID=addStudentAssignment.StudentAssignmentID;
            if (StudentAssignment_ID > 0)
            {
                //insert new assignment into AssignmentFiles table
                var addAssignmentFile = new AssignmentFile
                {
                    StudentAssignmentID = StudentAssignment_ID,
                    FileName = fileName,
                    UploadDate = System.DateTime.Now
                };

                db.AssignmentFiles.Add(addAssignmentFile);
                int AssignmentFile_ID = db.SaveChanges();
            }
            
        }
        //will create a feedback document using the users info  from the file upload
        private void createFeedback()
        {
                    
            string user = User.Identity.GetUserId();
            //gets the user name to display
            var query =(from c in db.Users
              where c.Id == user
              select new { c.LastName, c.FirstName }).Single();
            String stuName = query.FirstName + " " + query.LastName;
            //gets assignment number to help 
            int Assignment_ID = Convert.ToInt32(drpDwn_Assignment.SelectedValue);
            string assignName = drpDwn_Assignment.SelectedItem.Text;
            //uses attributes from the user and file to create a unique name for the file to be saved
           fileName = Server.MapPath(FolderPath + "Feedback_" + assignName + "_" + stuName + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".docx");
            // Set up our paragraph contents:
            string headerText = "Feedback Document for " + assignName;
            string letterBodyText = DateTime.Now.ToShortDateString();
            string StudentName =stuName + Environment.NewLine + Environment.NewLine;
            //Main area for comments to be left.  for now we added a little bacon ipsum
            string paraTwo = ""
                + "PLEASE WRITE INDIVIDUAL COMMENTS HERE" + Environment.NewLine + Environment.NewLine
                + "Bacon ipsum dolor amet cupim ball tip t-bone corned beef beef ribs drumstick doner "
            + "andouille brisket short ribs ground round shoulder. Prosciutto pig ham ham hock, "
            + "pork belly picanha chicken pastrami turducken capicola salami biltong hamburger. "
            + "Filet mignon alcatra chicken, ham doner short ribs cupim t-bone tail chuck drumstick biltong salami porchetta capicola. "
            + "Venison pastrami jerky leberkas pancetta tri-tip drumstick kevin landjaeger chuck meatloaf meatball capicola pork kielbasa. "
            + "Meatloaf doner pork chop, porchetta alcatra chicken tongue flank pork belly beef kielbasa beef ribs chuck. "
            + "Ground round boudin capicola, flank salami leberkas hamburger fatback shoulder tongue pork chop rump. "
            + "Venison pork loin picanha swine prosciutto, pastrami cow t-bone landjaeger meatloaf."

                + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            string paraThree = "GRADE GOES HERE";

            // Title Formatting:
            var titleFormat = new Formatting();
            titleFormat.FontFamily = new System.Drawing.FontFamily("Arial Black");
            titleFormat.Size = 18D;
            titleFormat.Position = 12;

            // Body Formatting
            var paraFormat = new Formatting();
            paraFormat.FontFamily = new System.Drawing.FontFamily("Calibri");
            paraFormat.Size = 10D;
            titleFormat.Position = 12;

            // Grade Formatting
            var gradeFormat = new Formatting();
            titleFormat.FontFamily = new System.Drawing.FontFamily("Arial Black");
            titleFormat.Size = 14D;
            titleFormat.Position = 12;



            // Create the document in memory:
            var doc = DocX.Create(fileName);

            // Insert each prargraph, with appropriate spacing and alignment:
            Paragraph title = doc.InsertParagraph(headerText, false, titleFormat);
            title.Alignment = Alignment.center;

            doc.InsertParagraph(Environment.NewLine);
            Paragraph letterBody = doc.InsertParagraph(letterBodyText, false, paraFormat);
            letterBody.Alignment = Alignment.both;

            doc.InsertParagraph(Environment.NewLine);
            Paragraph letterName = doc.InsertParagraph(StudentName, false, paraFormat);
            letterBody.Alignment = Alignment.both;

            doc.InsertParagraph(Environment.NewLine);
            doc.InsertParagraph(paraTwo, false, paraFormat);
            doc.InsertParagraph(paraThree, false, gradeFormat);
            //save
            doc.Save();

        }

        

        protected void drpDwn_Assignment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}