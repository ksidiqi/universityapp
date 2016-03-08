namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class CourseService
    {
        private readonly ICourseRepository repository;

        public CourseService(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public List<Course> GetCourseList(ref List<string> errors)
        {
            return this.repository.GetCourseList(ref errors);
        }

        public List<Course> GetCourseById(int id, ref List<string> errors)
        {
            return this.repository.GetCourseById(id, ref errors);
        }

        public List<PreReqCourse> GetPreReqList(int id, ref List<string> errors)
        {
            return this.repository.GetPreReqList(id, ref errors);
        }

        public void InsertCourse(Course course, ref List<string> errors)
        {
            this.repository.InsertCourse(course, ref errors);
        }

        public void UpdateCourse(Course course, ref List<string> errors)
        {
            if (course.CourseId == null)
            {
                errors.Add("Course ID cannot be null");
                throw new ArgumentException();
            }

            if (course.Title == null)
            {
                errors.Add("Course Title cannot be null");
                throw new ArgumentException();
            }

            if (course.Description == null)
            {
                errors.Add("Course Description cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateCourse(course, ref errors);
        }

        public void DeleteCourse(Course course, ref List<string> errors)
        {           
            this.repository.DeleteCourse(course, ref errors);
        }

        public void InsertPreReqCourse(PreReqCourse course, ref List<string> errors)
        {
            if (course.CourseId.Equals(0))
            {
                errors.Add("Course ID cannot be null");
                throw new ArgumentException();
            }

            if (course.PreReqId.Equals(0))
            {
                errors.Add("PreReq ID cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertPreReqCourse(course, ref errors);
        }

        public void DeletePreReqCourse(PreReqCourse course, ref List<string> errors)
        {
            if (course.CourseId == 0)
            {
                errors.Add("Course ID cannot be null");
                throw new ArgumentException();
            }

            if (course.CourseId == 0)
            {
                errors.Add("PreReq ID cannot be null");
                throw new ArgumentException();
            }

            this.repository.DeletePreReqCourse(course, ref errors);
        }
    }
}
