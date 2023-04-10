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
        private SchoolDbContext schoolDbContext = new SchoolDbContext();

        /// <summary>
        /// Returns a list of Students
        /// </summary>
        /// <param name="searchText">takes in search text (optional)</param>
        /// <returns>A list of Student objects</returns>
        /// Example : /api/StudentData/liststudents
        /// Example : /api/StudentData/liststudents/sarah
        [HttpGet]
        [Route("api/StudentData/ListStudent/{searchText?}")]
        public List<Student> ListStudents(string searchText = null)
        {

            MySqlConnection connection = schoolDbContext.AccessDatabase();

            connection.Open();

            MySqlCommand mySqlCommand = connection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM students WHERE LOWER(studentfname) like LOWER(@key) OR LOWER(studentlname) LIKE LOWER(@key)";
            mySqlCommand.Parameters.AddWithValue("@key", "%" + searchText + "%");
            mySqlCommand.Prepare();

            MySqlDataReader resultSet = mySqlCommand.ExecuteReader();

            List<Student> studentsList = new List<Student>();

            while (resultSet.Read())
            {
                Student student = new Student();
                student.StudentId = Convert.ToInt32(resultSet["studentid"]);
                student.StudentFname = resultSet["studentfname"].ToString();
                student.StudentLname = resultSet["studentlname"].ToString();
                student.StudentNumber = resultSet["studentnumber"].ToString();
                student.EnrolDate = Convert.ToDateTime(resultSet["enroldate"]);
                studentsList.Add(student);
            }

            connection.Close();

            return studentsList;
        }

        /// <summary>
        /// Gets details of a students from id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>Returns Student details</returns>
        /// Example: /api/StudentData/getstudent/1
        [HttpGet]
        public Student GetStudent(int id)
        {
            MySqlConnection connection = schoolDbContext.AccessDatabase();
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM students WHERE studentid = " + id;

            MySqlDataReader result = command.ExecuteReader();
            Student studentDetails = new Student();

            while (result.Read())
            {
                studentDetails.StudentId = Convert.ToInt32(result["studentid"]);
                studentDetails.StudentFname = result["studentfname"].ToString();
                studentDetails.StudentLname = result["studentlname"].ToString();
                studentDetails.StudentNumber = result["studentnumber"].ToString();
                studentDetails.EnrolDate = Convert.ToDateTime(result["enroldate"]);
            }

            return studentDetails;
        }
    }
}
