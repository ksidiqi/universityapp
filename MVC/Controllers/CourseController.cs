namespace MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetPreReq(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }
    }
}