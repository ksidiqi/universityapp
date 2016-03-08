namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IInstructorRepository
    {
        void InsertInstructor(Instructor instructor, ref List<string> errors);

        void UpdateInstructor(Instructor instructor, ref List<string> errors);

        void DeleteInstructor(Instructor instructor, ref List<string> errors);

        List<Instructor> GetInstructorList(ref List<string> errors);

        List<Instructor> GetInstructorInformation(string instructor_id, ref List<string> errors);
    }
}
