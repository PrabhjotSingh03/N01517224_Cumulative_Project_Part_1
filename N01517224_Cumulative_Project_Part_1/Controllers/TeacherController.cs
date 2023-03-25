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
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        [Route("/Teacher/List/{SearchKey}")]
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET: Teacher/Show/id
        [Route("Teacher/Show/{id}")]
        public ActionResult Show(int teacherid)
        {
            TeacherDataController controller = new TeacherDataController();

            TeacherCourse TeacherCourse = controller.FindTeacher(teacherid);

            //routes the single teacher information to Show.cshtml
            return View(TeacherCourse);
        }
    }
}