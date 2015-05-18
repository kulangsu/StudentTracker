using StudentTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Web.Security;


namespace StudentTracker.Instructor
{
    public partial class UploadGrade : System.Web.UI.Page
    {
        //see IdentityModels.cs
        StudentTrackerDBContext db = new StudentTrackerDBContext();
        //see STLib.cs file
        RoleManager roleManager = new RoleManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            //upload student grade for particular class required CourseID
            if (Request.QueryString["CourseID"] == null)
            {
                //page has been attempt without CourseID, redirect user back to Instructor Homepage
                Response.Redirect("~/Instructor");
            }
                
            //CourseID is found, let determine what class about to upload student grade
            int CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);
            //CourseID = 14;
            var msg = (from c in db.Courses where c.ID == CourseID select c.Name).SingleOrDefault();
            
            //Class not found, redirect to instructor homepage
            if (msg == null)
            {
                ErrorMessage.Text = "Class is not found, make sure your browser session still valid then try again.";
                DisableUpload();
                ClassName.Text = "Course Not Found!";
            }else
                ClassName.Text = msg.ToString();

            //load current enroll student list
            LoadCurrentEnrollStudent(CourseID);

            //default null dataset
            gvGradeUploadStudent.DataSource = null;
            gvGradeUploadStudent.DataBind();
            gvGradeUploadStatus.DataSource = null;
            gvGradeUploadStatus.DataBind();
            
        }

        protected void LoadCurrentEnrollStudent(int CourseID)
        {
            string userID = User.Identity.GetUserId();
            //load all instructor userID
            var users = roleManager.ReturnAllStudentID();
            
            var EnrollStudentLists = db.UsersCourses
                //.Join(db.Courses, c => c.CourseId, cm => cm.ID, (c, cm) => new { c, cm })
                .Join(db.Users, u => u.UserId, um => um.Id, (u, um) => new { u, um })
                .Where(w => w.u.CourseId == CourseID && !w.u.UserId.Equals(userID))
                .OrderBy(o => o.um.FirstName).ThenBy(i=>i.um.LastName)
                .Select(s => new { SID=s.um.SID,FirstName=s.um.FirstName,LastName=s.um.LastName }).ToList();
            
            gvCurrentStudentEnroll.DataSource = EnrollStudentLists;
            gvCurrentStudentEnroll.DataBind();
        }

        protected void UploadGrade_Click(object sender, EventArgs e)
        {
            //verify that the FileUpload control contains a file.
            if (StudentGradeFile.HasFile)
            {
                string isHeader = "Yes";  //Show Header Yes/No
                string FileName = Path.GetFileName(StudentGradeFile.PostedFile.FileName);
                string Extension = Path.GetExtension(StudentGradeFile.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["AppDataFolderPath"];

                string FilePath = Server.MapPath(FolderPath + FileName);
                StudentGradeFile.SaveAs(FilePath);
                ImportToGrid(FilePath, Extension, isHeader);
            }
            else
                ErrorMessage.Text = "You did not select Student Grade to upload.";
        }

        protected void ImportToGrid(string FilePath, string Extension, string isHDR)
        {
            string conStr = "";
            Boolean isFormat = true;
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
                default:
                    ErrorMessage.Text = "File format not support. Please upload excel .xls or .xlsx";
                    isFormat = false;
                    break;
            }

            if (isFormat)
            {
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                //Bind Data to GridView
                //gvGradeUploadStudent.Caption = Path.GetFileName(FilePath);
                gvGradeUploadStudent.DataSource = dt;
                gvGradeUploadStudent.DataBind();
            }
        }

        //disable all buttons
        protected void DisableUpload()
        {
            btnUploadGrade.Enabled = false;
            StudentGradeFile.Enabled = false;
        }
    }
}