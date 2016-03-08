namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IGradeDistributionRepository
    {
        void AddGradeDistribution(string grade_id, string grade_description, ref List<string> errors);

        void UpdateGradeDistribution(string grade_id, string grade_description, ref List<string> errors);

        void DeleteGradeDistribution(string grade_id, ref List<string> errors);

        List<Grades> GetGradeDistribution(ref List<string> errors);
    }
}
