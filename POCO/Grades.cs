namespace POCO
{
    public class Grades
    {
        public string GradeId { get; set; }

        public string GradeDescription { get; set; }

        public override string ToString()
        {
            return this.GradeId + "-" + this.GradeDescription;
        }
    }
}
