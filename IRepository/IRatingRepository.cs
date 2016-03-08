namespace IRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using POCO;

    public interface IRatingRepository
    {
        void InsertRating(string ratingCode, string ratingDescription, ref List<string> errors);

        void DeleteRating(string ratingCode, ref List<string> errors);

        void UpdateRating(string ratingCode, string ratingDescription, ref List<string> errors);
    }
}
