namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class InstructorService
    {
        private readonly IInstructorRepository repository;

        public InstructorService(IInstructorRepository repository)
        {
            this.repository = repository;
        }

        public List<Instructor> GetInstructorList(ref List<string> errors)
        {
            return this.repository.GetInstructorList(ref errors);
        }

        public List<Instructor> GetInstructorInformation(string id, ref List<string> errors)
        {
            return this.repository.GetInstructorInformation(id, ref errors);
        }

        public void InsertInstructor(Instructor instructor, ref List<string> errors)
        {
            if (instructor.Title == null)
            {
                errors.Add("instructor Title cannot be null");
                throw new ArgumentException();
            }

            if (instructor.FirstName == null)
            {
                errors.Add("First Name cannot be null");
                throw new ArgumentException();
            }

            if (instructor.LastName == null)
            {
                errors.Add("Last Name cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertInstructor(instructor, ref errors);
        }

        public void UpdateInstructor(Instructor instructor, ref List<string> errors)
        {
            if (instructor.Title == null)
            {
                errors.Add("instructor Title cannot be null");
                throw new ArgumentException();
            }

            if (instructor.FirstName == null)
            {
                errors.Add("First Name cannot be null");
                throw new ArgumentException();
            }

            if (instructor.LastName == null)
            {
                errors.Add("Last Name cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateInstructor(instructor, ref errors);
        }

        public void DeleteInstructor(Instructor instructor, ref List<string> errors)
        {         
            this.repository.DeleteInstructor(instructor, ref errors);
        }
    }
}
