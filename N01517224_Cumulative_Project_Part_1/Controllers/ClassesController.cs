using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace N01517224_Cumulative_Project_Part_1.Controllers
{
    public class ClassesController : Controller
    {
            // GET: Course/List
            // GET: Course/List?searchText=Web
            public ActionResult List(string searchText = null)
            {
                ClassesDataController classes = new ClassesDataController();
                List<Subject> courses = classes.ListCourses(searchText);
                return View(courses);
            }

            // GET: /Course/show/{id}
            public ActionResult Show(int id)
            {
            ClassesDataController Classes = new ClassesDataController();
                Subject courseDetails = Classes.GetCourse(id);
                return View(courseDetails);
            }
        }
}