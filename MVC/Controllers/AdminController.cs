namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class AdminController : Controller
    {
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult StudentList(string id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult CourseList(string id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult EditCourse(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult ManageInstructor(string id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult UpdateInstructor(string id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult HireInstructor()
        {
            return this.View();
        }

        public ActionResult ViewReviewProf(string id)
        {
            ViewBag.id = id;
            return this.View();
        }
    }
}
