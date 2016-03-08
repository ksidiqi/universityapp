namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class StaffController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult StudentList()
        {
            return this.View();
        }

        public ActionResult CreateStudent()
        {
            return this.View();
        }

        public ActionResult EditStudent(string id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult UpdateDegreeAudit(string id, string cid)
        {
            ViewBag.id = id;
            ViewBag.cid = cid;
            return this.View();
        }

        public ActionResult StudentEnrollmentDetail(string id)
        {
            ViewBag.Id = id;
            return this.View();  
        }

        public ActionResult ViewStudentDegreeAudit(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }
    }
}
