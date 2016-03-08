namespace MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Demo()
        {
            return this.View();
        }

        public ActionResult ManageCourseSchedule()
        {
            return this.View();
        }
    }
}