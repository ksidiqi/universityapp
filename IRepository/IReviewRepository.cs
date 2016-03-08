namespace IRepository
{
    using System; 
    using System.Collections.Generic;   
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using POCO;

    public interface IReviewRepository
    {
        void InsertReview(Review r, ref List<string> errors);

        void DeleteReview(Review r, ref List<string> errors);

        void UpdateReview(Review original, Review updated, ref List<string> errors);

        List<Review> GetAllReview(ref List<string> errors);

        List<Review> GetStudentReview(Student s, ref List<string> errors);

        List<Review> GetClassReview(Schedule sc, ref List<string> errors);

        List<Review> GetInstructorReview(int id, ref List<string> errors);
    }
}