using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace N01517224_Cumulative_Project_Part_1.Models
{
    public class TeacherCourse
    {
        public Teacher Teacher { get; set; }
        public List<Course> Courses { get; set; }
    }
}