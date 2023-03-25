using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using N01517224_Cumulative_Project_Part_1.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace N01517224_Cumulative_Project_Part_1.Controllers
{
    public class ClassesDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// Returns a list of Courses
        /// </summary>
        /// <param name="searchText">takes in search text (optional)</param>
        /// <returns>A list of Course objects</returns>
        /// Example : /api/CourseData/listcourses
        /// Example : /api/CourseData/listcourses/web
        [HttpGet]
        [Route("api/CourseData/ListCourse/{searchText?}")]
        public List<Subject> ListCourses(string searchText = null)
        {

            MySqlConnection connection = School.AccessDatabase();

            connection.Open();

            MySqlCommand mySqlCommand = connection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM classes WHERE LOWER(classname) like LOWER(@key)";
            mySqlCommand.Parameters.AddWithValue("@key", "%" + searchText + "%");
            mySqlCommand.Prepare();

            MySqlDataReader subjectsresult = mySqlCommand.ExecuteReader();

            List<Subject> coursesList = new List<Subject>();

            while (subjectsresult.Read())
            {
                Subject subject = new Subject()
                {
                    ClassCode = subjectsresult["classcode"].ToString(),
                    ClassId = Convert.ToInt32(subjectsresult["classid"]),
                    ClassName = subjectsresult["classname"].ToString(),
                    FinishDate = Convert.ToDateTime(subjectsresult["finishdate"]),
                    StartDate = Convert.ToDateTime(subjectsresult["startdate"])
                };
                coursesList.Add(subject);
            }

            connection.Close();

            return coursesList;
        }

        /// <summary>
        /// Gets details of a courses from id
        /// </summary>
        /// <param name="id">course id</param>
        /// <returns>Returns Course details</returns>
        /// Example: /api/CourseData/getcourse/1
        [HttpGet]
        public Subject GetCourse(int id)
        {
            MySqlConnection connection = School.AccessDatabase();
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM classes WHERE classid = " + id;

            MySqlDataReader subjectsresult = command.ExecuteReader();
            Subject courseDetails = new Subject();

            while (subjectsresult.Read())
            {
                courseDetails.ClassCode = subjectsresult["classcode"].ToString();
                courseDetails.ClassId = Convert.ToInt32(subjectsresult["classid"]);
                courseDetails.ClassName = subjectsresult["classname"].ToString();
                courseDetails.FinishDate = Convert.ToDateTime(subjectsresult["finishdate"]);
                courseDetails.StartDate = Convert.ToDateTime(subjectsresult["startdate"]);
                courseDetails.TeacherId = Convert.ToInt32(subjectsresult["teacherid"]);
            }

            subjectsresult.Close();

            MySqlCommand teacherCommand = connection.CreateCommand();
            teacherCommand.CommandText = "SELECT teacherfname, teacherlname FROM teachers WHERE teacherid = " + courseDetails.TeacherId;

            MySqlDataReader teacherresult = teacherCommand.ExecuteReader();
            courseDetails.TeacherName = string.Empty;
            if (teacherresult.HasRows)
            {
                while (teacherresult.Read())
                {
                    courseDetails.TeacherName = teacherresult["teacherfname"].ToString() + " " + teacherresult["teacherlname"].ToString();
                }
            }


            return courseDetails;
        }
    }
}
