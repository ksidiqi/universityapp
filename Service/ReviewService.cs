namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using IRepository;
    using POCO;

    public class ReviewService
    {
        private static Dictionary<string, int> gradeValuesDict = new Dictionary<string, int>();
        private readonly IReviewRepository repository;
        private readonly string studentId = @"[a-zA-z]{1}\d{6}";
        private readonly string courseId = @"\d";

        static ReviewService()
        {
            gradeValuesDict.Add("A+", 13);
            gradeValuesDict.Add("A", 12);
            gradeValuesDict.Add("A-", 11);
            gradeValuesDict.Add("B+", 10);
            gradeValuesDict.Add("B", 9);
            gradeValuesDict.Add("B-", 8);
            gradeValuesDict.Add("C+", 7);
            gradeValuesDict.Add("C", 6);
            gradeValuesDict.Add("C-", 5);
            gradeValuesDict.Add("D+", 4);
            gradeValuesDict.Add("D", 3);
            gradeValuesDict.Add("D-", 2);
            gradeValuesDict.Add("F", 1);
        }

        public ReviewService(IReviewRepository repository)
        {
            this.repository = repository;
        }

        public void InsertReview(Review review, ref List<string> errors)
        {
            if (review == null)
            {
                errors.Add("Review cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(review.Student) || !Regex.IsMatch(review.Student, this.studentId))
            {
                errors.Add("Invalid ID cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(review.Course) || !Regex.IsMatch(review.Course, this.courseId))
            {
                errors.Add("Invalid Course cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertReview(review, ref errors);
        }

        public void UpdateReview(Review review, ref List<string> errors)
        {
            if (review == null)
            {
                errors.Add("Review cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(review.Student) || !Regex.IsMatch(review.Student, this.studentId))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (review.Student.Length < 5)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(review.Course) || !Regex.IsMatch(review.Course, this.courseId))
            {
                errors.Add("Invalid course id");
                throw new ArgumentException();
            }

            this.repository.UpdateReview(new Review() { Student = review.Student, Course = review.Course }, review, ref errors);
        }

        public void DeleteReview(string studentId, string courseId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) || !Regex.IsMatch(studentId, studentId))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(courseId) || !Regex.IsMatch(courseId, courseId))
            {
                errors.Add("Invalid course id");
                throw new ArgumentException();
            }

            this.repository.DeleteReview(new Review() { Student = studentId, Course = courseId }, ref errors);
        }

        public List<Review> GetAllReview(ref List<string> errors)
        {
            var list = this.repository.GetAllReview(ref errors);
            if (list == null)
            {
                return new List<Review>(); 
            }

            return list;
        }

        public List<Review> GetStudentReview(string id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, this.studentId))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            var list = this.repository.GetStudentReview(new Student() { StudentId = id }, ref errors);
            if (list == null)
            {
                return new List<Review>();
            }

            return list;
        }

        public List<Review> GetInstructorReview(string instructorId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(instructorId) || !Regex.IsMatch(instructorId, instructorId))
            {
                errors.Add("Invalid Instructor id");
                throw new ArgumentException();
            }

            var list = this.repository.GetInstructorReview(Convert.ToInt32(instructorId), ref errors);
            if (list == null)
            {
                return new List<Review>();
            }

            return list;
        }

        public List<Review> GetCourseReview(string courseId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(courseId) || !Regex.IsMatch(courseId, courseId))
            {
                errors.Add("Invalid Instructor id");
                throw new ArgumentException();
            }

            return this.repository.GetClassReview(new Schedule() { ScheduleId = Convert.ToInt32(courseId) }, ref errors);
        }

        public Review GetAverageClassReview(string scheduleId, List<Review> reviews, ref List<string> errors)
        {
            if (reviews == null)
            {
                errors.Add("Invalid reviews id");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(scheduleId) || !Regex.IsMatch(scheduleId, this.courseId))
            {
                errors.Add("Invalid course id");
                throw new ArgumentException();
            }

            int[,] stat = new int[7, 2];

            var average = new Review();
            average.Course = scheduleId; 

            foreach (Review r in reviews)
            {
                if (!string.IsNullOrEmpty(r.InstructorRating))
                {
                    stat[0, 0] += Convert.ToInt32(r.InstructorRating);
                    stat[0, 1] += 1;
                }

                if (!string.IsNullOrEmpty(r.CourseRating))
                {
                    stat[1, 0] += Convert.ToInt32(r.CourseRating);
                    stat[1, 1] += 1;
                }

                if (!string.IsNullOrEmpty(r.ExamRepMaterialRating))
                {
                    stat[2, 0] += Convert.ToInt32(r.ExamRepMaterialRating);
                    stat[2, 1] += 1;
                }

                if (!string.IsNullOrEmpty(r.InstructorClearRating))
                {
                    stat[3, 0] += Convert.ToInt32(r.InstructorClearRating);
                    stat[3, 1] += 1;
                }

                if (!string.IsNullOrEmpty(r.ExpectedGrade))
                {
                    stat[4, 0] += Convert.ToInt32(GetGradeValue(r.ExpectedGrade));
                    stat[4, 1] += 1;
                }

                if (!string.IsNullOrEmpty(r.CourseDifficulty))
                {
                    stat[5, 0] += Convert.ToInt32(r.CourseDifficulty);
                    stat[5, 1] += 1;
                }

                if (!string.IsNullOrEmpty(r.HoursStudy))
                {
                    stat[6, 0] += Convert.ToInt32(r.HoursStudy);
                    stat[6, 1] += 1;
                }
            }

            double tmp = (double)stat[0, 0] / stat[0, 1];
            if (stat[0, 1] == 0)
            {
                tmp = 0.0;
            }

            average.InstructorRating = tmp.ToString();

            tmp = (double)stat[1, 0] / stat[1, 1];
            if (stat[1, 1] == 0)
            {
                tmp = 0.0;
            }

            average.CourseRating = tmp.ToString();

            tmp = (double)stat[2, 0] / stat[2, 1];
            if (stat[2, 1] == 0)
            {
                tmp = 0.0;
            }

            average.ExamRepMaterialRating = tmp.ToString();

            tmp = (double)stat[3, 0] / stat[3, 1];
            if (stat[3, 1] == 0)
            {
                tmp = 0.0;
            }

            average.InstructorClearRating = tmp.ToString();
            tmp = (double)stat[4, 0] / stat[4, 1];
            if (stat[4, 1] == 0)
            {
                tmp = 0.0;
            }

            average.ExpectedGrade = GetGradeString(Convert.ToInt32(tmp));
            tmp = (double)stat[5, 0] / stat[5, 1];
            if (stat[5, 1] == 0)
            {
                tmp = 0.0;
            }

            average.CourseDifficulty = tmp.ToString();

            tmp = (double)stat[6, 0] / stat[6, 1];
            if (stat[6, 1] == 0)
            {
                tmp = 0.0;
            }

            average.HoursStudy = tmp.ToString();

            return average; 
        }

        private static int GetGradeValue(string grade)
        {
            int value;
            try
            {
                value = gradeValuesDict[grade];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

            return value;
        }

        private static string GetGradeString(int grade)
        {
            foreach (KeyValuePair<string, int> pair in gradeValuesDict)
            {
                if (pair.Value.Equals(grade))
                {
                    return pair.Key;
                }
            }

            return "Na";
        }
    }
}
