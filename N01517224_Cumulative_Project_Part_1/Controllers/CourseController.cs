using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N01517224_Cumulative_Project_Part_1.Models;
using System.Diagnostics;

namespace N01517224_Cumulative_Project_Part_1.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course/List
        // GET: Course/List?searchText=Web
        public ActionResult List(string searchText = null)
        {
            CourseDataController courseDataController = new CourseDataController();
            List<Subject> courses = courseDataController.ListCourses(searchText);
            return View(courses);
        }

        // GET: /Course/show/{id}
        public ActionResult Show(int id)
        {
            CourseDataController courseDataController = new CourseDataController();
            Subject courseDetails = courseDataController.GetCourse(id);
            return View(courseDetails);
        }
    }
}