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
    public class TeacherDataController : ApiController
    {
        //allows to access our database
        private SchoolDbContext School = new SchoolDbContext();

        ///<summary>
        /// Return the list of Teachers
        ///</summary>
        ///<param name="SearchKey">search teacher by their first name or last name, both first and last name, their employeenumber, hiredate and salary</param>
        ///<example>GET api/TeacherData/Listteachers/linda</example>
        ///<example>GET api/TeacherData/Listteachers/cody</example>
        ///<return>
        ///A list of Teacher object (id, firstname, lastname, employeenumber, hiredate, salary)
        ///</return>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public List<Teacher> ListTeachers(string SearchKey = null)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //establish a new command
            MySqlCommand cmd = Conn.CreateCommand();

            //Sql query
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ',teacherlname)) like lower(@key) or lower(employeenumber) like lower(@key) or lower(hiredate) like lower(@key) or lower(salary) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //gather result of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //create an empty list of teachers
            List<Teacher> Teachers = new List<Teacher> {};

            //loop for the result
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Convert.ToDouble(ResultSet["salary"]);

                Teachers.Add(NewTeacher);
            }

            //close connection
            Conn.Close();

            return Teachers;
        }

        /// <summary>
        /// Finds an author from the MySQL Database through an id. Non-Deterministic.
        /// </summary>
        /// <param name="id">The teacher ID</param>
        /// <example>api/AuthorData/Findteacher/6 -> {teacher Object}</example>
        /// <example>api/AuthorData/Findteacher/10 -> {teacher Object}</example>
        [HttpGet]
        [Route("Teacher/Show/{id}")]
        public Teacher FindTeachers(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Convert.ToDouble(ResultSet["salary"]);
            }
            Conn.Close();

            return NewTeacher;
        }




        /// <summary>
        /// return a teacher information which match the teacher id
        /// </summary>
        /// <param name="teacherid">teacher's id number</param>
        /// <returns>
        /// return a teacher information (including id, firstname, lastname, employeenumber, hiredate, salary) and teacher's course 
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{teacherid}")]

        public TeacherCourse FindTeacher(int teacherid)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //establish a new command
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            // find teacher 
            cmd.CommandText = "SELECT * FROM teachers WHERE teacherid = @teacherid";
            cmd.Parameters.AddWithValue("@teacherid", teacherid);
            cmd.Prepare();

            //gather result of query into a variable
            MySqlDataReader TeacherResult = cmd.ExecuteReader();

            //create a teacher instance
            Teacher SelectedTeacher = new Teacher();


            //loop for the result
            while (TeacherResult.Read())
            {

                SelectedTeacher.TeacherId = Convert.ToInt32(TeacherResult["teacherid"]);
                SelectedTeacher.TeacherFName = TeacherResult["teacherfname"].ToString();
                SelectedTeacher.TeacherLName = TeacherResult["teacherlname"].ToString();
                SelectedTeacher.EmployeeNumber = TeacherResult["employeenumber"].ToString();
                SelectedTeacher.HireDate = Convert.ToDateTime(TeacherResult["hiredate"]);
                SelectedTeacher.Salary = Convert.ToDouble(TeacherResult["salary"]);

            }

            // close sql reader
            TeacherResult.Close();

            // establish a new command
            // MySqlCommand cmd2 = Conn.CreateCommand();

            // find course for the teacher
            cmd.CommandText = "SELECT * FROM classes WHERE teacherid = @teacherid";


            //gather result of query into a variable
            MySqlDataReader ClassResult = cmd.ExecuteReader();

            // create a list of classes
            List<Course> ClassList = new List<Course> { };

            //loop for the result
            while (ClassResult.Read())
            {
                Course NewCourse = new Course();
                NewCourse.ClassName = ClassResult["classname"].ToString();
                ClassList.Add(NewCourse);
            }

            // close sql reader
            ClassResult.Close();

            //close the connection
            Conn.Close();

            //combine a teacher and a couse information
            TeacherCourse TeacherCourse = new TeacherCourse();
            TeacherCourse.Teacher = SelectedTeacher;
            TeacherCourse.Courses = ClassList;

            //return the final list of teacher information
            return TeacherCourse;
        }
    }
}
