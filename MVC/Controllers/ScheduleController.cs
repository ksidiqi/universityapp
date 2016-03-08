namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class ScheduleController : Controller
    {
        public ActionResult Index(string year, string quarter)
        {
            ViewBag.year = year;
            ViewBag.quarter = quarter;
            return this.View();
        }

        public ActionResult EditSchedule(int id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult CreateSchedule()
        {
            return this.View();
        }
    }
}
