namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface ICourseRepository
    {
        List<Course> GetCourseList(ref List<string> errors);

        // adding new stuff here for insert course, update course, delete course      
        void InsertCourse(Course course, ref List<string> errors);   

        void UpdateCourse(Course course, ref List<string> errors);

        void DeleteCourse(Course course, ref List<string> errors);

        void InsertPreReqCourse(PreReqCourse course, ref List<string> errors);

        void DeletePreReqCourse(PreReqCourse course, ref List<string> errors);

        List<Course> GetCourseById(int id, ref List<string> errors);

        List<PreReqCourse> GetPreReqList(int id, ref List<string> errors);
    }
}
