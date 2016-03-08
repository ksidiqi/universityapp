namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class MajorService
    {
        private readonly IMajorRepository repository;

        public MajorService(IMajorRepository repository)
        {
            this.repository = repository;
        }

        public void InsertMajor(Major major, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(major.MajorCode))
            {
                errors.Add("major code cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(major.MajorDescription))
            {
                errors.Add("major description cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertMajor(major, ref errors);
        }

        public void UpdateMajor(Major major, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(major.MajorCode))
            {
                errors.Add("major code cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(major.MajorDescription))
            {
                errors.Add("major description cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateMajor(major, ref errors);
        }

        public void DeleteMajor(string major_code, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(major_code))
            {
                errors.Add("major code cannot be null");
                throw new ArgumentException();
            }

            this.repository.DeleteMajor(major_code, ref errors);
        }

        public List<Major> GetMajorList(ref List<string> errors)
        {
            return this.repository.GetMajorList(ref errors);
        }
    }
}
