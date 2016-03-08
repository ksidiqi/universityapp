namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class DegreeAuditRepository : BaseRepository, IDegreeAuditRepository
    {
        // const string
        private const string AddDegreeAudits = "spInsertStudentDegreeAudit";
        private const string UpdateDegreeAudits = "spUpdateStudentDegreeAudit";
        private const string DeleteDegreeAudits = "spDeleteStudentDegreeAudit";
        private const string GetDegreeAudits = "spGetDegreeAudit";

        // new method start from here (assignment #2)
        public void InsertDegreeAudit(DegreeAudit degree_audit, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(AddDegreeAudits, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_grade", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = degree_audit.StudentId;
                adapter.SelectCommand.Parameters["@course_id"].Value = degree_audit.CourseId;
                adapter.SelectCommand.Parameters["@course_grade"].Value = degree_audit.CourseGrade;

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

        public void UpdateDegreeAudit(DegreeAudit degree_audit, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateDegreeAudits, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_grade", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = degree_audit.StudentId;
                adapter.SelectCommand.Parameters["@course_id"].Value = degree_audit.CourseId;
                adapter.SelectCommand.Parameters["@course_grade"].Value = degree_audit.CourseGrade;

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

        public void DeleteDegreeAudit(DegreeAudit degreeAudit, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(DeleteDegreeAudits, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = degreeAudit.StudentId;
                adapter.SelectCommand.Parameters["@course_id"].Value = degreeAudit.CourseId;

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

        public List<DegreeAudit> GetDegreeAudit(string studentId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var degreeAuditList = new List<DegreeAudit>();

            try
            {
                var adapter = new SqlDataAdapter(GetDegreeAudits, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = studentId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var degreeAudit = new DegreeAudit
                    {
                        StudentId = dataSet.Tables[0].Rows[i]["studentId"].ToString(),
                        CourseId = dataSet.Tables[0].Rows[i]["course_id"].ToString(),
                        CourseGrade = dataSet.Tables[0].Rows[i]["course_grade"].ToString(),
                        Course = new Course
                        {
                            Title = dataSet.Tables[0].Rows[i]["course_title"].ToString(),
                            Description = dataSet.Tables[0].Rows[i]["course_description"].ToString(),
                        }
                    };

                    degreeAuditList.Add(degreeAudit);

                    ////System.Diagnostics.Debug.Write(dataSet.Tables[0].Rows[0]["student_id"].ToString());
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

            return degreeAuditList;
        }

        public List<DegreeAudit> GetDegreeAuditPerId(string studentId, string courseId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var degreeAuditList = new List<DegreeAudit>();

            try
            {
                var adapter = new SqlDataAdapter(GetDegreeAudits, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = studentId;
                adapter.SelectCommand.Parameters["@course_id"].Value = studentId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var degreeAudit = new DegreeAudit
                    {
                        CourseId = dataSet.Tables[0].Rows[i]["course_id"].ToString(),
                        CourseGrade = dataSet.Tables[0].Rows[i]["course_grade"].ToString(),
                        Course = new Course
                        {
                            Title = dataSet.Tables[0].Rows[i]["course_title"].ToString(),
                            Description = dataSet.Tables[0].Rows[i]["course_description"].ToString(),
                        }
                    };

                    degreeAuditList.Add(degreeAudit);

                    ////System.Diagnostics.Debug.Write(dataSet.Tables[0].Rows[0]["student_id"].ToString());
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

            return degreeAuditList;
        }
    }
}
