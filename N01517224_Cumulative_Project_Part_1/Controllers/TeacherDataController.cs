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
        private SchoolDbContext schoolDbContext = new SchoolDbContext();

        /// <summary>
        /// Returns a list of Teachers
        /// </summary>
        /// <param name="searchText">takes in search text (optional)</param>
        /// <returns>A list of Teacher objects</returns>
        /// Example : /api/TeacherData/listteachers
        /// Example : /api/TeacherData/listteachers/alex
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{searchText?}")]
        public List<Teacher> ListTeachers(string searchText = null)
        {

            MySqlConnection connection = schoolDbContext.AccessDatabase();

            connection.Open();

            MySqlCommand mySqlCommand = connection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM teachers WHERE LOWER(teacherfname) like LOWER(@key) OR LOWER(teacherlname) LIKE LOWER(@key)";
            mySqlCommand.Parameters.AddWithValue("@key", "%" + searchText + "%");
            mySqlCommand.Prepare();

            MySqlDataReader resultSet = mySqlCommand.ExecuteReader();

            List<Teacher> teachersList = new List<Teacher>();

            while (resultSet.Read())
            {
                Teacher teacher = new Teacher();
                teacher.TeacherId = Convert.ToInt32(resultSet["teacherid"]);
                teacher.TeacherFname = resultSet["teacherfname"].ToString();
                teacher.TeacherLname = resultSet["teacherlname"].ToString();
                teacher.EmployeeNumber = resultSet["employeenumber"].ToString();
                teacher.HireDate = Convert.ToDateTime(resultSet["hiredate"]);
                teacher.Salary = Convert.ToDecimal(resultSet["salary"]);
                teachersList.Add(teacher);
            }

            connection.Close();

            return teachersList;
        }

        /// <summary>
        /// Gets details of a teachers from id
        /// </summary>
        /// <param name="id">teacher id</param>
        /// <returns>Returns Teacher details</returns>
        /// Example: /api/TeacherData/getteacher/1
        [HttpGet]
        public Teacher GetTeacher(int id)
        {
            MySqlConnection connection = schoolDbContext.AccessDatabase();
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM teachers WHERE teacherid = " + id;

            MySqlDataReader result = command.ExecuteReader();
            Teacher teacherDetails = new Teacher();

            while (result.Read())
            {
                teacherDetails.TeacherId = Convert.ToInt32(result["teacherid"]);
                teacherDetails.TeacherFname = result["teacherfname"].ToString();
                teacherDetails.TeacherLname = result["teacherlname"].ToString();
                teacherDetails.EmployeeNumber = result["employeenumber"].ToString();
                teacherDetails.HireDate = Convert.ToDateTime(result["hiredate"]);
                teacherDetails.Salary = Convert.ToDecimal(result["salary"]);
            }

            result.Close();

            MySqlCommand getSubjectscommand = connection.CreateCommand();
            getSubjectscommand.CommandText = "SELECT * FROM classes WHERE teacherid = " + id;

            MySqlDataReader subjectsresult = getSubjectscommand.ExecuteReader();
            teacherDetails.Subjects = new List<Subject>();
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
                teacherDetails.Subjects.Add(subject);
            }

            return teacherDetails;
        }
        //POST
        /// <summary>
        /// Write query to delete specific teacher based on his/her id
        /// </summary>
        /// <param name="id"> Id of the teacher</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/5</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create a connection
            MySqlConnection Connection = schoolDbContext.AccessDatabase();

            //Open the connection between local db and web server
            Connection.Open();

            //New command - Query for DB
            MySqlCommand command = Connection.CreateCommand();

            //Write SQL query
            command.CommandText = "delete from teachers where teacherid=@id";
            command.Parameters.AddWithValue("@id", id);
            command.Prepare();

            //Run the query in db
            command.ExecuteNonQuery();

            //Close the db connection
            Connection.Close();
        }

        //POST
        /// <summary>
        /// Create a new teacher in db by using query (insert into). 
        /// </summary>
        /// <param name="NewTeacher"> Created object which is holding all info given by user to insert into teachers table</param>
        /// <example> /api/TeacherData/AddTeacher
        /// FORM / POST DATA / REQUEST
        /// {
        ///     TeacherFname: "Luis";
        ///     TeacherLname: "Miguel";
        ///     EmployeeNumber: "T639";
        ///     Hiredate: "2022-02-09 00:00:00";
        ///     Salary: "62.48";
        /// }
        /// </example>
        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create a connection
            MySqlConnection Connection = schoolDbContext.AccessDatabase();

            //Open the connection between local db and web server
            Connection.Open();

            //New command - Query for DB
            MySqlCommand command = Connection.CreateCommand();

            //Write SQL query
            command.CommandText = "insert into teachers(teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname, @TeacherLname, @EmployeeNumber, @Hiredate, @Salary)";
            command.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            command.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            command.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            command.Parameters.AddWithValue("@Hiredate", NewTeacher.HireDate);
            command.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            command.Prepare();

            //Run the query in db
            command.ExecuteNonQuery();

            //Close the db connection
            Connection.Close();
        }

        // POST
        /// <summary>
        /// Connect to database to update information changed by user
        /// </summary>
        /// <param name="id"> Teacher's id whose information is updated</param>
        /// <param name="TeacherInfo"> Teacher's Firstname, Lastname, Number, Hire date, and salary information with updated values</param>
        /// <returns>Taking user inputs (update/changes) and update the given data on database</returns>
        /// <example>
        /// POST : Teacher/UpdateTeacher/{id}
        /// POST DATA
        /// {
        /// "TeacherTname":"Dana",
        /// "TeacherTname":"Ford",
        /// "EmployeeNumber":"T401",
        /// "HireDate":"2015-10-23",
        /// "Salary":"71.15"
        /// }
        /// </example>
        [HttpPost]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //Create a connection
            MySqlConnection Connection = schoolDbContext.AccessDatabase();

            //Open the connection between local db and web server
            Connection.Open();

            //New command - Query for DB
            MySqlCommand command = Connection.CreateCommand();

            //Write SQL query
            command.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber, hiredate=@Hiredate, salary=@Salary where teacherid=@TeacherId";

            command.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            command.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            command.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            command.Parameters.AddWithValue("@Hiredate", TeacherInfo.HireDate);
            command.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            command.Parameters.AddWithValue("@TeacherId", id);
            command.Prepare();

            //Run the query in db
            command.ExecuteNonQuery();

            //Close the db connection
            Connection.Close();
        }
    }
}
