namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class MajorController : ApiController
    {
        [HttpGet]
        public List<Major> GetMajorList()
        {
            var service = new MajorService(new MajorRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetMajorList(ref errors);
        }

        [HttpPost]
        public string InsertMajor(Major major)
        {
            var errors = new List<string>();
            var repository = new MajorRepository();
            var service = new MajorService(repository);
            //// we could log the errors here if there are any...
            service.InsertMajor(major, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateMajor(Major major)
        {
            var errors = new List<string>();
            var repository = new MajorRepository();
            var service = new MajorService(repository);
            //// we could log the errors here if there are any...
            service.UpdateMajor(major, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteMajor(string major_code)
        {
            var errors = new List<string>();
            var repository = new MajorRepository();
            var service = new MajorService(repository);
            //// we could log the errors here if there are any...
            service.DeleteMajor(major_code, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
        //// you can add more [HttpGet] and [HttpPost] methods as you need
    }
}