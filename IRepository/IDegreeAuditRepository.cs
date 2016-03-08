namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IDegreeAuditRepository
    {
        void InsertDegreeAudit(DegreeAudit degree_audit, ref List<string> errors);

        void UpdateDegreeAudit(DegreeAudit degree_audit, ref List<string> errors);

        void DeleteDegreeAudit(DegreeAudit degree_audit, ref List<string> errors);

        List<DegreeAudit> GetDegreeAudit(string studentId, ref List<string> errors);

        List<DegreeAudit> GetDegreeAuditPerId(string studentId, string courseId, ref List<string> errors);
    }
}
