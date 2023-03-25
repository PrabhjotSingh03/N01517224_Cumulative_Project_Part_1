using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace N01517224_Cumulative_Project_Part_1.Models
{
    public class Classes
    {
        public int ClassId { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime StartDate { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}