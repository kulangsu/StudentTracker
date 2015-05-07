using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentTracker.Models
{
    public class QuarterYear
    {
        [Key]
        public int ID { get; set; }
        public int Year { get; set; }
        public string Quarter { get; set; }
    }

    public class QuarterYearsDBContext : DbContext
    {
        public QuarterYearsDBContext()
            : base("dbStudentTracker") {}

        public DbSet<QuarterYear> QuarterYears { get; set; }
    }

}