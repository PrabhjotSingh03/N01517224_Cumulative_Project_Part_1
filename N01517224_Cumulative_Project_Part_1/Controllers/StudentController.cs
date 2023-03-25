using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N01517224_Cumulative_Project_Part_1.Models;
using System.Diagnostics;

namespace N01517224_Cumulative_Project_Part_1.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        // GET: Student/List
        // GET: Student/List?searchText=alex
        public ActionResult List(string SearchKey = null)
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);
            return View(Students);
        } 

        // GET: /Student/show/{id}
        public ActionResult Show(int studentid)
        {
            StudentDataController controller = new StudentDataController();
            Student studentDetails = controller.FindStudents(studentid);
            return View(studentDetails);
        }
    }
}