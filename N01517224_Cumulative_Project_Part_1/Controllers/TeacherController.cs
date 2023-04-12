using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N01517224_Cumulative_Project_Part_1.Models;
using System.Diagnostics;

namespace N01517224_Cumulative_Project_Part_1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/List
        // GET: Teacher/List?searchText=alex
        public ActionResult List(string searchText = null)
        {
            TeacherDataController teacherDataController = new TeacherDataController();
            List<Teacher> teachers = teacherDataController.ListTeachers(searchText);
            return View(teachers);
        }

        // GET: /Teacher/show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController teacherDataController = new TeacherDataController();
            Teacher teacherDetails = teacherDataController.GetTeacher(id);
            return View(teacherDetails);
        }
        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.GetTeacher(id);

            return View(NewTeacher);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //reach api controller and delete teacher based on teacher's id 
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            //After delete, direct to teacher list
            return RedirectToAction("List");
        }


        //GET : /Teacher/New
        //Page to add new teacher
        public ActionResult New()
        {
            return View();
        }


        //POST /Teacher/Create
        //It will get a new teacher information and after submission, it will add it into db
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime Hiredate, double Salary)
        {

            //Add all given inputs into an object
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = Hiredate;
            NewTeacher.Salary = Convert.ToDecimal(Salary);

            //Reach Api controller and call AddTeacher, let the query insert into the db
            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            //After submission show teacher list
            return RedirectToAction("List");
        }
    }
}
  