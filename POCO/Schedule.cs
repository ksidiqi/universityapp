namespace POCO
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        public string Year { get; set; }

        public string Quarter { get; set; }

        public string Session { get; set; }

        public Course Course { get; set; }

        public int Schedule_day_id { get; set; }

        public int Schedule_time_id { get; set; }

        public string Schedule_day { get; set; }

        public string Schedule_time { get; set; }

        public Instructor Instructor { get; set; }

        public override string ToString()
        {
            return this.ScheduleId + "-" + this.Year + "-" + this.Quarter + "-" + this.Session + "-" + this.Course;
        }
    }
}
