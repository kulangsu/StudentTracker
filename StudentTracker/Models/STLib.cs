using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudentTracker.Models
{
    public static class Quarters
    {
        public const string Winter = "Winter";
        public const string Spring = "Spring";
        public const string Summer = "Summer";
        public const string Fall = "Fall";
    }

    public class GetQuarter
    {
        //determine default current quarter by Month of the year
        public string CurrentQuart()
        {
            return GetQuarters(InQuarter());
        }

        //return next quarter
        public string NextQuarter()
        {
            if (InQuarter() == 3) return GetQuarters(0);
            else return GetQuarters(InQuarter() + 1);
        }

        //return previous quarter
        public string PreviousQuarter()
        {
            if (InQuarter() == 0) return GetQuarters(3);
            else return GetQuarters(InQuarter() - 1);
        }

        //determine default current quarter by Month of the year
        public int InQuarter()
        {
            int month = DateTime.Now.Month;
            if (month >= 0 && month <= 3) return 0;     //winter
            else if (month >= 4 && month <= 6) return 1;    //spring
            else if (month >= 7 && month <= 9) return 2;    //summer
            else return 3;  //fall
        }

        public string GetQuarters(int num)
        {
            switch (num)
            {
                case 0: return Quarters.Winter;
                case 1: return Quarters.Spring;
                case 2: return Quarters.Summer;
                case 3: return Quarters.Fall;
                default: return null;
            }
        }
    }

    //default RoleManager object
    public class RoleManager
    {
        StudentTrackerDBContext db = new StudentTrackerDBContext();
        
        //return all Student ID
        public string[] ReturnAllStudentID()
        {
            return AllUserID(ReturnRoleID("Student"));
        }

        //return all Instructor ID
        public string[] ReturnAllInstructor()
        {
            return AllUserID(ReturnRoleID("Instructor"));
        }
        //return all Admin ID
        public string[] ReturnAllAdmin()
        {
            return AllUserID(ReturnRoleID("Admin"));
        }

        //return RoleID by role name
        public string ReturnRoleID(string roleName)
        {
            return (from r in db.Roles where r.Name.Contains(roleName) select r).FirstOrDefault().Id;
        }

        //return all user id by roleID
        public string[] AllUserID(string RoleID)
        {
            return db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(RoleID)).Select(u => u.Id).ToArray();
        }
    }

    //Capitalize first letter each word from a string
    //exmaple: "hello my name is lap to"
    //return: "Hello My Name Is Lap To"
    public class CapFirstLetter
    {
        //capitalize first letter of each word
        public String CapLetterString(String str, char delimiter)
        {
            str = str.ToLower().Trim();
            if (delimiter.ToString() == null) delimiter = ' ';
            string[] temp = str.Split(delimiter);
            str = " ";
            for (int i = 0; i < temp.Length; i++)
            {
                str += CapLetterWord(temp[i]);
                str += " ";
            }

            return str.Trim();
        }

        //capitalize first letter from word
        public string CapLetterWord(string word)
        {
            word = word.ToLower().Trim();
            return (char.ToUpper(word[0]) + word.Substring(1));
        }
    }

}
