using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N01517224_Cumulative_Project_Part_1.Models;
using System.Diagnostics;

namespace N01517224_Cumulative_Project_Part_1.Controllers
{
    public class ClassesController : Controller
    {
            // GET: Course/List
            // GET: Course/List?searchText=Web
            public ActionResult List(string searchText = null)
            {
                ClassesDataController Classes = new ClassesDataController();
                List<Classes> courses = Classes.ListCourses(searchText);
                return View(courses);
            }

            // GET: /Course/show/{id}
            public ActionResult Show(int id)
            {
            ClassesDataController Classes = new ClassesDataController();
            Classes courseDetails = Classes.GetCourse(id);
                return View(courseDetails);
            }
        }
}