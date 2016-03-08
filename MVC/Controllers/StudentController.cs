﻿namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class StudentController : Controller
    {
        public ActionResult Index(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Id = id;
            return this.View();       
        }

        public ActionResult CreateEnrollment(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult ViewDegreeAudit(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult ViewClassSchedule()
        {
            return this.View();
        }
    }
}
