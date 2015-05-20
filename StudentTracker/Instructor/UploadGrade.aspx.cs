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
using System.Reflection;


namespace StudentTracker.Instructor
{
    public partial class UploadGrade : System.Web.UI.Page
    {
        //see IdentityModels.cs
        StudentTrackerDBContext db = new StudentTrackerDBContext();
        //see STLib.cs file
        RoleManager roleManager = new RoleManager();

        DataTable studentLists = new DataTable();

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

            studentLists = ConvertToDataTable(EnrollStudentLists);
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
            DataTable dt = new DataTable();
            DataTable status = new DataTable();
            status.Columns.Add("Status", typeof(string));

            DataTable grade = new DataTable();
            grade.Columns.Add("Message", typeof(string));

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
                //gvGradeUploadStudent.DataSource = dt;
                //gvGradeUploadStudent.DataBind();
            }

            //test--------------------------------
            ErrorMessage.Text = "<hr>";
            Boolean test = false;
            foreach (DataRow row in studentLists.Rows)
            {
                test = false;
                foreach (DataRow upload in dt.Rows)
                {
                    if (row[0].ToString().Equals(upload[0].ToString()))
                    {
                        status.Rows.Add("Ready");
                        grade.Rows.Add("Ready batch upload grade for this student.");
                        test = true;
                        break;                        
                    }
                }
                if (!test)
                {
                    status.Rows.Add("<span class='text-danger'>Not Ready</span>");
                    grade.Rows.Add("<span class='text-danger'>Student not found from grade upload.</span>");
                }
            }

            gvGradeUploadStatus.DataSource = status;
            gvGradeUploadStatus.DataBind();

            gvGradeUploadStudent.DataSource = grade;
            gvGradeUploadStudent.DataBind();
        }

        //disable all buttons
        protected void DisableUpload()
        {
            btnUploadGrade.Enabled = false;
            StudentGradeFile.Enabled = false;
        }

        public DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }
}