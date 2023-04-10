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
    }
   }