namespace WebApi.Controllers
{
    using System; 
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class ReviewController : ApiController
    {
        [HttpGet]
        public List<Review> GetReviewList()
        {
            var errors = new List<string>();
            var repository = new ReviewRepository();
            var service = new ReviewService(repository);
            return service.GetAllReview(ref errors);
        }

        [HttpGet]
        public List<Review> GetStudentReview(string id)
        {
            var errors = new List<string>();
            var repository = new ReviewRepository();
            var service = new ReviewService(repository);
            return service.GetStudentReview(id, ref errors);
        }

        [HttpGet]
        public List<Review> GetCourseReview(string id)
        {
            var errors = new List<string>();
            var repository = new ReviewRepository();
            var service = new ReviewService(repository);
            return service.GetCourseReview(id, ref errors);
        }

        [HttpGet]
        public List<Review> GetProfessorReview(string id)
        {
            var errors = new List<string>();
            var repository = new ReviewRepository();
            var service = new ReviewService(repository);
            return service.GetInstructorReview(id, ref errors);
        }

        [HttpPost]
        public string InsertReview(Review r)
        {
            var errors = new List<string>();
            var repository = new ReviewRepository();
            var service = new ReviewService(repository);
            service.InsertReview(r, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }
            
            return "error";
        }

        [HttpPost]
        public string UpdateReview(Review review)
        {
            var errors = new List<string>();
            var repository = new ReviewRepository();
            var service = new ReviewService(repository);
            service.UpdateReview(review, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteReview(Review r)
        {
            string student = r.Student;
            string course = r.Course; 
            var errors = new List<string>();
            var repository = new ReviewRepository();
            var service = new ReviewService(repository);
            service.DeleteReview(student, course, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpGet]
        public Review GetAverageCourseReview(string id)
        {
            var errors = new List<string>();
            var repository = new ReviewRepository();
            var service = new ReviewService(repository);
            var list = service.GetCourseReview(id, ref errors); 
            return service.GetAverageClassReview(id, list, ref errors);
        }

        [HttpGet]
        public Review GetAverageProfCourseReview(string id)
        {
            var errors = new List<string>();
            var repository = new ReviewRepository();
            var service = new ReviewService(repository);
            var list = service.GetInstructorReview(id, ref errors);
            return service.GetAverageClassReview(id, list, ref errors);
        }
    }
}