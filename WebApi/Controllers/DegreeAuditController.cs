namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class DegreeAuditController : ApiController
    {
        [HttpGet]
        public List<DegreeAudit> GetStudentDegreeAudit(string id)
        {
            var errors = new List<string>();
            var repository = new DegreeAuditRepository();
            var service = new DegreeAuditService(repository);
            return service.GetDegreeAudit(id, ref errors);
        }

        [HttpGet]
        public List<DegreeAudit> GetDegreeAuditPerId(string id, string cid)
        {
            var errors = new List<string>();
            var repository = new DegreeAuditRepository();
            var service = new DegreeAuditService(repository);
            return service.GetDegreeAuditPerId(id, cid, ref errors);
        }

        [HttpPost]
        public string InsertDegreeAudit(DegreeAudit degree_audit)
        {
            var errors = new List<string>();
            var repository = new DegreeAuditRepository();
            var service = new DegreeAuditService(repository);
            //// we could log the errors here if there are any...
            service.InsertDegreeAudit(degree_audit, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateDegreeAudit(DegreeAudit degree_audit)
        {
            var errors = new List<string>();
            var repository = new DegreeAuditRepository();
            var service = new DegreeAuditService(repository);
            //// we could log the errors here if there are any...
            service.UpdateDegreeAudit(degree_audit, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteDegreeAudit(DegreeAudit degree_audit)
        {
            var errors = new List<string>();
            var repository = new DegreeAuditRepository();
            var service = new DegreeAuditService(repository);
            //// we could log the errors here if there are any...
            service.DeleteDegreeAudit(degree_audit, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}