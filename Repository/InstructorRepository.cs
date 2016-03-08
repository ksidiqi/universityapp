namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class InstructorRepository : BaseRepository, IInstructorRepository
    {
        // const string
        private const string InsertInstructorProcedure = "spInsertInstructor";
        private const string UpdateInstructorProcedure = "spUpdateInstructor";
        private const string DeleteInstructorProcedure = "spDeleteInstructor";
        private const string GetInstructorListProcedure = "spGetInstructorList";
        private const string GetInstructorInformationProcedure = "spGetInstructorInformation";

        // new method start from here (assignment #2)
        public void InsertInstructor(Instructor instructor, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertInstructorProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                ////adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 50));

                ////adapter.SelectCommand.Parameters["@instructor_id"].Value = instructor.Id;
                adapter.SelectCommand.Parameters["@first_name"].Value = instructor.FirstName;
                adapter.SelectCommand.Parameters["@last_name"].Value = instructor.LastName;
                adapter.SelectCommand.Parameters["@title"].Value = instructor.Title;

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

        public void UpdateInstructor(Instructor instructor, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateInstructorProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 50));

                adapter.SelectCommand.Parameters["@instructor_id"].Value = instructor.Id;
                adapter.SelectCommand.Parameters["@first_name"].Value = instructor.FirstName;
                adapter.SelectCommand.Parameters["@last_name"].Value = instructor.LastName;
                adapter.SelectCommand.Parameters["@title"].Value = instructor.Title;

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

        public void DeleteInstructor(Instructor instructor, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(DeleteInstructorProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@instructor_id"].Value = instructor.Id;
                
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

        public List<Instructor> GetInstructorList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var instructorList = new List<Instructor>();

            try
            {
                var adapter = new SqlDataAdapter(GetInstructorListProcedure, conn)
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
                    var instructor = new Instructor
                    {
                        Id = (int)dataSet.Tables[0].Rows[i]["instructor_id"],
                        FirstName = dataSet.Tables[0].Rows[i]["first_name"].ToString(),
                        LastName = dataSet.Tables[0].Rows[i]["last_name"].ToString(),
                        Title = dataSet.Tables[0].Rows[i]["title"].ToString()                  
                    };
                    instructorList.Add(instructor);
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

            return instructorList;
        }

        public List<Instructor> GetInstructorInformation(string instructor_id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var instructorInfo = new List<Instructor>();

            try
            {
                var adapter = new SqlDataAdapter(GetInstructorInformationProcedure, conn);

                if (instructor_id.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@instructor_id"].Value = instructor_id;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var instructor = new Instructor
                    {
                        Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),
                        FirstName = dataSet.Tables[0].Rows[i]["first_name"].ToString(),
                        LastName = dataSet.Tables[0].Rows[i]["last_name"].ToString(),
                        Title = dataSet.Tables[0].Rows[i]["title"].ToString()                     
                    };
                    instructorInfo.Add(instructor);
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

            return instructorInfo;
        }
    }
}
