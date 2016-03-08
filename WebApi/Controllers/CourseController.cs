namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class CourseController : ApiController
    {
        [HttpGet]
        public List<Course> GetCourseList()
        {
            var service = new CourseService(new CourseRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetCourseList(ref errors);
        }

        [HttpGet]
        public List<Course> GetCourseById(int id)
        {
            var service = new CourseService(new CourseRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetCourseById(id, ref errors);
        }

        [HttpGet]
        public List<PreReqCourse> GetPreReqList(int courseId)
        {
            var service = new CourseService(new CourseRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetPreReqList(courseId, ref errors);
        }

        [HttpPost]
        public string InsertCourse(Course course)
        {
            var errors = new List<string>();
            var repository = new CourseRepository();
            var service = new CourseService(repository);
            //// we could log the errors here if there are any...
            service.InsertCourse(course, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateCourse(Course course)
        {
            var errors = new List<string>();
            var repository = new CourseRepository();
            var service = new CourseService(repository);
            //// we could log the errors here if there are any...
            service.UpdateCourse(course, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteCourse(Course course)
        {
            var errors = new List<string>();
            var repository = new CourseRepository();
            var service = new CourseService(repository);
            service.DeleteCourse(course, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string InsertPreReqCourse(PreReqCourse course)
        {
            var errors = new List<string>();
            var repository = new CourseRepository();
            var service = new CourseService(repository);
            service.InsertPreReqCourse(course, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeletePreReqCourse(PreReqCourse course)
        {
            var errors = new List<string>();
            var repository = new CourseRepository();
            var service = new CourseService(repository);
            service.DeletePreReqCourse(course, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
        //// you can add more [HttpGet] and [HttpPost] methods as you need
    }
}