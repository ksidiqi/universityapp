namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class GradeDistributionRepository : BaseRepository, IGradeDistributionRepository
    {
        // const string
        private const string AddGradeDistributionProcedure = "spAddGradeDistribution";
        private const string DeleteGradeDistributionProcedure = "spDeleteGradeDistribution";
        private const string UpdateGradeDistributionProcedure = "spUpdateGradeDistribution";
        private const string GetGradeDistributionProcedure = "spgetGradesDistribution";

        public void AddGradeDistribution(string grade_id, string grade_description, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(AddGradeDistributionProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };             
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@grade_id", SqlDbType.VarChar, 10));     
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@grade_description", SqlDbType.VarChar, 40));    
  
                adapter.SelectCommand.Parameters["@grade_id"].Value = grade_id;
                adapter.SelectCommand.Parameters["@grade_description"].Value = grade_description;
               
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
        
        public void UpdateGradeDistribution(string grade_id, string grade_description, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateGradeDistributionProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@grade_id", SqlDbType.VarChar, 10));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@grade_description", SqlDbType.VarChar, 40));

                adapter.SelectCommand.Parameters["@grade_id"].Value = grade_id;
                adapter.SelectCommand.Parameters["@grade_description"].Value = grade_description;

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

        public void DeleteGradeDistribution(string grade_id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(DeleteGradeDistributionProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@grade_id", SqlDbType.VarChar, 10));
                
                adapter.SelectCommand.Parameters["@grade_id"].Value = grade_id;
                
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

        public List<Grades> GetGradeDistribution(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var gradeList = new List<Grades>();

            try
            {
                var adapter = new SqlDataAdapter(GetGradeDistributionProcedure, conn)
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
                    var grade = new Grades
                                     {
                                         GradeId = dataSet.Tables[0].Rows[i]["course_id"].ToString(),
                                         GradeDescription = dataSet.Tables[0].Rows[i]["course_title"].ToString(),
                                     };
                    gradeList.Add(grade);
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

            return gradeList;
        }
    }
}
