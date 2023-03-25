using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using N01517224_Cumulative_Project_Part_1.Models;

namespace N01517224_Cumulative_Project_Part_1.Controllers
{
    public class StudentDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// Returns a list of Students
        /// </summary>
        /// <param name="SearchKey">takes in search text (optional)</param>
        /// <returns>A list of Student objects</returns>
        /// Example : /api/StudentData/liststudents
        /// Example : /api/StudentData/liststudents/sarah
        [HttpGet]
        [Route("api/StudentData/ListStudent/{SearchKey?}")]
        public List<Student> ListStudents(string SearchKey = null)
        {

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();
             
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or lower(concat(studentfname, ' ',studentlname)) like lower(@key) or lower(studentnumber) like lower(@key) or lower(enroldate)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader(); 

            List<Student> studentsList = new List<Student>();

            while (ResultSet.Read())
            {
                
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];

                Student newstudent = new Student();
                newstudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                newstudent.StudentFname = StudentFname;
                newstudent.StudentLname = StudentLname;
                newstudent.StudentNumber = StudentNumber;
                newstudent.EnrolDate = EnrolDate;

                studentsList.Add(newstudent);
            }

            Conn.Close();

            return studentsList;
        }

        /// <summary>
        /// Gets details of a students from id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>Returns Student details</returns>
        /// Example: /api/StudentData/1
        [HttpGet]
        public Student FindStudents(int id)
        {
            
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM students WHERE studentid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();
            Student NewStudent = new Student();

            while (ResultSet.Read())
            {
                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.StudentFname = ResultSet["studentfname"].ToString();
                NewStudent.StudentLname = ResultSet["studentlname"].ToString();
                NewStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                NewStudent.EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);
            }
            return NewStudent;
        }

    }
}
