namespace MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult InsertReview(
            string student_id,
            string schedule_id,
            string rec_instructor,
            string rec_course,
            string exams_rep_material,
            string instructor_clear,
            string expected_grade,
            string course_difficult,
            string hours_study)
        {
            ViewBag.student_id = student_id;
            ViewBag.schedule_id = schedule_id;
            ViewBag.rec_instructor = rec_instructor;
            ViewBag.rec_course = rec_course;
            ViewBag.exams_rep_material = exams_rep_material;
            ViewBag.instructor_clear = instructor_clear;
            ViewBag.expected_grade = expected_grade;
            ViewBag.course_difficult = course_difficult;
            ViewBag.hours_study = hours_study;
            return this.ViewReview(student_id);
        }

        public ActionResult UpdateReview(
            string student_id,
            string schedule_id,
            string rec_instructor,
            string rec_course,
            string exams_rep_material,
            string instructor_clear,
            string expected_grade,
            string course_difficult,
            string hours_study)
        {
            ViewBag.student_id = student_id;
            ViewBag.schedule_id = schedule_id;
            ViewBag.rec_instructor = rec_instructor;
            ViewBag.rec_course = rec_course;
            ViewBag.exams_rep_material = exams_rep_material;
            ViewBag.instructor_clear = instructor_clear;
            ViewBag.expected_grade = expected_grade;
            ViewBag.course_difficult = course_difficult;
            ViewBag.hours_study = hours_study;
            return this.ViewReview(student_id);
        }

        public ActionResult ViewReview(string id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult ViewReviewProf()
        {
            return this.View();
        }

        public ActionResult ViewReviewStaff()
        {
            return this.View();
        }

        public ActionResult ViewClassAverageReview()
        {
            return this.View();
        }
    }
}