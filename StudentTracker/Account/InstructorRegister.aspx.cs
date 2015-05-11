using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using StudentTracker.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace StudentTracker.Account
{
    public partial class RegisterInstructor : Page
    {
        protected void CreateInstructorUser_Click(object sender, EventArgs e)
        {
            // grab the context
            UserDbContext context = Context.GetOwinContext().Get<UserDbContext>();
            ErrorMessage.Text = ""; //clear out previous message

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            //verify instructor sceret code before register
            string thisSecretCode = ConfigurationManager.AppSettings["registerSecretCode"];
            if(!SecretCode.Text.Trim().Equals(thisSecretCode))
            {
                ErrorMessage.Text = "Secret Code is mismatch, please contact Administrator or Staft who oversee this Student Tracker.";
                return;
            }
            
            //force First and Last name first letter to Uppercase.
            string fName = FirstName.Text;
            fName = char.ToUpper(fName[0]) + fName.Substring(1);
            string lName = LastName.Text;
            lName = char.ToUpper(lName[0]) + lName.Substring(1);

            //built new user information
            var user = new User
            {
                UserName = Email.Text,  //UserName will use email as login
                Email = Email.Text,
                FirstName = fName,
                LastName = lName,
                SID = Convert.ToInt32(SID.Text),
                City = City.Text,
                CreatedDate = System.DateTime.Now,
                LastLogin = System.DateTime.Now
            };

            //need to create store procedure to check make sure only 1 valid SID or EID.
            //check SID and Email and EmailConfirmed = True

            /*
             * code go here
             * 
             */

            IdentityResult result = manager.Create(user, Password.Text);
            
            if (result.Succeeded)
            {
                //add new user to default role "Student"
                // create a role manager from the context
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                // check if the "Admin" role exists; if it doesn't, create it
                IdentityRole role = roleManager.FindByName("Instructor");
                if (role == null)
                {
                    role = new IdentityRole("Instructor");
                    roleManager.Create(role);
                }

                // add the user to the role
                manager.AddToRole(user.Id, role.Name);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                if (user.EmailConfirmed)
                {
                    //IdentityHelper.SignIn(manager, user, isPersistent: false);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    ErrorMessage.Text = "New Account Successful Created:<br>An email has been sent to your account. Please view the email and confirm your account to complete the registration process.";
                    DisableAllField();

                    //redirec to login page after 5 seconds delay
                    Response.AddHeader("REFRESH", "5;URL=Login");
                }
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        //disable all field after sucess create new account
        protected void DisableAllField()
        {
            SecretCode.Enabled = false;
            FirstName.Enabled = false;
            LastName.Enabled = false;
            SID.Enabled = false;
            City.Enabled = false;
            Email.Enabled = false;
            Password.Enabled = false;
            ConfirmPassword.Enabled = false;
            InstructorRegister.Enabled = false;
            btnReset.Enabled = false;
        }
    }
}