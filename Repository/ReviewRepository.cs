namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
  
    using IRepository;

    using POCO;

    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        private const string GetReviewList = "spGetReviewList";
        private const string DeleteReviewProcedure = "spDeleteReview";
        private const string InsertReviewProcedure = "spInsertStudentReview";
        private const string UpdateReviewProcedure = "updateReview";
        private const string GetStudentReviewProcedure = "getStudentReview";
        private const string GetInstructorReviewProcedure = "getInstructorReview";
        private const string GetCourseReviewProcedure = "getCourseReview";
        private const string InsertRatingProcedure = "spInsertRating";
        private const string UpdateRatingProcedure = "spUpdateRating";
        private const string DeleteRatingProcedure = "spDeleteRating";

        public List<Review> GetAllReview(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var reviewList = new List<Review>();

            try
            {
                var adapter = new SqlDataAdapter(GetReviewList, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var review = new Review
                    {
                        Student = dataSet.Tables[0].Rows[i]["student_id"].ToString(),
                        Course = dataSet.Tables[0].Rows[i]["schedule_id"].ToString(),
                        InstructorRating = dataSet.Tables[0].Rows[i]["rec_instructor"].ToString(),
                        CourseRating = dataSet.Tables[0].Rows[i]["rec_course"].ToString(),
                        ExamRepMaterialRating = dataSet.Tables[0].Rows[i]["exams_rep_material"].ToString(),
                        InstructorClearRating = dataSet.Tables[0].Rows[i]["instructor_clear"].ToString(),
                        ExpectedGrade = dataSet.Tables[0].Rows[i]["expected_grade"].ToString(),
                        CourseDifficulty = dataSet.Tables[0].Rows[i]["course_difficult"].ToString(),
                        HoursStudy = dataSet.Tables[0].Rows[i]["hours_study"].ToString()
                    };
                    reviewList.Add(review);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return reviewList;
        }

        public void InsertReview(Review r, ref List<string> errors) 
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertReviewProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType
                            =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@rec_instructor", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@rec_course", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@exams_rep_material", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_clear", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@expected_grade", SqlDbType.VarChar, 10));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_difficult", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@hours_study", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = r.Student;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = r.Course;
                adapter.SelectCommand.Parameters["@rec_instructor"].Value = r.InstructorRating;
                adapter.SelectCommand.Parameters["@rec_course"].Value = r.CourseRating;
                adapter.SelectCommand.Parameters["@exams_rep_material"].Value = r.ExamRepMaterialRating;
                adapter.SelectCommand.Parameters["@instructor_clear"].Value = r.InstructorClearRating;
                adapter.SelectCommand.Parameters["@expected_grade"].Value = r.ExpectedGrade;
                adapter.SelectCommand.Parameters["@course_difficult"].Value = r.CourseDifficulty;
                adapter.SelectCommand.Parameters["@hours_study"].Value = r.HoursStudy;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void DeleteReview(Review r, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteReviewProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = r.Student;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = r.Course;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void UpdateReview(Review original, Review updated, ref List<string> errors) 
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateReviewProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@rec_instructor", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@rec_course", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@exams_rep_material", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_clear", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@expected_grade", SqlDbType.VarChar, 10));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_difficult", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@hours_study", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = original.Student;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = original.Course;
                adapter.SelectCommand.Parameters["@rec_instructor"].Value = updated.InstructorRating;
                adapter.SelectCommand.Parameters["@rec_course"].Value = updated.CourseRating;
                adapter.SelectCommand.Parameters["@exams_rep_material"].Value = updated.ExamRepMaterialRating;
                adapter.SelectCommand.Parameters["@instructor_clear"].Value = updated.InstructorClearRating;
                adapter.SelectCommand.Parameters["@expected_grade"].Value = updated.ExpectedGrade;
                adapter.SelectCommand.Parameters["@course_difficult"].Value = updated.CourseDifficulty;
                adapter.SelectCommand.Parameters["@hours_study"].Value = updated.HoursStudy;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public List<Review> GetStudentReview(Student s, ref List<string> errors) 
        {
            string student_id = s.StudentId;
            string colmn = "@student_id";
            return this.GetReviewWhere(new SqlParameter(colmn, SqlDbType.VarChar, 20), colmn, student_id, GetStudentReviewProcedure, ref errors); 
        }

        public List<Review> GetClassReview(Schedule sc, ref List<string> errors) 
        {
            int course_id = sc.ScheduleId;
            string colmn = "@schedule_id";
            var list = this.GetReviewWhere(new SqlParameter(colmn, SqlDbType.Int), colmn, course_id + string.Empty, GetCourseReviewProcedure, ref errors);
            return list; 
        }

        public List<Review> GetInstructorReview(int instructor_id, ref List<string> errors)
        {
            string colmn = "@instructor_id";
            return this.GetReviewWhere(new SqlParameter(colmn, SqlDbType.Int), colmn, instructor_id + string.Empty, GetInstructorReviewProcedure, ref errors);
        }

        private List<Review> GetReviewWhere(SqlParameter param, string column, string columnValue, string procedure, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var reviewList = new List<Review>();

            try
            {
                var adapter = new SqlDataAdapter(procedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };

                adapter.SelectCommand.Parameters.Add(param);

                adapter.SelectCommand.Parameters[column].Value = columnValue;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {                   
                    var review = new Review
                    {
                        Student = dataSet.Tables[0].Rows[i]["student_id"].ToString(),
                        Course = dataSet.Tables[0].Rows[i]["schedule_id"].ToString(),
                        InstructorRating = dataSet.Tables[0].Rows[i]["rec_instructor"].ToString(),
                        CourseRating = dataSet.Tables[0].Rows[i]["rec_course"].ToString(),
                        ExamRepMaterialRating = dataSet.Tables[0].Rows[i]["exams_rep_material"].ToString(),
                        InstructorClearRating = dataSet.Tables[0].Rows[i]["instructor_clear"].ToString(),
                        ExpectedGrade = dataSet.Tables[0].Rows[i]["expected_grade"].ToString(),
                        CourseDifficulty = dataSet.Tables[0].Rows[i]["course_difficult"].ToString(),
                        HoursStudy = dataSet.Tables[0].Rows[i]["hours_study"].ToString()
                    };
                    reviewList.Add(review);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return reviewList;
        }
    }
}
