namespace POCO
{
    public class DegreeAudit
    {
        public string StudentId { get; set; }

        public string CourseId { get; set; }        

        public string CourseGrade { get; set; }

        public Course Course { get; set; }

        public override string ToString()
        {
            return this.StudentId + "-" + this.CourseId + "-" + this.CourseGrade;
        }
    }
}
