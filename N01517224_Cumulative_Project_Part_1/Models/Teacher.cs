using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace N01517224_Cumulative_Project_Part_1.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherFname { get; set; }
        public string TeacherLname { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public List<Subject> Subjects { get; set; }

        public Teacher() { }
    }
}