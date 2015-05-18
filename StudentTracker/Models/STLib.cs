using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


}
