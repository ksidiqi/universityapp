namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IMajorRepository
    {
        void InsertMajor(Major major, ref List<string> errors);

        void UpdateMajor(Major major, ref List<string> errors);

        void DeleteMajor(string major_code, ref List<string> errors);

        List<Major> GetMajorList(ref List<string> errors);
    }
}
