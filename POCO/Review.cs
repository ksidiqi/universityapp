namespace POCO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Review
    {
        public string Student { get; set; }

        public string Course { get; set; }

        public string InstructorRating { get; set; }

        public string CourseRating { get; set; }

        public string ExamRepMaterialRating { get; set; }

        public string InstructorClearRating { get; set; }

        public string ExpectedGrade { get; set; }

        public string CourseDifficulty { get; set; }

        public string HoursStudy { get; set; }

        public override string ToString()
        {
            return
                "-" + this.Student +
                "-" + this.Course +
                "-" + this.InstructorRating +
                "-" + this.CourseRating +
                "-" + this.ExamRepMaterialRating +
                "-" + this.InstructorClearRating +
                "-" + this.ExpectedGrade +
                "-" + this.CourseDifficulty +
                "-" + this.HoursStudy; 
        }
    }
}
