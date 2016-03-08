namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class CourseRepository : BaseRepository, ICourseRepository
    {
        private const string GetCourseListProcedure = "spGetCourseList";

        // add new const string
        private const string AddCourseProcedure = "spInsertCourse";
        private const string UpdateCourseProcedure = "spUpdateCourse";
        private const string DeleteCourseProcedure = "spDeleteCourse";
        private const string InsertPreReqCourseProcedure = "spInsertPreReqCourse";
        private const string DeletePreReqCourseProcedure = "spDeletePreReqCourse";
        private const string GetPreReqCourseProcedure = "spGetPreReqCourse";
        private const string GetCourseByIdProcedure = "spGetCourseListById";

        // new method start from here (assignment #2)
       
        /*
         *  insert course, for now still hard-coded parameter
         */
        public void InsertCourse(Course course, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(AddCourseProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_title", SqlDbType.VarChar, 100));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_level", SqlDbType.VarChar, 10));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_description", SqlDbType.VarChar, -1));

                adapter.SelectCommand.Parameters["@course_title"].Value = course.Title;
                adapter.SelectCommand.Parameters["@course_level"].Value = course.CourseLevel;
                adapter.SelectCommand.Parameters["@course_description"].Value = course.Description;
                
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

        /*
        *  Update course, for now still hard-coded parameter
        */
        public void UpdateCourse(Course course, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateCourseProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_title", SqlDbType.VarChar, 100));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_level", SqlDbType.VarChar, 10));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_description", SqlDbType.VarChar, -1));

                adapter.SelectCommand.Parameters["@course_id"].Value = course.CourseId;
                adapter.SelectCommand.Parameters["@course_title"].Value = course.Title;
                adapter.SelectCommand.Parameters["@course_level"].Value = course.CourseLevel;
                adapter.SelectCommand.Parameters["@course_description"].Value = course.Description;

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

        /*
         *  Delete Course, for now still hard-coded parameter for course_id
         * */
        public void DeleteCourse(Course course, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(DeleteCourseProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
               
                adapter.SelectCommand.Parameters["@course_id"].Value = course.CourseId;         

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

        public List<Course> GetCourseList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var courseList = new List<Course>();

            try
            {
                var adapter = new SqlDataAdapter(GetCourseListProcedure, conn)
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
                    var course = new Course
                                     {
                                         CourseId = dataSet.Tables[0].Rows[i]["course_id"].ToString(),
                                         Title = dataSet.Tables[0].Rows[i]["course_title"].ToString(),
                                         CourseLevel =
                                             (CourseLevel)
                                             Enum.Parse(
                                                 typeof(CourseLevel),
                                                 dataSet.Tables[0].Rows[i]["course_level"].ToString()),
                                         Description = dataSet.Tables[0].Rows[i]["course_description"].ToString()
                                     };
                    courseList.Add(course);
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

            return courseList;
        }

        public List<PreReqCourse> GetPreReqList(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var prereqlist = new List<PreReqCourse>();

            try
            {
                var adapter = new SqlDataAdapter(GetCourseByIdProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@course_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var prereq = new PreReqCourse
                    {
                        CourseId = (int)dataSet.Tables[0].Rows[i]["course_id"],
                        PreReqId = (int)dataSet.Tables[0].Rows[i]["prereq_id"],                      
                    };
                    prereqlist.Add(prereq);
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

            return prereqlist;
        }

        public List<Course> GetCourseById(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var course = new List<Course>();

            try
            {
                var adapter = new SqlDataAdapter(GetCourseByIdProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@course_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var courseList = new Course
                    {
                        CourseId = dataSet.Tables[0].Rows[i]["course_id"].ToString(),
                        Title = dataSet.Tables[0].Rows[i]["course_title"].ToString(),
                        CourseLevel =
                                              (CourseLevel)
                                              Enum.Parse(
                                                  typeof(CourseLevel),
                                                  dataSet.Tables[0].Rows[i]["course_level"].ToString()),
                        Description = dataSet.Tables[0].Rows[i]["course_description"].ToString(),                                            
                    };

                    course.Add(courseList);
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

            return course;
        }
        
        public void InsertPreReqCourse(PreReqCourse course, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertPreReqCourseProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@prereq_id", SqlDbType.Int));
                
                adapter.SelectCommand.Parameters["@course_id"].Value = course.CourseId;
                adapter.SelectCommand.Parameters["@prereq_id"].Value = course.PreReqId;
                
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

        public void DeletePreReqCourse(PreReqCourse course, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(DeletePreReqCourseProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@prereq_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@course_id"].Value = course.CourseId;
                adapter.SelectCommand.Parameters["@course_id"].Value = course.PreReqId;

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
    }
}
