namespace POCO
{
    public class PreReqCourse
    {
        public int CourseId { get; set; }

        public int PreReqId { get; set; }

        public override string ToString()
        {
            return this.CourseId + "-" + this.PreReqId;
        }
    }
}
