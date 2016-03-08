namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class DegreeAuditService
    {
        private readonly IDegreeAuditRepository repository;

        public DegreeAuditService(IDegreeAuditRepository repository)
        {
            this.repository = repository;
        }

        public void InsertDegreeAudit(DegreeAudit degree_audit, ref List<string> errors)
        {
            if (degree_audit.StudentId == null)
            {
                errors.Add("Student Id cannot be null");
                throw new ArgumentException();
            }

            if (degree_audit.CourseId == null)
            {
                errors.Add("Student Id cannot be null");
                throw new ArgumentException();
            }

            if (degree_audit.CourseGrade == null)
            {
                errors.Add("Student Id cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertDegreeAudit(degree_audit, ref errors);
        }

        public void UpdateDegreeAudit(DegreeAudit degree_audit, ref List<string> errors)
        {
            if (degree_audit.StudentId == null)
            {
                errors.Add("Student Id cannot be null");
                throw new ArgumentException();
            }

            if (degree_audit.CourseId == null)
            {
                errors.Add("Student Id cannot be null");
                throw new ArgumentException();
            }

            if (degree_audit.CourseGrade == null)
            {
                errors.Add("Student Id cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateDegreeAudit(degree_audit, ref errors);
        }

        public void DeleteDegreeAudit(DegreeAudit deg, ref List<string> errors)
        {         
            this.repository.DeleteDegreeAudit(deg, ref errors);
        }

        public List<DegreeAudit> GetDegreeAudit(string studentId, ref List<string> errors)
        {
            return this.repository.GetDegreeAudit(studentId, ref errors);
        }

        public List<DegreeAudit> GetDegreeAuditPerId(string studentId, string courseId, ref List<string> errors)
        {
            return this.repository.GetDegreeAuditPerId(studentId, courseId, ref errors);
        }
    }
}
