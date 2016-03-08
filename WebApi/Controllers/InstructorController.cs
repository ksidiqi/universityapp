namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class InstructorController : ApiController
    {
        [HttpGet]
        public List<Instructor> GetInstructorList()
        {
            var service = new InstructorService(new InstructorRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetInstructorList(ref errors);
        }

        [HttpGet]
        public List<Instructor> GetInstructorInformation(string id)
        {
            var service = new InstructorService(new InstructorRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetInstructorInformation(id, ref errors);
        }

        [HttpPost]
        public string InsertIntructor(Instructor instructor)
        {
            var errors = new List<string>();
            var repository = new InstructorRepository();
            var service = new InstructorService(repository);
            //// we could log the errors here if there are any...
            service.InsertInstructor(instructor, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string Update(Instructor instructor)
        {
            var errors = new List<string>();
            var repository = new InstructorRepository();
            var service = new InstructorService(repository);
            //// we could log the errors here if there are any...
            service.UpdateInstructor(instructor, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteInstructor(Instructor instructor)
        {
            var errors = new List<string>();
            var repository = new InstructorRepository();
            var service = new InstructorService(repository);
            service.DeleteInstructor(instructor, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }     
    }
}